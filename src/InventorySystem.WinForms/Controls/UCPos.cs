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
    private Label lblStatus;

    public UCPos(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.txtBarcode = new TextBox();
        this.dgvCart = new DataGridView();
        this.lblTotal = new Label();
        this.btnCheckout = new Button();
        this.lblStatus = new Label();
        var lblBarcode = new Label();
        
        this.SuspendLayout();

        // lblBarcode
        lblBarcode.Text = "Scan Barcode:";
        lblBarcode.Location = new Point(20, 20);
        lblBarcode.AutoSize = true;

        // txtBarcode
        this.txtBarcode.Location = new Point(20, 45);
        this.txtBarcode.Size = new Size(300, 30);
        this.txtBarcode.Font = new Font("Segoe UI", 12F);
        this.txtBarcode.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) HandleBarcode(); };

        // dgvCart
        this.dgvCart.Location = new Point(20, 90);
        this.dgvCart.Size = new Size(740, 400);
        this.dgvCart.AllowUserToAddRows = false;
        this.dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvCart.Columns.Add("Item", "Item");
        this.dgvCart.Columns.Add("Price", "Price");
        this.dgvCart.Columns.Add("Qty", "Qty");
        this.dgvCart.Columns.Add("Total", "Total");

        // lblTotal
        this.lblTotal.Text = "Total: $0.00";
        this.lblTotal.Location = new Point(550, 500);
        this.lblTotal.Size = new Size(200, 30);
        this.lblTotal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        this.lblTotal.TextAlign = ContentAlignment.TopRight;

        // btnCheckout
        this.btnCheckout.Text = "Checkout & Print";
        this.btnCheckout.Location = new Point(20, 500);
        this.btnCheckout.Size = new Size(200, 45);
        this.btnCheckout.FlatStyle = FlatStyle.Flat;
        this.btnCheckout.BackColor = Color.ForestGreen;
        this.btnCheckout.ForeColor = Color.White;
        this.btnCheckout.Click += (s, e) => Checkout();

        // lblStatus
        this.lblStatus.Location = new Point(20, 550);
        this.lblStatus.Size = new Size(740, 30);
        this.lblStatus.ForeColor = Color.Red;

        this.Controls.Add(lblBarcode);
        this.Controls.Add(txtBarcode);
        this.Controls.Add(dgvCart);
        this.Controls.Add(lblTotal);
        this.Controls.Add(btnCheckout);
        this.Controls.Add(lblStatus);
        
        this.Name = "UCPos";
        this.Size = new Size(800, 600);
        this.ResumeLayout(false);
        this.PerformLayout();
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
