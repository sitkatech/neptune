### Authored by Nicholas Padinha for Sitka Technology Group, 2019
### A PyQGIS pipeline for topologically-correcting a polygon layer
### by replacing overlaps between features with new features.
### Attributes are assigned to the new features based on the overlapping
### features' attributes. In this case the determination is made
### based on a domain-specific rule.
### Consult the diagram for a conceptual overview of the process

import sys

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

# Create an exact duplicate of the delineation layer.
# Needed because the processing framework cannot actually operate with the same layer as multiple inputs to an algorithm
# This is infuriating if you've ever used the QGIS GUI, where it looks like you can set the same layer for more than one input
# One concludes, after much trial and error, that the processing framework is actually operating on two separate copies of the layer behind the scenes.
def duplicateLayer(qgs_vector_layer, duplicate_layer_name):
    # fixme: we might never use layer types other than polygon, but we might want this parameterized in the future
    layer_dupe = QgsVectorLayer("Polygon?crs=epsg:4326", duplicate_layer_name, "memory")

    mem_layer_data = layer_dupe.dataProvider()

    feats = [feat for feat in delineation_layer.getFeatures()]
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
    def __init__(self, candidate_layer):
        self.candidate_layer = candidate_layer
        self.fields = self.candidate_layer.dataProvider().fields()
        self.intersect_layer = None
        self.equality_cycles = {}
        self.next_equality_index = 0
        
    def run(self):
        # iterate until candidate_layer is empty

    def iterate(self):
        self.candidate_layer.startEditing()
        self.intersect_layer = QgsVectorLayer("Polygon?crs=epsg:4326", "Intersect Layer", "memory")
        assignFieldsToLayerFromSourceLayer(self.intersect_layer, self.candidate_layer)
        
        # (2)(a) Collapse equality chains
        self.removeEqualitiesFromCandidateLayer()

        # (2)(b) Deal with withins
        self.handleInclusionsInCandidateLayer()

        # (2)(c) Deal with ordinary overlaps
        self.handleOverlapsInCandidateLayer()

        ##at this point, intersection layer is complete (Up to correcting equality cycles)

        self.graduate()

    def removeEqualitiesFromCandidateLayer(self):
        print("Start collapse equality chains")
        print("Starting with {count} features".format(count = str(self.candidate_layer.featureCount())))

        dupe = duplicateLayer(self.candidate_layer, "Duplicate")
        join_prefix = "Joined_"

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
        filter_icl.setFilterExpression("DelineationID < {joined_id}DelineationID".format(joined_id = join_prefix))
        
        # for the equality predicate, we remove the losers from all future consideration
        for feat in equalities_layer.getFeatures(filter_icl):
            if feat["TrashCaptureEffectiveness"] <= feat["Joined_TrashCaptureEffectiveness"]:   # left side loses
                self.removeFromCandidateLayer(feat["DelineationID"])
            else:
                self.removeFromCandidateLayer(feat["Joined_DelineationID"])                     # right side loses

        self.candidate_layer.commitChanges()
        print("Ending with {count} features".format(count = str(self.candidate_layer.featureCount())))

    def handleInclusionsInCandidateLayer(self):
        print("Start handle inclusions")
        print("Starting with {count} features".format(count = str(self.candidate_layer.featureCount())))

        dupe = duplicateLayer(self.candidate_layer, "Duplicate")
        join_prefix = "Joined_"

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

        # within is not reflexive, so we only have to filter out self-equals
        filter_icl = QgsFeatureRequest()
        filter_icl.setFilterExpression("DelineationID != {joined_id}DelineationID".format(joined_id = join_prefix))

        # the smaller is removed if it loses; else it is retained for the next iteration
        for feat in inclusions_layer.getFeatures(filter_icl):
            if feat["TrashCaptureEffectiveness"] <= feat["Joined_TrashCaptureEffectiveness"]:   # smaller loses
                self.removeFromCandidateLayer(feat["DelineationID"])
            else:                                                                               # smaller wins
                self.addFeatureToIntersectLayer(feat["Joined_TrashCaptureEffectiveness"])

        self.intersect_layer.commitChanges()
        print("Ending with {count} features".format(count = str(self.candidate_layer.featureCount())))

    def handleOverlapsInCandidateLayer(self):
        dupe = duplicateLayer(self.candidate_layer, "Duplicate")

        join_prefix = "Joined_"

        res = processing.run("qgis:joinattributesbylocation", {
	        'INPUT':self.candidate_layer,
	        'JOIN':dupe,
	        'PREDICATE': ['4'], # 4 := Overlaps
	        'JOIN_FIELDS':'',
	        'METHOD':'0',
	        'DISCARD_NONMATCHING':False,
	        'PREFIX': join_prefix,
	        'OUTPUT':r'memory:delineation_overlaps'
        }, context=context)

        intersect_contrib_layer = res['OUTPUT']
        
        # the overlap operation is reflexive, so this filter is wlog
        filter_icl = QgsFeatureRequest()
        filter_icl.setFilterExpression("DelineationID < {joined_id}DelineationID".format(joined_id = join_prefix))
    
        for feat in intersect_contrib_layer.getFeatures(filter_icl):
            for lf in self.candidate_layer.getFeatures("DelineationID = {id}".format(id=feat["DelineationID"])):
                left_feat = lf

            for rf in self.candidate_layer.getFeatures("DelineationID = {id}".format(id=feat["Joined_DelineationID"])):
                right_feat = rf

            retained_intersection = left_feat.geometry().intersection(right_feat.geometry())
            
            if left_feat.isValid() and right_feat.isValid():
                if left_feat["TrashCaptureEffectiveness"] >= right_feat["TrashCaptureEffectiveness"]:
                    ret_int_feat = QgsFeature(left_feat)
                    ret_int_feat.setGeometry(retained_intersection)
                else:
                    ret_int_feat = QgsFeature(right_feat)
                    ret_int_feat.setGeometry(retained_intersection)
                self.intersect_layer.addFeature(ret_int_feat)
            else:
                print("Skipping")

        self.intersect_layer.commitChanges()

    def graduate(self):
        ## subtract candidate_layer - intersect_layer
        ## append the subtract to graduate_layer
        ## assign candidate_layer := intersect_layer

    def writeCandidateLayerToTempFile(self):
        crs = QgsCoordinateReferenceSystem("epsg:4326")
        error = QgsVectorFileWriter.writeAsVectorFormat(self.candidate_layer, r"c:\temp\wow.shp", "UTF-8", crs , "ESRI Shapefile")

    def writeIntersectLayerToTempFile(self):
        crs = QgsCoordinateReferenceSystem("epsg:4326")
        error = QgsVectorFileWriter.writeAsVectorFormat(self.intersect_layer, r"c:\temp\wow.shp", "UTF-8", crs , "ESRI Shapefile")

    def removeFromCandidateLayer(self, delineationID):
        for featToDelete in self.candidate_layer.getFeatures("DelineationID = {id}".format(id=delineationID)):
            self.candidate_layer.deleteFeature(featToDelete.id())

    def addFeatureToIntersectLayer(self, delineationID):
        for featToAppend in self.candidate_layer.getFeatures("DelineationID = {id}".format(id=delineationID)):
            self.intersect_layer.addFeature(featToAppend)
                

if __name__ == '__main__':
    connstring_base = parseConnstring()
    
    # See https://gis.stackexchange.com/a/155852/4972 for details about the prefix 
    QgsApplication.setPrefixPath(r'C:\OSGEO4W64\apps\qgis', True)
    qgs = QgsApplication([], False, "")
    qgs.initQgis()
        
    Processing.initialize()
    QgsApplication.processingRegistry().addProvider(QgsNativeAlgorithms())

    # must set processing framework to skip invalid geometries as it defaults to halt-and-catch-fire
    context = dataobjects.createContext()
    context.setInvalidGeometryCheck(QgsFeatureRequest.GeometrySkipInvalid)
    
    # (1) Get the delineation layer
    # Do note that the view here has all input filters built into it

    connstring_delineation = connstring_base + "tables=dbo.vDelineationTGUInput"
    delineation_layer = QgsVectorLayer(connstring_delineation, "Delineations", "ogr")
    
    if not delineation_layer.isValid():
        print("Layer failed to load!")
    else:
        print("Loaded Delineation layer!")

    flatten = Flatten(delineation_layer)
    flatten.iterate()


    qgs.exitQgis()