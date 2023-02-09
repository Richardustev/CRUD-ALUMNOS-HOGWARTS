
//Importando los modelos de la bdd
using BEHogwarts.Models;


namespace BEHogwarts.Services.hogwarts
{
    public interface IAlumno
    {
        //Metodo para obtener todos los alumnos
        Task<List<Alumno>> GetList();

        //Obtener un solo alumno
        Task<Alumno> Get(int id);

        //Agregar solicitudes de ingreso
        Task<Alumno> Add(Alumno modelo);

        //Editar un empleado
        Task<bool> Update(Alumno modelo);

        //Borrar alumno
        Task<bool> Delete(Alumno modelo);
    }
}
