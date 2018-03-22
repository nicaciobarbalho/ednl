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
            Negra = 1
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
                if (this.ehFilhoD())
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

        public bool isInternal()
        {
            return this.FilhoEsquerdo != null || this.FilhoDireito != null;
        }

        public bool isExternal()
        {
            return this.FilhoDireito == null && this.FilhoEsquerdo == null;
        }

        public bool ehFilhoE()
        {
            return this.Valor <= this.Pai.Valor;
        }

        public bool ehFilhoD()
        {
            return this.Valor >= this.Pai.Valor;
        }
    }
}
