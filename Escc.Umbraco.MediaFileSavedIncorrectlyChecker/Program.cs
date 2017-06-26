using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            IProblemDetector detector = new FilesInWrongFolderDetector();
            IFileListFilter filter = new UmbracoThumbnailFilter();
            var loggers = new ILogger[] { new ConsoleLogger(),
                new EmailLogger(new Uri(ConfigurationManager.AppSettings["AzureStorageBaseUrl"]), 
                                        ConfigurationManager.AppSettings["EmailFrom"],
                                        ConfigurationManager.AppSettings["EmailTo"]
                                        ) };

            IFileSystem fileSystem = new AzureBlobStorageFileSystem(ConfigurationManager.ConnectionStrings["Escc.Umbraco.MediaFileSavedIncorrectlyChecker.MediaBlobStorage"].ConnectionString);
            var folders = fileSystem.GetFolders();
            foreach (var folder in folders)
            {
                var files = fileSystem.GetFiles(folder);
                files = filter.Filter(files);

                if (detector.IsThereAProblem(files))
                {
                    foreach (var logger in loggers)
                    {
                        logger.Log(files);
                    }
                }
            }
        }
    }
}
