using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperDrone.Models;

[Table("Usuario")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [Column("senha_hash")]
    public string SenhaHash { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column("nivel_acesso")]
    public string? NivelAcesso { get; set; }

    [MaxLength(10)]
    [Column("status")]
    public string? Status { get; set; }

    [Column("data_criacao")]
    public DateTime DataCriacao { get; set; }

    [Column("data_atualizacao")]
    public DateTime? DataAtualizacao { get; set; }
}
