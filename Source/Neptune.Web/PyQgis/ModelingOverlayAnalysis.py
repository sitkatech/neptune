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
    bufferZero,
    fixGeometriesWithinLayer,
    snapGeometriesWithinLayer,
    union,
    QgisError,
    fetchLayerFromFileSystem,
    intersection,
    bufferSnapFix,
    clip,
    snapGeometriesToLayer,
    multipartToSinglePart,
    writeVectorLayerToDisk
)

JOIN_PREFIX = "Joined_"
DATABASE_SERVER_NAME = "DATABASE SERVER NAME ERROR"
DATABASE_NAME = "DATABASE NAME ERROR"
DATABASE_USER_NAME = "DATABASE USER NAME ERROR"
DATABASE_PASSWORD = "DATABASE PASSWORD ERROR"
OUTPUT_FOLDER = "OUTPUT FOLDER ERROR"
OUTPUT_FILE_PREFIX = "OUTPUT FILE PREFIX ERROR"
CLIP_PATH = None
RSB_IDs = None
PLANNED_PROJECT_ID = None

def parseArguments():
    parser = argparse.ArgumentParser(description='Test PyQGIS connections to MSSQL')
    parser.add_argument('database_server_name', metavar='s', type=str, help='The name of the server where the database is located.')
    parser.add_argument('database_name', metavar='s', type=str, help='The name of the database to connect to.')
    parser.add_argument('database_username', metavar='s', type=str, help='The user name to use to connect to the database.')
    parser.add_argument('database_password', metavar='s', type=str, help='The password to use to connect to the database.')
    parser.add_argument('output_folder', metavar='d', type=str, help='The folder to write the final output to.')
    parser.add_argument('output_file_prefix', metavar='d', type=str, help='The filename prefix to write the final output to.')
    parser.add_argument('--planned_project_id', type=int, help='If running the overlay for a particular Project, this will add delineations for any Treatment BMPs who belong to this project')
    parser.add_argument('--rsb_ids', type=str, help='If present, filters the rsb layer down to only rsbs whose id is present in the list. Should be numbers separated by commas')
    parser.add_argument('--clip', type=str, help='The path to a geojson file containing the shape to clip inputs to')
    args = parser.parse_args()

    # this is easier to write than anything sane
    global DATABASE_SERVER_NAME
    global DATABASE_NAME
    global DATABASE_USER_NAME
    global DATABASE_PASSWORD
    global OUTPUT_FOLDER
    global OUTPUT_FILE_PREFIX
    global OUTPUT_FOLDER_AND_FILE_PREFIX
    global CLIP_PATH
    global PLANNED_PROJECT_ID
    global RSB_IDs
    DATABASE_SERVER_NAME = args.database_server_name
    DATABASE_NAME = args.database_name
    DATABASE_USER_NAME = args.database_username
    DATABASE_PASSWORD = args.database_password
    OUTPUT_FOLDER = args.output_folder
    OUTPUT_FILE_PREFIX = args.output_file_prefix
    OUTPUT_FOLDER_AND_FILE_PREFIX = OUTPUT_FOLDER + '\\' + OUTPUT_FILE_PREFIX

    if args.clip:
        CLIP_PATH = args.clip
        print(CLIP_PATH)

    if args.planned_project_id:
        PLANNED_PROJECT_ID = args.planned_project_id
        print(PLANNED_PROJECT_ID)

    if args.rsb_ids:
        RSB_IDs = args.rsb_ids
        print(RSB_IDs)

