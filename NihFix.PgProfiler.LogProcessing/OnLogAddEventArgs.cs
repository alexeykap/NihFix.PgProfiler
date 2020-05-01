using System;
using System.Collections.Generic;

namespace NihFix.PgProfiler.LogProcessing
{
    /// <summary>
    /// Event args for LogReader.OnLogChange event.
    /// </summary>
    public class OnLogAddEventArgs:EventArgs
    {
        /// <summary>
        /// New data.
        /// </summary>
        public IEnumerable<PostgresLogRecord> NewData { get; }
        
        /// <summary>
        /// Path to log file.
        /// </summary>
        public string FilePath { get; }
        

        public OnLogAddEventArgs()
        {
            
        }

        /// <summary>
        /// Ctr.
        /// </summary>
        /// <param name="newData">New log data.</param>
        /// <param name="filePath">Path to log file.</param>
        public OnLogAddEventArgs(IEnumerable<PostgresLogRecord> newData, string filePath)
        {
            NewData = newData;
            FilePath = filePath;
        }
    }
}