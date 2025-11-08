using SalesWinForms.Models;
using SalesWinForms.Services;

namespace SalesWinForms.Forms;

public partial class SalesForm : Form
{
    private readonly ApiService _apiService;
    private List<Sale> _sales = new();
    private Sale? _selectedSale;
    private List<InventoryProduct> _products = new();

    public SalesForm(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void SalesForm_Load(object sender, EventArgs e)
    {
        await LoadProductsAsync();
        await LoadSalesAsync();
    }

    private async Task LoadProductsAsync()
    {
        try
        {
            _products = await _apiService.GetProductsAsync();
            cmbProduct.DataSource = _products;
            cmbProduct.DisplayMember = "Name";
            cmbProduct.ValueMember = "Id";
            cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CmbProduct_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbProduct.SelectedItem is InventoryProduct product)
        {
            // Cargar precio automáticamente si existe
            if (product.Price.HasValue)
            {
                txtUnitPrice.Text = product.Price.Value.ToString("F2");
            }
            else
            {
                txtUnitPrice.Clear();
            }
            
            // Mostrar stock disponible
            if (product.Stock.HasValue)
            {
                lblStockValue.Text = $"Stock disponible: {product.Stock.Value}";
                lblStockValue.ForeColor = product.Stock.Value > 0 ? Color.Green : Color.Red;
            }
            else
            {
                lblStockValue.Text = "Stock disponible: Sin límite";
                lblStockValue.ForeColor = Color.Blue;
            }
            
            // Limpiar cantidad al cambiar producto
            txtQuantity.Clear();
        }
    }

    private async Task LoadSalesAsync()
    {
        const int maxRetries = 3;
        const int delayMs = 2000;

        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnRefresh.Enabled = false;

                if (attempt > 1)
                {
                    lblStatus.Text = $"Reintentando conexión... (Intento {attempt}/{maxRetries})";
                    lblStatus.ForeColor = Color.Orange;
                    await Task.Delay(delayMs);
                }
                else
                {
                    lblStatus.Text = "Conectando con la API...";
                    lblStatus.ForeColor = Color.Blue;
                }

                _sales = await _apiService.GetSalesAsync();
                dgvSales.DataSource = _sales;
                
                lblStatus.Text = $"Total de ventas: {_sales.Count}";
                lblStatus.ForeColor = Color.Black;
                return;
            }
            catch (Exception ex)
            {
                if (attempt == maxRetries)
                {
                    var result = MessageBox.Show(
                        $"Error al cargar ventas después de {maxRetries} intentos:\n\n{ex.Message}\n\n" +
                        "¿La API está ejecutándose?",
                        "Error de Conexión",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error);

                    if (result == DialogResult.Retry)
                    {
                        attempt = 0;
                        continue;
                    }

                    lblStatus.Text = "Error: No se pudo conectar con la API";
                    lblStatus.ForeColor = Color.Red;
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                btnRefresh.Enabled = true;
            }
        }
    }

