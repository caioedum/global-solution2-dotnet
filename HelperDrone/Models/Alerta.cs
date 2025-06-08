using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperDrone.Models;

[Table("Alerta")]
public class Alerta
{
    [Key]
    [Column("id_alerta")]
    public int IdAlerta { get; set; }

    [MaxLength(20)]
    [Column("tipo_alerta")]
    public string? TipoAlerta { get; set; }

    [Column("data_hora")]
    public DateTime DataHora { get; set; }

    [MaxLength(20)]
    [Column("status")]
    public string? Status { get; set; }

    [Required]
    [Column("id_area")]
    public int IdArea { get; set; }

    [Column("id_drone")]
    public int? IdDrone { get; set; }

    [Column("id_usuario")]
    public int? IdUsuario { get; set; }

    [MaxLength(10)]
    [Column("gravidade")]
    public string? Gravidade { get; set; }

    [MaxLength(100)]
    [Column("descricao")]
    public string? Descricao { get; set; }
}
