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
            int[] avl = { 8, 6, 20, 2, 7, 11, 29, 3, 10, 12, 24, 32, 9, 22, 31 };

            AVL.Arvore arvore = new AVL.Arvore();

            foreach (int i in avl)
            {
                arvore.Inserir(i, i);
            }

            string s1 = arvore.Imprimir();


            Console.WriteLine(s1);


            Console.ReadKey();
        }
    }
}
