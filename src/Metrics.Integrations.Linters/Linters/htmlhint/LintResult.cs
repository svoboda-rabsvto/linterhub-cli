namespace Metrics.Integrations.Linters.htmlhint
{
    using System.Collections.Generic;

    public class LintResult : ILinterResult
    {
        public List<File> FilesList {get; set;} 
    }
}