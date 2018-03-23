using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDNL.RN
{
    public static class ImpressaoArvore
    {
        private class NoInfo
        {
            public No No;
            public string Texto;
            public int InicioPos;
            public int Tamanho { get { return Texto.Length; } }
            public int FimPos { get { return InicioPos + Tamanho; } set { InicioPos = value - Tamanho; } }
            public NoInfo Parente, Esquerdo, Direito;
        }

        public static void Imprimir(this No raiz, int margemTopo = 2, int margemEsquerdo = 2)
        {
            if (raiz == null) return;

            int rootTop = Console.CursorTop + margemTopo;
            var ultimo = new List<NoInfo>();
            var proximo = raiz;

            for (int nivel = 0; proximo != null; nivel++)
            {
                var item = new NoInfo { No = proximo, Texto = proximo.Valor.ToString(" 0 ") };
                if (nivel < ultimo.Count)
                {
                    item.InicioPos = ultimo[nivel].FimPos + 1;
                    ultimo[nivel] = item;
                }
                else
                {
                    item.InicioPos = margemEsquerdo;
                    ultimo.Add(item);
                }
                if (nivel > 0)
                {
                    item.Parente = ultimo[nivel - 1];
                    if (proximo == item.Parente.No.FilhoEsquerdo)
                    {
                        item.Parente.Esquerdo = item;
                        item.FimPos = Math.Max(item.FimPos, item.Parente.InicioPos);
                    }
                    else
                    {
                        item.Parente.Direito = item;
                        item.InicioPos = Math.Max(item.InicioPos, item.Parente.FimPos);
                    }
                }
                proximo = proximo.FilhoEsquerdo ?? proximo.FilhoDireito;
                for (; proximo == null; item = item.Parente)
                {
                    Imprimir(item, rootTop + 2 * nivel);
                    if (--nivel < 0) break;
                    if (item == item.Parente.Esquerdo)
                    {
                        item.Parente.InicioPos = item.FimPos;
                        proximo = item.Parente.No.FilhoDireito;
                    }
                    else
                    {
                        if (item.Parente.Esquerdo == null)
                            item.Parente.FimPos = item.InicioPos;
                        else
                            item.Parente.InicioPos += (item.InicioPos - item.Parente.FimPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * ultimo.Count - 1);
        }

        public static void Imprimir(this Arvore arvore, int margemTopo = 2, int margemEsquerdo = 2)
        {
            Imprimir(arvore.Raiz, margemTopo, margemEsquerdo);
        }

        private static void Imprimir(NoInfo item, int top)
        {
            TrocarCores(item.No);
            Imprimir(item.Texto, top, item.InicioPos);
            TrocarCores(item.No);
            if (item.Esquerdo != null)
                ImprimirLink(top + 1, "┌", "┘", item.Esquerdo.InicioPos + item.Esquerdo.Tamanho / 2, item.InicioPos);
            if (item.Direito != null)
                ImprimirLink(top + 1, "└", "┐", item.FimPos - 1, item.Direito.InicioPos + item.Direito.Tamanho / 2);
        }

        private static void Imprimir(string s, int topo, int esquerdo, int direito = -1)
        {
            Console.SetCursorPosition(esquerdo, topo);
            if (direito < 0) direito = esquerdo + s.Length;
            while (Console.CursorLeft < direito) Console.Write(s);
        }

        private static void ImprimirLink(int topo, string inicio, string fim, int inicioPos, int fimPos)
        {
            Imprimir(inicio, topo, inicioPos);
            Imprimir("─", topo, inicioPos + 1, fimPos);
            Imprimir(fim, topo, fimPos);
        }

        private static void TrocarCores(No no)
        {
            Console.ForegroundColor = no.Cor.Equals(No.CorRubroNegra.Rubro) ? ConsoleColor.Red : ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
        }
    }
}
