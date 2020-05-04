// using System;
// using System.Collections.Generic;
//
// namespace NihFix.PgProfiler.LogProcessing.SecondLevelInterpreter
// {
//     public class LogInterpreter
//     {
//         private readonly LogInterpreterConfig _config;
//         private LogData _currentData;
//
//         public LogInterpreter(LogInterpreterConfig config)
//         {
//             _config = config;
//         }
//
//         public List<LogData> ParsePostgresLogData(IEnumerable<PostgresLogRecord> logRecords)
//         {
//             var logData = new List<LogData>();
//             foreach (var logRecord in logRecords)
//             {
//                 var recordState = GetRecordState(logRecord);
//                 switch (recordState)
//                 {
//                     case InterpreterState.Query:
//                     {
//                         if (_currentData != null)
//                         {
//                             logData.Add(_currentData);
//                         }
//
//                         _currentData = ParseQueryLog(logRecord);
//                         break;
//                     }
//                     case InterpreterState.Params:
//                     {
//                         _currentData = ParseParams(logRecord, _currentData);
//                         break;
//                     }
//                     case InterpreterState.Duration:
//                     {
//                         _currentData = ParseDuration(logRecord, _currentData);
//                         logData.Add(_currentData);
//                         break;
//                     }
//                     default:
//                     {
//                         logData.Add(_currentData);
//                         break;
//                     }
//                 }
//             }
//
//             return logData;
//         }
//
//         private InterpreterState GetRecordState(PostgresLogRecord logRecord)
//         {
//             if (logRecord.Message.StartsWith(_config.QueryPrefix))
//             {
//                 return InterpreterState.Query;
//             }
//             else if (logRecord.Message.StartsWith(_config.DurationPrefix))
//             {
//                 return InterpreterState.Duration;
//             }
//             else if (logRecord.Message.StartsWith(_config.ParamsPrefix))
//             {
//                 return InterpreterState.Params;
//             }
//             else
//             {
//                 return InterpreterState.Unknown;
//             }
//         }
//
//         private LogData ParseQueryLog(PostgresLogRecord logRecord)
//         {
//             var logData = new LogData
//             {
//                 DateTime = DateTime.Parse( logRecord.LogTime),
//                 DataBaseName = logRecord.DatabaseName
//             };
//             logData.SqlText = logRecord.Message.Substring(_config.QueryPrefix.Length);
//             return logData;
//         }
//
//         private LogData ParseDuration(PostgresLogRecord logRecord, LogData destination)
//         {
//             destination.DurationMs = double.Parse(logRecord.Message.Substring(_config.DurationPrefix.Length));
//             return destination;
//         }
//
//         private LogData ParseParams(PostgresLogRecord logRecord, LogData destination)
//         {
//             destination.QueryParams = logRecord.Message.Substring(_config.QueryPrefix.Length);
//             return destination;
//         }
//     }
// }