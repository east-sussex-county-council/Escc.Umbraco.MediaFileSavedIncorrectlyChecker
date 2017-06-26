using System.Collections.Generic;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker
{
    internal interface ILogger
    {
        void Log(IEnumerable<string> files);
    }
}