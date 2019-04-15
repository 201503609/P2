using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using OLC2_Proyecto2.Grafica;
using System.Windows.Forms;
using System.Diagnostics;

namespace OLC2_Proyecto2.Gramatica
{
    class Sintactico : Grammar
    {
        public static bool analizar(String cadena)
        {
            Gramatica g = new Gramatica();
            LanguageData l = new LanguageData(g);
            Parser p = new Parser(l);
            ParseTree arbol = p.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if(raiz == null)
                return false;

            generarImagen(raiz);
            MessageBox.Show("Imagen generada correctamente");
            return true;
        }

        private static void generarImagen(ParseTreeNode raiz)
        {
            String grafoDOT = ControlDot.getDot(raiz);
            crear_Archivo(grafoDOT, "diego");
            generar("diego");
            
            //MessageBox.Show(grafoDOT);
            //WINGRAPHVIZLib.DOT dot = new WINGRAPHVIZLib.DOT();
            //WINGRAPHVIZLib.BinaryImage img = dot.ToPNG(grafoDOT);
            //img.Save("AST.png");
        }
        private static void crear_Archivo(String grafo, string nombre)
        {
            System.IO.StreamWriter archivo = new System.IO.StreamWriter("C:\\Grafos\\" + nombre + ".txt");
            
            archivo.Write(grafo + " ");
           
            archivo.Close();

        }
        public static void generar(string fileName)
        {
            string strCmdText;
            strCmdText = "dot -Tpng C:\\Grafos\\" + fileName + ".txt -o C:\\Grafos\\" + fileName + ".jpg";
            var proc1 = new ProcessStartInfo();
            proc1.UseShellExecute = true;
            proc1.WorkingDirectory = @"C:\Windows\System32";
            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            proc1.Verb = "runas";
            proc1.Arguments = "/c " + strCmdText;
            proc1.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(proc1);
        }
    }
}
