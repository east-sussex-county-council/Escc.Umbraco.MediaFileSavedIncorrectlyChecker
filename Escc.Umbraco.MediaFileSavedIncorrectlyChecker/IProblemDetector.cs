using System.Collections.Generic;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker
{
    internal interface IProblemDetector
    {
        bool IsThereAProblem(IEnumerable<string> files);
    }
}