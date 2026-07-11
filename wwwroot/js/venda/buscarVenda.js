configurarAutocomplete({
    campoBuscaId: "VendaBusca",
    resultadoId: "vendaBuscaResultado",
    fontes: [
        { url: "/Venda/BuscarClientes", tipo: "Cliente" },
        { url: "/Venda/BuscarProdutos", tipo: "Produto" }
    ],
    mensagemVazia: "Nenhum cliente ou produto encontrado",
    enviarAoSelecionar: true
});
const formularioBusca = document.querySelector(".venda-search-form");
const campoBuscaVenda = document.getElementById("VendaBusca");

if (formularioBusca && campoBuscaVenda) {
    formularioBusca.addEventListener("submit", function () {
        sessionStorage.setItem("limparBuscaVenda", "true");
    });

    if (sessionStorage.getItem("limparBuscaVenda") === "true") {
        campoBuscaVenda.value = "";
        sessionStorage.removeItem("limparBuscaVenda");
    }
}