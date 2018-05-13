import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { Application } from '../entities/application';

@Injectable()
export class ApplicationService {

  constructor(private http: HttpClient) {
  }

  getApplications(): Observable<Application[]> {
    return this.http.get<Application[]>(environment.api + "/apps");
  }
}
