### Authored by Nicholas Padinha for Sitka Technology Group, 2019
### A PyQGIS pipeline for topologically-correcting a polygon layer
### by replacing overlaps between features with new features.
### Attributes are assigned to the new features based on the overlapping
### features' attributes. In this case the determination is made
### based on a domain-specific rule.
### Consult the diagram for a conceptual overview of the process

import sys

print ("starting")

from qgis.core import (
     QgsApplication, 
     QgsProcessingFeedback, 
     QgsVectorLayer,
     QgsFeatureRequest,
     QgsDataSourceUri,
     QgsFeature,
     QgsVectorFileWriter,
     QgsCoordinateReferenceSystem
)
from qgis.analysis import QgsNativeAlgorithms

print ("imported Qgis")

# Append the path where processing plugin can be found
sys.path.append(r'C:\OSGeo4W64\apps\qgis\python\plugins')

import processing
from processing.tools import dataobjects
from processing.core.Processing import Processing

import argparse

JOIN_PREFIX = "Joined_"
CONNSTRING_BASE = "CONNSTRING ERROR"
OUTPUT_PATH = "OUTPUT PATH ERROR"

def parseArguments():
    parser = argparse.ArgumentParser(description='Test PyQGIS connections to MSSQL')
    parser.add_argument('connstring', metavar='s', type=str, help='The connection string. Do not specify tables; the script will specify which table(s) it wants to look at.')
    parser.add_argument('output_path', metavar='d', type=str, help='The path to write the final output to.')


    args = parser.parse_args()

    # this is easier to write than anything sane
    global CONNSTRING_BASE
    global OUTPUT_PATH
    CONNSTRING_BASE = "MSSQL:" + args.connstring
    OUTPUT_PATH = args.output_path

    if not CONNSTRING_BASE.endswith(";"):
            CONNSTRING_BASE = CONNSTRING_BASE + ";"
    if "True" in CONNSTRING_BASE:
            CONNSTRING_BASE = CONNSTRING_BASE.replace("True", "Yes")

# Create an exact duplicate of the input layer.
# Needed because the processing framework cannot actually operate with the same layer as multiple inputs to an algorithm
# This is infuriating if you've ever used the QGIS GUI, where it looks like you can set the same layer for more than one input
# One concludes, after much trial and error, that the processing framework is actually operating on two separate copies of the layer behind the scenes.
def duplicateLayer(qgs_vector_layer, duplicate_layer_name):
    # fixme: we might never use layer types other than polygon, but we might want this parameterized in the future
    layer_dupe = QgsVectorLayer("MultiPolygon?crs=epsg:4326", duplicate_layer_name, "memory")

    mem_layer_data = layer_dupe.dataProvider()

    feats = [feat for feat in qgs_vector_layer.getFeatures()]
    attr = qgs_vector_layer.dataProvider().fields().toList()
    mem_layer_data.addAttributes(attr)
    layer_dupe.updateFields()
    mem_layer_data.addFeatures(feats)

    return layer_dupe

def assignFieldsToLayerFromSourceLayer(target, source):
    target_layer_data = target.dataProvider()
    source_layer_data = source.dataProvider()

    attr = source_layer_data.fields().toList()
    target_layer_data.addAttributes(attr)
    target.updateFields()



