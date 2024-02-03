namespace TestingDBVenta.Models.Interfaces
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta> Register(Venta entity);
        Task<List<DetalleVenta>> Report(DateTime FechaInicio, DateTime FechaFin);
    }
}
