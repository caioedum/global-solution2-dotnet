using Dapper;
using HelperDrone.Contracts.Repositories;
using HelperDrone.Models;
using System.Data;

namespace HelperDrone.Repositories
{
    public class AlertaRepository : IAlertaRepository
    {

        private readonly IDbConnection _dbConnection;

        public AlertaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Alerta> ObterTodosAlertas()
        {
            const string sql = @"SELECT id_alerta AS IdAlerta,
                                        tipo_alerta AS TipoAlerta,
                                        data_hora AS DataHora,
                                        status AS Status,
                                        id_area AS IdArea,
                                        id_drone AS IdDrone,
                                        id_usuario AS IdUsuario,
                                        gravidade AS Gravidade,
                                        descricao AS Descricao
                                 FROM Alerta";
            return _dbConnection.Query<Alerta>(sql).ToList();
        }

        public Alerta? ObterAlertaPorId(int alertaId)
        {
            const string sql = @"SELECT id_alerta AS IdAlerta,
                                        tipo_alerta AS TipoAlerta,
                                        data_hora AS DataHora,
                                        status AS Status,
                                        id_area AS IdArea,
                                        id_drone AS IdDrone,
                                        id_usuario AS IdUsuario,
                                        gravidade AS Gravidade,
                                        descricao AS Descricao
                                 FROM Alerta
                                 WHERE id_alerta = :AlertaId";
            return _dbConnection.QueryFirstOrDefault<Alerta>(sql, new { AlertaId = alertaId });
        }

        public void AdicionarAlerta(Alerta alerta)
        {
            const string sql = @"
                INSERT INTO Alerta (tipo_alerta, status, id_area, id_drone, 
                                  id_usuario, gravidade, descricao)
                VALUES (:TipoAlerta, :Status, :IdArea, :IdDrone, 
                        :IdUsuario, :Gravidade, :Descricao)";
            _dbConnection.Execute(sql, alerta);
        }

        public void AtualizarAlerta(Alerta alerta)
        {
            const string sql = @"
                UPDATE Alerta SET
                    tipo_alerta = :TipoAlerta,
                    status = :Status,
                    id_area = :IdArea,
                    id_drone = :IdDrone,
                    id_usuario = :IdUsuario,
                    gravidade = :Gravidade,
                    descricao = :Descricao
                WHERE id_alerta = :IdAlerta";
            _dbConnection.Execute(sql, alerta);
        }

        public void RemoverAlerta(int alertaId)
        {
            const string sql = "DELETE FROM Alerta WHERE id_alerta = :AlertaId";
            _dbConnection.Execute(sql, new { AlertaId = alertaId });
        }

        public List<Alerta> ObterAlertasPorAreaRisco(int areaRiscoId)
        {
            const string sql = @"SELECT id_alerta AS IdAlerta,
                                        tipo_alerta AS TipoAlerta,
                                        data_hora AS DataHora,
                                        status AS Status,
                                        id_area AS IdArea,
                                        id_drone AS IdDrone,
                                        id_usuario AS IdUsuario,
                                        gravidade AS Gravidade,
                                        descricao AS Descricao
                                 FROM Alerta
                                 WHERE id_area = :AreaRiscoId";
            return _dbConnection.Query<Alerta>(sql, new { AreaRiscoId = areaRiscoId }).ToList();
        }

        public List<Alerta> ObterAlertasPorDrone(int droneId)
        {
            const string sql = @"SELECT id_alerta AS IdAlerta,
                                        tipo_alerta AS TipoAlerta,
                                        data_hora AS DataHora,
                                        status AS Status,
                                        id_area AS IdArea,
                                        id_drone AS IdDrone,
                                        id_usuario AS IdUsuario,
                                        gravidade AS Gravidade,
                                        descricao AS Descricao
                                 FROM Alerta
                                 WHERE id_drone = :DroneId";
            return _dbConnection.Query<Alerta>(sql, new { DroneId = droneId }).ToList();
        }

        public List<Alerta> ObterAlertasPorUsuario(int usuarioId)
        {
            const string sql = @"SELECT id_alerta AS IdAlerta,
                                        tipo_alerta AS TipoAlerta,
                                        data_hora AS DataHora,
                                        status AS Status,
                                        id_area AS IdArea,
                                        id_drone AS IdDrone,
                                        id_usuario AS IdUsuario,
                                        gravidade AS Gravidade,
                                        descricao AS Descricao
                                 FROM Alerta
                                 WHERE id_usuario = :UsuarioId";
            return _dbConnection.Query<Alerta>(sql, new { UsuarioId = usuarioId }).ToList();
        }
    }
}
