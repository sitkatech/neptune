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
     QgsCoordinateReferenceSystem,
     QgsProject
)
from qgis.analysis import QgsNativeAlgorithms

print ("imported Qgis")
# See https://gis.stackexchange.com/a/155852/4972 for details about the prefix 
QgsApplication.setPrefixPath('C:\\OSGEO4W64\\apps\\qgis', True)

# Append the path where processing plugin can be found
sys.path.append('C:\\OSGeo4W64\\apps\\qgis\\python\\plugins')

import processing
from processing.tools import dataobjects
from processing.core.Processing import Processing

import argparse

from pyqgis_utils import (
    duplicateLayer,
    fetchLayerFromDatabase,
    raiseIfLayerInvalid,
    QgisError
)

JOIN_PREFIX = "Joined_"
CONNSTRING_BASE = "CONNSTRING ERROR"
OUTPUT_FOLDER = "OUTPUT FOLDER ERROR"
OUTPUT_FILE = "OUTPUT FILE ERROR"


def parseArguments():
    parser = argparse.ArgumentParser(description='Test PyQGIS connections to MSSQL')
    parser.add_argument('connstring', metavar='s', type=str, help='The connection string. Do not specify tables; the script will specify which table(s) it wants to look at.')
    parser.add_argument('output_folder', metavar='d', type=str, help='The folder to write the final output to.')
    parser.add_argument('output_file', metavar='d', type=str, help='The filename to write the final output to.')

    args = parser.parse_args()

    # this is easier to write than anything sane
    global CONNSTRING_BASE
    global OUTPUT_FOLDER
    global OUTPUT_FILE
    CONNSTRING_BASE = "MSSQL:" + args.connstring
    OUTPUT_FOLDER = args.output_folder
    OUTPUT_FILE = args.output_file

    if not CONNSTRING_BASE.endswith(";"):
            CONNSTRING_BASE = CONNSTRING_BASE + ";"
    if "True" in CONNSTRING_BASE:
            CONNSTRING_BASE = CONNSTRING_BASE.replace("True", "Yes")

def assignFieldsToLayerFromSourceLayer(target, source):
    target_layer_data = target.dataProvider()
    source_layer_data = source.dataProvider()

    attr = source_layer_data.fields().toList()
    target_layer_data.addAttributes(attr)
    target.updateFields()

def unionAndFix(inputLayer, overlayLayer, inputLayerOutputPath, overlayLayerOutputPath, unionResultOutputPath, context=None):
    inputLayer = removeNullGeometries(inputLayer, 'nonnull', None, PROCESSING_CONTEXT)
    inputLayer = removeSlivers(inputLayer, 'noslivers', None, PROCESSING_CONTEXT)
    inputLayer = bufferZero(inputLayer, 'buffer', None, PROCESSING_CONTEXT)
    inputLayer = snapGeometriesWithinLayer(inputLayer, 'snapped', None, PROCESSING_CONTEXT)
    inputLayer = fixGeometriesWithinLayer(inputLayer, None, inputLayerOutputPath, PROCESSING_CONTEXT)
    overlayLayer = removeNullGeometries(overlayLayer, 'nonnull', None, PROCESSING_CONTEXT)    
    overlayLayer = removeSlivers(overlayLayer, 'noslivers', None, PROCESSING_CONTEXT)
    overlayLayer = bufferZero(overlayLayer, 'buffer', None, PROCESSING_CONTEXT)
    overlayLayer = snapGeometriesWithinLayer(overlayLayer, 'snapped', None, PROCESSING_CONTEXT)
    overlayLayer = fixGeometriesWithinLayer(overlayLayer, None, overlayLayerOutputPath, PROCESSING_CONTEXT)
    result = union(inputLayerOutputPath, overlayLayerOutputPath, None, unionResultOutputPath, PROCESSING_CONTEXT)
    print('Union result saved to ' + unionResultOutputPath)

