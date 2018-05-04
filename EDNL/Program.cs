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
            Grafo.TAD grafo = new Grafo.TAD();

            grafo.InserirVertice("v1");
            grafo.InserirVertice("v2");
            grafo.InserirVertice("v3");
            grafo.InserirVertice("v4");

            grafo.InserirAresta("v1", "v2", "v12");
            grafo.InserirAresta("v2", "v4", "v24");
            grafo.InserirAresta("v3", "v1", "v31");
            grafo.InserirAresta("v4", "v3", "v43");

            grafo.MatrizAdjacencia();

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
