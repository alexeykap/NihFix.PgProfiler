using System;

namespace NihFix.PgProfiler.LogProcessing.SecondLevelInterpreter
{
    public class LogData
    {
        public DateTime DateTime { get; set; }
        public string SqlText { get; set; }
        public string QueryParams { get; set; }
        
        public double ParseDurationMs { get; set; }

        public double BindDurationMs { get; set; }

        public double ExecuteDurationMs { get; set; }

        public string DataBaseName { get; set; }
    }
}