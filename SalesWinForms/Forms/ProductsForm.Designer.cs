using System.Drawing;
using System.Windows.Forms;

namespace SalesWinForms.Forms;

partial class ProductsForm
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView dgvProducts;
    private Label lblProducts;
    private Label lblId;
    private TextBox txtId;
    private Label lblName;
    private TextBox txtName;
    private Label lblPrice;
    private TextBox txtPrice;
    private Label lblStock;
    private TextBox txtStock;
    private Label lblImageUrl;
    private TextBox txtImageUrl;
    private Label lblImagePath;
    private TextBox txtImagePath;
    private Button btnSelectImage;
    private PictureBox pbProductImage;
    private Button btnNew;
    private Button btnSave;
    private Button btnEdit;
    private Button btnDelete;
    private Button btnRefresh;
    private Label lblStatus;
    private Panel pnlDetails;
    private Panel pnlList;

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
        dgvProducts = new DataGridView();
        lblProducts = new Label();
        lblId = new Label();
        txtId = new TextBox();
        lblName = new Label();
        txtName = new TextBox();
        lblPrice = new Label();
        txtPrice = new TextBox();
        lblStock = new Label();
        txtStock = new TextBox();
        lblImageUrl = new Label();
        txtImageUrl = new TextBox();
        lblImagePath = new Label();
        txtImagePath = new TextBox();
        btnSelectImage = new Button();
        pbProductImage = new PictureBox();
        btnNew = new Button();
        btnSave = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        btnRefresh = new Button();
        lblStatus = new Label();
        pnlDetails = new Panel();
        pnlList = new Panel();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pbProductImage).BeginInit();
        pnlDetails.SuspendLayout();
        pnlList.SuspendLayout();
        SuspendLayout();
        // 
        // dgvProducts
        // 
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AllowUserToDeleteRows = false;
        dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvProducts.Location = new Point(14, 60);
        dgvProducts.Margin = new Padding(3, 4, 3, 4);
        dgvProducts.MultiSelect = false;
        dgvProducts.Name = "dgvProducts";
        dgvProducts.ReadOnly = true;
        dgvProducts.RowHeadersWidth = 51;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.Size = new Size(1335, 453);
        dgvProducts.TabIndex = 2;
        dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
        // 
        // lblProducts
        // 
        lblProducts.AutoSize = true;
        lblProducts.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblProducts.Location = new Point(14, 16);
        lblProducts.Name = "lblProducts";
        lblProducts.Size = new Size(186, 28);
        lblProducts.TabIndex = 0;
        lblProducts.Text = "Lista de Productos";
        // 
        // lblId
        // 
        lblId.AutoSize = true;
        lblId.Location = new Point(14, 20);
        lblId.Name = "lblId";
        lblId.Size = new Size(27, 20);
        lblId.TabIndex = 0;
        lblId.Text = "ID:";
        // 
        // txtId
        // 
        txtId.Location = new Point(69, 16);
        txtId.Margin = new Padding(3, 4, 3, 4);
        txtId.Name = "txtId";
        txtId.ReadOnly = true;
        txtId.Size = new Size(114, 27);
        txtId.TabIndex = 1;
        // 
        // lblName
        // 
        lblName.AutoSize = true;
        lblName.Location = new Point(206, 20);
        lblName.Name = "lblName";
        lblName.Size = new Size(67, 20);
        lblName.TabIndex = 2;
        lblName.Text = "Nombre:";
        // 
        // txtName
        // 
        txtName.Location = new Point(274, 16);
        txtName.Margin = new Padding(3, 4, 3, 4);
        txtName.Name = "txtName";
        txtName.Size = new Size(342, 27);
        txtName.TabIndex = 3;
        // 
        // lblPrice
        // 
        lblPrice.AutoSize = true;
        lblPrice.Location = new Point(14, 67);
        lblPrice.Name = "lblPrice";
        lblPrice.Size = new Size(53, 20);
        lblPrice.TabIndex = 4;
        lblPrice.Text = "Precio:";
        // 
        // txtPrice
        // 
        txtPrice.Location = new Point(69, 63);
        txtPrice.Margin = new Padding(3, 4, 3, 4);
        txtPrice.Name = "txtPrice";
        txtPrice.Size = new Size(171, 27);
        txtPrice.TabIndex = 5;
        // 
        // lblStock
        // 
        lblStock.AutoSize = true;
        lblStock.Location = new Point(263, 67);
        lblStock.Name = "lblStock";
        lblStock.Size = new Size(48, 20);
        lblStock.TabIndex = 6;
        lblStock.Text = "Stock:";
        // 
        // txtStock
        // 
        txtStock.Location = new Point(320, 63);
        txtStock.Margin = new Padding(3, 4, 3, 4);
        txtStock.Name = "txtStock";
        txtStock.Size = new Size(171, 27);
        txtStock.TabIndex = 7;
        // 
        // lblImageUrl
        // 
        lblImageUrl.AutoSize = true;
        lblImageUrl.Location = new Point(14, 113);
        lblImageUrl.Name = "lblImageUrl";
        lblImageUrl.Size = new Size(92, 20);
        lblImageUrl.TabIndex = 8;
        lblImageUrl.Text = "URL Imagen:";
        // 
        // txtImageUrl
        // 
        txtImageUrl.Location = new Point(118, 109);
        txtImageUrl.Margin = new Padding(3, 4, 3, 4);
        txtImageUrl.Name = "txtImageUrl";
        txtImageUrl.ReadOnly = true;
        txtImageUrl.Size = new Size(499, 27);
        txtImageUrl.TabIndex = 9;
        // 
        // lblImagePath
        // 
        lblImagePath.AutoSize = true;
        lblImagePath.Location = new Point(14, 160);
        lblImagePath.Name = "lblImagePath";
        lblImagePath.Size = new Size(96, 20);
        lblImagePath.TabIndex = 10;
        lblImagePath.Text = "Ruta Imagen:";
        // 
        // txtImagePath
        // 
        txtImagePath.Location = new Point(129, 156);
        txtImagePath.Margin = new Padding(3, 4, 3, 4);
        txtImagePath.Name = "txtImagePath";
        txtImagePath.ReadOnly = true;
        txtImagePath.Size = new Size(399, 27);
        txtImagePath.TabIndex = 11;
        // 
        // btnSelectImage
        // 
        btnSelectImage.Location = new Point(537, 155);
        btnSelectImage.Margin = new Padding(3, 4, 3, 4);
        btnSelectImage.Name = "btnSelectImage";
        btnSelectImage.Size = new Size(80, 33);
        btnSelectImage.TabIndex = 12;
        btnSelectImage.Text = "Seleccionar";
        btnSelectImage.UseVisualStyleBackColor = true;
        btnSelectImage.Click += btnSelectImage_Click;
        // 
        // pbProductImage
        // 
        pbProductImage.BorderStyle = BorderStyle.FixedSingle;
        pbProductImage.Location = new Point(629, 16);
        pbProductImage.Margin = new Padding(3, 4, 3, 4);
        pbProductImage.Name = "pbProductImage";
        pbProductImage.Size = new Size(228, 266);
        pbProductImage.SizeMode = PictureBoxSizeMode.Zoom;
        pbProductImage.TabIndex = 13;
        pbProductImage.TabStop = false;
        // 
        // btnNew
        // 
        btnNew.Location = new Point(14, 333);
        btnNew.Margin = new Padding(3, 4, 3, 4);
        btnNew.Name = "btnNew";
        btnNew.Size = new Size(114, 47);
        btnNew.TabIndex = 14;
        btnNew.Text = "Nuevo";
        btnNew.UseVisualStyleBackColor = true;
        btnNew.Click += btnNew_Click;
        // 
        // btnSave
        // 
        btnSave.Location = new Point(137, 333);
        btnSave.Margin = new Padding(3, 4, 3, 4);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(114, 47);
        btnSave.TabIndex = 15;
        btnSave.Text = "Guardar";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        // 
        // btnEdit
        // 
        btnEdit.Enabled = false;
        btnEdit.Location = new Point(263, 333);
        btnEdit.Margin = new Padding(3, 4, 3, 4);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(114, 47);
        btnEdit.TabIndex = 16;
        btnEdit.Text = "Editar";
        btnEdit.UseVisualStyleBackColor = true;
        btnEdit.Click += btnEdit_Click;
        // 
        // btnDelete
        // 
        btnDelete.Enabled = false;
        btnDelete.Location = new Point(389, 333);
        btnDelete.Margin = new Padding(3, 4, 3, 4);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(114, 47);
        btnDelete.TabIndex = 17;
        btnDelete.Text = "Eliminar";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += btnDelete_Click;
        // 
        // btnRefresh
        // 
        btnRefresh.Location = new Point(1257, 13);
        btnRefresh.Margin = new Padding(3, 4, 3, 4);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(91, 40);
        btnRefresh.TabIndex = 1;
        btnRefresh.Text = "Actualizar";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Dock = DockStyle.Bottom;
        lblStatus.Location = new Point(0, 915);
        lblStatus.Name = "lblStatus";
        lblStatus.Padding = new Padding(11, 7, 0, 7);
        lblStatus.Size = new Size(11, 34);
        lblStatus.TabIndex = 2;
        // 
        // pnlDetails
        // 
        pnlDetails.Controls.Add(pbProductImage);
        pnlDetails.Controls.Add(btnSelectImage);
        pnlDetails.Controls.Add(txtImagePath);
        pnlDetails.Controls.Add(lblImagePath);
        pnlDetails.Controls.Add(txtImageUrl);
        pnlDetails.Controls.Add(lblImageUrl);
        pnlDetails.Controls.Add(txtStock);
        pnlDetails.Controls.Add(lblStock);
        pnlDetails.Controls.Add(txtPrice);
        pnlDetails.Controls.Add(lblPrice);
        pnlDetails.Controls.Add(txtName);
        pnlDetails.Controls.Add(lblName);
        pnlDetails.Controls.Add(txtId);
        pnlDetails.Controls.Add(lblId);
        pnlDetails.Controls.Add(btnNew);
        pnlDetails.Controls.Add(btnSave);
        pnlDetails.Controls.Add(btnEdit);
        pnlDetails.Controls.Add(btnDelete);
        pnlDetails.Dock = DockStyle.Fill;
        pnlDetails.Location = new Point(0, 533);
        pnlDetails.Margin = new Padding(3, 4, 3, 4);
        pnlDetails.Name = "pnlDetails";
        pnlDetails.Size = new Size(1371, 382);
        pnlDetails.TabIndex = 1;
        // 
        // pnlList
        // 
        pnlList.Controls.Add(dgvProducts);
        pnlList.Controls.Add(lblProducts);
        pnlList.Controls.Add(btnRefresh);
        pnlList.Dock = DockStyle.Top;
        pnlList.Location = new Point(0, 0);
        pnlList.Margin = new Padding(3, 4, 3, 4);
        pnlList.Name = "pnlList";
        pnlList.Size = new Size(1371, 533);
        pnlList.TabIndex = 0;
        // 
        // ProductsForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1371, 949);
        Controls.Add(pnlDetails);
        Controls.Add(pnlList);
        Controls.Add(lblStatus);
        Margin = new Padding(3, 4, 3, 4);
        Name = "ProductsForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Gestión de Productos";
        Load += ProductsForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
        ((System.ComponentModel.ISupportInitialize)pbProductImage).EndInit();
        pnlDetails.ResumeLayout(false);
        pnlDetails.PerformLayout();
        pnlList.ResumeLayout(false);
        pnlList.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}

