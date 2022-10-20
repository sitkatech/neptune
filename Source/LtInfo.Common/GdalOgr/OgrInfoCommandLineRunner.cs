/*-----------------------------------------------------------------------
<copyright file="OgrInfoCommandLineRunner.cs" company="Sitka Technology Group">
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
    public static class OgrInfoCommandLineRunner
    {
        private static ProcessUtilityResult ExecuteOgrInfoCommand(FileInfo ogrInfoFileInfo, double totalMilliseconds, List<string> commandLineArguments)
        {
            var gdalDataPath = new DirectoryInfo(Path.Combine(ogrInfoFileInfo.DirectoryName, "gdal-data"));
            Check.RequireDirectoryExists(gdalDataPath.FullName, "Can't find gdal-data directory needed for import with ogr2ogr");
            var gdalProjLibPath = new DirectoryInfo(Path.Combine(ogrInfoFileInfo.DirectoryName, "projlib"));
            Check.RequireDirectoryExists(gdalProjLibPath.FullName, "Can't find projlib directory needed for import with ogr2ogr");
            var gdalDriversPath = new DirectoryInfo(Path.Combine(ogrInfoFileInfo.DirectoryName, "gdalplugins"));
            Check.RequireDirectoryExists(gdalDriversPath.FullName, "Can't find gdalplugins directory needed for import with ogr2ogr");
            var environmentVariables = new Dictionary<string, string>{
                {"GDAL_DATA", $"{gdalDataPath.FullName}"},
                {"GDAL_DRIVER_PATH", $"{gdalDriversPath.FullName}"},
                {"PROJ_LIB", $"{gdalProjLibPath.FullName}"},
            };
            var processUtilityResult = ProcessUtility.ShellAndWaitImpl(ogrInfoFileInfo.DirectoryName, ogrInfoFileInfo.FullName, commandLineArguments, true, Convert.ToInt32(totalMilliseconds), environmentVariables);
            return processUtilityResult;
        }

        public static List<string> GetFeatureClassNamesFromFileGdb(FileInfo ogrInfoFileInfo, FileInfo gdbFileInfo, double totalMilliseconds)
        {

            var commandLineArguments = BuildOgrInfoCommandLineArgumentsToListFeatureClasses(gdbFileInfo);
            var processUtilityResult = ExecuteOgrInfoCommand(ogrInfoFileInfo, totalMilliseconds, commandLineArguments);
            var featureClassesFromFileGdb = processUtilityResult.StdOut.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return featureClassesFromFileGdb.Select(x => x.Split(' ').Skip(1).First()).ToList();
        }

        public static bool ConfirmAttributeExistsOnFeatureClass(FileInfo ogrInfoFileInfo, FileInfo gdbFileInfo, double totalMilliseconds, string featureClassName, string attributeName)
        {
            var commandLineArguments = BuildOgrInfoCommandLineArgumentsToConfirmAttributeExistsOnFeatureClass(gdbFileInfo, featureClassName);
            var processUtilityResult = ExecuteOgrInfoCommand(ogrInfoFileInfo, totalMilliseconds, commandLineArguments);
            return processUtilityResult.StdOut.Contains($"{attributeName}:", StringComparison.InvariantCultureIgnoreCase);
        }

        public static Tuple<double, double, double, double> GetExtentFromGeoJson(FileInfo ogrInfoFileInfo, string geoJson, double totalMilliseconds)
        {
            using (var geoJsonFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".json"))
            {
                File.WriteAllText(geoJsonFile.FileInfo.FullName, geoJson);

                var commandLineArguments = BuildOgrInfoCommandLineArgumentsGetExtent(geoJsonFile.FileInfo);
                var processUtilityResult = ExecuteOgrInfoCommand(ogrInfoFileInfo, totalMilliseconds, commandLineArguments);
                var lines = processUtilityResult.StdOut.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (lines.Any(x => x.Contains("Feature Count: 0")))
                {
                    return null;
                }

                var extentTokens = lines.First(x => x.StartsWith("Extent:")).Split(new[] {' ', '(', ')', ','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                return new Tuple<double, double, double, double>(double.Parse(extentTokens[1]), double.Parse(extentTokens[2]), double.Parse(extentTokens[4]), double.Parse(extentTokens[5]));
            }
        }

        public static List<string> BuildOgrInfoCommandLineArgumentsToListFeatureClasses(FileInfo inputGdbFile)
        {
            var commandLineArguments =  new List<string>
            {
                "-ro",
                "-so",
                "-q",
                inputGdbFile.FullName
            };

            return commandLineArguments;
        }

        public static List<string> BuildOgrInfoCommandLineArgumentsToConfirmAttributeExistsOnFeatureClass(FileInfo inputGdbFile, string featureClassName)
        {
            var commandLineArguments =  new List<string>
            {
                "-ro",
                "-so",
                inputGdbFile.FullName,
                featureClassName
            };

            return commandLineArguments;
        }
        public static List<string> BuildOgrInfoCommandLineArgumentsGetExtent(FileInfo inputGdbFile)
        {
            var commandLineArguments =  new List<string>
            {
                "-ro",
                "-al",
                "-so",
                "-geom=NO",
                inputGdbFile.FullName
            };

            return commandLineArguments;
        }
    }
}
