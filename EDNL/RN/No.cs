using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.RN
{
    public class No
    {
        public int Elemento {get;set;}
        public No Pai { get; set; }
        public No FilhoE { get; set; }
        public No FilhoD { get; set; }
        public string Cor { get; set; }
        public bool Duplo { get; set; }

        public No()
        {
        }

        public No(No pai, int elemento)
        {
            this.Pai = pai;
            this.Elemento = elemento;
            this.Cor = "Rubro";
            this.Duplo = false;
        }

        public No Irmao
        {
            get
            {
                if (this.ehFilhoD())
                    return Pai.FilhoE;
                else
                    return Pai.FilhoD;
            }
        }

        public bool isRoot()
        {
            return this.Pai == null;
        }

        public bool hasFilhoE()
        {
            return this.FilhoE != null;
        }

        public bool hasFilhoD()
        {
            return this.FilhoD != null;
        }

        public bool isInternal()
        {
            return this.FilhoE != null || this.FilhoD != null;
        }

        public bool isExternal()
        {
            return this.FilhoD == null && this.FilhoE == null;
        }

        public bool ehFilhoE()
        {
            return this.Elemento <= this.Pai.Elemento;
        }

        public bool ehFilhoD()
        {
            return this.Elemento >= this.Pai.Elemento;
        }
    }
}
