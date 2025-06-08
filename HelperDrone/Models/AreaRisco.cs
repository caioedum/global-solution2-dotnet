using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperDrone.Models;

[Table("AreaRisco")]
public class AreaRisco
{
    [Key]
    [Column("id_area")]
    public int IdArea { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("nome_area")]
    public string NomeArea { get; set; } = string.Empty;

    [MaxLength(255)]
    [Column("descricao")]
    public string? Descricao { get; set; }

    [Required]
    [Column("latitude")]
    public decimal Latitude { get; set; }

    [Required]
    [Column("longitude")]
    public decimal Longitude { get; set; }

    [MaxLength(20)]
    [Column("status")]
    public string? Status { get; set; }

    [Column("raio_cobertura")]
    public decimal? RaioCobertura { get; set; }

    [Column("data_cadastro")]
    public DateTime DataCadastro { get; set; }
}