if __name__ == '__main__':
    parseArguments()
    
    # See https://gis.stackexchange.com/a/155852/4972 for details about the prefix 
    QgsApplication.setPrefixPath('C:\\Program Files\\QGIS 3.22.13\\apps\\qgis-ltr', True)
    #qgs = QgsApplication([], False, "")
    qgs = QgsApplication([], False, r'C:\Sitka\Neptune\QGis', "server")

    qgs.initQgis()
    
    Processing.initialize()
    #QgsApplication.processingRegistry().addProvider(QgsNativeAlgorithms())

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
        clip_layer = fetchLayerFromFileSystem(CLIP_PATH, "ClipLayer")
        
    if PLANNED_PROJECT_ID is not None:
        delineationLayer = fetchLayer("vPyQgisProjectDelineationLGUInput", "DelineationGeometry")
        delineationLayer.setSubsetString("ProjectID is null or ProjectID=" + str(PLANNED_PROJECT_ID))
    else:
        delineationLayer = fetchLayer("vPyQgisDelineationLGUInput", "DelineationGeometry")

    delineationLayer_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'delineationLayer.geojson'
    writeVectorLayerToDisk(delineationLayer, delineationLayer_path, "GeoJSON")
    delineationLayer_buffersnapfixpath = OUTPUT_FOLDER_AND_FILE_PREFIX + 'delineationLayer_buffersnapfix.geojson'
    delineationLayerResult = bufferSnapFix(delineationLayer_path, delineationLayer_buffersnapfixpath, PROCESSING_CONTEXT)

    modelBasinLayer = fetchLayer("vPyQgisModelBasinLGUInput", "ModelBasinGeometry")
    modelBasinLayer_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'modelBasinLayer.geojson'
    writeVectorLayerToDisk(modelBasinLayer, modelBasinLayer_path, "GeoJSON")
    modelBasinLayer_buffersnapfixpath = OUTPUT_FOLDER_AND_FILE_PREFIX + 'modelBasinLayer_buffersnapfix.geojson'
    modelBasinLayerResult = bufferSnapFix(modelBasinLayer_path, modelBasinLayer_buffersnapfixpath, PROCESSING_CONTEXT)

    regionalSubbasinLayer = fetchLayer("vPyQgisRegionalSubbasinLGUInput", "CatchmentGeometry")
    if RSB_IDs is not None:
        regionalSubbasinLayer.setSubsetString("RSBID in (" + RSB_IDs + ")")
    regionalSubbasinLayer_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'regionalSubbasinLayer.geojson'
    writeVectorLayerToDisk(regionalSubbasinLayer, regionalSubbasinLayer_path, "GeoJSON")
    regionalSubbasinLayer_buffersnapfixpath = OUTPUT_FOLDER_AND_FILE_PREFIX + 'regionalSubbasinLayer_buffersnapfix.geojson'
    regionalSubbasinLayerResult = bufferSnapFix(regionalSubbasinLayer_path, regionalSubbasinLayer_buffersnapfixpath, PROCESSING_CONTEXT)

    wqmpLayer = fetchLayer("vPyQgisWaterQualityManagementPlanLGUInput", "WaterQualityManagementPlanBoundary")
    wqmpLayer_path = OUTPUT_FOLDER_AND_FILE_PREFIX + 'wqmpLayer.geojson'
    writeVectorLayerToDisk(wqmpLayer, wqmpLayer_path, "GeoJSON")
    wqmpLayer_buffersnapfixpath = OUTPUT_FOLDER_AND_FILE_PREFIX + 'wqmpLayer_buffersnapfix.geojson'
    wqmpLayerResult = bufferSnapFix(wqmpLayer_path, wqmpLayer_buffersnapfixpath, PROCESSING_CONTEXT)

    if RSB_IDs is not None:
        #If we've got set RSBs we want only what's within those RSBs'
        regionalSubbasinLayerClipped = clip(regionalSubbasinLayer_buffersnapfixpath, regionalSubbasinLayer_buffersnapfixpath, "RSBClipped", None)
        delineationLayerClipped = clip(delineationLayer_buffersnapfixpath, regionalSubbasinLayer_buffersnapfixpath, "DelineationClipped", None)
        wqmpLayerClipped = clip(wqmpLayer_buffersnapfixpath, regionalSubbasinLayer_buffersnapfixpath, "WQMPClipped", None)
    else:
        # At present time, we're only concerned with the area covered by Model basins. 
        regionalSubbasinLayerClipped = clip(regionalSubbasinLayer_buffersnapfixpath, modelBasinLayer_buffersnapfixpath, "RSBClipped", None)
        delineationLayerClipped = clip(delineationLayer_buffersnapfixpath, modelBasinLayer_buffersnapfixpath, "DelineationClipped", None)
        wqmpLayerClipped = clip(wqmpLayer_buffersnapfixpath, modelBasinLayer_buffersnapfixpath, "WQMPClipped", None)

    wqmpLayerClipped = bufferZero(wqmpLayerClipped, "WQMP", None, PROCESSING_CONTEXT)

    rsb_wqmp = union(regionalSubbasinLayerClipped, wqmpLayerClipped, "rsb_wqmp", None, PROCESSING_CONTEXT)
    #raiseIfLayerInvalid(lspc_rsb_delineation)
    rsb_wqmp = bufferZero(rsb_wqmp, "ModelBasin-RSB-D", None, PROCESSING_CONTEXT)


    # clip the model basin layer to the input boundary so that all further datasets will be clipped as well
    if clip_layer is not None:
        masterOverlay = union(rsb_wqmp, delineationLayerClipped, "MasterOverlay", None, PROCESSING_CONTEXT)
        masterOverlay = clip(masterOverlay, clip_layer, "MasterOverlay", None, PROCESSING_CONTEXT)
    else: 
        masterOverlay = union(rsb_wqmp, delineationLayerClipped, "MasterOverlay", None, PROCESSING_CONTEXT)

    masterOverlay = multipartToSinglePart(masterOverlay, "SinglePartLGUs", None, PROCESSING_CONTEXT)

    masterOverlay.startEditing()

    for feat in masterOverlay.getFeatures():
        ## todo: would be nice to also exclude those where the RegionalSubbasinID is non-exist. could also handle that by making ModelBasin_RSB as an intersect instead of a union.
        if feat.geometry().area() < 1 or feat["RSBID"] is None:
            masterOverlay.deleteFeature(feat.id())
    
    masterOverlay.commitChanges()

    finalOutputPath = OUTPUT_FOLDER_AND_FILE_PREFIX + '.geojson'
    writeVectorLayerToDisk(masterOverlay, finalOutputPath, "GeoJSON")

    #raiseIfLayerInvalid(masterOverlay)