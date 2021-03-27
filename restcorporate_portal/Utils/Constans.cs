using System;
using System.Runtime.InteropServices;

namespace restcorporate_portal.Utils
{
    public static class Constans
    {
        public const string FileDownloadPart = @"/api/files/download?filename=";
        //public const string ApiUrl = "http://localhost:5001";
        public static string OSFilesPath(string rootPath) => rootPath +
                (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ?
                "/Upload/" :
                "\\Upload\\");
        public const string ApiUrl = "https://localhost:44378";
    }
}
