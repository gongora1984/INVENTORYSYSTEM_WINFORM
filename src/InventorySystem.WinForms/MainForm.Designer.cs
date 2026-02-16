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
    private System.Windows.Forms.Button btnUsers;
    private System.Windows.Forms.Button btnLogout;
    private System.Windows.Forms.Button btnExit;
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
        btnExit = new Button();
        btnLogout = new Button();
        btnUsers = new Button();
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
        pnlNavigation.Controls.Add(btnExit);
        pnlNavigation.Controls.Add(btnLogout);
        pnlNavigation.Controls.Add(btnUsers);
        pnlNavigation.Controls.Add(btnReceiveStock);
        pnlNavigation.Controls.Add(btnDailySales);
        pnlNavigation.Controls.Add(btnInventory);
        pnlNavigation.Controls.Add(btnPos);
        pnlNavigation.Controls.Add(lblTitle);
        pnlNavigation.Dock = DockStyle.Left;
        pnlNavigation.Location = new Point(0, 0);
        pnlNavigation.Margin = new Padding(3, 4, 3, 4);
        pnlNavigation.Name = "pnlNavigation";
        pnlNavigation.Size = new Size(229, 800);
        pnlNavigation.TabIndex = 0;
        // 
        // btnExit
        // 
        btnExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnExit.BackColor = Color.Maroon;
        btnExit.FlatStyle = FlatStyle.Flat;
        btnExit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnExit.ForeColor = Color.White;
        btnExit.Image = Properties.Resources._3094700;
        btnExit.ImageAlign = ContentAlignment.MiddleRight;
        btnExit.Location = new Point(11, 733);
        btnExit.Margin = new Padding(3, 4, 3, 4);
        btnExit.Name = "btnExit";
        btnExit.Size = new Size(206, 53);
        btnExit.TabIndex = 7;
        btnExit.Text = "Salir";
        btnExit.UseVisualStyleBackColor = false;
        btnExit.Click += btnExit_Click;
        // 
        // btnLogout
        // 
        btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnLogout.BackColor = Color.DodgerBlue;
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLogout.ForeColor = Color.White;
        btnLogout.ImageAlign = ContentAlignment.MiddleLeft;
        btnLogout.Location = new Point(11, 667);
        btnLogout.Margin = new Padding(3, 4, 3, 4);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(206, 53);
        btnLogout.TabIndex = 6;
        btnLogout.Text = "Cerrar Sesión";
        btnLogout.UseVisualStyleBackColor = false;
        btnLogout.Click += btnLogout_Click;
        // 
        // btnUsers
        // 
        btnUsers.FlatStyle = FlatStyle.Flat;
        btnUsers.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnUsers.ForeColor = Color.White;
        btnUsers.Location = new Point(11, 453);
        btnUsers.Margin = new Padding(3, 4, 3, 4);
        btnUsers.Name = "btnUsers";
        btnUsers.Size = new Size(206, 60);
        btnUsers.TabIndex = 5;
        btnUsers.Text = "Usuarios";
        btnUsers.UseVisualStyleBackColor = true;
        btnUsers.Click += btnUsers_Click;
        // 
        // btnReceiveStock
        // 
        btnReceiveStock.FlatStyle = FlatStyle.Flat;
        btnReceiveStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnReceiveStock.ForeColor = Color.White;
        btnReceiveStock.Location = new Point(11, 380);
        btnReceiveStock.Margin = new Padding(3, 4, 3, 4);
        btnReceiveStock.Name = "btnReceiveStock";
        btnReceiveStock.Size = new Size(206, 60);
        btnReceiveStock.TabIndex = 0;
        btnReceiveStock.Text = "Recibir Productos";
        btnReceiveStock.UseVisualStyleBackColor = true;
        btnReceiveStock.Click += btnReceiveStock_Click;
        // 
        // btnDailySales
        // 
        btnDailySales.FlatStyle = FlatStyle.Flat;
        btnDailySales.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnDailySales.ForeColor = Color.White;
        btnDailySales.Location = new Point(11, 307);
        btnDailySales.Margin = new Padding(3, 4, 3, 4);
        btnDailySales.Name = "btnDailySales";
        btnDailySales.Size = new Size(206, 60);
        btnDailySales.TabIndex = 1;
        btnDailySales.Text = "Ventas Diarias";
        btnDailySales.UseVisualStyleBackColor = true;
        btnDailySales.Click += btnDailySales_Click;
        // 
        // btnInventory
        // 
        btnInventory.FlatStyle = FlatStyle.Flat;
        btnInventory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnInventory.ForeColor = Color.White;
        btnInventory.Location = new Point(11, 233);
        btnInventory.Margin = new Padding(3, 4, 3, 4);
        btnInventory.Name = "btnInventory";
        btnInventory.Size = new Size(206, 60);
        btnInventory.TabIndex = 2;
        btnInventory.Text = "Inventario";
        btnInventory.UseVisualStyleBackColor = true;
        btnInventory.Click += btnInventory_Click;
        // 
        // btnPos
        // 
        btnPos.FlatStyle = FlatStyle.Flat;
        btnPos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnPos.ForeColor = Color.White;
        btnPos.Location = new Point(11, 160);
        btnPos.Margin = new Padding(3, 4, 3, 4);
        btnPos.Name = "btnPos";
        btnPos.Size = new Size(206, 60);
        btnPos.TabIndex = 3;
        btnPos.Text = "Punto De Venta";
        btnPos.UseVisualStyleBackColor = true;
        btnPos.Click += btnPos_Click;
        // 
        // lblTitle
        // 
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(14, 27);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(201, 107);
        lblTitle.TabIndex = 4;
        lblTitle.Text = "Tienda";
        lblTitle.TextAlign = ContentAlignment.TopCenter;
        // 
        // pnlContent
        // 
        pnlContent.Dock = DockStyle.Fill;
        pnlContent.Location = new Point(229, 0);
        pnlContent.Margin = new Padding(3, 4, 3, 4);
        pnlContent.Name = "pnlContent";
        pnlContent.Size = new Size(914, 800);
        pnlContent.TabIndex = 1;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1143, 800);
        Controls.Add(pnlContent);
        Controls.Add(pnlNavigation);
        Margin = new Padding(3, 4, 3, 4);
        Name = "MainForm";
        Text = "Sistema de Inventario";
        WindowState = FormWindowState.Maximized;
        pnlNavigation.ResumeLayout(false);
        ResumeLayout(false);
    }
}
