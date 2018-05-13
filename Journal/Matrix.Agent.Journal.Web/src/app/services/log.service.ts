import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { Log } from '../entities/log';

@Injectable()
export class LogService {

  constructor(private http: HttpClient) {
  }

  getLogs(appId: string, startDate: string, endDate: string): Observable<Log[]> {
    return this.http.get<Log[]>(environment.api + "/apps/" + appId + "/logs/" + startDate + "/" + endDate);
  }

  searchLogs(appId: string, startDate: string, endDate: string, searchTerm: string): Observable<Log[]> {
    return this.http.get<Log[]>(environment.api + "/apps/" + appId + "/logs/" + startDate + "/" + endDate + "/" + searchTerm);
  }
}
