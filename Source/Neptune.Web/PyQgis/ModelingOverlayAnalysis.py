### Authored by Sitka Technology Group, 2020

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
sys.path.append('C:\\Program Files\\QGIS 3.22.13\\apps\\qgis-ltr\\python\\plugins')

import processing
from processing.tools import dataobjects
from processing.core.Processing import Processing

import argparse

from pyqgis_utils import (
    duplicateLayer,
    fetchLayerFromDatabase,
    raiseIfLayerInvalid,
    QgisError,
    fetchLayerFromGeoJson
)

JOIN_PREFIX = "Joined_"
DATABASE_SERVER_NAME = "DATABASE SERVER NAME ERROR"
DATABASE_NAME = "DATABASE NAME ERROR"
DATABASE_USER_NAME = "DATABASE USER NAME ERROR"
DATABASE_PASSWORD = "DATABASE PASSWORD ERROR"
OUTPUT_FOLDER = "OUTPUT FOLDER ERROR"
OUTPUT_FILE = "OUTPUT FILE ERROR"
CLIP_PATH = None
RSB_IDs = None
PLANNED_PROJECT_ID = None

def parseArguments():
    parser = argparse.ArgumentParser(description='Test PyQGIS connections to MSSQL')
    parser.add_argument('database_server_name', metavar='s', type=str, help='The name of the server where the database is located.')
    parser.add_argument('database_name', metavar='s', type=str, help='The name of the database to connect to.')
    parser.add_argument('database_username', metavar='s', type=str, help='The user name to use to connect to the database.')
    parser.add_argument('database_password', metavar='s', type=str, help='The password to use to connect to the database.')
    parser.add_argument('output_path', metavar='d', type=str, help='The path to write the final output to.')
    parser.add_argument('--planned_project_id', type=int, help='If running the overlay for a particular Project, this will add delineations for any Treatment BMPs who belong to this project')
    parser.add_argument('--rsb_ids', type=str, help='If present, filters the rsb layer down to only rsbs whose id is present in the list. Should be numbers separated by commas')
    parser.add_argument('--clip', type=str, help='The path to a geojson file containing the shape to clip inputs to')
    args = parser.parse_args()

    # this is easier to write than anything sane
    global DATABASE_SERVER_NAME
    global DATABASE_NAME
    global DATABASE_USER_NAME
    global DATABASE_PASSWORD
    global OUTPUT_PATH
    global CLIP_PATH
    global PLANNED_PROJECT_ID
    global RSB_IDs
    DATABASE_SERVER_NAME = args.database_server_name
    DATABASE_NAME = args.database_name
    DATABASE_USER_NAME = args.database_username
    DATABASE_PASSWORD = args.database_password
    OUTPUT_PATH = args.output_path

    if args.clip:
        CLIP_PATH = args.clip
        print(CLIP_PATH)

    if args.planned_project_id:
        PLANNED_PROJECT_ID = args.planned_project_id
        print(PLANNED_PROJECT_ID)

    if args.rsb_ids:
        RSB_IDs = args.rsb_ids
        print(RSB_IDs)

def union(inputLayer, overlayLayer, memoryOutputName=None, filesystemOutputPath=None, context = None):
    params = {
        'INPUT': inputLayer,
        'OVERLAY': overlayLayer,
        'OVERLAY_FIELDS_PREFIX':''
    }
    
    result = runNativeAlgorithm("native:union", params, memoryOutputName, filesystemOutputPath, context)
    return result

def intersection(inputLayer, overlayLayer, memoryOutputName=None, filesystemOutputPath=None, context = None):
    params = {
        'INPUT': inputLayer,
        'OVERLAY': overlayLayer,
        'INPUT_FIELDS':[],
        'OVERLAY_FIELDS':[],
        'OVERLAY_FIELDS_PREFIX':''
    }
    
    result = runNativeAlgorithm("native:intersection", params, memoryOutputName, filesystemOutputPath, context)
    return result

def clip(inputLayer, overlayLayer, memoryOutputName=None, filesystemOutputPath=None, context=None):
    params = {
        'INPUT': inputLayer,
        'OVERLAY': overlayLayer
    }
    
    result = runNativeAlgorithm("native:clip", params, memoryOutputName, filesystemOutputPath, context)
    return result

def bufferZero(inputLayer, memoryOutputName, filesystemOutputPath, context=None):
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

def fixGeometriesWithinLayer(inputLayer, memoryOutputName, filesystemOutputPath, context=None):
    params = {
        'INPUT':inputLayer
    }

    result = runNativeAlgorithm("native:fixgeometries", params, memoryOutputName, filesystemOutputPath, context)
    return result

def snapGeometriesWithinLayer(inputLayer, memoryOutputName, filesystemOutputPath, context=None):
    params = {
        'INPUT':inputLayer,
        'REFERENCE_LAYER':inputLayer,
        'TOLERANCE':1,
        'BEHAVIOR':1
    }

    result = runNativeAlgorithm("qgis:snapgeometries", params, memoryOutputName, filesystemOutputPath, context)
    return result

def snapGeometriesToLayer(inputLayer, referenceLayer, memoryOutputName, filesystemOutputPath, context=None):
    params = {
        'INPUT':inputLayer,
        'REFERENCE_LAYER':referenceLayer,
        'TOLERANCE':10,
        'BEHAVIOR':1
    }

    result = runNativeAlgorithm("qgis:snapgeometries", params, memoryOutputName, filesystemOutputPath, context)
    return result

