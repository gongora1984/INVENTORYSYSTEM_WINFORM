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
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    private Label lblD;
    private Label lblO;
    private Label lblI;
    private Label lblGrandTotal;

    public UCDailySales(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
        InitializeComponent();
        LoadSales();
    }

    private void InitializeComponent()
    {
        dtpDate = new DateTimePicker();
        btnLoad = new Button();
        dgvOrders = new DataGridView();
        dgvDetails = new DataGridView();
        lblGrandTotal = new Label();
        lblD = new Label();
        lblO = new Label();
        lblI = new Label();
        dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
        SuspendLayout();
        // 
        // dtpDate
        // 
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(160, 18);
        dtpDate.Name = "dtpDate";
        dtpDate.Size = new Size(150, 27);
        dtpDate.TabIndex = 1;
        // 
        // btnLoad
        // 
        btnLoad.Location = new Point(320, 15);
        btnLoad.Name = "btnLoad";
        btnLoad.Size = new Size(120, 30);
        btnLoad.TabIndex = 2;
        btnLoad.Text = "Cargar Reporte";
        btnLoad.Click += BtnLoad_Click;
        // 
        // dgvOrders
        // 
        dgvOrders.AllowUserToAddRows = false;
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOrders.BackgroundColor = Color.White;
        dgvOrders.ColumnHeadersHeight = 29;
        dgvOrders.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
        dgvOrders.Location = new Point(20, 85);
        dgvOrders.Name = "dgvOrders";
        dgvOrders.ReadOnly = true;
        dgvOrders.RowHeadersWidth = 51;
        dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOrders.Size = new Size(1350, 200);
        dgvOrders.TabIndex = 4;
        dgvOrders.SelectionChanged += DgvOrders_SelectionChanged;
        // 
        // dgvDetails
        // 
        dgvDetails.AllowUserToAddRows = false;
        dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDetails.BackgroundColor = Color.White;
        dgvDetails.ColumnHeadersHeight = 29;
        dgvDetails.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8 });
        dgvDetails.Location = new Point(20, 325);
        dgvDetails.Name = "dgvDetails";
        dgvDetails.ReadOnly = true;
        dgvDetails.RowHeadersWidth = 51;
        dgvDetails.Size = new Size(1350, 200);
        dgvDetails.TabIndex = 6;
        // 
        // lblGrandTotal
        // 
        lblGrandTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblGrandTotal.Location = new Point(550, 540);
        lblGrandTotal.Name = "lblGrandTotal";
        lblGrandTotal.Size = new Size(200, 30);
        lblGrandTotal.TabIndex = 7;
        lblGrandTotal.Text = "Total: $0.00";
        lblGrandTotal.TextAlign = ContentAlignment.TopRight;
        // 
        // lblD
        // 
        lblD.Location = new Point(0, 0);
        lblD.Name = "lblD";
        lblD.Size = new Size(100, 23);
        lblD.TabIndex = 0;
        // 
        // lblO
        // 
        lblO.Location = new Point(0, 0);
        lblO.Name = "lblO";
        lblO.Size = new Size(100, 23);
        lblO.TabIndex = 3;
        // 
        // lblI
        // 
        lblI.Location = new Point(0, 0);
        lblI.Name = "lblI";
        lblI.Size = new Size(100, 23);
        lblI.TabIndex = 5;
        // 
        // dataGridViewTextBoxColumn1
        // 
        dataGridViewTextBoxColumn1.HeaderText = "Numero de Orden";
        dataGridViewTextBoxColumn1.MinimumWidth = 6;
        dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
        dataGridViewTextBoxColumn1.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn2
        // 
        dataGridViewTextBoxColumn2.HeaderText = "Hora";
        dataGridViewTextBoxColumn2.MinimumWidth = 6;
        dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
        dataGridViewTextBoxColumn2.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn3
        // 
        dataGridViewTextBoxColumn3.HeaderText = "Productos";
        dataGridViewTextBoxColumn3.MinimumWidth = 6;
        dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
        dataGridViewTextBoxColumn3.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn4
        // 
        dataGridViewTextBoxColumn4.HeaderText = "Total";
        dataGridViewTextBoxColumn4.MinimumWidth = 6;
        dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
        dataGridViewTextBoxColumn4.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn5
        // 
        dataGridViewTextBoxColumn5.HeaderText = "Producto";
        dataGridViewTextBoxColumn5.MinimumWidth = 6;
        dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
        dataGridViewTextBoxColumn5.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn6
        // 
        dataGridViewTextBoxColumn6.HeaderText = "Precio";
        dataGridViewTextBoxColumn6.MinimumWidth = 6;
        dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
        dataGridViewTextBoxColumn6.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn7
        // 
        dataGridViewTextBoxColumn7.HeaderText = "Cantidad";
        dataGridViewTextBoxColumn7.MinimumWidth = 6;
        dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
        dataGridViewTextBoxColumn7.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn8
        // 
        dataGridViewTextBoxColumn8.HeaderText = "Subtotal";
        dataGridViewTextBoxColumn8.MinimumWidth = 6;
        dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
        dataGridViewTextBoxColumn8.ReadOnly = true;
        // 
        // UCDailySales
        // 
        Controls.Add(lblD);
        Controls.Add(dtpDate);
        Controls.Add(btnLoad);
        Controls.Add(lblO);
        Controls.Add(dgvOrders);
        Controls.Add(lblI);
        Controls.Add(dgvDetails);
        Controls.Add(lblGrandTotal);
        Name = "UCDailySales";
        Size = new Size(1415, 600);
        ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
        ResumeLayout(false);
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