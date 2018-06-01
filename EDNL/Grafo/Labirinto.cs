using EDNL.Grafo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.Grafo
{

    class LerDoArquivo
    {
        StreamReader sr = new StreamReader(@"C:\Users\nicac\Downloads\Labirinto-master\labirinto.dat");
        string[] labirintoCompleto;
        List<string[]> labirintos = new List<string[]>();
        public string[] getTexto()
        {
            string linha = "";
            for (int i = 0; linha != null; i++)
            {
                labirintos.Add(new string[12]);
                linha = sr.ReadLine();
                for (int j = 0; linha != null && !linha.Equals("-"); j++)
                {
                    labirintos[i][j] = linha;
                    linha = sr.ReadLine();
                }
            }

            Random num = new Random();
            int labNum = num.Next(0, 2);
            return labirintos[0];
        }
    }

    public class Labirinto
    {
        private static readonly int INFINITY = 9999;

        string[] labirinto;
        public void montaGrafo()
        {

        }

        public string[] getLabirinto()
        {
            return labirinto;
        }

        public void labirintoDoArquivo()
        {
            LerDoArquivo labirintoStr = new LerDoArquivo();
            labirinto = labirintoStr.getTexto();
        }

        public List<Vertice> Dijkstra()
        {
            this.labirintoDoArquivo();


                Vertice [,] labirintoVertice = new Vertice[labirinto.Length, labirinto[0].Length];
                Grafo grafo = new Grafo();
                int indiceVertice = 1;
                Vertice inicio = null;



            for (int i = 0; i < labirinto.Length; i++)
            {
                for (int j = 0; j < labirinto[i].Length; j++)
                {
                    if (labirinto[i][j].Equals('1')) continue;

                    labirintoVertice[i, j] = grafo.InserirVertice(new Ponto(indiceVertice++, labirinto[i][j]));

                    if (labirinto[i][j].Equals('2'))
                    {
                        inicio = labirintoVertice[i, j];
                        if (i > 0 && !labirinto[i - 1][j].Equals('1'))
                        {
                            // Console.WriteLine("Aresta entre: " + labirintoVertice[i][j].getValor().toString() + " <-> " + labirintoVertice[i - 1][j].getValor().toString());
                            grafo.InserirAresta(labirintoVertice[i, j], labirintoVertice[i - 1, j], 1);
                        }
                        if (j > 0 && !labirinto[i][j - 1].Equals('1'))
                        {
                            // Console.WriteLine("Aresta entre: " + labirintoVertice[i][j].getValor().toString() + " <-> " + labirintoVertice[i][j - 1].getValor().toString());
                            grafo.InserirAresta(labirintoVertice[i, j], labirintoVertice[i, j - 1], 1);
                        }
                    }
                }
            }

            List<Vertice> vertices = grafo.Vertices;
            List<Vertice> percorridos = new List<Vertice>();
            List<Vertice> naoPercorridos = grafo.Vertices;

            percorridos.Add(inicio);
            vertices.Remove(inicio);
            naoPercorridos.Remove(inicio);


            int[] distancias = new int[vertices.Count + 2];
            Vertice[] antecessor = new Vertice[vertices.Count + 2];
            distancias[Index(inicio)] = Distancia(grafo, inicio, inicio);

            foreach (Vertice v in vertices)
            {
                distancias[Index(v)] = Distancia(grafo, inicio, v);
            }

            Vertice w = inicio;


            while (percorridos.Count != grafo.Vertices.Count)
            {

                Ponto p = Ponto(w);


                if (w != null) Console.WriteLine("Ultimo w: " + p.Chave);

                w = maisProximo(naoPercorridos, distancias);

                if (w != null) Console.WriteLine("Novo w: " + p.Chave + " (" + distancias[Index(w)] + ")");

                if (distancias[Index(w)] >= INFINITY) break;

                percorridos.Add(w);
                naoPercorridos.Remove(w);

                if (p.Valor.Equals('3'))
                {
                    Console.WriteLine ("Parou no: " + w.Valor);
                    break;
                }

                foreach (Vertice v in naoPercorridos)
                {
                    if (distancias[Index(w)] + Distancia(grafo, w, v) < distancias[Index(v)])
                    {
                        antecessor[Index(v)] = w;
                    }
                    distancias[Index(v)] = Math.Min(distancias[Index(v)], distancias[Index(w)] + Distancia(grafo, w, v));
                }
            }


            Console.WriteLine("Tamanho percorridos: " + percorridos.Count);
            Console.WriteLine("Tamanho vertices: " + vertices.Count);
            Console.WriteLine("");

            Console.WriteLine("Labirinto:");

            for (int i = 0; i < labirinto.Length; i++)
            {
                for (int j = 0; j < labirinto[i].Length; j++)
                {
                    Vertice v = labirintoVertice[i,j];
                    String str = "░░░░";
                    if (v != null)
                    {
                        String borda = " ";
                        if (Ponto(w).Valor.Equals("3"))
                        {
                            borda = "↗";
                        }
                        else if (Ponto(w).Valor.Equals("2"))
                        {
                            borda = "↙";
                        }
                        str = borda + String.Format("{0:00}", (Ponto(w).Chave) + borda);
                    }
                    Console.Write(str);
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");

            Console.WriteLine("Antecessores: ");
            foreach (Vertice v in percorridos)
            {
                if (antecessor[Index(v)] == null) continue;
                Console.Write(antecessor[Index(v)].Valor);
                Console.Write(" -> ");
                Console.Write(v.Valor);
                Console.WriteLine("");
            }

            Console.WriteLine("Percorridos: ");
            foreach (Vertice v in percorridos)
            {
                Console.Write(v.Valor);
                Console.Write(" ");
            }

            Console.WriteLine("");

            if (w != null && (Ponto(w).Valor.Equals("3") && distancias[Index(w)] < INFINITY))
            {
                Console.WriteLine("Conseguiu.");
                Console.Write("Caminho: ");
                String caminho = "";
                int i = percorridos.Count - 1;
                Vertice v = percorridos[i];
                Vertice vAntecessor = antecessor[Index(v)];
                caminho = Index(v) + caminho;

                while (vAntecessor != null)
                {
                    i = percorridos.IndexOf(vAntecessor);
                    v = vAntecessor;
                    vAntecessor = antecessor[Index(v)];
                    caminho = Index(v) + "-> " + caminho;
                }

                caminho = Index(percorridos[0]) + "-> " + caminho;

                Console.WriteLine(caminho);
            }
                return null;
        }


        private static Vertice maisProximo(List<Vertice> vertices, int[] distancias)
        {
            Vertice w = vertices[0];

            int minDistancia = distancias[Index(w)];
            foreach (Vertice v in vertices)
            {
                if (distancias[Index(v)] < minDistancia)
                {
                    Console.WriteLine("D[v] < D[w], " + ((Ponto)v.Valor).Chave + " < " + ((Ponto)w.Valor).Chave + ", " + distancias[Index(v)] + " < " + minDistancia);
                    w = v;
                    minDistancia = distancias[Index(v)];
                }
            }
            return w;
        }

        private static int Index(Vertice v)
        {
            return int.Parse(Ponto(v).Valor.ToString());
        }

        private static Ponto Ponto(Vertice v)
        {
            Ponto p = null;

            if (v.Valor is Ponto)
            {
                return (Ponto)v.Valor;
            }
             if (v.Valor is Newtonsoft.Json.Linq.JObject )
            {
                dynamic results = JsonConvert.DeserializeObject<dynamic>(v.Valor.ToString());
                object valor = results.Valor;
                object chave = results.Chave;
                p = new EDNL.Grafo.Ponto(chave, valor);

                return p;
            }
            else
            {
                return p;
            }
        }

      

        private static int Distancia(Grafo g, Vertice a, Vertice b)
        {
            if (g.IsAdjacente(a, b))
            {
                return 1;
            }

            if (a.Equals(b))
            {
                return 0;
            }

            return INFINITY;
        }
    }
}
