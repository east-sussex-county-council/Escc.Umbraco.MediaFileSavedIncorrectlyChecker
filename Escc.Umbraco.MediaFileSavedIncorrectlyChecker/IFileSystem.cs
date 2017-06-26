using System.Collections.Generic;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker
{
    internal interface IFileSystem
    {
        IEnumerable<Folder> GetFolders();
        IEnumerable<string> GetFiles(Folder folder);
    }
}