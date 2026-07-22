document.addEventListener("DOMContentLoaded", function () {
    const tipoPessoa = document.getElementById("TipoPessoa");
    const campoCpf = document.getElementById("campoCpf");
    const campoCnpj = document.getElementById("campoCnpj");

    if (!tipoPessoa || !campoCpf || !campoCnpj) {
        return;
    }

    // Valores definidos no enum TipoPessoa: Fisica = 0 e Juridica = 1.
    const tipoFisica = "0";
    const tipoJuridica = "1";

    if (tipoPessoa.dataset.resetOnLoad === "true") {
        tipoPessoa.value = "";
    }

    function atualizarCamposDocumento() {
        const exibirCpf = tipoPessoa.value === tipoFisica;
        const exibirCnpj = tipoPessoa.value === tipoJuridica;

        campoCpf.hidden = !exibirCpf;
        campoCnpj.hidden = !exibirCnpj;
        campoCpf.style.display = exibirCpf ? "flex" : "none";
        campoCnpj.style.display = exibirCnpj ? "flex" : "none";

        campoCpf.querySelector("input").disabled = !exibirCpf;
        campoCnpj.querySelector("input").disabled = !exibirCnpj;
    }

    tipoPessoa.addEventListener("change", atualizarCamposDocumento);
    atualizarCamposDocumento();
});
