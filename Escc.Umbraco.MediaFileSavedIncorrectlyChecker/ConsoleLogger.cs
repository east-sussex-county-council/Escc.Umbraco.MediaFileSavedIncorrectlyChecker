using System;
using System.Collections.Generic;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker
{
    internal class ConsoleLogger : ILogger
    {
        public void Log(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }
    }
}