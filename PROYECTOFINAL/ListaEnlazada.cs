using System;
using System.Collections;
using System.Collections.Generic;

namespace PROYECTOFINAL
{
    // Nodo de la lista enlazada
    public class Nodo<T>
    {
        public T Valor { get; set; }
        public Nodo<T> Siguiente { get; set; }

        public Nodo(T valor)
        {
            Valor = valor;
            Siguiente = null;
        }
    }

    public class ListaEnlazada<T> : IEnumerable<T>
    {
        private Nodo<T> cabeza;
        private Nodo<T> cursor; // ← Cursor para recorrer con los métodos pedidos

        public ListaEnlazada()
        {
            cabeza = null;
            cursor = null;
        }

        // Insertar un valor al final de la lista
        public void InsertarFinal(T valor)
        {
            Nodo<T> nuevoNodo = new Nodo<T>(valor);
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                Nodo<T> actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }
        }

        // Implementar GetEnumerator para poder usar foreach
        public IEnumerator<T> GetEnumerator()
        {
            Nodo<T> actual = cabeza;
            while (actual != null)
            {
                yield return actual.Valor;
                actual = actual.Siguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Buscar un valor en la lista
        public bool Buscar(T valor)
        {
            Nodo<T> actual = cabeza;
            while (actual != null)
            {
                if (EqualityComparer<T>.Default.Equals(actual.Valor, valor))
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        // Buscar un valor por clave en la lista (para tipos específicos como Tarjeta)
        public T BuscarPorClave(string clave)
        {
            Nodo<T> actual = cabeza;
            while (actual != null)
            {
                if (actual.Valor is Tarjeta tarjeta && tarjeta.Numero == clave)
                {
                    return actual.Valor;
                }
                actual = actual.Siguiente;
            }
            return default(T);
        }

        // Eliminar un valor de la lista
        public bool Eliminar(T valor)
        {
            if (cabeza == null)
            {
                return false;
            }

            if (EqualityComparer<T>.Default.Equals(cabeza.Valor, valor))
            {
                cabeza = cabeza.Siguiente;
                return true;
            }

            Nodo<T> actual = cabeza;
            while (actual.Siguiente != null)
            {
                if (EqualityComparer<T>.Default.Equals(actual.Siguiente.Valor, valor))
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    return true;
                }
                actual = actual.Siguiente;
            }

            return false;
        }

        // Obtener el primer elemento de la lista
        public T ObtenerPrimer()
        {
            return cabeza != null ? cabeza.Valor : default(T);
        }

        // ✅ Método 1: Reiniciar el cursor al inicio
        public void Reiniciar()
        {
            cursor = cabeza;
        }

        // ✅ Método 2: ¿Hay siguiente nodo?
        public bool HaySiguiente()
        {
            return cursor != null;
        }

        // ✅ Método 3: Obtener el valor del nodo actual y avanzar
        public T ObtenerSiguiente()
        {
            if (cursor == null)
                return default(T);

            T valor = cursor.Valor;
            cursor = cursor.Siguiente;
            return valor;
        }

        // Obtener el último elemento de la lista
        public T ObtenerUltimo()
        {
            if (cabeza == null) return default(T);

            Nodo<T> actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            return actual.Valor;
        }
    }
}
