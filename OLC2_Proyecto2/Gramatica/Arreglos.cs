using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLC2_Proyecto2.Gramatica
{
    class Arreglos
    {
        //----- VALORES DE LOS ARREGLOS
        public string nombre, tipo, ambito, pertence, dimension1, dimension2, dimension3; 
        public int cantidadDimensiones, cantidadDatos;
        public ArrayList valoresArreglo = new ArrayList();

        //Constructor
        public Arreglos(string nom, string tip, string amb, string pert, int cantD, string dim1, string dim2, string dim3, ArrayList vals, int cantidadD)
        {
            this.ambito = amb;
            this.tipo = tip;
            this.nombre = nom;
            this.pertence = pert;
            this.cantidadDimensiones = cantD;
            this.dimension1 = dim1;
            this.dimension2 = dim2;
            this.dimension3 = dim3;
            this.valoresArreglo = vals;
            this.cantidadDatos = cantidadD;
        }

        //VERIFICAR TIPO
        public static Boolean comprobacionTipoArr(Arreglos v)
        {
            Boolean f = false;
            if (v.tipo.Equals("int"))
            {
                foreach(string hijo in v.valoresArreglo)
                {
                    int valorNumerico = 0;
                    if (int.TryParse(hijo, out valorNumerico))
                        f = true;
                    else
                    {
                        f = false;
                        break;
                    }
                }
            }
            else if (v.tipo.Equals("string"))
            {
                foreach (string hijo in v.valoresArreglo)
                {
                    if (hijo.Contains("\""))
                        f = true;
                    else
                    {
                        f = false;
                        break;
                    }
                }
            }
            else if (v.tipo.Equals("char"))
            {
                foreach (string hijo in v.valoresArreglo)
                {
                    if (hijo.Contains("'"))
                        f = true;
                    else
                    {
                        f = false;
                        break;
                    }
                }
            }
            else if (v.tipo.Equals("double"))
            {
                foreach (string hijo in v.valoresArreglo)
                {
                    Double valorNumerico = 0;
                    if (Double.TryParse(hijo, out valorNumerico))
                        f = true;
                    else
                    {
                        f = false;
                        break;
                    }
                }
            }
            else if (v.tipo.Equals("bool"))
            {
                foreach (string hijo in v.valoresArreglo)
                {
                    if (hijo.Contains("true")  || hijo.Contains("verdadero") ||
                        hijo.Contains("falso") || hijo.Contains("false"))
                        f = true;
                    else
                    {
                        f = false;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show(v.tipo + "######");
            }
            if (f == false)
                Console.WriteLine("EL TIPO NO ES EL ADECUADO DE " + v.nombre);
            return f;
        }


    }
}