def selectPolygonFeatures(inputLayer, context = None):
    params = {
        'INPUT':inputLayer,
        'EXPRESSION':"geometry_type($geometry) = 'Polygon' and $area >= 100"
    }
    print('Running qgis:selectbyexpression')
    if context is not None:
        result = processing.run("qgis:selectbyexpression", params ,context = context)
    else:
        result = processing.run("qgis:selectbyexpression", params)
    return result

def removeNullGeometries(inputLayer, memoryOutputName=None, filesystemOutputPath=None, context = None):
    params = {
        'INPUT':inputLayer,
        'REMOVE_EMPTY':True
    }
    result = runNativeAlgorithm("native:removenullgeometries", params, memoryOutputName, filesystemOutputPath, context)
    return result

def removeSlivers(inputLayer, memoryOutputName=None, filesystemOutputPath=None, context = None):
    params = {
        'INPUT':inputLayer,
        'MODE':2,
    }
    result = runNativeAlgorithm("qgis:eliminateselectedpolygons", params, memoryOutputName, filesystemOutputPath, context)
    return result

def saveSelectedFeatures(inputLayer, memoryOutputName=None, filesystemOutputPath=None, context = None):
    params = {
        'INPUT':inputLayer
    }
    result = runNativeAlgorithm("native:saveselectedfeatures", params, memoryOutputName, filesystemOutputPath, context)
    return result


def union(inputLayer, overlayLayer, memoryOutputName=None, filesystemOutputPath=None, context = None):
    params = {
        'INPUT':inputLayer,
        'OVERLAY':overlayLayer,
        'OVERLAY_FIELDS_PREFIX':''
    }
    
    result = runNativeAlgorithm("native:union", params, memoryOutputName, filesystemOutputPath, context)
    return result

def createSpatialIndex(inputLayer, context = None):
    params = {
        'INPUT':inputLayer
    }
    print('Running native:createspatialindex')
    if context is not None:
        result = processing.run("native:createspatialindex", params ,context = context)
    else:
        result = processing.run("native:createspatialindex", params)

def bufferZero(inputLayer, memoryOutputName=None, filesystemOutputPath=None, context=None):
    params = {
        'INPUT':inputLayer,
        'DISTANCE':0,
        'SEGMENTS':5,
        'END_CAP_STYLE':1,
        'JOIN_STYLE':1,
        'MITER_LIMIT':2,
        'DISSOLVE':False
    }
    
    result = runNativeAlgorithm("native:buffer", params, memoryOutputName, filesystemOutputPath, context)
    return result

def fixGeometriesWithinLayer(inputLayer, memoryOutputName=None, filesystemOutputPath=None, context=None):
    params = {
        'INPUT':inputLayer
    }
    
    result = runNativeAlgorithm("native:fixgeometries", params, memoryOutputName, filesystemOutputPath, context)
    return result

def snapGeometriesWithinLayer(inputLayer, memoryOutputName=None, filesystemOutputPath=None, context=None):
    params = {
        'INPUT':inputLayer,
        'REFERENCE_LAYER':inputLayer,
        'TOLERANCE':1,
        'BEHAVIOR':1
    }
    
    result = runNativeAlgorithm("qgis:snapgeometries", params, memoryOutputName, filesystemOutputPath, context)
    return result

def runNativeAlgorithm(algorithm, params, memoryOutputName=None, filesystemOutputPath=None, context=None):
    if memoryOutputName is not None:
        params['OUTPUT'] = 'memory:' + memoryOutputName
    elif filesystemOutputPath is not None:
        params['OUTPUT'] = filesystemOutputPath
    else:
        raise QgisError("No output provided for " + algorithm + " operation")

    print('Running ' + algorithm)
    if context is not None:
        result = processing.run(algorithm, params ,context = context)
    else:
        result = processing.run(algorithm, params)

    return result['OUTPUT']

