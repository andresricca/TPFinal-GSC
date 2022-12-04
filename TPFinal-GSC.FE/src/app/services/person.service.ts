import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Person } from '../interfaces/person';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  url = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Person[]> {
    return this.http.get<Person[]>(`${this.url}/people`);
  }

  delete(id: number): Observable<Object> {
    return this.http.delete(`${this.url}/people/${id}`);
  }
    
  create(person: Person): Observable<Person> {
    return this.http.post<Person>(`${this.url}/people/`, person)
  }

  getById(id: number): Observable<Person> {
    return this.http.get<Person>(`${this.url}/people/${id}`)
  }

  modify(person: Person): Observable<Person> {
    return this.http.put<Person>(`${this.url}/people/${person.id}`, person)
  }
}
