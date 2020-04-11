using System;

namespace NihFix.PgProfiler.LogProcessing
{
    public class LogRecord
    {
        public string LogTime { get; set; }
        public string UserName { get; set; }
        public string DatabaseName { get; set; }
        public string ProcessId { get; set; }
        
        public string ConnectionFrom { get; set; }
        public string SessionId { get; set; }
        public string SessionLineNum { get; set; }
        public string CommandTag { get; set; }
        public string SessionStartTime { get; set; }
        public string VirtualTransactionId { get; set; }
        public string TransactionId { get; set; }
        public string ErrorSeverity { get; set; }
        public string SqlStateCode { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string Hint { get; set; }
        public string InternalQuery { get; set; }
        public string InternalQueryPos { get; set; }
        public string Context { get; set; }
        public string Query { get; set; }
        public string QueryPos { get; set; }
        public string Location { get; set; }
        public string ApplicationName { get; set; }

    }
}