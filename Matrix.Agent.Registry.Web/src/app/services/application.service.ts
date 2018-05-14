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

  register(name: string, description: string): Observable<string> {
    return this.http.post<string>(environment.api + "/register", {
      name: name,
      description: description
    });
  }

  update(id: string, name: string, description: string): Observable<string> {
    return this.http.post<string>(environment.api + "/apps", {
      id: id,
      name: name,
      description: description
    });
  }

  unregister(id: string): Observable<boolean> {
    return this.http.get<boolean>(environment.api + "/unregister/" + id);
  }
}
