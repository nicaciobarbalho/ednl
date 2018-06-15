using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.Grafo
{
    [Serializable]
    public class Vertice
    {
        private object chave;
        private object valor;
        private int grau;

        public Vertice(object valor)
        {
            this.Valor = valor;
        }

        public object Chave
        {
            get
            {
                return chave;
            }

            set
            {
                chave = value;
            }
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

        public int Grau
        {
            get
            {
                return grau;
            }

            set
            {
                grau = value;
            }
        }
        
    }
}
