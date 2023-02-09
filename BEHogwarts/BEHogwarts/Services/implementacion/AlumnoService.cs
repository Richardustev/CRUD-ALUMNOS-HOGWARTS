using Microsoft.EntityFrameworkCore;
using BEHogwarts.Models;
using BEHogwarts.Services.hogwarts;

namespace BEHogwarts.Services.implementacion
{
    public class AlumnoService: IAlumno
    {
        private DbalumnoContext _dbContext;
        public AlumnoService (DbalumnoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Alumno> Add(Alumno modelo)
        {
            try
            {
                //añadir solicitud
                _dbContext.Alumnos.Add(modelo);
                await _dbContext.SaveChangesAsync();

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(Alumno modelo)
        {
            try
            {
                _dbContext.Alumnos.Remove(modelo);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Alumno> Get(int id)
        {
            try {
                Alumno? encontrado = new Alumno();
                encontrado = await _dbContext.Alumnos.Where(e => e.Id == id).FirstOrDefaultAsync();

                return encontrado;

            } catch(Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<List<Alumno>> GetList()
        {
            try
            {
                //Creamos una lista utilizando la tabla de la base de datos
                List<Alumno> lista = new List<Alumno> ();
                lista = await _dbContext.Alumnos.ToListAsync();

                return lista; //retornar los datos
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Alumno modelo)
        {
            try
            {
                //actualizar solicitud
                _dbContext.Alumnos.Update(modelo);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
