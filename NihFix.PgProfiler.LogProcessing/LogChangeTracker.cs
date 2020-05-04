using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using NihFix.PgProfiler.LogProcessing.SecondLevelInterpreter;

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

        private CancellationTokenSource _scanTaskCancellationTokenSource = new CancellationTokenSource();

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
            _fileSystemWatcher.Filter = "*.csv";
            _fileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime |
                                              NotifyFilters.FileName |
                                              NotifyFilters.LastWrite |
                                              NotifyFilters.LastAccess |
                                              NotifyFilters.Size |
                                              NotifyFilters.Attributes |
                                              NotifyFilters.Security |
                                              NotifyFilters.DirectoryName;
            _fileSystemWatcher.Changed += FileSystemWatcherOnChanged;
            _fileSystemWatcher.EnableRaisingEvents = true;
            Task.Run(() => Scan(_scanTaskCancellationTokenSource.Token), _scanTaskCancellationTokenSource.Token);
        }

        private void FileSystemWatcherOnChanged(object sender, FileSystemEventArgs e)
        {
            using var fileStream = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fileStream.Position = GetCurrentStreamPosition(e.FullPath);
            using var streamReader = new StreamReader(fileStream);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csvReader.Configuration.HasHeaderRecord = false;
            csvReader.Configuration.Delimiter = ",";
            var records = csvReader.GetRecords<PostgresLogRecord>();
            OnLogChange?.Invoke(this, new OnLogAddEventArgs( records, e.FullPath));
            SaveCurrentStreamPosition(e.FullPath, fileStream.Position);
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
            _streamPositionDictionary[filePath] = position;
        }

        private void Scan(CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var files = Directory.GetFiles(_logFolderPath, "*.csv");
                foreach (var file in files)
                {
                    using var fileObj = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    fileObj.Flush();
                    Thread.Sleep(TimeSpan.FromMilliseconds(1));
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }

            // ReSharper disable once FunctionNeverReturns
        }


        ///<inheritdoc/>
        public void Dispose()
        {
            _fileSystemWatcher?.Dispose();
            _scanTaskCancellationTokenSource.Cancel();
        }
    }
}