import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { PersonCreateComponent } from './person-create/person-create.component';
import { PersonEditComponent } from './person-edit/person-edit.component';
import { PersonListComponent } from './person-list/person-list.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'person', component: PersonListComponent , canActivate: [AuthGuard] },
  { path: 'create', component: PersonCreateComponent, canActivate: [AuthGuard] },
  { path: 'edit/:id', component: PersonEditComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/person', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
