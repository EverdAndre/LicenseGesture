# CHANGELOG

Todas as alterações relevantes do projeto **LicenseGesture** serão documentadas neste arquivo.

Este projeto segue o padrão de **Versionamento Semântico (SemVer)**.

- **MAJOR (X.0.0)** → Alterações incompatíveis com versões anteriores.
- **MINOR (1.X.0)** → Novas funcionalidades compatíveis.
- **PATCH (1.0.X)** → Correções de erros e pequenos ajustes.

# [1.1.0] — Em desenvolvimento

Nova evolução funcional do cadastro de produtos, controle de disponibilidade,
consulta de credenciais administrativas, navegação e experiência de cadastro.

Esta versão ainda não deve ser considerada publicada ou implantada.

## Adicionado

### Produtos

- Tela `Produto/Details` em modo somente leitura.
- Campos administrativos no Produto:
  - `EmailAdm`;
  - `SenhaAdm`;
  - `Quantidade`.
- Campo para consulta da credencial administrativa utilizada no painel externo
  de gestão de usuários e licenças.
- Exibição da quantidade disponível do produto.
- Preparação da alteração estrutural do banco por migration do Entity Framework.

### Vendas

- Validação de produto ativo ao abrir `Venda/Create`.
- Validação de quantidade no GET de `Venda/Create`.
- Validação definitiva de quantidade no POST de `Venda/Create`.
- Bloqueio da venda quando `Quantidade <= 0`.
- Redução da quantidade do Produto após a gravação da Venda.
- Persistência da Venda e da redução de quantidade no mesmo `SaveChanges()`.
- Retorno de `id`, `nome` e `quantidade` no endpoint `BuscarProdutos`.
- Opção `validarEstoque` adicionada à configuração reutilizável do autocomplete.
- Aviso antecipado quando o usuário tenta selecionar produto sem estoque.
- Preenchimento correto de `ProdutoBusca` e do campo oculto `ProdutoId`.
- Remoção do envio automático do formulário ao selecionar um produto no
  cadastro de Venda.

### Clientes

- Criação do arquivo `tipoPessoa.js`.
- Alternância visual entre os campos CPF e CNPJ conforme `TipoPessoa`.
- Exibição inicial correta ao carregar a página.
- Atualização dos campos ao evento `change` do select.

### Interface e navegação

- Navegação entre `Venda/Details` e `Produto/Details` com `returnUrl`.
- Botão Voltar respeitando a tela de origem.
- Separação consolidada entre telas `Edit` e `Details`.
- Uso de `asp-append-version="true"` nos arquivos estáticos aplicáveis.
- Padronização de mensagens de validação por propriedade com
  `asp-validation-for`.
- Uso de `asp-validation-summary="ModelOnly"` para erros globais da Model.

## Alterado

### Produto

- Produto com quantidade zerada não é transformado automaticamente em inativo.
- Disponibilidade para venda passa a depender de `Quantidade > 0`.
- `Ativo` e disponibilidade de estoque permanecem regras distintas.
- `SenhaAdm` é tratada como credencial consultável de sistema externo, e não
  como senha de autenticação do LicenseGesture.
- O campo visual da senha pode utilizar `type="text"` para facilitar consulta e
  cópia, conforme a finalidade operacional.

### Controllers

- `ProdutoController.Edit` continua recebendo a entidade `Produto` completa e
  utilizando `_context.Produtos.Update(produto)`.
- Os novos campos da Model são persistidos automaticamente quando enviados pela
  View.
- Retornos por falha de validação devem preservar a Model preenchida.
- A verificação de estoque no JavaScript não substitui a validação do servidor.

## 1.1.0 - 20/07/2026

### Adicionado

- Tela Produto/Details para consulta de produtos.
- Navegação entre entidades utilizando returnUrl.
- Separação arquitetural entre telas de consulta (Details) e edição (Edit).

### Alterado

- Venda/Details passa a abrir Produto/Details.
- Botão Voltar reutiliza automaticamente o returnUrl quando informado.
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