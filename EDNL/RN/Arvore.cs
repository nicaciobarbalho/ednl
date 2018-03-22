using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.RN
{
    public class Arvore
    {
        private No raiz;
        private int qtd;

        public void setRaiz(No no)
        {
            raiz = no;
            raiz.Cor = "Negro";
        }

        public No Raiz()
        {
            return raiz;
        }

        public No Pesquisar(int chave)
        {
            return Pesquisar(raiz, chave);
        }

        public No Pesquisar(No no, int chave)
        {
            //Verifica se o no não tem filho
            if (no.isExternal())
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
            else//((int)key > (int)no.Elemento)
            {
                return Pesquisar(no.FilhoDireito, chave);
            }
        }

        private No buscarPai(No no, int key)
        {
            //Verifica se o no não tem filho
            if (no.isExternal())
            {
                return no;
            }
            //Verifica se o no que procura esta do lado esquerdo
            if ((int)key < (int)no.Valor)
            {
                if (no.FilhoEsquerdo == null)
                    return no;
                else
                    return buscarPai(no.FilhoEsquerdo, key);
            }
            //Verifica se o no que procura esta do lado direito
            else
            {
                if (no.FilhoDireito == null)
                    return no;
                else
                    return buscarPai(no.FilhoDireito, key);
            }
        }

        public No incluir(int key)
        {
            //Busca o pai do no que vai ser inserido
            No pai = buscarPai(raiz, key);
            //Cria o novo no
            No novo = new No(pai, key);

            //Verifica se o novo no eh filho esquerdo do pai buscado
            if ((int)pai.Valor > (int)novo.Valor)
                pai.FilhoEsquerdo = novo;
            //Verifica se o novo no eh filho direito do pai buscado
            else
                pai.FilhoDireito = novo;
            qtd++;

            AtualizarCores(novo);

            return novo;
        }

        private No AtualizarCores(No no)
        {
            No pai = no.Pai;
            if (pai.Cor.Equals("Rubro"))
            {
                No tio = null;
                //se pai é filho direito então tio é filho esquerdo de vô
                if (pai.ehFilhoD())
                    tio = pai.Pai.FilhoEsquerdo;
                else
                    tio = pai.Pai.FilhoDireito;

                //tio eh negro
                if (tio == null || tio.Cor.Equals("Negro"))
                {
                    Console.WriteLine("Incluir: Situação 3 no nó: " + no.Valor);

                    no = Rotacionar(no);
                }
                //se o tio eh rubro
                else
                {
                    Console.WriteLine("Incluir: Situação 2 no nó:" + no.Valor);
                    tio.Cor = "Negro";
                    pai.Cor = "Negro";
                    if (!pai.Pai.ERaiz())
                    {
                        pai.Pai.Cor = "Rubro";
                        no = AtualizarCores(no.Pai.Pai);
                    }
                }
            }

            return no;
        }

        private No Rotacionar(No no)
        {
            No retorno = null;
            No avo = no.Pai.Pai;
            No pai = no.Pai;

            //Rotacao simples a direita
            if (no.ehFilhoE() && pai.ehFilhoE())
            {
                retorno = no.Pai;
                avo.Cor = "Rubro";
                pai.Cor = "Negro";

                RotacaoSimplesDireita(avo);
            }
            //Rotacao simples a esquerda
            else if (no.ehFilhoD() && pai.ehFilhoD())
            {
                retorno = no.Pai;
                avo.Cor = "Rubro";
                pai.Cor = "Negro";

                RotacaoSimplesEsquerda(avo);

            }
            //Rotacao dupla esquerda
            else if (pai.ehFilhoD())
            {
                retorno = no;
                avo.Cor = "Rubro";
                no.Cor = "Negro";

                RotacaoDuplaEsquerda(avo);
            }
            //Rotacao dupla direita
            else
            {
                retorno = no;
                avo.Cor = "Rubro";
                no.Cor = "Negro";

                RotacaoDuplaDireita(avo);
            }

            return retorno;
        }

        public No Sucessor(No no)
        {
            //Verifica se eh folha ou se nao tem filho esquerdo --> encontrou o menor filho do no a remover
            if (no.isExternal() || no.FilhoEsquerdo == null)
                return no;
            //Continua a procura a apartir do filho esquerdo
            else
                return Sucessor(no.FilhoEsquerdo);
        }

        public Object remover(int key)
        {
            if (remover(raiz, key) != null)
                qtd--;
            return null;
        }

        public Object remover(No root, int key)
        {
            //Busca a chave na arvore
            No r = Pesquisar(root, key);
            //Visite(r);
            //Verifica se o no esta na arvore
            if (r != null && ((int)r.Valor == (int)key))
            {
                //verifica se o no eh folha
                //Console.WriteLine("oii");
                if (r.isExternal())
                {
                    //Console.WriteLine("e aqui");
                    //No a ser removido eh negro (sucessor negro) entao segue para a situação 3
                    if (r.Cor.Equals("Negro"))
                    {
                        Situacao3(r);
                    }
                    //No a ser removido eh rubro então pode seguir normalmente

                    //Removendo...
                    //verifica se o no eh o filho direito
                    if (r.ehFilhoD())
                    {
                        //Console.WriteLine("aqui");
                        r.Pai.FilhoDireito = null;
                        r.Pai = null;
                    }
                    //o no eh filho esquerdo
                    else
                    {
                        //Console.WriteLine("nao eh aqui");
                        r.Pai.FilhoEsquerdo = null;
                        r.Pai = null;
                    }
                }
                //Verifica se o no a remover tem 1 filho e eh filho direito
                else if (r.FilhoDireito != null && r.FilhoEsquerdo == null)
                {
                    No sucessor = r.FilhoDireito;

                    //Sucessor eh rubro
                    //Acontece nada de mais

                    //Sucessor eh negro
                    if (sucessor.Cor.Equals("Negro"))
                    {
                        if (r.Cor.Equals("Negro"))
                        {
                            Situacao3(sucessor);
                        }
                        else
                        {
                            sucessor.Cor = "Rubro";
                            Situacao3(sucessor);
                        }
                    }

                    r.Valor = sucessor.Valor;
                    //sucessor.Elemento = key);

                    remover(r.FilhoDireito, sucessor.Valor);
                }
                //Verifica se o no tem 1 filho e eh filho esquerdo
                else if (r.FilhoEsquerdo != null && r.FilhoDireito == null)
                {
                    No sucessor = r.FilhoEsquerdo;

                    //Sucessor eh rubro
                    //Acontece nada de mais

                    //Sucessor eh negro
                    if (sucessor.Cor.Equals("Negro"))
                    {
                        if (r.Cor.Equals("Negro"))
                        {
                            Situacao3(sucessor);
                        }
                        else
                        {
                            sucessor.Cor = "Rubro";
                            Situacao3(sucessor);
                        }
                    }
                    r.Valor = sucessor.Valor;
                    remover(r.FilhoEsquerdo, sucessor.Valor);
                }
                //O no a remover tem 2 filhos
                else
                {
                    //Acha o sucessor
                    No herdeiro = Sucessor(r.FilhoDireito);

                    //Sucessor eh rubro
                    //Acontece nada de mais
                    //Sucessor eh negro
                    if (herdeiro.Cor.Equals("Negro"))
                    {
                        //no negro e sucessor negro
                        //situação 3
                        if (r.Cor.Equals("Negro"))
                        {
                            Situacao3(herdeiro);
                        }
                        //no rubro e sucessor negro
                        //situação 4
                        else
                        {
                            Console.WriteLine("Situação 4");
                            herdeiro.Cor = "Rubro";
                            Situacao3(herdeiro);
                        }
                    }

                    r.Valor = herdeiro.Valor;

                    remover(r.FilhoDireito, herdeiro.Valor);

                }
                return r;
            }
            return null;
        }

        public void Situacao3(No no)
        {
            //caso 1
            if (no.Irmao.Cor.Equals("Rubro"))
            {
                Console.WriteLine("Caso 1");
                if (no.Irmao.ehFilhoD())
                {
                    no.Irmao.Cor = "Negro";
                    if (!no.Pai.ERaiz())
                        no.Pai.Cor = "Rubro";

                    RotacaoSimplesEsquerda(no.Pai);
                    Situacao3(no);
                }
            }
            //caso 2B
            else if (no.Pai.Cor.Equals("Rubro"))
            {
                Console.WriteLine("Caso 2B");
                no.Pai.Cor = "Negro";
                no.Irmao.Cor = "Rubro";
            }
            //caso 3 para no esquerdo
            else if (no.ehFilhoE() && no.Irmao.FilhoEsquerdo.Cor.Equals("Rubro") && no.Irmao.FilhoDireito.Cor.Equals("Negro"))
            {
                Console.WriteLine("Caso 3");
                no.Irmao.FilhoEsquerdo.Cor = "Negro";
                no.Irmao.Cor = "Rubro";
                RotacaoSimplesDireita(no.Irmao);
                Situacao3(no);
            }
            //caso 3 para no direito (espelhado)
            else if (no.ehFilhoE() && no.Irmao.FilhoDireito.Cor.Equals("Rubro") && no.Irmao.FilhoEsquerdo.Cor.Equals("Negro"))
            {
                Console.WriteLine("Caso 3'");
                no.Irmao.FilhoDireito.Cor = "Negro";
                no.Irmao.Cor = "Rubro";
                RotacaoSimplesEsquerda(no.Irmao);
                Situacao3(no);
            }
            //caso 4 para no esquerdo
            else if (no.ehFilhoE() && no.Irmao.FilhoDireito.Cor.Equals("Rubro"))
            {
                Console.WriteLine("Caso 4");
                no.Irmao.Cor = no.Pai.Cor;
                no.Pai.Cor = "Negro";
                no.Irmao.FilhoDireito.Cor = "Negro";
                RotacaoSimplesEsquerda(no.Pai);
            }
            //caso 4 para no direito (espelhado)
            else if (no.ehFilhoD() && no.Irmao.FilhoEsquerdo.Cor.Equals("Rubro"))
            {
                Console.WriteLine("Caso 4_");
                no.Irmao.Cor = no.Pai.Cor;
                no.Pai.Cor = "Negro";
                no.Irmao.FilhoEsquerdo.Cor = "Negro";
                RotacaoSimplesDireita(no.Pai);
            }
            //caso 2A
            else
            {
                Console.WriteLine("Caso 2A");
                no.Irmao.Cor = "Rubro";
                Situacao3(no.Pai);
            }
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

        //Metodo que mostra as caracteristicas do no *OBS: Faz a verificações para não dar erro de referencia nula
        public void Visite(No n)
        {
            Console.WriteLine("Elemento:" + n.Valor + " Cor: " + n.Cor);
            if (!n.ERaiz())
                Console.WriteLine(" Pai:" + n.Pai.Valor);

            if (n.FilhoEsquerdo != null)
                Console.WriteLine(" FilhoE:" + n.FilhoEsquerdo.Valor);

            if (n.FilhoDireito != null)
                Console.WriteLine(" FilhoD:" + n.FilhoDireito.Valor);

            Console.WriteLine();
        }

        //Metodo para visualizar a arvore --> Algoritmo InOrder
        public void exibirArvore(No n)
        {
            if (n.isInternal() && n.FilhoEsquerdo != null)
            {
                exibirArvore(n.FilhoEsquerdo);
            }

            Visite(n);

            if (n.isInternal() && n.FilhoDireito != null)
            {
                exibirArvore(n.FilhoDireito);
            }
        }
    }
}
