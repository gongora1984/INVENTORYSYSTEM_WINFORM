using InventorySystem.Infrastructure.Data;
using InventorySystem.Shared;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.WinForms.Forms;

public partial class LoginForm : Form
{
    private readonly AppDbContext _db;
    public User? AuthenticatedUser { get; private set; }

    public LoginForm(AppDbContext db)
    {
        _db = db;
        InitializeComponent();
        
        // Match window icon with application (EXE) icon
        try {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        } catch { /* Fallback to default if extraction fails */ }
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string pin = txtPin.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(pin))
        {
            MessageBox.Show("Please enter both username and PIN.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Validate PIN constraints: numeric, 4-6 digits
        if (!System.Text.RegularExpressions.Regex.IsMatch(pin, @"^\d{4,6}$"))
        {
            MessageBox.Show("PIN must be numeric and between 4 and 6 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string encryptedPin = SecurityService.Encrypt(pin);
        var user = _db.Users.FirstOrDefault(u => u.Username == username && u.PasswordPin == encryptedPin);

        if (user != null)
        {
            AuthenticatedUser = user;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        else
        {
            MessageBox.Show("Invalid username or PIN.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}
