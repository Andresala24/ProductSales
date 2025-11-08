using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using SalesWinForms.Models;
using SalesWinForms.Services;

namespace SalesWinForms.Forms;

public partial class SalesReportForm : Form
{
    private readonly ApiService _apiService;
    private List<Sale> _sales = new();

    public SalesReportForm(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private void SalesReportForm_Load(object sender, EventArgs e)
    {
        // Establecer fechas por defecto: último mes
        dtpStartDate.Value = DateTime.Now.AddMonths(-1);
        dtpEndDate.Value = DateTime.Now;
        
        // Configurar columnas del DataGridView
        ConfigureDataGridView();
    }

    private void ConfigureDataGridView()
    {
        dgvReportDetails.AutoGenerateColumns = false;
        dgvReportDetails.Columns.Clear();

        dgvReportDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "SaleId",
            HeaderText = "ID Venta",
            DataPropertyName = "SaleId",
            Width = 80
        });

        dgvReportDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "SaleDate",
            HeaderText = "Fecha Venta",
            DataPropertyName = "SaleDate",
            Width = 150,
            DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
        });

        dgvReportDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "CreationUser",
            HeaderText = "Usuario",
            DataPropertyName = "CreationUser",
            Width = 150
        });

        dgvReportDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "ProductName",
            HeaderText = "Producto",
            DataPropertyName = "ProductName",
            Width = 200
        });

        dgvReportDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Quantity",
            HeaderText = "Cantidad",
            DataPropertyName = "Quantity",
            Width = 100,
            DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
        });

        dgvReportDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "UnitPrice",
            HeaderText = "Precio Unitario",
            DataPropertyName = "UnitPrice",
            Width = 120,
            DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
        });

        dgvReportDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Subtotal",
            HeaderText = "Subtotal",
            DataPropertyName = "Subtotal",
            Width = 120,
            DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
        });

        dgvReportDetails.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "SaleTotal",
            HeaderText = "Total Venta",
            DataPropertyName = "SaleTotal",
            Width = 120,
            DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
    }

    private async void btnGenerateReport_Click(object sender, EventArgs e)
    {
        if (dtpStartDate.Value > dtpEndDate.Value)
        {
            MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            Cursor = Cursors.WaitCursor;
            btnGenerateReport.Enabled = false;
            lblStatus.Text = "Generando reporte...";
            lblStatus.ForeColor = Color.Blue;

            var startDate = dtpStartDate.Value.Date;
            var endDate = dtpEndDate.Value.Date;

            _sales = await _apiService.GetSalesByDateRangeAsync(startDate, endDate);

            // Convertir ventas a detalles planos para el reporte
            var reportData = new List<SalesReportDetail>();
            decimal totalGeneral = 0;

            foreach (var sale in _sales)
            {
                foreach (var detail in sale.SalesDetails)
                {
                    reportData.Add(new SalesReportDetail
                    {
                        SaleId = sale.Id,
                        SaleDate = sale.CreationDate,
                        CreationUser = sale.CreationUser,
                        ProductName = detail.Product?.Name ?? $"ID: {detail.ProductId}",
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice,
                        Subtotal = detail.Quantity * detail.UnitPrice,
                        SaleTotal = sale.Total
                    });
                }
                totalGeneral += sale.Total;
            }

            dgvReportDetails.DataSource = reportData;
            
            // Actualizar estadísticas
            lblTotalSales.Text = $"Total de Ventas: {_sales.Count}";
            lblTotalDetails.Text = $"Total de Detalles: {reportData.Count}";
            lblTotalAmount.Text = $"Monto Total: {totalGeneral:C}";

            lblStatus.Text = $"Reporte generado: {reportData.Count} detalles encontrados";
            lblStatus.ForeColor = Color.Green;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al generar reporte: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            lblStatus.Text = "Error al generar reporte";
            lblStatus.ForeColor = Color.Red;
        }
        finally
        {
            Cursor = Cursors.Default;
            btnGenerateReport.Enabled = true;
        }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        if (dgvReportDetails.Rows.Count == 0)
        {
            MessageBox.Show("No hay datos para exportar. Genere un reporte primero.", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            using var saveDialog = new SaveFileDialog
            {
                Filter = "Archivos Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*",
                FileName = $"Reporte_Ventas_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                Title = "Guardar reporte como Excel"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(saveDialog.FileName);
                MessageBox.Show("Reporte exportado exitosamente a Excel.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al exportar reporte: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ExportToExcel(string filePath)
    {
        // Configurar licencia de EPPlus (gratuita para uso no comercial)
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Reporte de Ventas");

        // Obtener los datos del DataSource
        if (dgvReportDetails.DataSource is List<SalesReportDetail> reportData)
        {
            // Configurar encabezados
            var headers = new[] { "ID Venta", "Fecha Venta", "Usuario", "Producto", "Cantidad", "Precio Unitario", "Subtotal", "Total Venta" };
            
            // Estilo para encabezados
            var headerRange = worksheet.Cells[1, 1, 1, headers.Length];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(68, 114, 196));
            headerRange.Style.Font.Color.SetColor(Color.White);
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            // Escribir encabezados
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i];
            }

            // Escribir datos
            int row = 2;
            foreach (var detail in reportData)
            {
                worksheet.Cells[row, 1].Value = detail.SaleId;
                worksheet.Cells[row, 2].Value = detail.SaleDate;
                worksheet.Cells[row, 2].Style.Numberformat.Format = "dd/mm/yyyy hh:mm";
                worksheet.Cells[row, 3].Value = detail.CreationUser;
                worksheet.Cells[row, 4].Value = detail.ProductName;
                worksheet.Cells[row, 5].Value = detail.Quantity;
                worksheet.Cells[row, 6].Value = detail.UnitPrice;
                worksheet.Cells[row, 6].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[row, 7].Value = detail.Subtotal;
                worksheet.Cells[row, 7].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[row, 8].Value = detail.SaleTotal;
                worksheet.Cells[row, 8].Style.Numberformat.Format = "#,##0.00";

                // Alternar colores de fila para mejor legibilidad
                if (row % 2 == 0)
                {
                    var rowRange = worksheet.Cells[row, 1, row, headers.Length];
                    rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rowRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));
                }

                row++;
            }

            // Ajustar ancho de columnas automáticamente
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Agregar bordes a todas las celdas con datos
            var dataRange = worksheet.Cells[1, 1, row - 1, headers.Length];
            dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Top.Color.SetColor(Color.Black);
            dataRange.Style.Border.Bottom.Color.SetColor(Color.Black);
            dataRange.Style.Border.Left.Color.SetColor(Color.Black);
            dataRange.Style.Border.Right.Color.SetColor(Color.Black);

            // Alinear columnas numéricas a la derecha
            worksheet.Cells[2, 1, row - 1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // ID Venta
            worksheet.Cells[2, 5, row - 1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right; // Cantidad
            worksheet.Cells[2, 6, row - 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right; // Precios y totales

            // Agregar fila de totales
            if (reportData.Count > 0)
            {
                row++;
                worksheet.Cells[row, 4].Value = "TOTALES:";
                worksheet.Cells[row, 4].Style.Font.Bold = true;
                worksheet.Cells[row, 5].Formula = $"SUM(E2:E{row - 1})";
                worksheet.Cells[row, 5].Style.Font.Bold = true;
                worksheet.Cells[row, 7].Formula = $"SUM(G2:G{row - 1})";
                worksheet.Cells[row, 7].Style.Font.Bold = true;
                worksheet.Cells[row, 7].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells[row, 8].Formula = $"SUM(H2:H{row - 1})";
                worksheet.Cells[row, 8].Style.Font.Bold = true;
                worksheet.Cells[row, 8].Style.Numberformat.Format = "#,##0.00";
                
                var totalRange = worksheet.Cells[row, 1, row, headers.Length];
                totalRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                totalRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(217, 225, 242));
                totalRange.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                totalRange.Style.Border.Top.Color.SetColor(Color.Black);
            }

            // Congelar primera fila (encabezados)
            worksheet.View.FreezePanes(2, 1);
        }
        else
        {
            throw new Exception("No se encontraron datos para exportar.");
        }

        // Guardar el archivo
        var fileInfo = new FileInfo(filePath);
        package.SaveAs(fileInfo);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _apiService?.Dispose();
        base.OnFormClosing(e);
    }
}

// Clase auxiliar para el reporte
public class SalesReportDetail
{
    public int SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public string CreationUser { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public decimal SaleTotal { get; set; }
}

