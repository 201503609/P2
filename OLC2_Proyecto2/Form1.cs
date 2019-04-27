using OLC2_Proyecto2.Gramatica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLC2_Proyecto2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //GRAFICAR EL AST
        private void astToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //BOTON QUE COMPILA
        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool resultado = Sintactico.analizar(txtEntrada.Text);
            if (!resultado)
                MessageBox.Show("Adios");
        }
    }
}
