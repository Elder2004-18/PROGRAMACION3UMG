namespace PROYECTOFINAL
{
    public class Transaccion
    {
        public string Tipo { get; set; }  // "pago" o "consumo"
        public double Monto { get; set; }
        public string Fecha { get; set; }

        public Transaccion(string tipo, double monto, string fecha)
        {
            Tipo = tipo.ToLower();
            Monto = monto;
            Fecha = fecha;
        }
    }
}
