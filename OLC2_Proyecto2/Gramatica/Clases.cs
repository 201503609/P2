using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLC2_Proyecto2.Gramatica
{
    class Clases
    {
        //-------------DATOS DE LA CLASE
        public string nombre;
        public ArrayList Herencias = new ArrayList();   //Para saber a que clases hereda
        public ArrayList VariablesC = new ArrayList();  //Para saber que variables tiene la clase
        public ArrayList ArreglosC = new ArrayList();   //Para saber que arreglos tiene la clase
        public ArrayList FuncionesC = new ArrayList();  //Para saber que funciones tiene la clase
        public ArrayList MetodosC = new ArrayList();    //Para saber que metodos tiene la clase

        public static ArrayList clases = new ArrayList();       //Para almacenar las clases

        //CONSTRUCTOR
        public Clases(string n, ArrayList her, ArrayList var, ArrayList arr, ArrayList fun, ArrayList met)
        {
            this.nombre = n;
        }

        //INSERTAR CLASE
        public static void insertarClase(Clases dato)
        {
            if(existenciaClase(dato) == false)
            {
                clases.Add(dato);
                Console.WriteLine("Clase " + dato.nombre + " insertada");
            }
        }
        //VERIFICAR QUE NO EXISTA LA CLASE
        public static Boolean existenciaClase(Clases d)
        {
            Boolean f = false;
            foreach (Clases c in clases)
            {
                if (c.nombre.ToLower().Equals(d.nombre.ToLower()))
                {
                    f = true;
                }
            }
            return f;
        }


        //----------------------------------VARIABLES
        //Insertar Variable sin valor a la clase
        public static Boolean insertarVariableEnClase(string nomCla, Variables v)
        {
            foreach (Clases hijo in clases)
            {
                if (hijo.nombre.Equals(nomCla))
                {
                        hijo.VariablesC.Add(v);
                        Console.WriteLine ("Se inserto la variable " + v.nombre + " ");
                }
            }
            return true;
        }





        //Obtener valor de una variable de la clase
        public static string obtenerValorVariable(string nomCla, string nomVar)
        {
            string n = "";
            foreach (Clases hijo in clases)
            {
                if (hijo.nombre.Equals(nomCla))
                {
                    foreach (Variables hijo1 in hijo.VariablesC)
                    {
                        if (hijo1.nombre.Equals(nomVar))
                            n = hijo1.valor;
                    }
                }
            }
            if(n == "")
                Console.WriteLine("No existe la variable " + nomVar);
            return n;
        }

        //Modificar valor de una variable
        public static Boolean modificarValorVariable(string nomCla, string nomVar,string valorNuevo)
        {
            Boolean n = false;
            foreach (Clases hijo in clases)
            {
                if (hijo.nombre.Equals(nomCla))
                {
                    foreach (Variables hijo1 in hijo.VariablesC)
                    {
                        if (hijo1.nombre.Equals(nomVar))
                        {
                            //Verificar el tipo de la variable con el nuevo valor
                            Boolean v = Variables.comprobacionTipoN(hijo1, valorNuevo);
                            if(v == true)
                            {
                                hijo1.valor = valorNuevo;
                                Console.WriteLine("Se modifico el valor de la variable " + hijo1.nombre);
                                n = true;
                            }
                            else
                                Console.WriteLine("No se modifico el valor de la variable " + hijo1.nombre);

                        }
                    }
                }
            }
            return n;
        }

        //----------------------------------ARREGLOS
        public static Boolean insertarArregloc(string nomCla, Arreglos arr, ArrayList Dimensiones)
        {
            Boolean flag1, flag2;
            foreach (Clases hijo in clases)
            {
                if (hijo.nombre.Equals(nomCla))
                {
                    //Verificar que el tipo del valor este bien
                    //flag1 = Variables.comprobacionTipo(v);
                    //Verificar que no exista 
                    //flag2 = Variables.existenciaVariable(v);

                    /*if (flag1 == true && flag2 == false)
                    {
                        hijo.VariablesC.Add(v);
                        Console.WriteLine("Se inserto la variable " + v.nombre + " ");
                    }*/


                }
            }
            return true;
        }
    }
}
