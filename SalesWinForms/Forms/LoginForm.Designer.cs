using System.Drawing;
using System.Windows.Forms;

namespace SalesWinForms.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitle;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblPassword;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnCancel;
    private Label lblError;
    private Panel pnlMain;

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
        pnlMain = new Panel();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        btnLogin = new Button();
        btnCancel = new Button();
        lblError = new Label();
        pnlMain.SuspendLayout();
        SuspendLayout();
        
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.Location = new Point(14, 20);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(280, 37);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Sistema de Ventas";
        
        // 
        // pnlMain
        // 
        pnlMain.Controls.Add(lblError);
        pnlMain.Controls.Add(btnCancel);
        pnlMain.Controls.Add(btnLogin);
        pnlMain.Controls.Add(txtPassword);
        pnlMain.Controls.Add(lblPassword);
        pnlMain.Controls.Add(txtUsername);
        pnlMain.Controls.Add(lblUsername);
        pnlMain.Location = new Point(14, 70);
        pnlMain.Name = "pnlMain";
        pnlMain.Size = new Size(400, 250);
        pnlMain.TabIndex = 1;
        
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(20, 20);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(62, 20);
        lblUsername.TabIndex = 0;
        lblUsername.Text = "Usuario:";
        
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(20, 45);
        txtUsername.Margin = new Padding(3, 4, 3, 4);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(360, 27);
        txtUsername.TabIndex = 1;
        
        // 
        // lblPassword
        // 
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(20, 90);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(87, 20);
        lblPassword.TabIndex = 2;
        lblPassword.Text = "Contraseña:";
        
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(20, 115);
        txtPassword.Margin = new Padding(3, 4, 3, 4);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.Size = new Size(360, 27);
        txtPassword.TabIndex = 3;
        txtPassword.KeyPress += txtPassword_KeyPress;
        
        // 
        // btnLogin
        // 
        btnLogin.Location = new Point(20, 170);
        btnLogin.Margin = new Padding(3, 4, 3, 4);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(175, 40);
        btnLogin.TabIndex = 4;
        btnLogin.Text = "Iniciar Sesión";
        btnLogin.UseVisualStyleBackColor = true;
        btnLogin.Click += btnLogin_Click;
        
        // 
        // btnCancel
        // 
        btnCancel.Location = new Point(205, 170);
        btnCancel.Margin = new Padding(3, 4, 3, 4);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(175, 40);
        btnCancel.TabIndex = 5;
        btnCancel.Text = "Cancelar";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        
        // 
        // lblError
        // 
        lblError.AutoSize = true;
        lblError.ForeColor = Color.Red;
        lblError.Location = new Point(20, 220);
        lblError.Name = "lblError";
        lblError.Size = new Size(0, 20);
        lblError.TabIndex = 6;
        lblError.Visible = false;
        
        // 
        // LoginForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(430, 340);
        Controls.Add(pnlMain);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Iniciar Sesión";
        pnlMain.ResumeLayout(false);
        pnlMain.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}

