using EnviosWebApi.Models;
using EnviosWebApi.Repositories.Interfaces;
using EnviosWebApi.Services.Interfaces;

namespace EnviosWebApi.Services.Implementation
{
    public class EnviosService : IEnviosService
    {
        private readonly IEnviosRepository _repository;

        public EnviosService (IEnviosRepository repository)
        {
            _repository = repository;
        }

        public List<TEnvio> GetEnvios()
        {
            return _repository.GetEnvios();
        }
        public List<TEnvio> GetEnviosByDate(DateTime date)
        {
            return _repository.GetEnviosByDate(date);
        }
        public List<TEnvio> GetCancelledEnvios(string state)
        {
            return _repository.GetCancelledEnvios(state);
        }
        public TEnvio GetEnvioByCodigo(int codigo)
        {
            return _repository.GetEnvioByCodigo(codigo);
        }
        public void NewEnvio(TEnvio envio)
        {
            envio.Codigo = 0;
            _repository.NewEnvio(envio);
            _repository.SaveChanges();
        }
        public void DeleteEnvio(TEnvio envio)
        {
            _repository.DeleteEnvio(envio);
            _repository.SaveChanges();
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
