/*-----------------------------------------------------------------------
<copyright file="Ogr2OgrCommandLineRunner.cs" company="Sitka Technology Group">
Copyright (c) Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using LtInfo.Common.DesignByContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LtInfo.Common.GdalOgr
{
    /// <summary>
    /// Wrapper class for calling ogr2ogr.exe for the purpose of importing data from a File Geodatabase (.gdb) using the OpenFileGDB drivers in GDAL 1.11 and above
    /// </summary>
    public class Ogr2OgrCommandLineRunner
    {
        public const int DefaultCoordinateSystemId = 2771;
        public const int DefaultTimeOut = 210000;
        public const string OgrGeoJsonTableName = "OGRGeoJSON";

        public const string GEOMETRY_TYPE_POLYGON = "POLYGON";
        public const string GEOMETRY_TYPE_MULTIPOLYGON = "MULTIPOLYGON";

        private readonly FileInfo _ogr2OgrExecutable;
        private readonly int _coordinateSystemId;
        private readonly double _totalMilliseconds;
        protected readonly DirectoryInfo _gdalDataPath;
        protected readonly DirectoryInfo _gdalProjLibPath;
        protected readonly DirectoryInfo _gdalDriversPath;

        public Ogr2OgrCommandLineRunner(string pathToOgr2OgrExecutable, int coordinateSystemId, double totalMilliseconds)
        {
            _totalMilliseconds = totalMilliseconds;
            _ogr2OgrExecutable = new FileInfo(pathToOgr2OgrExecutable);
            _coordinateSystemId = coordinateSystemId;
            Check.RequireFileExists(_ogr2OgrExecutable, "Can't find ogr2ogr program in expected path. Is it installed?");
            Check.RequireNotNull(_ogr2OgrExecutable.Directory,
                $"ogr2ogr must be a full path including directory but was \"{_ogr2OgrExecutable.FullName}\"");
            // ReSharper disable once PossibleNullReferenceException
            _gdalDataPath = new DirectoryInfo(Path.Combine(_ogr2OgrExecutable.Directory.FullName, "gdal-data"));
            Check.RequireDirectoryExists(_gdalDataPath.FullName, "Can't find gdal-data directory needed for import with ogr2ogr");
            _gdalProjLibPath = new DirectoryInfo(Path.Combine(_ogr2OgrExecutable.Directory.FullName, "projlib"));
            Check.RequireDirectoryExists(_gdalProjLibPath.FullName, "Can't find projlib directory needed for import with ogr2ogr");
            _gdalDriversPath = new DirectoryInfo(Path.Combine(_ogr2OgrExecutable.Directory.FullName, "gdalplugins"));
            Check.RequireDirectoryExists(_gdalDriversPath.FullName, "Can't find gdalplugins directory needed for import with ogr2ogr");
        }

        /// <summary>
        /// Import GDB to SQL using GDAL Ogr2Ogr command line tool
        /// </summary>
        public void ImportFileGdbToMsSql(FileInfo inputGdbFile, string sourceLayerName, string destinationTableName,
            List<string> columnNameList, string connectionString, bool enforceGeometryType,
            string geometryTypeToEnforce, string whereClause)
        {
            Check.Require(inputGdbFile.FullName.ToLower().EndsWith(".gdb.zip"),
                $"Input filename for GDB input must end with .gdb.zip. Filename passed is {inputGdbFile.FullName}");
            Check.RequireFileExists(inputGdbFile, "Can't find input File GDB for import with ogr2ogr");

            var databaseConnectionString = $"MSSQL:{connectionString}";
            var commandLineArguments = BuildCommandLineArgumentsForFileGdbToMsSql(inputGdbFile, databaseConnectionString, sourceLayerName, destinationTableName, columnNameList, _coordinateSystemId, enforceGeometryType, geometryTypeToEnforce, whereClause);
            ExecuteOgr2OgrCommand(commandLineArguments);
        }
        
        public string ImportFileGdbToGeoJson(FileInfo inputGdbFile, string sourceLayerName, bool explodeCollections)
        {
            Check.Require(inputGdbFile.FullName.ToLower().EndsWith(".gdb.zip"),
                $"Input filename for GDB input must end with .gdb.zip. Filename passed is {inputGdbFile.FullName}");
            Check.RequireFileExists(inputGdbFile, "Can't find input File GDB for import with ogr2ogr");

            var commandLineArguments = BuildCommandLineArgumentsForFileGdbToGeoJson(inputGdbFile, sourceLayerName, _coordinateSystemId, explodeCollections);
            var processUtilityResult = ExecuteOgr2OgrCommand(commandLineArguments);
            return processUtilityResult.StdOut;
        }

        public void ImportGeoJsonToMsSql(string geoJson, string connectionString, string destinationTableName, string sqlSelectClause, int sourceCrsId, int targetCrsId)
        {
            var databaseConnectionString = $"MSSQL:{connectionString}";
            using (var geoJsonFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".json"))
            {
                File.WriteAllText(geoJsonFile.FileInfo.FullName, geoJson);
                var commandLineArguments = BuildCommandLineArgumentsForGeoJsonToMsSql(geoJsonFile.FileInfo,
                     destinationTableName, databaseConnectionString, sqlSelectClause, sourceCrsId, targetCrsId);
                ExecuteOgr2OgrCommand(commandLineArguments);
            }
        }

        // ReSharper disable once UnusedMember.Global
        public void ImportGeoJsonToEsriShapefile(string geoJson, string outputFilePath, string outputFileName)
        {
            using (var geoJsonFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".json"))
            {
                File.WriteAllText(geoJsonFile.FileInfo.FullName, geoJson);
                var commandLineArguments = BuildCommandLineArgumentsForGeoJsonToEsriShapefile(geoJsonFile.FileInfo, _coordinateSystemId, outputFilePath, outputFileName);
                ExecuteOgr2OgrCommand(commandLineArguments);
            }
        }

        public void ImportGeoJsonToFileGdb(string geoJson, string outputFilePath, string outputLayerName, bool update, bool assignSrs)
        {
            using (var geoJsonFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".json"))
            {
                File.WriteAllText(geoJsonFile.FileInfo.FullName, geoJson);
                var commandLineArguments = BuildCommandLineArgumentsForGeoJsonToFileGdb(geoJsonFile.FileInfo, _coordinateSystemId, outputFilePath, outputLayerName, update, assignSrs);
                ExecuteOgr2OgrCommandForFileGdbWrite(commandLineArguments);
            }
        }

        protected ProcessUtilityResult ExecuteOgr2OgrCommand(List<string> commandLineArguments)
        {
            var environmentVariables = new Dictionary<string, string>{
            {"GDAL_DATA", $"{_gdalDataPath.FullName}"},
            {"GDAL_DRIVER_PATH", $"{_gdalDriversPath.FullName}"},
            {"PROJ_LIB", $"{_gdalProjLibPath.FullName}"},
            };
            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(_ogr2OgrExecutable.DirectoryName, _ogr2OgrExecutable.FullName, commandLineArguments, true, Convert.ToInt32(_totalMilliseconds), environmentVariables);
            if (processUtilityResult.ReturnCode != 0 && processUtilityResult.ReturnCode != -1073741819) // RL 7/22/02 Ignore Warning 1: Ring Self-intersection
            {
                var argumentsAsString = String.Join(" ", commandLineArguments.Select(ProcessUtility.EncodeArgumentForCommandLine).ToList());
                var fullProcessAndArguments =
                    $"{ProcessUtility.EncodeArgumentForCommandLine(_ogr2OgrExecutable.FullName)} {argumentsAsString}";
                var errorMessage =
                    $"Process \"{_ogr2OgrExecutable.Name}\" returned with exit code {processUtilityResult.ReturnCode}, expected exit code 0.\r\n\r\nStdErr and StdOut:\r\n{processUtilityResult.StdOutAndStdErr}\r\n\r\nProcess Command Line:\r\n{fullProcessAndArguments}\r\n\r\nProcess Working Directory: {_ogr2OgrExecutable.DirectoryName}";
                throw new Ogr2OgrCommandLineException(errorMessage);
            }
            return processUtilityResult;
        }

        // The FileGDB driver for Ogr2Ogr prints an empty line to standard error and returns a code even when successful, so we have to trap that case explicitly
        // ReSharper disable once UnusedMethodReturnValue.Local
        private ProcessUtilityResult ExecuteOgr2OgrCommandForFileGdbWrite(List<string> commandLineArguments)
        {
            var processUtilityResult = ExecuteOgr2OgrCommand(commandLineArguments);
            if (processUtilityResult.ReturnCode != 0 && !(processUtilityResult.StdOutAndStdErr.Equals("[stdout] \r\n[stderr] \r\n") || processUtilityResult.StdOutAndStdErr.Equals("[stderr] \r\n[stdout] \r\n")))
            {
                var argumentsAsString = String.Join(" ", commandLineArguments.Select(ProcessUtility.EncodeArgumentForCommandLine).ToList());
                var fullProcessAndArguments =
                    $"{ProcessUtility.EncodeArgumentForCommandLine(_ogr2OgrExecutable.FullName)} {argumentsAsString}";
                var errorMessage =
                    $"Process \"{_ogr2OgrExecutable.Name}\" returned with exit code {processUtilityResult.ReturnCode}, expected exit code 0.\r\n\r\nStdErr and StdOut:\r\n{processUtilityResult.StdOutAndStdErr}\r\n\r\nProcess Command Line:\r\n{fullProcessAndArguments}\r\n\r\nProcess Working Directory: {_ogr2OgrExecutable.DirectoryName}";
                throw new Ogr2OgrCommandLineException(errorMessage);
            }
            return processUtilityResult;
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>"C:\Program Files\GDAL\ogr2ogr.exe" -progress -append --config GDAL_DATA "C:\Program Files\GDAL\gdal-data" -t_srs "EPSG:4326" -f MSSQLSpatial "MSSQL:server=(local);database=Scratch;trusted_connection=yes" "C:\temp\GdalScratch\Sub_Actions_20131219.gdb" "Sub_Actions_Polygon_20131219" -nln MyTable</example>
        /// </summary>
        internal static List<string> BuildCommandLineArgumentsForFileGdbToMsSql(FileInfo inputGdbFile, string databaseConnectionString, string sourceLayerName, string targetTableName, List<string> columnNameList, int coordinateSystemId, bool enforceGeometryType, string geometryTypeToEnforce, string whereClause)
        {
            var reservedFields = new[] { "Ogr_Fid", "Ogr_Geometry" };
            var filteredColumnNameList = columnNameList.Where(x => reservedFields.All(y => !String.Equals(x, y, StringComparison.InvariantCultureIgnoreCase))).ToList();
            const string ogr2OgrColumnListSeparator = ",";
            Check.Require(filteredColumnNameList.All(x => !x.Contains(ogr2OgrColumnListSeparator)),
                $"Found column names with separator character \"{ogr2OgrColumnListSeparator}\", can't continue. Columns:{String.Join("\r\n", filteredColumnNameList)}");

            var selectStatement =
                $"select {String.Join(ogr2OgrColumnListSeparator + " ", filteredColumnNameList)} from {sourceLayerName} {whereClause}";
            var commandLineArguments = new List<string>
            {
                "-append",
                "-sql",
                selectStatement,
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                "-f",
                "MSSQLSpatial",
                databaseConnectionString,
                inputGdbFile.FullName,
                "-nln",
                targetTableName,
                enforceGeometryType ? "-nlt" : null,
                enforceGeometryType ? geometryTypeToEnforce : null
            };
            
            return commandLineArguments.Where(x=> x != null).ToList();
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>"C:\Program Files\GDAL\ogr2ogr.exe" -progress -append --config GDAL_DATA "C:\Program Files\GDAL\gdal-data" -t_srs "EPSG:4326" -f MSSQLSpatial "MSSQL:server=(local);database=Scratch;trusted_connection=yes" "C:\temp\GdalScratch\Sub_Actions_20131219.gdb" "Sub_Actions_Polygon_20131219" -nln MyTable</example>
        /// </summary>
        internal static List<string> BuildCommandLineArgumentsForArgGisQueryToMsSql(string arcGisQuery, string databaseConnectionString, string targetTableName, string sourceColumnName, string destinationColumnName, int coordinateSystemId)
        {
            var commandLineArguments = new List<string>
            {
                "-append",
                "-sql",
                $"SELECT {sourceColumnName} AS {destinationColumnName} FROM {OgrGeoJsonTableName}",
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                "-f",
                "MSSQLSpatial",
                databaseConnectionString,
                $"\"{arcGisQuery}\"",
                "-nln",
                targetTableName
            };

            return commandLineArguments;
        }

        internal static List<string> BuildCommandLineArgumentsForGeoJsonToMsSql(FileInfo sourceGeoJsonFile, string destinationTableName, string databaseConnectionString, string sqlSelectClause, int sourceCrsId, int targetCrsId )
        {
            //c:\SVN\sitkatech\trunk\Corral\Build>"C:\Program Files\GDAL\ogr2ogr.exe" -preserve_fid -t_srs EPSG:4326 -f MSSQLSpatial "MSSQL:server=localhost;database=tempdb;trusted_connection=yes" "C:\temp\geojson.json" -nln "TestTable"            

            var commandLineArguments = new List<string>
            {
                "-append",
                "-sql",
                $"SELECT {sqlSelectClause} FROM {OgrGeoJsonTableName}",
                "-s_srs",
                GetMapProjection(sourceCrsId),
                "-t_srs",
                GetMapProjection(targetCrsId),
                "-f",
                "MSSQLSpatial",
                databaseConnectionString,
                sourceGeoJsonFile.FullName,
                "-nln",
                destinationTableName
            };

            return commandLineArguments;
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>"C:\Program Files\GDAL\ogr2ogr.exe" -preserve_fid -t_srs EPSG:4326 -f GeoJSON /dev/stdout "C:\\svn\\sitkatech\\trunk\\Corral\\Source\\Neptune.Web\\Models\\GdalOgr\\SampleFileGeodatabase.gdb.zip" "somelayername"</example>
        /// </summary>
        internal static List<string> BuildCommandLineArgumentsForFileGdbToGeoJson(FileInfo inputGdbFile, string sourceLayerName, int coordinateSystemId, bool explodeCollections)
        {
            var commandLineArguments = new List<string>
            {
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                explodeCollections ? "-explodecollections" : null,
                "-f",
                "GeoJSON",
                "/dev/stdout",
                inputGdbFile.FullName,
                $"\"{sourceLayerName}\"",
                "-dim",
                "2"
            };

            return commandLineArguments.Where(x => x != null).ToList();
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>
        /// "C:\Program Files\GDAL\ogr2ogr.exe" -preserve_fid -t_srs EPSG:4326 -f "ESRI Shapefile" "c:\temp\gestalten" "C:\temp\geoJay" -nln gestalten
        /// </example>
        /// </summary>
        private List<string> BuildCommandLineArgumentsForGeoJsonToEsriShapefile(FileInfo sourceGeoJsonFile, int coordinateSystemId, string outputPath, string outputName)
        {
            var commandLineArguments = new List<string>
            {
                "-preserve_fid",
                "-t_srs",
                GetMapProjection(coordinateSystemId),
                "-f",
                "ESRI Shapefile",
                outputPath,
                sourceGeoJsonFile.FullName,
                "-nln",
                outputName
            };

            return commandLineArguments;
        }

        /// <summary>
        /// Produces the command line arguments for ogr2ogr.exe to run the File Geodatabase import.
        /// <example>
        /// "C:\Program Files\GDAL\ogr2ogr.exe" -preserve_fid -t_srs EPSG:4326 -f FileGDB "C:\temp\gestalten" "C:\temp\geoJay.txt" -nln gestalten
        /// </example>
        /// </summary>
        private List<string> BuildCommandLineArgumentsForGeoJsonToFileGdb(FileInfo sourceGeoJsonFile, int coordinateSystemId, string outputPath, string outputLayerName,
            bool update, bool assignSrs)
        {
            var commandLineArguments = new List<string>
            {
                update ? "-update" : null,
                assignSrs ? "-a_srs" : "-t_srs",
                GetMapProjection(coordinateSystemId),
                "-f",
                "FileGDB",
                $"{outputPath}.gdb",
                sourceGeoJsonFile.FullName,
                "-nln",
                outputLayerName
            };

            return commandLineArguments.Where(x => x != null).ToList();
        }

        public static string GetMapProjection(int coordinateSystemId)
        {
            return $"EPSG:{coordinateSystemId}";
        }

        public static string SanitizeStringForGdb(string str)
        {
            var arr = str.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray();
            return new string(arr).Replace(" ", "_");
        }
    }
}
