using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using System.Windows.Forms;
using System.Collections;

namespace OLC2_Proyecto2.Gramatica
{
    class Recorrido
    {
        //Para la clase
        public static string nombreClase = "";

        //Para las importanciones
        public static bool import = false;
        public static ArrayList importanciones = new ArrayList();

        //Para las variables
        public static bool var = false;
        public static ArrayList variables = new ArrayList();
        public static string tipo = "", ambito = "publico", valorVal = "";

        //Para los arreglos
        public static ArrayList dimensiones = new ArrayList();
        public static string dim1 = "", dim2 = "", dim3 = "";

        public static string phrase;
        public static string[] words;

        //Recorrer el arbol
        public static string ejecutar(ParseTreeNode root, Ambito anterior)
        {
            switch (root.ToString())
            {
                #region INICIO 
                case "INICIO":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        ejecutar(hijo, null);
                    }
                    return "";
                #endregion

                //---
                #region CLASE
                case "CLASE":
                    //id + IMPORTACIONES + INS_CLA 
                    if (root.ChildNodes.Count == 3)//tiene importanciones e instrucciones
                    {

                        phrase = root.ChildNodes[0].ToString();
                        words = phrase.Split(' ');
                        nombreClase = words[0];                     //NOMBRE DE LA CLASE ACTUAL
                        Ambito nuevo = new Ambito(anterior);        //SE CREA EL PRIMER AMBITO CON EL ANTERIOR = a nulo

                        Clases c = new Clases(nombreClase, null, null, null, null, null);    //Creamos la clase
                        Clases.insertarClase(c);                                            //Insertarmos la clase

                        //HACER ALGO CON LAS IMPORTACIONES
                        ejecutar(root.ChildNodes[1], nuevo);

                        //RECORRER LAS INSTRUCCIONES --AQUI MANDAR A LLAMAR SOLO EL MAIN
                        ejecutar(root.ChildNodes[2], nuevo);
                        ReporteTS.reporteErrores(nuevo);
                    }
                    else if (root.ChildNodes.Count == 2) // pueden ser dos producciones
                    {
                        //id + INS_CLA
                        if (root.ChildNodes[1].ToString().Equals("INS_CLA"))
                        { //no tiene importanciones pero si instrucciones

                            phrase = root.ChildNodes[0].ToString();
                            words = phrase.Split(' ');
                            nombreClase = words[0];                     //NOMBRE DE LA CLASE ACTUAL
                            Ambito nuevo = new Ambito(anterior);        //SE CREA EL PRIMER AMBITO CON EL ANTERIOR = a nulo

                            Clases c = new Clases(nombreClase, null, null, null, null, null);    //Creamos la clase
                            Clases.insertarClase(c);                                            //Insertarmos la clase

                            //RECORRER LAS INSTRUCCIONES --AQUI MANDAR A LLAMAR SOLO EL MAIN
                            ejecutar(root.ChildNodes[1], nuevo);
                            ReporteTS.reporteErrores(nuevo);
                        }
                        //id + IMPORTACIONES
                        else    //tiene importacion pero no instruccion 
                        {
                            phrase = root.ChildNodes[0].ToString();
                            words = phrase.Split(' ');
                            nombreClase = words[0];                     //NOMBRE DE LA CLASE ACTUAL
                            Ambito nuevo = new Ambito(anterior);        //SE CREA EL PRIMER AMBITO CON EL ANTERIOR = a nulo

                            Clases c = new Clases(nombreClase, null, null, null, null, null);    //Creamos la clase
                            Clases.insertarClase(c);                                            //Insertarmos la clase
                        }
                    }
                    //id   la clase esta vacia
                    else if (root.ChildNodes.Count == 1) {
                        phrase = root.ChildNodes[0].ToString();
                        words = phrase.Split(' ');
                        nombreClase = words[0];                     //NOMBRE DE LA CLASE ACTUAL
                        Ambito nuevo = new Ambito(anterior);        //SE CREA EL PRIMER AMBITO CON EL ANTERIOR = a nulo

                        Clases c = new Clases(nombreClase, null, null, null, null, null);    //Creamos la clase
                        Clases.insertarClase(c);                                            //Insertarmos la clase                        
                    }
                    return "";
                #endregion

                #region INS
                case "INS_CLA":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        ejecutar(hijo, anterior);
                    }
                    return "";
                #endregion

