namespace PROYECTOFINAL
{
    public class Cola<T>
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

        private Nodo cabeza;
        private Nodo cola;

        public Cola()
        {
            cabeza = cola = null;
        }

        public void Encolar(T valor)
        {
            Nodo nuevoNodo = new Nodo(valor);
            if (cola == null)
            {
                cabeza = cola = nuevoNodo;
            }
            else
            {
                cola.Siguiente = nuevoNodo;
                cola = nuevoNodo;
            }
        }

        public T Desencolar()
        {
            if (EstaVacia())
                throw new InvalidOperationException("La cola está vacía.");

            T valor = cabeza.Valor;
            cabeza = cabeza.Siguiente;

            if (cabeza == null)
            {
                cola = null;
            }

            return valor;
        }

        public bool EstaVacia()
        {
            return cabeza == null;
        }
    }
}
