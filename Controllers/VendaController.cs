using LicenseGesture.Context;
using LicenseGesture.Models;
using LicenseGesture.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class VendaController : Controller
{
    private readonly LicenseDbContext _context;

    public VendaController(LicenseDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var vendas = _context.Vendas.ToList();
        return View(vendas);
    }

    // BuscarCliente : Venda/Create
    public IActionResult BuscarClientes(string busca)
    {
        var clientes = _context.Clientes.Where(p => p.Ativo).AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            busca = busca.Trim();
            clientes = clientes.Where(p => EF.Functions.Like(p.Nome, $"%{busca}%"));
        }

        var resultado = clientes
            .OrderBy(c => c.Nome)
            .Take(10)
            .Select(c => new { id = c.Id, nome = c.Nome })
            .ToList();

        return Json(resultado);
    }

    // BuscarProduto : Venda/Create
    public IActionResult BuscarProdutos(string busca)
    {
        var produtos = _context.Produtos.Where(p => p.Ativo).AsQueryable();
        if (!string.IsNullOrWhiteSpace(busca))
        {
            busca = busca.Trim();
            produtos = produtos.Where(p => EF.Functions.Like(p.Nome, $"%{busca}%"));
        }
        var resultado = produtos
            .OrderBy(p => p.Nome)
            .Take(10)
            .Select(p => new { id = p.Id, nome = p.Nome, quantidade = p.Quantidade })
            .ToList();

        return Json(resultado);
    }

    // Get : Venda/Create
    public IActionResult Create(int? produtoId)
    {
        var viewModel = new VendaCreateViewModel();
        if (produtoId.HasValue)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == produtoId.Value && p.Ativo);
            if (produto == null)
            {
                return NotFound("Produto não Encontrado");
            }
            else if(produto.Quantidade <= 0)
            {
                return BadRequest("Produto sem Estoque para Venda");
            }
            viewModel.ProdutoId = produto.Id;
            viewModel.ProdutoBusca = produto.Nome;
        }
        return View(viewModel);
    }

    // Post : Venda/Create
    [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(VendaCreateViewModel viewModel)
{
    if (!ModelState.IsValid)
    {
        return View(viewModel);
    }

    var produto = _context.Produtos
        .FirstOrDefault(p =>
            p.Id == viewModel.ProdutoId!.Value &&
            p.Ativo
        );

    if (produto == null)
    {
        ModelState.AddModelError(
            nameof(viewModel.ProdutoId),
            "Produto não encontrado ou inativo."
        );

        return View(viewModel);
    }

    if (produto.Quantidade <= 0)
    {
        ModelState.AddModelError(
            nameof(viewModel.ProdutoId),
            "Produto sem estoque disponível para venda."
        );

        viewModel.ProdutoBusca = produto.Nome;

        return View(viewModel);
    }

    var venda = new Venda
    {
        ClienteId = viewModel.ClienteId!.Value,
        ProdutoId = produto.Id,
        ValorFinalVenda = viewModel.ValorFinalVenda,
        NfSaida = viewModel.NfSaida,
        FormaPagamento = viewModel.FormaPagamento,
        DataAtivacao = viewModel.DataAtivacao,
        ExpiraEm = viewModel.ExpiraEm
    };

    produto.Quantidade--;

    _context.Vendas.Add(venda);
    _context.SaveChanges();

    return RedirectToAction("Index", "Home");
}

    // Get : Venda/Details
    public IActionResult Details(int Id)
    {
        var venda = _context
            .Vendas.Include(v => v.Cliente)
            .Include(v => v.Produto)
            .FirstOrDefault(v => v.Id == Id);

        if (venda == null)
        {
            return NotFound("Venda não encontrado.");
        }
        return View(venda);
    }

    // Get: Venda/Delete
    public IActionResult Delete(int Id)
    {
        var venda = _context.Vendas.FirstOrDefault(v => v.Id == Id);
        if (venda == null)
        {
            return NotFound("Venda não encontrada");
        }
        if (venda.Anulada)
        {
            return BadRequest("Esta venda já está cancelada.");
        }
        return View(new VendaCancelamentoViewModel { Id = venda.Id });
    }

    // Post: Venda/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(VendaCancelamentoViewModel viewModel)
    {
        var vendaBanco = _context.Vendas.Find(viewModel.Id);

        if (vendaBanco == null)
        {
            return NotFound("Venda não encontrada.");
        }

        if (vendaBanco.Anulada)
        {
            return BadRequest("Esta venda já está cancelada.");
        }

        if (string.IsNullOrWhiteSpace(viewModel.AnuladaPor))
        {
            ModelState.AddModelError(
                nameof(VendaCancelamentoViewModel.AnuladaPor),
                "Informe o responsável pelo cancelamento."
            );
        }

        if (string.IsNullOrWhiteSpace(viewModel.MotivoCancelamento))
        {
            ModelState.AddModelError(
                nameof(VendaCancelamentoViewModel.MotivoCancelamento),
                "Informe o motivo do cancelamento."
            );
        }

        if (!ModelState.IsValid)
        {
            return View("Delete", viewModel);
        }

        vendaBanco.Anulada = true;
        vendaBanco.AnuladaPor = viewModel.AnuladaPor.Trim();
        vendaBanco.MotivoCancelamento = viewModel.MotivoCancelamento.Trim();
        vendaBanco.CanceladaEm = DateTime.Now;
        _context.SaveChanges();

        return RedirectToAction("Details", new { id = vendaBanco.Id });
    }
}
