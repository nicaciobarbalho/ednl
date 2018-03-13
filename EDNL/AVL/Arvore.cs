using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.AVL
{
    public class Arvore : Rotacao
    {
        public enum Operacao
        {
            Inserir = 0,
            Remover = 1
        }

        private int tamanho;

        public bool AtualizarAutomaticamente { get; set; }

        public Arvore()
        {
            this.tamanho = 0;
            this.AtualizarAutomaticamente = true;
        }

        protected override No Obter(int chave)
        {
            if (this.Raiz == null)
            {
                return this.Raiz;
            }
            return Obter(chave, this.Raiz);
        }

        protected override No Obter(int chave, No no)
        {
            if (no.EExterno())
            {
                return no;
            }
            if (chave < no.Chave)
            {
                return no.Esquerdo != null ? this.Obter(chave, no.Esquerdo) : no;
            }
            else if (chave == no.Chave)
            {
                return no;
            }
            else if (chave > no.Chave)
            {
                return no.Direito != null ? this.Obter(chave, no.Direito) : no;
            }
            return null;
        }

        bool EstaVazia()
        {
            return this.Raiz == null;
        }

        void AtualizarFatorBalanceamento(No no, Operacao operacao)
        {
            No pai = no.Pai;

            if (no == null || pai == null) return;

            if (operacao == Operacao.Inserir)
            {

                if (no.Chave > pai.Chave)
                {
                    pai.FatorBalanceamento = pai.FatorBalanceamento - 1;
                }
                else
                {
                    pai.FatorBalanceamento = pai.FatorBalanceamento + 1;
                }

                if (pai.FatorBalanceamento != 0)
                {
                    if (pai.FatorBalanceamento > 1 || pai.FatorBalanceamento < -1)
                    {
                        Balancear(pai);
                    }
                    else
                    {
                        AtualizarFatorBalanceamento(pai, operacao);
                    }
                }
            }
            else if (operacao == Operacao.Remover)
            {
                if (no.Chave > pai.Chave)
                {
                    pai.FatorBalanceamento = pai.FatorBalanceamento + 1;
                }
                else
                {
                    pai.FatorBalanceamento = pai.FatorBalanceamento - 1;
                }

                if ((pai.FatorBalanceamento == -2) || pai.FatorBalanceamento == 2)
                {
                    Balancear(pai);
                    AtualizarFatorBalanceamento(pai.Pai, operacao);
                }
                else if (pai.FatorBalanceamento == 0)
                {
                    AtualizarFatorBalanceamento(pai, operacao);
                }
            }
        }

        void Balancear(No no)
        {
            if (!this.AtualizarAutomaticamente) return;

            int fator = no.FatorBalanceamento;

            switch (fator)
            {
                case 2:
                    if (no.Esquerdo != null && no.Esquerdo.FatorBalanceamento < 0)
                    {
                        this.DireitaDupla(no);
                    }
                    else
                    {
                        this.DireitaSimples(no);
                    }
                    break;
                case -2:
                    if (no.Direito != null && no.Direito.FatorBalanceamento > 0)
                    {
                        this.EsquerdaDupla(no);
                    }
                    else
                    {
                        this.EsquerdaSimples(no);
                    }
                    break;

            }
        }

        public void Inserir(int chave)
        {
            this.Inserir(chave, null);
        }

        public void Inserir(int chave, object valor)
        {
            this.tamanho++;

            No novoNo = new No(chave, valor);

            if (this.Raiz == null)
            {
                this.Raiz = novoNo;
                return;
            }

            No no = this.Obter(chave);

            if (no.Chave != chave)
            {
                novoNo.Pai = no;

                if (chave > no.Chave)
                {
                    no.Direito = novoNo;
                }
                else
                {
                    no.Esquerdo = novoNo;
                }

                AtualizarFatorBalanceamento(novoNo, Operacao.Inserir);
            }
            else
            {
                no.Valor = valor;
            }
        }

        public No Remover(int chave)
        {
            if (this.Raiz == null)
            {
                return null;
            }
            return this.Remover(chave, this.Raiz);
        }

        public No Remover(int chave, No no)
        {
            if (no == null)
            {
                return null;
            }
            if (chave < no.Chave)
            {
                return this.Remover(chave, no.Esquerdo);
            }
            else if (chave > no.Chave)
            {
                return Remover(chave, no.Direito);
            }
            else
            {
                if (no.Esquerdo != null && no.Direito != null)
                {
                    No retorno = no;
                    No novo = no.Direito;

                    while (novo.Esquerdo != null && no.Direito != null)
                    {
                        novo = novo.Esquerdo;
                    }

                    novo = Remover(novo.Chave, no);
                    no.Chave = novo.Chave;
                    no.Valor = novo.Valor;

                    return retorno;
                }
                else if (no.Esquerdo != null)
                {
                    No removido = no;
                    AtualizarFatorBalanceamento(no, Operacao.Remover);

                    if (no.Pai == null)
                    {
                        this.Raiz = no.Esquerdo;
                    }
                    else
                    {
                        if (no.Pai.Chave < no.Chave)
                        {
                            no.Pai.Direito = no.Esquerdo;
                        }
                        else
                        {
                            no.Pai.Esquerdo = no.Esquerdo;
                        }
                    }

                    no.Esquerdo.Pai = no.Pai;

                    return removido;
                }
                else if (no.Direito != null)
                {
                    No removido = no;
                    AtualizarFatorBalanceamento(no, Operacao.Remover);
                    if (no.Pai == null)
                    {
                        this.Raiz = no.Direito;
                    }
                    else
                    {
                        if (no.Pai.Chave < no.Chave)
                        {
                            no.Pai.Direito = no.Direito;
                        }
                        else
                        {
                            no.Pai.Esquerdo = no.Direito;
                        }
                    }
                    no.Direito.Pai = no.Pai;

                    return removido;
                }
                else
                {
                    No pai = no.Pai;

                    if (pai == null)
                    {
                        this.Raiz = null;
                        return no;
                    }
                    AtualizarFatorBalanceamento(no, Operacao.Remover);
                    no.Pai = null;

                    if (no.Chave > pai.Chave)
                    {
                        pai.Direito = null;
                    }
                    else
                    {
                        pai.Esquerdo = null;
                    }
                    return no;
                }
            }
        }

        public List<No> Filhos()
        {
            List<No> filhos = new List<No>();

            this.Filhos(filhos, this.Raiz);

            return filhos;
        }

        void Filhos(List<No> filhos, No no)
        {
            if (no != null)
            {
                if (no.EInterno())
                {
                    this.Filhos(filhos, no.Esquerdo);
                }

                filhos.Add(no);

                if (no.EInterno())
                {
                    this.Filhos(filhos, no.Direito);
                }
            }
        }

        void Ordem()
        {
            this.Ordem(this.Raiz);
        }

        void Ordem(No no)
        {
            if (no != null)
            {
                if (no.EInterno())
                {
                    Ordem(no.Esquerdo);
                }

                Console.WriteLine(String.Format("[{0:000}] - [{1}] - Profundidade = [{2}] - Altura = [{3}]\n", no.Chave, no.Valor, no.Profundidade(), no.Altura()));

                if (no.EInterno())
                {
                    Ordem(no.Direito);
                }
            }
        }

        void PosOrdem()
        {
            PosOrdem(this.Raiz);
        }

        void PosOrdem(No no)
        {
            if (no != null)
            {
                if (no.EInterno())
                {
                    PosOrdem(no.Direito);
                }

                Console.WriteLine(String.Format("[{0:000}] - [{1}] - Profundidade = [{2}] - Altura = [{3}]\n", no.Chave, no.Valor, no.Profundidade(), no.Altura()));

                if (no.EInterno())
                {
                    PosOrdem(no.Esquerdo);
                }
            }
        }

        public int Altura()
        {
            return this.Raiz != null ? this.Raiz.Altura() : 0;
        }

        public int Profundidade()
        {
            return this.Raiz != null ? this.Raiz.Profundidade() : 0;
        }

        public string Imprimir()
        {
            StringBuilder sb = new StringBuilder("\nAVL: ");

            sb.Append(this.AtualizarAutomaticamente ? "Automática" : "Manual");
            sb.Append("\n\n");

            if (this.Raiz == null)
            {
                sb.Append("A árvore está vazia!\n");
            }
            else
            {
                for (int i = 0; i <= this.Altura(); i++)
                {
                    for (int j = 0; j <= this.Filhos().Count + 1; j++)
                    {
                        bool ok = false;
                        int index = 0;

                        foreach (No filho in this.Filhos())
                        {
                            if (filho.Profundidade() == i && index + 1 == j)
                            {
                                sb.AppendFormat("{0:000}[{1}]", filho.Chave, filho.FatorBalanceamento);
                                ok = true;
                                break;
                            }
                            index++;
                        }

                        if (ok) continue;

                        sb.Append("------");
                    }
                    sb.Append("\n");
                }
            }
            sb.Append("\n");

            return sb.ToString();
        }
    }
}
