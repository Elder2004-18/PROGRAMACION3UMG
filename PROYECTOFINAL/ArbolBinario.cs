using System;

namespace PROYECTOFINAL
{
    public class Nodo
    {
        public int Valor { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = Derecho = null;
        }
    }

    public class ArbolBinarioBusqueda
    {
        public Nodo Raiz { get; private set; }

        public ArbolBinarioBusqueda()
        {
            Raiz = null;
        }

        // Insertar un valor
        public void Insertar(int valor)
        {
            Raiz = Insertar(Raiz, valor);
        }

        private Nodo Insertar(Nodo raiz, int valor)
        {
            if (raiz == null)
                return new Nodo(valor);

            if (valor < raiz.Valor)
                raiz.Izquierdo = Insertar(raiz.Izquierdo, valor);
            else
                raiz.Derecho = Insertar(raiz.Derecho, valor);

            return raiz;
        }

        // Buscar un valor
        public bool Buscar(int valor)
        {
            return Buscar(Raiz, valor) != null;
        }

        private Nodo Buscar(Nodo raiz, int valor)
        {
            if (raiz == null || raiz.Valor == valor)
                return raiz;

            if (valor < raiz.Valor)
                return Buscar(raiz.Izquierdo, valor);

            return Buscar(raiz.Derecho, valor);
        }
    }
}
