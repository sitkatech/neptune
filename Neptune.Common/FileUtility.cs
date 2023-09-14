﻿/*-----------------------------------------------------------------------
<copyright file="FileUtility.cs" company="Sitka Technology Group">
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

using System.Text.RegularExpressions;

namespace Neptune.Common
{
    public class FileUtility
    {
        public static string FileToString(FileInfo fileToRead)
        {
            return File.ReadAllText(fileToRead.FullName);
        }

        public static byte[] FileToBytes(FileInfo fileToRead)
        {
            return File.ReadAllBytes(fileToRead.FullName);
        }

        public static void StringToFile(string stuffToWrite, FileInfo fileToWriteTo)
        {
            File.WriteAllText(fileToWriteTo.FullName, stuffToWrite);
        }

        /// <summary>
        /// Looks for a directory in <see cref="Environment.CurrentDirectory"/> and upwards until the <paramref name="directoryFullNameRelativePath"/> is found
        /// </summary>
        /// <param name="directoryFullNameRelativePath">Directory name and (optionally) relative path to look for</param>
        /// <returns>Matching directory or throws</returns>
        /// <exception cref="DirectoryNotFoundException">Throws this if it can't find the directory</exception>
        public static DirectoryInfo FirstMatchingDirectoryUpDirectoryTree(string directoryFullNameRelativePath)
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            return FirstMatchingDirectoryUpDirectoryTree(currentDirectory, directoryFullNameRelativePath);
        }

        /// <summary>
        /// Looks for a directory in <paramref name="startingDir"/>and upwards until the <paramref name="directoryFullNameRelativePath"/> is found
        /// </summary>
        /// <param name="startingDir">Directory to start search in</param>
        /// <param name="directoryFullNameRelativePath"></param>
        /// <returns>Matching directory or throws</returns>
        /// <exception cref="DirectoryNotFoundException">Throws this if it can't find the directory</exception>
        public static DirectoryInfo FirstMatchingDirectoryUpDirectoryTree(DirectoryInfo startingDir, string directoryFullNameRelativePath)
        {
            var regex = $"^{Regex.Escape(Path.DirectorySeparatorChar.ToString())}";
            var directoryFullNameRelativePathNoStartingBackslash = Regex.Replace(directoryFullNameRelativePath, regex, "");
            var currentDirectory = startingDir;

            while (true)
            {
                var potentialDirectory = new DirectoryInfo(Path.Combine(currentDirectory.FullName, directoryFullNameRelativePathNoStartingBackslash));
                if (potentialDirectory.Exists)
                {
                    return potentialDirectory;
                }
                currentDirectory = currentDirectory.Parent;
                if (currentDirectory == null)
                {
                    throw new DirectoryNotFoundException(
                        $"Searched directory \"{startingDir.FullName}\" and upwards and could not find directory \"{directoryFullNameRelativePath}\".");
                }
            }
        }

        /// <summary>
        /// Looks for a file in <see cref="Environment.CurrentDirectory"/> and upwards until the <paramref name="fileFullNameRelativePath"/> is found
        /// </summary>
        /// <param name="fileFullNameRelativePath">File name and (optionally) relative path to look for</param>
        /// <returns>Matching file or throws</returns>
        /// <exception cref="FileNotFoundException">Throws this if it can't find the file</exception>
        public static FileInfo FirstMatchingFileUpDirectoryTree(string fileFullNameRelativePath)
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            return FirstMatchingFileUpDirectoryTree(currentDirectory, fileFullNameRelativePath);
        }

        /// <summary>
        /// Looks for a file in <paramref name="startingDir"/>and upwards until the <paramref name="fileFullNameRelativePath"/> is found
        /// </summary>
        /// <param name="startingDir">Directory to start search in</param>
        /// <param name="fileFullNameRelativePath">File name and (optionally) relative path to look for</param>
        /// <returns>Matching file or throws</returns>
        /// <exception cref="FileNotFoundException">Throws this if it can't find the file</exception>
        public static FileInfo FirstMatchingFileUpDirectoryTree(DirectoryInfo startingDir, string fileFullNameRelativePath)
        {
            var currentDirectory = startingDir;

            while (true)
            {
                var potentialFile = new FileInfo(Path.Combine(currentDirectory.FullName, fileFullNameRelativePath));
                if (potentialFile.Exists)
                {
                    return potentialFile;
                }
                currentDirectory = currentDirectory.Parent;
                if (currentDirectory == null)
                {
                    throw new FileNotFoundException(
                        $"Searched directory \"{startingDir.FullName}\" and upwards and could not find file \"{fileFullNameRelativePath}\".");
                }
            }
        }

        /// <summary>
        /// returns a human-readable representation of file-size
        /// </summary>
        /// <returns></returns>
        private static readonly string[] Orders = { "EB", "PB", "TB", "GB", "MB", "KB", "Bytes" };
        public static string FormatBytes(long bytes)
        {
            const long scale = 1024;

            var max = (long)Math.Pow(scale, (Orders.Length - 1));
            foreach (var order in Orders)
            {
                if (bytes > max)
                    return $"{Decimal.Divide(bytes, max):##.#} {order}";
                max /= scale;
            }
            return "0 Bytes";
        }

        public static void CreateDirectoryIfNeeded(DirectoryInfo directoryInfo)
        {
            if (!Directory.Exists(directoryInfo.FullName))
            {
               Directory.CreateDirectory(directoryInfo.FullName);
            }
            directoryInfo.Refresh();
        }

        public static string CreateTempDirectory()
        {
            return CreateTempDirectory(Path.GetTempPath());
        }

        public static string CreateTempDirectory(string pathPrefix)
        {
            var path = Path.Combine(pathPrefix, Path.GetRandomFileName());
            if (Directory.Exists(pathPrefix) && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return path;
            }

            throw new Exception($"unable to create temporary directory \"{path}\"");
        }

        public static List<string> CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            return CopyAll(new DirectoryInfo(sourceDirectory), new DirectoryInfo(targetDirectory));
        }

        private static List<string> CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            var result = new List<string>();

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into its new directory.
            foreach (var fi in source.GetFiles())
            {
                result.Add(fi.FullName);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each sub directory using recursion.
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                result.AddRange(CopyAll(diSourceSubDir, nextTargetSubDir));
            }

            return result;
        }

        public static string ExtensionFor(string file)
        {
            var idx = file.LastIndexOf('.');
            return idx < 0 ? "" : file.Substring(idx);
        }

        public static bool IsHiddenOrSystemFile(FileInfo fileInfo)
        {
            return (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden
                || (fileInfo.Attributes & FileAttributes.System) == FileAttributes.System;
        }

        public static string GetCleanFileName(string fileName, string replacementString = "")
        {
            return Path.GetInvalidFileNameChars().Select(x => x.ToString()).Aggregate(fileName, (current, c) => current.Replace(c, replacementString));
        }

        public static string GetCleanFolderName(string folderName, string replacementString = "")
        {
            return Path.GetInvalidPathChars().Select(x => x.ToString()).Aggregate(folderName, (current, c) => current.Replace(c, replacementString));
        }
    }
}
