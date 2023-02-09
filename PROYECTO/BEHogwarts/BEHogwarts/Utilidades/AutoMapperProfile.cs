using AutoMapper;
using BEHogwarts.DTOs;
using BEHogwarts.Models;
using System.Globalization;

namespace BEHogwarts.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() {
            #region Alumno
            CreateMap<Alumno, AlumnoDTO>().ReverseMap();
            #endregion
        }
    }
}
