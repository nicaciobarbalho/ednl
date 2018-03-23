using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.RN
{
    public class Rotacao
    {
        private No raiz;

        public delegate void EventMensagem(No no, string mensagem);
        public event EventMensagem Mensagem;
        public No Raiz
        {
            get
            {
                return raiz;
            }
            set
            {
                raiz = value;
                raiz.Cor = No.CorRubroNegra.Negro;
            }
        }

        protected No Rotacionar(No no)
        {
            No retorno = null;
            No avo = no.Pai.Pai;
            No pai = no.Pai;

            //Rotacao simples a direita
            if (no.EFilhoEsquerdo() && pai.EFilhoEsquerdo())
            {
                retorno = no.Pai;
                avo.Cor = No.CorRubroNegra.Rubro;
                pai.Cor = No.CorRubroNegra.Negro;

                RotacaoSimplesDireita(avo);
                this.OnMensagem(no, "Rotacao simples a direita");
            }
            //Rotacao simples a esquerda
            else if (no.EFilhoDireito() && pai.EFilhoDireito())
            {
                retorno = no.Pai;
                avo.Cor = No.CorRubroNegra.Rubro;
                pai.Cor = No.CorRubroNegra.Negro;

                RotacaoSimplesEsquerda(avo);
                this.OnMensagem(no, "Rotacao simples a esquerda");
            }
            //Rotacao dupla esquerda
            else if (pai.EFilhoDireito())
            {
                retorno = no;
                avo.Cor = No.CorRubroNegra.Rubro;
                no.Cor = No.CorRubroNegra.Negro;

                RotacaoDuplaEsquerda(avo);
                this.OnMensagem(no, "Rotacao dupla esquerda");
            }
            //Rotacao dupla direita
            else
            {
                retorno = no;
                avo.Cor = No.CorRubroNegra.Rubro;
                no.Cor = No.CorRubroNegra.Negro;

                RotacaoDuplaDireita(avo);
                this.OnMensagem(no, "Rotacao dupla direita");
            }

            return retorno;
        }

        public void RotacaoSimplesEsquerda(No no)
        {
            Console.WriteLine("Rotacao Simples Esquerda " + no.Valor);

            No netoE = null;

            //se necessario, atualiza a raiz
            if (no.ERaiz())
                raiz = no.FilhoDireito;

            //guarda o netoE e atualiza suas referencia para o pai
            if (no.FilhoDireito.FilhoEsquerdo != null)
            {
                netoE = no.FilhoDireito.FilhoEsquerdo;
                netoE.Pai = no;
            }

            //Atualiza as referencias do filho direito do no
            no.FilhoDireito.Pai = no;
            no.FilhoDireito.FilhoEsquerdo = no;

            //Atualiza as referencias do pai do no se existir
            if (no.Pai != null)
            {
                if (no.Valor > no.Pai.Valor)
                    no.Pai.FilhoDireito = no.FilhoDireito;
                else
                    no.Pai.FilhoEsquerdo = no.FilhoDireito;
            }

            //Atualiza as referencias do no
            no.Pai = no;
            //if(netoE != null)
            no.FilhoDireito = netoE;
            //else
            //   no.FilhoD = null;

            //exibirArvore(raiz);
        }

        public void RotacaoSimplesDireita(No no)
        {
            Console.WriteLine("Rotacao Simples Direita " + no.Valor);

            No netoD = null;

            //se necessario, atuliza a raiz
            if (no.ERaiz())
                raiz = no.FilhoEsquerdo;

            //guarda o netoD 
            if (no.FilhoEsquerdo.FilhoDireito != null)
            {
                netoD = no.FilhoEsquerdo.FilhoDireito;
                netoD.Pai = no;
            }

            //Atualiza as referencias do filho esquerdo do no
            no.FilhoEsquerdo.Pai = no;
            no.FilhoEsquerdo.FilhoDireito = no;

            //Atualiza as referencias do pai do no, se existir
            if (no.Pai != null)
            {
                if (no.Valor > no.Pai.Valor)
                    no.Pai.FilhoDireito = no.FilhoEsquerdo;
                else
                    no.Pai.FilhoEsquerdo = no.FilhoEsquerdo;
            }

            //Atualiza as referencias do no
            no.Pai = no;
            //if(netoD != null)
            no.FilhoEsquerdo = netoD;
            //else
            //    no.setFilhoE(null);

            //exibirArvore(raiz);
        }

        public void RotacaoDuplaEsquerda(No no)
        {
            RotacaoSimplesDireita(no.FilhoDireito);
            RotacaoSimplesEsquerda(no);
        }

        public void RotacaoDuplaDireita(No no)
        {
            RotacaoSimplesEsquerda(no.FilhoEsquerdo);
            RotacaoSimplesDireita(no);
        }

        protected void OnMensagem(No no, string mensagem)
        {
            EventMensagem handler = Mensagem;

            if (handler != null)
            {
                handler(no, mensagem);
            }
        }

        protected void OnMensagem(string mensagem)
        {
            EventMensagem handler = Mensagem;

            if (handler != null)
            {
                handler(null, mensagem);
            }
        }
    }
}
