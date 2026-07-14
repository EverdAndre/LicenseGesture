using System.ComponentModel.DataAnnotations;

namespace LicenseGesture.ViewModels;

public class VendaCancelamentoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o responsável pelo cancelamento.")]
    [StringLength(100, ErrorMessage = "O responsável deve ter no máximo 100 caracteres.")]
    [Display(Name = "Responsável pelo cancelamento")]
    public string AnuladaPor { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o motivo do cancelamento.")]
    [StringLength(500, ErrorMessage = "O motivo deve ter no máximo 500 caracteres.")]
    [Display(Name = "Motivo do cancelamento")]
    public string MotivoCancelamento { get; set; } = string.Empty;
}
