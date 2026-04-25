# GoodHamburger API

API REST para gestão de pedidos de hambúrgueres, desenvolvida em **.NET 8 (LTS)** com foco em **boas práticas, separação de responsabilidades e regras de negócio bem definidas**.


## Tecnologias

* .NET 8 (LTS)
* ASP.NET Core Web API
* Swagger (OpenAPI)
* xUnit (testes)
* Arquitetura em camadas


## Como executar o projeto

### 1. Clonar o repositório

```bash
git clone https://github.com/dev-Jxmes/Dotnet-GoodHamburger-System.git
cd Dotnet-GoodHamburger-System
```


### 2. Restaurar dependências

```bash
dotnet restore
```


### 3. Executar a aplicação

```bash
dotnet run
```

ou (modo desenvolvimento):

```bash
dotnet watch run
```


## Acessar a API

Após iniciar, acesse:

 https://localhost:5197/swagger


## Endpoints

### Cardápio

* `GET /api/cardapio` → Lista todos os itens disponíveis


### Pedidos

* `POST /api/pedidos` → Criar novo pedido
* `GET /api/pedidos` → Listar pedidos
* `GET /api/pedidos/{id}` → Consultar pedido por ID
* `DELETE /api/pedidos/{id}` → Remover pedido


## Exemplo de requisição

```json
{
  "itens": ["X Bacon", "Batata frita", "Refrigerante"]
}
```


## Regras de negócio

* Sanduíche + Batata + Refrigerante → **20% de desconto**
* Sanduíche + Refrigerante → **15% de desconto**
* Sanduíche + Batata → **10% de desconto**
* Apenas **1 item por categoria** (não permite duplicados)
* Pedido inválido ou item inexistente retorna erro


## Arquitetura

O projeto segue o padrão de **arquitetura em camadas**:

* **Domain** → Entidades e regras de negócio
* **Application** → Serviços e lógica de aplicação
* **Infrastructure** → Repositórios e implementações técnicas
* **Controllers** → Entrada HTTP (API)
* **Middlewares** → Tratamento global de exceções


## Tratamento de erros

A API possui um middleware global que retorna respostas padronizadas:

```json
{
  "error": "Mensagem de erro",
  "statusCode": 400
}
```


## Decisões técnicas

* Uso de DTOs para desacoplamento da API
* Repositório em memória (simples para MVP)
* Validação centralizada no service
* Normalização de input para evitar erros de comparação
* Uso de exceções customizadas para controle de fluxo


## Melhorias futuras

* Persistência com banco de dados (EF Core)
* Autenticação e autorização
* Testes de integração
* Versionamento da API
* Deploy automatizado (CI/CD)


## Autor

Desenvolvido por **dev-Jxmes**
