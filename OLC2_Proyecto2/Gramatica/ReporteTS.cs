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

        public static void reporteErrores(Ambito amb)
        {

            try
            {
                string archivo = @"C:\Users\Usuario\Desktop\GrafCompi\Proyecto2\tablaSimbolos.html";
                StreamWriter file = new StreamWriter(archivo);
                Console.WriteLine(archivo);
                file.WriteLine("<html> \n");
                file.WriteLine("<head>\n");
                file.WriteLine("<title> Reporte de Tokens </title>\n");
                file.WriteLine("</head>\n");
                file.WriteLine("<body>\n");

                file.WriteLine("<center>\n");
                file.WriteLine("<h1>Tabla de Simbolos</h1>\n");
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
                foreach (Variables item in amb.variableAmbito)
                {
                    i++;
                    file.WriteLine("<th> " + i + " </th>\n");
                    file.WriteLine("<td>" + "Variable" + "</td>\n");
                    file.WriteLine("<td> "+item.nombre+" </td>\n");
                    file.WriteLine("<td>" + item.tipo + " </td>\n");
                    file.WriteLine("<td>" + item.ambito + "</td>\n");
                    if (item.valor.Contains(";") )
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
                foreach (Arreglos item in amb.arreglosAmbito)
                {
                    i++;
                    file.WriteLine("<th> " + i + " </th>\n");
                    file.WriteLine("<td>" + "arreglo de " + item.cantidadDimensiones +" dim" + "</td>\n");
                    file.WriteLine("<td> " + item.nombre + " </td>\n");
                    file.WriteLine("<td>" + item.tipo + " </td>\n");
                    file.WriteLine("<td>" + item.ambito + "</td>\n");
                    if (item.cantidadDatos == 0)
                        file.WriteLine("<td>" + "No tiene elementos" + "</td>\n");
                    else
                        file.WriteLine("<td>" + "Tiene"+ item.cantidadDatos +" elementos" + "</td>\n");

                    file.WriteLine("<td>" + 0 + "</td>\n");
                    file.WriteLine("<td>" + 0 + "</td>\n");
                    file.WriteLine("</tr>\n");
                }

                file.WriteLine("</table>\n");
                file.WriteLine("</body>\n");
                file.WriteLine("</html>");
                file.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudo crear html", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
