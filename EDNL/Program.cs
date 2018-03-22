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

            EDNL.RN.Arvore tree = new RN.Arvore();
            EDNL.RN.No raiz = new EDNL.RN.No(7);
            tree.setRaiz(raiz);
            tree.incluir(5);
            tree.incluir(9);
            tree.incluir(8);
            tree.incluir(10);
            /*tree.incluir(70);
            tree.incluir(120);
            tree.incluir(60);
            tree.incluir(80);
            tree.incluir(110);
            tree.incluir(130);
            */
           // Console.WriteLine("removendo");
            //tree.remover(9);

           // tree.exibirArvore(tree.getRaiz());

           // tree.Print(tree.getRaiz(), 0);

            BTreePrinter.Print(tree.getRaiz());
            Console.ReadKey();
        }
    }
}
