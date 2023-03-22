using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    internal class DetalleFactura
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public string CodigoProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public DetalleFactura()
        {
        }

        public DetalleFactura(int id, int idFactura, string codigoProducto, decimal precio, int cantidad, decimal total)
        {
            Id = id;
            IdFactura = idFactura;
            CodigoProducto = codigoProducto;
            Precio = precio;
            Cantidad = cantidad;
            Total = total;
        }
    }
}
