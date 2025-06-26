import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RestService {
  private apiUrl = 'https://localhost:7202/somedata';

  constructor(private http: HttpClient) { }

  getSomeData(): Observable<string> {
    return this.http.get(this.apiUrl, { responseType: 'text' });
  }
}
