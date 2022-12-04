import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Person } from '../interfaces/person';
import { PersonService } from '../services/person.service';

@Component({
  selector: 'app-person-edit',
  templateUrl: './person-edit.component.html',
  styleUrls: ['./person-edit.component.css']
})
export class PersonEditComponent implements OnInit {

  error = '';

  editForm: FormGroup = this.fb.group({
    id: ['', Validators.required],
    name: ['', Validators.required],
    email: ['', [Validators.required, Validators.pattern(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/)]],
    phone: ['', [Validators.required, Validators.minLength(7), Validators.maxLength(13)]]
  });

  constructor(
    private personService: PersonService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    const id = + this.route.snapshot.paramMap.get('id')!;
    this.personService.getById(id).subscribe({
      next: p => { this.editForm.setValue(p) },
      error: () => { this.error = "No se encontro la persona con id" + id }
    })
  }

  submit() {
    const person = this.editForm.value;
    this.personService.modify(person).subscribe({
      next: () => {
        this.error = '';
        this.router.navigate(['/person'])},
      error: e => {
        this.error = "Hubo un error al guardar las modificaciones"
      }
    })
  }

}