class Flatten:
    
    def __init__(self, working_layer, layer_identifier, compareFeaturesViaJoinedLayer, compareFeaturesViaSeparateLayers):
        
        # don't use the DB as the datasource
        self.working_layer = duplicateLayer(working_layer, "Candidate Layer")
        
        self.fields = self.working_layer.dataProvider().fields()
                
        self.layer_identifier = layer_identifier
        self.compareFeaturesViaJoinedLayer = compareFeaturesViaJoinedLayer
        self.compareFeaturesViaSeparateLayers = compareFeaturesViaSeparateLayers
        
    def run(self):
        # (2)(a) Collapse equality chains
        self.removeEqualitiesFromCandidateLayer()
        # (2)(b) Deal with withins
        self.handleInclusionsInCandidateLayer()
        # (2)(c) Deal with ordinary overlaps
        self.handleOverlapsInCandidateLayer()


    def removeEqualitiesFromCandidateLayer(self):
        print("Start collapse equality chains")
        print("Starting with {count} features".format(count = str(self.working_layer.featureCount())))

        dupe = duplicateLayer(self.working_layer, "Duplicate")
        join_prefix = JOIN_PREFIX

        res = processing.run("qgis:joinattributesbylocation", {
	        'INPUT': self.working_layer,
	        'JOIN': dupe,
	        'PREDICATE': ['2'], # 2 := Equals
	        'JOIN_FIELDS':'',
	        'METHOD':'0',
	        'DISCARD_NONMATCHING':False,
	        'PREFIX': join_prefix,
	        'OUTPUT':r'memory:equalities'
        }, context=PROCESSING_CONTEXT)

        equalities_layer = res['OUTPUT']

        # equality is reflexive, so this filter is wlog
        filter_icl = QgsFeatureRequest()
        filter_icl.setFilterExpression(self.lessThanIDFilterString())
        
        self.working_layer.startEditing()

        # for the equality predicate, we remove the losers from all future consideration
        for feat in equalities_layer.getFeatures(filter_icl):
            if self.compareFeatures(feat):   # left side loses
                self.removeFromCandidateLayer(feat[self.getLayerIdentifier(False)])
            else:
                self.removeFromCandidateLayer(feat[self.getLayerIdentifier(True)])                     # right side loses

        self.working_layer.commitChanges()
        print("Ending with {count} features".format(count = str(self.working_layer.featureCount())))
    
    def handleInclusionsInCandidateLayer(self):
        print("Start handle inclusions")
        print("Starting with {count} features".format(count = str(self.working_layer.featureCount())))

        dupe = duplicateLayer(self.working_layer, "Duplicate")
        join_prefix = JOIN_PREFIX

        res = processing.run("qgis:joinattributesbylocation", {
	        'INPUT': self.working_layer,
	        'JOIN': dupe,
	        'PREDICATE': ['2'], # 5 := Within
	        'JOIN_FIELDS':'',
	        'METHOD':'0',
	        'DISCARD_NONMATCHING':False,
	        'PREFIX': join_prefix,
	        'OUTPUT':r'memory:inclusions'
        }, context=PROCESSING_CONTEXT)

        inclusions_layer = res['OUTPUT']

        self.working_layer.startEditing()

        self.inclusion_count_this_iteration = 0

        # the smaller is removed if it loses; else it is retained for the next iteration
        for feat in inclusions_layer.getFeatures(self.neqIDFilterString()):
            self.inclusion_count_this_iteration += 1
            if self.compareFeatures(feat):   # smaller loses and is removed, never to be heard from again
                
                self.removeFromCandidateLayer(feat[self.getLayerIdentifier(False)])
            else:   
                # smaller (left) wins, subtract it from the larger (right) geometry
                # unless you're using FIDs (blech), QGIS can only supply a list of features to respond to a request. We know this is a unique identifier though
                # (In the previous version of the algorithm, this would not work because the candidate layer would eventually have multiple features per layer identifier.
                # In this version, we no longer create new features for the candidate layer)
                for lf in self.working_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feat[self.getLayerIdentifier(False)])):
                    left_feat = lf

                for rf in self.working_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feat[self.getLayerIdentifier(True)])):
                    right_feat = rf

                # create the new geometry from the difference
                new_right_feat_geom = right_feat.geometry().difference(left_feat.geometry())
                right_feat.setGeometry(new_right_feat_geom)
                    
                # copy the old feature and set the geometry on the new feature
                new_feat = QgsFeature(right_feat)
                new_feat.setGeometry(new_right_feat_geom)

                # delete the old feature and add the new
                self.working_layer.deleteFeature(right_feat.id())
                self.working_layer.addFeature(new_feat)

        self.working_layer.commitChanges()

        print("Ending with {count} features".format(count = str(self.working_layer.featureCount())))

    def handleOverlapsInCandidateLayer(self):
        print("Starting handle overlaps")

        dupe = duplicateLayer(self.working_layer, "Duplicate")

        join_prefix = JOIN_PREFIX

        res = processing.run("qgis:joinattributesbylocation", {
	        'INPUT':self.working_layer,
	        'JOIN':dupe,
	        'PREDICATE': ['4'], # 4 := Overlaps
	        'JOIN_FIELDS':'',
	        'METHOD':'0',
	        'DISCARD_NONMATCHING':False,
	        'PREFIX': join_prefix,
	        'OUTPUT':r'memory:overlaps'
        }, context=PROCESSING_CONTEXT)

        intersect_contrib_layer = res['OUTPUT']

        
        self.overlap_count_this_iteration = 0
        # the overlap operation without withins is reflexive, so this filter is wlog
        for feat in intersect_contrib_layer.getFeatures(self.lessThanIDFilterString()):
            self.overlap_count_this_iteration += 1 
            self.working_layer.startEditing()
            # unless you're using FIDs (blech), QGIS can only supply a list of features to respond to a request. We know this is a unique identifier though
            # (In the previous version of the algorithm, this would not work because the candidate layer would eventually have multiple features per layer identifier.
            # In this version, we no longer create new features for the candidate layer)
            for lf in self.working_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feat[self.getLayerIdentifier(False)])):
                left_feat = lf

            for rf in self.working_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feat[self.getLayerIdentifier(True)])):
                right_feat = rf

            retained_intersection = left_feat.geometry().intersection(right_feat.geometry())
            
            if left_feat.isValid() and right_feat.isValid():
                if self.compareFeatures(left_feat, right_feat):   # Left side loses. Assign Left = Left - Right
                    
                    # create the new geometry from the difference
                    new_left_feat_geom = left_feat.geometry().difference(right_feat.geometry())
                    
                    # copy the old feature and set the geometry on the new feature
                    new_feat = QgsFeature(left_feat)
                    new_feat.setGeometry(new_left_feat_geom)

                    # delete the old feature and add the new
                    self.working_layer.deleteFeature(left_feat.id())
                    self.working_layer.addFeature(new_feat)
                else:                                             # Right side loses. Assign Right = Right - Left
                    # create the new geometry from the difference
                    new_right_feat_geom = right_feat.geometry().difference(left_feat.geometry())
                    
                    # copy the old feature and set the geometry on the new feature
                    new_feat = QgsFeature(right_feat)
                    new_feat.setGeometry(new_right_feat_geom)

                    # delete the old feature and add the new
                    self.working_layer.deleteFeature(right_feat.id())
                    self.working_layer.addFeature(new_feat)
            else:
                print("Skipping " + str(left_feat[self.layer_identifier]) + ", " + str(right_feat[self.layer_identifier]))
            saved = self.working_layer.commitChanges()
            if not saved:
                print("Error saving on compare {left}, {right}".format(left=left_feat[self.layer_identifier], right=right_feat[self.layer_identifier]))
                print(self.working_layer.commitErrors())

        print("Ending handle overlaps. Found " + str(self.overlap_count_this_iteration))

    # utility methods
    
    def lessThanIDFilterString(self):
        # eg Table < Joined_TableID; prevents duplicate/permute records
        return "{identifier} < {join_prefix}{identifier}".format(identifier = self.layer_identifier, join_prefix = JOIN_PREFIX)

    def neqIDFilterString(self):
        # eg TableID != Joined_TableID; prevents within from finding equalities
        return "{identifier} != {join_prefix}{identifier}".format(identifier = self.layer_identifier, join_prefix = JOIN_PREFIX)

    def getLayerIdentifier(self, joined = False):
        if not joined:
            return self.layer_identifier
        else:
            return "{jp}{li}".format(jp=JOIN_PREFIX, li=self.layer_identifier)

    def compareFeatures(self, left_feat, right_feat = None):
        # expected to return true if the left side (or the unjoined side) loses
        if right_feat == None:
            return self.compareFeaturesViaJoinedLayer(left_feat)
        else:
            return self.compareFeaturesViaSeparateLayers(left_feat,right_feat)

    def removeFromCandidateLayer(self, feature_id):
        print("Deleteing " + str(feature_id))
        for featToDelete in self.working_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feature_id)):
            self.working_layer.deleteFeature(featToDelete.id())

    def addFeatureToIntersectLayer(self, feature_id):
        for featToAppend in self.working_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feature_id)):
            self.intersect_layer.addFeature(featToAppend)    

