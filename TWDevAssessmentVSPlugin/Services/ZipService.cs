using System;
using System.IO;
using Ionic.Zip;

namespace TWDevAssessmentVSPlugin.Services
{
    public class ZipService
    {
        public string ZipDirectory(string solutionName, string destinationFileName)
        {
            var dirName = Directory.GetParent(solutionName).FullName;
            var destinationPath = LocalAppDataPath();
            Directory.CreateDirectory(destinationPath);
            var destinationFilePath = Path.Combine(destinationPath, destinationFileName);
            using (var zipFile = new ZipFile())
            {
                zipFile.AddDirectory(dirName);
                zipFile.Save(destinationFilePath);
            }
            return destinationFilePath;
        }

		public void DeleteExistingZips()
		{
			Directory.Delete(LocalAppDataPath(),true);
		}

	    private static string LocalAppDataPath()
	    {
		    return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TW","zip");
	    }
    }
}
