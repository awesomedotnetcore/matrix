import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';
import { Metadata } from '../entities/metadata';

@Injectable()
export class MetadataService {

  constructor(private http: HttpClient) {
  }

  getMetadata(): Observable<Metadata> {
    return this.http.get<Metadata>(environment.api + "/");
  }
}
