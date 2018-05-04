using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.Grafo
{
    public class Vertice
    {
        private string chave;
        private string valor;
        private int grau;

        public Vertice(String valor)
        {
            this.Valor = valor;
        }

        public string Chave
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

        public string Valor
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
