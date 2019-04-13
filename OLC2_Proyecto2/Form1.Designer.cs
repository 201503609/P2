namespace OLC2_Proyecto2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crearArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarPestañaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.erroresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lexicosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sintacticosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.semanticosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.astToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtEntrada = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtEntrada);
            this.panel1.Location = new System.Drawing.Point(12, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 280);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 362);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(617, 178);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(609, 149);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(609, 149);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.erroresToolStripMenuItem,
            this.compilarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(641, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crearArchivoToolStripMenuItem,
            this.abrirArchivoToolStripMenuItem,
            this.guardarArchivoToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.eliminarPestañaToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // crearArchivoToolStripMenuItem
            // 
            this.crearArchivoToolStripMenuItem.Name = "crearArchivoToolStripMenuItem";
            this.crearArchivoToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.crearArchivoToolStripMenuItem.Text = "Crear Archivo";
            // 
            // abrirArchivoToolStripMenuItem
            // 
            this.abrirArchivoToolStripMenuItem.Name = "abrirArchivoToolStripMenuItem";
            this.abrirArchivoToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.abrirArchivoToolStripMenuItem.Text = "Abrir Archivo";
            // 
            // guardarArchivoToolStripMenuItem
            // 
            this.guardarArchivoToolStripMenuItem.Name = "guardarArchivoToolStripMenuItem";
            this.guardarArchivoToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.guardarArchivoToolStripMenuItem.Text = "Guardar Archivo";
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.guardarComoToolStripMenuItem.Text = "Guardar Como";
            // 
            // eliminarPestañaToolStripMenuItem
            // 
            this.eliminarPestañaToolStripMenuItem.Name = "eliminarPestañaToolStripMenuItem";
            this.eliminarPestañaToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.eliminarPestañaToolStripMenuItem.Text = "Eliminar Pestaña";
            // 
            // erroresToolStripMenuItem
            // 
            this.erroresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lexicosToolStripMenuItem,
            this.sintacticosToolStripMenuItem,
            this.semanticosToolStripMenuItem,
            this.astToolStripMenuItem});
            this.erroresToolStripMenuItem.Name = "erroresToolStripMenuItem";
            this.erroresToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.erroresToolStripMenuItem.Text = "Errores";
            // 
            // lexicosToolStripMenuItem
            // 
            this.lexicosToolStripMenuItem.Name = "lexicosToolStripMenuItem";
            this.lexicosToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.lexicosToolStripMenuItem.Text = "Lexicos";
            // 
            // sintacticosToolStripMenuItem
            // 
            this.sintacticosToolStripMenuItem.Name = "sintacticosToolStripMenuItem";
            this.sintacticosToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.sintacticosToolStripMenuItem.Text = "Sintacticos";
            // 
            // semanticosToolStripMenuItem
            // 
            this.semanticosToolStripMenuItem.Name = "semanticosToolStripMenuItem";
            this.semanticosToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.semanticosToolStripMenuItem.Text = "Semanticos";
            // 
            // astToolStripMenuItem
            // 
            this.astToolStripMenuItem.Name = "astToolStripMenuItem";
            this.astToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.astToolStripMenuItem.Text = "AST";
            this.astToolStripMenuItem.Click += new System.EventHandler(this.astToolStripMenuItem_Click);
            // 
            // compilarToolStripMenuItem
            // 
            this.compilarToolStripMenuItem.Name = "compilarToolStripMenuItem";
            this.compilarToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.compilarToolStripMenuItem.Text = "Compilar";
            this.compilarToolStripMenuItem.Click += new System.EventHandler(this.compilarToolStripMenuItem_Click);
            // 
            // txtEntrada
            // 
            this.txtEntrada.Location = new System.Drawing.Point(0, 0);
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(619, 280);
            this.txtEntrada.TabIndex = 0;
            this.txtEntrada.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 552);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crearArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarPestañaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem erroresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lexicosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sintacticosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem semanticosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem astToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compilarToolStripMenuItem;
        private System.Windows.Forms.RichTextBox txtEntrada;
    }
}

