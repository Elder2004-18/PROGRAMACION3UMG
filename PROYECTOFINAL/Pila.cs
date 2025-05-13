namespace PROYECTOFINAL
{
    public class Pila<T>
    {
        private class Nodo
        {
            public T Valor;
            public Nodo Siguiente;

            public Nodo(T valor)
            {
                Valor = valor;
                Siguiente = null;
            }
        }

        private Nodo cima;

        public Pila()
        {
            cima = null;
        }

        public void Apilar(T valor)
        {
            Nodo nuevoNodo = new Nodo(valor);
            nuevoNodo.Siguiente = cima;
            cima = nuevoNodo;
        }

        public T Desapilar()
        {
            if (EstaVacia())
                throw new InvalidOperationException("La pila está vacía.");

            T valor = cima.Valor;
            cima = cima.Siguiente;
            return valor;
        }

        public bool EstaVacia()
        {
            return cima == null;
        }
    }
}
