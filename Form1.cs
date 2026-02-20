using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatosEmpleados
{

    public partial class Form1 : Form
    {

        private readonly string[] CargosGenericos = new string[]
     {
            "Seleccione un cargo...",
            "Gerente General",
            "Director de Proyectos",
            "Coordinador Administrativo",
            "Analista de Sistemas",
            "Desarrollador Senior",
            "Desarrollador Junior",
            "Asistente Administrativo",
            "Contador",
            "Recursos Humanos",
            "Ventas",
            "Marketing",
            "Servicio al Cliente",
            "Mantenimiento",
            "Seguridad"
     };
        private readonly string[] Departamentos = new string[]
        {
            "Seleccione un departamento...",
            "Tecnología",
            "Administración",
            "Ventas",
            "Marketing",
            "Recursos Humanos",
            "Contabilidad",
            "Operaciones",
            "Mantenimiento"
        };

        public Form1()
        {
            InitializeComponent();
            ConfigurarControles();


        }
        private void ConfigurarControles()
        {
            // Configurar ComboBox de cargos
            cmbCargo.Items.AddRange(CargosGenericos);
            cmbCargo.SelectedIndex = 0;
            cmbCargo.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configurar ComboBox de departamentos
            cmbDepartamento.Items.AddRange(Departamentos);
            cmbDepartamento.SelectedIndex = 0;
            cmbDepartamento.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configurar validaciones en tiempo real
            txtSalario.KeyPress += TxtSalario_KeyPress;
            txtTelefono.KeyPress += TxtTelefono_KeyPress;
            txtID.KeyPress += TxtID_KeyPress;
        }


        private bool ValidarCampos()
        { // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("El campo ID es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El campo Apellido es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("El campo Dirección es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("El campo Email es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSalario.Text))
            {
                MessageBox.Show("El campo Salario es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSalario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text) || txtTelefono.Text.Length < 8)
            {
                MessageBox.Show("El campo Teléfono es obligatorio (mínimo 8 dígitos).", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }

            // Validar selección de cargo
            if (cmbCargo.SelectedIndex <= 0)
            {
                MessageBox.Show("Por favor, seleccione un cargo válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCargo.Focus();
                return false;
            }

            // Validar selección de departamento
            if (cmbDepartamento.SelectedIndex <= 0)
            {
                MessageBox.Show("Por favor, seleccione un departamento válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDepartamento.Focus();
                return false;
            }

            // Validar formato de ID (solo números)
            if (!Regex.IsMatch(txtID.Text, @"^\d+$"))
            {
                MessageBox.Show("El ID debe contener solo números.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtID.Focus();
                return false;
            }

            // Validar formato de nombre (solo letras)
            if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-ZáéíóúñÑ\s]+$"))
            {
                MessageBox.Show("El Nombre debe contener solo letras.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            // Validar formato de apellido (solo letras)
            if (!Regex.IsMatch(txtApellido.Text, @"^[a-zA-ZáéíóúñÑ\s]+$"))
            {
                MessageBox.Show("El Apellido debe contener solo letras.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            // Validar formato de email
            if (!ValidarEmail(txtEmail.Text))
            {
                MessageBox.Show("El formato del email no es válido.\nEjemplo: usuario@dominio.com",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validar salario (valor numérico y positivo)
            decimal salario;
            if (!decimal.TryParse(txtSalario.Text, out salario) || salario <= 0)
            {
                MessageBox.Show("El Salario debe ser un número positivo.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSalario.Focus();
                return false;
            }

            // Validar que el salario no sea excesivamente alto
            if (salario > 1000000)
            {
                MessageBox.Show("El Salario no puede ser mayor a 1,000,000.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSalario.Focus();
                return false;
            }

            // Validar fecha de ingreso (no puede ser futura)
            if (dtpFechaIngreso.Value > DateTime.Now)
            {
                MessageBox.Show("La fecha de ingreso no puede ser futura.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaIngreso.Focus();
                return false;
            }

            // Validar fecha de ingreso (no puede ser muy antigua)
            if (dtpFechaIngreso.Value < DateTime.Now.AddYears(-50))
            {
                MessageBox.Show("La fecha de ingreso no es válida.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaIngreso.Focus();
                return false;
            }

            return true;
        }

        private bool ValidarEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void TxtSalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal y tecla de borrar
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Permitir solo un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números y tecla de borrar
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números y tecla de borrar
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // VALIDACIÓN AL SALIR DE CAMPOS
        private void TxtNombre_Leave(object sender, EventArgs e)
        {
            // Capitalizar primera letra
            if (!string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                txtNombre.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo
                    .ToTitleCase(txtNombre.Text.ToLower());
            }
        }

        private void TxtApellido_Leave(object sender, EventArgs e)
        {
            // Capitalizar primera letra
            if (!string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                txtApellido.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo
                    .ToTitleCase(txtApellido.Text.ToLower());
            }
        }

        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            // Convertir email a minúsculas
            txtEmail.Text = txtEmail.Text.ToLower().Trim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
       private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Archivo de texto (*.txt)|*.txt";
            saveDialog.Title = "Guardar Datos del Empleado";
            saveDialog.FileName = "Empleado_" + txtID.Text;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                    {
                        // Definir anchos de columna
                        int[] anchos = { 8, 15, 15, 25, 25, 12, 10, 15, 15, 12, 12 };

                        // ENCABEZADOS
                        writer.WriteLine(
                            $"{"ID".PadRight(anchos[0])} | " +
                            $"{"NOMBRE".PadRight(anchos[1])} | " +
                            $"{"APELLIDOS".PadRight(anchos[2])} | " +
                            $"{"DIRECCIÓN".PadRight(anchos[3])} | " +
                            $"{"EMAIL".PadRight(anchos[4])} | " +
                            $"{"TELÉFONO".PadRight(anchos[5])} | " +
                            $"{"GÉNERO".PadRight(anchos[6])} | " +
                            $"{"CARGO".PadRight(anchos[7])} | " +
                            $"{"DEPARTAMENTO".PadRight(anchos[8])} | " +
                            $"{"SALARIO".PadRight(anchos[9])} | " +
                            $"{"F.INGRESO".PadRight(anchos[10])}"
                        );

                        // LÍNEA SEPARADORA
                        int total = anchos.Sum() + (11 * 3); // anchos + (cantidad de | * 3)
                        writer.WriteLine(new string('-', total));

                        // DATOS
                        writer.WriteLine(
                            $"{txtID.Text.PadRight(anchos[0])} | " +
                            $"{txtNombre.Text.PadRight(anchos[1])} | " +
                            $"{txtApellido.Text.PadRight(anchos[2])} | " +
                            $"{txtDireccion.Text.PadRight(anchos[3])} | " +
                            $"{txtEmail.Text.PadRight(anchos[4])} | " +
                            $"{txtTelefono.Text.PadRight(anchos[5])} | " +
                            $"{cmbGenero.Text.PadRight(anchos[6])} | " +
                            $"{cmbCargo.Text.PadRight(anchos[7])} | " +
                            $"{cmbDepartamento.Text.PadRight(anchos[8])} | " +
                            $"{decimal.Parse(txtSalario.Text):C2}".PadRight(anchos[9]) + " | " +
                            $"{dtpFechaIngreso.Value.ToShortDateString().PadRight(anchos[10])}"
                        );
                    }

                    MessageBox.Show("Archivo guardado correctamente en formato tabla.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message);
                }
            }
        }
    

      
    private void btnAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Archivos de texto (*.txt)|*.txt|Archivos CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Diagnostics.Process.Start("notepad.exe", openDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir el archivo: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Desea salir de la aplicación?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de limpiar todos los campos?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                txtID.Clear();
                txtNombre.Clear();
                txtApellido.Clear();
                txtDireccion.Clear();
                txtEmail.Clear();
                txtTelefono.Clear();
                txtSalario.Clear();

                cmbGenero.SelectedIndex = 0;
                cmbCargo.SelectedIndex = 0;
                cmbDepartamento.SelectedIndex = 0;

                dtpFechaIngreso.Value = DateTime.Now;

                txtID.Focus();
            }
        }

        private void cmbGenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
