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
        public static ArrayList dimensiones = new ArrayList();
        public static string tipo = "", ambito = "publico", valorVal = "";

        public static string phrase;
        public static string[] words;

        //Recorrer el arbol
        public static string ejecutar(ParseTreeNode root)
        {
            switch (root.ToString())
            {

                #region INICIO
                case "INICIO":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        ejecutar(hijo);
                    }
                    return "";
                #endregion

                //--
                #region CLASE
                case "CLASE":
                    if (root.ChildNodes.Count == 3)//tiene importanciones e instrucciones
                    {
                        //NOMBRE DE LA CLASE ACTUAL
                        nombreClase = root.ChildNodes[0].ToString();
                        Clases c = new Clases(nombreClase, null, null, null, null);
                        MessageBox.Show("Nombre de la clase: " + c.nombre);
                        Clases.insertarClase(c);

                        //HACER ALGO CON LAS IMPORTACIONES
                        ejecutar(root.ChildNodes[1]);

                        //RECORRER LAS INSTRUCCIONES --AQUI MANDAR A LLAMAR SOLO EL MAIN
                        ejecutar(root.ChildNodes[2]);

                        //----HACER ALGO EXTRA

                    }
                    else if (root.ChildNodes.Count == 2) // pueden ser dos producciones
                    {
                        if (root.ChildNodes[1].ToString().Equals("INS_CLA"))
                        { //no tiene importanciones pero si instrucciones
                          //NOMBRE DE LA CLASE ACTUAL
                            phrase = root.ChildNodes[0].ToString();
                            words = phrase.Split(' ');
                            nombreClase = words[0];
                            Clases c = new Clases(nombreClase, null, null, null, null);
                            MessageBox.Show("Nombre de la clase: " + c.nombre);
                            Clases.insertarClase(c);

                            //RECORRER LAS INSTRUCCIONES --AQUI MANDAR A LLAMAR SOLO EL MAIN
                            ejecutar(root.ChildNodes[1]);

                            //----HACER ALGO EXTRA

                        }
                        else { } //tiene importacion pero no instruccion 
                    }
                    else if (root.ChildNodes.Count == 1) { }//CLASE VACIA
                    return "";
                #endregion

                //--
                #region IMPORTACIONES
                case "IMPORTACIONES":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        import = true;
                        ejecutar(hijo);
                        import = false;
                        //Hacer algo con las importaciones
                    }
                    return "";
                #endregion

                #region INS
                case "INS_CLA":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        ejecutar(hijo);
                    }
                    return "";
                #endregion

                //--
                #region DECLARACION
                case "DECLARACIONES":
                    ///TIPO + L_ID + FIN_DECLA
                    if (root.ChildNodes.Count == 3)
                    {
                        //Obtener Ambito
                        ambito = "publico";
                        //Obtener tipo
                        tipo = ejecutar(root.ChildNodes[0]);
                        //Obtener Ids
                        var = true;
                        variables.Clear();
                        ejecutar(root.ChildNodes[1]);
                        var = false;
                        //Obtener valor
                        valorVal = ejecutar(root.ChildNodes[2]);
                        MessageBox.Show(" V: " + valorVal);

                        //Almacenar Variables en alguna lista o algo
                        Variables vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {
                                vvv = new Variables(hijo, tipo, ambito, valorVal, nombreClase);
                                Clases.insertarVariableC(nombreClase, vvv);
                            }
                            else
                            {
                                vvv = new Variables(hijo, tipo, ambito, "", nombreClase);
                                Clases.insertarVariableC1(nombreClase, vvv);
                            }
                        }
                        //Limpiar variables
                        ambito = tipo = valorVal = "";
                    }
                    //AMBITO + TIPO + L_ID + FIN_DECLA
                    else if (root.ChildNodes.Count == 4)// TIENE TODO
                    {
                        //Obtener ambito 
                        ambito = ejecutar(root.ChildNodes[0]);
                        //Obtener tipo
                        tipo = ejecutar(root.ChildNodes[0]);
                        //Obtener Ids
                        var = true;
                        variables.Clear();
                        ejecutar(root.ChildNodes[1]);
                        var = false;
                        //Obtener valor
                        valorVal = ejecutar(root.ChildNodes[2]);
                        MessageBox.Show(" V: " + valorVal);

                        //Almacenar Variables en alguna lista o algo
                        Variables vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {
                                vvv = new Variables(hijo, tipo, ambito, valorVal, nombreClase);
                                Clases.insertarVariableC(nombreClase, vvv);
                            }
                            else
                            {
                                vvv = new Variables(hijo, tipo, ambito, "", nombreClase);
                                Clases.insertarVariableC1(nombreClase, vvv);
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
                        if (var == true)
                        {
                            phrase = hijo.ToString();
                            words = phrase.Split(' ');
                            variables.Add(words[0]);
                        }
                        else if (import == true)
                        {
                            phrase = hijo.ToString();
                            words = phrase.Split(' ');
                            importanciones.Add(words[0]);
                        }
                    }
                    return "";
                #endregion

                #region FIN_DECLA
                case "FIN_DECLA":
                    if (root.ChildNodes.Count == 0)// ES SOLO UNA DECLARACION ";"
                        return ";";
                    else if (root.ChildNodes.Count == 1) //TIENE ASIGNACION
                        return ejecutar(root.ChildNodes[0]);
                    else if (root.ChildNodes.Count == 2) // Es objeto
                    {
                        phrase = root.ChildNodes[1].ToString();
                        words = phrase.Split(' ');
                        return words[0];
                    }
                    else
                        return "";
                #endregion

                //--
                #region EXPRESION
                case "EXPRESION":
                    //EXPRESION SIMBOLO EXPRESION
                    if (root.ChildNodes.Count == 3)
                    {
                        phrase = root.ChildNodes[1].ToString();
                        words = phrase.Split(' ');
                        if (words[0].Equals("||"))
                            return ComprobarLogico(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]), "||");
                        else if (words[0].Equals("&&"))
                            return ComprobarLogico(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]), "&&");
                        //----RELACIONALES
                        else if (words[0].Equals(">"))
                            return ComprobarMayor(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                        else if (words[0].Equals(">="))
                            return ComprobarMayorI(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                        else if (words[0].Equals("<"))
                            return ComprobarMenor(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                        else if (words[0].Equals("<="))
                            return ComprobarMenorI(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                        else if (words[0].Equals("=="))
                            return ComprobarIgual(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                        else if (words[0].Equals("!="))
                            return ComprobarDiferente(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                        //----ARITMETICOS
                        else if (words[0].Equals("+"))
                            return ComprobarSuma(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                        else if (words[0].Equals("-"))
                            return ComprobarResta(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                        else if (words[0].Equals("*"))
                            return ComprobarMulti(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                        else if (words[0].Equals("/"))
                            return ComprobarDivision(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                        else if (words[0].Equals("^"))
                            return ComprobarPotencia(ejecutar(root.ChildNodes[0]), ejecutar(root.ChildNodes[2]));
                    }
                    else if (root.ChildNodes.Count == 2)
                    {
                        //P DIMENSIONES
                        if (root.ChildNodes[0].ToString().Equals("P"))
                        {

                        }
                        // not EXPR
                        else
                            return ComprobarLogico(root.ChildNodes[1].ToString(), "00", "!");

                    }
                    else if (root.ChildNodes.Count == 1)
                    {
                        //EXPRESION
                        if (root.ChildNodes[0].ToString().Equals("EXPRESION"))
                        {
                            return ejecutar(root.ChildNodes[0]);
                        }
                        //P
                        else
                        {
                            return ejecutar(root.ChildNodes[0]);
                        }
                    }
                    return "";
                #endregion

                #region P
                case "P":
                    if (root.ChildNodes[0].ToString().Equals("BOOL"))
                    {
                        return ejecutar(root.ChildNodes[0]);
                    }
                    else if (root.ChildNodes[0].ToString().Equals("OBJETOS"))
                    {
                        return ejecutar(root.ChildNodes[0]);
                    }
                    else
                    {
                        phrase = root.ChildNodes[0].ToString();
                        words = phrase.Split(' ');
                        if (words[1].Contains("TkCadena"))
                            return "\"" + words[0] + "\"";
                        else if (words[1].Contains("TkCarac"))
                        {
                            return "'" + words[0] + "'";
                        }
                        else if (words[1].Contains("id"))
                            return "@@" + words[0];
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
                    
                #region DECLA_ARRE
                case "DECLA_ARRE":
                    //AMBITO + TIPO + array + L_ID + DIMENSIONES + FIN_ARRE
                    if(root.ChildNodes.Count == 6)
                    {
                        //Obtener Ambito
                        ambito = ejecutar(root.ChildNodes[0]);
                        //Obtener tipo
                        tipo = ejecutar(root.ChildNodes[1]);
                        //Obtener Ids
                        var = true;
                        variables.Clear();
                        ejecutar(root.ChildNodes[3]);
                        var = false;
                        //Dimensiones
                        dimensiones.Clear();
                        ejecutar(root.ChildNodes[4]);
                        //FIN ARRE
                        //Almacenar Variables en alguna lista o algo
                        Variables vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {
                                
                            }
                            else
                            {
                                
                            }
                        }
                        MessageBox.Show("DIMENSIONES: " + dimensiones.Count);
                        //FIN ARRE

                    }
                    //TIPO + array + L_ID + DIMENSIONES + FIN_ARRE;
                    else if(root.ChildNodes.Count == 5)
                    {

                    }
                    return "";
                #endregion

                #region DIMENSIONES
                case "DIMENSIONES":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        string h = ejecutar(root.ChildNodes[0]);
                        dimensiones.Add(h);
                    }
                    return "";
                #endregion

                #region VAL_DIM
                case "VAL_DIM":
                    return ejecutar(root.ChildNodes[0]);
                #endregion

                #region MAIN
                case "MAIN":
                    if (root.ChildNodes.Count == 1 )
                    {
                        ejecutarMain(root.ChildNodes[0]);
                    }
                    return "";
                #endregion
                default:
                    return "";



            }
        }

        //Recorrer el arbol
        public static string ejecutarMain(ParseTreeNode root)
        {
            switch (root.ToString())
            {
                //-------------------INSTRUCCIONES
                #region INS
                case "INS_CLA":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        ejecutarMain(hijo);
                    }
                    return "";
                #endregion

                //-------------------DECLARACIONES --FALTA
                #region DECLARACION
                case "DECLARACIONES":
                    ///TIPO + L_ID + FIN_DECLA
                    if (root.ChildNodes.Count == 3)
                    {
                        //Obtener Ambito
                        ambito = "publico";
                        //Obtener tipo
                        tipo = ejecutarMain(root.ChildNodes[0]);
                        //Obtener Ids
                        var = true;
                        variables.Clear();
                        ejecutarMain(root.ChildNodes[1]);
                        var = false;
                        //Obtener valor
                        valorVal = ejecutarMain(root.ChildNodes[2]);
                        MessageBox.Show(" V: " + valorVal);

                        //Almacenar Variables en alguna lista o algo
                        Variables vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {
                                vvv = new Variables(hijo, tipo, ambito, valorVal, nombreClase);
                                Clases.insertarVariableC(nombreClase, vvv);
                            }
                            else
                            {
                                vvv = new Variables(hijo, tipo, ambito, "", nombreClase);
                                Clases.insertarVariableC1(nombreClase, vvv);
                            }
                        }
                        //Limpiar variables
                        ambito = tipo = valorVal = "";
                    }
                    //AMBITO + TIPO + L_ID + FIN_DECLA
                    else if (root.ChildNodes.Count == 4)// TIENE TODO
                    {
                        //Obtener ambito 
                        ambito = ejecutarMain(root.ChildNodes[0]);
                        //Obtener tipo
                        tipo = ejecutarMain(root.ChildNodes[0]);
                        //Obtener Ids
                        var = true;
                        variables.Clear();
                        ejecutarMain(root.ChildNodes[1]);
                        var = false;
                        //Obtener valor
                        valorVal = ejecutarMain(root.ChildNodes[2]);
                        MessageBox.Show(" V: " + valorVal);

                        //Almacenar Variables en alguna lista o algo
                        Variables vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {
                                vvv = new Variables(hijo, tipo, ambito, valorVal, nombreClase);
                                Clases.insertarVariableC(nombreClase, vvv);
                            }
                            else
                            {
                                vvv = new Variables(hijo, tipo, ambito, "", nombreClase);
                                Clases.insertarVariableC1(nombreClase, vvv);
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
                        if (var == true)
                        {
                            phrase = hijo.ToString();
                            words = phrase.Split(' ');
                            variables.Add(words[0]);
                        }
                        else if (import == true)
                        {
                            phrase = hijo.ToString();
                            words = phrase.Split(' ');
                            importanciones.Add(words[0]);
                        }
                    }
                    return "";
                #endregion

                #region FIN_DECLA
                case "FIN_DECLA":
                    if (root.ChildNodes.Count == 0)// ES SOLO UNA DECLARACION ";"
                        return ";";
                    else if (root.ChildNodes.Count == 1) //TIENE ASIGNACION
                        return ejecutarMain(root.ChildNodes[0]);
                    else if (root.ChildNodes.Count == 2) // Es objeto
                    {
                        phrase = root.ChildNodes[1].ToString();
                        words = phrase.Split(' ');
                        return words[0];
                    }
                    else
                        return "";
                #endregion

                //--
                #region EXPRESION
                case "EXPRESION":
                    //EXPRESION SIMBOLO EXPRESION
                    if (root.ChildNodes.Count == 3)
                    {
                        phrase = root.ChildNodes[1].ToString();
                        words = phrase.Split(' ');
                        if (words[0].Equals("||"))
                            return ComprobarLogico(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]), "||");
                        else if (words[0].Equals("&&"))
                            return ComprobarLogico(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]), "&&");
                        //----RELACIONALES
                        else if (words[0].Equals(">"))
                            return ComprobarMayor(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                        else if (words[0].Equals(">="))
                            return ComprobarMayorI(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                        else if (words[0].Equals("<"))
                            return ComprobarMenor(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                        else if (words[0].Equals("<="))
                            return ComprobarMenorI(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                        else if (words[0].Equals("=="))
                            return ComprobarIgual(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                        else if (words[0].Equals("!="))
                            return ComprobarDiferente(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                        //----ARITMETICOS
                        else if (words[0].Equals("+"))
                            return ComprobarSuma(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                        else if (words[0].Equals("-"))
                            return ComprobarResta(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                        else if (words[0].Equals("*"))
                            return ComprobarMulti(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                        else if (words[0].Equals("/"))
                            return ComprobarDivision(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                        else if (words[0].Equals("^"))
                            return ComprobarPotencia(ejecutarMain(root.ChildNodes[0]), ejecutarMain(root.ChildNodes[2]));
                    }
                    else if (root.ChildNodes.Count == 2)
                    {
                        //P DIMENSIONES
                        if (root.ChildNodes[0].ToString().Equals("P"))
                        {

                        }
                        // not EXPR
                        else
                            return ComprobarLogico(root.ChildNodes[1].ToString(), "00", "!");

                    }
                    else if (root.ChildNodes.Count == 1)
                    {
                        //EXPRESION
                        if (root.ChildNodes[0].ToString().Equals("EXPRESION"))
                        {
                            return ejecutarMain(root.ChildNodes[0]);
                        }
                        //P
                        else
                        {
                            return ejecutarMain(root.ChildNodes[0]);
                        }
                    }
                    return "";
                #endregion

                #region P
                case "P":
                    if (root.ChildNodes[0].ToString().Equals("BOOL"))
                    {
                        return ejecutarMain(root.ChildNodes[0]);
                    }
                    else if (root.ChildNodes[0].ToString().Equals("OBJETOS"))
                    {
                        return ejecutarMain(root.ChildNodes[0]);
                    }
                    else
                    {
                        phrase = root.ChildNodes[0].ToString();
                        words = phrase.Split(' ');
                        if (words[1].Contains("TkCadena"))
                            return "\"" + words[0] + "\"";
                        else if (words[1].Contains("TkCarac"))
                        {
                            return "'" + words[0] + "'";
                        }
                        else if (words[1].Contains("id"))
                            return "@@" + words[0];
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
                    //VALOR DE LA VARIABLE
                    v = ejecutarMain(root.ChildNodes[1]);
                    //GUARDAR VALOR DE LA ASIGNACION
                    Clases.modificarValorVariable(nombreClase, n, v);
                    return "";
                #endregion

                #region DECLA_ARRE
                case "DECLA_ARRE":
                    //AMBITO + TIPO + array + L_ID + DIMENSIONES + FIN_ARRE
                    if (root.ChildNodes.Count == 6)
                    {
                        //Obtener Ambito
                        ambito = ejecutarMain(root.ChildNodes[0]);
                        //Obtener tipo
                        tipo = ejecutarMain(root.ChildNodes[1]);
                        //Obtener Ids
                        var = true;
                        variables.Clear();
                        ejecutarMain(root.ChildNodes[3]);
                        var = false;
                        //Dimensiones
                        dimensiones.Clear();
                        ejecutarMain(root.ChildNodes[4]);
                        //FIN ARRE
                        //Almacenar Variables en alguna lista o algo
                        Variables vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {

                            }
                            else
                            {

                            }
                        }
                        MessageBox.Show("DIMENSIONES: " + dimensiones.Count);
                        //FIN ARRE

                    }
                    //TIPO + array + L_ID + DIMENSIONES + FIN_ARRE;
                    else if (root.ChildNodes.Count == 5)
                    {

                    }
                    return "";
                #endregion

                #region DIMENSIONES
                case "DIMENSIONES":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        string h = ejecutarMain(root.ChildNodes[0]);
                        dimensiones.Add(h);
                    }
                    return "";
                #endregion

                #region VAL_DIM
                case "VAL_DIM":
                    return ejecutarMain(root.ChildNodes[0]);
                #endregion

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
            //VERIFICAR PRIMERO SI ES ID, PARA LUEGO PODER ANALIZAR SU CONTENIDO
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
            //Arreglo

            //--------VALORES NO VALIDOS
            //Double
            if (Double.TryParse(val1, out valorNumeri) ||
                Double.TryParse(val2, out valorNumeri))
                return "????";
            //num
            else if (int.TryParse(val1, out valorNumerico) ||
                int.TryParse(val2, out valorNumerico1))
                return "????";
            //caracter
            else if (val1.StartsWith("'") || val2.StartsWith("'"))
                return "????";
            //cadena                
            else if (val1.StartsWith("\"") || val2.StartsWith("\""))
                return "????";
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
                    return "false";
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
                resultado = "????";
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
                resultado = "????";
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
                resultado = "????";
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
                resultado = "????";
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
                resultado = "????";
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
                resultado = "????";
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

    }
}
