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

        var lblD = new Label { Text = "Seleccionar Fecha:", Location = new Point(20, 20), AutoSize = true };
        dtpDate.Location = new Point(160, 18);
        dtpDate.Size = new Size(150, 25);
        dtpDate.Format = DateTimePickerFormat.Short;

        btnLoad.Text = "Cargar Reporte";
        btnLoad.Location = new Point(320, 15);
        btnLoad.Size = new Size(120, 30);
        btnLoad.Click += BtnLoad_Click;

        // Orders List
        var lblO = new Label { Text = "Ordenes:", Location = new Point(20, 60), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        dgvOrders.Location = new Point(20, 85);
        dgvOrders.Size = new Size(740, 200);
        dgvOrders.AllowUserToAddRows = false;
        dgvOrders.ReadOnly = true;
        dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOrders.Columns.Add("OrderNumber", "Numero de Orden");
        dgvOrders.Columns.Add("Time", "Hora");
        dgvOrders.Columns.Add("Items", "Productos");
        dgvOrders.Columns.Add("Total", "Total");
        dgvOrders.SelectionChanged += DgvOrders_SelectionChanged;

        // Details List
        var lblI = new Label { Text = "Detalles de la Orden:", Location = new Point(20, 300), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
        dgvDetails.Location = new Point(20, 325);
        dgvDetails.Size = new Size(740, 200);
        dgvDetails.AllowUserToAddRows = false;
        dgvDetails.ReadOnly = true;
        dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDetails.Columns.Add("Product", "Producto");
        dgvDetails.Columns.Add("Price", "Precio");
        dgvDetails.Columns.Add("Qty", "Cantidad");
        dgvDetails.Columns.Add("Subtotal", "Subtotal");

        lblGrandTotal.Text = "Total: $0.00";
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

    private void BtnLoad_Click(object? sender, EventArgs e)
    {
        LoadSales();
    }

    private void DgvOrders_SelectionChanged(object? sender, EventArgs e)
    {
        ShowOrderDetails();
    }

    private async void LoadSales()
    {
        try
        {
            _sales = await _inventoryService.GetDailySalesAsync(dtpDate.Value);

            dgvOrders.Rows.Clear();
            dgvDetails.Rows.Clear();

            var groups = _sales.GroupBy(s => s.OrderNumber ?? "No Numbero de Orden").ToList();
            decimal grandTotal = 0;

            foreach (var group in groups)
            {
                var total = group.Sum(s => s.Quantity * (s.Product?.SellingPrice ?? 0));
                var items = group.Sum(s => s.Quantity);
                var time = group.First().Date.ToLocalTime().ToShortTimeString();

                dgvOrders.Rows.Add(group.Key, time, items, total.ToString("C"));
                grandTotal += total;
            }

            lblGrandTotal.Text = $"Total: {grandTotal.ToString("C")}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error cargando ventas: {ex.Message}");
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