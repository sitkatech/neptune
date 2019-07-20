import argparse
import os
from qgis.core import *


import sys
sys.path.append(r'C:\OSGeo4W64\apps\qgis\python\plugins')
import processing
from processing.core.Processing import Processing




if __name__ == '__main__':
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

    qgs.initQgis()

    app2 = Processing
    test3 = app2.initialize()
    # check registry files
    for alg in QgsApplication.processingRegistry().algorithms():
        print("{}:{} --> {}".format(alg.provider().name(), alg.name(), alg.displayName()))
    
