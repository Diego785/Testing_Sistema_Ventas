using Microsoft.EntityFrameworkCore;
using TestingDBVenta.Models.Interfaces;

namespace TestingDBVenta.Models.Implementations
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbventaContext _dbventaContext;
        
        public VentaRepository(DbventaContext dbVentaContext) : base(dbVentaContext) 
        {
            _dbventaContext = dbVentaContext;
        }
        
        public async Task<Venta> Register(Venta entity)
        {
            Venta newVenta = new Venta();
            using (var transaction = _dbventaContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in entity.DetalleVenta)
                    {
                        Producto finded_product = _dbventaContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        finded_product.Stock = finded_product.Stock - dv.Cantidad;
                        _dbventaContext.Productos.Update(finded_product);
                    }
                    await _dbventaContext.SaveChangesAsync();

                    NumeroCorrelativo correlative = _dbventaContext.NumeroCorrelativos.Where(n => n.Gestion == "venta").First();
                    correlative.UltimoNumero = correlative.UltimoNumero + 1;
                    correlative.FechaActualizacion = DateTime.Now;

                    _dbventaContext.NumeroCorrelativos.Update(correlative);
                    await _dbventaContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", correlative.CantidadDigitos.Value));
                    string numeroVenta = ceros + correlative.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - correlative.CantidadDigitos.Value, correlative.CantidadDigitos.Value);

                    entity.NumeroVenta = numeroVenta;

                    await _dbventaContext.Venta.AddAsync(entity);
                    await _dbventaContext.SaveChangesAsync();

                    newVenta = entity;
                    transaction.Commit();

                }
                catch (Exception ex){ 
                transaction.Rollback();
                    throw ex;
                }
            }

            return newVenta;

        }

        public async Task<List<DetalleVenta>> Report(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleVenta> listResume = await _dbventaContext.DetalleVenta
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(tdv => tdv.IdTipoDocumentoVentaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date &&
                    dv.IdVentaNavigation.FechaRegistro.Value.Date <= FechaFin.Date).ToListAsync();

            return listResume;
        }
    }
}
