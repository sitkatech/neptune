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

    return layer_dupe

class QgisError(Exception):
    def __init__(self,  message):
        self.message = message

def raiseIfLayerInvalid(qgsVectorLayer):
    if not qgsVectorLayer.isValid():
        raise QgisError("Failed to load/generate " + qgsVectorLayer.sourceName())
    else:
        print("Loaded/generated " + qgsVectorLayer.sourceName())

def fetchLayerFromDatabase(connectionString, spatialTableName):
    qualifiedConnectionString = connectionString + "tables=dbo." + spatialTableName
    layer = QgsVectorLayer(qualifiedConnectionString, spatialTableName, "ogr")
    raiseIfLayerInvalid(layer)
    return layer