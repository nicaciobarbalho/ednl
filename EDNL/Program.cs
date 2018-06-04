using EDNL.RN;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Encoding altEnc = Encoding.GetEncoding("UTF-8");
            /* Console.OutputEncoding = Encoding.UTF8;

             Console.OutputEncoding = Encoding.BigEndianUnicode;

             Console.OutputEncoding = Encoding.Unicode;

             Console.OutputEncoding = Encoding.UTF32;

             Console.OutputEncoding = Encoding.UTF7;*/

            Console.OutputEncoding = altEnc;

            /*Grafo.TAD grafo = new Grafo.TAD();

            grafo.InserirVertice("v1");
            grafo.InserirVertice("v2");
            grafo.InserirVertice("v3");
            grafo.InserirVertice("v4");

            grafo.InserirAresta("v1", "v2", "10");
            grafo.InserirAresta("v2", "v4", "11");
            grafo.InserirAresta("v3", "v1", "12");
            grafo.InserirAresta("v4", "v3", "13");

            grafo.MatrizAdjacencia();
            Console.WriteLine("GRAU = {0}", grafo.GrauDoVertice("v2"));*/

            // Cript.CriptografiaRijndael rij = new Cript.CriptografiaRijndael();
            // Console.WriteLine("SENHA = {0}", rij.Decriptografar("9xQvmna7zUT2KbFszEvC2g=="));

             Grafo.Labirinto lab = new Grafo.Labirinto();

             lab.Dijkstra();
          //  Console.WriteLine("↗");
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
