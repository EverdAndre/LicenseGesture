# CHANGELOG

Todas as alterações relevantes do projeto **LicenseGesture** serão documentadas neste arquivo.

Este projeto segue o padrão de **Versionamento Semântico (SemVer)**.

- **MAJOR (X.0.0)** → Alterações incompatíveis com versões anteriores.
- **MINOR (1.X.0)** → Novas funcionalidades compatíveis.
- **PATCH (1.0.X)** → Correções de erros e pequenos ajustes.

---

# [1.0.1] - 17/07/2026

## Corrigido

- Corrigida a pesquisa de clientes para não diferenciar letras maiúsculas e minúsculas.
- Corrigida a pesquisa de produtos para não diferenciar letras maiúsculas e minúsculas.
- Padronizado o comportamento entre o autocomplete e as pesquisas das listagens.
- Corrigido o funcionamento dos botões de busca nas telas de Clientes e Produtos.
- Padronizadas as consultas utilizando `EF.Functions.Like()`.

---

# [1.0.0] - 17/07/2026

## Adicionado

### Estrutura Inicial

- Criação da arquitetura inicial do projeto ASP.NET Core MVC.
- Configuração do Entity Framework Core com SQLite.
- Configuração do Bootstrap e layout principal.
- Estrutura de autenticação.

### Cadastros

- Cadastro de Clientes.
- Cadastro de Produtos.
- Cadastro de Usuários.
- Cadastro de Licenças.

### Movimentações

- Registro de Vendas.
- Associação entre Cliente, Produto e Licença.

### Consultas

- Listagem de Clientes.
- Listagem de Produtos.
- Listagem de Licenças.
- Listagem de Vendas.

### Recursos

- Pesquisa por texto.
- Ordenação das listagens.
- Exclusão lógica de registros.
- Controle de registros ativos.

### Infraestrutura

- Banco SQLite.
- Publicação para Windows.
- Estrutura para execução via IIS.