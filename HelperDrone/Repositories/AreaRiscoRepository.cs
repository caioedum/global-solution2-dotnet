using HelperDrone.Models;
using System.Data;
using Dapper;
using HelperDrone.Contracts.Repositories;

namespace HelperDrone.Repositories
{
    public class AreaRiscoRepository : IAreaRiscoRepository
    {
        private readonly IDbConnection _dbConnection;

        public AreaRiscoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<AreaRisco> ObterTodasAreasRisco()
        {
            const string sql = @"SELECT id_area AS IdArea,
                                        nome_area AS NomeArea,
                                        descricao AS Descricao,
                                        latitude AS Latitude,
                                        longitude AS Longitude,
                                        status AS Status,
                                        raio_cobertura AS RaioCobertura,
                                        data_cadastro AS DataCadastro
                                 FROM AreaRisco";
            return _dbConnection.Query<AreaRisco>(sql).ToList();
        }

        public AreaRisco? ObterAreaRiscoPorId(int areaRiscoId)
        {
            const string sql = @"SELECT id_area AS IdArea,
                                        nome_area AS NomeArea,
                                        descricao AS Descricao,
                                        latitude AS Latitude,
                                        longitude AS Longitude,
                                        status AS Status,
                                        raio_cobertura AS RaioCobertura,
                                        data_cadastro AS DataCadastro
                                 FROM AreaRisco
                                 WHERE id_area = :AreaRiscoId";
            return _dbConnection.QueryFirstOrDefault<AreaRisco>(sql, new { AreaRiscoId = areaRiscoId });
        }

        public void AdicionarAreaRisco(AreaRisco areaRisco)
        {
            const string sql = @"
                INSERT INTO AreaRisco (nome_area, descricao, latitude, longitude, status, raio_cobertura)
                VALUES (:NomeArea, :Descricao, :Latitude, :Longitude, :Status, :RaioCobertura)";
            _dbConnection.Execute(sql, areaRisco);
        }

        public void AtualizarAreaRisco(AreaRisco areaRisco)
        {
            const string sql = @"
                UPDATE AreaRisco SET
                    nome_area = :NomeArea,
                    descricao = :Descricao,
                    latitude = :Latitude,
                    longitude = :Longitude,
                    status = :Status,
                    raio_cobertura = :RaioCobertura
                WHERE id_area = :IdArea";
            _dbConnection.Execute(sql, areaRisco);
        }

        public void RemoverAreaRisco(int areaRiscoId)
        {
            const string sql = "DELETE FROM AreaRisco WHERE id_area = :AreaRiscoId";
            _dbConnection.Execute(sql, new { AreaRiscoId = areaRiscoId });
        }
    }
}
