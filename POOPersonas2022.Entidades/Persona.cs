using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOPersonas2022.Entidades
{
    public class Persona
    {
        public long Dni { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Sexo Sexo { get; set; }
        public Categoria Categoria { get; set; }
        public int Cuota { get; set; }

        public int Edad()
        {
            int edad = DateTime.Today.Year - FechaNacimiento.Year;
            if (FechaNacimiento.AddYears(edad) > DateTime.Today)
            {
                edad--;
            }
            return edad;
        }

        public string NombreCompleto()
        {
            return $"{Apellido.ToUpper()}, {PrimerNombre} {SegundoNombre}.";
        }
    }
}
