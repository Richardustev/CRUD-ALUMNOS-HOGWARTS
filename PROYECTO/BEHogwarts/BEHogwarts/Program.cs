using AutoMapper;
using BEHogwarts.DTOs;
using BEHogwarts.Utilidades;

using BEHogwarts.Models;
using Microsoft.EntityFrameworkCore;

//Añadir la refencia del servicio y la implementacion
using BEHogwarts.Services.hogwarts;
using BEHogwarts.Services.implementacion;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Vamos a pasarle a Entity framework la cadena de conexion
builder.Services.AddDbContext<DbalumnoContext>(options =>
{
    //Buscamos el string de conexion ubicado en appsettings.json "SQLConexion"
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConexion"));
});

//Aqui estoy inyectando los servicios para luego poder usarlos en la rest api
builder.Services.AddScoped<IAlumno, AlumnoService>();

//Agregamos las tablas en formato DTOs gracias a Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//CORS
//Agregamos CORS para evitar conflictos con los enlaces de Angular y Cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//PETICIONES API REST
#region PETICIONES API REST

//GET ALL ALUMNOS
app.MapGet("alumnos/lista", async ( 
    IAlumno _alumnoServicio, //Llamamos el servicio
    IMapper _mapper //Preparamos el mapperDTO
    ) =>
{
    var listaAlumnos = await _alumnoServicio.GetList(); //Obtenemos los datos del servicio 
    var listaAlumnosDTO = _mapper.Map<List<AlumnoDTO>>(listaAlumnos); //Lo convertimos en formato DTO

    if (listaAlumnosDTO.Count > 0)
    {
        return Results.Ok(listaAlumnosDTO);
    } else
    {
        return Results.NotFound();
    }
});

//GET UN ALUMNOS
app.MapGet("alumnos/ver/{id}", async (
    int id,
    IAlumno _alumnoServicio,
    IMapper _mapper
) => {
    var alumno = await _alumnoServicio.Get(id);

    if (alumno != null)
    {
        var alumnoDTO = _mapper.Map<AlumnoDTO>(alumno);
        return Results.Ok(alumnoDTO);
    }
    else
    {
        return Results.NotFound();
    }
});

//AGREGAR ALUMNOS
app.MapPost("alumnos/add", async(
    AlumnoDTO modelo, //Para insertar datos se debe recibir el modelo
    IAlumno _alumnoServicio, //Llamamos el servicio
    IMapper _mapper //Preparamos el mapperDTO
    ) => {
        var _alumno = _mapper.Map<Alumno>(modelo);
        var _alumnoCreado = await _alumnoServicio.Add(_alumno);

        if (_alumnoCreado.Id != 0)
        {
            return Results.Ok(_mapper.Map<AlumnoDTO>(_alumnoCreado));
        } else
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
});

//ACTUALIZAR ALUMNOS
app.MapPut("alumnos/editar/{id}", async(
    int id, //Recibimos el id
    AlumnoDTO modelo, //Para insertar datos se debe recibir el modelo
    IAlumno _alumnoServicio, //Llamamos el servicio
    IMapper _mapper //Preparamos el mapperDTO
    ) => {
    var _encontrado = await _alumnoServicio.Get(id);
    //Si no encuentra ningun resultado, entonces procede a tirar un mensaje de error
    if(_encontrado is null) { return Results.NotFound(); }

    //Procedemos a igualar los valores para que sean cambiados
    var _alumno = _mapper.Map<Alumno>(modelo);
    _encontrado.Nombre = _alumno.Nombre;
    _encontrado.Apellido = _alumno.Apellido;
    _encontrado.Identificacion = _alumno.Identificacion;
    _encontrado.Edad = _alumno.Edad;
    _encontrado.Casa = _alumno.Casa;

    //Esperamos el valor de la respuesta del servidor
    var resp = await _alumnoServicio.Update(_encontrado);

    if (resp)
    {
        return Results.Ok(_mapper.Map<AlumnoDTO>(_encontrado));
    } else
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
});

//ARREGLAR ERROR DE AQUI PA SER FELIZ
//BORRAR ALUMNOS
app.MapDelete("alumnos/borrar/{id}", async (
    int id, //Recibimos el id
    IAlumno _alumnoServicio //Llamamos el servicio
    ) => {
        var _encontrado = await _alumnoServicio.Get(id);
        //Si no encuentra ningun resultado, entonces procede a tirar un mensaje de error
        if (_encontrado is null) { return Results.NotFound(); }

        //Aqui, si encuentra un alumno, lo borra
        var resp = await _alumnoServicio.Delete(_encontrado);

        if (resp) {
            return Results.Ok();
        } else
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
});
#endregion

app.UseCors("NuevaPolitica");

app.Run();