    private void dgvSales_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvSales.SelectedRows.Count > 0)
        {
            var selectedRow = dgvSales.SelectedRows[0];
            var saleId = (int)selectedRow.Cells["Id"].Value;
            _selectedSale = _sales.FirstOrDefault(s => s.Id == saleId);
            
            if (_selectedSale != null)
            {
                LoadSaleDetails(_selectedSale);
                btnDelete.Enabled = true;
            }
        }
        else
        {
            ClearSaleDetails();
            btnDelete.Enabled = false;
        }
    }

    private void LoadSaleDetails(Sale sale)
    {
        txtId.Text = sale.Id.ToString();
        txtCreationDate.Text = sale.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
        txtCreationUser.Text = sale.CreationUser;
        txtTotal.Text = sale.Total.ToString("C");
        
        dgvSaleDetails.DataSource = sale.SalesDetails.Select(d => new
        {
            Producto = d.Product?.Name ?? $"ID: {d.ProductId}",
            Cantidad = d.Quantity,
            PrecioUnitario = d.UnitPrice.ToString("C"),
            Subtotal = (d.Quantity * d.UnitPrice).ToString("C")
        }).ToList();
    }

    private void ClearSaleDetails()
    {
        txtId.Clear();
        txtCreationDate.Clear();
        txtCreationUser.Clear();
        txtTotal.Clear();
        dgvSaleDetails.DataSource = null;
        _selectedSale = null;
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        ClearSaleDetails();
        txtId.Text = "Nuevo";
        txtNewCreationUser.Text = Environment.UserName;
        dgvNewSaleDetails.Rows.Clear();
        txtNewTotal.Text = "0.00";
        lblStockValue.Text = "Stock disponible: -";
        lblStockValue.ForeColor = Color.Black;
        cmbProduct.SelectedIndex = -1;
        txtQuantity.Clear();
        txtUnitPrice.Clear();
    }

    private void btnAddDetail_Click(object sender, EventArgs e)
    {
        if (cmbProduct.SelectedItem == null)
        {
            MessageBox.Show("Seleccione un producto", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!int.TryParse(txtQuantity.Text, out var quantity) || quantity <= 0)
        {
            MessageBox.Show("La cantidad debe ser mayor a 0", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtQuantity.Focus();
            return;
        }

        if (!decimal.TryParse(txtUnitPrice.Text, out var unitPrice) || unitPrice < 0)
        {
            MessageBox.Show("El precio unitario debe ser mayor o igual a 0", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtUnitPrice.Focus();
            return;
        }

        var product = (InventoryProduct)cmbProduct.SelectedItem;
        
        // Validar stock disponible
        if (product.Stock.HasValue)
        {
            // Calcular cantidad ya agregada de este producto en la venta actual
            int cantidadYaAgregada = 0;
            foreach (DataGridViewRow row in dgvNewSaleDetails.Rows)
            {
                if (row.Cells[0].Value != null && (int)row.Cells[0].Value == product.Id)
                {
                    cantidadYaAgregada += (int)row.Cells[2].Value;
                }
            }
            
            int cantidadTotal = cantidadYaAgregada + quantity;
            
            if (cantidadTotal > product.Stock.Value)
            {
                MessageBox.Show(
                    $"Stock insuficiente.\n\n" +
                    $"Stock disponible: {product.Stock.Value}\n" +
                    $"Ya agregado en esta venta: {cantidadYaAgregada}\n" +
                    $"Intenta agregar: {quantity}\n" +
                    $"Total requerido: {cantidadTotal}\n\n" +
                    $"Solo puedes agregar {product.Stock.Value - cantidadYaAgregada} unidades más.",
                    "Validación de Stock",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return;
            }
        }

        var subtotal = quantity * unitPrice;

        // Verificar si el producto ya está en la venta
        bool productoExiste = false;
        int rowIndexExistente = -1;
        foreach (DataGridViewRow row in dgvNewSaleDetails.Rows)
        {
            if (row.Cells[0].Value != null && (int)row.Cells[0].Value == product.Id)
            {
                // Si el precio es diferente, agregar como nueva fila
                if (decimal.Parse(row.Cells[3].Value.ToString()!) != unitPrice)
                {
                    break;
                }
                productoExiste = true;
                rowIndexExistente = row.Index;
                break;
            }
        }

        if (productoExiste && rowIndexExistente >= 0)
        {
            // Sumar cantidad a la fila existente
            var row = dgvNewSaleDetails.Rows[rowIndexExistente];
            int cantidadAnterior = (int)row.Cells[2].Value;
            int nuevaCantidad = cantidadAnterior + quantity;
            row.Cells[2].Value = nuevaCantidad;
            row.Cells[4].Value = nuevaCantidad * unitPrice;
        }
        else
        {
            // Agregar nueva fila
            var rowIndex = dgvNewSaleDetails.Rows.Add(
                product.Id,
                product.Name,
                quantity,
                unitPrice,
                subtotal
            );
            
            // Hacer que la última columna (subtotal) sea solo lectura
            dgvNewSaleDetails.Rows[rowIndex].Cells[4].ReadOnly = true;
        }

        CalculateNewTotal();
        txtQuantity.Clear();
        // Mantener el precio para facilitar agregar más del mismo producto
    }

    private void CalculateNewTotal()
    {
        decimal total = 0;
        foreach (DataGridViewRow row in dgvNewSaleDetails.Rows)
        {
            if (row.Cells[4].Value != null && decimal.TryParse(row.Cells[4].Value.ToString(), out var subtotal))
            {
                total += subtotal;
            }
        }
        txtNewTotal.Text = total.ToString("C");
    }

    private void dgvNewSaleDetails_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
    {
        CalculateNewTotal();
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateNewSale())
            return;

        try
        {
            Cursor = Cursors.WaitCursor;
            btnSave.Enabled = false;

            var saleDto = new CreateSaleDto
            {
                CreationUser = txtNewCreationUser.Text.Trim(),
                SalesDetails = new List<SalesDetailDto>()
            };

            foreach (DataGridViewRow row in dgvNewSaleDetails.Rows)
            {
                saleDto.SalesDetails.Add(new SalesDetailDto
                {
                    ProductId = (int)row.Cells[0].Value,
                    Quantity = (int)row.Cells[2].Value,
                    UnitPrice = decimal.Parse(row.Cells[3].Value.ToString()!)
                });
            }

            var newSale = await _apiService.CreateSaleAsync(saleDto);
            MessageBox.Show($"Venta #{newSale.Id} creada exitosamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Recargar productos para actualizar stock
            await LoadProductsAsync();
            await LoadSalesAsync();
            btnNew_Click(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al crear venta: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
            btnSave.Enabled = true;
        }
    }

    private async void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selectedSale == null)
            return;

        var result = MessageBox.Show(
            $"¿Está seguro de eliminar la venta #{_selectedSale.Id}?",
            "Confirmar eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result != DialogResult.Yes)
            return;

        try
        {
            Cursor = Cursors.WaitCursor;
            btnDelete.Enabled = false;

            var deleted = await _apiService.DeleteSaleAsync(_selectedSale.Id);
            if (deleted)
            {
                MessageBox.Show("Venta eliminada exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadSalesAsync();
                ClearSaleDetails();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar la venta.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al eliminar venta: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
            btnDelete.Enabled = true;
        }
    }

    private async void btnRefresh_Click(object sender, EventArgs e)
    {
        await LoadSalesAsync();
    }

    private bool ValidateNewSale()
    {
        if (string.IsNullOrWhiteSpace(txtNewCreationUser.Text))
        {
            MessageBox.Show("El usuario de creación es requerido.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtNewCreationUser.Focus();
            return false;
        }

        if (dgvNewSaleDetails.Rows.Count == 0)
        {
            MessageBox.Show("La venta debe tener al menos un detalle.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _apiService?.Dispose();
        base.OnFormClosing(e);
    }
}

