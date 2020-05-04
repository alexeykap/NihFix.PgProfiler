import {Injectable} from "@angular/core";
import { ElectronService } from 'ngx-electron';
import {Observable, Subject} from "rxjs";
import {PostgresLogRecord} from "../models/postgres-log-record";

@Injectable()
export class ProfilerDataService {
  constructor(private _electronService: ElectronService) { }

  subscribe():Observable<PostgresLogRecord[]>{
    this._electronService.ipcRenderer.send('subscribeToProfilerData');
    let subject=new Subject<PostgresLogRecord[]>();
    this._electronService.ipcRenderer.on('profileData', (event: Electron.IpcRendererEvent, profilerRow:PostgresLogRecord[])=>{
      console.log(profilerRow);
      subject.next(profilerRow);
    })
    return subject;
  }

}
