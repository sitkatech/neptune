### Authored by Nicholas Padinha for Sitka Technology Group, 2019
### A PyQGIS pipeline for topologically-correcting a polygon layer
### by replacing overlaps between features with new features.
### Attributes are assigned to the new features based on the overlapping
### features' attributes. In this case the determination is made
### based on a domain-specific rule.
### Consult the diagram for a conceptual overview of the process

import os
import sys

print ("starting")

from qgis.core import (
     QgsApplication, 
     QgsProcessingFeedback, 
     QgsVectorLayer,
     QgsFeatureRequest,
     QgsDataSourceUri,
     QgsFeature,
     QgsCoordinateReferenceSystem,
     QgsProject,
     QgsWkbTypes
)
from qgis.analysis import QgsNativeAlgorithms

print ("imported Qgis")
# See https://gis.stackexchange.com/a/155852/4972 for details about the prefix 
QgsApplication.setPrefixPath('C:\\Program Files\\QGIS 3.22.13\\apps\\qgis-ltr', True)

# Append the path where processing plugin can be found
sys.path.append('C:\\Program Files\\QGIS 3.22.13\\apps\\qgis-ltr\\python\\plugins')

import processing
from processing.tools import dataobjects
from processing.core.Processing import Processing

import argparse

from pyqgis_utils import (
    duplicateLayer,
    fetchLayerFromDatabase,
    raiseIfLayerInvalid,
    bufferSnapFix,
    removeSliversAndNulls,
    bufferZero,
    fixGeometriesWithinLayer,
    snapGeometriesWithinLayer,
    union,
    QgisError,
    writeVectorLayerToDisk
)

JOIN_PREFIX = "Joined_"
DATABASE_SERVER_NAME = "DATABASE SERVER NAME ERROR"
DATABASE_NAME = "DATABASE NAME ERROR"
DATABASE_USER_NAME = "DATABASE USER NAME ERROR"
DATABASE_PASSWORD = "DATABASE PASSWORD ERROR"
OUTPUT_FOLDER = "OUTPUT FOLDER ERROR"
OUTPUT_FILE_PREFIX = "OUTPUT FILE PREFIX ERROR"


def parseArguments():
    parser = argparse.ArgumentParser(description='Test PyQGIS connections to MSSQL')
    parser.add_argument('database_server_name', metavar='s', type=str, help='The name of the server where the database is located.')
    parser.add_argument('database_name', metavar='s', type=str, help='The name of the database to connect to.')
    parser.add_argument('database_username', metavar='s', type=str, help='The user name to use to connect to the database.')
    parser.add_argument('database_password', metavar='s', type=str, help='The password to use to connect to the database.')
    parser.add_argument('output_folder', metavar='d', type=str, help='The folder to write the final output to.')
    parser.add_argument('output_file_prefix', metavar='d', type=str, help='The filename prefix to write the final output to.')

    args = parser.parse_args()

    # this is easier to write than anything sane
    global DATABASE_SERVER_NAME
    global DATABASE_NAME
    global DATABASE_USER_NAME
    global DATABASE_PASSWORD
    global OUTPUT_FOLDER
    global OUTPUT_FILE_PREFIX
    global OUTPUT_FOLDER_AND_FILE_PREFIX
    DATABASE_SERVER_NAME = args.database_server_name
    DATABASE_NAME = args.database_name
    DATABASE_USER_NAME = args.database_username
    DATABASE_PASSWORD = args.database_password
    OUTPUT_FOLDER = args.output_folder
    OUTPUT_FILE_PREFIX = args.output_file_prefix
    OUTPUT_FOLDER_AND_FILE_PREFIX = OUTPUT_FOLDER + '\\' + OUTPUT_FILE_PREFIX

def assignFieldsToLayerFromSourceLayer(target, source):
    target_layer_data = target.dataProvider()
    source_layer_data = source.dataProvider()

    attr = source_layer_data.fields().toList()
    target_layer_data.addAttributes(attr)
    target.updateFields()

def unionAndFix(inputLayer, overlayLayer, inputLayerOutputPath, overlayLayerOutputPath, unionResultOutputPath, context=None):
    inputLayer = bufferSnapFix(inputLayer, inputLayerOutputPath, context)
    overlayLayer = bufferSnapFix(overlayLayer, overlayLayerOutputPath, context)
    if unionResultOutputPath is not None:
        result = union(inputLayerOutputPath, overlayLayerOutputPath, None, unionResultOutputPath, context)
    else:
        result = union(inputLayerOutputPath, overlayLayerOutputPath, 'unioned', None, context)
    #selectPolygonFeatures(result, context)
    #saveSelectedFeatures(result, None, unionResultOutputPath, context)
    print('Union succeeded')
    return result

