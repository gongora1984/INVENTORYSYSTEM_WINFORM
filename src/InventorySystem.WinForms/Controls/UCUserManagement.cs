using InventorySystem.Shared;
using InventorySystem.WinForms.Services;
using InventorySystem.WinForms.Forms;

namespace InventorySystem.WinForms.Controls;

public class UCUserManagement : UserControl
{
    private readonly IInventoryService _service;
    private DataGridView dgvUsers;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Label lblTitle;

    public UCUserManagement(IInventoryService service)
    {
        _service = service;
        InitializeComponent();
        LoadUsers();
    }

    private void InitializeComponent()
    {
        lblTitle = new Label();
        dgvUsers = new DataGridView();
        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(20, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(272, 37);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Gestión de Usuarios";
        // 
        // dgvUsers
        // 
        dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvUsers.BackgroundColor = Color.White;
        dgvUsers.BorderStyle = BorderStyle.None;
        dgvUsers.ColumnHeadersHeight = 35;
        dgvUsers.Location = new Point(20, 70);
        dgvUsers.MultiSelect = false;
        dgvUsers.Name = "dgvUsers";
        dgvUsers.ReadOnly = true;
        dgvUsers.RowHeadersVisible = false;
        dgvUsers.RowHeadersWidth = 51;
        dgvUsers.RowTemplate.Height = 35;
        dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvUsers.Size = new Size(1672, 830);
        dgvUsers.TabIndex = 1;
        // 
        // btnAdd
        // 
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.BackColor = Color.FromArgb(40, 167, 69);
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(1265, 17);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(130, 40);
        btnAdd.TabIndex = 2;
        btnAdd.Text = "Nuevo Usuario";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += BtnAdd_Click;
        // 
        // btnEdit
        // 
        btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnEdit.BackColor = Color.FromArgb(0, 122, 204);
        btnEdit.FlatStyle = FlatStyle.Flat;
        btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnEdit.ForeColor = Color.White;
        btnEdit.Location = new Point(1416, 15);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(130, 40);
        btnEdit.TabIndex = 3;
        btnEdit.Text = "Editar Usuario";
        btnEdit.UseVisualStyleBackColor = false;
        btnEdit.Click += BtnEdit_Click;
        // 
        // btnDelete
        // 
        btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDelete.BackColor = Color.FromArgb(220, 53, 69);
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnDelete.ForeColor = Color.White;
        btnDelete.Location = new Point(1562, 15);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(130, 40);
        btnDelete.TabIndex = 4;
        btnDelete.Text = "Eliminar";
        btnDelete.UseVisualStyleBackColor = false;
        btnDelete.Click += BtnDelete_Click;
        // 
        // UCUserManagement
        // 
        BackColor = Color.White;
        Controls.Add(btnDelete);
        Controls.Add(btnEdit);
        Controls.Add(btnAdd);
        Controls.Add(lblTitle);
        Controls.Add(dgvUsers);
        Name = "UCUserManagement";
        Padding = new Padding(20);
        Size = new Size(1839, 920);
        ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private async void LoadUsers()
    {
        var users = await _service.GetUsersAsync();
        dgvUsers.DataSource = users.Select(u => new
        {
            u.Id,
            u.Username,
            u.Role
        }).ToList();
    }

    private async void BtnAdd_Click(object? sender, EventArgs e)
    {
        using (var form = new UserEditForm())
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                await _service.CreateUserAsync(form.User);
                LoadUsers();
            }
        }
    }

    private async void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (dgvUsers.SelectedRows.Count == 0) return;

        var userId = (Guid)dgvUsers.SelectedRows[0].Cells["Id"].Value;
        var users = await _service.GetUsersAsync();
        var user = users.FirstOrDefault(u => u.Id == userId);

        if (user != null)
        {
            using (var form = new UserEditForm(user))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await _service.UpdateUserAsync(form.User);
                    LoadUsers();
                }
            }
        }
    }

    private async void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (dgvUsers.SelectedRows.Count == 0) return;

        var userId = (Guid)dgvUsers.SelectedRows[0].Cells["Id"].Value;
        var username = dgvUsers.SelectedRows[0].Cells["Username"].Value.ToString();

        if (username?.ToLower() == "admin")
        {
            MessageBox.Show("No se puede eliminar el usuario administrador principal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (MessageBox.Show($"¿Está seguro de que desea eliminar al usuario '{username}'?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            await _service.DeleteUserAsync(userId);
            LoadUsers();
        }
    }
}