def multipartToSinglePart(inputLayer, memoryOutputName, filesystemOutputPath, context=None):
    params = {
        'INPUT':inputLayer
    }

    result = runNativeAlgorithm("native:multiparttosingleparts", params, memoryOutputName, filesystemOutputPath, context)
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

if __name__ == '__main__':
    parseArguments()
    
    # See https://gis.stackexchange.com/a/155852/4972 for details about the prefix 
    QgsApplication.setPrefixPath('C:\\Program Files\\QGIS 3.22.13\\apps\\qgis-ltr', True)
    #qgs = QgsApplication([], False, "")
    qgs = QgsApplication([], False, r'C:\Sitka\Neptune\QGis', "server")

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

    clip_layer = None
    delineationLayer = None
    if CLIP_PATH is not None:
        clip_layer = fetchLayerFromGeoJson(CLIP_PATH, "ClipLayer")
        
    modelBasinLayer = fetchLayer("vPyQgisModelBasinLGUInput", "ModelBasinGeometry")
    regionalSubbasinLayer = fetchLayer("vPyQgisRegionalSubbasinLGUInput", "CatchmentGeometry")
    if RSB_IDs is not None:
        regionalSubbasinLayer.setSubsetString("RSBID in (" + RSB_IDs + ")")
    if PLANNED_PROJECT_ID is not None:
        delineationLayer = fetchLayer("vPyQgisProjectDelineationLGUInput", "DelineationGeometry")
        delineationLayer.setSubsetString("ProjectID is null or ProjectID=" + str(PLANNED_PROJECT_ID))
    else:
        delineationLayer = fetchLayer("vPyQgisDelineationLGUInput", "DelineationGeometry")
    wqmpLayer = fetchLayer("vPyQgisWaterQualityManagementPlanLGUInput", "WaterQualityManagementPlanBoundary")

    # perhaps overly-aggressive application of the buffer-zero and 
    modelBasinLayer = bufferZero(modelBasinLayer, "ModelBasins", None, context=PROCESSING_CONTEXT)
    regionalSubbasinLayer = bufferZero(regionalSubbasinLayer, "RegionalSubbasins", None, context=PROCESSING_CONTEXT)

    delineationLayer = snapGeometriesWithinLayer(delineationLayer, "DelineationSnapped", None, context=PROCESSING_CONTEXT)
    delineationLayer = bufferZero(delineationLayer, "Delineations", None, context=PROCESSING_CONTEXT)

    wqmpLayer = fixGeometriesWithinLayer(wqmpLayer, "WQMPFixed", None, context=PROCESSING_CONTEXT)
    wqmpLayer = snapGeometriesWithinLayer(wqmpLayer, "WQMPSnapped", None, context=PROCESSING_CONTEXT)
    wqmpLayer = bufferZero(wqmpLayer, "WQMP", None, context=PROCESSING_CONTEXT)

    if RSB_IDs is not None:
        #If we've got set RSBs we want only what's within those RSBs'
        regionalSubbasinLayerClipped = clip(regionalSubbasinLayer, regionalSubbasinLayer, "RSBClipped", None)
        delineationLayerClipped = clip(delineationLayer, regionalSubbasinLayer, "DelineationClipped", None)
        wqmpLayerClipped = clip(wqmpLayer, regionalSubbasinLayer, "WQMPClipped", None)
    else:
        # At present time, we're only concerned with the area covered by Model basins. 
        regionalSubbasinLayerClipped = clip(regionalSubbasinLayer, modelBasinLayer, "RSBClipped", None)
        delineationLayerClipped = clip(delineationLayer, modelBasinLayer, "DelineationClipped", None)
        wqmpLayerClipped = clip(wqmpLayer, modelBasinLayer, "WQMPClipped", None)

    wqmpLayerClipped = bufferZero(wqmpLayerClipped, "WQMP", None, context=PROCESSING_CONTEXT)

    rsb_wqmp = union(regionalSubbasinLayerClipped, wqmpLayerClipped, memoryOutputName="rsb_wqmp", None, context=PROCESSING_CONTEXT)
    #raiseIfLayerInvalid(lspc_rsb_delineation)
    rsb_wqmp = bufferZero(rsb_wqmp, "ModelBasin-RSB-D", None, context=PROCESSING_CONTEXT)


    # clip the model basin layer to the input boundary so that all further datasets will be clipped as well
    if clip_layer is not None:
        masterOverlay = union(rsb_wqmp, delineationLayerClipped, memoryOutputName="MasterOverlay", None, context=PROCESSING_CONTEXT)
        masterOverlay = clip(masterOverlay, clip_layer, memoryOutputName="MasterOverlay", None, context=PROCESSING_CONTEXT)
    else: 
        masterOverlay = union(rsb_wqmp, delineationLayerClipped, memoryOutputName="MasterOverlay", None, context=PROCESSING_CONTEXT)

    masterOverlay = multipartToSinglePart(masterOverlay, "SinglePartLGUs", None, context=PROCESSING_CONTEXT)

    masterOverlay.startEditing()

    for feat in masterOverlay.getFeatures():
        ## todo: would be nice to also exclude those where the RegionalSubbasinID is non-exist. could also handle that by making ModelBasin_RSB as an intersect instead of a union.
        if feat.geometry().area() < 1 or feat["RSBID"] is None:
            masterOverlay.deleteFeature(feat.id())
    
    masterOverlay.commitChanges()

    QgsVectorFileWriter.writeAsVectorFormat(masterOverlay, OUTPUT_PATH, "utf-8", delineationLayer.crs(), "ESRI Shapefile")


    #raiseIfLayerInvalid(masterOverlay)