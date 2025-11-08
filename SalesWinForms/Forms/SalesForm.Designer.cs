using System.Drawing;
using System.Windows.Forms;

namespace SalesWinForms.Forms;

partial class SalesForm
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView dgvSales;
    private DataGridView dgvSaleDetails;
    private DataGridView dgvNewSaleDetails;
    private Label lblSales;
    private Label lblId;
    private TextBox txtId;
    private Label lblCreationDate;
    private TextBox txtCreationDate;
    private Label lblCreationUser;
    private TextBox txtCreationUser;
    private Label lblTotal;
    private TextBox txtTotal;
    private Label lblNewSale;
    private Label lblNewCreationUser;
    private TextBox txtNewCreationUser;
    private Label lblProduct;
    private ComboBox cmbProduct;
    private Label lblStockValue;
    private Label lblQuantity;
    private TextBox txtQuantity;
    private Label lblUnitPrice;
    private TextBox txtUnitPrice;
    private Button btnAddDetail;
    private Label lblNewTotal;
    private TextBox txtNewTotal;
    private Button btnNew;
    private Button btnSave;
    private Button btnDelete;
    private Button btnRefresh;
    private Label lblStatus;
    private Panel pnlList;
    private Panel pnlDetails;
    private Panel pnlNewSale;
    private SplitContainer splitContainer1;

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
        dgvSales = new DataGridView();
        dgvSaleDetails = new DataGridView();
        dgvNewSaleDetails = new DataGridView();
        lblSales = new Label();
        lblId = new Label();
        txtId = new TextBox();
        lblCreationDate = new Label();
        txtCreationDate = new TextBox();
        lblCreationUser = new Label();
        txtCreationUser = new TextBox();
        lblTotal = new Label();
        txtTotal = new TextBox();
        lblNewSale = new Label();
        lblNewCreationUser = new Label();
        txtNewCreationUser = new TextBox();
        lblProduct = new Label();
        cmbProduct = new ComboBox();
        lblStockValue = new Label();
        lblQuantity = new Label();
        txtQuantity = new TextBox();
        lblUnitPrice = new Label();
        txtUnitPrice = new TextBox();
        btnAddDetail = new Button();
        lblNewTotal = new Label();
        txtNewTotal = new TextBox();
        btnNew = new Button();
        btnSave = new Button();
        btnDelete = new Button();
        btnRefresh = new Button();
        lblStatus = new Label();
        pnlList = new Panel();
        pnlDetails = new Panel();
        pnlNewSale = new Panel();
        splitContainer1 = new SplitContainer();
        ((System.ComponentModel.ISupportInitialize)dgvSales).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvSaleDetails).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvNewSaleDetails).BeginInit();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.SuspendLayout();
        pnlList.SuspendLayout();
        pnlDetails.SuspendLayout();
        pnlNewSale.SuspendLayout();
        SuspendLayout();
        
        // 
        // pnlList
        // 
        pnlList.Controls.Add(dgvSales);
        pnlList.Controls.Add(lblSales);
        pnlList.Controls.Add(btnRefresh);
        pnlList.Dock = DockStyle.Top;
        pnlList.Location = new Point(0, 0);
        pnlList.Size = new Size(1400, 300);
        pnlList.TabIndex = 0;
        
        // 
        // lblSales
        // 
        lblSales.AutoSize = true;
        lblSales.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblSales.Location = new Point(14, 16);
        lblSales.Name = "lblSales";
        lblSales.Size = new Size(140, 28);
        lblSales.TabIndex = 0;
        lblSales.Text = "Lista de Ventas";
        
        // 
        // btnRefresh
        // 
        btnRefresh.Location = new Point(1286, 13);
        btnRefresh.Margin = new Padding(3, 4, 3, 4);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(91, 40);
        btnRefresh.TabIndex = 1;
        btnRefresh.Text = "Actualizar";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        
        // 
        // dgvSales
        // 
        dgvSales.AllowUserToAddRows = false;
        dgvSales.AllowUserToDeleteRows = false;
        dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSales.Location = new Point(14, 60);
        dgvSales.Margin = new Padding(3, 4, 3, 4);
        dgvSales.MultiSelect = false;
        dgvSales.Name = "dgvSales";
        dgvSales.ReadOnly = true;
        dgvSales.RowHeadersWidth = 51;
        dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvSales.Size = new Size(1363, 220);
        dgvSales.TabIndex = 2;
        dgvSales.SelectionChanged += dgvSales_SelectionChanged;
        
        // 
        // splitContainer1
        // 
        splitContainer1.Dock = DockStyle.Fill;
        splitContainer1.Location = new Point(0, 300);
        splitContainer1.Margin = new Padding(3, 4, 3, 4);
        splitContainer1.Name = "splitContainer1";
        splitContainer1.Orientation = Orientation.Horizontal;
        
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(pnlDetails);
        
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(pnlNewSale);
        splitContainer1.Size = new Size(1400, 400);
        splitContainer1.SplitterDistance = 200;
        splitContainer1.TabIndex = 1;
        
        // 
        // pnlDetails
        // 
        pnlDetails.Controls.Add(dgvSaleDetails);
        pnlDetails.Controls.Add(txtTotal);
        pnlDetails.Controls.Add(lblTotal);
        pnlDetails.Controls.Add(txtCreationUser);
        pnlDetails.Controls.Add(lblCreationUser);
        pnlDetails.Controls.Add(txtCreationDate);
        pnlDetails.Controls.Add(lblCreationDate);
        pnlDetails.Controls.Add(txtId);
        pnlDetails.Controls.Add(lblId);
        pnlDetails.Controls.Add(btnDelete);
        pnlDetails.Dock = DockStyle.Fill;
        pnlDetails.Location = new Point(0, 0);
        pnlDetails.Margin = new Padding(3, 4, 3, 4);
        pnlDetails.Name = "pnlDetails";
        pnlDetails.Size = new Size(1400, 200);
        pnlDetails.TabIndex = 0;
        
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
        // lblCreationDate
        // 
        lblCreationDate.AutoSize = true;
        lblCreationDate.Location = new Point(206, 20);
        lblCreationDate.Name = "lblCreationDate";
        lblCreationDate.Size = new Size(54, 20);
        lblCreationDate.TabIndex = 2;
        lblCreationDate.Text = "Fecha:";
        
        // 
        // txtCreationDate
        // 
        txtCreationDate.Location = new Point(274, 16);
        txtCreationDate.Margin = new Padding(3, 4, 3, 4);
        txtCreationDate.Name = "txtCreationDate";
        txtCreationDate.ReadOnly = true;
        txtCreationDate.Size = new Size(200, 27);
        txtCreationDate.TabIndex = 3;
        
        // 
        // lblCreationUser
        // 
        lblCreationUser.AutoSize = true;
        lblCreationUser.Location = new Point(500, 20);
        lblCreationUser.Name = "lblCreationUser";
        lblCreationUser.Size = new Size(64, 20);
        lblCreationUser.TabIndex = 4;
        lblCreationUser.Text = "Usuario:";
        
        // 
        // txtCreationUser
        // 
        txtCreationUser.Location = new Point(580, 16);
        txtCreationUser.Margin = new Padding(3, 4, 3, 4);
        txtCreationUser.Name = "txtCreationUser";
        txtCreationUser.ReadOnly = true;
        txtCreationUser.Size = new Size(200, 27);
        txtCreationUser.TabIndex = 5;
        
        // 
        // lblTotal
        // 
        lblTotal.AutoSize = true;
        lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTotal.Location = new Point(800, 20);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(52, 23);
        lblTotal.TabIndex = 6;
        lblTotal.Text = "Total:";
        
        // 
        // txtTotal
        // 
        txtTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        txtTotal.Location = new Point(870, 16);
        txtTotal.Margin = new Padding(3, 4, 3, 4);
        txtTotal.Name = "txtTotal";
        txtTotal.ReadOnly = true;
        txtTotal.Size = new Size(150, 30);
        txtTotal.TabIndex = 7;
        
        // 
        // dgvSaleDetails
        // 
        dgvSaleDetails.AllowUserToAddRows = false;
        dgvSaleDetails.AllowUserToDeleteRows = false;
        dgvSaleDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvSaleDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSaleDetails.Location = new Point(14, 60);
        dgvSaleDetails.Margin = new Padding(3, 4, 3, 4);
        dgvSaleDetails.MultiSelect = false;
        dgvSaleDetails.Name = "dgvSaleDetails";
        dgvSaleDetails.ReadOnly = true;
        dgvSaleDetails.RowHeadersWidth = 51;
        dgvSaleDetails.Size = new Size(1006, 120);
        dgvSaleDetails.TabIndex = 8;
        
        // 
        // btnDelete
        // 
        btnDelete.Enabled = false;
        btnDelete.Location = new Point(1040, 16);
        btnDelete.Margin = new Padding(3, 4, 3, 4);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(114, 47);
        btnDelete.TabIndex = 9;
        btnDelete.Text = "Eliminar";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += btnDelete_Click;
        
        // 
        // pnlNewSale
        // 
        pnlNewSale.Controls.Add(btnSave);
        pnlNewSale.Controls.Add(btnNew);
        pnlNewSale.Controls.Add(txtNewTotal);
        pnlNewSale.Controls.Add(lblNewTotal);
        pnlNewSale.Controls.Add(dgvNewSaleDetails);
        pnlNewSale.Controls.Add(btnAddDetail);
        pnlNewSale.Controls.Add(txtUnitPrice);
        pnlNewSale.Controls.Add(lblUnitPrice);
        pnlNewSale.Controls.Add(txtQuantity);
        pnlNewSale.Controls.Add(lblQuantity);
        pnlNewSale.Controls.Add(lblStockValue);
        pnlNewSale.Controls.Add(cmbProduct);
        pnlNewSale.Controls.Add(lblProduct);
        pnlNewSale.Controls.Add(txtNewCreationUser);
        pnlNewSale.Controls.Add(lblNewCreationUser);
        pnlNewSale.Controls.Add(lblNewSale);
        pnlNewSale.Dock = DockStyle.Fill;
        pnlNewSale.Location = new Point(0, 0);
        pnlNewSale.Margin = new Padding(3, 4, 3, 4);
        pnlNewSale.Name = "pnlNewSale";
        pnlNewSale.Size = new Size(1400, 196);
        pnlNewSale.TabIndex = 0;
        
        // 
        // lblNewSale
        // 
        lblNewSale.AutoSize = true;
        lblNewSale.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblNewSale.Location = new Point(14, 16);
        lblNewSale.Name = "lblNewSale";
        lblNewSale.Size = new Size(120, 28);
        lblNewSale.TabIndex = 0;
        lblNewSale.Text = "Nueva Venta";
        
        // 
        // lblNewCreationUser
        // 
        lblNewCreationUser.AutoSize = true;
        lblNewCreationUser.Location = new Point(200, 20);
        lblNewCreationUser.Name = "lblNewCreationUser";
        lblNewCreationUser.Size = new Size(64, 20);
        lblNewCreationUser.TabIndex = 1;
        lblNewCreationUser.Text = "Usuario:";
        
        // 
        // txtNewCreationUser
        // 
        txtNewCreationUser.Location = new Point(280, 16);
        txtNewCreationUser.Margin = new Padding(3, 4, 3, 4);
        txtNewCreationUser.Name = "txtNewCreationUser";
        txtNewCreationUser.Size = new Size(200, 27);
        txtNewCreationUser.TabIndex = 2;
        
        // 
        // lblProduct
        // 
        lblProduct.AutoSize = true;
        lblProduct.Location = new Point(14, 60);
        lblProduct.Name = "lblProduct";
        lblProduct.Size = new Size(67, 20);
        lblProduct.TabIndex = 1;
        lblProduct.Text = "Producto:";
        
        // 
        // cmbProduct
        // 
        cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbProduct.FormattingEnabled = true;
        cmbProduct.Location = new Point(100, 56);
        cmbProduct.Margin = new Padding(3, 4, 3, 4);
        cmbProduct.Name = "cmbProduct";
        cmbProduct.Size = new Size(300, 28);
        cmbProduct.TabIndex = 2;
        cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
        
        // 
        // lblStockValue
        // 
        lblStockValue.AutoSize = true;
        lblStockValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblStockValue.Location = new Point(420, 60);
        lblStockValue.Name = "lblStockValue";
        lblStockValue.Size = new Size(150, 20);
        lblStockValue.TabIndex = 3;
        lblStockValue.Text = "Stock disponible: -";
        
        // 
        // lblQuantity
        // 
        lblQuantity.AutoSize = true;
        lblQuantity.Location = new Point(580, 60);
        lblQuantity.Name = "lblQuantity";
        lblQuantity.Size = new Size(72, 20);
        lblQuantity.TabIndex = 3;
        lblQuantity.Text = "Cantidad:";
        
        // 
        // txtQuantity
        // 
        txtQuantity.Location = new Point(660, 56);
        txtQuantity.Margin = new Padding(3, 4, 3, 4);
        txtQuantity.Name = "txtQuantity";
        txtQuantity.Size = new Size(100, 27);
        txtQuantity.TabIndex = 4;
        
        // 
        // lblUnitPrice
        // 
        lblUnitPrice.AutoSize = true;
        lblUnitPrice.Location = new Point(780, 60);
        lblUnitPrice.Name = "lblUnitPrice";
        lblUnitPrice.Size = new Size(108, 20);
        lblUnitPrice.TabIndex = 5;
        lblUnitPrice.Text = "Precio Unitario:";
        
        // 
        // txtUnitPrice
        // 
        txtUnitPrice.Location = new Point(900, 56);
        txtUnitPrice.Margin = new Padding(3, 4, 3, 4);
        txtUnitPrice.Name = "txtUnitPrice";
        txtUnitPrice.Size = new Size(100, 27);
        txtUnitPrice.TabIndex = 6;
        
        // 
        // btnAddDetail
        // 
        btnAddDetail.Location = new Point(1020, 54);
        btnAddDetail.Margin = new Padding(3, 4, 3, 4);
        btnAddDetail.Name = "btnAddDetail";
        btnAddDetail.Size = new Size(100, 33);
        btnAddDetail.TabIndex = 7;
        btnAddDetail.Text = "Agregar";
        btnAddDetail.UseVisualStyleBackColor = true;
        btnAddDetail.Click += btnAddDetail_Click;
        
        // 
        // dgvNewSaleDetails
        // 
        dgvNewSaleDetails.AllowUserToAddRows = false;
        dgvNewSaleDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvNewSaleDetails.Columns.Add("ProductId", "ID Producto");
        dgvNewSaleDetails.Columns.Add("ProductName", "Producto");
        dgvNewSaleDetails.Columns.Add("Quantity", "Cantidad");
        dgvNewSaleDetails.Columns.Add("UnitPrice", "Precio Unitario");
        dgvNewSaleDetails.Columns.Add("Subtotal", "Subtotal");
        dgvNewSaleDetails.Columns["ProductId"].Visible = false;
        dgvNewSaleDetails.Columns["Subtotal"].ReadOnly = true;
        dgvNewSaleDetails.Location = new Point(14, 100);
        dgvNewSaleDetails.Margin = new Padding(3, 4, 3, 4);
        dgvNewSaleDetails.Name = "dgvNewSaleDetails";
        dgvNewSaleDetails.RowHeadersWidth = 51;
        dgvNewSaleDetails.Size = new Size(946, 80);
        dgvNewSaleDetails.TabIndex = 8;
        dgvNewSaleDetails.RowsRemoved += dgvNewSaleDetails_RowsRemoved;
        
        // 
        // lblNewTotal
        // 
        lblNewTotal.AutoSize = true;
        lblNewTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblNewTotal.Location = new Point(980, 100);
        lblNewTotal.Name = "lblNewTotal";
        lblNewTotal.Size = new Size(52, 23);
        lblNewTotal.TabIndex = 9;
        lblNewTotal.Text = "Total:";
        
        // 
        // txtNewTotal
        // 
        txtNewTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        txtNewTotal.Location = new Point(1040, 96);
        txtNewTotal.Margin = new Padding(3, 4, 3, 4);
        txtNewTotal.Name = "txtNewTotal";
        txtNewTotal.ReadOnly = true;
        txtNewTotal.Size = new Size(150, 30);
        txtNewTotal.TabIndex = 10;
        txtNewTotal.Text = "0.00";
        
        // 
        // btnNew
        // 
        btnNew.Location = new Point(980, 140);
        btnNew.Margin = new Padding(3, 4, 3, 4);
        btnNew.Name = "btnNew";
        btnNew.Size = new Size(100, 40);
        btnNew.TabIndex = 11;
        btnNew.Text = "Nuevo";
        btnNew.UseVisualStyleBackColor = true;
        btnNew.Click += btnNew_Click;
        
        // 
        // btnSave
        // 
        btnSave.Location = new Point(1090, 140);
        btnSave.Margin = new Padding(3, 4, 3, 4);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(100, 40);
        btnSave.TabIndex = 12;
        btnSave.Text = "Guardar";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Dock = DockStyle.Bottom;
        lblStatus.Location = new Point(0, 688);
        lblStatus.Name = "lblStatus";
        lblStatus.Padding = new Padding(11, 7, 0, 7);
        lblStatus.Size = new Size(11, 34);
        lblStatus.TabIndex = 2;
        
        // 
        // SalesForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1400, 722);
        Controls.Add(splitContainer1);
        Controls.Add(pnlList);
        Controls.Add(lblStatus);
        Margin = new Padding(3, 4, 3, 4);
        Name = "SalesForm";
        Text = "Gestión de Ventas";
        Load += SalesForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvSales).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvSaleDetails).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvNewSaleDetails).EndInit();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        pnlList.ResumeLayout(false);
        pnlList.PerformLayout();
        pnlDetails.ResumeLayout(false);
        pnlDetails.PerformLayout();
        pnlNewSale.ResumeLayout(false);
        pnlNewSale.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}

