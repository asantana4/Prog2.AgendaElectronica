using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Prog2.AgendaElectronica.Business;
using Prog2.AgendaElectronica.Core;

namespace Prog2.AgendaElectronica.UI
{
    public partial class Form1 : Form
    {
        private ContactoService _servicio;
        private int _idContactoSeleccionado = 0;

        public Form1()
        {
            InitializeComponent();
            _servicio = new ContactoService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void CargarGrid()
        {
            dgvContactos.DataSource = _servicio.ObtenerContactos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Contacto contacto = new Contacto
                {
                    Id = _idContactoSeleccionado, // Aquí se asigna el ID (0 si
                                                  // es nuevo, >0 si es modificación)
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Direccion = txtDireccion.Text,
                    Genero = cmbGenero.Text,
                    EstadoCivil = cmbEstadoCivil.Text,
                    Movil = txtMovil.Text,
                    Telefono = txtTelefono.Text,
                    CorreoElectronico = txtCorreo.Text
                };

                _servicio.GuardarContacto(contacto);

                string accion = _idContactoSeleccionado == 0 ? "guardado" : "modificado";
                MessageBox.Show($"Contacto {accion} exitosamente.");

                CargarGrid();
                LimpiarCampos(); // Método opcional para vaciar los textboxes
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            _idContactoSeleccionado = 0;
            txtNombre.Clear();
            txtApellido.Clear();
            txtDireccion.Clear();
            txtMovil.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            cmbGenero.SelectedIndex = -1;
            cmbEstadoCivil.SelectedIndex = -1;
            dtpFechaNacimiento.Value = DateTime.Now;
        }

        private void dgvContactos_CellDoubleClick(object sender, 
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvContactos.Rows[e.RowIndex];

                // Guardamos el ID en memoria
                _idContactoSeleccionado = Convert.ToInt32(fila.Cells["Id"].Value);

                // Pasamos los datos a los controles
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtApellido.Text = fila.Cells["Apellido"].Value.ToString();
                dtpFechaNacimiento.Value = Convert.ToDateTime(fila.Cells
                    ["FechaNacimiento"].Value);
                txtDireccion.Text = fila.Cells["Direccion"].Value.ToString();
                cmbGenero.Text = fila.Cells["Genero"].Value.ToString();
                cmbEstadoCivil.Text = fila.Cells["EstadoCivil"].Value.ToString();
                txtMovil.Text = fila.Cells["Movil"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["CorreoElectronico"].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un registro
            if (_idContactoSeleccionado == 0)
            {
                MessageBox.Show("Por favor, seleccione un contacto de la lista " +
                    "haciendo doble clic antes de eliminar.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Pedir confirmación al usuario
            DialogResult respuesta = MessageBox.Show("¿Está seguro que desea " +
                "eliminar este contacto de la agenda?", "Confirmar Eliminación", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    // Enviar la orden a la capa de negocio
                    _servicio.BorrarContacto(_idContactoSeleccionado);
                    MessageBox.Show("Contacto eliminado exitosamente.");

                    // Refrescar la tabla y limpiar el formulario
                    CargarGrid();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Pasamos el texto del buscador al método. 
                // Si está vacío, traerá todos los registros. Si tiene texto,
                // filtrará por nombre.
                dgvContactos.DataSource = _servicio.ObtenerContactos(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
    }
}

