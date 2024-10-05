using EnviosWebApi.Models;
using EnviosWebApi.Repositories.Interfaces;

namespace EnviosWebApi.Repositories.Implementations
{
    public class EnvioRepository : IEnviosRepository
    {
        private readonly EnviosContext _context;

        public EnvioRepository (EnviosContext context)
        {
            _context = context;
        }

        public List<TEnvio> GetEnvios()
        {
            return _context.TEnvios.ToList();
        }

        public List<TEnvio> GetEnviosByDate(DateTime date)
        {
            return _context.TEnvios.Where(p=>p.FechaEnvio == date).ToList();
        }

        public List<TEnvio> GetCancelledEnvios(string state)
        {
            return _context.TEnvios.Where(p => p.Estado == state).ToList();
        }
        public TEnvio GetEnvioByCodigo(int codigo)
        {
            return _context.TEnvios.FirstOrDefault(p => p.Codigo == codigo);
        }
        public void NewEnvio(TEnvio envio)
        {
            _context.Add(envio);
        }

        public void DeleteEnvio(TEnvio envio)
        {
            _context.Update(envio);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
