from qgis.core import (
     QgsApplication, 
     QgsProcessingFeedback, 
     QgsVectorLayer,
     QgsFeatureRequest,
     QgsDataSourceUri,
     QgsFeature,
     QgsProject,
     QgsVectorFileWriter,
     QgsCoordinateReferenceSystem,
     QgsCoordinateTransform,
     QgsWkbTypes
)

import processing
from processing.tools import dataobjects
from processing.core.Processing import Processing

# Create an exact duplicate of the input layer.
# Needed because the processing framework cannot actually operate with the same layer as multiple inputs to an algorithm
# This is infuriating if you've ever used the QGIS GUI, where it looks like you can set the same layer for more than one input
# One concludes, after much trial and error, that the processing framework is actually operating on two separate copies of the layer behind the scenes.
def duplicateLayer(qgs_vector_layer, duplicate_layer_name):
    # fixme: we might never use layer types other than polygon, but we might want this parameterized in the future
    layer_dupe = QgsVectorLayer("MultiPolygon?crs=epsg:2771", duplicate_layer_name, "memory")

    mem_layer_data = layer_dupe.dataProvider()

    feats = [feat for feat in qgs_vector_layer.getFeatures()]
    attr = qgs_vector_layer.dataProvider().fields().toList()
    mem_layer_data.addAttributes(attr)
    layer_dupe.updateFields()
    mem_layer_data.addFeatures(feats)
    #print("Geom type:" + QgsWkbTypes.geometryDisplayString(layer_dupe.geometryType()))
    return layer_dupe

class QgisError(Exception):
    def __init__(self,  message):
        self.message = message

def raiseIfLayerInvalid(qgsVectorLayer):
    if not qgsVectorLayer.isValid():
        raise QgisError("Failed to load/generate " + qgsVectorLayer.sourceName())
    else:
        print("Loaded/generated " + qgsVectorLayer.sourceName())

def fetchLayerFromDatabase(uri, spatialTableName, geometryColumn):
    uri.setDataSource("dbo", spatialTableName, geometryColumn)
#    uri.setSrid("2771")
    layer = QgsVectorLayer(uri.uri(), spatialTableName, "mssql")
    raiseIfLayerInvalid(layer)
    return layer

def fetchLayerFromGeoJson(path, name):
    layer = QgsVectorLayer(path, name, "ogr")
    raiseIfLayerInvalid(layer)
    return layer

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

def writeVectorLayerToDisk(layer, output_path, driverName):
    transform_context = QgsProject.instance().transformContext()
    save_options = QgsVectorFileWriter.SaveVectorOptions()
    save_options.driverName = driverName
    save_options.fileEncoding = "UTF-8"
    save_options.ct = QgsCoordinateTransform(QgsCoordinateReferenceSystem('EPSG:2771'), QgsCoordinateReferenceSystem('EPSG:2771'), transform_context)
    error = QgsVectorFileWriter.writeAsVectorFormatV3(layer, output_path, transform_context, save_options)
    if error[0] == QgsVectorFileWriter.NoError:
        print("Saved to " + output_path)
    else:
      print(error)
