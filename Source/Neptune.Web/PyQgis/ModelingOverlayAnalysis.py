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
sys.path.append(r'C:\OSGeo4W64\apps\qgis\python\plugins')

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
CONNSTRING_BASE = "CONNSTRING ERROR"
OUTPUT_PATH = "OUTPUT PATH ERROR"
CLIP_PATH = None
RSB_IDs = None
PLANNED_PROJECT_ID = None

def parseArguments():
    parser = argparse.ArgumentParser(description='Test PyQGIS connections to MSSQL')
    parser.add_argument('connstring', metavar='s', type=str, help='The connection string. Do not specify tables; the script will specify which table(s) it wants to look at.')
    parser.add_argument('output_path', metavar='d', type=str, help='The path to write the final output to.')
    parser.add_argument('--planned_project_id', type=int, help='If running the overlay for a particular Project, this will add delineations for any Treatment BMPs who belong to this project')
    parser.add_argument('--rsb_ids', type=str, help='If present, filters the rsb layer down to only rsbs whose id is present in the list. Should be numbers separated by commas')
    parser.add_argument('--clip', type=str, help='The path to a geojson file containing the shape to clip inputs to')
    args = parser.parse_args()

    # this is easier to write than anything sane
    global CONNSTRING_BASE
    global OUTPUT_PATH
    global CLIP_PATH
    global PLANNED_PROJECT_ID
    global RSB_IDs
    CONNSTRING_BASE = "MSSQL:" + args.connstring
    OUTPUT_PATH = args.output_path
    print(OUTPUT_PATH)

    if not CONNSTRING_BASE.endswith(";"):
            CONNSTRING_BASE = CONNSTRING_BASE + ";"
    if "True" in CONNSTRING_BASE:
            CONNSTRING_BASE = CONNSTRING_BASE.replace("True", "Yes")

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

    if memoryOutputName is not None:
        params['OUTPUT'] = 'memory:' + memoryOutputName
    elif filesystemOutputPath is not None:
        params['OUTPUT'] = filesystemOutputPath
    else:
        raise QgisError("No output provided for union operation")

    if context is not None:
        unionResult = processing.run("native:union", params, context = context)
    else: 
        unionResult = processing.run("native:union", params)
    
    return unionResult['OUTPUT']

def intersection(inputLayer, overlayLayer, memoryOutputName=None, filesystemOutputPath=None, context = None):
    params = {
        'INPUT': inputLayer,
        'OVERLAY': overlayLayer,
        'INPUT_FIELDS':[],
        'OVERLAY_FIELDS':[],
        'OVERLAY_FIELDS_PREFIX':''
    }

    if memoryOutputName is not None:
        params['OUTPUT'] = 'memory:' + memoryOutputName
    elif filesystemOutputPath is not None:
        params['OUTPUT'] = filesystemOutputPath
    else:
        raise QgisError("No output provided for intersection operation")

    if context is not None:
        intersectionResult = processing.run("native:intersection", params, context = context)
    else: 
        intersectionResult = processing.run("native:intersection", params)
    
    return intersectionResult['OUTPUT']

def clip(inputLayer, overlayLayer, memoryOutputName=None, filesystemOutputPath=None, context=None):
    params = {
        'INPUT': inputLayer,
        'OVERLAY': overlayLayer
    }

    if memoryOutputName is not None:
        params['OUTPUT'] = 'memory:' + memoryOutputName
    elif filesystemOutputPath is not None:
        params['OUTPUT'] = filesystemOutputPath
    else:
        raise QgisError("No output provided for union operation")

    if context is not None:
        clipResult = processing.run("native:clip", params, context = context)
    else: 
        clipResult = processing.run("native:clip", params)
    
    return clipResult['OUTPUT']

def bufferZero(inputLayer, memoryOutputName, context=None):
    params = {
        'INPUT':inputLayer,
        'DISTANCE':0,
        'SEGMENTS':5,
        'END_CAP_STYLE':1,
        'JOIN_STYLE':1,
        'MITER_LIMIT':2,
        'DISSOLVE':False,
        'OUTPUT':'memory:' + memoryOutputName
    }
    
    if context is not None:
        bufferZeroResult = processing.run("native:buffer", params ,context = context)
    else:
        bufferZeroResult = processing.run("native:buffer", params)
    
    return bufferZeroResult['OUTPUT']

def fixGeometriesWithinLayer(inputLayer, memoryOutputName, context=None):
    params = {
        'INPUT':inputLayer,
        'OUTPUT':'memory:'+memoryOutputName}
    if context is not None:
        fixResult = processing.run("native:fixgeometries", params)
    else:
        fixResult = processing.run("native:fixgeometries", params, context= context)

    return fixResult['OUTPUT']

def snapGeometriesWithinLayer(inputLayer, memoryOutputName, context=None):
    params = {
        'INPUT':inputLayer,
        'REFERENCE_LAYER':inputLayer,
        'TOLERANCE':1,
        'BEHAVIOR':1,
        'OUTPUT':'TEMPORARY_OUTPUT'
    }
    
    if context is not None:
        snapResult = processing.run("qgis:snapgeometries", params, context = context)
    else:
        snapResult = processing.run("qgis:snapgeometries", params)

    return snapResult['OUTPUT']

def snapGeometriesToLayer(inputLayer, referenceLayer, memoryOutputName, context=None):
    params = {
        'INPUT':inputLayer,
        'REFERENCE_LAYER':referenceLayer,
        'TOLERANCE':10,
        'BEHAVIOR':1,
        'OUTPUT':'memory:'+memoryOutputName
    }
    
    if context is not None:
        snapResult = processing.run("qgis:snapgeometries", params, context = context)
    else:
        snapResult = processing.run("qgis:snapgeometries", params)

    return snapResult['OUTPUT']