class Flatten:
    
    def __init__(self, working_layer, layer_identifier, compareFeaturesViaJoinedLayer, compareFeaturesViaSeparateLayers, fieldToRemove):
        
        # don't use the DB as the datasource
        self.working_layer = duplicateLayer(working_layer, "Candidate Layer")
        
        self.fields = self.working_layer.dataProvider().fields()
                
        self.layer_identifier = layer_identifier
        self.compareFeaturesViaJoinedLayer = compareFeaturesViaJoinedLayer
        self.compareFeaturesViaSeparateLayers = compareFeaturesViaSeparateLayers
        self.fieldToRemove = fieldToRemove
        
    def run(self):
        # (2)(a) Collapse equality chains
        self.removeEqualitiesFromCandidateLayer()
        # (2)(b) Deal with withins
        self.handleInclusionsInCandidateLayer()
        # (2)(c) Deal with ordinary overlaps
        self.handleOverlapsInCandidateLayer()
        # remove fieldToRemove
        self.removeFieldFromLayer()

    def removeFieldFromLayer(self):
        fieldIndex = self.fields.indexFromName(self.fieldToRemove)
        print("Removing field '" + self.fieldToRemove + "'")
        self.working_layer.dataProvider().deleteAttributes([fieldIndex])
        self.working_layer.updateFields()

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
	        'OUTPUT':'memory:equalities'
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
	        'OUTPUT':'memory:inclusions'
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
        print("Starting with {count} features".format(count = str(self.working_layer.featureCount())))

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
	        'OUTPUT':'memory:overlaps'
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
            
            if left_feat.isValid() and right_feat.isValid():
                if self.compareFeatures(left_feat, right_feat):   # Left side loses. Assign Left = Left - Right
                    
                    # create the new geometry from the difference
                    new_left_feat_geom = left_feat.geometry().difference(right_feat.geometry())
                    
                    # copy the old feature and set the geometry on the new feature
                    new_feat = QgsFeature(left_feat)
                    new_feat.setGeometry(new_left_feat_geom)

#                    if(new_feat.geometry().type() != self.working_layer.geometryType()):
#                        print("Left won: " + str(left_feat[self.layer_identifier]) + " - " + QgsWkbTypes.geometryDisplayString(new_feat.geometry().type()) + str(new_feat.geometry().type()))
                    # delete the old feature and add the new
                    self.working_layer.deleteFeature(left_feat.id())
                    self.working_layer.addFeature(new_feat)
                else:                                             # Right side loses. Assign Right = Right - Left
                    # create the new geometry from the difference
                    new_right_feat_geom = right_feat.geometry().difference(left_feat.geometry())
                    
                    # copy the old feature and set the geometry on the new feature
                    new_feat = QgsFeature(right_feat)
                    new_feat.setGeometry(new_right_feat_geom)

#                   if(new_feat.geometry().type() != self.working_layer.geometryType()):
#                        print("Right won: " + str(right_feat[self.layer_identifier]) + " - " + QgsWkbTypes.geometryDisplayString(new_feat.geometry().type()) + str(new_feat.geometry().type()))
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
        print("Ending with {count} features".format(count = str(self.working_layer.featureCount())))

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
        print("Deleting " + str(feature_id))
        for featToDelete in self.working_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feature_id)):
            self.working_layer.deleteFeature(featToDelete.id())

    def addFeatureToIntersectLayer(self, feature_id):
        ## todo: I think this method is unused?
        for featToAppend in self.working_layer.getFeatures("{identifier} = {id}".format(identifier = self.layer_identifier, id=feature_id)):
            self.intersect_layer.addFeature(featToAppend)    

