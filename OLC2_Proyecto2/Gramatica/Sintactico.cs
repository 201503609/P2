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

            if (raiz == null)
            {
                for (int i = 0; i < arbol.ParserMessages.Count(); i++)
                {
                    ErroresSin errs = new ErroresSin(arbol.ParserMessages.ElementAt(i).Message,
                        arbol.ParserMessages.ElementAt(i).Location.Line,
                        arbol.ParserMessages.ElementAt(i).Location.Column,
                        arbol.ParserMessages.ElementAt(i).ParserState.ExpectedTerminals.ToString());
                    ErroresSin.erroresSin.Add(errs);
                }
                return false;
            }
            else
            {
                generarImagen(raiz);
                Recorrido.nodosImportantes.Clear();
                Recorrido.funcionesClase.Clear();
                MessageBox.Show("Imagen generada correctamente");

                Ambito diego = new Ambito(null);
                diego.nombreA = "Diego";
                Recorrido.primeraLectura(raiz,diego);
                //MessageBox.Show("FUNCIONES ENCONTRADAS " + Recorrido.funcionesClase.Count);
                foreach (Nodo hijo in Recorrido.nodosImportantes)
                {
                    Recorrido.ejecutar(hijo.root,diego);
                }

                //Recorrido.ejecutar(raiz, null);
                return true;
            }
        }

        private static void generarImagen(ParseTreeNode raiz)
        {
            String grafoDOT = ControlDot.getDot(raiz);
            crear_Archivo(grafoDOT, "diego");
            generar("diego");
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
