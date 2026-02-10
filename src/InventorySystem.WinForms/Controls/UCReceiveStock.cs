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
    private NumericUpDown numQty;
    private NumericUpDown numCost;
    private NumericUpDown numTransport;
    private DateTimePicker dtpExpiry;
    private TextBox txtNotes;
    private Button btnSave;
    private Button btnAddNewProduct;
    private Label lblB;
    private Label lblQ;
    private Label lblC;
    private Label lblT;
    private Label lblE;
    private Label lblN;
    private Label lblBarcode;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label lblCurrentStock;
    private Label lblStatus;

    public UCReceiveStock(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        txtBarcode = new TextBox();
        lblProductName = new Label();
        numQty = new NumericUpDown();
        numCost = new NumericUpDown();
        numTransport = new NumericUpDown();
        dtpExpiry = new DateTimePicker();
        txtNotes = new TextBox();
        btnSave = new Button();
        btnAddNewProduct = new Button();
        lblStatus = new Label();
        lblB = new Label();
        lblQ = new Label();
        lblC = new Label();
        lblT = new Label();
        lblE = new Label();
        lblN = new Label();
        lblBarcode = new Label();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        label4 = new Label();
        label5 = new Label();
        lblCurrentStock = new Label();
        ((System.ComponentModel.ISupportInitialize)numQty).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numCost).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numTransport).BeginInit();
        SuspendLayout();
        // 
        // txtBarcode
        // 
        txtBarcode.Location = new Point(20, 45);
        txtBarcode.Name = "txtBarcode";
        txtBarcode.Size = new Size(300, 27);
        txtBarcode.TabIndex = 1;
        txtBarcode.KeyDown += TxtBarcode_KeyDown;
        // 
        // lblProductName
        // 
        lblProductName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblProductName.Location = new Point(20, 90);
        lblProductName.Name = "lblProductName";
        lblProductName.Size = new Size(400, 30);
        lblProductName.TabIndex = 2;
        lblProductName.Text = "Ningun Producto Seleccionado";
        // 
        // numQty
        // 
        numQty.Location = new Point(20, 215);
        numQty.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        numQty.Name = "numQty";
        numQty.Size = new Size(200, 27);
        numQty.TabIndex = 6;
        // 
        // numCost
        // 
        numCost.DecimalPlaces = 2;
        numCost.Location = new Point(236, 215);
        numCost.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        numCost.Name = "numCost";
        numCost.Size = new Size(184, 27);
        numCost.TabIndex = 8;
        // 
        // numTransport
        // 
        numTransport.DecimalPlaces = 2;
        numTransport.Location = new Point(456, 215);
        numTransport.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numTransport.Name = "numTransport";
        numTransport.Size = new Size(200, 27);
        numTransport.TabIndex = 10;
        // 
        // dtpExpiry
        // 
        dtpExpiry.Format = DateTimePickerFormat.Short;
        dtpExpiry.Location = new Point(686, 215);
        dtpExpiry.Name = "dtpExpiry";
        dtpExpiry.Size = new Size(200, 27);
        dtpExpiry.TabIndex = 12;
        // 
        // txtNotes
        // 
        txtNotes.Location = new Point(20, 290);
        txtNotes.Multiline = true;
        txtNotes.Name = "txtNotes";
        txtNotes.Size = new Size(866, 60);
        txtNotes.TabIndex = 14;
        // 
        // btnSave
        // 
        btnSave.BackColor = Color.ForestGreen;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(20, 368);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(866, 45);
        btnSave.TabIndex = 15;
        btnSave.Text = "Recibir Producto";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += BtnSave_Click;
        // 
        // btnAddNewProduct
        // 
        btnAddNewProduct.BackColor = Color.DodgerBlue;
        btnAddNewProduct.FlatStyle = FlatStyle.Flat;
        btnAddNewProduct.ForeColor = Color.White;
        btnAddNewProduct.Location = new Point(20, 134);
        btnAddNewProduct.Name = "btnAddNewProduct";
        btnAddNewProduct.Size = new Size(180, 30);
        btnAddNewProduct.TabIndex = 4;
        btnAddNewProduct.Text = "Crear Nuevo Producto";
        btnAddNewProduct.UseVisualStyleBackColor = false;
        btnAddNewProduct.Visible = false;
        btnAddNewProduct.Click += BtnAddNewProduct_Click;
        // 
        // lblStatus
        // 
        lblStatus.Location = new Point(20, 570);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(400, 30);
        lblStatus.TabIndex = 16;
        // 
        // lblB
        // 
        lblB.Location = new Point(0, 0);
        lblB.Name = "lblB";
        lblB.Size = new Size(100, 23);
        lblB.TabIndex = 0;
        // 
        // lblQ
        // 
        lblQ.Location = new Point(0, 0);
        lblQ.Name = "lblQ";
        lblQ.Size = new Size(100, 23);
        lblQ.TabIndex = 5;
        // 
        // lblC
        // 
        lblC.Location = new Point(0, 0);
        lblC.Name = "lblC";
        lblC.Size = new Size(100, 23);
        lblC.TabIndex = 7;
        // 
        // lblT
        // 
        lblT.Location = new Point(0, 0);
        lblT.Name = "lblT";
        lblT.Size = new Size(100, 23);
        lblT.TabIndex = 9;
        // 
        // lblE
        // 
        lblE.Location = new Point(0, 0);
        lblE.Name = "lblE";
        lblE.Size = new Size(100, 23);
        lblE.TabIndex = 11;
        // 
        // lblN
        // 
        lblN.Location = new Point(0, 0);
        lblN.Name = "lblN";
        lblN.Size = new Size(100, 23);
        lblN.TabIndex = 13;
        // 
        // lblBarcode
        // 
        lblBarcode.AutoSize = true;
        lblBarcode.Location = new Point(20, 22);
        lblBarcode.Name = "lblBarcode";
        lblBarcode.Size = new Size(129, 20);
        lblBarcode.TabIndex = 17;
        lblBarcode.Text = "Escanear Barcode:";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(20, 192);
        label1.Name = "label1";
        label1.Size = new Size(69, 20);
        label1.TabIndex = 18;
        label1.Text = "Cantidad";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(236, 192);
        label2.Name = "label2";
        label2.Size = new Size(116, 20);
        label2.TabIndex = 19;
        label2.Text = "Precio de Costo:";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(456, 192);
        label3.Name = "label3";
        label3.Size = new Size(176, 20);
        label3.TabIndex = 20;
        label3.Text = "Precio de Transportacion:";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(686, 192);
        label4.Name = "label4";
        label4.Size = new Size(144, 20);
        label4.TabIndex = 21;
        label4.Text = "Fecha de Expiracion:";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(20, 267);
        label5.Name = "label5";
        label5.Size = new Size(135, 20);
        label5.TabIndex = 22;
        label5.Text = "Notas/Descripcion:";
        // 
        // lblCurrentStock
        // 
        lblCurrentStock.Location = new Point(326, 45);
        lblCurrentStock.Name = "lblCurrentStock";
        lblCurrentStock.Size = new Size(400, 30);
        lblCurrentStock.TabIndex = 3;
        lblCurrentStock.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // UCReceiveStock
        // 
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(lblBarcode);
        Controls.Add(lblB);
        Controls.Add(txtBarcode);
        Controls.Add(lblProductName);
        Controls.Add(lblCurrentStock);
        Controls.Add(btnAddNewProduct);
        Controls.Add(lblQ);
        Controls.Add(numQty);
        Controls.Add(lblC);
        Controls.Add(numCost);
        Controls.Add(lblT);
        Controls.Add(numTransport);
        Controls.Add(lblE);
        Controls.Add(dtpExpiry);
        Controls.Add(lblN);
        Controls.Add(txtNotes);
        Controls.Add(btnSave);
        Controls.Add(lblStatus);
        Name = "UCReceiveStock";
        Size = new Size(903, 441);
        ((System.ComponentModel.ISupportInitialize)numQty).EndInit();
        ((System.ComponentModel.ISupportInitialize)numCost).EndInit();
        ((System.ComponentModel.ISupportInitialize)numTransport).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private void TxtBarcode_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SearchProduct();
        }
    }

    private void BtnAddNewProduct_Click(object? sender, EventArgs e)
    {
        ShowAddForm();
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        SaveMovement();
    }

    private async void SearchProduct()
    {
        string barcode = txtBarcode.Text.Trim();
        if (string.IsNullOrEmpty(barcode)) return;

        _selectedProduct = await _inventoryService.GetProductByBarcodeAsync(barcode);
        if (_selectedProduct != null)
        {
            lblProductName.Text = _selectedProduct.Name;
            lblCurrentStock.Text = $"Cantidad Actual: {_selectedProduct.CurrentStock} | Costo Promedio: {_selectedProduct.AverageCost:C}";
            numCost.Value = _selectedProduct.AverageCost;
            lblStatus.Text = "";
            btnAddNewProduct.Visible = false;
        }
        else
        {
            lblProductName.Text = "Producto no encontrado!";
            lblCurrentStock.Text = "";
            lblStatus.Text = "Escaneando Barcode... producto no encontrado en el sistema.";
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
            lblStatus.Text = "Nuevo producto creado!";
            lblStatus.ForeColor = Color.Green;
            btnAddNewProduct.Visible = false;

            // Re-search to select it
            _selectedProduct = await _inventoryService.GetProductByBarcodeAsync(product.Barcode);
            if (_selectedProduct != null)
            {
                lblProductName.Text = _selectedProduct.Name;
                lblCurrentStock.Text = $"Cantidad Actual: {_selectedProduct.CurrentStock} | Costo Promedio: {_selectedProduct.AverageCost:C}";
                numCost.Value = _selectedProduct.AverageCost;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error creando producto: {ex.Message}");
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

            lblStatus.Text = "Producto recibido satisfactoriamente!";
            lblStatus.ForeColor = Color.Green;

            // Reset
            _selectedProduct = null;
            txtBarcode.Clear();
            numQty.Value = 0;
            numCost.Value = 0;
            numTransport.Value = 0;
            txtNotes.Clear();
            lblProductName.Text = "No producto seleccionado";
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