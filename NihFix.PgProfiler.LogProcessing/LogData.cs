using System;

namespace NihFix.PgProfiler.LogProcessing
{
    public class LogData
    {
        public DateTime DateTime { get; set; }
        public string SqlText { get; set; }
        public string QueryParams { get; set; }
        public double DurationMs { get; set; }
        public string DataBaseName { get; set; }
    }
}