### Authored by Nicholas Padinha for Sitka Technology Group, 2020

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

def parseArguments():
    parser = argparse.ArgumentParser(description='Test PyQGIS connections to MSSQL')
    parser.add_argument('connstring', metavar='s', type=str, help='The connection string. Do not specify tables; the script will specify which table(s) it wants to look at.')
    parser.add_argument('output_path', metavar='d', type=str, help='The path to write the final output to.')
    parser.add_argument('--clip', type=str, help='The path to a geojson file containing the shape to clip inputs to')

    args = parser.parse_args()

    # this is easier to write than anything sane
    global CONNSTRING_BASE
    global OUTPUT_PATH
    global CLIP_PATH
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

def snapGeometriesWithinLayer(inputLayer, memoryOutputName, context=None):
    params = {
        'INPUT':inputLayer,
        'REFERENCE_LAYER':inputLayer,
        'TOLERANCE':5,
        'BEHAVIOR':1,
        'OUTPUT':'TEMPORARY_OUTPUT'
    }
    
    if context is not None:
        snapResult = processing.run("qgis:snapgeometries", params, context = context)
    else:
        snapResult = processing.run("qgis:snapgeometries", params)

    return snapResult['OUTPUT']

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
    if CLIP_PATH is not None:
        clip_layer = fetchLayerFromGeoJson(CLIP_PATH, "ClipLayer")
        
    lspcLayer = fetchLayer("vLSPCBasinLGUInput")
    regionalSubbasinLayer = fetchLayer("vRegionalSubbasinLGUInput")
    delineationLayer = fetchLayer("vDelineationLGUInput")
    wqmpLayer = fetchLayer("vWaterQualityManagementPlanLGUInput")

    # perhaps overly-aggressive application of the buffer-zero and 
    lspcLayer = bufferZero(lspcLayer, "LSPCBasins", context=PROCESSING_CONTEXT)
    regionalSubbasinLayer = bufferZero(regionalSubbasinLayer, "RegionalSubbasins", context=PROCESSING_CONTEXT)

    delineationLayer = snapGeometriesWithinLayer(delineationLayer, "DelineationSnapped", context=PROCESSING_CONTEXT)
    delineationLayer = bufferZero(delineationLayer, "Delineations", context=PROCESSING_CONTEXT)

    wqmpLayer = snapGeometriesWithinLayer(wqmpLayer, "WQMPSnapped", context=PROCESSING_CONTEXT)
    wqmpLayer = bufferZero(wqmpLayer, "WQMP", context=PROCESSING_CONTEXT)

    QgsVectorFileWriter.writeAsVectorFormat(delineationLayer, 'C:\\temp\\snappo.shp', "utf-8", delineationLayer.crs(), "ESRI Shapefile")

    # At present time, we're only concerned with the area covered by LSPC basins. 
    regionalSubbasinLayerClipped = clip(regionalSubbasinLayer, lspcLayer, "RSBClipped")
    delineationLayerClipped = clip(delineationLayer, lspcLayer, "DelineationClipped")
    wqmpLayerClipped = clip(wqmpLayer, lspcLayer, "WQMPClipped")

    wqmpLayerClipped = bufferZero(wqmpLayerClipped, "WQMP", context=PROCESSING_CONTEXT)

    lspc_rsb = union(lspcLayer, regionalSubbasinLayerClipped, memoryOutputName="lspc_rsb", context=PROCESSING_CONTEXT)
    #raiseIfLayerInvalid(lspc_rsb)
    lspc_rsb = bufferZero(lspc_rsb, "LSPC-RSB", context=PROCESSING_CONTEXT)

    lspc_rsb_wqmp = union(lspc_rsb, wqmpLayerClipped, memoryOutputName="lspc_rsb_wqmp", context=PROCESSING_CONTEXT)
    #raiseIfLayerInvalid(lspc_rsb_delineation)
    lspc_rsb_wqmp = bufferZero(lspc_rsb_wqmp, "LSPC-RSB-D", context=PROCESSING_CONTEXT)


    # clip the lspc layer to the input boundary so that all further datasets will be clipped as well
    if clip_layer is not None:
        masterOverlay = union(lspc_rsb_wqmp, delineationLayerClipped, memoryOutputName="MasterOverlay", context=PROCESSING_CONTEXT)
        masterOverlayClipped = clip(masterOverlay, clip_layer, filesystemOutputPath=OUTPUT_PATH, context=PROCESSING_CONTEXT)
    else: 
        masterOverlay = union(lspc_rsb_wqmp, delineationLayerClipped, filesystemOutputPath=OUTPUT_PATH, context=PROCESSING_CONTEXT)

    #raiseIfLayerInvalid(masterOverlay)