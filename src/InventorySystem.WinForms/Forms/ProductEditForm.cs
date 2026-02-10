using InventorySystem.Shared;

namespace InventorySystem.WinForms.Forms;

public class ProductEditForm : Form
{
    public Product Product { get; private set; }
    private TextBox txtName;
    private TextBox txtBarcode;
    private NumericUpDown numPrice;
    private Button btnSave;
    private bool _isEdit;

    public ProductEditForm(Product? p = null)
    {
        Product = p ?? new Product();
        _isEdit = Product.Id != Guid.Empty;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = _isEdit ? "Edit Product" : "Add New Product";
        this.Size = new Size(400, 300);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterParent;

        var lblName = new Label { Text = "Name:", Location = new Point(20, 20), AutoSize = true };
        txtName = new TextBox { Location = new Point(20, 45), Size = new Size(340, 25), Text = Product.Name };
        if (_isEdit) txtName.ReadOnly = true;

        var lblBarcode = new Label { Text = "Barcode:", Location = new Point(20, 80), AutoSize = true };
        txtBarcode = new TextBox { Location = new Point(20, 105), Size = new Size(340, 25), Text = Product.Barcode };
        if (_isEdit) txtBarcode.ReadOnly = true;

        var lblPrice = new Label { Text = "Selling Price:", Location = new Point(20, 140), AutoSize = true };
        numPrice = new NumericUpDown { Location = new Point(20, 165), Size = new Size(120, 25), DecimalPlaces = 2, Maximum = 1000000, Value = Product.SellingPrice };

        btnSave = new Button { Text = "Save", Location = new Point(260, 210), Size = new Size(100, 35), DialogResult = DialogResult.OK };
        btnSave.Click += (s, e) => {
            Product.Name = txtName.Text;
            Product.Barcode = txtBarcode.Text;
            Product.SellingPrice = numPrice.Value;
        };

        this.Controls.AddRange(new Control[] { lblName, txtName, lblBarcode, txtBarcode, lblPrice, numPrice, btnSave });
    }
}
