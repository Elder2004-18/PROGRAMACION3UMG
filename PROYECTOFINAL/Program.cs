using System;

namespace PROYECTOFINAL
{
    class Program
    {
        static void Main(string[] args)
        {
            TablaHash<Cliente> tablaClientes = new TablaHash<Cliente>();
            Cola<Transaccion> colaPendientes = new Cola<Transaccion>();
            Pila<Transaccion> pilaHistorial = new Pila<Transaccion>();

            try
            {
                LectorJson.CargarClientesDesdeJson("clientes.json", tablaClientes);
                Console.WriteLine("Clientes cargados correctamente desde JSON.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer archivo JSON: " + ex.Message);
                return;
            }

            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("--- Menú Principal ---");
                Console.WriteLine("1. Buscar cliente");
                Console.WriteLine("2. Ver tarjetas de cliente");
                Console.WriteLine("3. Agregar transacción a tarjeta");
                Console.WriteLine("4. Ver historial de transacciones");
                Console.WriteLine("5. Procesar transacciones pendientes");
                Console.WriteLine("6. Mostrar todos los clientes y sus tarjetas");
                Console.WriteLine("7. Renovar tarjeta");
                Console.WriteLine("8. Cambiar PIN de tarjeta");
                Console.WriteLine("9. Bloquear/Desbloquear tarjeta");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese ID de cliente: ");
                        string idCliente = Console.ReadLine();
                        Cliente cliente = tablaClientes.Buscar(idCliente);
                        Console.WriteLine(cliente != null
                            ? $"Cliente encontrado: {cliente.Nombre}"
                            : "Cliente no encontrado.");
                        break;

                    case 2:
                        Console.Write("Ingrese ID de cliente: ");
                        idCliente = Console.ReadLine();
                        cliente = tablaClientes.Buscar(idCliente);
                        if (cliente != null)
                        {
                            Console.WriteLine($"Tarjetas de {cliente.Nombre}:");
                            cliente.Tarjetas.Reiniciar();
                            while (cliente.Tarjetas.HaySiguiente())
                            {
                                var tarjeta = cliente.Tarjetas.ObtenerSiguiente();
                                Console.WriteLine($"- Número: {tarjeta.Numero}, Límite: Q{tarjeta.LimiteCredito}, " +
                                    $"Saldo: Q{tarjeta.Saldo}, Vence: {tarjeta.FechaVencimiento:yyyy-MM-dd}, " +
                                    $"Bloqueada: {(tarjeta.Bloqueada ? "Sí" : "No")}, PIN: {tarjeta.PIN}");
                            }
                        }
                        else
                            Console.WriteLine("Cliente no encontrado.");
                        break;


                    case 3:
                        Console.Write("Ingrese ID de cliente: ");
                        idCliente = Console.ReadLine();
                        cliente = tablaClientes.Buscar(idCliente);
                        if (cliente != null)
                        {
                            Console.Write("Ingrese número de tarjeta: ");
                            string numTarjeta = Console.ReadLine();
                            cliente.Tarjetas.Reiniciar();
                            while (cliente.Tarjetas.HaySiguiente())
                            {
                                var tarjeta = cliente.Tarjetas.ObtenerSiguiente();
                                if (tarjeta.Numero == numTarjeta)
                                {
                                    Console.Write("Tipo de transacción (compra/pago): ");
                                    string tipo = Console.ReadLine();
                                    Console.Write("Monto: ");
                                    double monto = double.Parse(Console.ReadLine());
                                    Console.Write("Fecha (YYYY-MM-DD): ");
                                    string fecha = Console.ReadLine();

                                    var nuevaTrans = new Transaccion(tipo, monto, fecha);
                                    tarjeta.AgregarTransaccion(nuevaTrans);
                                    pilaHistorial.Apilar(nuevaTrans);
                                    colaPendientes.Encolar(nuevaTrans);
                                    break;
                                }
                            }
                        }
                        else Console.WriteLine("Cliente no encontrado.");
                        break;

                    case 4:
                        Console.Write("Ingrese ID de cliente: ");
                        idCliente = Console.ReadLine();
                        cliente = tablaClientes.Buscar(idCliente);
                        if (cliente != null)
                        {
                            Console.WriteLine($"Historial de transacciones de {cliente.Nombre}:");
                            bool hay = false;
                            cliente.Tarjetas.Reiniciar();
                            while (cliente.Tarjetas.HaySiguiente())
                            {
                                var tarjeta = cliente.Tarjetas.ObtenerSiguiente();
                                tarjeta.Transacciones.Reiniciar();
                                while (tarjeta.Transacciones.HaySiguiente())
                                {
                                    var t = tarjeta.Transacciones.ObtenerSiguiente();
                                    Console.WriteLine($"- {tarjeta.Numero}: {t.Tipo} Q{t.Monto} el {t.Fecha}");
                                    hay = true;
                                }
                            }
                            if (!hay) Console.WriteLine("Sin transacciones.");
                        }
                        else Console.WriteLine("Cliente no encontrado.");
                        break;

                    case 5:
                        Console.WriteLine("Procesando transacciones pendientes:");
                        while (!colaPendientes.EstaVacia())
                        {
                            var t = colaPendientes.Desencolar();
                            Console.WriteLine($"- Procesada {t.Tipo} Q{t.Monto} el {t.Fecha}");
                        }
                        break;

                    case 6:
                        Console.WriteLine("Todos los clientes y tarjetas:");
                        var todos = tablaClientes.ObtenerTodosLosValores();
                        foreach (var cli in todos)
                        {
                            if (cli != null)
                            {
                                Console.WriteLine($"Cliente: {cli.Nombre} ({cli.Id})");
                                cli.Tarjetas.Reiniciar();
                                while (cli.Tarjetas.HaySiguiente())
                                {
                                    var tarjeta = cli.Tarjetas.ObtenerSiguiente();
                                    Console.WriteLine($"\tTarjeta: {tarjeta.Numero} | Vence: {tarjeta.FechaVencimiento:yyyy-MM-dd}");
                                    tarjeta.Transacciones.Reiniciar();
                                    while (tarjeta.Transacciones.HaySiguiente())
                                    {
                                        var t = tarjeta.Transacciones.ObtenerSiguiente();
                                        Console.WriteLine($"\t\t{t.Fecha} - {t.Tipo} - Q{t.Monto}");
                                    }
                                }
                            }
                        }
                        break;

                    case 7:
                        Console.Write("Ingrese ID de cliente: ");
                        idCliente = Console.ReadLine();
                        cliente = tablaClientes.Buscar(idCliente);
                        if (cliente != null)
                        {
                            Console.Write("Ingrese número de tarjeta: ");
                            string numero = Console.ReadLine();
                            cliente.Tarjetas.Reiniciar();
                            while (cliente.Tarjetas.HaySiguiente())
                            {
                                var tarjeta = cliente.Tarjetas.ObtenerSiguiente();
                                if (tarjeta.Numero == numero)
                                {
                                    tarjeta.RenovarTarjeta();
                                    break;
                                }
                            }
                        }
                        else Console.WriteLine("Cliente no encontrado.");
                        break;

                    case 8:
                        Console.Write("Ingrese ID de cliente: ");
                        idCliente = Console.ReadLine();
                        cliente = tablaClientes.Buscar(idCliente);
                        if (cliente != null)
                        {
                            Console.Write("Ingrese número de tarjeta: ");
                            string numero = Console.ReadLine();
                            cliente.Tarjetas.Reiniciar();
                            while (cliente.Tarjetas.HaySiguiente())
                            {
                                var tarjeta = cliente.Tarjetas.ObtenerSiguiente();
                                if (tarjeta.Numero == numero)
                                {
                                    Console.Write("Nuevo PIN: ");
                                    string nuevoPIN = Console.ReadLine();
                                    tarjeta.CambiarPIN(nuevoPIN);
                                    break;
                                }
                            }
                        }
                        else Console.WriteLine("Cliente no encontrado.");
                        break;

                    case 9:
                        Console.Write("Ingrese ID de cliente: ");
                        idCliente = Console.ReadLine();
                        cliente = tablaClientes.Buscar(idCliente);
                        if (cliente != null)
                        {
                            Console.Write("Ingrese número de tarjeta: ");
                            string numero = Console.ReadLine();
                            cliente.Tarjetas.Reiniciar();
                            while (cliente.Tarjetas.HaySiguiente())
                            {
                                var tarjeta = cliente.Tarjetas.ObtenerSiguiente();
                                if (tarjeta.Numero == numero)
                                {
                                    tarjeta.CambiarEstadoBloqueo(!tarjeta.Bloqueada);
                                    break;
                                }
                            }
                        }
                        else Console.WriteLine("Cliente no encontrado.");
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 0);
        }
    }
}
