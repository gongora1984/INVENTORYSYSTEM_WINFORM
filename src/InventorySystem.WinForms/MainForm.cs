using InventorySystem.WinForms.Services;
using InventorySystem.WinForms.Controls;

namespace InventorySystem.WinForms;

public partial class MainForm : Form
{
    private readonly IInventoryService _inventoryService;

    public MainForm(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        InitializeComponent();
        
        // Load default screen
        ShowControl(new UCPos(_inventoryService));
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
}
