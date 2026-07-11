using System.ComponentModel.DataAnnotations;
using LicenseGesture.Enums;

namespace LicenseGesture.ViewModels;

public class VendaCreateViewModel
{
    [Required(ErrorMessage = "Digite o Nome do Cliente")]
    public int? ClienteId { get; set; }
    public string? ClienteBusca { get; set; }

    [Required(ErrorMessage = "Digite um Produto")]
    public int? ProdutoId { get; set; }
    public string? ProdutoBusca { get; set; }
    public decimal? ValorFinalVenda { get; set; }

    public string? NfSaida { get; set; }

    public FormaPagamento? FormaPagamento { get; set; }

    public DateOnly? DataAtivacao { get; set; }
    public DateOnly ExpiraEm { get; set; }
    public bool Anulada { get; set; } = false;
    public string? MotivoCancelamento {get; set;}
    public string? AnuladaPor {get; set;}
    public DateTime? CanceladaEm { get; set; } = DateTime.Now;
}
