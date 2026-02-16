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
        dgvProducts = new DataGridView();
        btnRefresh = new Button();
        btnAdd = new Button();
        lblStatus = new Label();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
        SuspendLayout();
        // 
        // dgvProducts
        // 
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvProducts.BackgroundColor = Color.White;
        dgvProducts.ColumnHeadersHeight = 29;
        dgvProducts.Location = new Point(20, 70);
        dgvProducts.Name = "dgvProducts";
        dgvProducts.ReadOnly = true;
        dgvProducts.RowHeadersWidth = 51;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.Size = new Size(1350, 480);
        dgvProducts.TabIndex = 2;
        dgvProducts.CellDoubleClick += DgvProducts_CellDoubleClick;
        // 
        // btnRefresh
        // 
        btnRefresh.BackColor = Color.Green;
        btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnRefresh.ForeColor = Color.White;
        btnRefresh.Location = new Point(20, 20);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(100, 35);
        btnRefresh.TabIndex = 0;
        btnRefresh.Text = "Actualizar";
        btnRefresh.UseVisualStyleBackColor = false;
        btnRefresh.Click += BtnRefresh_Click;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = Color.DodgerBlue;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(130, 20);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(160, 35);
        btnAdd.TabIndex = 1;
        btnAdd.Text = "+ Agregar Nuevo Producto";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += BtnAdd_Click;
        // 
        // lblStatus
        // 
        lblStatus.Location = new Point(20, 560);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(740, 25);
        lblStatus.TabIndex = 3;
        lblStatus.Text = "Double-click en fila para editar precio de venta.";
        // 
        // UCInventory
        // 
        Controls.Add(btnRefresh);
        Controls.Add(btnAdd);
        Controls.Add(dgvProducts);
        Controls.Add(lblStatus);
        Name = "UCInventory";
        Size = new Size(1391, 600);
        ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
        ResumeLayout(false);
    }

    private void BtnRefresh_Click(object? sender, EventArgs e)
    {
        LoadProducts();
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        ShowAddForm();
    }

    private void DgvProducts_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            ShowEditForm();
        }
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
            MessageBox.Show($"Error cargando productos: {ex.Message}");
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
            MessageBox.Show($"Error salvando producto: {ex.Message}");
        }
    }
}