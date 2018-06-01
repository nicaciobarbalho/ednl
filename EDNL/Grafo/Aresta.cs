using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.Grafo
{
    public class Aresta
    {
        private Vertice v;  
        private Vertice w;
        private bool orientado;
        private object valor;

        public Aresta(Vertice v, Vertice w)
        {
            this.V = v;
            this.W = w;
            this.Orientado = false;
        }

        public Aresta(Vertice v, Vertice w, object valor)
        {
            this.V = v;
            this.W = w;
            this.valor = valor;
            this.Orientado = false;
        }
        public Aresta(Vertice v, Vertice w, bool orientado)
        {
            this.V = v;
            this.W = w;
            this.Orientado = orientado;
        }

        public object Valor
        {
            get
            {
                return valor;
            }

            set
            {
                valor = value;
            }
        }

        public Vertice V
        {
            get
            {
                return v;
            }

            set
            {
                v = value;
            }
        }

        public Vertice W
        {
            get
            {
                return w;
            }

            set
            {
                w = value;
            }
        }

        public bool Orientado
        {
            get
            {
                return orientado;
            }

            set
            {
                orientado = value;
            }
        }

        public bool HasVertice(Vertice x)
        {
            return V == x || W == x;
        }
    }
}