def multipartToSinglePart(inputLayer, memoryOutputName, context=None):
    params = {
        'INPUT':inputLayer,
        'OUTPUT':'memory:'+memoryOutputName
    }

    if context is not None:
        mtsResult =  processing.run("native:multiparttosingleparts", params, context=context)
    else:
        mtsResult = processing.run("native:multiparttosingleparts", params)
    
    return mtsResult['OUTPUT']

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

    def fetchLayer(spatialTableName):
        return fetchLayerFromDatabase(CONNSTRING_BASE, spatialTableName)

    clip_layer = None
    delineationLayer = None
    if CLIP_PATH is not None:
        clip_layer = fetchLayerFromGeoJson(CLIP_PATH, "ClipLayer")
        
    modelBasinLayer = fetchLayer("vModelBasinLGUInput")
    regionalSubbasinLayer = fetchLayer("vRegionalSubbasinLGUInput")
    if RSB_IDs is not None:
        regionalSubbasinLayer.setSubsetString("RSBID in (" + RSB_IDs + ")")
    if PLANNED_PROJECT_ID is not None:
        nonProjectDelineationLayer = fetchLayer("vDelineationLGUInput")
        projectDelineationLayer = fetchLayer("vPlannedProjectDelineationLGUInput")
        projectDelineationLayer.setSubsetString("ProjectID=" + PLANNED_PROJECT_ID)
        delineationLayer = union(regionalSubbasinLayerClipped, wqmpLayerClipped, memoryOutputName="delineationLayer", context=PROCESSING_CONTEXT)
    else:
        delineationLayer = fetchLayer("vDelineationLGUInput")
    wqmpLayer = fetchLayer("vWaterQualityManagementPlanLGUInput")

    # perhaps overly-aggressive application of the buffer-zero and 
    modelBasinLayer = bufferZero(modelBasinLayer, "ModelBasins", context=PROCESSING_CONTEXT)
    regionalSubbasinLayer = bufferZero(regionalSubbasinLayer, "RegionalSubbasins", context=PROCESSING_CONTEXT)

    delineationLayer = snapGeometriesWithinLayer(delineationLayer, "DelineationSnapped", context=PROCESSING_CONTEXT)
    delineationLayer = bufferZero(delineationLayer, "Delineations", context=PROCESSING_CONTEXT)

    wqmpLayer = fixGeometriesWithinLayer(wqmpLayer, "WQMPFixed", context=PROCESSING_CONTEXT)
    wqmpLayer = snapGeometriesWithinLayer(wqmpLayer, "WQMPSnapped", context=PROCESSING_CONTEXT)
    wqmpLayer = bufferZero(wqmpLayer, "WQMP", context=PROCESSING_CONTEXT)

    if RSB_IDs is not None:
        #If we've got set RSBs we want only what's within those RSBs'
        regionalSubbasinLayerClipped = clip(regionalSubbasinLayer, regionalSubbasinLayer, "RSBClipped")
        delineationLayerClipped = clip(delineationLayer, regionalSubbasinLayer, "DelineationClipped")
        wqmpLayerClipped = clip(wqmpLayer, regionalSubbasinLayer, "WQMPClipped")
    else:
        # At present time, we're only concerned with the area covered by Model basins. 
        regionalSubbasinLayerClipped = clip(regionalSubbasinLayer, modelBasinLayer, "RSBClipped")
        delineationLayerClipped = clip(delineationLayer, modelBasinLayer, "DelineationClipped")
        wqmpLayerClipped = clip(wqmpLayer, modelBasinLayer, "WQMPClipped")

    wqmpLayerClipped = bufferZero(wqmpLayerClipped, "WQMP", context=PROCESSING_CONTEXT)

    rsb_wqmp = union(regionalSubbasinLayerClipped, wqmpLayerClipped, memoryOutputName="rsb_wqmp", context=PROCESSING_CONTEXT)
    #raiseIfLayerInvalid(lspc_rsb_delineation)
    rsb_wqmp = bufferZero(rsb_wqmp, "ModelBasin-RSB-D", context=PROCESSING_CONTEXT)


    # clip the model basin layer to the input boundary so that all further datasets will be clipped as well
    if clip_layer is not None:
        masterOverlay = union(rsb_wqmp, delineationLayerClipped, memoryOutputName="MasterOverlay", context=PROCESSING_CONTEXT)
        masterOverlay = clip(masterOverlay, clip_layer, memoryOutputName="MasterOverlay", context=PROCESSING_CONTEXT)
    else: 
        masterOverlay = union(rsb_wqmp, delineationLayerClipped, memoryOutputName="MasterOverlay", context=PROCESSING_CONTEXT)

    masterOverlay = multipartToSinglePart(masterOverlay, "SinglePartLGUs", context=PROCESSING_CONTEXT)

    masterOverlay.startEditing()

    for feat in masterOverlay.getFeatures():
        ## todo: would be nice to also exclude those where the RegionalSubbasinID is non-exist. could also handle that by making ModelBasin_RSB as an intersect instead of a union.
        if feat.geometry().area() < 1 or feat["RSBID"] is None:
            masterOverlay.deleteFeature(feat.id())
    
    masterOverlay.commitChanges()

    QgsVectorFileWriter.writeAsVectorFormat(masterOverlay, OUTPUT_PATH, "utf-8", delineationLayer.crs(), "ESRI Shapefile")


    #raiseIfLayerInvalid(masterOverlay)