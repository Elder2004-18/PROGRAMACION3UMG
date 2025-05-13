using System;
using System.Collections.Generic;
using System.Transactions;

namespace PROYECTOFINAL
{
    public class Cliente
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public ListaEnlazada<Tarjeta> Tarjetas { get; set; }

        // Constructor
        public Cliente(string id, string nombre, string correo)
        {
            Id = id;
            Nombre = nombre;
            Correo = correo;
            Tarjetas = new ListaEnlazada<Tarjeta>();
        }
    }


    public class Tarjeta
    {
        public string Numero { get; set; }
        public double LimiteCredito { get; set; }
        public double Saldo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string PIN { get; set; }
        public bool Bloqueada { get; set; }
        public ListaEnlazada<Transaccion> Transacciones { get; set; }

        public Tarjeta(string numero, double limiteCredito)
        {
            Numero = numero;
            LimiteCredito = limiteCredito;
            Saldo = limiteCredito;

            // Nuevos valores predeterminados para compatibilidad si no vienen del JSON
            FechaVencimiento = DateTime.Now.AddYears(3); // Ej: 3 años por defecto
            PIN = "0000"; // PIN por defecto
            Bloqueada = false;

            Transacciones = new ListaEnlazada<Transaccion>();
        }

        public void AgregarTransaccion(Transaccion transaccion)
        {
            if (Bloqueada)
            {
                Console.WriteLine("Transacción denegada: La tarjeta está temporalmente bloqueada.");
                return;
            }

            string tipo = transaccion.Tipo.ToLower();

            if (tipo == "consumo" || tipo == "compra")
            {
                if (Saldo >= transaccion.Monto)
                {
                    Saldo -= transaccion.Monto;
                    Transacciones.InsertarFinal(transaccion);
                    Console.WriteLine($"Consumo exitoso. Saldo restante: Q{Saldo}");
                }
                else
                {
                    Console.WriteLine("Fondos insuficientes para este consumo.");
                }
            }
            else if (tipo == "pago")
            {
                Saldo += transaccion.Monto;
                if (Saldo > LimiteCredito)
                    Saldo = LimiteCredito;

                Transacciones.InsertarFinal(transaccion);
                Console.WriteLine($"Pago registrado. Saldo actualizado: Q{Saldo}");
            }
            else
            {
                Console.WriteLine("Tipo de transacción inválido.");
            }
        }

        // Método para renovar la tarjeta (cambia la fecha de vencimiento)
        public void RenovarTarjeta()
        {
            FechaVencimiento = DateTime.Now.AddYears(3);
            Console.WriteLine($"Tarjeta renovada. Nueva fecha de vencimiento: {FechaVencimiento:yyyy-MM-dd}");
        }

        // Método para cambiar PIN
        public void CambiarPIN(string nuevoPin)
        {
            PIN = nuevoPin;
            Console.WriteLine("PIN actualizado correctamente.");
        }

        // Método para bloquear o desbloquear tarjeta
        public void CambiarEstadoBloqueo(bool estado)
        {
            Bloqueada = estado;
            Console.WriteLine(estado ? "Tarjeta bloqueada temporalmente." : "Tarjeta desbloqueada.");
        }
    }




}
