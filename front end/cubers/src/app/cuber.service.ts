import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Cuber} from './models/cuber';
import {CuberSummary} from './models/CuberSummary'
import {Observable} from 'rxjs';
import {environment} from '../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class CuberService {

  constructor(private http: HttpClient) { }

  public getCuberSummary(): Observable<CuberSummary>{
    return this.http.get<CuberSummary>(environment.serverAddress + "/cuber");
  }

  public getCuber(id: number): Observable<Cuber>{
      return this.http.get<Cuber>(environment.serverAddress + "/cuber/" + id);
  }

  public deleteCuber(id :number): Observable<{}>{
    return this.http.delete(environment.serverAddress + "/cuber/" + id);
  }

  public updateCuber(cuber: Cuber): Observable<Cuber>{
    return this.http.post<Cuber>(environment.serverAddress + "/cuber/", cuber);
  }
}
