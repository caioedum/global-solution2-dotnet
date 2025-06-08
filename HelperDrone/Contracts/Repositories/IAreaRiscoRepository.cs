using HelperDrone.Models;

namespace HelperDrone.Contracts.Repositories
{
    public interface IAreaRiscoRepository
    {
        public List<AreaRisco> ObterTodasAreasRisco();
        public AreaRisco? ObterAreaRiscoPorId(int areaRiscoId);
        public void AdicionarAreaRisco(AreaRisco areaRisco);
        public void AtualizarAreaRisco(AreaRisco areaRisco);
        public void RemoverAreaRisco(int areaRiscoId);
    }
}
