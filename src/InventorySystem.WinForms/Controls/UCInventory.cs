using InventorySystem.Shared;
using InventorySystem.WinForms.Services;
using InventorySystem.WinForms.Forms;

namespace InventorySystem.WinForms.Controls;

public class UCInventory : UserControl
{
    private readonly IInventoryService _inventoryService;
    private IEnumerable<Product>? _products;
    
    private DataGridView dgvProducts;
    private Button btnRefresh;
    private Button btnAdd;
    private Label lblStatus;

    public UCInventory(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        InitializeComponent();
        LoadProducts();
    }

    private void InitializeComponent()
    {
        this.dgvProducts = new DataGridView();
        this.btnRefresh = new Button();
        this.btnAdd = new Button();
        this.lblStatus = new Label();
        
        this.SuspendLayout();

        // btnRefresh
        this.btnRefresh.Text = "Refresh";
        this.btnRefresh.Location = new Point(20, 20);
        this.btnRefresh.Size = new Size(100, 35);
        this.btnRefresh.Click += (s, e) => LoadProducts();

        // btnAdd
        this.btnAdd.Text = "+ Add New Product";
        this.btnAdd.Location = new Point(130, 20);
        this.btnAdd.Size = new Size(160, 35);
        this.btnAdd.BackColor = Color.DodgerBlue;
        this.btnAdd.ForeColor = Color.White;
        this.btnAdd.FlatStyle = FlatStyle.Flat;
        this.btnAdd.Click += (s, e) => ShowAddForm();

        // dgvProducts
        this.dgvProducts.Location = new Point(20, 70);
        this.dgvProducts.Size = new Size(740, 480);
        this.dgvProducts.AllowUserToAddRows = false;
        this.dgvProducts.ReadOnly = true;
        this.dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvProducts.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) ShowEditForm(); };

        // lblStatus
        this.lblStatus.Location = new Point(20, 560);
        this.lblStatus.Size = new Size(740, 25);
        this.lblStatus.Text = "Double-click a row to edit price.";

        this.Controls.Add(btnRefresh);
        this.Controls.Add(btnAdd);
        this.Controls.Add(dgvProducts);
        this.Controls.Add(lblStatus);

        this.Name = "UCInventory";
        this.Size = new Size(800, 600);
        this.ResumeLayout(false);
    }

    private async void LoadProducts()
    {
        try
        {
            _products = await _inventoryService.GetProductsAsync();
            dgvProducts.DataSource = _products.ToList();
            
            // Customize columns
            if (dgvProducts.Columns["Id"] != null) dgvProducts.Columns["Id"].Visible = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading products: {ex.Message}");
        }
    }

    private void ShowAddForm()
    {
        using (var form = new ProductEditForm())
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                SaveProduct(form.Product, false);
            }
        }
    }

    private void ShowEditForm()
    {
        if (dgvProducts.SelectedRows.Count == 0) return;
        var p = (Product)dgvProducts.SelectedRows[0].DataBoundItem;
        
        using (var form = new ProductEditForm(p))
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                SaveProduct(form.Product, true);
            }
        }
    }

    private async void SaveProduct(Product product, bool isEdit)
    {
        try
        {
            if (isEdit)
            {
                await _inventoryService.UpdateProductAsync(product);
            }
            else
            {
                await _inventoryService.CreateProductAsync(product);
            }
            LoadProducts();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving product: {ex.Message}");
        }
    }
}
