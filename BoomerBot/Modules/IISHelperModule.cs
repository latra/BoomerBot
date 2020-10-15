using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BoomerBot.Modulos
{
    internal static class IISHelperModule
    {
        public static string GetAppPath()
        {
            return IISHelperModule.GetContentRoot() ?? Directory.GetCurrentDirectory();
        }
        public static string GetContentRoot()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && NativeMethods.IsAspNetCoreModuleLoaded())
            {
                var iisConfigData = NativeMethods.HttpGetApplicationProperties();
                var contentRoot = iisConfigData.pwzFullApplicationPath.TrimEnd(Path.DirectorySeparatorChar);

                return contentRoot;
            }

            return null;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IISConfigurationData
        {
            public IntPtr pNativeApplication;
            [MarshalAs(UnmanagedType.BStr)]
            public string pwzFullApplicationPath;
            [MarshalAs(UnmanagedType.BStr)]
            public string pwzVirtualApplicationPath;
            public bool fWindowsAuthEnabled;
            public bool fBasicAuthEnabled;
            public bool fAnonymousAuthEnable;
        }

        private static class NativeMethods
        {
            public static bool IsAspNetCoreModuleLoaded()
            {
                return GetModuleHandle("aspnetcorev2_inprocess.dll") != IntPtr.Zero;
            }

            public static IISConfigurationData HttpGetApplicationProperties()
            {
                var iisConfigurationData = default(IISConfigurationData);
                var errorCode = http_get_application_properties(ref iisConfigurationData);

                if (errorCode != 0)
                {
                    throw Marshal.GetExceptionForHR(errorCode);
                }

                return iisConfigurationData;
            }

            [DllImport("kernel32.dll")]
            private static extern IntPtr GetModuleHandle(string lpModuleName);

            [DllImport("aspnetcorev2_inprocess.dll")]
            private static extern int http_get_application_properties(ref IISConfigurationData iiConfigData);
        }
    }
}
