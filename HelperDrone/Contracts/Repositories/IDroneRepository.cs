using HelperDrone.Models;

namespace HelperDrone.Contracts.Repositories
{
    public interface IDroneRepository
    {
        public List<Drone> ObterTodos();
        public Drone? ObterPorId(int droneId);
        public void AdicionarDrone(Drone drone);
        public void AtualizarDrone(Drone drone);
        public void RemoverDrone(int droneId);
        public List<Drone> ObterDronesDisponiveis();
        public List<Drone> ObterDronesEmMissao();
    }
}
