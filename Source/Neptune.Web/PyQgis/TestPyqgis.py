import os
from qgis.core import *

# Supply path to qgis install location
QgsApplication.setPrefixPath('C:/OSGEO4~1/apps/qgis', True)

# Create a reference to the QgsApplication.  Setting the
# second argument to False disables the GUI.
qgs = QgsApplication([], False)

# Load providers
qgs.initQgis()

# Write your code here to load some layers, use processing
# algorithms, etc.


path_to_backone_layer = r'test_pyqgis'

vlayer = QgsVectorLayer(path_to_backone_layer, "Backbone layer", "ogr")
if not vlayer.isValid():
	print("Failure!")
else:
        print("Success!")
        for field in vlayer.fields():
                print(field.name(), field.typeName())

# Finally, exit() is called to remove the
# provider and layer registries from memory

qgs.exit()
