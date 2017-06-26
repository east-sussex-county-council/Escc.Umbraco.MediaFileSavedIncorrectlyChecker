using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker
{
    public class FilesInWrongFolderDetector : IProblemDetector
    {
        public bool IsThereAProblem(IEnumerable<string> files)
        {
            return (files.Count() > 1);
        }

    }
}