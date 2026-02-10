using InventorySystem.Shared;
using InventorySystem.WinForms.Services;

namespace InventorySystem.WinForms.Controls;

public class UCDailySales : UserControl
{
    private readonly IInventoryService _inventoryService;
    private IEnumerable<StockMovement>? _sales;
    
    private DateTimePicker dtpDate;
    private Button btnLoad;
    private DataGridView dgvOrders;
    private DataGridView dgvDetails;
    private Label lblGrandTotal;

    public UCDailySales(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        InitializeComponent();
        LoadSales();
    }

    private void InitializeComponent()
    {
        this.dtpDate = new DateTimePicker();
        this.btnLoad = new Button();
        this.dgvOrders = new DataGridView();
        this.dgvDetails = new DataGridView();
        this.lblGrandTotal = new Label();
        
        this.SuspendLayout();

        var lblD = new Label { Text = "Select Date:", Location = new Point(20, 20), AutoSize = true };
        dtpDate.Location = new Point(100, 18);
        dtpDate.Size = new Size(150, 25);
        dtpDate.Format = DateTimePickerFormat.Short;

        btnLoad.Text = "Load Report";
        btnLoad.Location = new Point(260, 15);
        btnLoad.Size = new Size(120, 30);
        btnLoad.Click += (s, e) => LoadSales();

        // Orders List
        var lblO = new Label { Text = "Orders Overview:", Location = new Point(20, 60), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        dgvOrders.Location = new Point(20, 85);
        dgvOrders.Size = new Size(740, 200);
        dgvOrders.AllowUserToAddRows = false;
        dgvOrders.ReadOnly = true;
        dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOrders.Columns.Add("OrderNumber", "Order #");
        dgvOrders.Columns.Add("Time", "Time");
        dgvOrders.Columns.Add("Items", "Items");
        dgvOrders.Columns.Add("Total", "Total");
        dgvOrders.SelectionChanged += (s, e) => ShowOrderDetails();

        // Details List
        var lblI = new Label { Text = "Order Details:", Location = new Point(20, 300), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        dgvDetails.Location = new Point(20, 325);
        dgvDetails.Size = new Size(740, 200);
        dgvDetails.AllowUserToAddRows = false;
        dgvDetails.ReadOnly = true;
        dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDetails.Columns.Add("Product", "Product");
        dgvDetails.Columns.Add("Price", "Price");
        dgvDetails.Columns.Add("Qty", "Qty");
        dgvDetails.Columns.Add("Subtotal", "Subtotal");

        lblGrandTotal.Text = "Grand Total: $0.00";
        lblGrandTotal.Location = new Point(550, 540);
        lblGrandTotal.Size = new Size(200, 30);
        lblGrandTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblGrandTotal.TextAlign = ContentAlignment.TopRight;

        this.Controls.AddRange(new Control[] { lblD, dtpDate, btnLoad, lblO, dgvOrders, lblI, dgvDetails, lblGrandTotal });
        this.Name = "UCDailySales";
        this.Size = new Size(800, 600);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private async void LoadSales()
    {
        try
        {
            _sales = await _inventoryService.GetDailySalesAsync(dtpDate.Value);
            
            dgvOrders.Rows.Clear();
            dgvDetails.Rows.Clear();
            
            var groups = _sales.GroupBy(s => s.OrderNumber ?? "No Order ID").ToList();
            decimal grandTotal = 0;

            foreach (var group in groups)
            {
                var total = group.Sum(s => s.Quantity * (s.Product?.SellingPrice ?? 0));
                var items = group.Sum(s => s.Quantity);
                var time = group.First().Date.ToLocalTime().ToShortTimeString();
                
                dgvOrders.Rows.Add(group.Key, time, items, total.ToString("C"));
                grandTotal += total;
            }

            lblGrandTotal.Text = $"Grand Total: {grandTotal.ToString("C")}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading sales: {ex.Message}");
        }
    }

    private void ShowOrderDetails()
    {
        dgvDetails.Rows.Clear();
        if (dgvOrders.SelectedRows.Count == 0 || _sales == null) return;

        string orderId = dgvOrders.SelectedRows[0].Cells["OrderNumber"].Value.ToString() ?? "";
        var items = _sales.Where(s => (s.OrderNumber ?? "No Order ID") == orderId).ToList();

        foreach (var item in items)
        {
            decimal price = item.Product?.SellingPrice ?? 0;
            decimal subtotal = price * item.Quantity;
            dgvDetails.Rows.Add(item.Product?.Name ?? "Unknown", price.ToString("C"), item.Quantity, subtotal.ToString("C"));
        }
    }
}
