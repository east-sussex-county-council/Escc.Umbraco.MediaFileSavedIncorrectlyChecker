using System.Collections.Generic;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker
{
    internal interface IFileListFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> files);
    }
}