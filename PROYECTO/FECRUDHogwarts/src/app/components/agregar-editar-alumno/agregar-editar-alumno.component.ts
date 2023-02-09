import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarRef } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Alumno } from 'src/app/interface/alumno';
import { AlumnoService } from 'src/app/services/alumno.service';


interface casa {
  value: string;
  casa: string;
}

@Component({
  selector: 'app-agregar-editar-alumno',
  templateUrl: './agregar-editar-alumno.component.html',
  styleUrls: ['./agregar-editar-alumno.component.css']
})
export class AgregarEditarAlumnoComponent implements OnInit {
  loading : boolean = false;  
  id:number;
  operacion: string = 'AGREGAR';

  form: FormGroup;
  tituloAccion: string = 'NUEVO';
  listaAlumnos: Alumno[]=[];
  constructor(
    private _snackBar: MatSnackBar,
    private fb: FormBuilder,
    private _alumnoService: AlumnoService,
    private aRoute: ActivatedRoute
    ){
      //CAPTANDO EL ID PARA SABER SI SE VA A EDITAR UN ALUMNO O A REGISTRAR UNO NUEVO
      this.id = Number(this.aRoute.snapshot.paramMap.get('id'));

      //COMPROBANTES DEL FORMULARIO
      this.form = this.fb.group({
        nombre: ['', [Validators.required, Validators.maxLength(20)]],
        apellido: ['', [Validators.required, Validators.maxLength(20)]],
        identificacion: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(10)]],
        edad: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(2)]],
        casa: ['', Validators.required],
      })

      this._alumnoService.getList().subscribe({
        next:(dataResponse)=>{
          this.listaAlumnos = dataResponse;
        },error:(e)=>{}
      })
    }

  ngOnInit(): void {
    if(this.id !- 0){
      this.operacion = "EDITAR";
      this.getAlumnoData(this.id);
    }
  }

  //OBTENER LOS DATOS DE UN ALUMNO PARA PODER EDITARLO
  getAlumnoData(id: number){
    this.loading = true;

    this._alumnoService.getAlumno(id).subscribe(data => {
      console.log(data);
      this.loading = false;

      this.form.patchValue({
        nombre: data.nombre,
        apellido: data.apellido,
        edad: data.edad,
        identificacion: data.identificacion,
        casa: data.casa
      })
    })
  }

  //OBTENER LOS VALORES DEL FORM Y SUBIR LOS DATOS
  AddEditAlumno(){

    const modelo: Alumno = {
      id: 0,
      nombre:this.form.get('nombre')?.value,
      apellido:this.form.get('apellido')?.value,
      identificacion:this.form.get('identificacion')?.value,
      casa:this.form.get('casa')?.value,
      edad:this.form.get('edad')?.value,
    }
    //Este if es para determinar si se va a editar o a subir los datos ingresados
    if(this.id != 0){
      modelo.id = this.id; //Hay que setear el id
      this.updateAlumno(this.id, modelo);
    } else {
      this.agregarAlumno(modelo);
    }
    
  }
  agregarAlumno(modelo: any){
    this._alumnoService.add(modelo).subscribe({
      next:(dataResponse) => {
        this.mostrarAlerta("El alumno fue creado", "Listo");
      }, error:(e)=>{
        console.log(e);
        this.mostrarAlerta("No se pudo crear un alumno", "Error");
      }
    })
  }
  updateAlumno(id:number, modelo: any){
    this.loading = true;
    this._alumnoService.update(id, modelo).subscribe(() => {
      this.loading = false;
      this.mostrarAlerta("El alumno fue actualizado con exito.", "Listo");

    })
  }

  //Notificacion dinamica
  mostrarAlerta(msg: string, accion: string){
    this._snackBar.open(msg, accion,{
      horizontalPosition:"end",
      verticalPosition:"top",
      duration:3000
    })      
  }  

  //VALORES DEL SELECT
  casas: casa[] = [
    {value: 'Gryffindor', casa: 'Gryffindor'},
    {value: 'Hufflepuff', casa: 'Hufflepuff'},
    {value: 'Ravenclaw', casa: 'Ravenclaw'},
    {value: 'Slytherin', casa: 'Slytherin'},
  ];
}