namespace InventorySystem.WinForms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Panel pnlNavigation;
    private System.Windows.Forms.Panel pnlContent;
    private System.Windows.Forms.Button btnPos;
    private System.Windows.Forms.Button btnInventory;
    private System.Windows.Forms.Button btnDailySales;
    private System.Windows.Forms.Button btnReceiveStock;
    private System.Windows.Forms.Label lblTitle;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlNavigation = new Panel();
        btnReceiveStock = new Button();
        btnDailySales = new Button();
        btnInventory = new Button();
        btnPos = new Button();
        lblTitle = new Label();
        pnlContent = new Panel();
        pnlNavigation.SuspendLayout();
        SuspendLayout();
        // 
        // pnlNavigation
        // 
        pnlNavigation.BackColor = Color.FromArgb(45, 45, 48);
        pnlNavigation.Controls.Add(btnReceiveStock);
        pnlNavigation.Controls.Add(btnDailySales);
        pnlNavigation.Controls.Add(btnInventory);
        pnlNavigation.Controls.Add(btnPos);
        pnlNavigation.Controls.Add(lblTitle);
        pnlNavigation.Dock = DockStyle.Left;
        pnlNavigation.Location = new Point(0, 0);
        pnlNavigation.Name = "pnlNavigation";
        pnlNavigation.Size = new Size(200, 600);
        pnlNavigation.TabIndex = 0;
        // 
        // btnReceiveStock
        // 
        btnReceiveStock.FlatStyle = FlatStyle.Flat;
        btnReceiveStock.ForeColor = Color.White;
        btnReceiveStock.Location = new Point(10, 285);
        btnReceiveStock.Name = "btnReceiveStock";
        btnReceiveStock.Size = new Size(180, 45);
        btnReceiveStock.TabIndex = 0;
        btnReceiveStock.Text = "Recibir Productos";
        btnReceiveStock.UseVisualStyleBackColor = true;
        btnReceiveStock.Click += btnReceiveStock_Click;
        // 
        // btnDailySales
        // 
        btnDailySales.FlatStyle = FlatStyle.Flat;
        btnDailySales.ForeColor = Color.White;
        btnDailySales.Location = new Point(10, 230);
        btnDailySales.Name = "btnDailySales";
        btnDailySales.Size = new Size(180, 45);
        btnDailySales.TabIndex = 1;
        btnDailySales.Text = "Ventas Diarias";
        btnDailySales.UseVisualStyleBackColor = true;
        btnDailySales.Click += btnDailySales_Click;
        // 
        // btnInventory
        // 
        btnInventory.FlatStyle = FlatStyle.Flat;
        btnInventory.ForeColor = Color.White;
        btnInventory.Location = new Point(10, 175);
        btnInventory.Name = "btnInventory";
        btnInventory.Size = new Size(180, 45);
        btnInventory.TabIndex = 2;
        btnInventory.Text = "Inventario";
        btnInventory.UseVisualStyleBackColor = true;
        btnInventory.Click += btnInventory_Click;
        // 
        // btnPos
        // 
        btnPos.FlatStyle = FlatStyle.Flat;
        btnPos.ForeColor = Color.White;
        btnPos.Location = new Point(10, 120);
        btnPos.Name = "btnPos";
        btnPos.Size = new Size(180, 45);
        btnPos.TabIndex = 3;
        btnPos.Text = "Punto De Venta";
        btnPos.UseVisualStyleBackColor = true;
        btnPos.Click += btnPos_Click;
        // 
        // lblTitle
        // 
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(12, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(176, 80);
        lblTitle.TabIndex = 4;
        lblTitle.Text = "Inventory System";
        lblTitle.TextAlign = ContentAlignment.TopCenter;
        // 
        // pnlContent
        // 
        pnlContent.Dock = DockStyle.Fill;
        pnlContent.Location = new Point(200, 0);
        pnlContent.Name = "pnlContent";
        pnlContent.Size = new Size(800, 600);
        pnlContent.TabIndex = 1;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 600);
        Controls.Add(pnlContent);
        Controls.Add(pnlNavigation);
        Name = "MainForm";
        Text = "Inventory Management System";
        WindowState = FormWindowState.Maximized;
        pnlNavigation.ResumeLayout(false);
        ResumeLayout(false);
    }
}
