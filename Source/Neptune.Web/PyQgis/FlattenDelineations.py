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

def parseConnstring():
    parser = argparse.ArgumentParser(description='Test PyQGIS connections to MSSQL')
    parser.add_argument('connstring', metavar='s', type=str, help='The connection string. This test will only run if the connection string points to a Neptune DB. Do not specify tables; the script will specify which table(s) it wants to look at.')

    args = parser.parse_args()

    connstring_base = "MSSQL:" + args.connstring

    if not connstring_base.endswith(";"):
            connstring_base = connstring_base + ";"
    if "True" in connstring_base:
            connstring_base = connstring_base.replace("True", "Yes")

    return connstring_base

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


JOIN_PREFIX = "Joined_"

class Flatten:
    
    def __init__(self, candidate_layer, layer_identifier, compareFeaturesViaJoinedLayer, compareFeaturesViaSeparateLayers):
        
        # don't use the DB as the datasource
        self.candidate_layer = duplicateLayer(candidate_layer, "Candidate Layer")
        
        
        
        self.fields = self.candidate_layer.dataProvider().fields()
        
        self.equality_cycles = {}
        self.next_equality_index = 0
        self.layer_identifier = layer_identifier
        self.compareFeaturesViaJoinedLayer = compareFeaturesViaJoinedLayer
        self.compareFeaturesViaSeparateLayers = compareFeaturesViaSeparateLayers
        
    def run(self):
        iteration_number = 0
        # python doesn't have do-while
        while True:
            print("Starting Iteration #" + str(iteration_number))
            self.iterate()
            iteration_number += 1
            print("Ending Iteration #" + str(iteration_number) + "\n")

            break; #todo: delete, testing only

            if self.overlap_count_this_iteration == 0 and self.inclusion_count_this_iteration == 0:
                break;
            if iteration_number > 5:
                print("Exceeded 5 iterations; dying now.")
                break;

        print("Finished after {count} iterations!\n\n\n".format(count=iteration_number))

    def iterate(self):
        # (2)(a) Collapse equality chains
        self.removeEqualitiesFromCandidateLayer()

        # (2)(b) Deal with withins
        self.handleInclusionsInCandidateLayer()
        # (2)(c) Deal with ordinary overlaps
        self.handleOverlapsInCandidateLayer()
        

    #def dissolveGraduateLayer(self):
    #    print("Starting dissolve graduates with {count} graduates".format(count=self.graduate_layer.featureCount()))
    #    res = processing.run("native:dissolve",
    #        {'INPUT': self.graduate_layer, 
    #         'FIELD': [self.layer_identifier],  
    #         'OUTPUT':'memory:dissolved_output'})

    #    res2 = processing.run("native:buffer", 
    #        {'INPUT':res['OUTPUT'],
    #         'DISTANCE': -0.00005,
    #         'SEGMENTS':5,
    #         'END_CAP_STYLE':1,
    #         'JOIN_STYLE':1,
    #         'MITER_LIMIT':2,
    #         'DISSOLVE':False,
    #         'OUTPUT':'memory:debuffered_output'})

    #    self.graduate_layer = res2['OUTPUT']

    #    print("Ending dissolve graduates with {count} graduates".format(count=self.graduate_layer.featureCount()))


    #def divideMultipartCandidates(self):
        #print("Starting divide multipart candidates with {count} candidate features".format(count=self.candidate_layer.featureCount()))

        #res = processing.run("native:multiparttosingleparts", {'INPUT': self.candidate_layer, 'OUTPUT':'memory:single_partized'})

        #self.candidate_layer = res['OUTPUT']
        #print("Ending divide multipart candidates with {count} candidate features".format(count=self.candidate_layer.featureCount()))


    def removeEqualitiesFromCandidateLayer(self):
        print("Start collapse equality chains")
        print("Starting with {count} features".format(count = str(self.candidate_layer.featureCount())))

        dupe = duplicateLayer(self.candidate_layer, "Duplicate")
        join_prefix = JOIN_PREFIX

        res = processing.run("qgis:joinattributesbylocation", {
	        'INPUT': self.candidate_layer,
	        'JOIN': dupe,
	        'PREDICATE': ['2'], # 2 := Equals
	        'JOIN_FIELDS':'',
	        'METHOD':'0',
	        'DISCARD_NONMATCHING':False,
	        'PREFIX': join_prefix,
	        'OUTPUT':r'memory:equalities'
        }, context=context)

        equalities_layer = res['OUTPUT']

        # equality is reflexive, so this filter is wlog
        filter_icl = QgsFeatureRequest()
        filter_icl.setFilterExpression(self.lessThanIDFilterString())
        
        self.candidate_layer.startEditing()

        # for the equality predicate, we remove the losers from all future consideration
        for feat in equalities_layer.getFeatures(filter_icl):
            if self.compareFeatures(feat):   # left side loses
                self.removeFromCandidateLayer(feat[self.getLayerIdentifier(False)])
            else:
                self.removeFromCandidateLayer(feat[self.getLayerIdentifier(True)])                     # right side loses

        self.candidate_layer.commitChanges()
        print("Ending with {count} features".format(count = str(self.candidate_layer.featureCount())))
    
    def handleInclusionsInCandidateLayer(self):
        print("Start handle inclusions")
        print("Starting with {count} features".format(count = str(self.candidate_layer.featureCount())))

        dupe = duplicateLayer(self.candidate_layer, "Duplicate")
        join_prefix = JOIN_PREFIX

        res = processing.run("qgis:joinattributesbylocation", {
	        'INPUT': self.candidate_layer,
	        'JOIN': dupe,
	        'PREDICATE': ['2'], # 5 := Within
	        'JOIN_FIELDS':'',
	        'METHOD':'0',
	        'DISCARD_NONMATCHING':False,
	        'PREFIX': join_prefix,
	        'OUTPUT':r'memory:equalities'
        }, context=context)

        inclusions_layer = res['OUTPUT']

        self.candidate_layer.startEditing()

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
                for lf in self.candidate_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feat[self.getLayerIdentifier(False)])):
                    left_feat = lf

                for rf in self.candidate_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feat[self.getLayerIdentifier(True)])):
                    right_feat = rf

                # create the new geometry from the difference
                new_right_feat_geom = right_feat.geometry().difference(left_feat.geometry())
                right_feat.setGeometry(new_right_feat_geom)
                    
                # copy the old feature and set the geometry on the new feature
                new_feat = QgsFeature(right_feat)
                new_feat.setGeometry(new_right_feat_geom)

                # delete the old feature and add the new
                self.candidate_layer.deleteFeature(right_feat.id())
                self.candidate_layer.addFeature(new_feat)

        self.candidate_layer.commitChanges()

        print("Ending with {count} features".format(count = str(self.candidate_layer.featureCount())))

    def handleOverlapsInCandidateLayer(self):
        print("Starting handle overlaps")

        dupe = duplicateLayer(self.candidate_layer, "Duplicate")

        join_prefix = JOIN_PREFIX

        res = processing.run("qgis:joinattributesbylocation", {
	        'INPUT':self.candidate_layer,
	        'JOIN':dupe,
	        'PREDICATE': ['4'], # 4 := Overlaps
	        'JOIN_FIELDS':'',
	        'METHOD':'0',
	        'DISCARD_NONMATCHING':False,
	        'PREFIX': join_prefix,
	        'OUTPUT':r'memory:overlaps'
        }, context=context)

        intersect_contrib_layer = res['OUTPUT']

        
        self.overlap_count_this_iteration = 0
        # the overlap operation without withins is reflexive, so this filter is wlog
        for feat in intersect_contrib_layer.getFeatures(self.lessThanIDFilterString()):
            self.overlap_count_this_iteration += 1 
            self.candidate_layer.startEditing()
            # unless you're using FIDs (blech), QGIS can only supply a list of features to respond to a request. We know this is a unique identifier though
            # (In the previous version of the algorithm, this would not work because the candidate layer would eventually have multiple features per layer identifier.
            # In this version, we no longer create new features for the candidate layer)
            for lf in self.candidate_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feat[self.getLayerIdentifier(False)])):
                left_feat = lf

            for rf in self.candidate_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feat[self.getLayerIdentifier(True)])):
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
                    self.candidate_layer.deleteFeature(left_feat.id())
                    self.candidate_layer.addFeature(new_feat)
                else:                                             # Right side loses. Assign Right = Right - Left
                    # create the new geometry from the difference
                    new_right_feat_geom = right_feat.geometry().difference(left_feat.geometry())
                    
                    # copy the old feature and set the geometry on the new feature
                    new_feat = QgsFeature(right_feat)
                    new_feat.setGeometry(new_right_feat_geom)

                    # delete the old feature and add the new
                    self.candidate_layer.deleteFeature(right_feat.id())
                    self.candidate_layer.addFeature(new_feat)
            else:
                print("Skipping " + str(left_feat[self.layer_identifier]) + ", " + str(right_feat[self.layer_identifier]))
            saved = self.candidate_layer.commitChanges()
            if not saved:
                print("Error saving on compare {left}, {right}".format(left=left_feat[self.layer_identifier], right=right_feat[self.layer_identifier]))
                print(self.candidate_layer.commitErrors())

        print("Ending handle overlaps. Found " + str(self.overlap_count_this_iteration))
    
    #def graduate(self):
    #    self.intersect_layer.countSymbolFeatures()
    #    feature_count = self.intersect_layer.featureCount()

    #    print("{count} features in intersect layer...".format(count=feature_count))

    #    # Subtract the intersections from the layer under consideration
    #    join_prefix = JOIN_PREFIX

    #    res = processing.run("native:difference", {
	   #     'INPUT':self.candidate_layer,
	   #     'OVERLAY': self.intersect_layer,
	   #     'OUTPUT':r'memory:difference'
    #    }, context=context)

    #    difference_layer = res["OUTPUT"]

    #    # todo: don't use mergevectorlayers; just loop and add

    #    # merge the difference in with the corrected layer from previous iterations
    #    res = processing.run("native:mergevectorlayers", {
	   #     'LAYERS': [self.graduate_layer, difference_layer],
    #        'CRS': 'EPSG:4326',
	   #     'OUTPUT':r'memory:graduate'
    #    }, context=context)
        
    #    # reassign
    #    self.graduate_layer = res['OUTPUT']

    #    self.candidate_layer = self.intersect_layer

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


    def writeDelineationLayerToTempFile(self):
        crs = QgsCoordinateReferenceSystem("epsg:4326")
        error = QgsVectorFileWriter.writeAsVectorFormat(self.candidate_layer, r"c:\temp\delineations.shp", "UTF-8", crs , "ESRI Shapefile")

    def writeOVTALayerToTempFile(self):
        crs = QgsCoordinateReferenceSystem("epsg:4326")
        error = QgsVectorFileWriter.writeAsVectorFormat(self.candidate_layer, r"c:\temp\ovta.shp", "UTF-8", crs , "ESRI Shapefile")

    def writeIntersectLayerToTempFile(self):
        crs = QgsCoordinateReferenceSystem("epsg:4326")
        error = QgsVectorFileWriter.writeAsVectorFormat(self.intersect_layer, r"c:\temp\intersect.shp", "UTF-8", crs , "ESRI Shapefile")

    def writeGraduateLayerToTempFile(self):
        crs = QgsCoordinateReferenceSystem("epsg:4326")
        error = QgsVectorFileWriter.writeAsVectorFormat(self.graduate_layer, r"c:\temp\graduate.shp", "UTF-8", crs , "ESRI Shapefile")

    def removeFromCandidateLayer(self, feature_id):
        print("Deleteing " + str(feature_id))
        for featToDelete in self.candidate_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feature_id)):
            self.candidate_layer.deleteFeature(featToDelete.id())

    def addFeatureToIntersectLayer(self, feature_id):
        for featToAppend in self.candidate_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feature_id)):
            self.intersect_layer.addFeature(featToAppend)    

