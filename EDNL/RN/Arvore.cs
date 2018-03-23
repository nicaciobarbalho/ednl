using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.RN
{
    public class Arvore : Rotacao
    {
        public Arvore(int chave)
        {
            this.Raiz = new RN.No(chave);
        }
        public Arvore()
        {
            this.Raiz = null;
        }

        private int qtd;

        public No Pesquisar(int chave)
        {
            return Pesquisar(this.Raiz, chave);
        }

        public No Pesquisar(No no, int chave)
        {
            //Verifica se o no não tem filho
            if (no.EExterno())
            {
                return no;
            }
            //Verifica se achou o no
            if (no.Valor == chave)
            {
                return no;
            }
            //Verifica se o no que procura esta do lado esquerdo
            else if ((int)chave < (int)no.Valor)
            {
                return Pesquisar(no.FilhoEsquerdo, chave);
            }
            //Verifica se o no que procura esta do lado direito
            else
            {
                return Pesquisar(no.FilhoDireito, chave);
            }
        }

        private No PesquisarPai(No no, int chave)
        {
            //Verifica se o no não tem filho
            if (no.EExterno())
            {
                return no;
            }
            //Verifica se o no que procura esta do lado esquerdo
            if ((int)chave < (int)no.Valor)
            {
                if (no.FilhoEsquerdo == null)
                    return no;
                else
                    return PesquisarPai(no.FilhoEsquerdo, chave);
            }
            //Verifica se o no que procura esta do lado direito
            else
            {
                if (no.FilhoDireito == null)
                    return no;
                else
                    return PesquisarPai(no.FilhoDireito, chave);
            }
        }

        private No AtualizarCores(No no)
        {
            No pai = no.Pai;
            if (pai.Cor.Equals(No.CorRubroNegra.Rubro))
            {
                No tio = null;
                //se pai é filho direito então tio é filho esquerdo de vô
                if (pai.EFilhoDireito())
                    tio = pai.Pai.FilhoEsquerdo;
                else
                    tio = pai.Pai.FilhoDireito;

                //tio é negro
                if (tio == null || tio.Cor.Equals(No.CorRubroNegra.Negro))
                {
                    base.OnMensagem(no, "Situação 3");
                    no = base.Rotacionar(no);
                }
                //se o tio é rubro
                else
                {
                    base.OnMensagem(no, "Situação 2");
                    tio.Cor = No.CorRubroNegra.Negro;
                    pai.Cor = No.CorRubroNegra.Negro;
                    if (!pai.Pai.ERaiz())
                    {
                        pai.Pai.Cor = No.CorRubroNegra.Rubro;
                        no = this.AtualizarCores(no.Pai.Pai);
                    }
                }
            }

            return no;
        }

        public No Sucessor(No no)
        {
            //Verifica se é folha ou se nao tem filho esquerdo --> encontrou o menor filho do no a remover
            if (no.EExterno() || no.FilhoEsquerdo == null)
                return no;
            //Continua a procura a apartir do filho esquerdo
            else
                return this.Sucessor(no.FilhoEsquerdo);
        }

        public No Incluir(int chave)
        {
            //Busca o pai do no que vai ser inserido
            No pai = this.PesquisarPai(this.Raiz, chave);
            //Cria o novo no
            No novo = new No(pai, chave);

            //Verifica se o novo no é filho esquerdo do pai buscado
            if ((int)pai.Valor > (int)novo.Valor)
                pai.FilhoEsquerdo = novo;
            //Verifica se o novo no é filho direito do pai buscado
            else
                pai.FilhoDireito = novo;
            qtd++;

            this.AtualizarCores(novo);

            return novo;
        }

        public object Remover(int chave)
        {
            if (this.Remover(this.Raiz, chave) != null)
                qtd--;
            return null;
        }

        public object Remover(No root, int chave)
        {
            //Busca a chave na arvore
            No r = this.Pesquisar(root, chave);
            //Visite(r);
            //Verifica se o no esta na arvore
            if (r != null && ((int)r.Valor == (int)chave))
            {
                //verifica se o no é folho
                if (r.EExterno())
                {
                    //No a ser removido é negro (sucessor negro) entao segue para a situação 3
                    if (r.Cor.Equals(No.CorRubroNegra.Negro))
                    {
                        this.Situacao3(r);
                    }
                    //No a ser removido é rubro então pode seguir normalmente

                    //Removendo...
                    //verifica se o no é o filho direito
                    if (r.EFilhoDireito())
                    {
                        r.Pai.FilhoDireito = null;
                        r.Pai = null;
                    }
                    //o no é filho esquerdo
                    else
                    {
                        r.Pai.FilhoEsquerdo = null;
                        r.Pai = null;
                    }
                }
                //Verifica se o no a remover tem 1 filho e é filho direito
                else if (r.FilhoDireito != null && r.FilhoEsquerdo == null)
                {
                    No sucessor = r.FilhoDireito;

                    //Sucessor é rubro
                    //Acontece nada de mais

                    //Sucessor é negro
                    if (sucessor.Cor.Equals(No.CorRubroNegra.Negro))
                    {
                        if (r.Cor.Equals(No.CorRubroNegra.Negro))
                        {
                            this.Situacao3(sucessor);
                        }
                        else
                        {
                            sucessor.Cor = No.CorRubroNegra.Rubro;
                            this.Situacao3(sucessor);
                        }
                    }

                    r.Valor = sucessor.Valor;

                    this.Remover(r.FilhoDireito, sucessor.Valor);
                }
                //Verifica se o no tem 1 filho e é filho esquerdo
                else if (r.FilhoEsquerdo != null && r.FilhoDireito == null)
                {
                    No sucessor = r.FilhoEsquerdo;

                    //Sucessor é rubro
                    //Acontece nada de mais

                    //Sucessor é negro
                    if (sucessor.Cor.Equals(No.CorRubroNegra.Negro))
                    {
                        if (r.Cor.Equals(No.CorRubroNegra.Negro))
                        {
                            this.Situacao3(sucessor);
                        }
                        else
                        {
                            sucessor.Cor = No.CorRubroNegra.Rubro;
                            this.Situacao3(sucessor);
                        }
                    }
                    r.Valor = sucessor.Valor;
                    this.Remover(r.FilhoEsquerdo, sucessor.Valor);
                }
                //O no a remover tem 2 filhos
                else
                {
                    //Acha o sucessor
                    No herdeiro = this.Sucessor(r.FilhoDireito);

                    //Sucessor é rubro
                    //Acontece nada de mais
                    //Sucessor é negro
                    if (herdeiro.Cor.Equals(No.CorRubroNegra.Negro))
                    {
                        //no negro e sucessor negro
                        //situação 3
                        if (r.Cor.Equals(No.CorRubroNegra.Negro))
                        {
                            this.Situacao3(herdeiro);
                            base.OnMensagem("Situação 3");
                        }
                        //no rubro e sucessor negro
                        //situação 4
                        else
                        {
                            herdeiro.Cor = No.CorRubroNegra.Rubro;
                            this.Situacao3(herdeiro);
                            base.OnMensagem("Situação 4");
                        }
                    }

                    r.Valor = herdeiro.Valor;

                    this.Remover(r.FilhoDireito, herdeiro.Valor);

                }
                return r;
            }
            return null;
        }

        public void Situacao3(No no)
        {
            //caso 1
            if (no.Irmao.Cor.Equals(No.CorRubroNegra.Rubro))
            {
                base.OnMensagem(no, "Situação: Caso 1");
                if (no.Irmao.EFilhoDireito())
                {
                    no.Irmao.Cor = No.CorRubroNegra.Negro;
                    if (!no.Pai.ERaiz())
                        no.Pai.Cor = No.CorRubroNegra.Rubro;

                    base.RotacaoSimplesEsquerda(no.Pai);
                    this.Situacao3(no);
                }
            }
            //caso 2B
            else if (no.Pai.Cor.Equals(No.CorRubroNegra.Rubro))
            {
                base.OnMensagem(no, "Situação: Caso 2B");
                no.Pai.Cor = No.CorRubroNegra.Negro;
                no.Irmao.Cor = No.CorRubroNegra.Rubro;
            }
            //caso 3 para no esquerdo
            else if (no.EFilhoEsquerdo() && no.Irmao.FilhoEsquerdo.Cor.Equals(No.CorRubroNegra.Rubro) && no.Irmao.FilhoDireito.Cor.Equals(No.CorRubroNegra.Negro))
            {
                base.OnMensagem(no, "Situação: Caso 3(Esquerdo)");
                no.Irmao.FilhoEsquerdo.Cor = No.CorRubroNegra.Negro;
                no.Irmao.Cor = No.CorRubroNegra.Rubro;
                base.RotacaoSimplesDireita(no.Irmao);
                this.Situacao3(no);
            }
            //caso 3 para no direito (espelhado)
            else if (no.EFilhoEsquerdo() && no.Irmao.FilhoDireito.Cor.Equals(No.CorRubroNegra.Rubro) && no.Irmao.FilhoEsquerdo.Cor.Equals(No.CorRubroNegra.Negro))
            {
                base.OnMensagem(no, "Situação: Caso 3(Direito)");
                no.Irmao.FilhoDireito.Cor = No.CorRubroNegra.Negro;
                no.Irmao.Cor = No.CorRubroNegra.Rubro;
                base.RotacaoSimplesEsquerda(no.Irmao);
                this.Situacao3(no);
            }
            //caso 4 para no esquerdo
            else if (no.EFilhoEsquerdo() && no.Irmao.FilhoDireito.Cor.Equals(No.CorRubroNegra.Rubro))
            {
                base.OnMensagem(no, "Situação: Caso 4(Esquerdo)");
                no.Irmao.Cor = no.Pai.Cor;
                no.Pai.Cor = No.CorRubroNegra.Negro;
                no.Irmao.FilhoDireito.Cor = No.CorRubroNegra.Negro;
                base.RotacaoSimplesEsquerda(no.Pai);
            }
            //caso 4 para no direito (espelhado)
            else if (no.EFilhoDireito() && no.Irmao.FilhoEsquerdo.Cor.Equals(No.CorRubroNegra.Rubro))
            {
                base.OnMensagem(no, "Situação: Caso 4(Direito)");
                no.Irmao.Cor = no.Pai.Cor;
                no.Pai.Cor = No.CorRubroNegra.Negro;
                no.Irmao.FilhoEsquerdo.Cor = No.CorRubroNegra.Negro;
                base.RotacaoSimplesDireita(no.Pai);
            }
            //caso 2A
            else
            {
                base.OnMensagem(no, "Situação: Caso 2A");
                no.Irmao.Cor = No.CorRubroNegra.Rubro;
                this.Situacao3(no.Pai);
            }
        }

    }
}
