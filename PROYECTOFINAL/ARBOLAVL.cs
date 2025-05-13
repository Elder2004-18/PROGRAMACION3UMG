using System;

namespace PROYECTOFINAL
{
    public class NodoAVL
    {
        public int Valor { get; set; }
        public NodoAVL Izquierdo { get; set; }
        public NodoAVL Derecho { get; set; }
        public int Altura { get; set; }

        public NodoAVL(int valor)
        {
            Valor = valor;
            Izquierdo = Derecho = null;
            Altura = 1;  // La altura de un nuevo nodo es 1
        }
    }

    public class ArbolAVL
    {
        public NodoAVL Raiz { get; private set; }

        public ArbolAVL()
        {
            Raiz = null;
        }

        // Insertar un valor
        public void Insertar(int valor)
        {
            Raiz = Insertar(Raiz, valor);
        }

        private NodoAVL Insertar(NodoAVL raiz, int valor)
        {
            if (raiz == null)
                return new NodoAVL(valor);

            if (valor < raiz.Valor)
                raiz.Izquierdo = Insertar(raiz.Izquierdo, valor);
            else if (valor > raiz.Valor)
                raiz.Derecho = Insertar(raiz.Derecho, valor);
            else
                return raiz;

            raiz.Altura = 1 + Math.Max(ObtenerAltura(raiz.Izquierdo), ObtenerAltura(raiz.Derecho));

            int balance = ObtenerBalance(raiz);

            if (balance > 1 && valor < raiz.Izquierdo.Valor)
                return RotarDerecha(raiz);

            if (balance < -1 && valor > raiz.Derecho.Valor)
                return RotarIzquierda(raiz);

            if (balance > 1 && valor > raiz.Izquierdo.Valor)
            {
                raiz.Izquierdo = RotarIzquierda(raiz.Izquierdo);
                return RotarDerecha(raiz);
            }

            if (balance < -1 && valor < raiz.Derecho.Valor)
            {
                raiz.Derecho = RotarDerecha(raiz.Derecho);
                return RotarIzquierda(raiz);
            }

            return raiz;
        }

        // Obtener altura de un nodo
        private int ObtenerAltura(NodoAVL nodo)
        {
            return nodo == null ? 0 : nodo.Altura;
        }

        // Obtener el balance de un nodo
        private int ObtenerBalance(NodoAVL nodo)
        {
            return nodo == null ? 0 : ObtenerAltura(nodo.Izquierdo) - ObtenerAltura(nodo.Derecho);
        }

        // Rotación derecha
        private NodoAVL RotarDerecha(NodoAVL y)
        {
            NodoAVL x = y.Izquierdo;
            NodoAVL T2 = x.Derecho;

            x.Derecho = y;
            y.Izquierdo = T2;

            y.Altura = Math.Max(ObtenerAltura(y.Izquierdo), ObtenerAltura(y.Derecho)) + 1;
            x.Altura = Math.Max(ObtenerAltura(x.Izquierdo), ObtenerAltura(x.Derecho)) + 1;

            return x;
        }

        // Rotación izquierda
        private NodoAVL RotarIzquierda(NodoAVL x)
        {
            NodoAVL y = x.Derecho;
            NodoAVL T2 = y.Izquierdo;

            y.Izquierdo = x;
            x.Derecho = T2;

            x.Altura = Math.Max(ObtenerAltura(x.Izquierdo), ObtenerAltura(x.Derecho)) + 1;
            y.Altura = Math.Max(ObtenerAltura(y.Izquierdo), ObtenerAltura(y.Derecho)) + 1;

            return y;
        }
    }
}
