# 🌱 CRM Operacional de Produtos Sustentáveis

O **CRM Operacional de Produtos Sustentáveis** é um sistema desenvolvido como **Trabalho de Conclusão de Curso (TCC)** do curso de **Engenharia da Computação** da **Faculdade de Engenharia de Sorocaba (FACENS)**.

O projeto tem como objetivo demonstrar a aplicação de **conceitos de engenharia de software**, **integração entre front-end e back-end** e **boas práticas de desenvolvimento web**.  
A proposta consiste em um **sistema CRM voltado à gestão operacional de leads, pedidos e pagamentos** de produtos sustentáveis.

O sistema permite:
- O cadastro e autenticação de leads;  
- A montagem de pedidos com seleção de produtos;  
- A simulação de pagamentos via cartão de crédito;  
- O acompanhamento e atualização do status do pedido e do cliente.

## ⚙️ Tecnologias Utilizadas

### 🖥️ **Back-end**
- ASP.NET Core (C#)  
- Entity Framework Core  
- SQL Server  
- Arquitetura MVC (Model–View–Controller)

### 💻 **Front-end**
- Angular  
- Bootstrap  
- TypeScript  

### 🔗 **Integrações e Serviços**
- **API Stripe** — utilizada para **simulação de pagamentos com cartão de crédito**, em ambiente seguro e controlado.

## 🧱 Estrutura do Sistema

O projeto está dividido em duas camadas principais:

1. **API Back-end (ASP.NET Core)**  
   Responsável pela lógica de negócios, persistência de dados e comunicação com o banco.  
   Contém controladores para:
   - Autenticação de leads;  
   - Manipulação de pedidos;  
   - Integração com o serviço Stripe.

2. **Front-end (Angular)**  
   Interface do usuário que permite o fluxo completo de interação:  
   - Login do lead;  
   - Montagem do pedido;  
   - Processamento do pagamento.  

## 💾 Banco de Dados

O sistema utiliza o **SQL Server** como banco de dados relacional, com o **Entity Framework Core** para o mapeamento objeto-relacional.  

### Principais Tabelas:
| Tabela | Descrição |
|--------|------------|
| `tb_Lead` | Armazena informações dos leads (clientes) |
| `tb_Pedido` | Controla os pedidos realizados |
| `tb_PedidoItem` | Contém o catálogo de produtos sustentáveis |
| `tb_Usuario` | Gerencia usuários admins do sistema |


## ▶️ Execução do Projeto

### 🔧 Pré-requisitos
- Node.js 18+  
- Angular CLI  
- .NET 8.0 SDK  
- SQL Server  
- Conta de desenvolvedor Stripe (modo de teste)

### 🚀 Passos para executar

1️⃣ **Clonar o repositório**

git clone https://github.com/seuusuario/CRM-Operacional.git


O projeto utiliza o Entity Framework Core com o padrão Code First, ou seja, o banco de dados e suas tabelas são criados automaticamente a partir das classes de modelo existentes no projeto back-end.
No diretório backend, abra o arquivo appsettings.json.<br>
Localize a seção ConnectionStrings e altere a DefaultConnection para corresponder à sua instância local do SQL Server, conforme o exemplo abaixo:

```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=CRMOperacionalDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
Após ajustar a conexão, execute o comando abaixo para aplicar as migrations e criar todas as tabelas automaticamente no banco de dados:<br>
```bash
dotnet tool install --global dotnet-ef
```
2️⃣ **Executar o back-end**
```bash
cd backend
```
```bash
dotnet run
```

3️⃣ **Executar o front-end**
```bash
cd frontend
```
```bash
npm install
```
```bash
ng serve
```
