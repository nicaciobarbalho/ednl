using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.Grafo
{
    [Serializable]
    public  class Ponto
    {

        private object chave;
        private object valor;

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

        public Ponto(object chave, object valor)
        {
            this.Chave = chave;
            this.Valor = valor;
        }
        

        public override string ToString()
        {
            return this.Chave.ToString();
        }
       
    }
}
