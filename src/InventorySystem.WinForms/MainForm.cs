using InventorySystem.WinForms.Services;
using InventorySystem.WinForms.Controls;
using InventorySystem.Shared;

namespace InventorySystem.WinForms;

public partial class MainForm : Form
{
    private readonly IInventoryService _inventoryService;
    private User _currentUser;

    public MainForm(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        InitializeComponent();
        
        // Match window icon with application (EXE) icon
        try {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        } catch { /* Fallback to default if extraction fails */ }
    }

    public void SetUser(User user)
    {
        _currentUser = user;
        ApplyUserPermissions();
        
        // Load default screen
        ShowControl(new UCPos(_inventoryService));
    }

    private void ApplyUserPermissions()
    {
        if (_currentUser.Role == UserRole.Seller)
        {
            // If user is a seller, only POS is visible
            btnInventory.Visible = false;
            btnDailySales.Visible = false;
            btnReceiveStock.Visible = false;
            btnUsers.Visible = false;
            
            // Re-position btnPos if needed or just leave it
        }
        // Admin sees everything (default)
    }

    private void ShowControl(UserControl control)
    {
        pnlContent.Controls.Clear();
        control.Dock = DockStyle.Fill;
        pnlContent.Controls.Add(control);
    }

    private void btnPos_Click(object sender, EventArgs e)
    {
        ShowControl(new UCPos(_inventoryService));
    }

    private void btnInventory_Click(object sender, EventArgs e)
    {
        ShowControl(new UCInventory(_inventoryService));
    }

    private void btnDailySales_Click(object sender, EventArgs e)
    {
        ShowControl(new UCDailySales(_inventoryService));
    }

    private void btnReceiveStock_Click(object sender, EventArgs e)
    {
        ShowControl(new UCReceiveStock(_inventoryService));
    }

    private void btnUsers_Click(object sender, EventArgs e)
    {
        ShowControl(new UCUserManagement(_inventoryService));
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        var result = MessageBox.Show("¿Está seguro de que desea cerrar la sesión?", "Confirmar Cierre de Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        
        if (result == DialogResult.Yes)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        var result = MessageBox.Show("¿Está seguro de que desea salir de la aplicación?", "Confirmar Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        
        if (result == DialogResult.Yes)
        {
            Application.Exit();
        }
    }
}
