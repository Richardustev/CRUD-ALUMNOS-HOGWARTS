import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { enviroment } from 'src/enviroments/enviroment';
import { Observable } from 'rxjs';
import { Alumno } from '../interface/alumno';

@Injectable({
  providedIn: 'root'
})
export class AlumnoService {
  //Variables de conexion de nuestro endPoint
  private endpoint:string = enviroment.endPoint;
  private apirUrl:string = this.endpoint + "alumnos/";

  constructor(private http:HttpClient) { }

  //LISTA DE LOS ESTUDIANTES DE HOGWARTS
  getList():Observable<Alumno[]>{
    return this.http.get<Alumno[]>(`${this.apirUrl}lista`);
  }

  //OBTERNER UN SOLO ALUMNO
  getAlumno(id:number):Observable<Alumno>{
    return this.http.get<Alumno>(`${this.apirUrl}ver/${id}`)
  }

  //AÑADIR ESTUDIANTES
  add(modelo:Alumno):Observable<Alumno>{
    return this.http.post<Alumno>(`${this.apirUrl}add`, modelo);
  }

  //EDITAR ESTUDIANTES
  update(id:number, modelo:Alumno):Observable<Alumno>{
    return this.http.put<Alumno>(`${this.apirUrl}editar/${id}`, modelo);
  }

  //BORRAR ESTUDIANTES
  DeleteAlumno(id:number):Observable<void>{
    return this.http.delete<void>(`${this.apirUrl}borrar/${id}`);
  }
}