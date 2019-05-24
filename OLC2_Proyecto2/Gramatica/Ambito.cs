using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLC2_Proyecto2.Gramatica
{
    class Ambito
    {
        //Va a tener un anterior 
        public Ambito anterior = null;
        public Boolean salida = false;
        public Boolean continuar = false;
        public Boolean retorno = false;
        public string nombreA = "";
       

        //Va a tener variables 
        public ArrayList variableAmbito = new ArrayList();
        public ArrayList arreglosAmbito = new ArrayList();
        public ArrayList funcionesAmbito = new ArrayList();

        public Ambito(Ambito ant)
        {
            this.anterior = ant;
            this.salida = false;
            this.continuar = false;
            this.retorno = false;
            this.nombreA = "";
        }


        //------------------------- VARIABLES
        //Insertar Variable ---AQUI SE TIENE QUE ENVIAR LOS ERRORES DE EXISTIR
        public static Boolean insertarVarSinValor(Variables variable, Ambito actual)
        {
            Boolean flag = false;
            //--------- VERIFICAR SI YA EXISTE UNA VARIABLE CON EL MISMO NOMBRE
            Ambito aux = actual;
            //while (aux != null)
            //{
            foreach (Variables v in aux.variableAmbito)
            {
                if (v.nombre.Equals(variable.nombre))
                    flag = true;
            }
            aux = aux.anterior;
            //}
            //--------- VERIFICAR SI YA EXISTE UN ARREGLO CON EL MISMO NOMBRE
            aux = actual;
            //while (aux != null)
            //{
            foreach (Arreglos v in aux.arreglosAmbito)
            {
                if (v.nombre.Equals(variable.nombre))
                    flag = true;
            }
            aux = aux.anterior;
            //}

            //--------INSERTAR O NO LA VARIABLE
            if (flag == false)  //SI NO EXISTE
            {
                actual.variableAmbito.Add(variable);
                Console.WriteLine("Se ha insertado la variable " + variable.nombre + " " + variable.valor);
                return true;
            }
            else
            {
                ErroresSem es = new ErroresSem("Ya existe una variable con el nombre: " + variable.nombre, 0, 0);
                es.insertarErrSem(es);
                //Console.WriteLine("Ya existe una variable con el nombre: " + variable.nombre);
                return false;
            }
        }

        public static Boolean insertarVarConValor(Variables variable, Ambito actual)
        {
            Boolean flag = false, flag1 = false;
            //--------- VERIFICAR SI YA EXISTE UNA VARIABLE CON EL MISMO NOMBRE
            foreach (Variables v in actual.variableAmbito)
            {
                if (v.nombre.Equals(variable.nombre))
                {
                    flag = true;
                    break;
                }
            }
            //--------- VERIFICAR SI YA EXISTE UN ARREGLO CON EL MISMO NOMBRE
            foreach (Arreglos v in actual.arreglosAmbito)
            {
                if (v.nombre.Equals(variable.nombre))
                {
                    flag = true;
                    break;
                }
            }

            //--------INSERTAR O NO LA VARIABLE
            if (flag == false)
            {
                //Obtener valor de la variable SI ES UN ID
                if (variable.valor.Contains("@@"))
                {
                    Variables ev = obtenerValorVariable(actual, variable.valor);
                    if (ev != null)
                        variable.valor = ev.valor;
                    else
                    {
                        ErroresSem es = new ErroresSem("El valor de la variable " + variable.nombre + " es incorrecto,\n por lo que no se puede insertar." + variable.nombre, 0, 0);
                        es.insertarErrSem(es);
                        Console.WriteLine("El valor de la variable " + variable.nombre + " es incorrecto.");
                        return false;
                    }
                }

                flag1 = Variables.comprobacionTipo(variable);                //Verificar que el tipo del valor este bien
                if (flag1 == true)
                {
                    actual.variableAmbito.Add(variable);
                    //Console.WriteLine("Se ha insertado la variable " + variable.nombre + " " + variable.valor +". " + actual.nombreA);
                    return true;
                }
                else
                {
                    ErroresSem es = new ErroresSem("El valor de la variable " + variable.nombre + " es incorrecto,\n por lo que no se puede insertar." + variable.nombre, 0, 0);
                    es.insertarErrSem(es);
                    Console.WriteLine("El valor asignado no es el adecuado para la variable " + variable.nombre);
                    return false;
                }
            }
            else
            {
                ErroresSem es = new ErroresSem("Ya existe una variable con el nombre: " + variable.nombre, 0, 0);
                es.insertarErrSem(es);
                //Console.WriteLine("Ya existe una variable con el nombre: " + variable.nombre);
                return false;
            }

        }

        //----OBTENER VALOR DE UNA VARIABLE
        public static Variables obtenerValorVariable(Ambito actual, string nombre)
        {
            string n = "";
            if (nombre.Contains("@@"))
                n = nombre.Substring(2);
            else
                n = nombre;
            Boolean flag = false;
            Boolean encontrado = false;
            Ambito aux = actual;
            Variables vv = null;
            int i = 0;
            while (aux != null)
            {
                //MessageBox.Show("Ambito " + aux.nombreA);
                i++;          
                foreach (Variables v in aux.variableAmbito)
                {
                    //MessageBox.Show("Variable " + v.nombre);
                    if (v.nombre.ToLower().Equals(n.ToLower())  && encontrado == false)
                    {
                        vv = v;
                        encontrado = true;
                        flag = true;
                        break;
                    }
                }
                if (encontrado == true)
                {
                    break;
                }
                aux = aux.anterior;
            }
            if (flag == false)
            {
                MessageBox.Show("No se encuentra la variable " + n);
                ErroresSem es = new ErroresSem("La variable " + nombre + " no existe,\n por lo que no se puede devolver el valor.", 0, 0);
                es.insertarErrSem(es);
            }
            return vv;
        }

        //----CAMBIAR VALOR DE UNA VARIABLE
        public static Boolean CambiarValorVariable(Ambito actual, Variables variable)
        {
            Boolean bandera1 = false;
            Ambito aux = actual;
            while (aux != null)
            {
                foreach (Variables v in aux.variableAmbito)
                {
                    if (v.nombre.Equals(variable.nombre))
                    {
                        bandera1 = true;
                        //OBTENER EL NUEVO VALOR SI LO QUE TRAE ES UN ID
                        if (variable.valor.Contains("@@"))
                        {
                            Variables ev = obtenerValorVariable(actual, variable.valor);
                            if (ev != null)
                            {
                                variable.valor = ev.valor;
                            }
                            else
                            {
                                ErroresSem es = new ErroresSem("La variable " + variable.valor + " no existe,\n por lo que no se puede cambiar el valor a " +
                                    variable.nombre + " .", 0, 0);
                                es.insertarErrSem(es);
                                return false;
                            }
                        }

                        //Verificar que el tipo del valor este bien
                        variable.tipo = v.tipo;
                        Boolean flag1 = Variables.comprobacionTipo(variable);
                        if (flag1 == true)
                        {
                            v.valor = variable.valor;
                            //Console.WriteLine("El valor de la variable " + v.nombre + " ha sido modificado a " + v.valor + ".");
                            return true;
                        }
                        else
                        {
                            ErroresSem es = new ErroresSem("El valor nuevo que se desea asignar a la variable " + v.nombre + " no es compatible.", 0, 0);
                            es.insertarErrSem(es);
                            return false;
                        }
                    }
                }
                aux = aux.anterior;
            }

            if (bandera1 == false)      //VER SI LA VARIABLE A CAMBIAR EXISTE O NO
            {
                ErroresSem es = new ErroresSem("La variable " + variable.nombre + " no existe en ningun ambito.", 0, 0);
                es.insertarErrSem(es);
                //Console.WriteLine("La variable " + variable.nombre + " no existe en ningun ambito.");
                return false;
            }
            else
                return true;
        }

        //----AUMENTAR VALOR DE UNA VARIABLE
        public static Boolean AumentarValorVariable(Ambito actual, Variables variable)
        {
            Boolean bandera1 = false;
            Ambito aux = actual;
            while (aux != null)
            {
                foreach (Variables v in aux.variableAmbito)
                {
                    if (v.nombre.Equals(variable.nombre))
                    {
                        bandera1 = true;
                        //OBTENER EL NUEVO VALOR SI LO QUE TRAE ES UN ID
                        Variables ev = obtenerValorVariable(actual, variable.nombre);
                        if (ev != null)
                        {
                            variable.valor = ev.valor;
                        }
                        else
                        {
                            ErroresSem es = new ErroresSem("La variable " + v.nombre + " no existe, por lo que no se puede aumentar nada.", 0, 0);
                            es.insertarErrSem(es);
                            Console.WriteLine("La variable " + v.nombre + " no existe.");
                            return false;
                        }
                        //APLICAR AUMENTO
                        //Double
                        Double valNumericoD = 0.0;
                        int valNumericoI = 0;
                        if (double.TryParse(variable.valor, out valNumericoD))
                        {
                            v.valor = (Convert.ToDouble(variable.valor) + 1).ToString();
                            return true;
                        }
                        //Int
                        else if (int.TryParse(variable.valor, out valNumericoI))
                        {
                            v.valor = (Convert.ToInt32(variable.valor) + 1).ToString();
                            return true;
                        }
                        //Char
                        else if (variable.valor.Contains("'"))
                        {
                            char[] my = variable.valor.ToCharArray();
                            v.valor = (1 + my[0]).ToString();
                            return true;
                        }
                        break;
                    }
                }
                aux = aux.anterior;
            }

            if (bandera1 == false)      //VER SI LA VARIABLE A CAMBIAR EXISTE O NO
            {
                ErroresSem es = new ErroresSem("La variable " + variable.nombre + " no existe en ningun ambito.", 0, 0);
                es.insertarErrSem(es);
                //Console.WriteLine("La variable " + variable.nombre + " no existe en ningun ambito.");
                return false;
            }
            else
                return true;
        }

        //----DISMINUIR VALOR DE UNA VARIABLE
        public static Boolean DisminuirValorVariable(Ambito actual, Variables variable)
        {
            Boolean bandera1 = false;
            Ambito aux = actual;
            while (aux != null)
            {
                foreach (Variables v in aux.variableAmbito)
                {
                    if (v.nombre.Equals(variable.nombre))
                    {
                        bandera1 = true;
                        //OBTENER EL NUEVO VALOR SI LO QUE TRAE ES UN ID
                        Variables ev = obtenerValorVariable(actual, variable.nombre);
                        if (ev != null)
                        {
                            variable.valor = ev.valor;
                        }
                        else
                        {
                            ErroresSem es = new ErroresSem("La variable " + v.nombre + " no existe, por lo que no se puede disminuir nada.", 0, 0);
                            es.insertarErrSem(es);
                            Console.WriteLine("La variable " + v.nombre + " no existe.");
                            return false;
                        }
                        //APLICAR AUMENTO
                        //Double
                        Double valNumericoD = 0.0;
                        int valNumericoI = 0;
                        if (double.TryParse(variable.valor, out valNumericoD))
                        {
                            v.valor = (Convert.ToDouble(variable.valor) - 1).ToString();
                            return true;
                        }
                        //Int
                        else if (int.TryParse(variable.valor, out valNumericoI))
                        {
                            v.valor = (Convert.ToInt32(variable.valor) - 1).ToString();
                            return true;
                        }
                        //Char
                        else if (variable.valor.Contains("'"))
                        {
                            char[] my = variable.valor.ToCharArray();
                            v.valor = (my[0] - 1).ToString();
                            return true;
                        }
                        break;
                    }
                }
                aux = aux.anterior;
            }

            if (bandera1 == false)      //VER SI LA VARIABLE A CAMBIAR EXISTE O NO
            {
                ErroresSem es = new ErroresSem("La variable " + variable.nombre + " no existe", 0, 0);
                es.insertarErrSem(es);
                //Console.WriteLine("La variable " + variable.nombre + " no existe en ningun ambito.");
                return false;
            }
            else
                return true;
        }


        //------------------------- ARREGLOS
        //Insertar Arreglo
        public static Boolean insertarArrSinValor(Arreglos arreglo, Ambito actual)
        {
            Boolean flag = false;
            //--------- VERIFICAR SI YA EXISTE UNA VARIABLE CON EL MISMO NOMBRE
            Ambito aux = actual;
            //while (aux != null)
            //{
            foreach (Variables v in aux.variableAmbito)
            {
                if (v.nombre.Equals(arreglo.nombre))
                    flag = true;
            }
            aux = aux.anterior;
            //}
            //--------- VERIFICAR SI YA EXISTE UN ARREGLO CON EL MISMO NOMBRE
            aux = actual;
            //while (aux != null)
            //{
            foreach (Arreglos v in aux.arreglosAmbito)
            {
                if (v.nombre.Equals(arreglo.nombre))
                    flag = true;
            }
            aux = aux.anterior;
            //}

            //--------INSERTAR O NO EL ARREGLO
            if (flag == false)  //SI NO EXISTE
            {
                actual.arreglosAmbito.Add(arreglo);
                Console.WriteLine("Se inserto el arreglo con el nombre: " + arreglo.nombre + "." + arreglo.dimension1 + "." + arreglo.dimension2 + "." + arreglo.dimension3);
                return true;
            }
            else
            {
                ErroresSem es = new ErroresSem("Ya existe una variable o arreglo con el nombre " + arreglo.nombre, 0, 0);
                es.insertarErrSem(es);
                Console.WriteLine("Ya existe una variable o arreglo con el nombre: " + arreglo.nombre);
                return false;
            }
        }

        public static Boolean insertarArrConValor(Arreglos arreglo, Ambito actual)
        {
            Boolean flag = false;
            //--------- VERIFICAR SI YA EXISTE UNA VARIABLE CON EL MISMO NOMBRE
            Ambito aux = actual;
            //while (aux != null)
            //{
            foreach (Variables v in aux.variableAmbito)
            {
                if (v.nombre.Equals(arreglo.nombre))
                    flag = true;
            }
            aux = aux.anterior;
            //}
            //--------- VERIFICAR SI YA EXISTE UN ARREGLO CON EL MISMO NOMBRE
            aux = actual;
            //while (aux != null)
            //{
            foreach (Arreglos v in aux.arreglosAmbito)
            {
                if (v.nombre.Equals(arreglo.nombre))
                    flag = true;
            }
            aux = aux.anterior;
            //}

            //--------INSERTAR O NO EL ARREGLO
            if (flag == false)
            {
                Boolean flag1 = Arreglos.comprobacionTipoArr(arreglo);                //Verificar que el tipo del valor este bien
                if (flag1 == true)
                {
                    actual.arreglosAmbito.Add(arreglo);
                    Console.WriteLine("Se inserto el arreglo con el nombre: " + arreglo.nombre + "." + arreglo.dimension1 + "." + arreglo.dimension2 + "." + arreglo.dimension3);
                    return true;
                }
                else
                {
                    ErroresSem es = new ErroresSem("El valor asignado al arreglo " + arreglo.nombre + "no es el adecuado.", 0, 0);
                    es.insertarErrSem(es);
                    Console.WriteLine("El valor asignado no es el adecuado para el arreglo " + arreglo.nombre);
                    return false;
                }
            }
            else
            {
                ErroresSem es = new ErroresSem("Ya existe una variable o arreglo con el nombre " + arreglo.nombre, 0, 0);
                es.insertarErrSem(es);
                Console.WriteLine("Ya existe una variable o arreglo con el nombre: " + arreglo.nombre);
                return false;
            }
        }

        public static Boolean asignarValorArreglo(Arreglos arreglo, Ambito actual, string valorNuevo)
        {
            Boolean flag = false;
            Ambito aux = actual;
            Arreglos apoyo = null;

            //--------- VERIFICAR SI YA EXISTE UN ARREGLO CON EL MISMO NOMBRE
            aux = actual;
            while (aux != null)
            {
                foreach (Arreglos v in aux.arreglosAmbito)
                {
                    if (v.nombre.Equals(arreglo.nombre))
                    {
                        apoyo = v;
                        flag = true;
                    }
                }
                aux = aux.anterior;
            }

            // EXISTE O NO 
            if (flag == true)
            {
                //----VER QUE LAS DIMENSIONES SEAN LAS ADECUADAS
                if (arreglo.cantidadDimensiones == 1)
                {
                    int dimen1 = Convert.ToInt32(apoyo.dimension1);
                    int posi1 = Convert.ToInt32(arreglo.dimension1);
                    if (posi1 > dimen1)
                    {
                        ErroresSem es = new ErroresSem("Las dimensiones que se buscan son incorrectas " + arreglo.nombre, 0, 0);
                        es.insertarErrSem(es);
                        Console.WriteLine("La dimension del arreglo que se desea asignar es incorrecta");
                        return false;
                    }
                }
                else if (arreglo.cantidadDimensiones == 2)
                {
                    int dimen1 = Int32.Parse(apoyo.dimension1);
                    int dimen2 = Int32.Parse(apoyo.dimension2);
                    int posi1 = Int32.Parse(arreglo.dimension1);
                    int posi2 = Int32.Parse(arreglo.dimension2);
                    if (posi1 > dimen1 || posi2 > dimen2)
                    {
                        ErroresSem es = new ErroresSem("Las dimensiones que se buscan son incorrectas " + arreglo.nombre, 0, 0);
                        es.insertarErrSem(es);
                        Console.WriteLine("La dimension del arreglo que se desea asignar es incorrecta");
                        return false;
                    }
                }
                else if (arreglo.cantidadDimensiones == 3)
                {
                    int dimen1 = Int32.Parse(apoyo.dimension1);
                    int dimen2 = Int32.Parse(apoyo.dimension2);
                    int dimen3 = Int32.Parse(apoyo.dimension3);
                    int posi1 = Int32.Parse(arreglo.dimension1);
                    int posi2 = Int32.Parse(arreglo.dimension2);
                    int posi3 = Int32.Parse(arreglo.dimension3);
                    if (posi1 > dimen1 || posi2 > dimen2 || posi3 > dimen3)
                    {
                        ErroresSem es = new ErroresSem("Las dimensiones que se buscan son incorrectas " + arreglo.nombre, 0, 0);
                        es.insertarErrSem(es);
                        Console.WriteLine("La dimension del arreglo que se desea asignar es incorrecta");
                        return false;
                    }
                }
                else
                {
                    ErroresSem es = new ErroresSem("Las dimensiones a asignar del arreglo " + arreglo.nombre + " exceden el limite.", 0, 0);
                    es.insertarErrSem(es);
                }

                //----CALCULAR LA POSICION
                int posicion = 0;
                if (arreglo.cantidadDimensiones == 1)
                {
                    posicion = int.Parse(arreglo.dimension1) - 1;
                }
                else if (arreglo.cantidadDimensiones == 2)
                {
                    int dimen2 = Int32.Parse(apoyo.dimension2);
                    int posi1 = Int32.Parse(arreglo.dimension1);
                    int posi2 = Int32.Parse(arreglo.dimension2);
                    posicion = ((posi1 - 1) * dimen2) + (posi2 - 1);
                }
                else if (arreglo.cantidadDimensiones == 3)
                {
                    int dimen2 = Int32.Parse(apoyo.dimension2);
                    int dimen3 = Int32.Parse(apoyo.dimension3);
                    int posi1 = Int32.Parse(arreglo.dimension1);
                    int posi2 = Int32.Parse(arreglo.dimension2);
                    int posi3 = Int32.Parse(arreglo.dimension3);
                    posicion = ((posi1 - 1) * (dimen2 * dimen3)) + (((posi2 - 1) * dimen3) + (posi3 - 1));
                }
                //----VALIDAR TIPO CON EL NUEVO VALOR
                arreglo.tipo = apoyo.tipo;
                Boolean f1 = Arreglos.comprobacionTipoArr(arreglo);

                //----ASIGNAR EL VALOR NUEVO
                if (f1 == true)
                {
                    aux = actual;
                    while (aux != null)
                    {
                        foreach (Arreglos v in aux.arreglosAmbito)
                        {
                            if (v.nombre.Equals(arreglo.nombre))
                            {
                                //Recorrer el arreglo en busca de la pos deseada
                                v.valoresArreglo.RemoveAt(posicion);
                                v.valoresArreglo.Insert(posicion, valorNuevo);
                                Console.WriteLine("Se cambio el valor de la posicion " + arreglo.dimension1 + "." + arreglo.dimension2 + "." + arreglo.dimension3 + "del arreglo " + arreglo.nombre);
                            }
                        }
                        aux = aux.anterior;
                    }
                }
            }
            else
            {
                ErroresSem es = new ErroresSem("No existe una variable o arreglo con el nombre " + arreglo.nombre, 0, 0);
                es.insertarErrSem(es);
                Console.WriteLine("No existe una variable o arreglo con el nombre: " + arreglo.nombre);
                return false;
            }

            return flag;
        }

        public static string obtenerValorArregloPos(Arreglos arreglo, Ambito actual)
        {
            string dd = "";
            Boolean flag = false;
            Ambito aux = actual;
            Arreglos apoyo = null;

            //--------- VERIFICAR SI YA EXISTE UN ARREGLO CON EL MISMO NOMBRE
            aux = actual;
            while (aux != null)
            {
                foreach (Arreglos v in aux.arreglosAmbito)
                {
                    if (v.nombre.Equals(arreglo.nombre))
                    {
                        apoyo = v;
                        flag = true;
                    }
                }
                aux = aux.anterior;
            }

            // EXISTE O NO 
            if (flag == true)
            {
                //----VER QUE LAS DIMENSIONES SEAN LAS ADECUADAS
                if (arreglo.cantidadDimensiones == 1)
                {
                    int dimen1 = Convert.ToInt32(apoyo.dimension1);
                    int posi1 = Convert.ToInt32(arreglo.dimension1);
                    if (posi1 > dimen1)
                    {
                        ErroresSem es = new ErroresSem("Las dimensiones que se buscan son incorrectas " + arreglo.nombre, 0, 0);
                        es.insertarErrSem(es);
                        Console.WriteLine("La dimension del arreglo que se desea buscar es incorrecta");
                        return "??";
                    }
                }
                else if (arreglo.cantidadDimensiones == 2)
                {
                    int dimen1 = Int32.Parse(apoyo.dimension1);
                    int dimen2 = Int32.Parse(apoyo.dimension2);
                    int posi1 = Int32.Parse(arreglo.dimension1);
                    int posi2 = Int32.Parse(arreglo.dimension2);
                    if (posi1 > dimen1 || posi2 > dimen2)
                    {
                        ErroresSem es = new ErroresSem("Las dimensiones que se buscan son incorrectas " + arreglo.nombre, 0, 0);
                        es.insertarErrSem(es);
                        Console.WriteLine("La dimension del arreglo que se desea buscar es incorrecta");
                        return "??";
                    }
                }
                else if (arreglo.cantidadDimensiones == 3)
                {
                    int dimen1 = Int32.Parse(apoyo.dimension1);
                    int dimen2 = Int32.Parse(apoyo.dimension2);
                    int dimen3 = Int32.Parse(apoyo.dimension3);
                    int posi1 = Int32.Parse(arreglo.dimension1);
                    int posi2 = Int32.Parse(arreglo.dimension2);
                    int posi3 = Int32.Parse(arreglo.dimension3);
                    if (posi1 > dimen1 || posi2 > dimen2 || posi3 > dimen3)
                    {
                        ErroresSem es = new ErroresSem("Las dimensiones que se buscan son incorrectas " + arreglo.nombre, 0, 0);
                        es.insertarErrSem(es);
                        Console.WriteLine("La dimension del arreglo que se desea buscar es incorrecta");
                        return "??";
                    }
                }else
                {
                    ErroresSem es = new ErroresSem("Las dimensiones que se buscan son incorrectas " + arreglo.nombre, 0, 0);
                    es.insertarErrSem(es);
                }

                //----CALCULAR LA POSICION
                int posicion = 0;
                if (arreglo.cantidadDimensiones == 1)
                {
                    posicion = int.Parse(arreglo.dimension1) - 1;
                }
                else if (arreglo.cantidadDimensiones == 2)
                {
                    int dimen2 = Int32.Parse(apoyo.dimension2);
                    int posi1 = Int32.Parse(arreglo.dimension1);
                    int posi2 = Int32.Parse(arreglo.dimension2);
                    posicion = ((posi1 - 1) * dimen2) + (posi2 - 1);
                }
                else if (arreglo.cantidadDimensiones == 3)
                {
                    int dimen2 = Int32.Parse(apoyo.dimension2);
                    int dimen3 = Int32.Parse(apoyo.dimension3);
                    int posi1 = Int32.Parse(arreglo.dimension1);
                    int posi2 = Int32.Parse(arreglo.dimension2);
                    int posi3 = Int32.Parse(arreglo.dimension3);
                    posicion = ((posi1 - 1) * (dimen2 * dimen3)) + (((posi2 - 1) * dimen3) + (posi3 - 1));
                }
                //----OBTENER EL VALOR 
                aux = actual;
                while (aux != null)
                {
                    foreach (Arreglos v in aux.arreglosAmbito)
                    {
                        if (v.nombre.Equals(arreglo.nombre))
                        {
                            //Recorrer el arreglo en busca de la pos deseada
                            //MessageBox.Show("SE BUSCABA" + v.valoresArreglo[posicion].ToString());
                            dd = v.valoresArreglo[posicion].ToString();
                            return v.valoresArreglo[posicion].ToString();
                        }
                    }
                    aux = aux.anterior;
                }
            }
            else
            {
                ErroresSem es = new ErroresSem("No existe un arreglo con el nombre " + arreglo.nombre, 0, 0);
                es.insertarErrSem(es);
                Console.WriteLine("No existe una variable o arreglo con el nombre: " + arreglo.nombre);
                return "??";
            }

            return dd;
        }

        public static void imprimirArreglo(Arreglos arr)
        {
            foreach (string hijo in arr.valoresArreglo)
            {
                MessageBox.Show(hijo);
            }
        }

        //------------------------- FUNCIONES
        //Insertar Variable ---AQUI SE TIENE QUE ENVIAR LOS ERRORES DE EXISTIR
        public static Boolean insertarFuncion(Funciones variable, Ambito actual)
        {
            Boolean flag = false;
            //--------- VERIFICAR SI YA EXISTE UNA VARIABLE CON EL MISMO NOMBRE
            Ambito aux = actual;

            foreach (Funciones v in aux.funcionesAmbito)
            {
                if (v.nombreF.Equals(variable.nombreF))
                {
                    if (v.overRide.Equals("si") || variable.overRide.Equals("si"))
                        flag = true;
                }
            }
            aux = aux.anterior;


            //--------INSERTAR O NO LA FUNCION
            if (flag == false)  //SI NO EXISTE
            {
                actual.funcionesAmbito.Add(variable);
                return true;
            }
            else
            {
                ErroresSem es = new ErroresSem("Ya existe una funcion con el nombre " + variable.nombreF, 0, 0);
                es.insertarErrSem(es);
                Console.WriteLine("Ya existe una funcion con el nombre: " + variable.nombreF);
                return false;
            }
        }

        public static Funciones buscarFuncion(string variable, Ambito actual)
        {
            Funciones flag = null;
            Boolean ff = false;
            //--------- VERIFICAR SI YA EXISTE UNA VARIABLE CON EL MISMO NOMBRE
            Ambito aux = actual;

            while (aux != null)
            {
                foreach (Funciones v in aux.funcionesAmbito)
                {
                    if (v.nombreF.Equals(variable))
                    {
                        flag = v;
                        ff = true;
                    }
                }
                aux = aux.anterior;
            }
            if (ff == false)
            {
                ErroresSem es = new ErroresSem("No existe una funcion con el nombre " + variable, 0, 0);
                es.insertarErrSem(es);
            }
            return flag;

        }

    }
}
