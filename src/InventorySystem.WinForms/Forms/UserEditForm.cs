using InventorySystem.Shared;

namespace InventorySystem.WinForms.Forms;

public class UserEditForm : Form
{
    private readonly User _user;
    private readonly bool _isEdit;
    
    private TextBox txtUsername;
    private TextBox txtPin;
    private ComboBox cmbRole;
    private Button btnSave;
    private Button btnCancel;

    public User User => _user;

    public UserEditForm(User? user = null)
    {
        _user = user ?? new User { Id = Guid.NewGuid() };
        _isEdit = user != null;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = _isEdit ? "Editar Usuario" : "Nuevo Usuario";
        this.Size = new Size(350, 300);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterParent;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        var lblUsername = new Label { Text = "Usuario:", Location = new Point(30, 20), AutoSize = true };
        txtUsername = new TextBox { 
            Location = new Point(30, 45), 
            Size = new Size(270, 25), 
            Text = _user.Username 
        };

        var lblPin = new Label { 
            Text = _isEdit ? "Nuevo PIN (dejar vacío para no cambiar):" : "PIN (4-6 dígitos):", 
            Location = new Point(30, 80), 
            AutoSize = true 
        };
        txtPin = new TextBox { 
            Location = new Point(30, 105), 
            Size = new Size(270, 25), 
            PasswordChar = '*' 
        };

        var lblRole = new Label { Text = "Rol:", Location = new Point(30, 140), AutoSize = true };
        cmbRole = new ComboBox { 
            Location = new Point(30, 165), 
            Size = new Size(270, 25), 
            DropDownStyle = ComboBoxStyle.DropDownList 
        };
        cmbRole.Items.AddRange(Enum.GetNames(typeof(UserRole)));
        cmbRole.SelectedItem = _user.Role.ToString();

        btnSave = new Button { 
            Text = "Guardar", 
            Location = new Point(140, 210), 
            Size = new Size(80, 35), 
            BackColor = Color.FromArgb(0, 122, 204),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnSave.Click += BtnSave_Click;

        btnCancel = new Button { 
            Text = "Cancelar", 
            Location = new Point(230, 210), 
            Size = new Size(80, 35),
            FlatStyle = FlatStyle.Flat
        };
        btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

        this.Controls.AddRange(new Control[] { lblUsername, txtUsername, lblPin, txtPin, lblRole, cmbRole, btnSave, btnCancel });
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            MessageBox.Show("El nombre de usuario es requerido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string pin = txtPin.Text.Trim();
        if (!_isEdit || !string.IsNullOrEmpty(pin))
        {
            if (pin.Length < 4 || pin.Length > 6 || !pin.All(char.IsDigit))
            {
                MessageBox.Show("El PIN debe ser numérico y tener entre 4 y 6 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _user.PasswordPin = SecurityService.Encrypt(pin);
        }

        _user.Username = txtUsername.Text.Trim();
        _user.Role = (UserRole)Enum.Parse(typeof(UserRole), cmbRole.SelectedItem.ToString()!);

        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}
