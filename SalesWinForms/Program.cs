using SalesWinForms.Forms;

namespace SalesWinForms;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        
        // Mostrar formulario de login primero
        using var loginForm = new LoginForm();
        if (loginForm.ShowDialog() == DialogResult.OK && loginForm.AuthenticatedApiService != null)
        {
            // Si el login fue exitoso, abrir el formulario principal con el ApiService autenticado
            Application.Run(new MainForm(loginForm.AuthenticatedApiService));
        }
    }    
}