using InventorySystem.Shared;
using InventorySystem.WinForms.Services;
using InventorySystem.WinForms.Forms;

namespace InventorySystem.WinForms.Controls;

public class UCReceiveStock : UserControl
{
    private readonly IInventoryService _inventoryService;
    private Product? _selectedProduct;
    private StockMovement _movement = new();
    
    private TextBox txtBarcode;
    private Label lblProductName;
    private Label lblCurrentStock;
    private NumericUpDown numQty;
    private NumericUpDown numCost;
    private NumericUpDown numTransport;
    private DateTimePicker dtpExpiry;
    private TextBox txtNotes;
    private Button btnSave;
    private Button btnAddNewProduct;
    private Label lblStatus;

    public UCReceiveStock(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.txtBarcode = new TextBox();
        this.lblProductName = new Label();
        this.lblCurrentStock = new Label();
        this.numQty = new NumericUpDown();
        this.numCost = new NumericUpDown();
        this.numTransport = new NumericUpDown();
        this.dtpExpiry = new DateTimePicker();
        this.txtNotes = new TextBox();
        this.btnSave = new Button();
        this.btnAddNewProduct = new Button();
        this.lblStatus = new Label();
        
        var lblB = new Label { Text = "Scan Barcode:", Location = new Point(20, 20), AutoSize = true };
        txtBarcode.Location = new Point(20, 45);
        txtBarcode.Size = new Size(300, 30);
        txtBarcode.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) SearchProduct(); };
        
        lblProductName.Location = new Point(20, 90);
        lblProductName.Size = new Size(400, 30);
        lblProductName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblProductName.Text = "No product selected";

        lblCurrentStock.Location = new Point(20, 120);
        lblCurrentStock.Size = new Size(400, 30);

        // btnAddNewProduct
        btnAddNewProduct.Text = "Create New Product?";
        btnAddNewProduct.Location = new Point(20, 150);
        btnAddNewProduct.Size = new Size(180, 30);
        btnAddNewProduct.BackColor = Color.DodgerBlue;
        btnAddNewProduct.ForeColor = Color.White;
        btnAddNewProduct.FlatStyle = FlatStyle.Flat;
        btnAddNewProduct.Visible = false; // Hidden by default
        btnAddNewProduct.Click += (s, e) => ShowAddForm();

        var lblQ = new Label { Text = "Quantity to Add:", Location = new Point(20, 190), AutoSize = true };
        numQty.Location = new Point(20, 215);
        numQty.Size = new Size(120, 25);
        numQty.Maximum = 100000;

        var lblC = new Label { Text = "Unit Cost (Base):", Location = new Point(20, 250), AutoSize = true };
        numCost.Location = new Point(20, 275);
        numCost.Size = new Size(120, 25);
        numCost.DecimalPlaces = 2;
        numCost.Maximum = 100000;

        var lblT = new Label { Text = "Transportation Cost (Per Unit):", Location = new Point(20, 310), AutoSize = true };
        numTransport.Location = new Point(20, 335);
        numTransport.Size = new Size(120, 25);
        numTransport.DecimalPlaces = 2;
        numTransport.Maximum = 1000;

        var lblE = new Label { Text = "Expiration Date:", Location = new Point(20, 370), AutoSize = true };
        dtpExpiry.Location = new Point(20, 395);
        dtpExpiry.Size = new Size(200, 25);
        dtpExpiry.Format = DateTimePickerFormat.Short;

        var lblN = new Label { Text = "Notes:", Location = new Point(20, 430), AutoSize = true };
        txtNotes.Location = new Point(20, 455);
        txtNotes.Size = new Size(400, 60);
        txtNotes.Multiline = true;

        btnSave.Text = "Receive Stock";
        btnSave.Location = new Point(20, 520);
        btnSave.Size = new Size(150, 45);
        btnSave.BackColor = Color.ForestGreen;
        btnSave.ForeColor = Color.White;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Click += (s, e) => SaveMovement();

        lblStatus.Location = new Point(20, 570);
        lblStatus.Size = new Size(400, 30);

        this.Controls.AddRange(new Control[] { lblB, txtBarcode, lblProductName, lblCurrentStock, btnAddNewProduct, lblQ, numQty, lblC, numCost, lblT, numTransport, lblE, dtpExpiry, lblN, txtNotes, btnSave, lblStatus });
        this.Name = "UCReceiveStock";
        this.Size = new Size(800, 610);
    }

    private async void SearchProduct()
    {
        string barcode = txtBarcode.Text.Trim();
        if (string.IsNullOrEmpty(barcode)) return;

        _selectedProduct = await _inventoryService.GetProductByBarcodeAsync(barcode);
        if (_selectedProduct != null)
        {
            lblProductName.Text = _selectedProduct.Name;
            lblCurrentStock.Text = $"Current Stock: {_selectedProduct.CurrentStock} | Avg Cost: {_selectedProduct.AverageCost:C}";
            numCost.Value = _selectedProduct.AverageCost;
            lblStatus.Text = "";
            btnAddNewProduct.Visible = false;
        }
        else
        {
            lblProductName.Text = "Product not found!";
            lblCurrentStock.Text = "";
            lblStatus.Text = "Scanning barcode... not in system.";
            lblStatus.ForeColor = Color.Orange;
            btnAddNewProduct.Visible = true;
        }
    }

    private void ShowAddForm()
    {
        string barcode = txtBarcode.Text.Trim();
        using (var form = new ProductEditForm(new Product { Barcode = barcode }))
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                SaveNewProduct(form.Product);
            }
        }
    }

    private async void SaveNewProduct(Product product)
    {
        try
        {
            await _inventoryService.CreateProductAsync(product);
            lblStatus.Text = "New product created successfully!";
            lblStatus.ForeColor = Color.Green;
            btnAddNewProduct.Visible = false;
            
            // Re-search to select it
            _selectedProduct = await _inventoryService.GetProductByBarcodeAsync(product.Barcode);
            if (_selectedProduct != null)
            {
                lblProductName.Text = _selectedProduct.Name;
                lblCurrentStock.Text = $"Current Stock: {_selectedProduct.CurrentStock} | Avg Cost: {_selectedProduct.AverageCost:C}";
                numCost.Value = _selectedProduct.AverageCost;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error creating product: {ex.Message}");
        }
    }

    private async void SaveMovement()
    {
        if (_selectedProduct == null) return;

        try
        {
            var movement = new StockMovement
            {
                ProductId = _selectedProduct.Id,
                Quantity = (int)numQty.Value,
                UnitCost = numCost.Value,
                TransportationCost = numTransport.Value,
                ExpirationDate = dtpExpiry.Value,
                Type = MovementType.In,
                Date = DateTime.Now,
                Notes = txtNotes.Text
            };

            await _inventoryService.CreateMovementAsync(movement);
            
            lblStatus.Text = "Stock received successfully!";
            lblStatus.ForeColor = Color.Green;
            
            // Reset
            _selectedProduct = null;
            txtBarcode.Clear();
            numQty.Value = 0;
            numCost.Value = 0;
            numTransport.Value = 0;
            txtNotes.Clear();
            lblProductName.Text = "No product selected";
            lblCurrentStock.Text = "";
            btnAddNewProduct.Visible = false;
            txtBarcode.Focus();
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Error: {ex.Message}";
            lblStatus.ForeColor = Color.Red;
        }
    }
}
