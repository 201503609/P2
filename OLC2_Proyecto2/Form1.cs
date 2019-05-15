using OLC2_Proyecto2.CSS.templated_introspect;
using OLC2_Proyecto2.Gramatica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLC2_Proyecto2
{
    public partial class Form1 : Form
    {
        public OpenFileDialog abrir = new OpenFileDialog();
        public static String ruta = "";
        public static int contadorDG = 0;

        public Form1()
        {
            InitializeComponent();
        }

                //BOTON QUE COMPILA
        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.TabCount == 0)
                {
                    MessageBox.Show("Click on button1 to create a new tab.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ErroresSem n = new ErroresSem();
                    n.limpiarArregloSem();
                    ErroresLex el = new ErroresLex();
                    el.limpiarArregloLex();
                    ErroresSin sin = new ErroresSin();
                    sin.limpiarArregloSin();


                    int selectedTab = tabControl1.SelectedIndex;
                    Control ctrl = tabControl1.Controls[selectedTab].Controls[0];
                    RichTextBox rtb = ctrl as RichTextBox;
                    string cadena = rtb.Text;

                    //MessageBox.Show(cadena);
                    Recorrido.impresiones = "";
                    txtConsola.Text = "";
                    
                    bool resultado = Sintactico.analizar(cadena);
                    if (!resultado)
                        MessageBox.Show("Existe algun error Lexico o Sintactico en el archivo");
                    else if (ErroresSem.erroreSeman.Count > 0)
                        MessageBox.Show("Existe algun error Semantico en el archivo");
                    else
                    {
                        txtConsola.Text = Recorrido.impresiones;

                        //DataTable dt = new DataTable();
                        //dt.Rows.Add(dt.NewRow());
                        //dataGridView1.DataSource = dt;
                        //dataGridView1.Rows[0].Visible = false;

                        //dataGridView1.DataSource = null;

                        foreach (TablaSimbolos hijo in TablaSimbolos.tablaSimbolo)
                        {
                            int i = dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = hijo.tipo1;
                            dataGridView1.Rows[i].Cells[1].Value = hijo.id;
                            dataGridView1.Rows[i].Cells[2].Value = hijo.tipo;
                            dataGridView1.Rows[i].Cells[3].Value = hijo.ambito;
                            dataGridView1.Rows[i].Cells[4].Value = hijo.valor;
                        }
                        contadorDG++;

                    }
                    
                    //--------- GENERAR REPORTES
                    ReporteTS.reporteErroresSemanticos();
                    ReporteTS.reporteErroresLexicos();
                    ReporteTS.reporteErroresSintacticos();

                }

            }
            catch (Exception excMsg)
            {
                MessageBox.Show(excMsg.Message.ToString(), "Error");
            } 
        }

        //---------------- NUEVO ARCHIVO
        private void crearArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.Visible == false)
                {
                    tabControl1.Visible = true;
                }
                // --- CREAR TAB
                TabPage tp = new TabPage();
                int tc = (tabControl1.TabCount + 1);
                tp.Text = "Archivo " + tc.ToString();
                tabControl1.TabPages.Add(tp);
                RichTextBox rtb = new RichTextBox();
                rtb.Dock = DockStyle.Fill;
                tp.Controls.Add(rtb);

                // --- GUARDAR
                int selectedTab = tabControl1.SelectedIndex;
                Control ctrl = tabControl1.Controls[selectedTab].Controls[0];
                string cadena = rtb.Text;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Fi Files|*.fi";
                saveFileDialog1.Title = "Save fi file";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    MessageBox.Show(saveFileDialog1.FileName + cadena);
                    ruta = saveFileDialog1.FileName + cadena;
                    File.WriteAllText(saveFileDialog1.FileName, cadena);
                }


                return;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message.ToString(), "Error");
            }
        }
        //---------------- ABRIR ARCHIVO
        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrir.Filter = "Fi Files|*.fi";
            abrir.Title = "Select Fi file";

            try
            {
                if (tabControl1.Visible == false)
                {
                    tabControl1.Visible = true;
                }

                //Abrir el archivo
                if (abrir.ShowDialog() == DialogResult.OK)
                {
                    TabPage tp = new TabPage();
                    int tc = (tabControl1.TabCount + 1);
                    tp.Text = "Archivo " + tc.ToString();
                    tabControl1.TabPages.Add(tp);
                    RichTextBox rtb = new RichTextBox();
                    rtb.Dock = DockStyle.Fill;
                    tp.Controls.Add(rtb);
                    ruta = abrir.FileName;
                    rtb.Text = File.ReadAllText(ruta, Encoding.Default);
                }
                abrir.Dispose();
                return;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message.ToString(), "Error");
            }
        }
        //---------------- GUARDAR ARCHIVO
        private void guardarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.TabCount == 0)
                {
                    MessageBox.Show("Click on button1 to create a new tab.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int selectedTab = tabControl1.SelectedIndex;
                    Control ctrl = tabControl1.Controls[selectedTab].Controls[0];
                    RichTextBox rtb = ctrl as RichTextBox;
                    string cadena = rtb.Text;
                    if (ruta == "")
                    {
                        ruta = @"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\fi.fi";
                        MessageBox.Show("Se creo un archivo nuevo" + "\n" + " con el nombre de: " + "\n" + "fi");
                        System.IO.File.WriteAllText(ruta, cadena);
                    }
                    else
                    {
                        System.IO.File.WriteAllText(ruta, cadena);
                    }

                }
            }
            catch (Exception excMsg)
            {
                MessageBox.Show(excMsg.Message.ToString(), "Error");
            }
        }
        //---------------- GUARDAR COMO ARCHIVO
        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.TabCount == 0)
                {
                    MessageBox.Show("Click on button1 to create a new tab.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int selectedTab = tabControl1.SelectedIndex;
                    Control ctrl = tabControl1.Controls[selectedTab].Controls[0];
                    RichTextBox rtb = ctrl as RichTextBox;
                    string cadena = rtb.Text;
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Fi Files|*.fi";
                    saveFileDialog1.Title = "Save fi file";
                    saveFileDialog1.ShowDialog();

                    if (saveFileDialog1.FileName != "")
                    {
                        File.WriteAllText(saveFileDialog1.FileName, cadena);
                    }

                }
            }
            catch (Exception excMsg)
            {
                MessageBox.Show(excMsg.Message.ToString(), "Error");
            }
        }
        //---------------- ELIMINAR TAB
        private void eliminarPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.Visible == false)
                {
                    return;
                }
                else
                {
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    if (tabControl1.TabCount == 0)
                    {
                        tabControl1.Visible = false;
                    }
                    return;
                }
            }
            catch (Exception excMsg)
            {
                MessageBox.Show(excMsg.Message.ToString(), "Error");
            }
        }

        // ---- REPORTE DE ERRORES LEXICOS
        private void lexicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\ErroresLexicos.html");
        }
        // ---- REPORTE DE ERRORES SINTACTICOS
        private void sintacticosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\ErroresSintacticos.html");
        }
        // ---- REPORTE DE ERRORES SEMANTICOS
        private void semanticosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\ErroresSemanticos.html");
        }
        // ---- GRAFO DEL AST
        private void astToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Grafos\diego.jpg");
        }
        // --- MOSTRAR LA INFO DEL USUARIO
        private void informacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Diego Josue Berrios Gutierres \n    201503609", "Informacion",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        // --- MOSTRAR MANUAL DE USUARIO
        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\ManualUsuario.pdf");
        }
        // --- MOSTRAR MANUAL TECNICO
        private void manualTecnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\ManualTecnico.pdf");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
