using System;
using System.Collections.Generic;
using System.IO;

namespace NihFix.PgProfiler.LogProcessing
{
    /// <summary>
    /// Inform about new record in log;
    /// </summary>
    public class LogChangeTracker : IDisposable
    {
        private string _logFolderPath;
        private readonly FileSystemWatcher _fileSystemWatcher;
        private readonly Dictionary<string, long> _streamPositionDictionary = new Dictionary<string, long>();
        
        /// <summary>
        /// Occurs when new data adds to log.
        /// </summary>
        public event EventHandler<OnLogAddEventArgs> OnLogChange;

        /// <summary>
        /// Ctr.
        /// </summary>
        /// <param name="logFolderPath">Path to folder with log files.</param>
        public LogChangeTracker(string logFolderPath)
        {
            _logFolderPath = logFolderPath;
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.Path = logFolderPath;
            _fileSystemWatcher.Filter = "*.log";
            _fileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime |
                                              NotifyFilters.FileName |
                                              NotifyFilters.LastWrite;
            _fileSystemWatcher.Changed += FileSystemWatcherOnChanged;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void FileSystemWatcherOnChanged(object sender, FileSystemEventArgs e)
        {
            using var fileStream = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fileStream.Position = GetCurrentStreamPosition(e.FullPath);
            using var streamReader = new StreamReader(fileStream);
            OnLogChange?.Invoke(this, new OnLogAddEventArgs(streamReader.ReadToEnd(), e.FullPath));
            SaveCurrentStreamPosition(e.FullPath,fileStream.Position);
        }

        private long GetCurrentStreamPosition(string filePath)
        {
            if (_streamPositionDictionary.TryGetValue(filePath, out var position))
            {
                return position;
            }

            _streamPositionDictionary.Add(filePath, 0);
            return 0;
        }

        private void SaveCurrentStreamPosition(string filePath, long position)
        {
            _streamPositionDictionary[filePath]=position;
        }


        ///<inheritdoc/>
        public void Dispose()
        {
            _fileSystemWatcher?.Dispose();
        }
    }
}