public class ClienteJson
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public List<TarjetaJson> Tarjetas { get; set; }
}

public class TarjetaJson
{
    public string Numero { get; set; }
    public double LimiteCredito { get; set; }
    public double Saldo { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public string PIN { get; set; }
    public bool Bloqueada { get; set; }
    public List<TransaccionJson> HistorialTransacciones { get; set; }
}

public class TransaccionJson
{
    public string Tipo { get; set; }
    public double Monto { get; set; }
    public DateTime Fecha { get; set; }
}
