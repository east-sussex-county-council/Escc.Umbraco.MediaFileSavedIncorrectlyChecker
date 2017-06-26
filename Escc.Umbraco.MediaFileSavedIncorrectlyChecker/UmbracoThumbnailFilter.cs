using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker
{
    public class UmbracoThumbnailFilter : IFileListFilter
    {
        public IEnumerable<string> Filter(IEnumerable<string> files)
        {
            var excludingThumbnails = new List<string>(files);
            foreach (var file in files)
            {
                var expectedThumbnails = new List<string>();
                var extension = Path.GetExtension(file).ToLowerInvariant();
                if (extension == ".jpg" || extension == ".gif" || extension == ".png")
                {
                    expectedThumbnails.Add(file.Substring(0, file.Length - extension.Length) + "_thumb.jpg");
                    expectedThumbnails.Add(file.Substring(0, file.Length - extension.Length) + "_big-thumb.jpg");
                }
                if (extension == ".gif" || extension == ".png")
                {
                    expectedThumbnails.Add(file.Substring(0, file.Length - extension.Length) + "_thumb" + extension);
                    expectedThumbnails.Add(file.Substring(0, file.Length - extension.Length) + "_big-thumb" + extension);
                }

                foreach (var expectedThumbnail in expectedThumbnails)
                {
                    if (excludingThumbnails.Contains(expectedThumbnail))
                    {
                        excludingThumbnails.Remove(expectedThumbnail);
                    }
                }
            }
            return excludingThumbnails;
        }
    }
}