if __name__ == '__main__':
    parseArguments()
    
    # See https://gis.stackexchange.com/a/155852/4972 for details about the prefix 
    QgsApplication.setPrefixPath(r'C:\OSGEO4W64\apps\qgis', True)
    #qgs = QgsApplication([], False, "")
    qgs = QgsApplication([], False, r'C:\Sitka\Neptune\QGis', "server")

    qgs.initQgis()
        
    Processing.initialize()
    QgsApplication.processingRegistry().addProvider(QgsNativeAlgorithms())

    # must set processing framework to skip invalid geometries as it defaults to halt-and-catch-fire
    PROCESSING_CONTEXT = dataobjects.createContext()
    PROCESSING_CONTEXT.setInvalidGeometryCheck(QgsFeatureRequest.GeometrySkipInvalid)

    # Set the decision functions for delineations
    def compareDelineationsViaJoinedLayer(feat):
        return feat["TrashCaptureEffectiveness"] <= feat["{jp}TrashCaptureEffectiveness".format(jp=JOIN_PREFIX)]
    def compareDelineationsViaSeparateLayers(left_feat, right_feat):
        return left_feat["TrashCaptureEffectiveness"] <= right_feat["TrashCaptureEffectiveness"]

    # Set the decision functions for OVTAs
    def compareAssessmentAreasViaJoinedLayer(feat):
        return feat["MostRecentAssessmentDate"] <= feat["{jp}MostRecentAssessmentDate".format(jp=JOIN_PREFIX)]
    def compareAssessmentAreasViaSeparateLayers(left_feat, right_feat):
        return left_feat["MostRecentAssessmentDate"] <= right_feat["MostRecentAssessmentDate"]
    
    #Do note that the views here has all input filters built into it

    connstring_delineation = CONNSTRING_BASE + "tables=dbo.vDelineationTGUInput"
    delineation_layer = QgsVectorLayer(connstring_delineation, "Delineations", "ogr")
    
    if not delineation_layer.isValid():
        print("Layer failed to load!")
    else:
        print("Loaded Delineation layer!")

    # DEM-generated catchments are highly prone to ring-self-intersections near their edges, so for this layer we debuffer to smooth those out.
    debuffer = processing.run("native:buffer", {
        'INPUT':delineation_layer,
        'DISTANCE':-1e-05,
        'SEGMENTS':5,
        'END_CAP_STYLE':1,
        'JOIN_STYLE':1,
        'MITER_LIMIT':2,
        'DISSOLVE':False,
        'OUTPUT':'memory:debuffered_delineations'},
        context = PROCESSING_CONTEXT)

    debuffered_delineation_layer = debuffer['OUTPUT']

    print("Flattening Delineations...\n")
    flatten_delineations = Flatten(delineation_layer, "DelineationID", compareDelineationsViaJoinedLayer, compareDelineationsViaSeparateLayers)
    flatten_delineations.run()
    delineation_flattened_layer = flatten_delineations.working_layer
    print("\n\n")

    connstring_ovta = CONNSTRING_BASE + "tables=dbo.vOnlandVisualTrashAssessmentAreaDated"
    ovta_layer = QgsVectorLayer(connstring_ovta, "OVTAs", "ogr")

    if not ovta_layer.isValid():
        print("OVTA Layer failed to Load!")
    else:
        print("Loaded OVTA Layer")

    print("Flattening OVTAs...\n")
    flatten_ovtas = Flatten(ovta_layer, "OnlandVisualTrashAssessmentAreaID", compareAssessmentAreasViaJoinedLayer, compareAssessmentAreasViaSeparateLayers)
    flatten_ovtas.run()
    ovta_flattened_layer = flatten_ovtas.working_layer

    print("Union OVTA with Delineation\n")

    # union and write to a temp file for testing
    union_res = processing.run("native:union", {
        'INPUT': ovta_flattened_layer,
        'OVERLAY': delineation_flattened_layer,
        'OVERLAY_FIELDS_PREFIX':'',
        'OUTPUT':'memory:union_layer'
        }, context=PROCESSING_CONTEXT)

    union_layer = union_res['OUTPUT']

    connstring_land_use_block = CONNSTRING_BASE + "tables=dbo.LandUseBlock"
    land_use_block_layer = QgsVectorLayer(connstring_land_use_block, "Land Use Blocks", "ogr")

    if not land_use_block_layer.isValid():
        print("Land Use Block Layer failed to Load!")
    else:
        print("Loaded Land Use Block Layer")

    print("Union Land Use Block layer with Delineation-OVTA Layer. Will write to: " + OUTPUT_PATH)

    # The union will include false TGUs, where there is no land use block ID. The GDAL query will remove those.
    tgu_res = processing.run("native:union", {
        'INPUT': land_use_block_layer,
        'OVERLAY': union_layer,
        'OVERLAY_FIELDS_PREFIX':'',
        'OUTPUT':OUTPUT_PATH
        }, context=PROCESSING_CONTEXT)

    print("Successed!")

    qgs.exitQgis()