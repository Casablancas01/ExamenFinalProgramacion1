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
    public partial class frmPersonasAE : Form
    {



        public frmPersonasAE()
        {
            InitializeComponent();
        }
        private Persona persona;
        private bool esEdicion = false;
        private Club repositorio = new Club();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            if (persona != null)
            {
                esEdicion = true;
                DNITextBox.Text = persona.Dni.ToString();
                DNITextBox.Enabled = false;
                ApellidoTextBox.Text = persona.Apellido;
                PrimerNombreTextBox.Text = persona.PrimerNombre;
                SegundoNombreTextBox.Text = persona.SegundoNombre;
                persona.FechaNacimiento = FechaDateTimePicker.Value.Date;
                if (persona.Sexo == Sexo.Masculino)
                {
                    MasculinoRadioButton.Checked = true;
                }
                else
                {
                    FemeninoRadioButton.Checked = true;
                }           
            }
        }

        

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (!esEdicion)
            {
                if (string.IsNullOrEmpty(DNITextBox.Text))
                {
                    valido = false;
                    errorProvider1.SetError(DNITextBox, "Debe ingresar el DNI");
                }
                else if (DNITextBox.Text.Length < 7)
                {
                    valido = false;
                    errorProvider1.SetError(DNITextBox, "DNI mal ingresado");
                }
                else if (!long.TryParse(DNITextBox.Text, out long dni))
                {
                    valido = false;
                    errorProvider1.SetError(DNITextBox, "DNI no válido");
                }
                else if (repositorio.ExistePersona(dni))
                {
                    valido = false;
                    errorProvider1.SetError(DNITextBox, "DNI repetido!!!");
                }

            }
            if (string.IsNullOrEmpty(ApellidoTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(ApellidoTextBox, "Debe ingresar el apellido");
            }

            if (string.IsNullOrEmpty(PrimerNombreTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(PrimerNombreTextBox, "Debe ingresar el primer nombre");
            }

            if (FechaDateTimePicker.Value.Date > DateTime.Now.Date)
            {
                valido = false;
                errorProvider1.SetError(FechaDateTimePicker, "Fecha no válida");
            }
            


            return valido;
        }

        public Persona GetPersona()
        {
            return persona;
        }

        public void SetPersona(Persona pEditar)
        {
            persona = pEditar;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (persona == null)
                {
                    persona = new Persona();
                }

                persona.Dni = long.Parse(DNITextBox.Text);
                persona.Apellido = ApellidoTextBox.Text;
                persona.PrimerNombre = PrimerNombreTextBox.Text;
                persona.SegundoNombre = SegundoNombreTextBox.Text;
                persona.Sexo = MasculinoRadioButton.Checked ? Sexo.Masculino : Sexo.Femenino;
                persona.FechaNacimiento = FechaDateTimePicker.Value.Date;
                
                if (7>persona.Edad())
                {
                    MessageBox.Show("El socio debe ser mayor de 7 anios", "Error",MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    DialogResult = DialogResult.OK;
                }

               
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