                //--- FALTA VER LOS OBJETOS
                #region DECLARACION
                case "DECLARACIONES":
                    ///TIPO + L_ID + FIN_DECLA
                    if (root.ChildNodes.Count == 3)
                    {
                        ambito = "publico";                                 //Obtener Ambito    
                        tipo = ejecutar(root.ChildNodes[0], anterior);      //Obtener tipo

                        //Obtener Ids
                        var = true;
                        variables.Clear();
                        ejecutar(root.ChildNodes[1], anterior);
                        var = false;

                        valorVal = ejecutar(root.ChildNodes[2], anterior);            //Obtener valor
                        MessageBox.Show(" Valor: " + valorVal);

                        //Almacenar Variables en alguna lista o algo
                        Variables vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {
                                vvv = new Variables(hijo, tipo, ambito, valorVal, nombreClase);
                                Boolean fInserto = Ambito.insertarVarConValor(vvv, anterior);              //INSERTAR VARIABLE EN EL AMBITO
                                if (fInserto == true)
                                    Clases.insertarVariableEnClase(nombreClase, vvv);


                            }
                            else
                            {
                                vvv = new Variables(hijo, tipo, ambito, valorVal, nombreClase);
                                Boolean fInserto = Ambito.insertarVarSinValor(vvv, anterior);                  //VER QUE LA VARIABLE NO EXISTA
                                if (fInserto == true)
                                    Clases.insertarVariableEnClase(nombreClase, vvv);

                            }
                        }
                        //Limpiar variables
                        ambito = tipo = valorVal = "";
                    }
                    //AMBITO + TIPO + L_ID + FIN_DECLA
                    else if (root.ChildNodes.Count == 4)// TIENE TODO
                    {
                        ambito = ejecutar(root.ChildNodes[0], anterior);  //Obtener ambito 
                        tipo = ejecutar(root.ChildNodes[1], anterior);    //Obtener tipo

                        //Obtener Ids
                        var = true;
                        variables.Clear();
                        ejecutar(root.ChildNodes[2], anterior);
                        var = false;

                        valorVal = ejecutar(root.ChildNodes[3], anterior);        //Obtener valor
                        MessageBox.Show(" Valor: " + valorVal);

                        //Almacenar Variables en alguna lista o algo
                        Variables vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {
                                vvv = new Variables(hijo, tipo, ambito, valorVal, nombreClase);
                                Boolean fInserto = Ambito.insertarVarConValor(vvv, anterior);              //INSERTAR VARIABLE EN EL AMBITO
                                if (fInserto == true)
                                    Clases.insertarVariableEnClase(nombreClase, vvv);
                            }
                            else
                            {
                                vvv = new Variables(hijo, tipo, ambito, valorVal, nombreClase);
                                Boolean fInserto = Ambito.insertarVarSinValor(vvv, anterior);                  //VER QUE LA VARIABLE NO EXISTA
                                if (fInserto == true)
                                    Clases.insertarVariableEnClase(nombreClase, vvv);
                            }
                        }
                        //Limpiar variables
                        ambito = tipo = valorVal = "";
                    }
                    return "";
                #endregion

                #region AMBITO
                case "AMBITO":
                    phrase = root.ChildNodes[0].ToString();
                    words = phrase.Split(' ');
                    return words[0].ToLower();
                #endregion

                #region TIPO
                case "TIPO":
                    phrase = root.ChildNodes[0].ToString();
                    words = phrase.Split(' ');
                    return words[0].ToLower();
                #endregion

                //--
                #region L_ID
                case "L_ID":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        if (var == true)                //SI SON VARIABLES
                        {
                            phrase = hijo.ToString();
                            words = phrase.Split(' ');
                            variables.Add(words[0].ToLower());
                        }
                        else if (import == true)        //SI SON IMPORT
                        {
                            phrase = hijo.ToString();
                            words = phrase.Split(' ');
                            importanciones.Add(words[0].ToLower());
                        }
                    }
                    return "";
                #endregion

                //--- FALTA VER LOS OBJETOS
                #region FIN_DECLA
                case "FIN_DECLA":
                    if (root.ChildNodes.Count == 0)// ES SOLO UNA DECLARACION ";"
                        return ";";
                    else if (root.ChildNodes.Count == 1) //TIENE ASIGNACION
                        return ejecutar(root.ChildNodes[0], anterior);
                    else if (root.ChildNodes.Count == 2) // Es objeto
                    {
                        //phrase = root.ChildNodes[1].ToString();
                        //words = phrase.Split(' ');
                        //return words[0];
                        return ";";
                    }
                    else
                        return "";
                #endregion

                #region EXPRESION
                case "EXPRESION":
                    //EXPRESION SIMBOLO EXPRESION
                    if (root.ChildNodes.Count == 3)
                    {
                        phrase = root.ChildNodes[1].ToString();
                        words = phrase.Split(' ');
                        if (words[0].Equals("||"))
                            return ComprobarLogico(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), "||");
                        else if (words[0].Equals("&&"))
                            return ComprobarLogico(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), "&&");
                        //----RELACIONALES
                        else if (words[0].Equals(">"))
                            return ComprobarMayor(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                        else if (words[0].Equals(">="))
                            return ComprobarMayorI(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                        else if (words[0].Equals("<"))
                            return ComprobarMenor(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                        else if (words[0].Equals("<="))
                            return ComprobarMenorI(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                        else if (words[0].Equals("=="))
                            return ComprobarIgual(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                        else if (words[0].Equals("!="))
                            return ComprobarDiferente(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                        //----ARITMETICOS
                        else if (words[0].Equals("+"))
                            return ComprobarSuma(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                        else if (words[0].Equals("-"))
                            return ComprobarResta(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                        else if (words[0].Equals("*"))
                            return ComprobarMulti(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                        else if (words[0].Equals("/"))
                            return ComprobarDivision(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                        else if (words[0].Equals("^"))
                            return ComprobarPotencia(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior));
                    }
                    else if (root.ChildNodes.Count == 2)
                    {
                        //P DIMENSIONES
                        if (root.ChildNodes[0].ToString().Equals("P"))
                        {
                            //----- ESTO ES UN ARREGLO
                            string nombreArr = ejecutar(root.ChildNodes[0], anterior);
                            if (nombreArr.Contains("@@") )
                            {
                                nombreArr = nombreArr.Substring(2);
                            }
                            //----- OBTENER DIMENSIONES
                            dimensiones.Clear();
                            ejecutar(root.ChildNodes[1], anterior);
                            obtenerDimensionesArreglo(anterior);
                            //----- OBTENER VALOR DEL ARREGLO
                            Arreglos nn = new Arreglos(nombreArr, "", "", "", dimensiones.Count, dim1, dim2, dim3, null, 0);
                            string esAlgo = Ambito.obtenerValorArregloPos(nn, anterior);
                            if (esAlgo.Contains("??") )
                            {
                                return "";
                            }
                            else
                            {
                                return esAlgo;
                            }
                        }
                        // not EXPR
                        else
                            return ComprobarLogico(ejecutar(root.ChildNodes[1], anterior), "00", "!");

                    }
                    else if (root.ChildNodes.Count == 1)
                    {
                        // ( EXPRESION )
                        if (root.ChildNodes[0].ToString().Equals("EXPRESION"))
                        {
                            return ejecutar(root.ChildNodes[0], anterior);
                        }
                        //P
                        else
                        {
                            return ejecutar(root.ChildNodes[0], anterior);
                        }
                    }
                    return "";
                #endregion

                #region P
                case "P":
                    if (root.ChildNodes[0].ToString().Equals("BOOL"))
                    {
                        return ejecutar(root.ChildNodes[0], anterior);
                    }
                    else if (root.ChildNodes[0].ToString().Equals("OBJETOS"))
                    {
                        return ejecutar(root.ChildNodes[0], anterior);
                    }
                    else
                    {
                        phrase = root.ChildNodes[0].ToString();
                        words = phrase.Split(' ');
                        if (words[1].Contains("TkCadena"))
                            return "\"" + words[0].ToLower() + "\"";
                        else if (words[1].Contains("TkCarac"))
                        {
                            return "'" + words[0] + "'";
                        }
                        else if (words[1].Contains("id"))
                            return "@@" + words[0].ToLower();
                        else
                            return words[0];
                    }
                #endregion

                #region BOOL
                case "BOOL":
                    phrase = root.ChildNodes[0].ToString();
                    words = phrase.Split(' ');
                    if (words[0].ToLower().Equals("verdadero") || words[0].ToLower().Equals("true"))
                        return "verdadero";
                    else if (words[0].Contains("falso") || words[0].Contains("false"))
                        return "falso";
                    else
                        return words[0];
                #endregion

                #region ASIGNA
                case "ASIGNA":
                    //id + Igual + EXPRESION + PuntoComa
                    string n, v;

                    //NOMBRE DE LA VARIABLE 0 
                    phrase = root.ChildNodes[0].ToString();
                    words = phrase.Split(' ');
                    n = words[0];

                    v = ejecutar(root.ChildNodes[1], anterior);       //VALOR DE LA VARIABLE

                    //GUARDAR VALOR DE LA ASIGNACION
                    Variables n1 = new Variables(n, "", "", v, "");
                    Boolean f = Ambito.CambiarValorVariable(anterior, n1);

                    return "";
                #endregion

                #region INCREMENTO
                case "INCREMENTO":
                    //P + incremento + PuntoComa;
                    string incre = ejecutar(root.ChildNodes[0], anterior);
                    return realizarIncremento(anterior, incre);
                #endregion

                #region DECREMENTO
                case "DECREMENTO":
                    //P + incremento + PuntoComa;
                    string decre = ejecutar(root.ChildNodes[0], anterior);
                    return realizarDecremento(anterior, decre);
                #endregion

                ///-------------- ARREGLO
                #region DECLA_ARRE
                case "DECLA_ARRE":
                    //AMBITO + TIPO + array + L_ID + DIMENSIONES + FIN_ARRE  5
                    if (root.ChildNodes.Count() == 5)
                    {
                        ambito = tipo = valorVal = "";
                        ambito = ejecutar(root.ChildNodes[0], anterior);     //Obtener Ambito    
                        tipo = ejecutar(root.ChildNodes[1], anterior);      //Obtener tipo

                        //Obtener Ids
                        var = true;
                        variables.Clear();
                        ejecutar(root.ChildNodes[2], anterior);
                        var = false;

                        //Dimensiones
                        dimensiones.Clear();
                        ejecutar(root.ChildNodes[3], anterior);

                        //Valor
                        valorVal = ejecutar(root.ChildNodes[4], anterior);            //Obtener valor
                        MessageBox.Show("ValArr: " + valorVal);

                        //Almacenar los arreglos en alguna lista o algo
                        Arreglos vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {
                                obtenerDimensionesArreglo(anterior);                        // ---OBTENER LAS DIMENSIONES DEL ARREGLO
                                ArrayList nue = obtenerValoresArreglo(anterior,valorVal);   // ---OBTENER LOS VALORES DEL ARREGLO
                                vvv = new Arreglos(hijo, tipo, ambito, nombreClase, dimensiones.Count, dim1, dim2, dim3, nue, nue.Count);

                                Boolean fInserto = Ambito.insertarArrConValor(vvv, anterior);              //INSERTAR VARIABLE EN EL AMBITO
                            }
                            else
                            {
                                obtenerDimensionesArreglo(anterior);        // ---OBTENER LAS DIMENSIONES DEL ARREGLO
                                vvv = new Arreglos(hijo, tipo, ambito, nombreClase, dimensiones.Count, dim1, dim2, dim3, null, 0);
                                
                                Boolean fInserto = Ambito.insertarArrSinValor(vvv, anterior);              //INSERTAR VARIABLE EN EL AMBITO
                            }
                        }
                        
                        ambito = tipo = valorVal = dim1 = dim2 = dim3 = "";     //Limpiar variables
                    }
                    //TIPO + array + L_ID + DIMENSIONES + FIN_ARRE 4
                    else if (root.ChildNodes.Count() == 4)
                    {
                        ambito = tipo = valorVal = "";
                        ambito = "publico";                                 //Obtener Ambito    
                        tipo = ejecutar(root.ChildNodes[0], anterior);      //Obtener tipo

                        //Obtener Ids
                        var = true;
                        variables.Clear();
                        ejecutar(root.ChildNodes[1], anterior);
                        var = false;

                        //Dimensiones
                        dimensiones.Clear();
                        ejecutar(root.ChildNodes[2], anterior);

                        //Valor
                        valorVal = ejecutar(root.ChildNodes[3], anterior);            //Obtener valor
                        MessageBox.Show("ValArr: " + valorVal);

                        //Almacenar los arreglos en alguna lista o algo
                        Arreglos vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {
                                obtenerDimensionesArreglo(anterior);                        // ---OBTENER LAS DIMENSIONES DEL ARREGLO
                                ArrayList nue = obtenerValoresArreglo(anterior, valorVal);  // ---OBTENER LOS VALORES DEL ARREGLO
                                vvv = new Arreglos(hijo, tipo, ambito, nombreClase, dimensiones.Count, dim1, dim2, dim3, nue, nue.Count);

                                Boolean fInserto = Ambito.insertarArrConValor(vvv, anterior);              //INSERTAR VARIABLE EN EL AMBITO
                            }
                            else
                            {
                                obtenerDimensionesArreglo(anterior);        // ---OBTENER LAS DIMENSIONES DEL ARREGLO
                                vvv = new Arreglos(hijo, tipo, ambito, nombreClase, dimensiones.Count, dim1, dim2, dim3, null, 0);

                                Boolean fInserto = Ambito.insertarArrSinValor(vvv, anterior);              //INSERTAR VARIABLE EN EL AMBITO
                            }
                        }

                        ambito = tipo = valorVal = dim1 = dim2 = dim3 = "";     //Limpiar variables
                    }
                    
                    return "";
                #endregion

                #region DIMENSIONES
                case "DIMENSIONES":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        ejecutar(hijo, anterior);
                    }
                    return "";
                #endregion

                #region VAL_DIM
                case "VAL_DIM":
                    string valDim = ejecutar(root.ChildNodes[0], anterior);
                    dimensiones.Add(valDim);
                    return "";
                #endregion

                #region FIN_ARRE
                case "FIN_ARRE":
                    if (root.ChildNodes.Count == 0)// ES SOLO UNA DECLARACION ";"
                        return ";";
                    else if (root.ChildNodes.Count == 1) //TIENE ASIGNACION
                        return ejecutar(root.ChildNodes[0], anterior);
                    else
                        return "";
                #endregion

                #region VAL_AA
                case "VAL_AA":
                    string di1 = "";
                    int die1 = 0;
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        if (die1 != 0)
                            di1 += ",";
                        di1 += ejecutar(hijo, anterior);
                        die1++;
                    }
                    return di1;
                #endregion

                #region VAL_AA1
                case "VAL_AA1":
                    return ejecutar(root.ChildNodes[0],anterior);
                #endregion

                #region VAA
                case "VAA":
                    string di = "";
                    int die = 0;
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        if (die != 0)
                            di += ",";
                        di += ejecutar(hijo, anterior);
                        die++; 
                    }
                    return di;
                #endregion

                //--REASIGNACION
                #region USO_ARRE
                case "USO_ARRE":
                    //  id + DIMENSIONES + Igual + EXPRESION + PuntoComa;
                    string nomArre = "";
                    phrase = root.ChildNodes[0].ToString();
                    words = phrase.Split(' ');
                    nomArre = words[0];

                    //Dimensiones
                    dimensiones.Clear();
                    ejecutar(root.ChildNodes[1], anterior);

                    //Valor
                    valorVal = ejecutar(root.ChildNodes[2], anterior);            //Obtener valor
                    MessageBox.Show(nomArre+" ValArr: " + valorVal);

                    //CAMBIAR VALOR
                    obtenerDimensionesArreglo(anterior);
                    ArrayList emergencia = new ArrayList();
                    emergencia.Add(valorVal);
                    Arreglos nuevoA = new Arreglos(nomArre, "", "","",dimensiones.Count,dim1, dim2, dim3,emergencia,emergencia.Count);
                    if (1+1 == 2)
                    {
                        Boolean fInserto = Ambito.asignarValorArreglo(nuevoA, anterior, valorVal);
                    }
                    valorVal = dim1 = dim2 = dim3 = "";
                    return "";
                #endregion

                ///-------------- FIN ARREGLO
                default:
                    return "";
                

            }
        }

        


        //---------COMPROBACION LOGICA
        public static string ComprobarLogico(string val1, string val2, string operador)
        {
            int valorNumerico = 0, valorNumerico1 = 0;
            Double valorNumeri = 0;
            string v1 = "", v2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("1." + val1 + " 2." + val2);

            #region OBTENER
            //objetos

            //VERIFICAR PRIMERO SI ES ID, PARA LUEGO PODER ANALIZAR SU CONTENIDO
            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                v1 = Clases.obtenerValorVariable(nombreClase, val1);
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                v2 = Clases.obtenerValorVariable(nombreClase, val2);
            }
            //Arreglo

            //--------VALORES NO VALIDOS
            //Double
            if (Double.TryParse(val1, out valorNumeri) ||
                Double.TryParse(val2, out valorNumeri))
            {
                Console.WriteLine("No se puede hacer uso de objetos de tipo Double para la operacion logica");
                return "????";
            }
            //num
            else if (int.TryParse(val1, out valorNumerico) ||
                int.TryParse(val2, out valorNumerico1))
            {
                Console.WriteLine("No se puede hacer uso de objetos de tipo Int para la operacion logica");
                return "????";
            }
            //caracter
            else if (val1.StartsWith("'") || val2.StartsWith("'"))
            {
                Console.WriteLine("No se puede hacer uso de objetos de tipo Char para la operacion logica");
                return "????";
            }
            //cadena                
            else if (val1.StartsWith("\"") || val2.StartsWith("\""))
            {
                Console.WriteLine("No se puede hacer uso de objetos de tipo String para la operacion logica");
                return "????";
            }
            //--------VALORES VALIDOS
            //bool      
            if (val1.Contains("verdadero") || val1.Contains("falso"))
                v1 = val1;
            if (val1.Contains("verdadero") || val1.Contains("falso"))
                v2 = val2;
            #endregion

            #region Operaciones
            if (operador.Equals("||"))
            {
                if (v1.Contains("verdadero") || v2.Contains("verdadero"))
                    return "verdadero";
                else
                    return "falso";
            }
            else if (operador.Equals("&&"))
            {
                if (v1.Contains("verdadero") && v2.Contains("verdadero"))
                    return "verdadero";
                else
                    return "falso";
            }
            else if (operador.Equals("!"))
            {
                if (v1.Contains("verdadero"))
                    return "falso";
                else
                    return "verdadero";
            }
            else
            {
                return "falso";
            }
            #endregion
        }

        //---------COMPROBACION RELACIONAL
        public static string ComprobarMayor(string val1, string val2)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("1." + val1 + " 2." + val2);
            //OBTENER LOS VALORES A OPERAR
            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }

            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion

            string resultado = "falso";
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                if (Int32.Parse(v1) > Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                if (Int32.Parse(v1) > Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Int32.Parse(v1) > my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                if (Int32.Parse(v1) > f)
                    resultado = "verdadero";
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                if (Convert.ToDouble(v1) > Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                if (Convert.ToDouble(v1) > Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Convert.ToDouble(v1) > my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] > Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] > Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                if (my[0] > my1[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                resultado = "????";

            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            return resultado;
            #endregion
        }

        public static string ComprobarMenor(string val1, string val2)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("1." + val1 + " 2." + val2);

            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }

            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion

            string resultado = "falso";
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                if (Int32.Parse(v1) < Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                if (Int32.Parse(v1) < Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Int32.Parse(v1) < my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                if (Int32.Parse(v1) < f)
                    resultado = "verdadero";
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                if (Convert.ToDouble(v1) < Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                if (Convert.ToDouble(v1) < Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Convert.ToDouble(v1) < my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] < Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] < Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                if (my[0] < my1[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                resultado = "????";

            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            return resultado;
            #endregion
        }

        public static string ComprobarMayorI(string val1, string val2)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("1." + val1 + " 2." + val2);
            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }

            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion

            string resultado = "falso";
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                if (Int32.Parse(v1) >= Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                if (Int32.Parse(v1) >= Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Int32.Parse(v1) >= my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                if (Int32.Parse(v1) >= f)
                    resultado = "verdadero";
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                if (Convert.ToDouble(v1) >= Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                if (Convert.ToDouble(v1) >= Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Convert.ToDouble(v1) >= my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] >= Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] >= Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                if (my[0] >= my1[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                resultado = "????";

            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            return resultado;
            #endregion
        }

        public static string ComprobarMenorI(string val1, string val2)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("1." + val1 + " 2." + val2);

            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }

            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion

            string resultado = "falso";
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                if (Int32.Parse(v1) <= Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                if (Int32.Parse(v1) <= Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Int32.Parse(v1) <= my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                if (Int32.Parse(v1) <= f)
                    resultado = "verdadero";
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                if (Convert.ToDouble(v1) <= Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                if (Convert.ToDouble(v1) <= Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Convert.ToDouble(v1) <= my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] <= Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] <= Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                if (my[0] <= my1[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                resultado = "????";

            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            return resultado;
            #endregion
        }

        public static string ComprobarIgual(string val1, string val2)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("1." + val1 + " 2." + val2);
            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }

            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion

            string resultado = "falso";
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                if (Int32.Parse(v1) == Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                if (Int32.Parse(v1) == Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Int32.Parse(v1) == my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                if (Int32.Parse(v1) == f)
                    resultado = "verdadero";
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                if (v1.Equals(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                if (Convert.ToDouble(v1) == Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                if (Convert.ToDouble(v1) == Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Convert.ToDouble(v1) == my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] == Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] == Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                if (my[0] == my1[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                resultado = "????";

            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                if (v1.Equals(v2))
                    resultado = "verdadero";
            }
            return resultado;
            #endregion
        }

        public static string ComprobarDiferente(string val1, string val2)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("1." + val1 + " 2." + val2);
            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }

            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion

            string resultado = "falso";
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                if (Int32.Parse(v1) != Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                if (Int32.Parse(v1) != Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Int32.Parse(v1) != my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                if (Int32.Parse(v1) != f)
                    resultado = "verdadero";
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                if (!v1.Equals(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                if (Convert.ToDouble(v1) != Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                if (Convert.ToDouble(v1) != Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                if (Convert.ToDouble(v1) != my[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] != Int32.Parse(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                if (my[0] != Convert.ToDouble(v2))
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                if (my[0] != my1[0])
                    resultado = "verdadero";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {
                resultado = "????";
            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                resultado = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                resultado = "????";

            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                if (!v1.Equals(v2))
                    resultado = "verdadero";
            }
            return resultado;
            #endregion
        }

        //---------COMPROBACION ARITMETICA
        public static string ComprobarSuma(string val1, string val2)
        {
            string v1 = "", v2 = "";
            string tipo1 = "", tipo2 = "", suma = "";
            int sum = 0;
            double sum1 = 0.0;
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("Suma #1 " + val1 + " #2 " + val2);
            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }
            //Double
            Double valNumericoD = 0.0;
            if (double.TryParse(val1, out valNumericoD))
            {
                tipo1 = "double";
                v1 = val1;
            }
            if (double.TryParse(val2, out valNumericoD))
            {
                tipo2 = "double";
                v2 = val2;
            }
            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion

            //que tipo de suma es
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                sum = Int32.Parse(v1) + Int32.Parse(v2);
                suma = sum.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                suma = v1 + " " + v2;
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                sum1 = Int32.Parse(v1) + Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum = Int32.Parse(v1) + my[0];
                suma = sum.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = Int32.Parse(v1) + f;
                suma = sum.ToString();
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                suma = v1 + " " + v2;
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                suma = v1 + " " + v2;
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                suma = v1 + " " + v2;
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                suma = v1 + " " + v2;
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                suma = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                sum1 = Convert.ToDouble(v1) + Int32.Parse(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                suma = v1 + " " + v2;
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                sum1 = Convert.ToDouble(v1) + Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum1 = Convert.ToDouble(v1) + my[0];
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum1 = Convert.ToDouble(v1) + f;
                suma = sum1.ToString();
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                sum = my[0] + Int32.Parse(v2);
                suma = sum.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                suma = v1 + " " + v2;
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                sum1 = my[0] + Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                sum = my[0] + my1[0];
                suma = sum.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {
                char[] my = v1.ToCharArray();
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = my[0] + f;
                suma = sum.ToString();
            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;

                sum = Int32.Parse(v2) + f;
                suma = sum.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;
                sum1 = Convert.ToDouble(v2) + f;
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;
                sum = f + my[0];
                suma = sum.ToString();

            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                int f = 0, f1 = 0;
                if (v1.Contains("verdadero"))
                    f = 1;
                if (v2.Contains("verdadero"))
                    f1 = 1;
                if (f1 == 1 || f == 1)
                    return "verdadero";
                else
                    return "false";
            }
            #endregion

            return suma;
        }

        public static string ComprobarResta(string val1, string val2)
        {
            string v1 = "", v2 = "";
            string tipo1 = "", tipo2 = "", suma = "";
            int sum = 0;
            double sum1 = 0.0;
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("Resta #1 " + val1 + " #2 " + val2);
            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }

            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion


            //que tipo de resta es
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                sum = Int32.Parse(v1) + Int32.Parse(v2);
                suma = sum.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                sum1 = Int32.Parse(v1) - Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum = Int32.Parse(v1) - my[0];
                suma = sum.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = Int32.Parse(v1) - f;
                suma = sum.ToString();
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                suma = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                sum1 = Convert.ToDouble(v1) - Int32.Parse(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                sum1 = Convert.ToDouble(v1) - Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum1 = Convert.ToDouble(v1) - my[0];
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum1 = Convert.ToDouble(v1) - f;
                suma = sum1.ToString();
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                sum = my[0] - Int32.Parse(v2);
                suma = sum.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                sum1 = my[0] - Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                sum = my[0] + my1[0];
                suma = sum.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {
                suma = "????";
            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;

                sum = Int32.Parse(v2) - f;
                suma = sum.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;
                sum1 = Convert.ToDouble(v2) - f;
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                suma = "????";

            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                suma = "????";
            }
            #endregion

            return suma;
        }

        public static string ComprobarMulti(string val1, string val2)
        {
            string v1 = "", v2 = "";
            string tipo1 = "", tipo2 = "", suma = "";
            int sum = 0;
            double sum1 = 0.0;
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("Multi #1 " + val1 + " #2 " + val2);
            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }

            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion


            //que tipo de multiplicacion es
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                sum = Int32.Parse(v1) * Int32.Parse(v2);
                suma = sum.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                sum1 = Int32.Parse(v1) * Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum = Int32.Parse(v1) * my[0];
                suma = sum.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = Int32.Parse(v1) * f;
                suma = sum.ToString();
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                suma = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                sum1 = Convert.ToDouble(v1) * Int32.Parse(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                sum1 = Convert.ToDouble(v1) * Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum1 = Convert.ToDouble(v1) * my[0];
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum1 = Convert.ToDouble(v1) * f;
                suma = sum1.ToString();
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                sum = my[0] * Int32.Parse(v2);
                suma = sum.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                sum1 = my[0] * Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                sum = my[0] * my1[0];
                suma = sum.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {

                char[] my = v1.ToCharArray();
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = my[0] * f;
                suma = sum.ToString();

            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;

                sum = Int32.Parse(v2) * f;
                suma = sum.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;
                sum1 = Convert.ToDouble(v2) * f;
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;

                sum = my[0] * f;
                suma = sum.ToString();

            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                int f = 0, f1 = 0;
                if (v1.Contains("verdadero"))
                    f = 1;
                if (v2.Contains("verdadero"))
                    f1 = 1;
                if (f1 == 1 && f == 1)
                    return "verdadero";
                else
                    return "false";
            }
            #endregion

            return suma;
        }

        public static string ComprobarDivision(string val1, string val2)
        {
            string v1 = "", v2 = "";
            string tipo1 = "", tipo2 = "", suma = "";
            int sum = 0;
            double sum1 = 0.0;
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("Div #1 " + val1 + " #2 " + val2);
            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }

            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion


            //que tipo de resta es
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                sum1 = Int32.Parse(v1) / Int32.Parse(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                sum1 = Int32.Parse(v1) / Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum1 = Int32.Parse(v1) / my[0];
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = Int32.Parse(v1) / f;
                suma = sum.ToString();
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                suma = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                sum1 = Convert.ToDouble(v1) / Int32.Parse(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                sum1 = Convert.ToDouble(v1) / Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum1 = Convert.ToDouble(v1) / my[0];
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum1 = Convert.ToDouble(v1) / f;
                suma = sum1.ToString();
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                sum1 = my[0] / Int32.Parse(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                sum1 = my[0] / Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                sum1 = my[0] / my1[0];
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {
                char[] my = v1.ToCharArray();
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = my[0] / f;
                suma = sum.ToString();
            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;

                sum1 = Int32.Parse(v2) / f;
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;
                sum1 = Convert.ToDouble(v2) / f;
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;

                sum1 = my[0] / f;
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                suma = "????";
            }
            #endregion

            return suma;
        }

        public static string ComprobarPotencia(string val1, string val2)
        {
            string v1 = "", v2 = "";
            string tipo1 = "", tipo2 = "", suma = "";
            int sum = 0;
            double sum1 = 0.0;
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
                return "????";
            MessageBox.Show("Potencia #1 " + val1 + " #2 " + val2);
            #region OBTENER
            //id
            if (val1.StartsWith("@@"))
            {
                v1 = val1.Substring(2);
                val1 = Clases.obtenerValorVariable(nombreClase, v1);
            }
            if (val2.StartsWith("@@"))
            {
                v2 = val2.Substring(2);
                val2 = Clases.obtenerValorVariable(nombreClase, v2);
            }
            //objetos
            //Arreglos

            //num
            int valorNumerico = 0, valorNumerico1 = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico1))
            {
                tipo2 = "int";
                v2 = val2;
            }

            //caracter
            if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "char";
            }
            if (val2.Contains("'"))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "char";
            }

            //cadena    /*error*/            
            if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 2);
                tipo2 = "string";
            }

            //bool      /*error*/
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo1 = "bool";
                v1 = val1;
            }
            if (val1.Contains("verdadero") || val1.Contains("falso"))
            {
                tipo2 = "bool";
                v2 = val2;
            }
            #endregion


            //que tipo de multiplicacion es
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                sum = Int32.Parse(v1) * Int32.Parse(v2);
                suma = sum.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                sum1 = Int32.Parse(v1) * Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum = Int32.Parse(v1) * my[0];
                suma = sum.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = Int32.Parse(v1) * f;
                suma = sum.ToString();
            }
            #endregion

            //PRIMER VALOR CON STRING
            #region V1_String
            if (tipo1.Equals("string") && tipo2.Equals("int"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("bool"))
            {
                suma = "????";
            }
            #endregion

            //PRIMER VALOR CON DOUBLE
            #region V1_DOUBLE
            if (tipo1.Equals("double") && tipo2.Equals("int"))
            {
                sum1 = Convert.ToDouble(v1) * Int32.Parse(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                sum1 = Convert.ToDouble(v1) * Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum1 = Convert.ToDouble(v1) * my[0];
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum1 = Convert.ToDouble(v1) * f;
                suma = sum1.ToString();
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                sum = my[0] * Int32.Parse(v2);
                suma = sum.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                sum1 = my[0] * Convert.ToDouble(v2);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                sum = my[0] * my1[0];
                suma = sum.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {

                char[] my = v1.ToCharArray();
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = my[0] * f;
                suma = sum.ToString();

            }
            #endregion

            //PRIMER VALOR CON BOOL
            #region V1_BOOL
            if (tipo1.Equals("bool") && tipo2.Equals("int"))
            {
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;

                sum = Int32.Parse(v2) * f;
                suma = sum.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("double"))
            {
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;
                sum1 = Convert.ToDouble(v2) * f;
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;

                sum = my[0] * f;
                suma = sum.ToString();

            }
            else if (tipo1.Equals("bool") && tipo2.Equals("bool"))
            {
                return "????";
            }
            #endregion

            return suma;
        }


        //--------- REALIZAR INCREMENTO FALTA
        public static string realizarIncremento(Ambito am, string incre)
        {
            //Double
            Double valNumericoD = 0.0;
            int valNumericoI = 0;
            if (double.TryParse(incre, out valNumericoD))
            {
                return (Convert.ToDouble(incre) + 1).ToString();
            }
            //Int
            else if (int.TryParse(incre, out valNumericoI))
            {
                return (Convert.ToInt32(incre) + 1).ToString();
            }
            //Char
            else if (incre.Contains("'"))
            {
                char[] my = incre.ToCharArray();
                return ( 1 + my[0]).ToString();
            }
            //Id
            else if (incre.Contains("@"))
            {
                string va = incre.Substring(2);
                Variables v = new Variables(va, "", "", "", "");
                Boolean ff = Ambito.AumentarValorVariable(am,v);
                if (ff)
                {
                    Variables vv = Ambito.obtenerValorVariable(am, incre);
                    return vv.valor;
                }
                return "";
            }
            //Objeto
            //Si es algo mas
            else
            {
                return "";
            }
        }

        //--------- REALIZAR INCREMENTO FALTA
        public static string realizarDecremento(Ambito am, string incre)
        {
            //Double
            Double valNumericoD = 0.0;
            int valNumericoI = 0;
            if (double.TryParse(incre, out valNumericoD))
            {
                return (Convert.ToDouble(incre) - 1).ToString();
            }
            //Int
            else if (int.TryParse(incre, out valNumericoI))
            {
                return (Convert.ToInt32(incre) - 1).ToString();
            }
            //Char
            else if (incre.Contains("'"))
            {
                char[] my = incre.ToCharArray();
                return (my[0]-1).ToString();
            }
            //Id
            else if (incre.Contains("@"))
            {
                string va = incre.Substring(2);
                Variables v = new Variables(va, "", "", "", "");
                Boolean ff = Ambito.DisminuirValorVariable(am, v);
                if (ff)
                {
                    Variables vv = Ambito.obtenerValorVariable(am, incre);
                    return vv.valor;
                }
                return "";
            }
            //Objeto
            //Si es algo mas
            else
            {
                return "";
            }
        }

        //--------- OBTENER LOS VALORES DE LAS DIMENSIONES ARREGLO
        public static void obtenerDimensionesArreglo(Ambito ambito)
        {
            int c = 0;
            foreach (string hijo in dimensiones)
            {
                string val = "";
                if (hijo.Contains("@@"))
                {
                    Variables aux = Ambito.obtenerValorVariable(ambito, hijo);
                    val = aux.valor;
                }
                else
                    val = hijo;

                if (c == 0)
                    dim1 = val;
                else if (c == 1)
                    dim2 = val;
                else
                    dim3 = val;

                c++;
            }
        }

        //--------- LIMPIAR LOS VALORES DEL ARREGLO
        public static ArrayList obtenerValoresArreglo(Ambito ambito, string valores)
        {
            ArrayList nuevo = new ArrayList();

            string[] words = valores.Split(',');
            foreach (string word in words)
            {
                string val = "";
                if (word.Contains("@@"))
                {
                    Variables aux = Ambito.obtenerValorVariable(ambito, word);
                    val = aux.valor;
                    nuevo.Add(val);
                }
                else
                    nuevo.Add(word);
            }
            return nuevo;
        }



    }
}
