function configurarAutocompleteVenda(config) {
    const campoBusca = document.getElementById(config.campoBuscaId);
    const campoId = document.getElementById(config.campoId);
    const resultado = document.getElementById(config.resultadoId);

    if (!campoBusca || !campoId || !resultado) {
        return;
    }

    let itens = [];
    let indiceSelecionado = -1;
    let controleBusca = null;

    const limparResultado = function () {
        resultado.replaceChildren();
        resultado.classList.remove("aberto");
        campoBusca.setAttribute("aria-expanded", "false");
        indiceSelecionado = -1;
    };

    const selecionarItem = function (index) {
        const item = itens[index];

        if (!item) {
            return;
        }

        campoBusca.value = item.nome;
        campoId.value = item.id;
        limparResultado();
    };

    const atualizarSelecao = function () {
        const opcoes = resultado.querySelectorAll(".autocomplete-item");

        opcoes.forEach(function (opcao, index) {
            const selecionado = index === indiceSelecionado;
            opcao.classList.toggle("selecionado", selecionado);
            opcao.setAttribute("aria-selected", selecionado.toString());

            if (selecionado) {
                opcao.scrollIntoView({ block: "nearest" });
            }
        });
    };

    const renderizarItens = function () {
        resultado.replaceChildren();

        if (itens.length === 0) {
            const vazio = document.createElement("div");
            vazio.className = "autocomplete-empty";
            vazio.textContent = config.mensagemVazia;
            resultado.appendChild(vazio);
            resultado.classList.add("aberto");
            campoBusca.setAttribute("aria-expanded", "true");
            return;
        }

        itens.forEach(function (item, index) {
            const opcao = document.createElement("button");
            opcao.type = "button";
            opcao.className = "autocomplete-item";
            opcao.setAttribute("role", "option");
            opcao.setAttribute("aria-selected", "false");
            opcao.textContent = item.nome;

            opcao.addEventListener("mousedown", function (event) {
                event.preventDefault();
                selecionarItem(index);
            });

            resultado.appendChild(opcao);
        });

        resultado.classList.add("aberto");
        campoBusca.setAttribute("aria-expanded", "true");
    };

    campoBusca.setAttribute("aria-autocomplete", "list");
    campoBusca.setAttribute("aria-expanded", "false");
    campoBusca.setAttribute("aria-controls", config.resultadoId);
    resultado.setAttribute("role", "listbox");

    campoBusca.addEventListener("input", async function () {
        const busca = campoBusca.value.trim();

        campoId.value = "";
        itens = [];
        limparResultado();

        if (controleBusca) {
            controleBusca.abort();
        }

        if (busca.length < 2) {
            return;
        }

        controleBusca = new AbortController();

        try {
            const resposta = await fetch(`${config.url}?busca=${encodeURIComponent(busca)}`, {
                signal: controleBusca.signal
            });

            if (!resposta.ok) {
                throw new Error("Falha ao buscar sugestoes.");
            }

            itens = await resposta.json();
            renderizarItens();
        } catch (erro) {
            if (erro.name !== "AbortError") {
                limparResultado();
            }
        }
    });

    campoBusca.addEventListener("keydown", function (event) {
        const opcoes = resultado.querySelectorAll(".autocomplete-item");

        if (event.key === "Escape") {
            limparResultado();
            return;
        }

        if (opcoes.length === 0) {
            return;
        }

        if (event.key === "ArrowDown") {
            event.preventDefault();
            indiceSelecionado = (indiceSelecionado + 1) % opcoes.length;
            atualizarSelecao();
        }

        if (event.key === "ArrowUp") {
            event.preventDefault();
            indiceSelecionado = (indiceSelecionado - 1 + opcoes.length) % opcoes.length;
            atualizarSelecao();
        }

        if (event.key === "Enter" && indiceSelecionado >= 0) {
            event.preventDefault();
            selecionarItem(indiceSelecionado);
        }
    });

    document.addEventListener("click", function (event) {
        if (!resultado.contains(event.target) && event.target !== campoBusca) {
            limparResultado();
        }
    });
}

configurarAutocompleteVenda({
    campoBuscaId: "ClienteBusca",
    campoId: "ClienteId",
    resultadoId: "clienteResultado",
    url: "/Venda/BuscarClientes",
    mensagemVazia: "Nenhum cliente encontrado"
});
