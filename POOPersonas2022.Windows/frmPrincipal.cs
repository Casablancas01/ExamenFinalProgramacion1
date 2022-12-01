using POOPersonas2022.Datos;
using POOPersonas2022.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POOPersonas2022.Windows
{
    public partial class frmPrincipal : Form
    {
        
        private Club repositorio;
        private List<Persona> lista;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            repositorio = new Club();
            if (repositorio.GetCantidad() > 0)
            {
                RecargarDataGrid();
            }

        }


        #region DATAGRID
        private void RecargarDataGrid()
        {
            lista = repositorio.GetLista();
            MostrarDataGrid();
           
        }
        private void MostrarDataGrid()
        {
            lbCantidad.Text = repositorio.GetCantidad().ToString();
            dgvDatos.Rows.Clear();
            foreach (var i in lista)
            {
                var r = ConstruirFila();
                SetearFila(r, i);
                AgregarFila(r);
            }
        }
        private DataGridViewRow ConstruirFila()
        {
            var r = new DataGridViewRow();
            r.CreateCells(dgvDatos);
            return r;
        }
        private void SetearFila(DataGridViewRow r, Persona entidad)
        {
            entidad = repositorio.GetCategoria(entidad);
            entidad = repositorio.GetCuota(entidad);
            r.Cells[cmnDni.Index].Value = entidad.Dni;
            r.Cells[ColNombre.Index].Value = entidad.NombreCompleto().ToString();
            r.Cells[cmnSexo.Index].Value = entidad.Sexo;
            r.Cells[ColCategoria.Index].Value = entidad.Categoria;
            r.Cells[ColCuota.Index].Value = entidad.Cuota;


            r.Tag = entidad;
        }
        private void AgregarFila(DataGridViewRow r)
        {
            dgvDatos.Rows.Add(r);
        }


        #endregion

        #region Botones
        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmPersonasAE frm = new frmPersonasAE();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            Persona enti = frm.GetPersona();

            
            repositorio.Agregar(enti);
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, enti);
            AgregarFila(r);
            

        }
       

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }

            var r = dgvDatos.SelectedRows[0];
            Persona pBorrar = r.Tag as Persona;
            DialogResult dr = MessageBox.Show($"¿Desea borrar a {pBorrar.NombreCompleto()}?",
                "Confirmar Borrado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                return;
            }
            repositorio.Borrar(pBorrar);
            dgvDatos.Rows.Remove(r);
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }

            var r = dgvDatos.SelectedRows[0];
            Persona pEditar = r.Tag as Persona;
            frmPersonasAE frm = new frmPersonasAE() { Text = "Editar Persona" };
            frm.SetPersona(pEditar);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            pEditar = frm.GetPersona();
            
            SetearFila(r, pEditar);
        }

        private void tsbRefrescar_Click(object sender, EventArgs e)
        {
            RecargarDataGrid();
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show($"¿Salir De la Aplicacion?",
               "Confirmar",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                return;
            }
            repositorio.Guardar();
            Application.Exit();
        }
        #endregion

        #region Filtros
        private void masculinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sexo = Sexo.Masculino;
            ListarPorSexo(sexo);
        }

        private void femeninoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sexo = Sexo.Femenino;
            ListarPorSexo(sexo);
        }
        private void ListarPorSexo(Sexo sexo)
        {
            lista = repositorio.FiltrarPorSexo(sexo);
            MostrarDataGrid();
        }


        #endregion

        #region Ordenar
        private void aZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repositorio.OrdenarAlfaAsc();
            MostrarDataGrid();
        }

        private void zAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repositorio.OrdenarAlfaDesc();
            MostrarDataGrid();
        }
        #endregion


        //mostrar el nombre completo en la columna socio 
        //edad categoria y cuota 
        // contador de socios

    }
}
