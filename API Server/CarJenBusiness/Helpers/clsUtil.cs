using CarJenBusiness.Inspections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarJenBusiness.Helpers
{
    public class clsUtil
    {
        private static bool _CreateFolderIfNotExists(string DirectoryPath)
        {
            if (!Directory.Exists(DirectoryPath))
            {
                try
                {
                    // It doesn't exist, so create the folder
                    Directory.CreateDirectory(DirectoryPath);
                    return true;
                }
                catch (Exception ex)
                {
                    //string methodName = MethodBase.GetCurrentMethod().Name;
                    //clsEventLogger.LogError(ex.Message, methodName);
                    return false;
                }

            }
            return true;
        }
        private static string _GenerateGUID()
        {
            // Generate a new GUID
            Guid newGuid = Guid.NewGuid();

            // convert the GUID to a string
            return newGuid.ToString();
        }
        private static string _ReplaceFileNameWithGuid(string SourceFile)
        {
            // Get the file extension
            FileInfo fileInfo = new FileInfo(SourceFile);
            string extension = fileInfo.Extension;

            // Generate a new GUID and append the file extension
            return _GenerateGUID() + extension;
        }
        public static bool CopyImageToProjectImagesFolder(ref string SourceFile)
        {

            // this funciton will copy the image to the
            // project images foldr after renaming it
            // with GUID with the same extention, then it
            // will update the sourceFileName with the new name.


            string DestinationFolder = @"C:\CarJen_People_Images\";

            // Check if the folder exists
            if (!_CreateFolderIfNotExists(DestinationFolder))
            {
                return false;
            }

            // Generate a new name for the file copy
            string FileName = _ReplaceFileNameWithGuid(SourceFile);


            // Prepare Destination File for the new file name
            string DestinationFile = DestinationFolder + FileName;

            try
            {
                // Copy fileImage to the new folder with new name

                File.Copy(SourceFile, DestinationFile, true);

            }
            catch (IOException iox)
            {
                //string methodName = MethodBase.GetCurrentMethod().Name;
                //clsEventLogger.LogError(iox.Message, methodName);
                return false;
            }

            SourceFile = DestinationFile;
            return true;
        }
        public static bool CopyImageToFolder(ref string SourceFile, string DestinationFolder)
        {

            // this funciton will copy the image to the
            // project images foldr after renaming it
            // with GUID with the same extention, then it
            // will update the sourceFileName with the new name.


            //string DestinationFolder = @"C:\CarJen_People_Images\";

            // Check if the folder exists
            if (!_CreateFolderIfNotExists(DestinationFolder))
            {
                return false;
            }

            // Generate a new name for the file copy
            string FileName = _ReplaceFileNameWithGuid(SourceFile);


            // Prepare Destination File for the new file name
            string DestinationFile = DestinationFolder + FileName;

            try
            {
                // Copy fileImage to the new folder with new name

                File.Copy(SourceFile, DestinationFile, true);

            }
            catch (IOException iox)
            {
                //string methodName = MethodBase.GetCurrentMethod().Name;
                //clsEventLogger.LogError(iox.Message, methodName);
                return false;
            }

            SourceFile = DestinationFile;
            return true;
        }

        public static void LogError(string Message, string MethodName)
        {
            //clsEventLogger.LogError(Message, MethodName);
        }
        public static void Serialize(Dictionary<string, clsInspectResult> inspectionsList)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(inspectionsList, options);
            File.WriteAllText("TechnicalInspections.json", json);
        }

        /// <summary>
        /// Checks for any uncompleted inspections.
        /// </summary>
        /// <param name="CarInspectionID">Reference parameter to store the found uncompleted inspection ID.</param>
        /// <returns>A dictionary of uncompleted inspections.</returns>

        public static Dictionary<string, clsInspectResult> Deserialize(ref int? carInspectionID)
        {
            try
            {
                var json = File.ReadAllText("TechnicalInspections.json");
                var recoveredList = JsonSerializer.Deserialize<Dictionary<string, clsInspectResult>>(json);

                if (recoveredList != null && recoveredList.Count > 0)
                {
                    var firstResult = recoveredList.Values.FirstOrDefault();
                    if (firstResult?.CarInspectionID.HasValue == true)
                    {
                        carInspectionID = firstResult.CarInspectionID.Value;
                    }

                    return recoveredList;
                }

                carInspectionID = null;
                return null;
            }
            catch
            {
                carInspectionID = null;
                return null;
            }
        }

    }

}
