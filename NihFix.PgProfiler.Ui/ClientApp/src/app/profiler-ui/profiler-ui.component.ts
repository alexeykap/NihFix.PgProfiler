import {Component, OnInit} from '@angular/core';
import {ColDef, ColumnApi, GridApi, GridReadyEvent} from "ag-grid-community";
import {nameof} from "../common/functions/nameof";
import {PostgresLogRecord} from "../models/postgres-log-record";
import {ProfilerDataService} from "../services/profiler-data.service";

@Component({
  selector: 'app-profiler-ui',
  templateUrl: './profiler-ui.component.html',
  styleUrls: ['./profiler-ui.component.css']
})
export class ProfilerUiComponent implements OnInit {

  gridApi: GridApi;
  columnApi: ColumnApi;

  constructor(private pgProfileDataService: ProfilerDataService) {


  }

  colDefs: ColDef[] = [
    {
      headerName: nameof<PostgresLogRecord>("logTime"),
      field: nameof<PostgresLogRecord>("logTime")
    },
    {
      headerName: nameof<PostgresLogRecord>("userName"),
      field: nameof<PostgresLogRecord>("userName")
    },
    {
      headerName: nameof<PostgresLogRecord>("databaseName"),
      field: nameof<PostgresLogRecord>("databaseName")
    },
    {
      headerName: nameof<PostgresLogRecord>("processId"),
      field: nameof<PostgresLogRecord>("processId")
    },
    {
      headerName: nameof<PostgresLogRecord>("connectionFrom"),
      field: nameof<PostgresLogRecord>("connectionFrom")
    },
    {
      headerName: nameof<PostgresLogRecord>("sessionId"),
      field: nameof<PostgresLogRecord>("sessionId")
    },
    {
      headerName: nameof<PostgresLogRecord>("sessionLineNum"),
      field: nameof<PostgresLogRecord>("sessionLineNum")
    },
    {
      headerName: nameof<PostgresLogRecord>("commandTag"),
      field: nameof<PostgresLogRecord>("commandTag")
    },
    {
      headerName: nameof<PostgresLogRecord>("sessionStartTime"),
      field: nameof<PostgresLogRecord>("sessionStartTime")
    },
    {
      headerName: nameof<PostgresLogRecord>("virtualTransactionId"),
      field: nameof<PostgresLogRecord>("virtualTransactionId")
    },
    {
      headerName: nameof<PostgresLogRecord>("transactionId"),
      field: nameof<PostgresLogRecord>("transactionId")
    },
    {
      headerName: nameof<PostgresLogRecord>("errorSeverity"),
      field: nameof<PostgresLogRecord>("errorSeverity")
    },
    {
      headerName: nameof<PostgresLogRecord>("sqlStateCode"),
      field: nameof<PostgresLogRecord>("sqlStateCode")
    },
    {
      headerName: nameof<PostgresLogRecord>("message"),
      field: nameof<PostgresLogRecord>("message")
    },
    {
      headerName: nameof<PostgresLogRecord>("detail"),
      field: nameof<PostgresLogRecord>("detail")
    },
    {
      headerName: nameof<PostgresLogRecord>("hint"),
      field: nameof<PostgresLogRecord>("hint")
    },
    {
      headerName: nameof<PostgresLogRecord>("internalQuery"),
      field: nameof<PostgresLogRecord>("internalQuery")
    },
    {
      headerName: nameof<PostgresLogRecord>("internalQueryPos"),
      field: nameof<PostgresLogRecord>("internalQueryPos")
    },
    {
      headerName: nameof<PostgresLogRecord>("context"),
      field: nameof<PostgresLogRecord>("context")
    },
    {
      headerName: nameof<PostgresLogRecord>("query"),
      field: nameof<PostgresLogRecord>("query")
    },
    {
      headerName: nameof<PostgresLogRecord>("queryPos"),
      field: nameof<PostgresLogRecord>("queryPos")
    },
    {
      headerName: nameof<PostgresLogRecord>("location"),
      field: nameof<PostgresLogRecord>("location")
    },
    {
      headerName: nameof<PostgresLogRecord>("applicationName"),
      field: nameof<PostgresLogRecord>("applicationName")
    },
  ];

  ngOnInit() {

  }

  onGridReady(params: GridReadyEvent) {
    this.gridApi = params.api;
    this.columnApi = params.columnApi;

  }

  subscr() {
    this.pgProfileDataService.subscribe().subscribe((v) => {
      this.gridApi.updateRowData(
        {add: v}
      )
    })
  }

}
