using System;
using System.ComponentModel.DataAnnotations;
using LicenseGesture.Enums;

namespace LicenseGesture.Models;

public class Venda
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    [Required]
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;
    public decimal? ValorFinalVenda { get; set; }
    public string? NfSaida { get; set; }
    public string? FormaPagamento { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public DateOnly? DataAtivacao { get; set; }
    public DateOnly ExpiraEm { get; set; }
}
