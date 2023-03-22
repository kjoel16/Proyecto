using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Vista
{
    public partial class ClientesForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ClientesForm()
        {
            InitializeComponent();
        }
        string tipoOperacion;
        DataTable dt = new DataTable();
        ClienteDB ClienteDB = new ClienteDB();
        Cliente user = new Cliente();


        private void HabilitarControles()
        {
            IdentidadtextBox.Enabled = true;
            NombretextBox.Enabled = true;
            TelefonotextBox.Enabled = true;
            CorreotextBox.Enabled = true;
            DirecciontextBox.Enabled = true;
            EstaActivocheckBox.Enabled = true;
            FechaNacimientodateTimePicker.Enabled = true;
            Guardarbutton.Enabled = true;
            Cancelarbutton.Enabled = true;
            Modificarbutton.Enabled = false;
        }
        private void DeshabilitarControles()
        {
            IdentidadtextBox.Enabled = false;
            NombretextBox.Enabled = false;
            TelefonotextBox.Enabled = false;
            CorreotextBox.Enabled = false;
            DirecciontextBox.Enabled = false;
            EstaActivocheckBox.Enabled = false;
            FechaNacimientodateTimePicker.Enabled = false;          
            Guardarbutton.Enabled = false;
            Cancelarbutton.Enabled = false;
            Modificarbutton.Enabled = true;
        }

        private void LimpiarControles()
        {
            IdentidadtextBox.Clear();
            NombretextBox.Clear();
            TelefonotextBox.Clear();
            CorreotextBox.Clear();
            DirecciontextBox.Clear();
            DirecciontextBox.Clear();
            EstaActivocheckBox.Checked = false;
           
        }
        

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            IdentidadtextBox.Focus();
            HabilitarControles();
            tipoOperacion = "Nuevo";
        }

        private void Cancelarbutton_Click(object sender, EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
        }


        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            if (tipoOperacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(IdentidadtextBox.Text))
                {
                    errorProvider1.SetError(IdentidadtextBox, "Ingrese una identidad");
                    IdentidadtextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(NombretextBox.Text))
                {
                    errorProvider1.SetError(NombretextBox, "Ingrese un nombre");
                    NombretextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(TelefonotextBox.Text))
                {
                    errorProvider1.SetError(TelefonotextBox, "Ingrese un numero de telefono");
                    TelefonotextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(CorreotextBox.Text))
                {
                    errorProvider1.SetError(CorreotextBox, "Ingrese un correo electronico");
                    CorreotextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(DirecciontextBox.Text))
                {
                    errorProvider1.SetError(DirecciontextBox, "Ingrese una Direccion");
                    DirecciontextBox.Focus();
                    return;
                }

                errorProvider1.Clear();
                user.Identidad = IdentidadtextBox.Text;
                user.Nombre = NombretextBox.Text;
                user.Telefono = TelefonotextBox.Text;
                user.Correo = CorreotextBox.Text;
                user.Direccion = DirecciontextBox.Text;
                user.FechaNacimiento = FechaNacimientodateTimePicker.Value;
                user.EstaActivo = EstaActivocheckBox.Checked;


                bool inserto = ClienteDB.Insertar(user);

                if (inserto)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                    TraerClientes();
                    MessageBox.Show("Registro Guardado");
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el registro");
                }
            }
            else if (tipoOperacion == "Modificar")
            {
                user.Identidad = IdentidadtextBox.Text;
                user.Nombre = NombretextBox.Text;
                user.Telefono = TelefonotextBox.Text;
                user.Correo = CorreotextBox.Text;
                user.Direccion = DirecciontextBox.Text;
                user.FechaNacimiento = FechaNacimientodateTimePicker.Value;
                user.EstaActivo = EstaActivocheckBox.Checked;


                bool modifico = ClienteDB.Editar(user);
                if (modifico)
                {
                    LimpiarControles();
                    DeshabilitarControles();
                    TraerClientes();
                    MessageBox.Show("Registro actualizado correctamente");
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el registro");
                }

            }
        }

            

        

        
        
        private void TraerClientes()
        {
            dt = ClienteDB.DevolverClientes();

            ClientesdataGridView.DataSource = dt;

        }

        private void Modificarbutton_Click(object sender, EventArgs e)
        {
            TraerClientes();

            tipoOperacion = "Modificar";
            if (ClientesdataGridView.SelectedRows.Count > 0)
            {
                IdentidadtextBox.Text = ClientesdataGridView.CurrentRow.Cells["Identidad"].Value.ToString();
                NombretextBox.Text = ClientesdataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                TelefonotextBox.Text = ClientesdataGridView.CurrentRow.Cells["Telefono"].Value.ToString();
                CorreotextBox.Text = ClientesdataGridView.CurrentRow.Cells["Correo"].Value.ToString();
                DirecciontextBox.Text = ClientesdataGridView.CurrentRow.Cells["Direccion"].Value.ToString();
                EstaActivocheckBox.Checked = Convert.ToBoolean(ClientesdataGridView.CurrentRow.Cells["EstaActivo"].Value);

                HabilitarControles();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            if (ClientesdataGridView.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("Esta seguro de eliminar el registro", "Advertencia", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    bool elimino = ClienteDB.Eliminar(ClientesdataGridView.CurrentRow.Cells["Identidad"].Value.ToString());

                    if (elimino)
                    {
                        LimpiarControles();
                        DeshabilitarControles();
                        TraerClientes();
                        MessageBox.Show("Registro eliminado");
                    }
                    else
                    { MessageBox.Show("No se pudo eliminar el registro"); }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }
    }
}
