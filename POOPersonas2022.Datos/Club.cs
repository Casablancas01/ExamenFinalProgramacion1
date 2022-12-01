using POOPersonas2022.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOPersonas2022.Datos
{
    public class Club
    {
         
        public List<Persona> lista;
        private bool hayCambios = false;
        public Club()
        {
            lista = new List<Persona>();
            lista = ManejadorArchivoSecuencial.LeerArchivo();
        }

        public  Persona GetCategoria(Persona p)
        {
            int edad = p.Edad();
            if (edad>=7 &&edad<=12 )
            {
                p.Categoria = Categoria.Infantil;
            }
            if (edad>=12 && edad<=17)
            {
                p.Categoria = Categoria.Juvenil;
            }
            if (edad>=18 && edad<=60)
            {
                p.Categoria = Categoria.Mayor;
            }
            if (edad>60)
            {
                p.Categoria = Categoria.Vitalicio;
            }
            return p;
           
        }

        public Persona GetCuota(Persona p)
        {
            if (p.Categoria== Categoria.Infantil)
            {
                p.Cuota = 1000;
            }
            if (p.Categoria==Categoria.Juvenil)
            {
                p.Cuota = 1200;
            }
            if (p.Categoria==Categoria.Mayor)
            {
                p.Cuota = 1500;
            }
            if (p.Categoria==Categoria.Vitalicio)
            {
                p.Cuota = 500;
            }

            return p;
        }

        #region Manipulador de la lista
        public void Agregar(Persona entidad)
        {

            lista.Add(entidad);
        }
        public void Borrar(Persona entidad)
        {

            lista.Remove(entidad);
        }
        public int GetCantidad()
        {
            return lista.Count;
        }
        public List<Persona> GetLista()
        {
            return lista;
        }
        #endregion

        #region Archivos Secuenciales 
        public void Guardar()
        {
            
                ManejadorArchivoSecuencial.GuardarEnArchivo(lista);
            
        }
        #endregion

        public bool ExistePersona(long dniBuscado)
        {
            return lista.Any(p => p.Dni == dniBuscado);
        }

        #region Filtro y orden
        public List<Persona> FiltrarPorSexo(Sexo sexo)
        {
            return lista.Where(p => p.Sexo == sexo).ToList();
        }


        public List<Persona> OrdenarAlfaAsc()
        {
            return lista
                .OrderBy(p => p.Apellido)
                .ThenBy(p => p.PrimerNombre)
                .ThenBy(p => p.SegundoNombre)
                .ToList();
        }

        public List<Persona> OrdenarAlfaDesc()
        {
            return lista.OrderByDescending(p => p.Apellido)
                .ThenByDescending(p => p.PrimerNombre)
                .ThenByDescending(p => p.SegundoNombre)
                .ToList();
        }

        #endregion




    }
}
