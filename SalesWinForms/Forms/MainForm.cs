using SalesWinForms.Forms;
using SalesWinForms.Services;

namespace SalesWinForms.Forms;

public partial class MainForm : Form
{
    private ProductsForm? _productsForm;
    private SalesForm? _salesForm;
    private SalesReportForm? _salesReportForm;
    private readonly ApiService _apiService;

    public MainForm(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private void menuItemProducts_Click(object sender, EventArgs e)
    {
        if (_productsForm == null || _productsForm.IsDisposed)
        {
            _productsForm = new ProductsForm(_apiService)
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            _productsForm.FormClosed += (s, args) => _productsForm = null;
        }
        _productsForm.Show();
        _productsForm.BringToFront();
    }

    private void menuItemSales_Click(object sender, EventArgs e)
    {
        if (_salesForm == null || _salesForm.IsDisposed)
        {
            _salesForm = new SalesForm(_apiService)
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            _salesForm.FormClosed += (s, args) => _salesForm = null;
        }
        _salesForm.Show();
        _salesForm.BringToFront();
    }

    private void menuItemReports_Click(object sender, EventArgs e)
    {
        if (_salesReportForm == null || _salesReportForm.IsDisposed)
        {
            _salesReportForm = new SalesReportForm(_apiService)
            {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };
            _salesReportForm.FormClosed += (s, args) => _salesReportForm = null;
        }
        _salesReportForm.Show();
        _salesReportForm.BringToFront();
    }

    private void menuItemExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        // Abrir formulario de productos por defecto
        menuItemProducts_Click(sender, e);
    }
}

