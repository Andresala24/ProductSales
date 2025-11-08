using System.Drawing;
using System.Windows.Forms;

namespace SalesWinForms.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private MenuStrip menuStrip;
    private ToolStripMenuItem menuItemFile;
    private ToolStripMenuItem menuItemProducts;
    private ToolStripMenuItem menuItemSales;
    private ToolStripMenuItem menuItemReports;
    private ToolStripMenuItem menuItemExit;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblStatus;

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
        menuStrip = new MenuStrip();
        menuItemFile = new ToolStripMenuItem();
        menuItemProducts = new ToolStripMenuItem();
        menuItemSales = new ToolStripMenuItem();
        menuItemReports = new ToolStripMenuItem();
        menuItemExit = new ToolStripMenuItem();
        statusStrip = new StatusStrip();
        lblStatus = new ToolStripStatusLabel();
        menuStrip.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        
        // 
        // menuStrip
        // 
        menuStrip.ImageScalingSize = new Size(20, 20);
        menuStrip.Items.AddRange(new ToolStripItem[] {
            menuItemFile
        });
        menuStrip.Location = new Point(0, 0);
        menuStrip.Name = "menuStrip";
        menuStrip.Size = new Size(1200, 28);
        menuStrip.TabIndex = 0;
        menuStrip.Text = "menuStrip1";
        
        // 
        // menuItemFile
        // 
        menuItemFile.DropDownItems.AddRange(new ToolStripItem[] {
            menuItemProducts,
            menuItemSales,
            menuItemReports,
            new ToolStripSeparator(),
            menuItemExit
        });
        menuItemFile.Name = "menuItemFile";
        menuItemFile.Size = new Size(60, 24);
        menuItemFile.Text = "Menú";
        
        // 
        // menuItemProducts
        // 
        menuItemProducts.Name = "menuItemProducts";
        menuItemProducts.Size = new Size(180, 26);
        menuItemProducts.Text = "Productos";
        menuItemProducts.Click += menuItemProducts_Click;
        
        // 
        // menuItemSales
        // 
        menuItemSales.Name = "menuItemSales";
        menuItemSales.Size = new Size(180, 26);
        menuItemSales.Text = "Ventas";
        menuItemSales.Click += menuItemSales_Click;
        
        // 
        // menuItemReports
        // 
        menuItemReports.Name = "menuItemReports";
        menuItemReports.Size = new Size(180, 26);
        menuItemReports.Text = "Reportes";
        menuItemReports.Click += menuItemReports_Click;
        
        // 
        // menuItemExit
        // 
        menuItemExit.Name = "menuItemExit";
        menuItemExit.Size = new Size(180, 26);
        menuItemExit.Text = "Salir";
        menuItemExit.Click += menuItemExit_Click;
        
        // 
        // statusStrip
        // 
        statusStrip.ImageScalingSize = new Size(20, 20);
        statusStrip.Items.AddRange(new ToolStripItem[] {
            lblStatus
        });
        statusStrip.Location = new Point(0, 678);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1200, 22);
        statusStrip.TabIndex = 1;
        statusStrip.Text = "statusStrip1";
        
        // 
        // lblStatus
        // 
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(1185, 17);
        lblStatus.Spring = true;
        lblStatus.Text = "Sistema de Gestión de Ventas e Inventario";
        lblStatus.TextAlign = ContentAlignment.MiddleLeft;
        
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1200, 700);
        Controls.Add(statusStrip);
        Controls.Add(menuStrip);
        IsMdiContainer = true;
        MainMenuStrip = menuStrip;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Sistema de Gestión de Ventas e Inventario";
        WindowState = FormWindowState.Maximized;
        Load += MainForm_Load;
        menuStrip.ResumeLayout(false);
        menuStrip.PerformLayout();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}

