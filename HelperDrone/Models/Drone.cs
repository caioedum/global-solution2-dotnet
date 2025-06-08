using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperDrone.Models;

[Table("Drone")]
public class Drone
{
    [Key]
    [Column("id_drone")]
    public int IdDrone { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("modelo")]
    public string Modelo { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column("status")]
    public string? Status { get; set; }

    [Column("latitude")]
    public decimal? Latitude { get; set; }

    [Column("longitude")]
    public decimal? Longitude { get; set; }

    [Column("bateria")]
    public decimal? Bateria { get; set; }

    [Column("capacidade_carga")]
    public decimal? CapacidadeCarga { get; set; }

    [Column("data_ultima_manutencao")]
    public DateTime? DataUltimaManutencao { get; set; }

    [MaxLength(20)]
    [Column("horario_operacao")]
    public string? HorarioOperacao { get; set; }

    [Column("data_cadastro")]
    public DateTime DataCadastro { get; set; }
}
