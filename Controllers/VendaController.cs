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
            clientes = clientes.Where(p => p.Nome.Contains(busca));
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
            produtos = produtos.Where(p => p.Nome.Contains(busca));
        }
        var resultado = produtos
            .OrderBy(c => c.Nome)
            .Take(10)
            .Select(c => new { id = c.Id, nome = c.Nome })
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
        if (ModelState.IsValid)
        {
            var venda = new Venda
            {
                ClienteId = viewModel.ClienteId!.Value,
                ProdutoId = viewModel.ProdutoId!.Value,
                ValorFinalVenda = viewModel.ValorFinalVenda,
                NfSaida = viewModel.NfSaida,
                FormaPagamento = viewModel.FormaPagamento,
                DataAtivacao = viewModel.DataAtivacao,
                ExpiraEm = viewModel.ExpiraEm,
            };

            _context.Vendas.Add(venda);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        return View(viewModel);
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
}
