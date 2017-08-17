// ***********************************************************************
// Assembly         : RRD.FSG.RP.Utilities
// Author           : 
// Created          : 11-12-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************
using Ionic.Zip;

namespace RRD.FSG.RP.Utilities
{
    /// <summary>
    /// Class ZipHelper.
    /// </summary>
    public static class ZipHelper
    {
        /// <summary>
        /// Creates a zip file
        /// </summary>
        /// <param name="zipFileFullPath">zipFileFullPath with file name</param>
        /// <param name="directoryFullPath">Full path of directory to be zipped.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
       
        public static bool CreateZipFile(string zipFileFullPath, string directoryFullPath)
        {
            bool isSuccess = false;
            try
            {
                ZipFile zip = new ZipFile(zipFileFullPath);
                zip.AddDirectory(directoryFullPath);                               
                zip.Save();

                isSuccess = true;
            }
            catch
            {
                
            }
            return isSuccess;
        }
    }
}
