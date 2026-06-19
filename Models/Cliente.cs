using System;
using System.ComponentModel.DataAnnotations;
using LicenseGesture.Enums;

namespace LicenseGesture.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = string.Empty;
    public string? Cnpj { get; set; }
    public string? Cpf { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Endereco { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.Now;

    [Required]
    public TipoPessoa TipoPessoa { get; set; }
    public ICollection<Venda> Vendas { get; set; } = [];
}
