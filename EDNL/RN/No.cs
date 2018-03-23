using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.RN
{
    public class No
    {
        public enum CorRubroNegra
        {
            Rubro = 0,
            Negro = 1
        }

        public int Valor {get;set;}
        public No Pai { get; set; }
        public No FilhoEsquerdo { get; set; }
        public No FilhoDireito { get; set; }
        public CorRubroNegra Cor { get; set; }
        public bool Duplo { get; set; }

        public No()
        {
        }

        public No(No pai, int valor)
        {
            this.Pai = pai;
            this.Valor = valor;
            this.Cor = CorRubroNegra.Rubro;
            this.Duplo = false;
        }

        public No(int valor)
        {
            this.Pai = null;
            this.Valor = valor;
            this.Cor = CorRubroNegra.Rubro;
            this.Duplo = false;
        }

        public No Irmao
        {
            get
            {
                if (this.EFilhoDireito())
                    return Pai.FilhoEsquerdo;
                else
                    return Pai.FilhoDireito;
            }
        }

        public bool ERaiz()
        {
            return this.Pai == null;
        }

        public bool TemFilhoEsquerdo()
        {
            return this.FilhoEsquerdo != null;
        }

        public bool TemFilhoDireito()
        {
            return this.FilhoDireito != null;
        }

        public bool EInterno()
        {
            return this.FilhoEsquerdo != null || this.FilhoDireito != null;
        }

        public bool EExterno()
        {
            return this.FilhoDireito == null && this.FilhoEsquerdo == null;
        }

        public bool EFilhoEsquerdo()
        {
            return this.Valor <= this.Pai.Valor;
        }

        public bool EFilhoDireito()
        {
            return this.Valor >= this.Pai.Valor;
        }
    }
}