if __name__ == '__main__':
    parseArguments()
    
    #qgs = QgsApplication([], False, "")
    qgs = QgsApplication([], False, 'C:\\Sitka\\Neptune\\QGis', "server")

    qgs.initQgis()
        
    Processing.initialize()
    QgsApplication.processingRegistry().addProvider(QgsNativeAlgorithms())

    # must set processing framework to skip invalid geometries as it defaults to halt-and-catch-fire
    PROCESSING_CONTEXT = dataobjects.createContext()
    PROCESSING_CONTEXT.setInvalidGeometryCheck(QgsFeatureRequest.GeometrySkipInvalid)

    neptuneDataSource = QgsDataSourceUri()
    neptuneDataSource.setConnection(DATABASE_SERVER_NAME, "1433", DATABASE_NAME, DATABASE_USER_NAME, DATABASE_PASSWORD)

    def fetchLayer(spatialTableName, geometryColumnName):
        return fetchLayerFromDatabase(neptuneDataSource, spatialTableName, geometryColumnName)

    # Set the decision functions for delineations
    def compareDelineationsViaJoinedLayer(feat):
        return feat["TCEffect"] <= feat["{jp}TCEffect".format(jp=JOIN_PREFIX)]
    def compareDelineationsViaSeparateLayers(left_feat, right_feat):
        return left_feat["TCEffect"] <= right_feat["TCEffect"]

    # Set the decision functions for OVTAs
    def compareAssessmentAreasViaJoinedLayer(feat):
        return feat["AssessDate"] <= feat["{jp}AssessDate".format(jp=JOIN_PREFIX)]
    def compareAssessmentAreasViaSeparateLayers(left_feat, right_feat):
        return left_feat["AssessDate"] <= right_feat["AssessDate"]
    
    #Do note that the views here have all input filters built into them
    delineation_layer = fetchLayer("vPyQgisDelineationTGUInput", "DelineationGeometry")    
    # DEM-generated catchments are highly prone to ring-self-intersections near their edges, so for this layer we use the buffer-0 trick to smooth those out.
    delineation_layer = bufferZero(delineation_layer, 'debuffered_delineations', context = PROCESSING_CONTEXT)
    print("Flattening Delineations...\n")
    flatten_delineations = Flatten(delineation_layer, "DelinID", compareDelineationsViaJoinedLayer, compareDelineationsViaSeparateLayers, "TCEffect")
    flatten_delineations.run()
    print("\n\n")

    ovta_layer = fetchLayer("vPyQgisOnlandVisualTrashAssessmentAreaDated", "OnlandVisualTrashAssessmentAreaGeometry")
    print("Flattening OVTAs...\n")
    flatten_ovtas = Flatten(ovta_layer, "OVTAID", compareAssessmentAreasViaJoinedLayer, compareAssessmentAreasViaSeparateLayers, "AssessDate")
    flatten_ovtas.run()
            
    print("Union OVTA with Delineation\n")
    delineation_flattened_layer_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'delineation_flattened_layer.geojson'
    ovta_flattened_layer_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'ovta_flattened_layer.geojson'
    ovta_delineation_layer_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'ovta_delineation_layer.geojson'
    ovta_delineation_layer = unionAndFix(flatten_ovtas.working_layer, flatten_delineations.working_layer, ovta_flattened_layer_path, delineation_flattened_layer_path, ovta_delineation_layer_path, PROCESSING_CONTEXT)

    wqmp_layer = fetchLayer("vPyQgisWaterQualityManagementPlanTGUInput", "WaterQualityManagementPlanBoundary")
    print("Flattening WQMPs...\n")
    flatten_wqmps = Flatten(wqmp_layer, "WQMPID", compareDelineationsViaJoinedLayer, compareDelineationsViaSeparateLayers, "TCEffect")
    flatten_wqmps.run()

    print("Union OVTA-Delineation with WQMP\n")
    wqmp_flattened_layer_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'wqmp_flattened_layer.geojson'
    ovta_delineation_layer_unionedandfixed_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'ovta_delineation_layer_unionedandfixed.geojson'
    odw_layer_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'odw_layer.geojson'
    odw_layer = unionAndFix(ovta_delineation_layer_path, flatten_wqmps.working_layer, ovta_delineation_layer_unionedandfixed_path, wqmp_flattened_layer_path, odw_layer_path, PROCESSING_CONTEXT)

    land_use_block_layer = fetchLayer("vPyQgisLandUseBlockTGUInput", "LandUseBlockGeometry")
    land_use_block_layer_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'land_use_block_layer.geojson'
    writeVectorLayerToDisk(land_use_block_layer, land_use_block_layer_path, "GeoJSON")

    finalOutputPath = OUTPUT_FOLDER_AND_FILE_PREFIX + '.geojson'
    print("Union Land Use Block layer with Delineation-OVTA Layer. Will write to: " + finalOutputPath)

    # The union will include false TGUs, where there is no land use block ID. The GDAL query will remove those.
    land_use_block_layer_unionandfixed_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'land_use_block_layer_unionedandfixed.geojson'
    odw_layer_unionandfixed_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'odw_layer_unionedandfixed.geojson'
    tgu_layer = unionAndFix(land_use_block_layer_path, odw_layer_path, land_use_block_layer_unionandfixed_path, odw_layer_unionandfixed_path, finalOutputPath, PROCESSING_CONTEXT)

    print("Succeeded!")
    qgs.exitQgis()