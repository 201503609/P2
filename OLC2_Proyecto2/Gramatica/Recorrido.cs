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
        #region DECLARACIONES
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

        //----PARA LA FUNCION PRINT
        public static string impresiones = "";

        //----PARA EL IF
        public static Boolean flagIf = false;

        //----PARA EL COMPROBAR
        public static Boolean flagCom = false;
        public static string comparar = "";

        //----PARA LAS FIGURAS
        public static ArrayList figuras = new ArrayList();
        public static Boolean flagFig = false;

        //---- PARA SALIR, CONTINUAR
        public static Boolean salirFlag = false;
        public static Boolean continuaFlag = false;
        //---- PARA RETURN
        public static Boolean retornoFlag = false, puedeRetornar = false;
        public static string valorRetorno = "";

        //------ ALGO EXTRA 
        public static string phrase;
        public static string[] words;

        #endregion

        public static int contF = 0;
        public static ArrayList nodosImportantes = new ArrayList();
        public static ArrayList funcionesClase = new ArrayList();
        public static ArrayList parametrosEnviar = new ArrayList();
        public static ArrayList parametrosRecibidos = new ArrayList();

        //--- Ver lo que se debe ejecutar desde el inico
        public static string primeraLectura(ParseTreeNode root, Ambito anterior)
        {
            switch (root.ToString())
            {
                case "INICIO":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        primeraLectura(hijo, anterior);
                    }
                    return "";

                // --- DECLARACION DE VARIABLES
                #region DECLARACIONES
                case "DECLARACIONES":
                    //Ambito nuevaDecla = new Ambito(anterior);
                    Nodo n = new Nodo(root, anterior);
                    nodosImportantes.Add(n);
                    //MessageBox.Show("VIENE UNA DECLARACION DE VARIABLE");
                    return "";
                #endregion

                #region NO SIRVE pt1
                case "AMBITO":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "TIPO":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "L_ID":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "FIN_DECLA":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "EXPRESION":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "P":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "BOOL":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "ASIGNA":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "INCREMENTO":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "DECREMENTO":
                    //--- NO ME SIRVE AHORITA
                    return "";
                #endregion

                // --- DECLARACION DE ARREGLOS
                #region DECLA_ARRE
                case "DECLA_ARRE":
                    //Ambito nuevoArre = new Ambito(anterior);
                    Nodo n1 = new Nodo(root, anterior);
                    nodosImportantes.Add(n1);
                    //MessageBox.Show("VIENE UNA DECLARACION DE ARREGLO");
                    return "";
                #endregion

                #region NO SIRVE pt2
                case "DIMENSIONES":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "VAL_DIM":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "FIN_ARRE":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "VAL_AA":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "VAL_AA1":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "VAA":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "USO_ARRE":
                    //--- NO ME SIRVE AHORITA
                    return "";
                #endregion

                #region CLASE
                case "CLASE":
                    //clase + id + IMPORTACIONES + TllaA + INS_CLA + TllaC
                    if (root.ChildNodes.Count == 3)//tiene importanciones e instrucciones
                    {
                        primeraLectura(root.ChildNodes[2], anterior);
                    }
                    else if (root.ChildNodes.Count == 2) // pueden ser dos producciones
                    {
                        //clase + id + TllaA + INS_CLA + TllaC
                        if (root.ChildNodes[1].ToString().Equals("INS_CLA"))
                        { //no tiene importanciones pero si instrucciones
                            primeraLectura(root.ChildNodes[1], anterior);
                        }
                        //clase + id + IMPORTACIONES + TllaA + TllaC
                        else    //tiene importacion pero no instruccion 
                        {
                        }
                    }
                    //clase + id + TllaA + TllaC --- La clase esta vacia
                    else if (root.ChildNodes.Count == 1)
                    {

                    }
                    return "";
                #endregion

                #region INS
                case "INS_CLA":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        primeraLectura(hijo, anterior);
                    }
                    return "";
                #endregion

                #region NO SIRVE pt4
                case "IMPORTACIONES":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "OBJETOS":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "L_ID1":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "PARAMETROS":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "L_PARAM":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "AS_OBJ":
                    //--- NO ME SIRVE AHORITA
                    return "";
                #endregion

                // --- FUNCION SIN RETORNO
                #region FUNCION_SR
                case "FUNCION_SR":
                    //id + voir + OVER + PARAMETROS1 + TllaA + TllaC                        3
                    if (root.ChildNodes.Count == 3)
                    {
                        string phrase = root.ChildNodes[0].ToString();
                        string[] words = phrase.Split(' ');
                        NodoFuncion nf = new NodoFuncion(words[0], root, anterior);
                        funcionesClase.Add(nf);

                    }
                    else if (root.ChildNodes.Count == 4)
                    {
                        //AMBITO + id + voir + OVER + PARAMETROS1 + TllaA + TllaC               4
                        if (root.ChildNodes[0].ToString().Equals("AMBITO"))
                        {
                            string phrase = root.ChildNodes[1].ToString();
                            string[] words = phrase.Split(' ');
                            NodoFuncion nf = new NodoFuncion(words[0], root, anterior);
                            funcionesClase.Add(nf);
                        }
                        //id + voir + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC              4
                        else
                        {
                            string phrase = root.ChildNodes[0].ToString();
                            string[] words = phrase.Split(' ');
                            NodoFuncion nf = new NodoFuncion(words[0], root, anterior);
                            funcionesClase.Add(nf);
                        }

                    }
                    //AMBITO + id + voir + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC     5
                    else if (root.ChildNodes.Count == 5)
                    {
                        string phrase = root.ChildNodes[1].ToString();
                        string[] words = phrase.Split(' ');
                        NodoFuncion nf = new NodoFuncion(words[0], root, anterior);
                        funcionesClase.Add(nf);
                    }

                    return "";
                #endregion

                // --- FUNCION CON RETORNO
                #region FUNCION_CR
                case "FUNCION_CR":
                    if (root.ChildNodes.Count == 6)
                    {
                        string phrase = root.ChildNodes[1].ToString();
                        string[] words = phrase.Split(' ');
                        NodoFuncion nf = new NodoFuncion(words[0], root, anterior);
                        funcionesClase.Add(nf);
                    }
                    return "";
                #endregion

                // --- MAIN
                #region MAIN
                case "MAIN":
                    //Ambito metodoMain = new Ambito(anterior);
                    Nodo n2 = new Nodo(root, anterior);
                    nodosImportantes.Add(n2);
                    //MessageBox.Show("VIENE EL MAIN");
                    return "";
                #endregion

                #region NO SIRVE pt3
                case "OVER":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "PARAMETROS1":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "L_PARAM1":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "PARAM1":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "RETORNO":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "IMPRIMIR":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "SHOW":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "SI":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "EXTRA_SI":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "L_SINO":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "SINO":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "FOR":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "V_FOR":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "ACTUALIZACION":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "REPETIR":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "MIENTRAS":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "COMPROBAR":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "L_CASO":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "CASO":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "SALIR":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "HACER":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "CONTINUAR":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "FIGURE":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "FIGURAS":
                    //--- NO ME SIRVE AHORITA
                    return "";
                case "ADD_FIGURE":
                    //--- NO ME SIRVE AHORITA
                    return "";
                #endregion

                default:
                    return "";
            }
        }


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
                        TablaSimbolos.pasarTabla(nuevo);
                        ReporteTS.reporteTablaSimbolos(nuevo, nombreClase);
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
                            TablaSimbolos.pasarTabla(nuevo);
                            ReporteTS.reporteTablaSimbolos(nuevo, nombreClase);
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
                    else if (root.ChildNodes.Count == 1)
                    {
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
                        if (salirFlag == false && continuaFlag == false && retornoFlag == false)
                        {
                            ejecutar(hijo, anterior);
                        }
                        else if (salirFlag == true || continuaFlag == true || retornoFlag == true)
                            return "";
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
                        //MessageBox.Show(" Valor: " + valorVal);

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
                        //MessageBox.Show(" Valor: " + valorVal);

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
                            return ComprobarLogico(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), "||", anterior);
                        else if (words[0].Equals("&&"))
                            return ComprobarLogico(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), "&&", anterior);
                        //----RELACIONALES
                        else if (words[0].Equals(">"))
                            return ComprobarMayor(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                        else if (words[0].Equals(">="))
                            return ComprobarMayorI(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                        else if (words[0].Equals("<"))
                            return ComprobarMenor(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                        else if (words[0].Equals("<="))
                            return ComprobarMenorI(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                        else if (words[0].Equals("=="))
                            return ComprobarIgual(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                        else if (words[0].Equals("!="))
                            return ComprobarDiferente(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                        //----ARITMETICOS
                        else if (words[0].Equals("+"))
                            return ComprobarSuma(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                        else if (words[0].Equals("-"))
                            return ComprobarResta(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                        else if (words[0].Equals("*"))
                            return ComprobarMulti(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                        else if (words[0].Equals("/"))
                            return ComprobarDivision(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                        else if (words[0].Equals("^"))
                            return ComprobarPotencia(ejecutar(root.ChildNodes[0], anterior), ejecutar(root.ChildNodes[2], anterior), anterior);
                    }
                    else if (root.ChildNodes.Count == 2)
                    {
                        //P DIMENSIONES
                        if (root.ChildNodes[0].ToString().Equals("P"))
                        {
                            //----- ESTO ES UN ARREGLO
                            string nombreArr = ejecutar(root.ChildNodes[0], anterior);
                            if (nombreArr.Contains("@@"))
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
                            if (esAlgo.Contains("??"))
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
                            return ComprobarLogico(ejecutar(root.ChildNodes[1], anterior), "00", "!", anterior);

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
                        int i = 0;
                        foreach (string hijo in words)
                        {
                            i++;
                        }

                        if (words[i - 1].Equals("(TkCadena)"))
                        {
                            string tkC = "";
                            for (int j = 0; j < i - 1; j++)
                            {
                                tkC += words[j] + " ";
                            }
                            return "\"" + tkC + "\"";
                        }
                        else if (words[1].Equals("(TkCarac)"))
                            return "'" + words[0] + "'";
                        else if (words[1].Equals("(num)"))
                            return words[0];
                        else if (words[1].Contains("(id)"))
                            return "@@" + words[0].ToLower();
                        else
                            return words[0].ToLower();
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
                    //P + decremento + PuntoComa;
                    string decre = ejecutar(root.ChildNodes[0], anterior);
                    return realizarDecremento(anterior, decre);
                #endregion

                //---------------------------- ARREGLO
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
                        //MessageBox.Show("ValArr: " + valorVal);

                        //Almacenar los arreglos en alguna lista o algo
                        Arreglos vvv;
                        foreach (String hijo in variables)
                        {
                            if (!valorVal.Equals(";"))
                            {
                                obtenerDimensionesArreglo(anterior);                        // ---OBTENER LAS DIMENSIONES DEL ARREGLO
                                ArrayList nue = obtenerValoresArreglo(anterior, valorVal);   // ---OBTENER LOS VALORES DEL ARREGLO
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
                        //MessageBox.Show("ValArr: " + valorVal);

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
                    return ejecutar(root.ChildNodes[0], anterior);
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
                    //MessageBox.Show(nomArre + " ValArr: " + valorVal);

                    //CAMBIAR VALOR
                    obtenerDimensionesArreglo(anterior);
                    ArrayList emergencia = new ArrayList();
                    emergencia.Add(valorVal);
                    Arreglos nuevoA = new Arreglos(nomArre, "", "", "", dimensiones.Count, dim1, dim2, dim3, emergencia, emergencia.Count);
                    if (1 + 1 == 2)
                    {
                        Boolean fInserto = Ambito.asignarValorArreglo(nuevoA, anterior, valorVal);
                    }
                    valorVal = dim1 = dim2 = dim3 = "";
                    return "";
                #endregion
                //---------------------------- FIN ARREGLO


                //---------------------------- OBJETOS
                #region OBJETOS
                case "OBJETOS":
                    //L_ID1 + PARAMETROS
                    if (root.ChildNodes.Count == 2)
                    {
                        string met = ejecutar(root.ChildNodes[0], anterior);
                        string vals = ejecutar(root.ChildNodes[1], anterior);

                        if (!vals.Equals(""))
                        {
                            string[] words = vals.Split(',');
                            parametrosEnviar.Clear();
                            foreach (string hijo in words)
                            {
                                parametrosEnviar.Add(hijo);
                            }
                        }

                        return met + ";" + vals;

                    }
                    //L_ID1
                    else
                        return ejecutar(root.ChildNodes[0], anterior);
                #endregion

                #region L_ID1
                case "L_ID1":
                    string jo = "";
                    int jos = 0;
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        phrase = hijo.ToString();
                        words = phrase.Split(' ');

                        if (jos != 0)
                            jo += ".";
                        jo += words[0];
                        jos++;
                    }
                    return jo;
                #endregion

                #region PARAMETROS
                case "PARAMETROS":
                    //TparA + TparC
                    if (root.ChildNodes.Count == 0)
                    {
                        return "";
                    }
                    //TparA + L_PARAM + TparC
                    else
                    {
                        flagFig = true;
                        string no = ejecutar(root.ChildNodes[0], anterior);
                        flagFig = false;
                        return no;
                    }
                #endregion

                #region L_PARAM
                case "L_PARAM":
                    string di3 = "";
                    int die3 = 0;
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        if (flagFig == true)
                        {
                            if (die3 != 0)
                                di3 += ",";
                            di3 += ejecutar(hijo, anterior);
                            die3++;
                        }
                    }
                    return di3;
                #endregion


                #region AS_OBJ
                case "AS_OBJ":
                    //OBJETOS + PuntoComa
                    if (root.ChildNodes.Count == 1)
                    {
                        //------- ESTO ES UNA LLAMADA A UNA FUNCION

                        string obj = ejecutar(root.ChildNodes[0], anterior);

                        //BUSCAR LA FUNCION Y EJECUTARLA
                        string[] words = obj.Split(';');
                        //Funciones f1 = Ambito.buscarFuncion(words[0], anterior);

                        foreach (NodoFuncion hijo in funcionesClase)
                        {
                            if (hijo.nombreF.ToLower().Equals(words[0].ToLower()))
                            {
                                Ambito nuevo = new Ambito(anterior);
                                nuevo.nombreA = "FUNCION";
                                return ejecutar(hijo.root, nuevo);
                            }
                        }
                    }
                    else
                    {

                    }
                    return "";
                #endregion
                //---------------------------- FIN OBJETOS 

                //------
                #region MAIN
                case "MAIN":
                    if (root.ChildNodes.Count == 0)
                    {
                        return "";
                    }
                    else
                    {
                        Ambito nuevo = new Ambito(anterior);
                        nuevo.salida = false;
                        nuevo.continuar = false;
                        nuevo.retorno = false;
                        nuevo.nombreA = "Main";

                        ejecutar(root.ChildNodes[0], nuevo);

                        TablaSimbolos.pasarTabla(nuevo);
                        TablaSimbolos.pasarTabla(anterior);
                        ReporteTS.reporteTablaSimbolos(nuevo, "Main");
                        return "";
                    }
                #endregion

                //----------------------------FUNCIONES
                #region FUNCION_SR
                case "FUNCION_SR":
                    //id + voir + OVER + PARAMETROS1 + TllaA + TllaC            3   sin instrucciones
                    //AMBITO + id + voir + OVER + PARAMETROS1 + TllaA + TllaC       4   sin instrucciones
                    //id + voir + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC;         4 

                    //AMBITO + id + voir + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC 5
                    if (root.ChildNodes.Count == 5)
                    {
                        // -- Obtener el ambito de la funcion
                        ambito = ejecutar(root.ChildNodes[0], anterior);

                        // -- Nombre de la funcion
                        phrase = root.ChildNodes[1].ToString();
                        words = phrase.Split(' ');
                        string nombreFunc = words[0];

                        // -- Over
                        string ov = ejecutar(root.ChildNodes[2], anterior);

                        // -- Desde aca puedo empezar a probar a enviar el nuevo ambito
                        string pp = "";

                        // -- VER LO DE LOS PARAMETROS
                        parametrosRecibidos.Clear();
                        ejecutar(root.ChildNodes[3], anterior);




                        // -- Hacer algo con los parametros
                        if ((parametrosRecibidos.Count == parametrosEnviar.Count) || (parametrosEnviar.Count == 0 && parametrosRecibidos.Count == 0))
                        {
                            Variables vvv;
                            for (int a = 0; a < parametrosEnviar.Count; a++)
                            {
                                Variables aux = (Variables)parametrosRecibidos[a];
                                vvv = new Variables(aux.nombre, aux.tipo, "", parametrosEnviar[a].ToString(), "");
                                pp += limpiarVariables2(vvv.valor, anterior) + ",";

                                Boolean fInserto = Ambito.insertarVarConValor(vvv, anterior);              //INSERTAR VARIABLE EN EL AMBITO
                                //MessageBox.Show("Inserto en la funcion: " + fInserto);
                            }
                            MessageBox.Show("Funcion a ejecutar: " + nombreFunc + " \n valores: " + pp);
                            ejecutar(root.ChildNodes[4], anterior);
                        }
                        else
                        {
                            ErroresSem es = new ErroresSem("Los parametros que se reciben no coinciden con los que se envian", 0, 0);
                            es.insertarErrSem(es);
                        }
                    }

                    return "";
                #endregion


                #region OVER
                case "OVER":
                    if (root.ChildNodes.Count == 0)
                        return "no";
                    else
                        return "si";
                #endregion

                #region PARAMETROS1
                case "PARAMETROS1":
                    //TparA + TparC
                    if (root.ChildNodes.Count == 0)
                    {
                    }
                    //TparA + L_PARAM + TparC
                    else if (root.ChildNodes.Count == 1)
                    {
                        ejecutar(root.ChildNodes[0], anterior);
                    }
                    return "";
                #endregion

                #region L_PARAM1
                case "L_PARAM1":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        ejecutar(hijo, anterior);
                    }
                    return "";
                #endregion

                #region PARAM1
                case "PARAM1":
                    if (root.ChildNodes.Count == 2)
                    {
                        //TIPO + id
                        // -- Obtener el ambito de la funcion
                        tipo = ejecutar(root.ChildNodes[0], anterior);
                        // -- Nombre de la funcion
                        phrase = root.ChildNodes[1].ToString();
                        words = phrase.Split(' ');
                        string nombreFunc = words[0];
                        Variables nat = new Variables(nombreFunc, tipo, "", "", "");
                        parametrosRecibidos.Add(nat);

                    }
                    return "";
                #endregion

                #region RETORNO
                case "RETORNO":
                    //retorno + PuntoComa
                    //retorno + EXPRESION + PuntoComa
                    if (root.ChildNodes.Count == 1)
                    {
                        MessageBox.Show("Entra");
                        if (anterior.retorno == true)
                        {
                            retornoFlag = true;
                            valorRetorno = limpiarVariables(ejecutar(root.ChildNodes[0], anterior), anterior);
                            MessageBox.Show("h" + valorRetorno);
                            return valorRetorno;
                        }
                        else
                        {
                            ErroresSem es = new ErroresSem("La sentencia RETORNO no puede venir en el ambito colocado", 0, 0);
                            es.insertarErrSem(es);
                        }
                    }
                    return "";
                #endregion

                #region FUNCION_CR
                case "FUNCION_CR":
                    //AMBITO + id + TIPO + OVER + PARAMETROS1 + TllaA + TllaC                                           5
                    //AMBITO + id + array + TIPO + DIMENSIONES + OVER + PARAMETROS1 + TllaA + TllaC                     6
                    //AMBITO + id + TIPO + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC                                 6
                    //AMBITO + id + array + TIPO + DIMENSIONES + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC           7

                    //AMBITO + id + TIPO + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC
                    if (root.ChildNodes.Count == 6)
                    {
                        if (root.ChildNodes[2].ToString().Equals("TIPO"))
                        {
                            // -- Obtener el ambito de la funcion
                            ambito = ejecutar(root.ChildNodes[0], anterior);

                            // -- Nombre de la funcion
                            phrase = root.ChildNodes[1].ToString();
                            words = phrase.Split(' ');
                            string nombreFunc = words[0];

                            // -- Tipo
                            tipo = ejecutar(root.ChildNodes[2], anterior);

                            // -- Over
                            string ov = ejecutar(root.ChildNodes[3], anterior);

                            // -- Desde aca puedo empezar a probar a enviar el nuevo ambito
                            string pp = "";

                            // -- VER LO DE LOS PARAMETROS
                            parametrosRecibidos.Clear();
                            ejecutar(root.ChildNodes[4], anterior);
                            anterior.retorno = true;
                            retornoFlag = false;

                            // -- Hacer algo con los parametros
                            //MessageBox.Show(parametrosRecibidos.Count + "," + parametrosEnviar.Count);
                            if ((parametrosRecibidos.Count == parametrosEnviar.Count) || (parametrosEnviar.Count == 0 && parametrosRecibidos.Count == 0))
                            {
                                Variables vvv;
                                for (int a = 0; a < parametrosEnviar.Count; a++)
                                {
                                    Variables aux = (Variables)parametrosRecibidos[a];
                                    vvv = new Variables(aux.nombre, aux.tipo, "", parametrosEnviar[a].ToString(), "");
                                    pp += limpiarVariables2(vvv.valor, anterior) + ",";

                                    Boolean fInserto = Ambito.insertarVarConValor(vvv, anterior);              //INSERTAR VARIABLE EN EL AMBITO
                                    if (fInserto == false)
                                    {
                                        Boolean cambio = Ambito.CambiarValorVariable(anterior, vvv);
                                    }
                                }
                                MessageBox.Show("Funcion a ejecutar: " + nombreFunc + " \n valores: " + pp);
                                string rr = ejecutar(root.ChildNodes[5], anterior);
                                MessageBox.Show("Retorna " + rr);
                                return rr;
                            }
                            else
                            {
                                ErroresSem es = new ErroresSem("Los parametros que se reciben no coinciden con los que se envian", 0, 0);
                                es.insertarErrSem(es);
                            }

                            retornoFlag = false;
                            anterior.retorno = false;
                        }

                    }
                    return "";

                #endregion
                //---------------------------- FUNCIONES NATIVAS   --DE aca para abajo, todo ok en teoria
                #region IMPRIMIR
                case "IMPRIMIR":
                    //imprimir + TparA + EXPRESION + TparC + PuntoComa;
                    impresiones += ">> " + limpiarVariables(ejecutar(root.ChildNodes[0], anterior), anterior) + "\n";
                    return "";
                #endregion

                #region SHOW
                case "SHOW":
                    //show + TparA + EXPRESION + Coma + EXPRESION + TparC + PuntoComa;
                    string tituloS = limpiarVariables(ejecutar(root.ChildNodes[0], anterior), anterior);
                    string mensajeS = limpiarVariables(ejecutar(root.ChildNodes[1], anterior), anterior);
                    MessageBox.Show(mensajeS, tituloS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return "";
                #endregion

                //---------------------------- SENTENCIA SI 
                #region SI
                case "SI":
                    //si + TparA + EXPRESION + TparC + TllaA + TllaC + EXTRA_SI                     2   EL IF NO TIENE NADA
                    if (root.ChildNodes.Count == 2)
                    {
                        string condicion = ejecutar(root.ChildNodes[0], anterior);
                        Ambito nuevo = new Ambito(anterior);
                        salirFlag = false;
                        nuevo.salida = false;

                        //--Si la condicion es verdadero, se ejecuta el codigo del if
                        if (condicion.Equals("verdadero"))
                        { }
                        //--Si la condicion es falso, se ejecuta el codigo de EXTRA_SI
                        else
                        {
                            string condicion1 = ejecutar(root.ChildNodes[1], anterior);
                        }
                    }
                    //---------------
                    else if (root.ChildNodes.Count == 3)
                    {
                        //si + TparA + EXPRESION + TparC + TllaA + INS_CLA + TllaC + EXTRA_SI           3
                        if (root.ChildNodes[1].ToString().Equals("INS_CLA"))
                        {
                            string condicion = ejecutar(root.ChildNodes[0], anterior);
                            Ambito nuevo = new Ambito(anterior);
                            salirFlag = false;
                            if (anterior.salida == true)
                                nuevo.salida = true;
                            else
                                nuevo.salida = false;
                            if (anterior.continuar == true)
                                nuevo.continuar = true;
                            else
                                nuevo.continuar = false;

                            //--Si la condicion es verdadero, se ejecuta el codigo del if
                            if (condicion.Equals("verdadero"))
                            {
                                continuaFlag = false;
                                ejecutar(root.ChildNodes[1], nuevo);

                                if (anterior.salida == false)
                                    salirFlag = false;
                                if (anterior.continuar == false)
                                    continuaFlag = false;
                                nuevo.continuar = false;
                                nuevo.salida = false;

                            }
                            //--Si la condicion es falso, se ejecuta el codigo del EXTRA_SI
                            else
                            {
                                ejecutar(root.ChildNodes[2], anterior);
                            }
                        }
                        //si + TparA + EXPRESION + TparC + TllaA + TllaC + L_SINO + EXTRA_SI
                        else
                        {
                            string condicion = ejecutar(root.ChildNodes[0], anterior);
                            Ambito nuevo = new Ambito(anterior);
                            salirFlag = false;
                            nuevo.salida = false;

                            //--Si la condicion es verdadero, se ejecuta el codigo del if
                            if (condicion.Equals("verdadero"))
                            { }
                            //--Si la condicion es falso, se ejecuta el codigo de L_SINO
                            else
                            {
                                flagIf = false;
                                string condicion1 = ejecutar(root.ChildNodes[1], anterior);
                                //--Si la condicion1 es verdadero, ahi queda
                                if (condicion1.Equals("verdadero"))
                                { }
                                //--Si la condicion1 es falso, se ejecuta el codigo de EXTRA_SI
                                else
                                {
                                    ejecutar(root.ChildNodes[2], anterior);
                                }
                            }
                        }
                    }
                    //si + TparA + EXPRESION + TparC + TllaA + INS_CLA + TllaC + L_SINO + EXTRA_SI; 4
                    else if (root.ChildNodes.Count == 4)
                    {
                        string condicion = ejecutar(root.ChildNodes[0], anterior);         //Valor
                        Ambito nuevo = new Ambito(anterior);
                        salirFlag = false;
                        if (anterior.salida == true)
                            nuevo.salida = true;
                        else
                            nuevo.salida = false;
                        if (anterior.continuar == true)
                            nuevo.continuar = true;
                        else
                            nuevo.continuar = false;

                        //--Si la condicion es verdadero, se ejecuta el codigo del if
                        if (condicion.Equals("verdadero"))
                        {
                            continuaFlag = false;
                            ejecutar(root.ChildNodes[1], nuevo);

                            if (anterior.salida == false)
                                salirFlag = false;
                            if (anterior.continuar == false)
                                continuaFlag = false;
                            nuevo.continuar = false;
                            nuevo.salida = false;
                        }
                        //--Si la condicion es falso, se ejecuta el codigo del L_SINO
                        else
                        {
                            flagIf = false;
                            string condicion1 = ejecutar(root.ChildNodes[2], anterior);
                            //--Si la condicion1 es verdadero, ahi queda
                            if (condicion1.Equals("verdadero"))
                            { }
                            //--Si la condicion1 es falso, se ejecuta el codigo de EXTRA_SI
                            else
                            {
                                ejecutar(root.ChildNodes[3], anterior);
                            }
                        }
                    }

                    return "";
                #endregion

                #region L_SINO
                case "L_SINO":
                    string re = "falso";
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        if (flagIf == false)
                        {
                            re = ejecutar(hijo, anterior);
                        }
                    }
                    return re;
                #endregion

                #region SINO
                case "SINO":
                    //sino + si + TparA + EXPRESION + TparC + TllaA + TllaC     NO TIENE INSTRUCCIONES
                    if (root.ChildNodes.Count == 1)
                    {
                        string condicion = ejecutar(root.ChildNodes[0], anterior);
                        //--Si la condicion es verdadero
                        if (condicion.Equals("verdadero"))
                        {
                            flagIf = true;
                            return "verdadero";
                        }
                        //--Si la condicion es falso
                        else
                            return "falso";
                    }
                    //sino + si + TparA + EXPRESION + TparC + TllaA + INS_CLA + TllaC;
                    else
                    {
                        {
                            string condicion = ejecutar(root.ChildNodes[0], anterior);
                            Ambito nuevo = new Ambito(anterior);
                            salirFlag = false;
                            if (anterior.salida == true)
                                nuevo.salida = true;
                            else
                                nuevo.salida = false;
                            if (anterior.continuar == true)
                                nuevo.continuar = true;
                            else
                                nuevo.continuar = false;

                            //--Si la condicion es verdadero, se ejecuta el codigo
                            if (condicion.Equals("verdadero"))
                            {
                                flagIf = true;
                                continuaFlag = false;
                                ejecutar(root.ChildNodes[1], nuevo);

                                if (anterior.salida == false)
                                    salirFlag = false;
                                if (anterior.continuar == false)
                                    continuaFlag = false;
                                nuevo.continuar = false;
                                nuevo.salida = false;

                                return "verdadero";
                            }
                            //--Si la condicion es falso
                            else
                                return "falso";
                        }
                    }
                #endregion

                #region EXTRA_SI
                case "EXTRA_SI":
                    //sino + TllaA + TllaC              0
                    //Empty                             0

                    //sino + TllaA + INS_CLA + TllaC    1
                    if (root.ChildNodes.Count == 1)
                    {
                        Ambito nuevo = new Ambito(anterior);
                        salirFlag = false;
                        if (anterior.salida == true)
                            nuevo.salida = true;
                        else
                            nuevo.salida = false;
                        if (anterior.continuar == true)
                            nuevo.continuar = true;
                        else
                            nuevo.continuar = false;

                        // --- EJECUTAR EL CODIGO
                        continuaFlag = false;
                        ejecutar(root.ChildNodes[0], nuevo);

                        if (anterior.salida == false)
                            salirFlag = false;
                        if (anterior.continuar == false)
                            continuaFlag = false;
                        nuevo.continuar = false;
                        nuevo.salida = false;

                    }
                    return "";
                #endregion

                //---------------------------- SENTENCIA FOR 
                #region FOR
                case "FOR":
                    //para + TparA + V_FOR + EXPRESION + PuntoComa + ACTUALIZACION + TparC + TllaA + TllaC                  3
                    if (root.ChildNodes.Count == 3)
                    {
                        Ambito nuevo = new Ambito(anterior);
                        ejecutar(root.ChildNodes[0], nuevo);         //V_FOR
                        while ((ejecutar(root.ChildNodes[1], nuevo)).Equals("verdadero"))    //EXPRESION
                        {
                            ejecutar(root.ChildNodes[2], nuevo);    //ACTUALIZANCION
                            //--- NO TIENE INSTRUCCIONES --//
                        }
                    }
                    //para + TparA + V_FOR + EXPRESION + PuntoComa + ACTUALIZACION + TparC + TllaA + INS_CLA + TllaC;       4
                    if (root.ChildNodes.Count == 4)
                    {
                        Ambito nuevo = new Ambito(anterior);
                        salirFlag = false;
                        nuevo.salida = true;
                        continuaFlag = false;
                        nuevo.continuar = true;

                        ejecutar(root.ChildNodes[0], nuevo);         //V_FOR
                        while ((ejecutar(root.ChildNodes[1], nuevo)).Equals("verdadero"))    //EXPRESION
                        {
                            if (salirFlag == true && anterior.salida == false)
                            {
                                salirFlag = false;
                                break;
                            }
                            else if (salirFlag == true)
                                break;

                            // --- EJECUTAR LAS INTRUCCIONES
                            continuaFlag = false;
                            ejecutar(root.ChildNodes[3], nuevo);

                            ejecutar(root.ChildNodes[2], nuevo);    //ACTUALIZANCION
                        }
                        nuevo.salida = false;
                        nuevo.continuar = false;
                        if (anterior.continuar == false)
                        {
                            continuaFlag = false;
                        }
                    }
                    return "";
                #endregion

                #region V_FOR
                case "V_FOR":
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        ejecutar(hijo, anterior);
                    }
                    return "";
                #endregion

                #region ACTUALIZACION
                case "ACTUALIZACION":
                    phrase = root.ChildNodes[1].ToString();
                    words = phrase.Split(' ');

                    if (words[0].Equals("++"))
                    {
                        //P + incremento;
                        string increm = ejecutar(root.ChildNodes[0], anterior);
                        return realizarIncremento(anterior, increm);
                    }
                    else
                    {
                        //P + decremento + PuntoComa;
                        string decrem = ejecutar(root.ChildNodes[0], anterior);
                        return realizarDecremento(anterior, decrem);
                    }
                #endregion

                //---------------------------- SENTENCIA REPETIR 
                #region REPETIR
                case "REPETIR":
                    //repetir + TparA + EXPRESION + TparC + TllaA + TllaC               1
                    if (root.ChildNodes.Count == 1)
                    {
                        int valorNumerico = 0, veces = 0;
                        string valor = ejecutar(root.ChildNodes[0], anterior);           //OBTENER LAS VECES QUE SE REPITE
                        //----VER SI ES UN NUMERO
                        if (int.TryParse(valor, out valorNumerico))
                        {
                            veces = Convert.ToInt32(valor);
                        }
                        else if (valor.Contains("@@"))
                        {
                            veces = Convert.ToInt32(limpiarVariables(valor, anterior));
                        }
                        //-----EJECUTAR EL REPETIR
                        Ambito nuevo = new Ambito(anterior);
                        for (int zz = 0; zz < veces; zz++)
                        { /*--- NO TIENE INSTRUCCIONES ---*/ }
                    }
                    //repetir + TparA + EXPRESION + TparC + TllaA + INS_CLA + TllaC     2
                    else if (root.ChildNodes.Count == 2)
                    {
                        int valorNumerico = 0, veces = 0;
                        string valor = ejecutar(root.ChildNodes[0], anterior);           //OBTENER LAS VECES QUE SE REPITE
                        //----VER SI ES UN NUMERO
                        if (int.TryParse(valor, out valorNumerico))
                        {
                            veces = Convert.ToInt32(valor);
                        }
                        else if (valor.Contains("@@"))
                        {
                            veces = Convert.ToInt32(limpiarVariables(valor, anterior));
                        }
                        //-----EJECUTAR EL REPETIR
                        Ambito nuevo = new Ambito(anterior);
                        salirFlag = false;
                        nuevo.salida = true;
                        continuaFlag = false;
                        nuevo.continuar = true;

                        for (int zz = 0; zz < veces; zz++)
                        {
                            //---- EJECUTAR LAS INSTRUCCIONES
                            continuaFlag = false;
                            ejecutar(root.ChildNodes[1], nuevo);

                            if (salirFlag == true && anterior.salida == false)
                            {
                                salirFlag = false;
                                break;
                            }
                            if (salirFlag == true)
                                break;
                        }
                        nuevo.salida = false;
                        nuevo.continuar = false;
                        if (anterior.continuar == false)
                        {
                            continuaFlag = false;
                        }
                    }
                    return "";
                #endregion

                //---------------------------- SENTENCIA WHILE 
                #region WHILE
                case "MIENTRAS":
                    //mientras + TparA + EXPRESION + TparC + TllaA + TllaC              1
                    if (root.ChildNodes.Count == 1)
                    {
                        Ambito nuevo = new Ambito(anterior);
                        while (ejecutar(root.ChildNodes[0], anterior).Equals("verdadero"))
                        {
                            //-------NO TIENE INSTRUCCIONES
                        }
                    }
                    //mientras + TparA + EXPRESION + TparC + TllaA + INS_CLA + TllaC    2
                    else if (root.ChildNodes.Count == 2)
                    {
                        Ambito nuevo = new Ambito(anterior);
                        salirFlag = false;
                        nuevo.salida = true;
                        continuaFlag = false;
                        nuevo.continuar = true;

                        while (ejecutar(root.ChildNodes[0], anterior).Equals("verdadero"))
                        {
                            //---- EJECUTAR LAS INSTRUCCIONES
                            continuaFlag = false;
                            ejecutar(root.ChildNodes[1], nuevo);

                            if (salirFlag == true && anterior.salida == false)
                            {
                                salirFlag = false;
                                break;
                            }
                            if (salirFlag == true)
                            {
                                break;
                            }
                        }
                        nuevo.salida = false;
                        nuevo.continuar = false;
                        if (anterior.continuar == false)
                        {
                            continuaFlag = false;
                        }
                    }
                    return "";
                #endregion

                //---------------------------- SENTENCIA COMPROBAR
                #region COMPROBAR
                case "COMPROBAR":
                    //comprobar + TparA + EXPRESION + TparC + TllaA + TllaC             1
                    if (root.ChildNodes.Count == 1)
                    {
                        //-- Variable que se va a comparar
                        comparar = "";
                        comparar = limpiarVariables(ejecutar(root.ChildNodes[0], anterior), anterior);
                        flagCom = false;
                        //-- Ejecutar lista de casos -- NO TIENE
                    }
                    //comprobar + TparA + EXPRESION + TparC + TllaA + L_CASO + TllaC    2
                    if (root.ChildNodes.Count == 2)
                    {
                        //-- Variable que se va a comparar
                        comparar = "";
                        comparar = limpiarVariables(ejecutar(root.ChildNodes[0], anterior), anterior);

                        Ambito nuevo = new Ambito(anterior);
                        flagCom = false;

                        salirFlag = false;          //-- Salir
                        nuevo.salida = true;
                        continuaFlag = false;       //-- Continuar
                        nuevo.continuar = true;

                        //-- Ejecutar lista de casos
                        ejecutar(root.ChildNodes[1], nuevo);

                        if (anterior.salida == false)
                            salirFlag = false;
                        if (anterior.continuar == false)
                            continuaFlag = false;
                        nuevo.salida = false;
                        nuevo.continuar = false;
                    }

                    return "";
                #endregion

                #region L_CASO
                case "L_CASO":
                    string reC = "falso";
                    foreach (ParseTreeNode hijo in root.ChildNodes)
                    {
                        if (flagCom == false)
                        {
                            reC = ejecutar(hijo, anterior);
                        }
                    }
                    return reC;
                #endregion

                #region CASO
                case "CASO":
                    //caso + EXPRESION + DosPuntos + INS_CLA    2
                    if (root.ChildNodes.Count == 2)
                    {
                        string cc = limpiarVariables(ejecutar(root.ChildNodes[0], anterior), anterior);
                        if (cc.Equals(comparar))
                        {
                            continuaFlag = false;
                            ejecutar(root.ChildNodes[1], anterior);     //Ejecutar el codigo
                            if (salirFlag == true)
                                flagCom = true;
                            if (continuaFlag == true)
                                flagCom = true;
                        }
                    }
                    //defecto + DosPuntos + INS_CLA             1
                    if (root.ChildNodes.Count == 1)
                    {
                        continuaFlag = false;
                        ejecutar(root.ChildNodes[0], anterior);     // Unicamente Ejecutar el codigo del defecto
                        if (salirFlag == true)
                            flagCom = true;
                        if (continuaFlag == true)
                            flagCom = true;
                    }

                    return "";
                #endregion

                #region SALIR
                case "SALIR":
                    if (anterior.salida == true)
                    {
                        salirFlag = true;
                    }
                    else
                    {
                        ErroresSem es = new ErroresSem("La sentencia SALIR no puede venir en el ambito colocado", 0, 0);
                        es.insertarErrSem(es);
                    }
                    return "#salir#";
                #endregion
                //---------------------------- SENTENCIA HACER MIENTRAS 
                #region HACER
                case "HACER":
                    //hacer + TllaA + TllaC + mient + TparA + EXPRESION + TparC + PuntoComa             1
                    if (root.ChildNodes.Count == 1)
                    {
                        Ambito nuevo = new Ambito(anterior);
                        do
                        {
                            //--- NO TIENE INSTRUCCIONES ---//
                        } while (ejecutar(root.ChildNodes[0], anterior).Equals("verdadero"));
                    }
                    //hacer + TllaA + INS_CLA + TllaC + mient + TparA + EXPRESION + TparC + PuntoComa   2
                    else if (root.ChildNodes.Count == 2)
                    {
                        Ambito nuevo = new Ambito(anterior);
                        salirFlag = false;          //-- Salir
                        nuevo.salida = true;
                        continuaFlag = false;       //-- Continuar
                        nuevo.continuar = true;

                        do
                        {
                            continuaFlag = false;
                            if (salirFlag == true && anterior.salida == false)
                            {
                                salirFlag = false;
                                break;
                            }
                            else if (salirFlag == true)
                                break;

                            ejecutar(root.ChildNodes[0], nuevo);     //EJECUTAR LAS INSTRUCCIONES

                        } while (ejecutar(root.ChildNodes[1], anterior).Equals("verdadero"));
                        if (anterior.continuar == false)
                        {
                            continuaFlag = false;
                        }
                        nuevo.salida = false;
                        nuevo.continuar = false;
                    }
                    return "";
                #endregion

                #region CONTINUAR
                case "CONTINUAR":
                    if (anterior.continuar == true)
                    {
                        continuaFlag = true;
                    }
                    else
                    {
                        ErroresSem es = new ErroresSem("La sentencia CONTINUAR no puede venir en el ambito colocado", 0, 0);
                        es.insertarErrSem(es);
                    }
                    return "#continuar#";
                #endregion
                //---------------------------- FUNCION NATIVA IMAGEN
                #region FIGURE
                case "FIGURE":
                    //addFigure + TparA + FIGURAS + TparC + PuntoComa
                    if (root.ChildNodes.Count == 1)
                    {
                        ejecutar(root.ChildNodes[0], anterior);
                    }
                    return "";
                #endregion

                #region FIGURAS
                case "FIGURAS":
                    if (root.ChildNodes.Count == 2)
                    {
                        //circle + TparA + L_PARAM + TparC
                        //triangle + TparA + L_PARAM + TparC
                        //square + TparA + L_PARAM + TparC
                        //line + TparA + L_PARAM + TparC
                        phrase = root.ChildNodes[0].ToString();
                        words = phrase.Split(' ');
                        if (words[0].Equals("circle"))
                        {
                            //-----------PARAMETROS
                            //--Color,Radio,Solido,PosX,PosY
                            flagFig = true;
                            string fig = ejecutar(root.ChildNodes[1], anterior);
                            fig = "circulo," + limpiarFigura(fig, anterior);
                            flagFig = false;
                            figuras.Add(fig);
                        }
                        else if (words[0].Equals("triangle"))
                        {
                            //-----------PARAMETROS
                            //--Color,Solido,PosX,PosY,PosX1,PosY1,PosX2,PosY2
                            flagFig = true;
                            string fig = ejecutar(root.ChildNodes[1], anterior);
                            fig = "triangulo," + limpiarFigura(fig, anterior);
                            flagFig = false;
                            figuras.Add(fig);
                        }
                        else if (words[0].Equals("square"))
                        {
                            //-----------PARAMETROS
                            //--Color,Solido,PosX,PosY,Alto,Ancho
                            flagFig = true;
                            string fig = ejecutar(root.ChildNodes[1], anterior);
                            fig = "rectangulo," + limpiarFigura(fig, anterior);
                            flagFig = false;
                            figuras.Add(fig);
                        }
                        else if (words[0].Equals("line"))
                        {
                            //-----------PARAMETROS
                            //--Color,InicioX,InicioY,FinX,FinY,Grosor
                            flagFig = true;
                            string fig = ejecutar(root.ChildNodes[1], anterior);
                            fig = "linea," + limpiarFigura(fig, anterior);
                            flagFig = false;
                            figuras.Add(fig);
                        }
                    }

                    return "";
                #endregion

                #region ADD_FIGURE
                case "ADD_FIGURE":
                    //figure + TparA + EXPRESION + TparC + PuntoComa
                    if (root.ChildNodes.Count == 1)
                    {
                        //-----GRAFICAR LA IMAGEN
                        string nombIm = limpiarVariables(ejecutar(root.ChildNodes[0], anterior), anterior);
                        Dibujar d = new Dibujar();
                        d.nom = nombIm;
                        d.dibujo(figuras);
                        figuras.Clear();
                    }
                    return "";
                #endregion


                default:
                    return "";


            }
        }




        //---------COMPROBACION LOGICA
        public static string ComprobarLogico(string val1, string val2, string operador, Ambito actual)
        {
            int valorNumerico = 0, valorNumerico1 = 0;
            Double valorNumeri = 0;
            string v1 = "", v2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la comprobacion logica ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }

            //MessageBox.Show("1." + val1 + " 2." + val2);

            #region OBTENER
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
            }
            //Arreglo (---- LA EXPRESION YA DEVUELVE EL VALOR ----)
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
        public static string ComprobarMayor(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la comprobacion de mayor > ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }
            //MessageBox.Show("1." + val1 + " 2." + val2);

            #region OBTENER
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXPRESION YA DEVUELVE EL VALOR ---)
            //Int
            int valorNumerico = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico))
            {
                tipo2 = "int";
                v2 = val2;
            }
            //Double
            Double valorNumerico1 = 0;
            if (Double.TryParse(val1, out valorNumerico1))
            {
                tipo1 = "double";
                v1 = val1;
            }
            if (Double.TryParse(val2, out valorNumerico1))
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

        public static string ComprobarMenor(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la comprobacion de menor < ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }
            //MessageBox.Show("1." + val1 + " 2." + val2);

            #region OBTENER
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXPRESION YA DEVUELVE EL VALOR ---)
            //Int
            int valorNumerico = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico))
            {
                tipo2 = "int";
                v2 = val2;
            }
            //Double
            Double valorNumerico1 = 0;
            if (Double.TryParse(val1, out valorNumerico1))
            {
                tipo1 = "double";
                v1 = val1;
            }
            if (Double.TryParse(val2, out valorNumerico1))
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

        public static string ComprobarMayorI(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la comprobacion mayor igual >= ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }
            //MessageBox.Show("1." + val1 + " 2." + val2);

            #region OBTENER
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXPRESION YA DEVUELVE EL VALOR ---)
            //Int
            int valorNumerico = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico))
            {
                tipo2 = "int";
                v2 = val2;
            }
            //Double
            Double valorNumerico1 = 0;
            if (Double.TryParse(val1, out valorNumerico1))
            {
                tipo1 = "double";
                v1 = val1;
            }
            if (Double.TryParse(val2, out valorNumerico1))
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

        public static string ComprobarMenorI(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la comprobacion menor igual <= ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }
            //MessageBox.Show("1." + val1 + " 2." + val2);

            #region OBTENER
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXPRESION YA DEVUELVE EL VALOR ---)
            //Int
            int valorNumerico = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico))
            {
                tipo2 = "int";
                v2 = val2;
            }
            //Double
            Double valorNumerico1 = 0;
            if (Double.TryParse(val1, out valorNumerico1))
            {
                tipo1 = "double";
                v1 = val1;
            }
            if (Double.TryParse(val2, out valorNumerico1))
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

        public static string ComprobarIgual(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la comprobacion igual == ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }
            //MessageBox.Show("1." + val1 + " 2." + val2);

            #region OBTENER
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXPRESION YA DEVUELVE EL VALOR ---)
            //Int
            int valorNumerico = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico))
            {
                tipo2 = "int";
                v2 = val2;
            }
            //Double
            Double valorNumerico1 = 0;
            if (Double.TryParse(val1, out valorNumerico1))
            {
                tipo1 = "double";
                v1 = val1;
            }
            if (Double.TryParse(val2, out valorNumerico1))
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

        public static string ComprobarDiferente(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "";
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la comprobacion de diferente != ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }
            //MessageBox.Show("1." + val1 + " 2." + val2);

            #region OBTENER
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXPRESION YA DEVUELVE EL VALOR ---)
            //Int
            int valorNumerico = 0;
            if (int.TryParse(val1, out valorNumerico))
            {
                tipo1 = "int";
                v1 = val1;
            }
            if (int.TryParse(val2, out valorNumerico))
            {
                tipo2 = "int";
                v2 = val2;
            }
            //Double
            Double valorNumerico1 = 0;
            if (Double.TryParse(val1, out valorNumerico1))
            {
                tipo1 = "double";
                v1 = val1;
            }
            if (Double.TryParse(val2, out valorNumerico1))
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
        public static string ComprobarSuma(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "", tipo1 = "", tipo2 = "", suma = "";
            int sum = 0;
            double sum1 = 0.0;
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la suma + ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }

            //MessageBox.Show("Suma #1 " + val1 + " #2 " + val2);

            #region OBTENER            
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXP YA DEVUELVE EL VALOR --)
            //int
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
                v1 = val1.Substring(1, val1.Length - 3); ///SI FALLA EL CODIGO, AQUI LA CAGUE
                tipo1 = "string";
            }
            if (val2.Contains("\""))
            {
                v2 = val2.Substring(1, val2.Length - 3);
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
                suma = v1 + v2;
                suma = "\"" + suma + "\"";
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
                suma = v1 + v2;
                suma = "\"" + suma + "\"";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("string"))
            {
                suma = v1 + v2;
                suma = "\"" + suma + "\"";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("double"))
            {
                suma = v1 + v2;
                suma = "\"" + suma + "\"";
            }
            else if (tipo1.Equals("string") && tipo2.Equals("char"))
            {
                suma = v1 + v2;
                suma = "\"" + suma + "\"";
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
                suma = v1 + v2;
                suma = "\"" + suma + "\"";
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
                suma = v1 + v2;
                suma = "\"" + suma + "\"";
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

            if (suma.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la suma + ", 0, 0);
                es.insertarErrSem(es);
            }
            return suma;
        }

        public static string ComprobarResta(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "";
            string tipo1 = "", tipo2 = "", suma = "";
            int sum = 0;
            double sum1 = 0.0;
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la resta - ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }
            //MessageBox.Show("Resta #1 " + val1 + " #2 " + val2);

            #region OBTENER            
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXP YA DEVUELVE EL VALOR --)
            //int
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

            //que tipo de resta es
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                sum = Int32.Parse(v1) - Int32.Parse(v2);
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
                sum = my[0] - my1[0];
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

            if (suma.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la resta - ", 0, 0);
                es.insertarErrSem(es);
            }
            return suma;
        }

        public static string ComprobarMulti(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "";
            string tipo1 = "", tipo2 = "", suma = "";
            int sum = 0;
            double sum1 = 0.0;
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la multiplicacion * ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }
            //MessageBox.Show("Multi #1 " + val1 + " #2 " + val2);

            #region OBTENER            
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXP YA DEVUELVE EL VALOR --)
            //int
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

            if (suma.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la multiplicacion * ", 0, 0);
                es.insertarErrSem(es);
            }
            return suma;
        }

        public static string ComprobarDivision(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "";
            string tipo1 = "", tipo2 = "", suma = "";
            int sum = 0;
            double sum1 = 0.0;
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la division / ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }
            //MessageBox.Show("Div #1 " + val1 + " #2 " + val2);

            #region OBTENER            
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXP YA DEVUELVE EL VALOR --)
            //int
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

            if (suma.Contains("????") )
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la division / ", 0, 0);
                es.insertarErrSem(es);
            }

            return suma;
        }

        public static string ComprobarPotencia(string val1, string val2, Ambito actual)
        {
            string v1 = "", v2 = "";
            string tipo1 = "", tipo2 = "", suma = "";
            double sum = 0;
            double sum1 = 0.0;
            //Ver si lo que viene es error
            if (val1.Contains("????") || val2.Contains("????"))
            {
                ErroresSem es = new ErroresSem("Existe un error con los valores de la potencia ", 0, 0);
                es.insertarErrSem(es);
                return "????";
            }
            //MessageBox.Show("Potencia #1 " + val1 + " #2 " + val2);

            #region OBTENER            
            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);

                v1 = auxiliar.valor;
                tipo1 = auxiliar.tipo;
            }
            if (val2.StartsWith("@@"))
            {
                val2 = val2.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val2);

                v2 = auxiliar.valor;
                tipo2 = auxiliar.tipo;
            }
            //Arreglos (--- LA EXP YA DEVUELVE EL VALOR --)
            //int
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

            //que tipo de Potencia es
            //PRIMER VALOR CON INT
            #region V1_INT
            if (tipo1.Equals("int") && tipo2.Equals("int"))
            {
                sum = Math.Pow(Int32.Parse(v1), Int32.Parse(v2));
                suma = sum.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("int") && tipo2.Equals("double"))
            {
                sum1 = Math.Pow(Int32.Parse(v1), Convert.ToDouble(v2));
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum = Math.Pow(Int32.Parse(v1), my[0]);
                suma = sum.ToString();
            }
            else if (tipo1.Equals("int") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = Math.Pow(Int32.Parse(v1), f);
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
                sum1 = Math.Pow(Convert.ToDouble(v1), Int32.Parse(v2));
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("double") && tipo2.Equals("double"))
            {
                sum1 = Math.Pow(Convert.ToDouble(v1), Convert.ToDouble(v2));
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                sum1 = Math.Pow(Convert.ToDouble(v1), my[0]);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("double") && tipo2.Equals("bool"))
            {
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum1 = Math.Pow(Convert.ToDouble(v1), f);
                suma = sum1.ToString();
            }
            #endregion

            //PRIMER VALOR CON CHAR
            #region V1_CHAR
            if (tipo1.Equals("char") && tipo2.Equals("int"))
            {
                char[] my = v1.ToCharArray();
                sum = Math.Pow(my[0], Int32.Parse(v2));
                suma = sum.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("string"))
            {
                suma = "????";
            }
            else if (tipo1.Equals("char") && tipo2.Equals("double"))
            {
                char[] my = v1.ToCharArray();
                sum1 = Math.Pow(my[0], Convert.ToDouble(v2));
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("char"))
            {
                char[] my = v1.ToCharArray();
                char[] my1 = v2.ToCharArray();
                sum = Math.Pow(my[0], my1[0]);
                suma = sum.ToString();
            }
            else if (tipo1.Equals("char") && tipo2.Equals("bool"))
            {

                char[] my = v1.ToCharArray();
                int f = 0;
                if (v2.Contains("verdadero"))
                    f = 1;

                sum = Math.Pow(my[0], f);
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

                sum = Math.Pow(Int32.Parse(v2), f);
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
                sum1 = Math.Pow(Convert.ToDouble(v2), f);
                suma = sum1.ToString();
            }
            else if (tipo1.Equals("bool") && tipo2.Equals("char"))
            {
                char[] my = v2.ToCharArray();
                int f = 0;
                if (v1.Contains("verdadero"))
                    f = 1;

                sum = Math.Pow(my[0], f);
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
                return (1 + my[0]).ToString();
            }
            //Id
            else if (incre.Contains("@"))
            {
                string va = incre.Substring(2);
                Variables v = new Variables(va, "", "", "", "");
                Boolean ff = Ambito.AumentarValorVariable(am, v);
                if (ff)
                {
                    Variables vv = Ambito.obtenerValorVariable(am, incre);
                    return vv.valor;
                }
                ErroresSem es = new ErroresSem("Existe un error al intentar realizar el incremento ", 0, 0);
                es.insertarErrSem(es);
                return "";
            }
            //Objeto
            //Si es algo mas
            else
            {

                ErroresSem es = new ErroresSem("Existe un error al intentar realizar el incremento ", 0, 0);
                es.insertarErrSem(es);
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
                return (my[0] - 1).ToString();
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
                ErroresSem es = new ErroresSem("Existe un error al intentar realizar el decremento ", 0, 0);
                es.insertarErrSem(es);
                return "";
            }
            //Objeto
            //Si es algo mas
            else
            {
                ErroresSem es = new ErroresSem("Existe un error al intentar realizar el decremento ", 0, 0);
                es.insertarErrSem(es);
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

        public static string limpiarVariables(string val1, Ambito actual)
        {
            string v1 = "";

            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);
                v1 = auxiliar.valor;
            }
            //caracter
            else if (val1.Contains("'"))
            {
                v1 = val1.Substring(1, val1.Length - 2);
            }
            //cadena    /*error*/            
            else if (val1.Contains("\""))
            {
                v1 = val1.Substring(1, val1.Length - 2);
            }
            else
                v1 = val1;
            //bool      /*error*/
            //Arreglos (--- LA EXPRESION YA DEVUELVE EL VALOR ---)
            //Int
            //Double

     
            return v1;
        }

        public static string limpiarVariables2(string val1, Ambito actual)
        {
            string v1 = "";

            //objetos

            //id
            if (val1.StartsWith("@@"))
            {
                val1 = val1.Substring(2);
                Variables auxiliar = Ambito.obtenerValorVariable(actual, val1);
                v1 = auxiliar.valor;
            }
            //caracter
            //cadena    /*error*/            
            else
                v1 = val1;
            //bool      /*error*/
            //Arreglos (--- LA EXPRESION YA DEVUELVE EL VALOR ---)
            //Int
            //Double

            return v1;
        }


        public static string limpiarFigura(string val, Ambito actual)
        {
            string phrase = val;
            string[] words = phrase.Split(',');
            string devolver = "";
            int i = 0;
            foreach (string hijo in words)
            {
                if (i != 0)
                    devolver += ",";
                devolver += limpiarVariables(hijo, actual);
                i++;
            }
            return devolver;
        }

    }
}
