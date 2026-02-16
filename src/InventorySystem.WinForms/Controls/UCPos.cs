using System.Drawing.Printing;
using InventorySystem.Shared;
using InventorySystem.WinForms.Services;

namespace InventorySystem.WinForms.Controls;

public class UCPos : UserControl
{
    private readonly IInventoryService _inventoryService;
    private List<CartItem> _cart = new();

    // Printing variables
    private string? _lastOrderNumber;
    private List<CartItem>? _lastOrderItems;
    private DateTime _lastOrderDate;
    private decimal _lastOrderTotal;

    private TextBox txtBarcode;
    private DataGridView dgvCart;
    private Label lblTotal;
    private Button btnCheckout;
    private Label lblBarcode;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private Label lblStatus;

    public UCPos(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        txtBarcode = new TextBox();
        dgvCart = new DataGridView();
        dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
        lblTotal = new Label();
        btnCheckout = new Button();
        lblStatus = new Label();
        lblBarcode = new Label();
        ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
        SuspendLayout();
        // 
        // txtBarcode
        // 
        txtBarcode.Font = new Font("Segoe UI", 12F);
        txtBarcode.Location = new Point(20, 45);
        txtBarcode.Name = "txtBarcode";
        txtBarcode.Size = new Size(300, 34);
        txtBarcode.TabIndex = 1;
        txtBarcode.KeyDown += TxtBarcode_KeyDown;
        // 
        // dgvCart
        // 
        dgvCart.AllowUserToAddRows = false;
        dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCart.ColumnHeadersHeight = 29;
        dgvCart.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
        dgvCart.Location = new Point(20, 90);
        dgvCart.Name = "dgvCart";
        dgvCart.RowHeadersWidth = 51;
        dgvCart.Size = new Size(1350, 400);
        dgvCart.TabIndex = 2;
        // 
        // dataGridViewTextBoxColumn1
        // 
        dataGridViewTextBoxColumn1.HeaderText = "Producto";
        dataGridViewTextBoxColumn1.MinimumWidth = 6;
        dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
        // 
        // dataGridViewTextBoxColumn2
        // 
        dataGridViewTextBoxColumn2.HeaderText = "Precio";
        dataGridViewTextBoxColumn2.MinimumWidth = 6;
        dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
        // 
        // dataGridViewTextBoxColumn3
        // 
        dataGridViewTextBoxColumn3.HeaderText = "Cantidad";
        dataGridViewTextBoxColumn3.MinimumWidth = 6;
        dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
        // 
        // dataGridViewTextBoxColumn4
        // 
        dataGridViewTextBoxColumn4.HeaderText = "Costo Total";
        dataGridViewTextBoxColumn4.MinimumWidth = 6;
        dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
        // 
        // lblTotal
        // 
        lblTotal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTotal.Location = new Point(1170, 503);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(200, 30);
        lblTotal.TabIndex = 3;
        lblTotal.Text = "Total: $0.00";
        lblTotal.TextAlign = ContentAlignment.TopRight;
        // 
        // btnCheckout
        // 
        btnCheckout.BackColor = Color.ForestGreen;
        btnCheckout.FlatStyle = FlatStyle.Flat;
        btnCheckout.ForeColor = Color.White;
        btnCheckout.Location = new Point(20, 500);
        btnCheckout.Name = "btnCheckout";
        btnCheckout.Size = new Size(241, 45);
        btnCheckout.TabIndex = 4;
        btnCheckout.Text = "Vender / Imprimir Recibo";
        btnCheckout.UseVisualStyleBackColor = false;
        btnCheckout.Click += BtnCheckout_Click;
        // 
        // lblStatus
        // 
        lblStatus.ForeColor = Color.Red;
        lblStatus.Location = new Point(20, 550);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(740, 30);
        lblStatus.TabIndex = 5;
        // 
        // lblBarcode
        // 
        lblBarcode.AutoSize = true;
        lblBarcode.Location = new Point(20, 20);
        lblBarcode.Name = "lblBarcode";
        lblBarcode.Size = new Size(129, 20);
        lblBarcode.TabIndex = 0;
        lblBarcode.Text = "Escanear Barcode:";
        // 
        // UCPos
        // 
        Controls.Add(lblBarcode);
        Controls.Add(txtBarcode);
        Controls.Add(dgvCart);
        Controls.Add(lblTotal);
        Controls.Add(btnCheckout);
        Controls.Add(lblStatus);
        Name = "UCPos";
        Size = new Size(1553, 600);
        ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private void TxtBarcode_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            HandleBarcode();
        }
    }

    private void BtnCheckout_Click(object? sender, EventArgs e)
    {
        Checkout();
    }

    private async void HandleBarcode()
    {
        string barcode = txtBarcode.Text.Trim();
        if (string.IsNullOrEmpty(barcode)) return;

        lblStatus.Text = "";
        var product = await _inventoryService.GetProductByBarcodeAsync(barcode);

        if (product != null)
        {
            AddToCart(product);
            txtBarcode.Clear();
            txtBarcode.Focus();
        }
        else
        {
            lblStatus.Text = "Product not found!";
            lblStatus.ForeColor = Color.DarkOrange;
        }
    }

    private void AddToCart(Product product)
    {
        var existing = _cart.FirstOrDefault(c => c.Product.Id == product.Id);
        int currentQty = existing?.Quantity ?? 0;

        if (currentQty + 1 > product.CurrentStock)
        {
            lblStatus.Text = $"Insufficient stock for {product.Name}. Available: {product.CurrentStock}";
            lblStatus.ForeColor = Color.Red;
            return;
        }

        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            _cart.Add(new CartItem { Product = product, Quantity = 1 });
        }

        UpdateGrid();
    }

    private void UpdateGrid()
    {
        dgvCart.Rows.Clear();
        decimal total = 0;
        foreach (var item in _cart)
        {
            decimal itemTotal = item.Product.SellingPrice * item.Quantity;
            dgvCart.Rows.Add(item.Product.Name, item.Product.SellingPrice.ToString("C"), item.Quantity, itemTotal.ToString("C"));
            total += itemTotal;
        }
        lblTotal.Text = $"Total: {total.ToString("C")}";
    }

    private async void Checkout()
    {
        if (!_cart.Any()) return;

        try
        {
            _lastOrderDate = DateTime.Now;
            _lastOrderNumber = $"O-{_lastOrderDate:yyyyMMddHHmmss}";
            _lastOrderItems = new List<CartItem>(_cart);
            _lastOrderTotal = _cart.Sum(i => i.Product.SellingPrice * i.Quantity);

            foreach (var item in _cart)
            {
                var movement = new StockMovement
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Type = MovementType.Out,
                    Date = _lastOrderDate,
                    OrderNumber = _lastOrderNumber,
                    Notes = "POS Sale (WinForms)"
                };
                await _inventoryService.CreateMovementAsync(movement);
            }

            _cart.Clear();
            UpdateGrid();
            lblStatus.Text = $"Sale {_lastOrderNumber} completed successfully! Printing...";
            lblStatus.ForeColor = Color.Green;

            PrintInvoice();
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Error: {ex.Message}";
            lblStatus.ForeColor = Color.Red;
        }
    }

    private void PrintInvoice()
    {
        PrintDocument pd = new PrintDocument();
        pd.PrintPage += new PrintPageEventHandler(PrintPageHandler);

        try
        {
            pd.Print();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Printing failed: {ex.Message}");
        }
    }

    private void PrintPageHandler(object sender, PrintPageEventArgs e)
    {
        Graphics g = e.Graphics!;
        Font headerFont = new Font("Segoe UI", 16, FontStyle.Bold);
        Font subHeaderFont = new Font("Segoe UI", 10, FontStyle.Bold);
        Font bodyFont = new Font("Segoe UI", 10);

        float y = 20;
        float leftMargin = 20;
        float centerOffset = e.PageBounds.Width / 2;

        // Title
        string title = "STORE INVENTORY SYSTEM";
        SizeF titleSize = g.MeasureString(title, headerFont);
        g.DrawString(title, headerFont, Brushes.Black, centerOffset - (titleSize.Width / 2), y);
        y += titleSize.Height + 10;

        // Receipt Info
        g.DrawString($"Receipt #: {_lastOrderNumber}", subHeaderFont, Brushes.Black, leftMargin, y);
        y += 20;
        g.DrawString($"Date: {_lastOrderDate:g}", bodyFont, Brushes.Black, leftMargin, y);
        y += 30;

        // Table Header
        g.DrawString("Item", subHeaderFont, Brushes.Black, leftMargin, y);
        g.DrawString("Qty", subHeaderFont, Brushes.Black, centerOffset, y);
        g.DrawString("Price", subHeaderFont, Brushes.Black, centerOffset + 100, y);
        g.DrawString("Total", subHeaderFont, Brushes.Black, e.PageBounds.Width - 100, y);
        y += 20;
        g.DrawLine(Pens.Black, leftMargin, y, e.PageBounds.Width - 20, y);
        y += 10;

        // Items
        if (_lastOrderItems != null)
        {
            foreach (var item in _lastOrderItems)
            {
                g.DrawString(item.Product.Name, bodyFont, Brushes.Black, leftMargin, y);
                g.DrawString(item.Quantity.ToString(), bodyFont, Brushes.Black, centerOffset, y);
                g.DrawString(item.Product.SellingPrice.ToString("C"), bodyFont, Brushes.Black, centerOffset + 100, y);
                g.DrawString((item.Product.SellingPrice * item.Quantity).ToString("C"), bodyFont, Brushes.Black, e.PageBounds.Width - 100, y);
                y += 20;
            }
        }

        y += 10;
        g.DrawLine(Pens.Black, leftMargin, y, e.PageBounds.Width - 20, y);
        y += 10;

        // Grand Total
        string totalText = $"GRAND TOTAL: {_lastOrderTotal:C}";
        SizeF totalSize = g.MeasureString(totalText, subHeaderFont);
        g.DrawString(totalText, subHeaderFont, Brushes.Black, e.PageBounds.Width - totalSize.Width - 20, y);

        y += 50;
        string footer = "Thank you for your business!";
        SizeF footerSize = g.MeasureString(footer, bodyFont);
        g.DrawString(footer, bodyFont, Brushes.Black, centerOffset - (footerSize.Width / 2), y);
    }

    private class CartItem
    {
        public Product Product { get; set; } = default!;
        public int Quantity { get; set; }
    }
}