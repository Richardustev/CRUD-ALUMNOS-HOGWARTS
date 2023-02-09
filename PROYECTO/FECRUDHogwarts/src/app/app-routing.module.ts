import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//COMPONENTES
import { AgregarEditarAlumnoComponent } from './components/agregar-editar-alumno/agregar-editar-alumno.component';
import { ListadoAlumnosComponent } from './components/listado-alumnos/listado-alumnos.component';

const routes: Routes = [
  { path: '', redirectTo: 'alumnos/lista', pathMatch: 'full' },
  { path: 'alumnos/lista', component: ListadoAlumnosComponent },
  { path: 'alumnos/add', component: AgregarEditarAlumnoComponent },
  { path: 'alumnos/editar/:id', component: AgregarEditarAlumnoComponent },
  { path: '**', redirectTo: 'alumnos/lista', pathMatch: 'full' },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
