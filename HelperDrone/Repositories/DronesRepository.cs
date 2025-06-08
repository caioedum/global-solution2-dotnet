using Dapper;
using HelperDrone.Contracts.Repositories;
using HelperDrone.Models;
using System.Data;

namespace HelperDrone.Repositories
{
    public class DroneRepository : IDroneRepository
    {
        private readonly IDbConnection _dbConnection;

        public DroneRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Drone> ObterTodos()
        {
            const string sql = @"SELECT id_drone AS IdDrone,
                                        nome AS Nome,
                                        modelo AS Modelo,
                                        status AS Status,
                                        latitude AS Latitude,
                                        longitude AS Longitude,
                                        bateria AS Bateria,
                                        capacidade_carga AS CapacidadeCarga,
                                        data_ultima_manutencao AS DataUltimaManutencao,
                                        horario_operacao AS HorarioOperacao,
                                        data_cadastro AS DataCadastro
                                 FROM Drone";
            return _dbConnection.Query<Drone>(sql).ToList();
        }

        public Drone? ObterPorId(int droneId)
        {
            const string sql = @"SELECT id_drone AS IdDrone,
                                        nome AS Nome,
                                        modelo AS Modelo,
                                        status AS Status,
                                        latitude AS Latitude,
                                        longitude AS Longitude,
                                        bateria AS Bateria,
                                        capacidade_carga AS CapacidadeCarga,
                                        data_ultima_manutencao AS DataUltimaManutencao,
                                        horario_operacao AS HorarioOperacao,
                                        data_cadastro AS DataCadastro
                                 FROM Drone
                                 WHERE id_drone = :DroneId";
            return _dbConnection.QueryFirstOrDefault<Drone>(sql, new { DroneId = droneId });
        }

        public void AdicionarDrone(Drone drone)
        {
            const string sql = @"
                INSERT INTO Drone (nome, modelo, status, latitude, longitude, 
                                  bateria, capacidade_carga, data_ultima_manutencao, horario_operacao)
                VALUES (:Nome, :Modelo, :Status, :Latitude, :Longitude, 
                        :Bateria, :CapacidadeCarga, :DataUltimaManutencao, :HorarioOperacao)";
            _dbConnection.Execute(sql, drone);
        }

        public void AtualizarDrone(Drone drone)
        {
            const string sql = @"
                UPDATE Drone SET
                    nome = :Nome,
                    modelo = :Modelo,
                    status = :Status,
                    latitude = :Latitude,
                    longitude = :Longitude,
                    bateria = :Bateria,
                    capacidade_carga = :CapacidadeCarga,
                    data_ultima_manutencao = :DataUltimaManutencao,
                    horario_operacao = :HorarioOperacao
                WHERE id_drone = :IdDrone";
            _dbConnection.Execute(sql, drone);
        }

        public void RemoverDrone(int droneId)
        {
            const string sql = "DELETE FROM Drone WHERE id_drone = :DroneId";
            _dbConnection.Execute(sql, new { DroneId = droneId });
        }

        public List<Drone> ObterDronesDisponiveis()
        {
            const string sql = @"SELECT * FROM Drone 
                                WHERE status = 'Disponível' 
                                ORDER BY data_cadastro DESC";
            return _dbConnection.Query<Drone>(sql).ToList();
        }

        public List<Drone> ObterDronesEmMissao()
        {
            const string sql = @"SELECT * FROM Drone 
                                WHERE status = 'Em Missão' 
                                ORDER BY data_cadastro DESC";
            return _dbConnection.Query<Drone>(sql).ToList();
        }
    }
}
