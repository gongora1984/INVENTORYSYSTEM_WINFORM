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
        this.pnlNavigation = new System.Windows.Forms.Panel();
        this.pnlContent = new System.Windows.Forms.Panel();
        this.btnPos = new System.Windows.Forms.Button();
        this.btnInventory = new System.Windows.Forms.Button();
        this.btnDailySales = new System.Windows.Forms.Button();
        this.btnReceiveStock = new System.Windows.Forms.Button();
        this.lblTitle = new System.Windows.Forms.Label();
        this.pnlNavigation.SuspendLayout();
        this.SuspendLayout();
        
        // pnlNavigation
        this.pnlNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
        this.pnlNavigation.Controls.Add(this.btnReceiveStock);
        this.pnlNavigation.Controls.Add(this.btnDailySales);
        this.pnlNavigation.Controls.Add(this.btnInventory);
        this.pnlNavigation.Controls.Add(this.btnPos);
        this.pnlNavigation.Controls.Add(this.lblTitle);
        this.pnlNavigation.Dock = System.Windows.Forms.DockStyle.Left;
        this.pnlNavigation.Location = new System.Drawing.Point(0, 0);
        this.pnlNavigation.Name = "pnlNavigation";
        this.pnlNavigation.Size = new System.Drawing.Size(200, 600);
        this.pnlNavigation.TabIndex = 0;
        
        // lblTitle
        this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblTitle.ForeColor = System.Drawing.Color.White;
        this.lblTitle.Location = new System.Drawing.Point(12, 20);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(176, 80);
        this.lblTitle.Text = "Inventory System";
        this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;

        // btnPos
        this.btnPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnPos.ForeColor = System.Drawing.Color.White;
        this.btnPos.Location = new System.Drawing.Point(10, 120);
        this.btnPos.Name = "btnPos";
        this.btnPos.Size = new System.Drawing.Size(180, 45);
        this.btnPos.Text = "Point Of Sale";
        this.btnPos.UseVisualStyleBackColor = true;
        this.btnPos.Click += new System.EventHandler(this.btnPos_Click);

        // btnInventory
        this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnInventory.ForeColor = System.Drawing.Color.White;
        this.btnInventory.Location = new System.Drawing.Point(10, 175);
        this.btnInventory.Name = "btnInventory";
        this.btnInventory.Size = new System.Drawing.Size(180, 45);
        this.btnInventory.Text = "Inventory";
        this.btnInventory.UseVisualStyleBackColor = true;
        this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);

        // btnDailySales
        this.btnDailySales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnDailySales.ForeColor = System.Drawing.Color.White;
        this.btnDailySales.Location = new System.Drawing.Point(10, 230);
        this.btnDailySales.Name = "btnDailySales";
        this.btnDailySales.Size = new System.Drawing.Size(180, 45);
        this.btnDailySales.Text = "Daily Sales";
        this.btnDailySales.UseVisualStyleBackColor = true;
        this.btnDailySales.Click += new System.EventHandler(this.btnDailySales_Click);

        // btnReceiveStock
        this.btnReceiveStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnReceiveStock.ForeColor = System.Drawing.Color.White;
        this.btnReceiveStock.Location = new System.Drawing.Point(10, 285);
        this.btnReceiveStock.Name = "btnReceiveStock";
        this.btnReceiveStock.Size = new System.Drawing.Size(180, 45);
        this.btnReceiveStock.Text = "Receive Stock";
        this.btnReceiveStock.UseVisualStyleBackColor = true;
        this.btnReceiveStock.Click += new System.EventHandler(this.btnReceiveStock_Click);

        // pnlContent
        this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlContent.Location = new System.Drawing.Point(200, 0);
        this.pnlContent.Name = "pnlContent";
        this.pnlContent.Size = new System.Drawing.Size(800, 600);
        this.pnlContent.TabIndex = 1;
        
        // MainForm
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1000, 600);
        this.Controls.Add(this.pnlContent);
        this.Controls.Add(this.pnlNavigation);
        this.Name = "MainForm";
        this.Text = "Inventory Management System";
        this.pnlNavigation.ResumeLayout(false);
        this.ResumeLayout(false);
    }
}
