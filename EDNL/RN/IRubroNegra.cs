using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDNL.AVL;

namespace EDNL.RN
{
    interface IRubroNegra
    {
        No Incluir(int chave, object valor);
        No Incluir(int chave);

        bool isEmpty();

        object Remover(Object key);

        No Raiz();

        void EmOrdemRN(No no);

        void PreOrdem(No no, int n);

        void Mostrar(No no, int A);
    }
}
