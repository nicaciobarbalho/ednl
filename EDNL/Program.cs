using EDNL.RN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL
{
    class Program
    {
        static void Main(string[] args)
        {
            /* int[] avl = { 8, 6, 20, 2, 7, 11, 29, 3, 10, 12, 24, 32, 9, 22, 31 };

             AVL.Arvore arvore = new AVL.Arvore();

             foreach (int i in avl)
             {
                 arvore.Inserir(i, i);
             }

             string s1 = arvore.Imprimir();


             Console.WriteLine(s1);*/

            EDNL.RN.Arvore tree = new RN.Arvore(10);
            tree.Mensagem += Tree_Mensagem;

            Console.WriteLine("\nINCLUIR\n");
            tree.Incluir(6);
            tree.Incluir(22);
            tree.Incluir(3);
            tree.Incluir(8);
            tree.Incluir(7);
            tree.Incluir(9);

            Console.WriteLine("\nREMOVER\n");
            tree.Remover(3);


            tree.Imprimir();
            Console.ReadKey();
        }

        private static void Tree_Mensagem(No no, string mensagem)
        {
            if (no != null)
            {
                Console.WriteLine("Nó: {0} -> {1}", no.Valor, mensagem);
            }
            else
            {
                Console.WriteLine(mensagem);
            }

            Console.WriteLine("\n------------------------------------------------------\n");
        }
    }
}
