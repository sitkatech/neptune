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
     QgsDataSourceUri
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
    
    # (1) Get the delineation layer and a duplicate
    # Do note that the view here has all input filters built into it

    connstring_delineation = connstring_base + "tables=dbo.vDelineationTGUInput"
    delineation_layer = QgsVectorLayer(connstring_delineation, "Delineations", "ogr")
    
    if not delineation_layer.isValid():
        print("Layer failed to load!")
    else:
        print("Loaded Delineation layer!")

    delineation_layer_dupe = duplicateLayer(delineation_layer, "Delineations (Duplicate)")

    # (2) Identify all fields that contribute to intersections

    join_prefix = "Joined_"

    res = processing.run("qgis:joinattributesbylocation", {
	    'INPUT':delineation_layer,
	    'JOIN':delineation_layer_dupe,
	    'PREDICATE': ['4','5'], # 4 := Overlaps, 5:= Within
	    'JOIN_FIELDS':'',
	    'METHOD':'0',
	    'DISCARD_NONMATCHING':False,
	    'PREFIX': join_prefix,
	    'OUTPUT':r'memory:delineation_overlaps'
    }, context=context)

    intersect_contrib_layer = res['OUTPUT']
        
    if intersect_contrib_layer.isValid():
        print("Intersection contributing features: " + str(intersect_contrib_layer.featureCount()))
    else:
        print("Output layer invalid :(")

    # (3) and (4) Isolate the geometries from above
    # a) We have to filter layer (2) to ignore the rows where both delineation IDs are equal (a field is always within itself)
    # b) We also have to ignore duplicate records where the delineation IDs on the left and right are swapped.
    # putting (a) and (b) together gives us the following, frustratingly simple, filter:

    filter_icl = QgsFeatureRequest()
    filter_icl.setFilterExpression("DelineationID < {joined_id}DelineationID".format(joined_id = join_prefix))

    left_ic_layer = QgsVectorLayer("Polygon?crs=epsg:4326", "Left IC Layer", "memory")
    right_ic_layer = QgsVectorLayer("Polygon?crs=epsg:4326", "Right IC Layer", "memory")
    assignFieldsToLayerFromSourceLayer(left_ic_layer, delineation_layer)
    assignFieldsToLayerFromSourceLayer(right_ic_layer, delineation_layer)
    ids_to_add_to_left_ic_layer = set()
    ids_to_add_to_right_ic_layer = set()


    
    for feat in intersect_contrib_layer.getFeatures(filter_icl):
        ids_to_add_to_left_ic_layer.add(feat["DelineationID"])
        ids_to_add_to_right_ic_layer.add(feat["{joined_id}DelineationID".format(joined_id = join_prefix)])

    print(ids_to_add_to_left_ic_layer)
    print(ids_to_add_to_right_ic_layer)

    qgs.exitQgis()