import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Person } from '../interfaces/person';
import { PersonService } from '../services/person.service';

@Component({
  selector: 'app-person-create',
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.css']
})
export class PersonCreateComponent implements OnInit {

  error = '';

  createForm: FormGroup = this.fb.group({
    name: ['', Validators.required],
    email: ['', [Validators.required, Validators.pattern(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/)]],
    phone: ['', [Validators.required, Validators.minLength(7), Validators.maxLength(13)]]
  });

  constructor(
    private personService: PersonService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  submit() {
    const person: Person = this.createForm.value;
    this.personService.create(person).subscribe({
      next: p => {
        this.error = '';
        this.router.navigate(['/person'])},
      error: e => {
        this.error = "Hubo un error al guardar la persona"}
    }
    );
  }

}
