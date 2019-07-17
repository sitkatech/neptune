import argparse
import os
from qgis.core import *
from qgis.core import QgsDataSourceUri

parser = argparse.ArgumentParser(description='Test PyQGIS connections to MSSQL')
parser.add_argument('connstring', metavar='s', type=str, help='The connection string. This test will only run if the connection string points to a Neptune DB. Do not specify tables; the script will specify which table(s) it wants to look at.')

args = parser.parse_args()

connstring_base = "MSSQL:" + args.connstring

if not connstring_base.endswith(";"):
        connstring_base = connstring_base + ";"
if "True" in connstring_base:
        connstring_base = connstring_base.replace("True", "Yes")


QgsApplication.setPrefixPath('C:/OSGEO4~1/apps/qgis', True)

qgs = QgsApplication([], False)

print("I haven't tried to hit the database yet...")

qgs.initQgis()

uri = QgsDataSourceUri()

backbone_connstring = connstring_base + "tables=dbo.BackboneSegment"

vlayer = QgsVectorLayer(backbone_connstring, "BackboneSegment", "ogr")

## refactor at this point 

if not vlayer.isValid():
        print("Layer failed to load!")
        print("Base connection string: " + connstring_base)
        print("Connection string with table: " + backbone_connstring)
        print(args)
else:
        print("Loaded BackboneSegment layer!")

request = QgsFeatureRequest()

print("Looking for BackboneSegment with ID = 1...")

request.setFilterExpression("BackboneSegmentID = 1")

features = vlayer.getFeatures(request)

for feature in features:
        print("Found. WKT:")
        print(feature.geometry().asWkt())        

qgs.exitQgis()
