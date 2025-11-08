using SalesWinForms.Models;
using SalesWinForms.Services;

namespace SalesWinForms.Forms;

public partial class ProductsForm : Form
{
    private readonly ApiService _apiService;
    private List<InventoryProduct> _products = new();
    private InventoryProduct? _selectedProduct;

    public ProductsForm(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void ProductsForm_Load(object sender, EventArgs e)
    {
        await LoadProductsAsync();
    }

    private async Task LoadProductsAsync()
    {
        const int maxRetries = 3;
        const int delayMs = 2000; // 2 segundos entre intentos

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

                _products = await _apiService.GetProductsAsync();
                dgvProducts.DataSource = _products;
                
                lblStatus.Text = $"Total de productos: {_products.Count}";
                lblStatus.ForeColor = Color.Black;
                return; // Éxito, salir del método
            }
            catch (Exception ex)
            {
                if (attempt == maxRetries)
                {
                    // Último intento fallido
                    var result = MessageBox.Show(
                        $"Error al cargar productos después de {maxRetries} intentos:\n\n{ex.Message}\n\n" +
                        "¿La API está ejecutándose?\n" +
                        "Verifica que la API esté corriendo en https://localhost:7263",
                        "Error de Conexión",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error);

                    if (result == DialogResult.Retry)
                    {
                        attempt = 0; // Reiniciar contador
                        continue;
                    }

                    lblStatus.Text = "Error: No se pudo conectar con la API";
                    lblStatus.ForeColor = Color.Red;
                }
                else
                {
                    // Continuar con el siguiente intento
                    continue;
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                btnRefresh.Enabled = true;
            }
        }
    }

    private void dgvProducts_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count > 0)
        {
            var selectedRow = dgvProducts.SelectedRows[0];
            var productId = (int)selectedRow.Cells["Id"].Value;
            _selectedProduct = _products.FirstOrDefault(p => p.Id == productId);
            
            if (_selectedProduct != null)
            {
                LoadProductDetails(_selectedProduct);
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
        else
        {
            ClearProductDetails();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
    }

    private void LoadProductDetails(InventoryProduct product)
    {
        txtId.Text = product.Id.ToString();
        txtName.Text = product.Name;
        txtPrice.Text = product.Price?.ToString("F2") ?? "";
        txtStock.Text = product.Stock?.ToString() ?? "";
        txtImageUrl.Text = product.Image ?? "";
        
        if (!string.IsNullOrEmpty(product.Image))
        {
            try
            {
                pbProductImage.LoadAsync(product.Image);
            }
            catch
            {
                pbProductImage.Image = null;
            }
        }
        else
        {
            pbProductImage.Image = null;
        }
    }

    private void ClearProductDetails()
    {
        txtId.Clear();
        txtName.Clear();
        txtPrice.Clear();
        txtStock.Clear();
        txtImageUrl.Clear();
        pbProductImage.Image = null;
        _selectedProduct = null;
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        ClearProductDetails();
        txtId.Text = "Nuevo";
        txtName.Focus();
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateInputs())
            return;

        try
        {
            Cursor = Cursors.WaitCursor;
            btnSave.Enabled = false;

            if (_selectedProduct == null)
            {
                // Crear nuevo producto
                var createDto = new CreateInventoryProductDto
                {
                    Name = txtName.Text.Trim(),
                    Price = decimal.TryParse(txtPrice.Text, out var price) ? price : null,
                    Stock = int.TryParse(txtStock.Text, out var stock) ? stock : null,
                    Image = txtImageUrl.Text.Trim()
                };

                string? imagePath = null;
                if (!string.IsNullOrEmpty(txtImagePath.Text))
                    imagePath = txtImagePath.Text;

                var newProduct = await _apiService.CreateProductAsync(createDto, imagePath);
                MessageBox.Show($"Producto '{newProduct.Name}' creado exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Actualizar producto existente
                var updateDto = new UpdateInventoryProductDto
                {
                    Name = txtName.Text.Trim(),
                    Price = decimal.TryParse(txtPrice.Text, out var price) ? price : null,
                    Stock = int.TryParse(txtStock.Text, out var stock) ? stock : null,
                    Image = txtImageUrl.Text.Trim()
                };

                string? imagePath = null;
                if (!string.IsNullOrEmpty(txtImagePath.Text))
                    imagePath = txtImagePath.Text;

                var updatedProduct = await _apiService.UpdateProductAsync(_selectedProduct.Id, updateDto, imagePath);
                MessageBox.Show($"Producto '{updatedProduct.Name}' actualizado exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            txtImagePath.Clear();
            await LoadProductsAsync();
            ClearProductDetails();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al guardar producto: {ex.Message}", "Error",
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
        if (_selectedProduct == null)
            return;

        var result = MessageBox.Show(
            $"¿Está seguro de eliminar el producto '{_selectedProduct.Name}'?",
            "Confirmar eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result != DialogResult.Yes)
            return;

        try
        {
            Cursor = Cursors.WaitCursor;
            btnDelete.Enabled = false;

            var deleted = await _apiService.DeleteProductAsync(_selectedProduct.Id);
            if (deleted)
            {
                MessageBox.Show("Producto eliminado exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductsAsync();
                ClearProductDetails();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar el producto.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
            btnDelete.Enabled = true;
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (_selectedProduct != null)
        {
            txtName.Focus();
        }
    }

    private async void btnRefresh_Click(object sender, EventArgs e)
    {
        await LoadProductsAsync();
    }

    private void btnSelectImage_Click(object sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.gif;*.webp",
            Title = "Seleccionar imagen del producto"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            txtImagePath.Text = openFileDialog.FileName;
            try
            {
                pbProductImage.Image = Image.FromFile(openFileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar imagen: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private bool ValidateInputs()
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("El nombre del producto es requerido.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtName.Focus();
            return false;
        }

        if (!string.IsNullOrWhiteSpace(txtPrice.Text) && 
            !decimal.TryParse(txtPrice.Text, out _))
        {
            MessageBox.Show("El precio debe ser un número válido.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPrice.Focus();
            return false;
        }

        if (!string.IsNullOrWhiteSpace(txtStock.Text) && 
            !int.TryParse(txtStock.Text, out _))
        {
            MessageBox.Show("El stock debe ser un número entero válido.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtStock.Focus();
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

