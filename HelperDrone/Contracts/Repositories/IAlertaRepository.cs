using HelperDrone.Models;

namespace HelperDrone.Contracts.Repositories
{
    public interface IAlertaRepository
    {
        public List<Alerta> ObterTodosAlertas();
        public Alerta? ObterAlertaPorId(int alertaId);
        public void AdicionarAlerta(Alerta alerta);
        public void AtualizarAlerta(Alerta alerta);
        public void RemoverAlerta(int alertaId);
        public List<Alerta> ObterAlertasPorAreaRisco(int areaRiscoId);
        public List<Alerta> ObterAlertasPorDrone(int droneId);
        public List<Alerta> ObterAlertasPorUsuario(int usuarioId);
    }
}
