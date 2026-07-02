using System;
using System.ComponentModel.DataAnnotations;
using LicenseGesture.Enums;

namespace LicenseGesture.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Nome é Obrigatório!")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Campo Chave é Obrigatório!")]
    public string Chave { get; set; } = string.Empty;
    public int? QuantDispositivos { get; set; }
    public int? QuantUsuarios { get; set; }
    public decimal? ValorCusto { get; set; }
    public decimal? ValorVenda { get; set; }
    public string? NfEntrada { get; set; }
    public string? Validade { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public TipoProduto? TipoProduto { get; set; }
    public ICollection<Venda> Vendas { get; set; } = [];
}