if __name__ == '__main__':
    connstring_base = parseConnstring()
    
    # See https://gis.stackexchange.com/a/155852/4972 for details about the prefix 
    QgsApplication.setPrefixPath(r'C:\OSGEO4W64\apps\qgis', True)
    #qgs = QgsApplication([], False, "")
    qgs = QgsApplication([], False, r'C:\Sitka\Neptune\QGis', "server")

    qgs.initQgis()
        
    Processing.initialize()
    QgsApplication.processingRegistry().addProvider(QgsNativeAlgorithms())

    # must set processing framework to skip invalid geometries as it defaults to halt-and-catch-fire
    context = dataobjects.createContext()
    context.setInvalidGeometryCheck(QgsFeatureRequest.GeometrySkipInvalid)

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
    
    #(1) Get the delineation layer
    #Do note that the view here has all input filters built into it

    connstring_delineation = connstring_base + "tables=dbo.vDelineationTGUInput"
    delineation_layer = QgsVectorLayer(connstring_delineation, "Delineations", "ogr")
    
    if not delineation_layer.isValid():
        print("Layer failed to load!")
    else:
        print("Loaded Delineation layer!")

    print("Flattening Delineations...\n")
    flatten = Flatten(delineation_layer, "DelineationID", compareDelineationsViaJoinedLayer, compareDelineationsViaSeparateLayers)
    flatten.run()
    flatten.writeDelineationLayerToTempFile()
    print("\n\n")

    connstring_ovta = connstring_base + "tables=dbo.vOnlandVisualTrashAssessmentAreaDated"
    ovta_layer = QgsVectorLayer(connstring_ovta, "OVTAs", "ogr")

    if not ovta_layer.isValid():
        print("OVTA Layer failed to Load!")
    else:
        print("Loaded OVTA Layer")

    print("Flattening OVTAs...\n")
    flatten = Flatten(ovta_layer, "OnlandVisualTrashAssessmentAreaID", compareAssessmentAreasViaJoinedLayer, compareAssessmentAreasViaSeparateLayers)
    flatten.run()
    flatten.writeOVTALayerToTempFile()

    qgs.exitQgis()