using Dapper;
using HelperDrone.Contracts.Repositories;
using HelperDrone.Models;
using System.Data;

namespace HelperDrone.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _dbConnection;

        public UsuarioRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Usuario> ObterTodos()
        {
            const string sql = @"SELECT id_usuario AS IdUsuario,
                                    nome,
                                    email,
                                    senha_hash AS SenhaHash,
                                    nivel_acesso AS NivelAcesso,
                                    status,
                                    data_criacao AS DataCriacao,
                                    data_atualizacao AS DataAtualizacao
                             FROM Usuario";
            return _dbConnection.Query<Usuario>(sql).ToList();
        }

        public Usuario? ObterPorId(int usuarioId)
        {
            const string sql = @"SELECT id_usuario AS IdUsuario,
                                    nome,
                                    email,
                                    senha_hash AS SenhaHash,
                                    nivel_acesso AS NivelAcesso,
                                    status,
                                    data_criacao AS DataCriacao,
                                    data_atualizacao AS DataAtualizacao
                             FROM Usuario
                             WHERE id_usuario = :IdUsuario";
            return _dbConnection.QueryFirstOrDefault<Usuario>(sql, new { IdUsuario = usuarioId });
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            const string sql = @"
            INSERT INTO Usuario (nome, email, senha_hash, nivel_acesso, status, data_atualizacao)
            VALUES (:Nome, :Email, :SenhaHash, :NivelAcesso, :Status, :DataAtualizacao)";
            _dbConnection.Execute(sql, usuario);
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            const string sql = @"
            UPDATE Usuario
            SET nome = :Nome,
                email = :Email,
                senha_hash = :SenhaHash,
                nivel_acesso = :NivelAcesso,
                status = :Status,
                data_atualizacao = :DataAtualizacao
            WHERE id_usuario = :IdUsuario";
            _dbConnection.Execute(sql, usuario);
        }
    }
}
