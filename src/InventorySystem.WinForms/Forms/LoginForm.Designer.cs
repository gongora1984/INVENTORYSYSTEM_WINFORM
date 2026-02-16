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
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.Location = new Point(57, 27);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(242, 41);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Inventory Login";
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(34, 107);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(78, 20);
        lblUsername.TabIndex = 1;
        lblUsername.Text = "Username:";
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(34, 133);
        txtUsername.Margin = new Padding(3, 4, 3, 4);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(274, 27);
        txtUsername.TabIndex = 2;
        // 
        // lblPin
        // 
        lblPin.AutoSize = true;
        lblPin.Location = new Point(34, 187);
        lblPin.Name = "lblPin";
        lblPin.Size = new Size(74, 20);
        lblPin.TabIndex = 3;
        lblPin.Text = "PIN Code:";
        // 
        // txtPin
        // 
        txtPin.Location = new Point(34, 213);
        txtPin.Margin = new Padding(3, 4, 3, 4);
        txtPin.Name = "txtPin";
        txtPin.PasswordChar = '*';
        txtPin.Size = new Size(274, 27);
        txtPin.TabIndex = 4;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.FromArgb(0, 122, 204);
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(34, 280);
        btnLogin.Margin = new Padding(3, 4, 3, 4);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(126, 47);
        btnLogin.TabIndex = 5;
        btnLogin.Text = "Login";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        // 
        // btnCancel
        // 
        btnCancel.BackColor = Color.Tomato;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.ForeColor = SystemColors.ActiveCaptionText;
        btnCancel.Location = new Point(183, 280);
        btnCancel.Margin = new Padding(3, 4, 3, 4);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(126, 47);
        btnCancel.TabIndex = 6;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += btnCancel_Click;
        // 
        // LoginForm
        // 
        AcceptButton = btnLogin;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(343, 373);
        Controls.Add(btnCancel);
        Controls.Add(btnLogin);
        Controls.Add(txtPin);
        Controls.Add(lblPin);
        Controls.Add(txtUsername);
        Controls.Add(lblUsername);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(3, 4, 3, 4);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Login - Inventory System";
        ResumeLayout(false);
        PerformLayout();
    }
}
