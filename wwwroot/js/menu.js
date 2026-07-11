const menuButton = document.getElementById("menuButton");
const menuLateral = document.getElementById("menuLateral");
const menuCloseButton = document.getElementById("menuCloseButton");
const menuOverlay = document.getElementById("menuOverlay");

if (menuButton && menuLateral && menuCloseButton && menuOverlay) {
    const definirEstadoMenu = function (aberto) {
        menuLateral.classList.toggle("aberto", aberto);
        menuOverlay.classList.toggle("aberto", aberto);
        document.body.classList.toggle("menu-aberto", aberto);
        menuButton.setAttribute("aria-expanded", aberto.toString());
        menuLateral.setAttribute("aria-hidden", (!aberto).toString());
        menuOverlay.setAttribute("aria-hidden", (!aberto).toString());
    };

    menuButton.addEventListener("click", function () {
        definirEstadoMenu(!menuLateral.classList.contains("aberto"));
    });

    menuCloseButton.addEventListener("click", function () {
        definirEstadoMenu(false);
        menuButton.focus();
    });

    menuOverlay.addEventListener("click", function () {
        definirEstadoMenu(false);
    });

    document.addEventListener("keydown", function (event) {
        if (event.key === "Escape" && menuLateral.classList.contains("aberto")) {
            definirEstadoMenu(false);
            menuButton.focus();
        }
    });
}
