using SalesWinForms.Models;
using SalesWinForms.Services;

namespace SalesWinForms.Forms;

public partial class LoginForm : Form
{
    private readonly ApiService _apiService;
    public bool IsAuthenticated { get; private set; }
    public string? Username { get; private set; }
    public ApiService? AuthenticatedApiService { get; private set; }

    public LoginForm()
    {
        InitializeComponent();
        _apiService = new ApiService();
        IsAuthenticated = false;
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show("Por favor ingrese usuario y contraseña.", "Validación",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            Cursor = Cursors.WaitCursor;
            btnLogin.Enabled = false;
            lblError.Text = "";
            lblError.Visible = false;

            var loginDto = new LoginDto
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };

            var response = await _apiService.LoginAsync(loginDto);

            if (response == null)
            {
                lblError.Text = "Usuario o contraseña incorrectos";
                lblError.Visible = true;
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            IsAuthenticated = true;
            Username = response.Username;
            AuthenticatedApiService = _apiService; // Compartir la instancia con el token
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            lblError.Text = $"Error: {ex.Message}";
            lblError.Visible = true;
        }
        finally
        {
            Cursor = Cursors.Default;
            btnLogin.Enabled = true;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }

    private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            btnLogin_Click(sender, e);
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        // No disponer del ApiService si se autenticó exitosamente, ya que se comparte con MainForm
        // Solo disponer si se cancela el login
        if (!IsAuthenticated && DialogResult != DialogResult.OK)
        {
            _apiService?.Dispose();
        }
        base.OnFormClosing(e);
    }
}

