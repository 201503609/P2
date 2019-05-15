using OLC2_Proyecto2.CSS.templated_introspect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLC2_Proyecto2.Gramatica
{
    class ReporteTS
    {

        public static int contador = 0;
        public static void reporteTablaSimbolos(Ambito ambt, string nombre)
        {

            try
            {
                string archivo = @"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\tablaSimbolos" + nombre + contador + ".html";
                StreamWriter file = new StreamWriter(archivo);
                Console.WriteLine(archivo);
    
                file.WriteLine("<html> \n");
                file.WriteLine("<head>\n");
                file.WriteLine("<title> Tabla de Simbolos </title>\n");

                file.WriteLine("<meta charset=\"utf - 8\" /> \n");
                file.WriteLine("<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1, user - scalable = no\" /> \n");
                file.WriteLine("<link rel=\"stylesheet\" href=\"" + @"C:/Users/Usuario/Documents/Visual Studio 2015/Projects/OLC2_Proyecto2/P2/OLC2_Proyecto2/CSS/templated-introspect/assets/css/main.css" + "\" /> \n");

                file.WriteLine("</head>\n");
                file.WriteLine("<body>\n");

                file.WriteLine("<header id=\"header\"> \n");
                file.WriteLine("<div class=\"inner\"> \n");
                file.WriteLine("<a  class=\"logo\">Tabla de Simbolos</a>\n");
                file.WriteLine("</div>\n");
                file.WriteLine("</header> \n");
                file.WriteLine("<a href=\"#menu\" class=\"navPanelToggle\"><span class=\"fa fa-bars\"></span></a> \n");


                file.WriteLine("<center>\n");
                file.WriteLine("<h1>Tabla de Simbolos de " + nombre + " " + contador + " </h1>\n");
                file.WriteLine("</center>\n");
                file.WriteLine("<table border=2 style = \"margin: 0 auto\">\n");
                //file.WriteLine("< table style = \"margin: 0 auto\">\n ");
                file.WriteLine("<tr>\n");

                file.WriteLine("<th>No.</th>\n");
                file.WriteLine("<th>Tipo 1</th>\n");
                file.WriteLine("<th>Identificador</th>\n");
                file.WriteLine("<th>Tipo</th>\n");
                file.WriteLine("<th>Ambito</th>\n");
                file.WriteLine("<th>Valor</th>\n");
                file.WriteLine("<th>No.Fila</th>\n");
                file.WriteLine("<th>No. Columna</th>\n");
                //file.WriteLine("<th>Lexema</th>\n");
                file.WriteLine("</tr>\n");
                int i = 0;
                Ambito aux = ambt;
                while (aux != null)
                {
                    foreach (Variables item in aux.variableAmbito)
                    {
                        i++;
                        file.WriteLine("<th> " + i + " </th>\n");
                        file.WriteLine("<td>" + "Variable" + "</td>\n");
                        file.WriteLine("<td> " + item.nombre + " </td>\n");
                        file.WriteLine("<td>" + item.tipo + " </td>\n");
                        file.WriteLine("<td>" + item.ambito + "</td>\n");
                        if (item.valor.Contains(";"))
                        {
                            file.WriteLine("<td>" + "" + "</td>\n");
                        }
                        else
                        {
                            file.WriteLine("<td>" + item.valor + "</td>\n");
                        }
                        file.WriteLine("<td>" + 0 + "</td>\n");
                        file.WriteLine("<td>" + 0 + "</td>\n");
                        file.WriteLine("</tr>\n");
                    }
                    foreach (Arreglos item in aux.arreglosAmbito)
                    {
                        i++;
                        file.WriteLine("<th> " + i + " </th>\n");
                        file.WriteLine("<td>" + "arreglo de " + item.cantidadDimensiones + " dim" + "</td>\n");
                        file.WriteLine("<td> " + item.nombre + " </td>\n");
                        file.WriteLine("<td>" + item.tipo + " </td>\n");
                        file.WriteLine("<td>" + item.ambito + "</td>\n");
                        if (item.cantidadDatos == 0)
                            file.WriteLine("<td>" + "No tiene elementos" + "</td>\n");
                        else
                            file.WriteLine("<td>" + "Tiene" + item.cantidadDatos + " elementos" + "</td>\n");

                        file.WriteLine("<td>" + 0 + "</td>\n");
                        file.WriteLine("<td>" + 0 + "</td>\n");
                        file.WriteLine("</tr>\n");
                    }
                    aux = aux.anterior;
                }

                file.WriteLine("</table>\n");
                file.WriteLine("</body>\n");
                file.WriteLine("</html>");
                file.Close();
                contador++;
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo crear html", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void reporteErroresSemanticos()
        {

            try
            {
                string archivo = @"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\ErroresSemanticos.html";
                StreamWriter file = new StreamWriter(archivo);
                Console.WriteLine(archivo);
                file.WriteLine("<!DOCTYPE HTML> \n");



                file.WriteLine("<html> \n");
                file.WriteLine("<head>\n");
                file.WriteLine("<title> Reporte de Errores Semanticos </title>\n");

                file.WriteLine("<meta charset=\"utf - 8\" /> \n");
                file.WriteLine("<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1, user - scalable = no\" /> \n");
                file.WriteLine("<link rel=\"stylesheet\" href=\"" + @"C:/Users/Usuario/Documents/Visual Studio 2015/Projects/OLC2_Proyecto2/P2/OLC2_Proyecto2/CSS/templated-introspect/assets/css/main.css" + "\" /> \n");

                file.WriteLine("</head>\n");
                file.WriteLine("<body>\n");

                file.WriteLine("<header id=\"header\"> \n");
                file.WriteLine("<div class=\"inner\"> \n");
                file.WriteLine("<a  class=\"logo\">Reporte Errores Semanticos</a>\n");
                file.WriteLine("</div>\n");
                file.WriteLine("</header> \n");
                file.WriteLine("<a href=\"#menu\" class=\"navPanelToggle\"><span class=\"fa fa-bars\"></span></a> \n");

                file.WriteLine("<center>\n");
                file.WriteLine("<h1>Tabla de Errores Semanticos </h1>\n");
                file.WriteLine("</center>\n");
                file.WriteLine("<table border=2 style = \"margin: 0 auto\">\n");
                //file.WriteLine("< table style = \"margin: 0 auto\">\n ");
                file.WriteLine("<tr>\n");

                file.WriteLine("<th>No.</th>\n");
                file.WriteLine("<th>Descripcion del error</th>\n");
                file.WriteLine("<th>No.Fila</th>\n");
                file.WriteLine("<th>No. Columna</th>\n");
                file.WriteLine("</tr>\n");
                int i = 0;
                foreach (ErroresSem item in ErroresSem.erroreSeman)
                {
                    i++;
                    file.WriteLine("<th> " + i + " </th>\n");
                    file.WriteLine("<td>" + item.descError + "</td>\n");
                    file.WriteLine("<td>" + item.fil + "</td>\n");
                    file.WriteLine("<td>" + item.col + "</td>\n");
                    file.WriteLine("</tr>\n");
                }

                file.WriteLine("</table>\n");
                file.WriteLine("</body>\n");
                file.WriteLine("</html>");
                file.Close();
                contador++;
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo crear html", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void reporteErroresSintacticos()
        {

            try
            {
                string archivo = @"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\ErroresSintacticos.html";
                StreamWriter file = new StreamWriter(archivo);
                Console.WriteLine(archivo);
                file.WriteLine("<!DOCTYPE HTML> \n");



                file.WriteLine("<html> \n");
                file.WriteLine("<head>\n");
                file.WriteLine("<title> Reporte de Errores Sintacticos </title>\n");

                file.WriteLine("<meta charset=\"utf - 8\" /> \n");
                file.WriteLine("<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1, user - scalable = no\" /> \n");
                file.WriteLine("<link rel=\"stylesheet\" href=\"" + @"C:/Users/Usuario/Documents/Visual Studio 2015/Projects/OLC2_Proyecto2/P2/OLC2_Proyecto2/CSS/templated-introspect/assets/css/main.css" + "\" /> \n");

                file.WriteLine("</head>\n");
                file.WriteLine("<body>\n");

                file.WriteLine("<header id=\"header\"> \n");
                file.WriteLine("<div class=\"inner\"> \n");
                file.WriteLine("<a  class=\"logo\">Reporte Errores Sintacticos</a>\n");
                file.WriteLine("</div>\n");
                file.WriteLine("</header> \n");
                file.WriteLine("<a href=\"#menu\" class=\"navPanelToggle\"><span class=\"fa fa-bars\"></span></a> \n");

                file.WriteLine("<center>\n");
                file.WriteLine("<h1>Tabla de Errores Sintacticos </h1>\n");
                file.WriteLine("</center>\n");
                file.WriteLine("<table border=2 style = \"margin: 0 auto\">\n");
                //file.WriteLine("< table style = \"margin: 0 auto\">\n ");
                file.WriteLine("<tr>\n");

                file.WriteLine("<th>No.</th>\n");
                file.WriteLine("<th>Descripcion del error</th>\n");
                file.WriteLine("<th>Se esperaba</th>\n");
                file.WriteLine("<th>No.Fila</th>\n");
                file.WriteLine("<th>No. Columna</th>\n");
                file.WriteLine("</tr>\n");
                int i = 0;
                foreach (ErroresSin item in ErroresSin.erroresSin)
                {
                    i++;
                    file.WriteLine("<th> " + i + " </th>\n");
                    file.WriteLine("<td>" + item.descError + "</td>\n");
                    file.WriteLine("<td>" + item.valEsp + "</td>\n");
                    file.WriteLine("<td>" + item.fil + "</td>\n");
                    file.WriteLine("<td>" + item.col + "</td>\n");
                    file.WriteLine("</tr>\n");
                }

                file.WriteLine("</table>\n");
                file.WriteLine("</body>\n");
                file.WriteLine("</html>");
                file.Close();
                contador++;
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo crear html", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void reporteErroresLexicos()
        {

            try
            {
                string archivo = @"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\ErroresLexicos.html";
                StreamWriter file = new StreamWriter(archivo);
                Console.WriteLine(archivo);
                file.WriteLine("<!DOCTYPE HTML> \n");



                file.WriteLine("<html> \n");
                file.WriteLine("<head>\n");
                file.WriteLine("<title> Reporte de Errores Lexicos </title>\n");

                file.WriteLine("<meta charset=\"utf - 8\" /> \n");
                file.WriteLine("<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1, user - scalable = no\" /> \n");
                file.WriteLine("<link rel=\"stylesheet\" href=\"" + @"C:/Users/Usuario/Documents/Visual Studio 2015/Projects/OLC2_Proyecto2/P2/OLC2_Proyecto2/CSS/templated-introspect/assets/css/main.css" + "\" /> \n");

                file.WriteLine("</head>\n");
                file.WriteLine("<body>\n");

                file.WriteLine("<header id=\"header\"> \n");
                file.WriteLine("<div class=\"inner\"> \n");
                file.WriteLine("<a  class=\"logo\">Reporte Errores Lexicos</a>\n");
                file.WriteLine("</div>\n");
                file.WriteLine("</header> \n");
                file.WriteLine("<a href=\"#menu\" class=\"navPanelToggle\"><span class=\"fa fa-bars\"></span></a> \n");

                file.WriteLine("<center>\n");
                file.WriteLine("<h1>Tabla de Errores Lexicos </h1>\n");
                file.WriteLine("</center>\n");
                file.WriteLine("<table border=2 style = \"margin: 0 auto\">\n");
                //file.WriteLine("< table style = \"margin: 0 auto\">\n ");
                file.WriteLine("<tr>\n");

                file.WriteLine("<th>No.</th>\n");
                file.WriteLine("<th>Descripcion del error</th>\n");
                file.WriteLine("<th>No.Fila</th>\n");
                file.WriteLine("<th>No. Columna</th>\n");
                file.WriteLine("</tr>\n");
                int i = 0;
                foreach (ErroresLex item in ErroresLex.erroresLex)
                {
                    i++;
                    file.WriteLine("<th> " + i + " </th>\n");
                    file.WriteLine("<td>" + item.descError + "</td>\n");
                    file.WriteLine("<td>" + item.fil + "</td>\n");
                    file.WriteLine("<td>" + item.col + "</td>\n");
                    file.WriteLine("</tr>\n");
                }

                file.WriteLine("</table>\n");
                file.WriteLine("</body>\n");
                file.WriteLine("</html>");
                file.Close();
                contador++;
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo crear html", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
