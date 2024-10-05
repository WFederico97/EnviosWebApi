using EnviosWebApi.Models;

namespace EnviosWebApi.Services.Interfaces
{
    public interface IEnviosService
    {
        List<TEnvio> GetEnvios();
        List<TEnvio> GetEnviosByDate(DateTime fechaConsulta);
        List<TEnvio> GetCancelledEnvios(string State);
        TEnvio GetEnvioByCodigo(int codigo);
        void NewEnvio(TEnvio envio);
        void DeleteEnvio(TEnvio envio);
        void SaveChanges();
    }
}
