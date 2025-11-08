using System.Drawing;
using System.Windows.Forms;

namespace SalesWinForms.Forms;

partial class SalesReportForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitle;
    private Label lblStartDate;
    private DateTimePicker dtpStartDate;
    private Label lblEndDate;
    private DateTimePicker dtpEndDate;
    private Button btnGenerateReport;
    private Button btnExport;
    private DataGridView dgvReportDetails;
    private Label lblTotalSales;
    private Label lblTotalDetails;
    private Label lblTotalAmount;
    private Label lblStatus;
    private Panel pnlFilters;
    private Panel pnlStats;
    private Panel pnlReport;

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
        pnlFilters = new Panel();
        btnExport = new Button();
        btnGenerateReport = new Button();
        dtpEndDate = new DateTimePicker();
        lblEndDate = new Label();
        dtpStartDate = new DateTimePicker();
        lblStartDate = new Label();
        pnlStats = new Panel();
        lblTotalAmount = new Label();
        lblTotalDetails = new Label();
        lblTotalSales = new Label();
        pnlReport = new Panel();
        dgvReportDetails = new DataGridView();
        lblStatus = new Label();
        pnlFilters.SuspendLayout();
        pnlStats.SuspendLayout();
        pnlReport.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvReportDetails).BeginInit();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTitle.Location = new Point(14, 16);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(222, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Reporte de Ventas";
        // 
        // pnlFilters
        // 
        pnlFilters.Controls.Add(btnExport);
        pnlFilters.Controls.Add(btnGenerateReport);
        pnlFilters.Controls.Add(dtpEndDate);
        pnlFilters.Controls.Add(lblEndDate);
        pnlFilters.Controls.Add(dtpStartDate);
        pnlFilters.Controls.Add(lblStartDate);
        pnlFilters.Dock = DockStyle.Top;
        pnlFilters.Location = new Point(0, 0);
        pnlFilters.Name = "pnlFilters";
        pnlFilters.Size = new Size(1400, 80);
        pnlFilters.TabIndex = 1;
        // 
        // btnExport
        // 
        btnExport.Location = new Point(720, 14);
        btnExport.Name = "btnExport";
        btnExport.Size = new Size(120, 35);
        btnExport.TabIndex = 5;
        btnExport.Text = "Exportar Excel";
        btnExport.UseVisualStyleBackColor = true;
        btnExport.Click += btnExport_Click;
        // 
        // btnGenerateReport
        // 
        btnGenerateReport.Location = new Point(580, 14);
        btnGenerateReport.Name = "btnGenerateReport";
        btnGenerateReport.Size = new Size(120, 35);
        btnGenerateReport.TabIndex = 4;
        btnGenerateReport.Text = "Generar Reporte";
        btnGenerateReport.UseVisualStyleBackColor = true;
        btnGenerateReport.Click += btnGenerateReport_Click;
        // 
        // dtpEndDate
        // 
        dtpEndDate.Format = DateTimePickerFormat.Short;
        dtpEndDate.Location = new Point(400, 16);
        dtpEndDate.Name = "dtpEndDate";
        dtpEndDate.Size = new Size(150, 27);
        dtpEndDate.TabIndex = 3;
        // 
        // lblEndDate
        // 
        lblEndDate.AutoSize = true;
        lblEndDate.Location = new Point(300, 20);
        lblEndDate.Name = "lblEndDate";
        lblEndDate.Size = new Size(73, 20);
        lblEndDate.TabIndex = 2;
        lblEndDate.Text = "Fecha Fin:";
        // 
        // dtpStartDate
        // 
        dtpStartDate.Format = DateTimePickerFormat.Short;
        dtpStartDate.Location = new Point(120, 16);
        dtpStartDate.Name = "dtpStartDate";
        dtpStartDate.Size = new Size(150, 27);
        dtpStartDate.TabIndex = 1;
        // 
        // lblStartDate
        // 
        lblStartDate.AutoSize = true;
        lblStartDate.Location = new Point(14, 20);
        lblStartDate.Name = "lblStartDate";
        lblStartDate.Size = new Size(90, 20);
        lblStartDate.TabIndex = 0;
        lblStartDate.Text = "Fecha Inicio:";
        // 
        // pnlStats
        // 
        pnlStats.Controls.Add(lblTotalAmount);
        pnlStats.Controls.Add(lblTotalDetails);
        pnlStats.Controls.Add(lblTotalSales);
        pnlStats.Dock = DockStyle.Top;
        pnlStats.Location = new Point(0, 80);
        pnlStats.Name = "pnlStats";
        pnlStats.Size = new Size(1400, 50);
        pnlStats.TabIndex = 2;
        // 
        // lblTotalAmount
        // 
        lblTotalAmount.AutoSize = true;
        lblTotalAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTotalAmount.ForeColor = Color.Green;
        lblTotalAmount.Location = new Point(400, 15);
        lblTotalAmount.Name = "lblTotalAmount";
        lblTotalAmount.Size = new Size(162, 23);
        lblTotalAmount.TabIndex = 2;
        lblTotalAmount.Text = "Monto Total: $0.00";
        // 
        // lblTotalDetails
        // 
        lblTotalDetails.AutoSize = true;
        lblTotalDetails.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTotalDetails.Location = new Point(200, 15);
        lblTotalDetails.Name = "lblTotalDetails";
        lblTotalDetails.Size = new Size(163, 23);
        lblTotalDetails.TabIndex = 1;
        lblTotalDetails.Text = "Total de Detalles: 0";
        // 
        // lblTotalSales
        // 
        lblTotalSales.AutoSize = true;
        lblTotalSales.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTotalSales.Location = new Point(14, 15);
        lblTotalSales.Name = "lblTotalSales";
        lblTotalSales.Size = new Size(151, 23);
        lblTotalSales.TabIndex = 0;
        lblTotalSales.Text = "Total de Ventas: 0";
        // 
        // pnlReport
        // 
        pnlReport.Controls.Add(dgvReportDetails);
        pnlReport.Dock = DockStyle.Fill;
        pnlReport.Location = new Point(0, 130);
        pnlReport.Name = "pnlReport";
        pnlReport.Size = new Size(1400, 570);
        pnlReport.TabIndex = 3;
        // 
        // dgvReportDetails
        // 
        dgvReportDetails.AllowUserToAddRows = false;
        dgvReportDetails.AllowUserToDeleteRows = false;
        dgvReportDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvReportDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvReportDetails.Dock = DockStyle.Fill;
        dgvReportDetails.Location = new Point(0, 0);
        dgvReportDetails.Name = "dgvReportDetails";
        dgvReportDetails.ReadOnly = true;
        dgvReportDetails.RowHeadersWidth = 51;
        dgvReportDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvReportDetails.Size = new Size(1400, 570);
        dgvReportDetails.TabIndex = 0;
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Dock = DockStyle.Bottom;
        lblStatus.Location = new Point(0, 700);
        lblStatus.Name = "lblStatus";
        lblStatus.Padding = new Padding(11, 7, 0, 7);
        lblStatus.Size = new Size(11, 34);
        lblStatus.TabIndex = 4;
        // 
        // SalesReportForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1400, 734);
        Controls.Add(pnlReport);
        Controls.Add(pnlStats);
        Controls.Add(pnlFilters);
        Controls.Add(lblTitle);
        Controls.Add(lblStatus);
        Name = "SalesReportForm";
        Text = "Reporte de Ventas";
        Load += SalesReportForm_Load;
        pnlFilters.ResumeLayout(false);
        pnlFilters.PerformLayout();
        pnlStats.ResumeLayout(false);
        pnlStats.PerformLayout();
        pnlReport.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvReportDetails).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}

