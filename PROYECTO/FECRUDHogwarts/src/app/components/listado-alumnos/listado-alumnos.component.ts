import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
//Immportando la interfaz
import { Alumno } from 'src/app/interface/alumno';
import { AlumnoService } from 'src/app/services/alumno.service';
@Component({
  selector: 'app-listado-alumnos',
  templateUrl: './listado-alumnos.component.html',
  styleUrls: ['./listado-alumnos.component.css']
})

export class ListadoAlumnosComponent implements OnInit, AfterViewInit {
  loading: boolean = false;
  
  //ROWS DE LA TABLA
  displayedColumns: string[] = ['nombre', 'apellido', 'identificacion', 'edad', 'casa', 'acciones'];
  dataSource = new MatTableDataSource<Alumno>();
  constructor(
    private _snackBar: MatSnackBar,
    private _alumnoService: AlumnoService,
  ){
    
    
  }

  ngOnInit(): void {
    this.mostrarAlumnos();
  
  }

  //FUNCION PARA MOSTRAR ALUMNOS
  mostrarAlumnos(){
    this._alumnoService.getList().subscribe({
      next:(dataResponse) => {
        console.log(dataResponse);
        this.dataSource.data = dataResponse;
      },error:(e)=> {}
    })
  }

  //PAGINACION
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  //FUNCION PARA QUE EL FILTRO FUNCIONE
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  //BORRAR ALUMNOS
  eliminarAlumno(id: number){
    console.log(id);
    this.loading = true;
    this._alumnoService.DeleteAlumno(id).subscribe(() => {
      this.mensajeExito();
      this.loading = false;
      this.mostrarAlumnos();
    })
  }
  //MENSAJE PARA CUANDO SE BORRAN ALUMNOS
  mensajeExito(){ 
    this._snackBar.open("El alumno fue eliminado", "Listo", {
      duration: 4000,
      horizontalPosition: 'right',
      verticalPosition: 'top'
    })
  }
}

