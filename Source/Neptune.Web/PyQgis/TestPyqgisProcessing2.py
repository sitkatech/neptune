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

if __name__ == '__main__':
    connstring_base = parseConnstring()
    
    # See https://gis.stackexchange.com/a/155852/4972 for details about the prefix 
    QgsApplication.setPrefixPath(r'C:\OSGEO4W64\apps\qgis', True)
    qgs = QgsApplication([], False, "")
    qgs.initQgis()


    
    Processing.initialize()
    QgsApplication.processingRegistry().addProvider(QgsNativeAlgorithms())

    #connstring_stormwaterjurisdiction = connstring_base + "tables=dbo.StormwaterJurisdiction"
    #connstring_watershed = connstring_base + "tables=dbo.Watershed"

    ## check registry files
    ##for alg in QgsApplication.processingRegistry().algorithms():
    ##    print("{}:{} --> {}".format(alg.provider().name(), alg.name(), alg.displayName()))

    #layer1 = QgsVectorLayer(connstring_stormwaterjurisdiction, 'StormwaterJurisdiction', 'ogr')
    #layer2 = QgsVectorLayer(connstring_watershed, 'Watershed', 'ogr')

    #params = { 
    #    'INPUT' : layer1,
    #    'OVERLAY': layer2,
    #    'OVERLAY_FIELDS_PREFIX': '',
    #    'OUTPUT' : r'C:\temp\output.shp',
    #}
    #feedback = QgsProcessingFeedback()

    context = dataobjects.createContext()
    context.setInvalidGeometryCheck(QgsFeatureRequest.GeometrySkipInvalid)

    #res = processing.run('native:union', params, feedback=feedback, context=context)
    #res['OUTPUT'] # Access your output layer

    #Get the delineation Layer

    uri = QgsDataSourceUri()
    connstring_delineation = connstring_base + "tables=dbo.vDelineationTGUInput"

    delineation_layer = QgsVectorLayer(connstring_delineation, "vDelineationTGUInput1", "ogr")
    delineation_layer_dupe = QgsVectorLayer("Polygon?crs=epsg:2771", "vDelineationTGUInput2", "memory")

    feats = [feat for feat in delineation_layer.getFeatures()]

    mem_layer_data = delineation_layer_dupe.dataProvider()
    attr = delineation_layer.dataProvider().fields().toList()
    mem_layer_data.addAttributes(attr)
    delineation_layer_dupe.updateFields()
    mem_layer_data.addFeatures(feats)

    if not delineation_layer.isValid():
            print("Layer failed to load!")
    else:
            print("Loaded Delineation layer!")


    # it is unnecessary to filter by IsVerified because the view already does

    # The following pipeline identifies all fields that don't contribute to intersections within the delineation layer

    res = processing.run("qgis:joinattributesbylocation", {
	    'INPUT':delineation_layer,
	    'JOIN':delineation_layer_dupe,
	    'PREDICATE': '5',
	    'JOIN_FIELDS':'',
	    'METHOD':'0',
	    'DISCARD_NONMATCHING':False,
	    'PREFIX':'',
	    'OUTPUT':r'memory:delineation_overlaps'
    }, context=context)

    intersect_contrib_feats = res['OUTPUT']
        
    if intersect_contrib_feats.isValid():
        print("Intersection contributing features: " + str(intersect_contrib_feats.featureCount()))
    else:
        print("Output layer invalid :(")

    qgs.exitQgis()