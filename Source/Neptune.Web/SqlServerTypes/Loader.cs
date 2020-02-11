using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Neptune.Web.SqlServerTypes
{
    /// <summary>
    /// Utility methods related to CLR Types for SQL Server 
    /// </summary>
    public class Utilities
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string libname);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);

        private static IntPtr _msvcrPtr = IntPtr.Zero;
        private static IntPtr _spatialPtr = IntPtr.Zero;

        /// <summary>
        /// Loads the required native assemblies for the current architecture (x86 or x64)
        /// </summary>
        /// <param name="rootApplicationPath">
        /// Root path of the current application. Use Server.MapPath(".") for ASP.NET applications
        /// and AppDomain.CurrentDomain.BaseDirectory for desktop applications.
        /// </param>
        public static void LoadNativeAssemblies(string rootApplicationPath)
        {
            if (_msvcrPtr != IntPtr.Zero || _spatialPtr != IntPtr.Zero)
                throw new Exception("LoadNativeAssemblies already called.");

            var nativeBinaryPath = IntPtr.Size > 4
                ? Path.Combine(rootApplicationPath, @"SqlServerTypes\x64\")
                : Path.Combine(rootApplicationPath, @"SqlServerTypes\x86\");

            _msvcrPtr = LoadNativeAssembly(nativeBinaryPath, "msvcr120.dll");
            _spatialPtr = LoadNativeAssembly(nativeBinaryPath, "SqlServerSpatial140.dll");

            AppDomain.CurrentDomain.DomainUnload += (sender, e) =>
            {
                if (_msvcrPtr != IntPtr.Zero)
                {
                    FreeLibrary(_msvcrPtr);
                    _msvcrPtr = IntPtr.Zero;
                }

                if (_spatialPtr != IntPtr.Zero)
                {
                    FreeLibrary(_spatialPtr);
                    _spatialPtr = IntPtr.Zero;
                }
            };
        }

        private static IntPtr LoadNativeAssembly(string nativeBinaryPath, string assemblyName)
        {
            var path = Path.Combine(nativeBinaryPath, assemblyName);
            var ptr = LoadLibrary(path);
            if (ptr == IntPtr.Zero)
            {
                throw new Exception(string.Format(
                    "Error loading {0} (ErrorCode: {1})",
                    assemblyName,
                    Marshal.GetLastWin32Error()));
            }

            return ptr;
        }
    }
}