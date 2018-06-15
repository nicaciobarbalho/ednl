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
                    
                    if (int.Parse(labirinto[i][j].ToString()).Equals(1)) continue;

                    labirintoVertice[i, j] = grafo.InserirVertice(new Ponto(indiceVertice++, int.Parse(labirinto[i][j].ToString())));

                    if (int.Parse(labirinto[i][j].ToString()).Equals(2))
                    {
                        inicio = labirintoVertice[i, j];
                        if (i > 0 && !int.Parse(labirinto[i - 1][j].ToString()).Equals(1))
                        {
                            grafo.InserirAresta(labirintoVertice[i, j], labirintoVertice[i - 1, j], 1);
                        }
                        if (j > 0 && !int.Parse(labirinto[i][j - 1].ToString()).Equals(1))
                        {
                            grafo.InserirAresta(labirintoVertice[i, j], labirintoVertice[i, j - 1], 1);
                        }
                    }
                }
            }

            List<Vertice> vertices = grafo.Vertices;
            List<Vertice> percorridos = new List<Vertice>();
            List<Vertice> naoPercorridos = grafo.Vertices;

            percorridos.Add(inicio);
            vertices.RemoveAll(r => ((Ponto)r.Valor).Valor.Equals(2));
            naoPercorridos.RemoveAll(r => ((Ponto)r.Valor).Valor.Equals(2));


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
                    String str = " ";
                    if (v != null)
                    {
                        String borda = "░░";
                        int index = Index(v);
                        
                        if (index.Equals(3))
                        {
                            borda = "↗";
                        }
                        
                        else if (index.Equals(2))
                        {
                            borda = "↙";
                        }
                        str = borda + String.Format("{0:00}", RecuperaChave(v)) + borda;
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
                Console.Write(Index(v));
                Console.WriteLine("");
            }

            Console.WriteLine("Percorridos: ");
            foreach (Vertice v in percorridos)
            {
                Console.Write(Index(v));
                Console.Write(" ");
            }

            Console.WriteLine("");

            if (w != null && Index(w).Equals(3) && distancias[Index(w)] < INFINITY)
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
                    caminho = Index(v) + "." + caminho;
                }

                caminho = Index(percorridos[0]) + "." + caminho;

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

        private static object RecuperaChave(Vertice v)
        {
            return Ponto(v).Chave;
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