def writeVectorLayerToDisk(layer, output_path):
    # Write to an ESRI Shapefile format dataset using UTF-8 text encoding
    save_options = QgsVectorFileWriter.SaveVectorOptions()
    save_options.driverName = "ESRI Shapefile"
    save_options.fileEncoding = "UTF-8"
    transform_context = QgsProject.instance().transformContext()
    error = QgsVectorFileWriter.writeAsVectorFormatV3(layer, output_path, transform_context, save_options)
    if error[0] == QgsVectorFileWriter.NoError:
        print("Saved to " + output_path)
    else:
      print(error)

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

    def fetchLayer(spatialTableName):
        return fetchLayerFromDatabase(CONNSTRING_BASE, spatialTableName)

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
    delineation_layer = fetchLayer("vPyQgisDelineationTGUInput")    
    # DEM-generated catchments are highly prone to ring-self-intersections near their edges, so for this layer we use the buffer-0 trick to smooth those out.
    delineation_layer = bufferZero(delineation_layer, 'debuffered_delineations', context = PROCESSING_CONTEXT)
    print("Flattening Delineations...\n")
    flatten_delineations = Flatten(delineation_layer, "DelinID", compareDelineationsViaJoinedLayer, compareDelineationsViaSeparateLayers, "TCEffect")
    flatten_delineations.run()
    print("\n\n")

    ovta_layer = fetchLayer("vPyQgisOnlandVisualTrashAssessmentAreaDated")
    print("Flattening OVTAs...\n")
    flatten_ovtas = Flatten(ovta_layer, "OVTAID", compareAssessmentAreasViaJoinedLayer, compareAssessmentAreasViaSeparateLayers, "AssessDate")
    flatten_ovtas.run()
            
    print("Union OVTA with Delineation\n")
    delineation_flattened_layer_path = OUTPUT_FOLDER + '\\delineation_flattened_layer.geojson'
    ovta_flattened_layer_path = OUTPUT_FOLDER + '\\ovta_flattened_layer.geojson'
    ovta_delineation_layer_path = OUTPUT_FOLDER + '\\ovta_delineation_layer.geojson'
    ovta_delineation_layer = unionAndFix(flatten_ovtas.working_layer, flatten_delineations.working_layer, delineation_flattened_layer_path, ovta_flattened_layer_path, ovta_delineation_layer_path, PROCESSING_CONTEXT)

    wqmp_layer = fetchLayer("vPyQgisWaterQualityManagementPlanTGUInput")
    print("Flattening WQMPs...\n")
    flatten_wqmps = Flatten(wqmp_layer, "WQMPID", compareDelineationsViaJoinedLayer, compareDelineationsViaSeparateLayers, "TCEffect")
    flatten_wqmps.run()

    print("Union OVTA-Delineation with WQMP\n")
    wqmp_flattened_layer_path = OUTPUT_FOLDER + '\\wqmp_flattened_layer.geojson'
    ovta_delineation_layer_unionedandfixed_path = OUTPUT_FOLDER + '\\ovta_delineation_layer_unionedandfixed.geojson'
    odw_layer_path = OUTPUT_FOLDER + '\\odw_layer.geojson'
    odw_layer = unionAndFix(ovta_delineation_layer_path, flatten_wqmps.working_layer, ovta_delineation_layer_unionedandfixed_path, wqmp_flattened_layer_path, odw_layer_path, PROCESSING_CONTEXT)

    land_use_block_layer = fetchLayer("vPyQgisLandUseBlockTGUInput")
    land_use_block_layer_path = OUTPUT_FOLDER + '\\land_use_block_layer'
    writeVectorLayerToDisk(land_use_block_layer, land_use_block_layer_path)

    finalOutputPath = OUTPUT_FOLDER + '\\' + OUTPUT_FILE
    print("Union Land Use Block layer with Delineation-OVTA Layer. Will write to: " + finalOutputPath)

    # The union will include false TGUs, where there is no land use block ID. The GDAL query will remove those.
    land_use_block_layer_unionandfixed_path = OUTPUT_FOLDER + '\\land_use_block_layer_unionedandfixed.geojson'
    odw_layer_unionandfixed_path = OUTPUT_FOLDER + '\\odw_layer_unionedandfixed.geojson'
    tgu_layer = unionAndFix(land_use_block_layer_path + ".shp", odw_layer_path, land_use_block_layer_unionandfixed_path, odw_layer_unionandfixed_path, finalOutputPath, PROCESSING_CONTEXT)

    print("Succeeded!")

    qgs.exitQgis()