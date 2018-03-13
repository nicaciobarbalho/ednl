using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.AVL
{
    public class No
    {
        #region Construtor
        public No()
        {

        }

        public No(int chave, object valor)
        {
            this.Chave = chave;
            this.Valor = valor;
        }

        #endregion

        #region Propriedades
        public No Pai { get; set; }
        public No Esquerdo { get; set; }
        public No Direito { get; set; }
        public int Chave { get; set; }
        public object Valor { get; set; }
        public int FatorBalanceamento { get; set; }
        #endregion

        #region Métodos
        public bool ExisteDireito()
        {
            return this.Direito != null;
        }

        public bool ExisteEsquerdo()
        {
            return this.Esquerdo != null;
        }

        public bool EInterno()
        {
            return this.ExisteDireito() || this.ExisteEsquerdo();
        }

        public bool EExterno()
        {
            return !EInterno();
        }

        public int Profundidade()
        {
            return this.Pai == null ? 0 : 1 + this.Pai.Profundidade();
        }

        public int Altura()
        {
            if (this.EExterno())
            {
                return 0;
            }
            else
            {
                int altura = 0;
                int alturaDireito = 0;
                int alturaEsquerdo = 0;

                if (this.ExisteDireito())
                {
                    alturaDireito = this.Direito.Altura();
                }
                if (this.ExisteEsquerdo())
                {
                    alturaEsquerdo = this.Esquerdo.Altura();
                }

                altura = alturaDireito > alturaEsquerdo ? alturaDireito : alturaEsquerdo;
                return altura + 1;
            }
        }
        #endregion
    }
}
