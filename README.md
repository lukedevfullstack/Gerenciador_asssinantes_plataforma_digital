# Subscriber Management API 🚀

API RESTful desenvolvida em **.NET 9** para o gerenciamento de assinantes de uma plataforma digital. O projeto foi construído seguindo princípios de **Clean Architecture**, **DDD (Domain-Driven Design)** e **SOLID**.

---

## 🛠️ Tecnologias e Frameworks
* **Runtime:** .NET 9.0
* **Banco de Dados:** MySQL (Produção/Desenvolvimento)
* **ORM:** Entity Framework Core
* **Testes:** xUnit, Moq, SQLite (In-Memory para Integração)
* **Documentação:** Swagger (OpenAPI)

---

## 🏗️ Arquitetura e Padrões
O projeto está dividido em camadas para garantir a separação de responsabilidades:

1.  **Domain**: Contém a Entidade Rica `Subscriber`, Enums e interfaces de repositório. Toda a regra de negócio (validações e cálculos) está centralizada aqui.
2.  **Application**: Serviços de aplicação, DTOs e mapeamentos.
3.  **Infrastructure**: Implementação do `AppDbContext`, Repositórios e Migrations.
4.  **API**: Controllers e configuração de Injeção de Dependência.

---

## ⚙️ Configuração do Ambiente

### 1. Banco de Dados (MySQL)
No arquivo `appsettings.json` do projeto **API**, ajuste a connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=SubscriberDB;Uid=seu_usuario;Pwd=sua_senha;"
}


## ⚙️ Configuração e Execução

### 2. Migrations
Execute o comando abaixo na raiz da solução para criar a estrutura do banco de dados automaticamente:
```bash
dotnet ef database update --project Infrastructure --startup-project API

### 3. Execução
Para rodar a aplicação, utilize o comando:

```bash
dotnet run --project API

## Após iniciar, acesse a documentação interativa pelo Swagger no endereço:
```https://localhost:[PORTA]/swagger


### 🧪 Testes Automatizados
O projeto conta com uma suíte de testes que cobre:

Testes Unitários: Validação de regras de negócio (E-mail único, data futura, valor positivo).

Testes de Integração: Persistência real em banco de dados utilizando SQLite In-Memory.

Para rodar os testes:

```Bash
dotnet test

### 📌 Regras de Negócio Implementadas
Cálculo Dinâmico: O tempo de assinatura é calculado em tempo de execução e nunca retorna 0.

Soft Delete: A operação de desativar apenas altera o status IsActive, mantendo o histórico.

Filtros de Atividade: Listagem e edição filtram automaticamente apenas assinantes ativos.

Domínio Rico: A entidade se auto-valida no construtor, impedindo estados inválidos no sistema.

## 🛣️ Endpoints Principais

| Verbo | Endpoint | Descrição |
| :--- | :--- | :--- |
| **POST** | `/api/subscribers` | Cria um novo assinante |
| **GET** | `/api/subscribers` | Lista todos os assinantes ativos |
| **GET** | `/api/subscribers/{id}` | Detalhes de um assinante específico |
| **PUT** | `/api/subscribers/{id}` | Atualiza dados de um assinante |
| **PATCH** | `/api/subscribers/{id}/deactivate` | Desativa um assinante  |
| **DELETE** | `/api/subscribers/{id}` | Exclusão do registro |