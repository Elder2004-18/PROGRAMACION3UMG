using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace PROYECTOFINAL
{
    public static class LectorJson
    {
        public static void CargarClientesDesdeJson(string rutaArchivo, TablaHash<Cliente> tablaClientes)
        {
            string contenido = File.ReadAllText(rutaArchivo);
            var clientesJson = JsonSerializer.Deserialize<List<ClienteJson>>(contenido);

            foreach (var clienteJson in clientesJson)
            {
                Cliente cliente = new Cliente(clienteJson.Id, clienteJson.Nombre, clienteJson.Correo);

                // Aquí va el foreach modificado:
                foreach (var tarjetaJson in clienteJson.Tarjetas)
                {
                    Tarjeta tarjeta = new Tarjeta(tarjetaJson.Numero, tarjetaJson.Saldo);

                    // Asignar manualmente los valores adicionales del JSON
                    tarjeta.PIN = tarjetaJson.PIN;
                    tarjeta.FechaVencimiento = tarjetaJson.FechaVencimiento;
                    tarjeta.Bloqueada = tarjetaJson.Bloqueada;

                    foreach (var transJson in tarjetaJson.HistorialTransacciones)
                    {
                        Transaccion trans = new Transaccion(transJson.Tipo, transJson.Monto, transJson.Fecha.ToString("yyyy-MM-dd"));
                        tarjeta.AgregarTransaccion(trans);
                    }

                    cliente.Tarjetas.InsertarFinal(tarjeta);
                }

                tablaClientes.Insertar(cliente.Id, cliente);
            }
        }

    }
}
