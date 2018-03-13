using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.AVL
{
    public abstract class Rotacao
    {
        public No Raiz { get; set; }

        protected void EsquerdaSimples(int chave)
        {
            No no = this.Obter(chave);
            if (no != null) EsquerdaSimples(no);
        }

        public No EsquerdaSimples(No no)
        {
            No direito = no.Direito;
            No pai = no.Pai;
            No novoDireito = direito.Esquerdo;

            if (pai != null)
            {
                if (no.Chave > pai.Chave)
                    pai.Direito = direito;
                else
                    pai.Esquerdo = direito;
            }
            else
            {
                this.Raiz = direito;
            }
            direito.Pai = pai;
            direito.Esquerdo = no;

            no.Pai = direito;
            no.Direito = novoDireito;

            if (novoDireito != null)
            {
                novoDireito.Pai = no;
            }

            no.FatorBalanceamento = (no.FatorBalanceamento + 1 - Math.Min(direito.FatorBalanceamento, 0));
            direito.FatorBalanceamento = (direito.FatorBalanceamento + 1 + Math.Max(no.FatorBalanceamento, 0));

            return no;
        }

        protected void DireitaSimples(int chave)
        {
            No no = this.Obter(chave);
            if (no != null) DireitaSimples(no);
        }

        protected No DireitaSimples(No no)
        {
            No esquerdo = no.Esquerdo;
            No pai = no.Pai;
            No novoEsquerdo = esquerdo.Direito;

            if (pai != null)
            {
                if (no.Chave > pai.Chave)
                    pai.Direito = esquerdo;
                else
                    pai.Esquerdo = esquerdo;
            }
            else
            {
                this.Raiz = esquerdo;
            }
            esquerdo.Pai = pai;
            esquerdo.Direito = no;

            no.Pai = esquerdo;
            no.Esquerdo = novoEsquerdo;

            if (novoEsquerdo != null)
            {
                novoEsquerdo.Pai = no;
            }

            no.FatorBalanceamento = (no.FatorBalanceamento - 1 - Math.Max(esquerdo.FatorBalanceamento, 0));
            esquerdo.FatorBalanceamento = (esquerdo.FatorBalanceamento - 1 + Math.Min(no.FatorBalanceamento, 0));

            return no;
        }

        protected void EsquerdaDupla(int chave)
        {
            No no = this.Obter(chave);
            if (no != null) EsquerdaDupla(no);
        }

        protected No EsquerdaDupla(No no)
        {
            No d = no.Direito;

            this.DireitaSimples(d);
            this.EsquerdaSimples(no);

            return no;
        }

        protected void DireitaDupla(int chave)
        {
            No no = this.Obter(chave);
            if (no != null) DireitaDupla(no);
        }

        protected No DireitaDupla(No no)
        {
            No e = no.Esquerdo;

            this.EsquerdaSimples(e);
            this.DireitaSimples(no);

            return no;
        }

        protected abstract No Obter(int chave);
        protected abstract No Obter(int chave, No no);
    }
}
