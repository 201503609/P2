using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace OLC2_Proyecto2.Gramatica
{
    class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: false)
        {
            #region ER
            //Numero
            NumberLiteral num = TerminalFactory.CreateCSharpNumber("num");
            //id
            IdentifierTerminal id = TerminalFactory.CreateCSharpIdentifier("id");
            //Carcater
            StringLiteral TkCaract = TerminalFactory.CreateCSharpChar("TkCaract");
            //Cadena
            StringLiteral TkCadena = TerminalFactory.CreateCSharpString("TkCadena");
            //comentario linea
            var comentarioLinea = new CommentTerminal("ComentarioL", ">>", "\n");
            //comentario multilinea 
            var comentarioMultilinea = new CommentTerminal("comentarioMultilinea", "<-", "->");

            base.NonGrammarTerminals.Add(comentarioLinea);
            base.NonGrammarTerminals.Add(comentarioMultilinea);
            #endregion

            #region Terminales
            var entero = ToTerm("int");
            var doble = ToTerm("double");
            var booleano = ToTerm("bool");
            var caract = ToTerm("char");
            var caden = ToTerm("string");

            var addFigure = ToTerm("addFigure");
            var figure = ToTerm("figure");
            var circle = ToTerm("circle");
            var triangle = ToTerm("triangle");
            var square = ToTerm("square");
            var line = ToTerm("line");

            var continuar = ToTerm("continuar");
            var hacer = ToTerm("hacer");
            var mient = ToTerm("mientras");
            var comprobar = ToTerm("comprobar");
            var caso = ToTerm("caso");
            var defecto = ToTerm("defecto");
            var salir = ToTerm("salir");
            var mientras = ToTerm("while");
            var repetir = ToTerm("Repeat");
            var para = ToTerm("for");
            var si = ToTerm("if");
            var sino = ToTerm("else");
            var imprimir = ToTerm("print");
            var show = ToTerm("show");
            var retorno = ToTerm("return");
            var over = ToTerm("override");
            var voir = ToTerm("void");
            var main = ToTerm("main");
            var nuevo = ToTerm("new");
            var array = ToTerm("array");
            var clase = ToTerm("clase");
            var importar = ToTerm("importar");
            var publico = ToTerm("publico");
            var privado = ToTerm("privado");

            var verdadero = ToTerm("verdadero");
            var falso = ToTerm("falso");
            var v = ToTerm("true");
            var f = ToTerm("false");

            var incremento = ToTerm("++");
            var decremento = ToTerm("--");

            var Tand = ToTerm("&&");
            var Tor = ToTerm("||");
            var Tnot = ToTerm("!");

            var Tmayor = ToTerm(">");
            var TmayorI = ToTerm(">=");
            var Tmenor = ToTerm("<");
            var TmenorI = ToTerm("<=");
            var Tigualacion = ToTerm("==");
            var Tdiferente = ToTerm("!=");

            var Tmas = ToTerm("+");
            var Tmenos = ToTerm("-");
            var Tpor = ToTerm("*");
            var Tdiv = ToTerm("/");
            var Tpot = ToTerm("^");

            var DosPuntos = ToTerm(":");
            var PuntoComa = ToTerm(";");
            var Coma = ToTerm(",");
            var Punto = ToTerm(".");
            var Igual = ToTerm("=");

            var TparA = ToTerm("(");
            var TparC = ToTerm(")");
            var TcorA = ToTerm("[");
            var TcorC = ToTerm("]");
            var TllaA = ToTerm("{");
            var TllaC = ToTerm("}");
            #endregion

            #region No Terminales
            NonTerminal S = new NonTerminal("S"),
                INICIO = new NonTerminal("INICIO"),
                INSTRUCCIONES = new NonTerminal("INSTRUCCIONES"),
                DECLARACIONES = new NonTerminal("DECLARACIONES"),
                AMBITO = new NonTerminal("AMBITO"),
                TIPO = new NonTerminal("TIPO"),
                L_ID = new NonTerminal("L_ID"),
                FIN_DECLA = new NonTerminal("FIN_DECLA"),
                EXPRESION = new NonTerminal("EXPRESION"),
                EXP = new NonTerminal("EXP"),
                EX = new NonTerminal("EX"),
                E = new NonTerminal("E"),
                T = new NonTerminal("T"),
                F = new NonTerminal("F"),
                P = new NonTerminal("P"),
                BOOL = new NonTerminal("BOOL"),
                ASIGNA = new NonTerminal("ASIGNA"),
                INCREMENTO = new NonTerminal("INCREMENTO"),
                DECREMENTO = new NonTerminal("DECREMENTO"),
                DECLA_ARRE = new NonTerminal("DECLA_ARRE"),
                DIMENSIONES = new NonTerminal("DIMENSIONES"),
                VAL_DIM = new NonTerminal("VAL_DIM"),
                FIN_ARRE = new NonTerminal("FIN_ARRE"),
                VAL_AA = new NonTerminal("VAL_AA"),
                VAL_AA1 = new NonTerminal("VAL_AA1"),
                VAA = new NonTerminal("VAA"),
                USO_ARRE = new NonTerminal("USO_ARRE"),
                CLASE = new NonTerminal("CLASE"),
                INS_CLA = new NonTerminal("INS_CLA"),
                IMPORTACIONES = new NonTerminal("IMPORTACIONES"),
                OBJETOS = new NonTerminal("OBJETOS"),
                L_ID1 = new NonTerminal("L_ID1"),
                PARAMETROS = new NonTerminal("PARAMETROS"),
                L_PARAM = new NonTerminal("L_PARAM"),
                AS_OBJ = new NonTerminal("AS_OBJ"),
                MAIN = new NonTerminal("MAIN"),
                FUNCION_SR = new NonTerminal("FUNCION_SR"),
                FUNCION_CR = new NonTerminal("FUNCION_CR"),
                OVER = new NonTerminal("OVER"),
                PARAMETROS1 = new NonTerminal("PARAMETROS1"),
                L_PARAM1 = new NonTerminal("L_PARAM1"),
                PARAM1 = new NonTerminal("PARAM1"),
                RETORNO = new NonTerminal("RETORNO"),
                IMPRIMIR = new NonTerminal("IMPRIMIR"),
                SHOW = new NonTerminal("SHOW"),
                SI = new NonTerminal("SI"),
                EXTRA_SI = new NonTerminal("EXTRA_SI"),
                L_SINO = new NonTerminal("L_SINO"),
                SINO = new NonTerminal("SINO"),
                FOR = new NonTerminal("FOR"),
                V_FOR = new NonTerminal("V_FOR"),
                ACTUALIZACION = new NonTerminal("ACTUALIZACION"),
                REPETIR = new NonTerminal("REPETIR"),
                MIENTRAS = new NonTerminal("MIENTRAS"),
                COMPROBAR = new NonTerminal("COMPROBAR"),
                L_CASO = new NonTerminal("L_CASO"),
                CASO = new NonTerminal("CASO"),
                SALIR = new NonTerminal("SALIR"),
                HACER = new NonTerminal("HACER"),
                CONTINUAR = new NonTerminal("CONTINUAR"),
                FIGURE = new NonTerminal("FIGURE"),
                FIGURAS = new NonTerminal("FIGURAS"),
                ADD_FIGURE = new NonTerminal("ADD_FIGURE");



            #endregion

            #region Gramatica
            S.Rule = INICIO;

            INICIO.Rule = INICIO + CLASE
                                    | CLASE;

            //--------------------------------------------CLASE 4.8.1
            CLASE.Rule = clase + id + IMPORTACIONES + TllaA + INS_CLA + TllaC
                                    | clase + id + IMPORTACIONES + TllaA + TllaC
                                    | clase + id + TllaA + INS_CLA + TllaC
                                    | clase + id + TllaA + TllaC;

            //--------------------------------------------IMPORTACIONES 
            IMPORTACIONES.Rule = importar + L_ID;

            INS_CLA.Rule = INS_CLA + INSTRUCCIONES
                                    | INSTRUCCIONES;

            INSTRUCCIONES.Rule = DECLARACIONES
                                    | ASIGNA
                                    | INCREMENTO
                                    | DECREMENTO
                                    | DECLA_ARRE
                                    | USO_ARRE
                                    | OBJETOS
                                    | AS_OBJ
                                    | MAIN
                                    | FUNCION_SR
                                    | RETORNO
                                    | FUNCION_CR
                                    | IMPRIMIR
                                    | SHOW
                                    | SI
                                    | FOR
                                    | REPETIR
                                    | MIENTRAS
                                    | COMPROBAR
                                    | SALIR
                                    | HACER
                                    | CONTINUAR
                                    | FIGURE
                                    | ADD_FIGURE;
            //--------------------------------------------DECLARACION DE VARIABLES 4.7.1
            //--------------------------------------------INSTANCIA DE CLASE 4.8.2
            DECLARACIONES.Rule = AMBITO + TIPO + L_ID + FIN_DECLA
                                    | TIPO + L_ID + FIN_DECLA;
            //AMBITOS
            AMBITO.Rule = publico
                                    | privado;
            //TIPO DE DATO
            TIPO.Rule = entero
                                    | doble
                                    | booleano
                                    | caract
                                    | caden
                                    | id;       //Si en dado caso no me sale es por esto
            //LISTA DE ID
            L_ID.Rule = L_ID + Coma + id
                                    | id;
            //FIN DE LA DECLARACION
            FIN_DECLA.Rule = PuntoComa
                                    | Igual + EXPRESION + PuntoComa
                                    | Igual + nuevo + id + TparA + TparC + PuntoComa; //Para los objetos
            //EXPRESION
            EXPRESION.Rule = EXPRESION + Tor + EXP
                                    | EXPRESION + Tand + EXP
                                    | Tnot + EXPRESION
                                    | EXP;
            EXP.Rule = EXP + Tmayor + EX
                                    | EXP + TmayorI + EX
                                    | EXP + TmayorI + EX
                                    | EXP + Tmenor + EX
                                    | EXP + TmenorI + EX
                                    | EXP + Tigualacion + EX
                                    | EXP + Tdiferente + EX
                                    | EX;
            EX.Rule = EX + Tmas + E
                                    | EX + Tmenos + E
                                    | E;
            E.Rule = E + Tpor + T
                                    | E + Tdiv + T
                                    | T;
            T.Rule = T + Tpot + F
                                    | F;
            //PRODUCCION DONDE TAMBIEN PUEDE LLAMAR METODOS
            F.Rule = TparA + EXPRESION + TparC
                                    //| P + TparA + TparC
                                    //| P + TparA + EXPRESION + TparC
                                    | P + DIMENSIONES
                                    | P;
            P.Rule = num
                                    | id
                                    | TkCadena
                                    | TkCaract
                                    | BOOL
                                    | OBJETOS;
            BOOL.Rule = verdadero
                                    | falso
                                    | v
                                    | f;
            //--------------------------------------------ASIGNACION 4.7.2 4.7.6
            ASIGNA.Rule = id + Igual + EXPRESION + PuntoComa;
            //--------------------------------------------INCREMENTO 4.7.3
            INCREMENTO.Rule = P + incremento + PuntoComa;
            //--------------------------------------------DECREMENTO 4.7.4
            DECREMENTO.Rule = P + decremento + PuntoComa;
            //--------------------------------------------DECLARACION DE ARREGLOS 4.7.5
            DECLA_ARRE.Rule = AMBITO + TIPO + array + L_ID + DIMENSIONES + FIN_ARRE
                                    | TIPO + array + L_ID + DIMENSIONES + FIN_ARRE;
            DIMENSIONES.Rule = DIMENSIONES + VAL_DIM
                                    | VAL_DIM;
            VAL_DIM.Rule = TcorA + EXPRESION + TcorC;

            FIN_ARRE.Rule = PuntoComa
                                    | Igual + OBJETOS + PuntoComa
                                    | Igual + TllaA + VAL_AA + TllaC + PuntoComa;
            VAL_AA.Rule = VAL_AA + Coma + VAL_AA1
                                    | VAL_AA1;
            VAL_AA1.Rule = TllaA + VAA + TllaC;

            VAA.Rule = VAA + Coma + EXPRESION
                                    | EXPRESION;
            //--------------------------------------------REASIGNACION DE ARREGLOS 4.7.7
            USO_ARRE.Rule = id + DIMENSIONES + Igual + EXPRESION + PuntoComa;

            //--------------------------------------------4.8.3 
            OBJETOS.Rule = //L_ID1 + PARAMETROS + PuntoComa
                                     L_ID1 + PARAMETROS
                                    //L_ID1 + PuntoComa
                                    | L_ID1;

            L_ID1.Rule = L_ID1 + Punto + id
                                    | id;

            PARAMETROS.Rule = TparA + TparC
                                    | TparA + L_PARAM + TparC;

            L_PARAM.Rule = L_PARAM + Coma + EXPRESION
                                    | EXPRESION;
            //--------------------------------------------4.84 REASINACION DE VAR GLOBALES
            AS_OBJ.Rule = AMBITO + OBJETOS + Igual + EXPRESION + PuntoComa
                                    | OBJETOS + Igual + EXPRESION + PuntoComa
                                    | AMBITO + OBJETOS + PuntoComa
                                    | OBJETOS + PuntoComa;
            //--------------------------------------------METODO MAIN 4.8.7
            MAIN.Rule = main + TparA + TparC + TllaA + TllaC
                                    | main + TparA + TparC + TllaA + INS_CLA + TllaC;
            //--------------------------------------------FUNCION SIN RETORNO 4.8.8
            FUNCION_SR.Rule = AMBITO + id + voir + OVER + PARAMETROS1 + TllaA + TllaC
                                    | id + voir + OVER + PARAMETROS1 + TllaA + TllaC
                                    | AMBITO + id + voir + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC
                                    | id + voir + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC;

            OVER.Rule = over
                                    | Empty;

            PARAMETROS1.Rule = TparA + TparC
                                    | TparA + L_PARAM1 + TparC;
            L_PARAM1.Rule = L_PARAM1 + Coma + PARAM1
                                    | PARAM1;
            PARAM1.Rule = TIPO + id;
            //--------------------------------------------RETURN 4.8.9
            RETORNO.Rule = retorno + EXPRESION + PuntoComa;
            //--------------------------------------------FUNCION CON RETORNO 4.8.10
            FUNCION_CR.Rule = AMBITO + id + TIPO + OVER + PARAMETROS1 + TllaA + TllaC
                                    | AMBITO + id + array + TIPO + DIMENSIONES + OVER + PARAMETROS1 + TllaA + TllaC
                                    | AMBITO + id + TIPO + id + OVER + PARAMETROS1 + TllaA + TllaC
                                    | AMBITO + id + TIPO + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC
                                    | AMBITO + id + array + TIPO + DIMENSIONES + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC
                                    | AMBITO + id + TIPO + id + OVER + PARAMETROS1 + TllaA + INS_CLA + TllaC;
            //--------------------------------------------IMPRIMIR 4.9
            IMPRIMIR.Rule = imprimir + TparA + EXPRESION + TparC + PuntoComa;
            //--------------------------------------------SHOW 4.10
            SHOW.Rule = show + TparA + EXPRESION + Coma + EXPRESION + TparC + PuntoComa;
            //--------------------------------------------SENTENCIA IF 4.11.1
            SI.Rule = si + TparA + EXPRESION + TparC + TllaA + TllaC + EXTRA_SI
                                    | si + TparA + EXPRESION + TparC + TllaA + TllaC + L_SINO + EXTRA_SI
                                    | si + TparA + EXPRESION + TparC + TllaA + INS_CLA + TllaC + EXTRA_SI
                                    | si + TparA + EXPRESION + TparC + TllaA + INS_CLA + TllaC + L_SINO + EXTRA_SI;

            L_SINO.Rule = L_SINO + SINO
                                    | SINO;

            SINO.Rule = sino + si + TparA + EXPRESION + TparC + TllaA + TllaC
                                    | sino + si + TparA + EXPRESION + TparC + TllaA + INS_CLA + TllaC;

            EXTRA_SI.Rule = sino + TllaA + TllaC
                                    | sino + TllaA + INS_CLA + TllaC
                                    | Empty;
            //--------------------------------------------FOR 
            FOR.Rule = para + TparA + V_FOR + EXPRESION + PuntoComa + ACTUALIZACION + TparC + TllaA + TllaC
                                    | para + TparA + V_FOR + EXPRESION + PuntoComa + ACTUALIZACION + TparC + TllaA + INS_CLA + TllaC;
            V_FOR.Rule = ASIGNA
                                    | DECLARACIONES;
            ACTUALIZACION.Rule = P + incremento
                                    | P + decremento;
            //--------------------------------------------REPETIR
            REPETIR.Rule = repetir + TparA + EXPRESION + TparC + TllaA + TllaC
                                    | repetir + TparA + EXPRESION + TparC + TllaA + INS_CLA + TllaC; ;
            //--------------------------------------------MIENTRAS
            MIENTRAS.Rule = mientras + TparA + EXPRESION + TparC + TllaA + TllaC
                                    | mientras + TparA + EXPRESION + TparC + TllaA + INS_CLA + TllaC;
            //--------------------------------------------COMPROBAR
            COMPROBAR.Rule = comprobar + TparA + EXPRESION + TparC + TllaA + TllaC
                                    | comprobar + TparA + EXPRESION + TparC + TllaA + L_CASO + TllaC;

            L_CASO.Rule             = L_CASO + CASO
                                    | CASO;

            CASO.Rule               = caso + EXPRESION + DosPuntos + INS_CLA
                                    | defecto + DosPuntos + INS_CLA;

            SALIR.Rule              = salir + PuntoComa;
            //--------------------------------------------HACER MIENTRAS
            HACER.Rule              = hacer + TllaA + TllaC + mient + TparA + EXPRESION + TparC + PuntoComa
                                    | hacer + TllaA + INS_CLA + TllaC + mient + TparA + EXPRESION + TparC + PuntoComa;
            //--------------------------------------------CONTINUAR
            CONTINUAR.Rule = continuar + PuntoComa;
            //--------------------------------------------ADD FIGURE
            FIGURE.Rule             = addFigure + TparA + FIGURAS + TparC + PuntoComa;

            FIGURAS.Rule            = circle + TparA + L_PARAM + TparC
                                    | triangle + TparA + L_PARAM + TparC
                                    | square + TparA + L_PARAM + TparC
                                    | line + TparA + L_PARAM + TparC;
            //--------------------------------------------FUCION NATIVA FIGURE
            ADD_FIGURE.Rule         = figure + TparA + EXPRESION + TparC + PuntoComa;


            #endregion


            #region Preferencias
            this.Root = S;
            #endregion
        }

    }
}
