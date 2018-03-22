using System;
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

        public No getRaiz()
        {
            return raiz;
        }

        public No find(int key)
        {
            return pesquisar(raiz, key);
        }

        public No pesquisar(No no, int key)
        {
            //Verifica se o no não tem filho
            if (no.isExternal())
            {
                return no;
            }
            //Verifica se achou o no
            if (no.Elemento == key)
            {
                return no;
            }
            //Verifica se o no que procura esta do lado esquerdo
            else if ((int)key < (int)no.Elemento)
            {
                return pesquisar(no.FilhoE, key);
            }
            //Verifica se o no que procura esta do lado direito
            else//((int)key > (int)no.Elemento)
            {
                return pesquisar(no.FilhoD, key);
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
            if ((int)key < (int)no.Elemento)
            {
                if (no.FilhoE == null)
                    return no;
                else
                    return buscarPai(no.FilhoE, key);
            }
            //Verifica se o no que procura esta do lado direito
            else
            {
                if (no.FilhoD == null)
                    return no;
                else
                    return buscarPai(no.FilhoD, key);
            }
        }

        public No incluir(int key)
        {
            //Busca o pai do no que vai ser inserido
            No pai = buscarPai(raiz, key);
            //Cria o novo no
            No novo = new No(pai, key);

            //Verifica se o novo no eh filho esquerdo do pai buscado
            if ((int)pai.Elemento > (int)novo.Elemento)
                pai.FilhoE = novo;
            //Verifica se o novo no eh filho direito do pai buscado
            else
                pai.FilhoD = novo;
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
                    tio = pai.Pai.FilhoE;
                else
                    tio = pai.Pai.FilhoD;

                //tio eh negro
                if (tio == null || tio.Cor.Equals("Negro"))
                {
                    Console.WriteLine("Incluir: Situação 3 no nó: " + no.Elemento);

                    no = Rotacionar(no);
                }
                //se o tio eh rubro
                else
                {
                    Console.WriteLine("Incluir: Situação 2 no nó:" + no.Elemento);
                    tio.Cor = "Negro";
                    pai.Cor = "Negro";
                    if (!pai.Pai.isRoot())
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
            if (no.isExternal() || no.FilhoE == null)
                return no;
            //Continua a procura a apartir do filho esquerdo
            else
                return Sucessor(no.FilhoE);
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
            No r = pesquisar(root, key);
            //Visite(r);
            //Verifica se o no esta na arvore
            if (r != null && ((int)r.Elemento == (int)key))
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
                        r.Pai.FilhoD = null;
                        r.Pai = null;
                    }
                    //o no eh filho esquerdo
                    else
                    {
                        //Console.WriteLine("nao eh aqui");
                        r.Pai.FilhoE = null;
                        r.Pai = null;
                    }
                }
                //Verifica se o no a remover tem 1 filho e eh filho direito
                else if (r.FilhoD != null && r.FilhoE == null)
                {
                    No sucessor = r.FilhoD;

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

                    r.Elemento = sucessor.Elemento;
                    //sucessor.Elemento = key);

                    remover(r.FilhoD, sucessor.Elemento);
                }
                //Verifica se o no tem 1 filho e eh filho esquerdo
                else if (r.FilhoE != null && r.FilhoD == null)
                {
                    No sucessor = r.FilhoE;

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
                    r.Elemento = sucessor.Elemento;
                    remover(r.FilhoE, sucessor.Elemento);
                }
                //O no a remover tem 2 filhos
                else
                {
                    //Acha o sucessor
                    No herdeiro = Sucessor(r.FilhoD);

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

                    r.Elemento = herdeiro.Elemento;

                    remover(r.FilhoD, herdeiro.Elemento);

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
                    if (!no.Pai.isRoot())
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
            else if (no.ehFilhoE() && no.Irmao.FilhoE.Cor.Equals("Rubro") && no.Irmao.FilhoD.Cor.Equals("Negro"))
            {
                Console.WriteLine("Caso 3");
                no.Irmao.FilhoE.Cor = "Negro";
                no.Irmao.Cor = "Rubro";
                RotacaoSimplesDireita(no.Irmao);
                Situacao3(no);
            }
            //caso 3 para no direito (espelhado)
            else if (no.ehFilhoE() && no.Irmao.FilhoD.Cor.Equals("Rubro") && no.Irmao.FilhoE.Cor.Equals("Negro"))
            {
                Console.WriteLine("Caso 3'");
                no.Irmao.FilhoD.Cor = "Negro";
                no.Irmao.Cor = "Rubro";
                RotacaoSimplesEsquerda(no.Irmao);
                Situacao3(no);
            }
            //caso 4 para no esquerdo
            else if (no.ehFilhoE() && no.Irmao.FilhoD.Cor.Equals("Rubro"))
            {
                Console.WriteLine("Caso 4");
                no.Irmao.Cor = no.Pai.Cor;
                no.Pai.Cor = "Negro";
                no.Irmao.FilhoD.Cor = "Negro";
                RotacaoSimplesEsquerda(no.Pai);
            }
            //caso 4 para no direito (espelhado)
            else if (no.ehFilhoD() && no.Irmao.FilhoE.Cor.Equals("Rubro"))
            {
                Console.WriteLine("Caso 4_");
                no.Irmao.Cor = no.Pai.Cor;
                no.Pai.Cor = "Negro";
                no.Irmao.FilhoE.Cor = "Negro";
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
            Console.WriteLine("Rotacao Simples Esquerda " + no.Elemento);

            No netoE = null;

            //se necessario, atualiza a raiz
            if (no.isRoot())
                raiz = no.FilhoD;

            //guarda o netoE e atualiza suas referencia para o pai
            if (no.FilhoD.FilhoE != null)
            {
                netoE = no.FilhoD.FilhoE;
                netoE.Pai = no;
            }

            //Atualiza as referencias do filho direito do no
            no.FilhoD.Pai = no;
            no.FilhoD.FilhoE = no;

            //Atualiza as referencias do pai do no se existir
            if (no.Pai != null)
            {
                if (no.Elemento > no.Pai.Elemento)
                    no.Pai.FilhoD = no.FilhoD;
                else
                    no.Pai.FilhoE = no.FilhoD;
            }

            //Atualiza as referencias do no
            no.Pai = no;
            //if(netoE != null)
            no.FilhoD = netoE;
            //else
            //   no.FilhoD = null;

            //exibirArvore(raiz);
        }

        public void RotacaoSimplesDireita(No no)
        {
            Console.WriteLine("Rotacao Simples Direita " + no.Elemento);

            No netoD = null;

            //se necessario, atuliza a raiz
            if (no.isRoot())
                raiz = no.FilhoE;

            //guarda o netoD 
            if (no.FilhoE.FilhoD != null)
            {
                netoD = no.FilhoE.FilhoD;
                netoD.Pai = no;
            }

            //Atualiza as referencias do filho esquerdo do no
            no.FilhoE.Pai = no;
            no.FilhoE.FilhoD = no;

            //Atualiza as referencias do pai do no, se existir
            if (no.Pai != null)
            {
                if (no.Elemento > no.Pai.Elemento)
                    no.Pai.FilhoD = no.FilhoE;
                else
                    no.Pai.FilhoE = no.FilhoE;
            }

            //Atualiza as referencias do no
            no.Pai = no;
            //if(netoD != null)
            no.FilhoE = netoD;
            //else
            //    no.setFilhoE(null);

            //exibirArvore(raiz);
        }

        public void RotacaoDuplaEsquerda(No no)
        {
            RotacaoSimplesDireita(no.FilhoD);
            RotacaoSimplesEsquerda(no);
        }

        public void RotacaoDuplaDireita(No no)
        {
            RotacaoSimplesEsquerda(no.FilhoE);
            RotacaoSimplesDireita(no);
        }

        //Metodo que mostra as caracteristicas do no *OBS: Faz a verificações para não dar erro de referencia nula
        public void Visite(No n)
        {
            Console.WriteLine("Elemento:" + n.Elemento + " Cor: " + n.Cor);
            if (!n.isRoot())
                Console.WriteLine(" Pai:" + n.Pai.Elemento);

            if (n.FilhoE != null)
                Console.WriteLine(" FilhoE:" + n.FilhoE.Elemento);

            if (n.FilhoD != null)
                Console.WriteLine(" FilhoD:" + n.FilhoD.Elemento);

            Console.WriteLine();
        }

        //Metodo para visualizar a arvore --> Algoritmo InOrder
        public void exibirArvore(No n)
        {
            if (n.isInternal() && n.FilhoE != null)
            {
                exibirArvore(n.FilhoE);
            }

            Visite(n);

            if (n.isInternal() && n.FilhoD != null)
            {
                exibirArvore(n.FilhoD);
            }
        }
    }
}
