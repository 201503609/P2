using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC2_Proyecto2.Gramatica
{
    class TablaSimbolos
    {
        public string tipo1, id, tipo, ambito, valor, fila, col;

        public static ArrayList tablaSimbolo = new ArrayList();

        public TablaSimbolos(string ti1, string id, string tip,string amb, string val, string fi , string co)
        {
            this.tipo1 = ti1;
            this.id = id;
            this.tipo = tip;
            this.ambito = amb;
            this.valor = val;
            this.fila = fi;
            this.col = co;
        }

        public static void InsertarTS(TablaSimbolos simbolo)
        {
            tablaSimbolo.Add(simbolo);
        }

        public static void pasarTabla(Ambito actual)
        {
            foreach (Variables hijo in actual.variableAmbito)
            {
                TablaSimbolos t = new TablaSimbolos("variable",hijo.nombre,hijo.tipo,hijo.ambito,hijo.valor,"0","0");
                InsertarTS(t);
            }
            foreach (Arreglos hijo in actual.arreglosAmbito)
            {
                TablaSimbolos t = new TablaSimbolos("arreglo", hijo.nombre, hijo.tipo, hijo.ambito,hijo.cantidadDatos.ToString(), "0", "0");
                InsertarTS(t);
            }
            //foreach (Variables hijo in actual.variableAmbito)
            //{
            //    TablaSimbolos t = new TablaSimbolos("variable", hijo.nombre, hijo.tipo, hijo.ambito, hijo.valor, "0", "0");
            //    InsertarTS(t);
            //}

        }


    }

}
