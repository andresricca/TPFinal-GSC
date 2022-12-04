import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Person } from '../interfaces/person';
import { PersonService } from '../services/person.service';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css']
})
export class PersonListComponent implements OnInit {

  people$!: Observable<Person[]>;

  constructor(
    private personService: PersonService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadPeople();
  }

  loadPeople() {
    this.people$ = this.personService.getAll();
  }

  create() {
    this.router.navigate(['/create']);
  }

  delete(id: number) {
    this.personService.delete(id).subscribe({
      next: () => { this.loadPeople() }
    });
  }
}
