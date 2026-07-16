# LicenseGesture

Sistema web para gerenciamento de licenças de software, clientes e vendas. O projeto centraliza o cadastro dos dados comerciais, acompanha a validade das licenças e mantém o histórico de vendas e cancelamentos.

## Funcionalidades

- Cadastro, edição, pesquisa e inativação de clientes.
- Suporte a clientes pessoa física e pessoa jurídica.
- Cadastro, edição, pesquisa e inativação de produtos/licenças.
- Registro de chave, tipo do produto, validade, quantidade de usuários e dispositivos, valores e nota fiscal.
- Registro de vendas vinculando cliente e produto.
- Busca assistida de clientes e produtos durante a venda.
- Registro de valor final, forma de pagamento, nota fiscal, ativação e expiração.
- Consulta de vendas com pesquisa e ordenação por cliente, produto, valor e validade.
- Visualização detalhada de cada venda.
- Cancelamento de venda com responsável, motivo e data do cancelamento.
- Interface responsiva para uso em computadores e dispositivos móveis.

## Tecnologias utilizadas

- ASP.NET Core MVC 9
- C# e .NET 9
- Entity Framework Core 9
- SQLite
- Razor Views
- HTML, CSS e JavaScript
- Bootstrap e jQuery

## Pré-requisitos

Antes de executar o projeto, instale:

- [.NET SDK 9](https://dotnet.microsoft.com/download/dotnet/9.0)
- EF Core CLI, caso ainda não esteja disponível:

```bash
dotnet tool install --global dotnet-ef
```

## Como executar

1. Clone o repositório:

```bash
git clone https://github.com/EverdAndre/LicenseGesture.git
cd LicenseGesture
```

2. Restaure as dependências:

```bash
dotnet restore
```

3. Crie ou atualize o banco de dados usando as migrations:

```bash
dotnet ef database update
```

4. Inicie a aplicação:

```bash
dotnet run
```

5. Acesse no navegador:

```text
http://localhost:5038
```

O perfil HTTPS também está configurado em `https://localhost:7279`.

## Banco de dados

O projeto utiliza SQLite. A conexão padrão está definida em `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=licensegesture.db"
}
```

O arquivo `licensegesture.db` é criado na raiz do projeto após a aplicação das migrations. Para gerar uma nova migration depois de alterar os modelos, execute:

```bash
dotnet ef migrations add NomeDaMigration
dotnet ef database update
```

## Estrutura do projeto

```text
LicenseGesture/
├── Context/        # Contexto do Entity Framework Core
├── Controllers/    # Regras e endpoints MVC
├── Enums/          # Tipos de pessoa, produto e pagamento
├── Migrations/     # Histórico de alterações do banco
├── Models/         # Entidades Cliente, Produto e Venda
├── ViewModels/     # Dados específicos das telas de venda
├── Views/          # Páginas Razor organizadas por recurso
└── wwwroot/        # CSS, JavaScript e bibliotecas do frontend
```

## Fluxo básico de uso

1. Cadastre um cliente.
2. Cadastre um produto ou licença.
3. Registre a venda selecionando o cliente e o produto.
4. Consulte a venda na página inicial para acompanhar seus dados e sua validade.
5. Quando necessário, cancele a venda informando o responsável e o motivo.

## Observações

- Clientes e produtos excluídos são inativados, preservando o histórico relacionado.
- Vendas canceladas permanecem registradas para consulta e auditoria.
- O banco SQLite local não deve ser versionado com dados reais ou sensíveis.

