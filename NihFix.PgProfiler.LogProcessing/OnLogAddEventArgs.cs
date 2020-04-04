using System;

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
        public string NewData { get; }
        
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
        public OnLogAddEventArgs(string newData, string filePath)
        {
            NewData = newData;
            FilePath = filePath;
        }
    }
}