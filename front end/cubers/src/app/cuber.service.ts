import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Cuber} from './models/cuber';
import {Observable} from 'rxjs';
import {CuberSummary} from './models/CuberSummary';

@Injectable({
  providedIn: 'root'
})
export class CuberService {

  private baseUrl = "https://localhost:44318";

  constructor(private http: HttpClient) { }

  public getCuberSummary(): Observable<CuberSummary>{
    return this.http.get<CuberSummary>(this.baseUrl + "/cuber");
  }

  public getCuber(id: number): Observable<Cuber>{
      return this.http.get<Cuber>(this.baseUrl + "/cuber/" + id);
  }

  public deleteCuber(id :number): Observable<{}>{
    return this.http.delete(this.baseUrl + "/cuber/" + id);
  }

  public updateCuber(cuber: Cuber): Observable<{}>{
    return this.http.post(this.baseUrl + "/cuber/", cuber);
  }
}
