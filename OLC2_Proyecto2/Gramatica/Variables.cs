using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLC2_Proyecto2.Gramatica
{
    class Variables
    {
        public string nombre, tipo, ambito, valor, pertence;
        public static ArrayList variables = new ArrayList();
        public static int h;

        //Constructor
        public Variables(string n, string t, string a, string v, string p)
        {
            this.ambito = a;
            this.tipo = t;
            this.nombre = n;
            this.valor = v;
            this.pertence = p;
        }

        //Insertar Variable
        public static void insertarVariable(Variables dato)
        {
            variables.Add(dato);
            Console.WriteLine("Variable" + dato.nombre + " insertada");
        }

        //VERIFICAR TIPO
        public static Boolean comprobacionTipo(Variables v)
        {
            Boolean f = false;
            if (v.tipo.Equals("int"))
            {
                int valorNumerico = 0;
                if (int.TryParse(v.valor, out valorNumerico))
                    f = true;
            }
            else if (v.tipo.Equals("string"))
            {
                if (v.valor.Contains("\""))
                    f = true;
            }
            else if (v.tipo.Equals("char"))
            {
                if (v.valor.Contains("'"))
                    f = true;
            }
            else if (v.tipo.Equals("double"))
            {
                double valorNumerico = 0;
                if (Double.TryParse(v.valor, out valorNumerico))
                    f = true;
            }
            else if (v.tipo.Equals("bool"))
            {
                if (v.valor.Contains("true") || v.valor.Contains("verdadero") ||
                    v.valor.Contains("falso") || v.valor.Contains("false"))
                    f = true;
            }
            else
            {
                MessageBox.Show(v.tipo + "######");
            }
            if (f == false)
                Console.WriteLine("EL TIPO NO ES EL ADECUADO DE " + v.nombre);
            return f;
        }



        //Ver si ya existe
        public static Boolean existenciaVariable(Variables v)
        {
            Boolean f = false;
            foreach(Clases hijo in Clases.clases)
            {
                if (hijo.nombre.Equals(v.pertence))
                {
                    foreach(Variables hijo1 in hijo.VariablesC)
                    {
                        if (hijo1.nombre.Equals(v.nombre))
                            f = true;
                    }
                }
            }
            if (f == true)
                Console.WriteLine("YA EXISTE LA VARIABLE" + v.nombre);
            return f;
        }

        //Comprobar el nuevo valor de la variable con el tipo actual
        public static Boolean comprobacionTipoN(Variables v, string nuevoValor)
        {
            Boolean f = false;
            if (v.tipo.Equals("int"))
            {
                int valorNumerico = 0;
                if (int.TryParse(nuevoValor, out valorNumerico))
                    f = true;
            }
            else if (v.tipo.Equals("string"))
            {
                if (nuevoValor.Contains("\""))
                    f = true;
            }
            else if (v.tipo.Equals("char"))
            {
                if (nuevoValor.Contains("'"))
                    f = true;
            }
            else if (v.tipo.Equals("double"))
            {
                double valorNumerico = 0;
                if (Double.TryParse(nuevoValor, out valorNumerico))
                    f = true;
            }
            else if (v.tipo.Equals("bool"))
            {
                if (nuevoValor.Contains("true") || nuevoValor.Contains("verdadero") ||
                    nuevoValor.Contains("falso") || nuevoValor.Contains("false"))
                    f = true;
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
