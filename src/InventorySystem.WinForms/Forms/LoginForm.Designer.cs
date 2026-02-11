namespace InventorySystem.WinForms.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label lblUsername;
    private System.Windows.Forms.Label lblPin;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.TextBox txtPin;
    private System.Windows.Forms.Button btnLogin;
    private System.Windows.Forms.Button btnCancel;
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
        lblTitle = new Label();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPin = new Label();
        txtPin = new TextBox();
        btnLogin = new Button();
        btnCancel = new Button();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
        lblTitle.Location = new Point(50, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(208, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Inventory Login";
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(30, 80);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(60, 15);
        lblUsername.TabIndex = 1;
        lblUsername.Text = "Username:";
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(30, 100);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(240, 23);
        txtUsername.TabIndex = 2;
        // 
        // lblPin
        // 
        lblPin.AutoSize = true;
        lblPin.Location = new Point(30, 140);
        lblPin.Name = "lblPin";
        lblPin.Size = new Size(57, 15);
        lblPin.TabIndex = 3;
        lblPin.Text = "PIN Code:";
        // 
        // txtPin
        // 
        txtPin.Location = new Point(30, 160);
        txtPin.Name = "txtPin";
        txtPin.PasswordChar = '*';
        txtPin.Size = new Size(240, 23);
        txtPin.TabIndex = 4;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.FromArgb(0, 122, 204);
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(30, 210);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(110, 35);
        btnLogin.TabIndex = 5;
        btnLogin.Text = "Login";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        // 
        // btnCancel
        // 
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Location = new Point(160, 210);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(110, 35);
        btnCancel.TabIndex = 6;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        // 
        // LoginForm
        // 
        AcceptButton = btnLogin;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(300, 280);
        Controls.Add(btnCancel);
        Controls.Add(btnLogin);
        Controls.Add(txtPin);
        Controls.Add(lblPin);
        Controls.Add(txtUsername);
        Controls.Add(lblUsername);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Login - Inventory System";
        ResumeLayout(false);
        PerformLayout();
    }
}
