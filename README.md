# API de Autenticação - Desafio Técnico

Este projeto é uma API de autenticação desenvolvida em **.NET 8** e **ASP.NET Core**, como parte de um desafio técnico.

O objetivo é realizar a autenticação de usuários existentes no banco de dados **PostgreSQL**, validando senhas criptografadas com **BCrypt** e gerando tokens **JWT** (access_token e refresh_token).

---

## ⚙️ Tecnologias Utilizadas

- .NET 8
- ASP.NET Core
- PostgreSQL
- Entity Framework Core
- BCrypt.Net
- JWT (JSON Web Tokens)

---

## 🎯 Funcionalidades Implementadas

- Recebimento de credenciais (`username` e `password`) via **grant_type=password**.
- Consulta à tabela `tbl_user` no banco PostgreSQL.
- Validação segura da senha utilizando **BCrypt**.
- Geração de dois tokens JWT:
  - **access_token** (válido por 6 minutos)
  - **refresh_token** (válido por 1 hora)
- Assinatura dos tokens utilizando **HMAC-SHA256** com chave secreta do `appsettings.json`.
- Retorno de:
  - `200 OK` com tokens em caso de autenticação válida.
  - `401 Unauthorized` em caso de credenciais inválidas.

---

## 📂 Estrutura do Projeto

- **Controllers**: Lida com a autenticação (`AuthController`).
- **Models**: Entidades `User` e `LoginRequest`.
- **Services**: Geração de tokens (`TokenService`).
- **Data**: Configuração do `AppDbContext`.

---

## 🗄️ Banco de Dados

- O projeto se conecta a um banco de dados PostgreSQL.
- **Importante**:  
  Não foi permitido alterar a estrutura da tabela `tbl_user`.  
  Algumas propriedades do modelo `User` foram marcadas como `[NotMapped]` para respeitar essa regra.

---

## 📑 Observações

- O banco de dados fornecido possui um usuário (`assessment@letshare-test.com`) cuja senha criptografada não corresponde à senha `devtest2025` fornecida nos testes.
- Como não é permitido alterar os dados no banco, a API corretamente retorna `401 Unauthorized` quando as credenciais não batem.
- A geração dos tokens JWT foi implementada e funciona corretamente quando credenciais válidas são fornecidas.

---

## 🔥 Como Executar

1. Clonar o repositório:

```bash
git clone https://github.com/rocharenata/letshare_auth

Restaurar os pacotes:
dotnet restore

Rodar o projeto:
dotnet build
dotnet run

A API estará disponível em:
http://localhost:5000


🧠 Considerações Finais
O projeto foi desenvolvido respeitando todas as regras e limitações estabelecidas pelo desafio técnico, focando em segurança, boas práticas e organização do código.
