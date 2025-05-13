using System;
using System.Collections.Generic;

namespace PROYECTOFINAL
{
    public class TablaHash<T>
    {
        private const int TAM_TABLA = 997;
        private const double R = 0.618034;
        private List<Nodo>[] tabla;

        public TablaHash()
        {
            tabla = new List<Nodo>[TAM_TABLA];
            for (int i = 0; i < TAM_TABLA; i++)
                tabla[i] = new List<Nodo>();
        }

        private class Nodo
        {
            public string Clave { get; set; }
            public T Valor { get; set; }

            public Nodo(string clave, T valor)
            {
                Clave = clave;
                Valor = valor;
            }
        }

        private long TransformarClave(string clave)
        {
            long d = 0;
            for (int j = 0; j < Math.Min(clave.Length, 10); j++)
            {
                d = d * 27 + (int)clave[j];
            }
            return d < 0 ? -d : d;
        }

        private int FuncionHash(string clave)
        {
            long x = TransformarClave(clave);
            double t = R * x - Math.Floor(R * x);
            return (int)(TAM_TABLA * t);
        }

        public void Insertar(string clave, T valor)
        {
            int pos = FuncionHash(clave);
            // Eliminar cualquier duplicado con la misma clave
            tabla[pos].RemoveAll(n => n.Clave == clave);
            tabla[pos].Add(new Nodo(clave, valor));
        }

        public T Buscar(string clave)
        {
            int pos = FuncionHash(clave);
            foreach (var nodo in tabla[pos])
            {
                if (nodo.Clave == clave)
                    return nodo.Valor;
            }
            return default(T);
        }

        public void Eliminar(string clave)
        {
            int pos = FuncionHash(clave);
            tabla[pos].RemoveAll(n => n.Clave == clave);
        }

        public T[] ObtenerTodosLosValores()
        {
            List<T> resultados = new List<T>();
            foreach (var lista in tabla)
            {
                foreach (var nodo in lista)
                    resultados.Add(nodo.Valor);
            }
            return resultados.ToArray();
        }
    }
}
