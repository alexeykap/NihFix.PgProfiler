export class PostgresLogRecord {
  logTime: Date;
  userName: string;
  databaseName: string;
  processId?: number;
  connectionFrom: string
  sessionId: string;
  sessionLineNum?: number;
  commandTag: string;
  sessionStartTime: Date;
  virtualTransactionId: string;
  transactionId?: number;
  errorSeverity: string;
  sqlStateCode: string;
  message: string;
  detail: string;
  hint: string;
  internalQuery: string;
  internalQueryPos?: number;
  context: string;
  query: string;
  queryPos?: number;
  location: string;
  applicationName: string;
}
