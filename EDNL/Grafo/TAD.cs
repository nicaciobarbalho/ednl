using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.Grafo
{
    public class TAD
    {

        private List<Vertice> vertices = new List<Vertice>();
        private List<Aresta> arestas = new List<Aresta>();

        public List<Vertice> Vertices
        {
            get
            {
                return vertices;
            }
        }

        public List<Aresta> Arestas
        {
            get
            {
                return arestas;
            }
        }

        public Vertice FindVertice(string o)
        {
            foreach (Vertice v in this.vertices)
            {
                if (v.Valor.Equals(o))
                {
                    return v;
                }
            }
            return null;
        }

        public Aresta FindAresta(string o)
        {
            foreach (Aresta a in this.arestas)
            {
                if (a.Valor.Equals(o))
                {
                    return a;
                }
            }
            return null;
        }

        public List<Vertice> FinalVertices(Aresta e)
        {
            List<Vertice> vertice = new List<Grafo.Vertice>();
            vertice.Add(e.V);
            vertice.Add(e.W);

            return vertice;
        }

        public List<Vertice> FinalVertices(string o)
        {
            Aresta e = this.FindAresta(o);
            List<Vertice> vertice = new List<Grafo.Vertice>();
            vertice.Add(e.V);
            vertice.Add(e.W);

            return vertice;
        }

        public Vertice Oposto(Vertice v, Aresta e)
        {
            if (v == e.V)
            {
                return e.W;
            }
            else if (v == e.W)
            {
                return e.V;
            }
            else
            {
                return null;
            }
        }

        public Vertice Oposto(string o, string b)
        {
            Vertice v = this.FindVertice(o);
            Aresta e = this.FindAresta(b);
            if (v == e.V)
            {
                return e.W;
            }
            else if (v == e.W)
            {
                return e.V;
            }
            else
            {
                return null;
            }
        }

        public bool IsAdjacente(Vertice v, Vertice w)
        {
            foreach (Aresta e in this.arestas)
            {
                if ((e.V == v && e.W == w) || (e.V == w && e.W == v))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsAdjacente(string o, string b)
        {
            Vertice v = this.FindVertice(o);
            Vertice w = this.FindVertice(b);
            foreach (Aresta e in this.arestas)
            {
                if (e.HasVertice(v) && e.HasVertice(w))
                {
                    return true;
                }
            }
            return false;
        }

        public void IsAdjacenteMostrar(string o, string b)
        {
            Vertice v = this.FindVertice(o);
            Vertice w = this.FindVertice(b);
            foreach (Aresta e in this.arestas)
            {
                if (e.HasVertice(v) && e.HasVertice(w))
                {
                    Console.WriteLine("É adjacente.");
                }
                else
                {
                    Console.WriteLine("Não é adjacente.");
                }
            }
        }

        public void SubstituirVertice(Vertice v, string x)
        {
            foreach (Vertice w in this.vertices)
            {
                if (w == v)
                {
                    w.Valor = x;
                    break;
                }
            }
        }

        public void SubstituirVertice(string o, string x)
        {
            Vertice v = this.FindVertice(o);
            foreach (Vertice w in this.vertices)
            {
                if (w == v)
                {
                    w.Valor = x;
                    break;
                }
            }
        }

        public void SubstituirAresta(Aresta a, string x)
        {
            foreach (Aresta d in this.arestas)
            {
                if (d == a)
                {
                    d.Valor = x;
                    break;
                }
            }
        }

        public void SubstituirAresta(string o, string x)
        {
            Aresta a = this.FindAresta(o);
            foreach (Aresta d in this.arestas)
            {
                if (d == a)
                {
                    d.Valor = x;
                    break;
                }
            }
        }

        public Vertice InserirVertice(string o)
        {
            Vertice v = new Vertice(o);
            this.vertices.Add(v);
            return v;
        }

        public Aresta InserirAresta(Vertice v, Vertice w, string o)
        {
            Aresta a = new Aresta(v, w);
            a.Valor = o;
            this.arestas.Add(a);
            return a;
        }

        public Aresta InserirAresta(string i, string j, string o)
        {
            Vertice v = this.FindVertice(i);
            Vertice w = this.FindVertice(j);
            Aresta a = new Aresta(v, w);
            a.Valor = o;
            this.arestas.Add(a);
            return a;
        }

        public Aresta InserirArestaOrientada(Vertice V, Vertice w, string o)
        {
            Aresta a = new Aresta(V, w, true);
            a.Valor = o;
            this.arestas.Add(a);
            return a;
        }

        public Aresta InserirArestaOrientada(string i, string j, string o)
        {
            Vertice v = FindVertice(i);
            Vertice w = FindVertice(j);
            Aresta a = new Aresta(v, w, true);
            a.Valor = o;
            this.arestas.Add(a);
            return a;
        }

        public List<Aresta> ArestasIncidentes(Vertice v)
        {
            List<Aresta> lista = new List<Aresta>();
            foreach (Aresta a in this.arestas)
            {
                if (a.HasVertice(v))
                {
                    lista.Add(a);
                }
            }
            return lista;
        }

        public List<Aresta> ArestasIncidentes(string o)
        {
            Vertice v = this.FindVertice(o);
            List<Aresta> lista = new List<Grafo.Aresta>();
            foreach (Aresta a in this.arestas)
            {
                if (a.HasVertice(v))
                {
                    lista.Add(a);
                }
            }
            return lista;
        }

        public bool Direcionado(Aresta e)
        {
            return e.Orientado;
        }

        public bool Direcionado(string o)
        {
            Aresta e = this.FindAresta(o);
            return e.Orientado;
        }

        public string RemoverVertice(Vertice v)
        {
            foreach (Aresta a in this.arestas)
            {
                if (a.HasVertice(v))
                {
                    this.arestas.Remove(a);
                }
            }
            string retorno = v.Valor;
            this.vertices.Remove(v);
            return retorno;
        }

        public string RemoverVertice(string o)
        {
            Vertice v = this.FindVertice(o);
            foreach (Aresta a in this.arestas)
            {
                if (a.HasVertice(v))
                {
                    this.arestas.Remove(a);
                }
            }
            string retorno = v.Valor;
            this.vertices.Remove(v);
            return retorno;
        }

        public string RemoverAresta(Aresta a)
        {
            string retorno = a.Valor;
            this.arestas.Remove(a);
            return retorno;
        }

        public string RemoverAresta(string o)
        {
            Aresta a = this.FindAresta(o);
            string retorno = a.Valor;
            this.arestas.Remove(a);
            return retorno;
        }

        // Matriz de Adjacencia
        public void MatrizAdjacencia()
        {
            Console.WriteLine("========================== MATRIZ DE ADJACENCIA ==========================\n");
          
            for (int i = -1; i < vertices.Count; i++)
            {
                
                if (i >= 0)
                {
                    
                    Console.Write(vertices[i].Valor);
                }

                for (int j = -1; j < vertices.Count; j++)
                {
                    if (i == -1)
                        if (j >= 0)
                            Console.Write(vertices[j].Valor + "  ");
                        else
                            Console.Write("    ");
                    else if (i >= 0 && j >= 0)
                    {
                        Console.Write("|");
                        if (IsAdjacente(vertices[i], vertices[j]))
                        {
                            Console.Write(" 1 ");
                        }
                        else
                        {
                            Console.Write(" 0 ");
                        }
                    }
                }
                Console.WriteLine("");
            }
            Console.Write("_______________\n");


        }

    }
}
