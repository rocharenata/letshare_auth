📚 AuthApi - Autenticação de Usuários em .NET 8

A AuthApi foi desenvolvida para autenticação de usuários utilizando tecnologias como .NET 8, ASP.NET Core, PostgreSQL, BCrypt e JWT.

Esta documentação descreve a criação, organização e instruções de execução da API.

🏛️ Estrutura do Projeto
O projeto foi estruturado para garantir a separação de responsabilidades:

Controllers/: Contém o AuthController, responsável por gerenciar as requisições de autenticação.


Models/: Define as classes de modelo:


User: Representa a tabela tbl_user no banco de dados.


LoginRequest: Representa os dados enviados para autenticação.


Services/: Implementa a lógica de geração de tokens JWT no TokenService.


Data/: Configura o contexto do banco de dados (AppDbContext) para interagir com o PostgreSQL.


appsettings.json: Contém configurações como a string de conexão e a chave secreta para geração dos tokens JWT.


Program.cs: Configura os serviços necessários, autenticação JWT e middlewares.



🛠️ Configuração do Banco de Dados

A conexão com o PostgreSQL é feita através da configuração no appsettings.json, onde são definidos:
String de conexão com o banco.


Chave secreta para assinatura dos tokens.


No Program.cs, o AppDbContext é registrado para trabalhar com o PostgreSQL.
A classe AppDbContext gerencia diretamente a tabela tbl_user, sem alterações em sua estrutura.

🧩 Modelos

User: Representa os usuários cadastrados na tabela tbl_user. Algumas propriedades são decoradas com [NotMapped], para não persistirem no banco.


LoginRequest: Modelo usado para mapear as credenciais enviadas pelo cliente durante o processo de login.


🎯 Controlador

O AuthController expõe o endpoint:
POST /api/auth/token


Valida o grant_type, clientId e clientSecret.


Busca o usuário no banco de dados.


Valida a senha utilizando BCrypt.


Gera e retorna um par de tokens (access_token e refresh_token).



🔒 Serviço de Tokens

O TokenService é responsável por gerar os JWTs:

GenerateAccessToken: Gera um token de acesso válido por 6 minutos, contendo claims opcionais do usuário.

GenerateRefreshToken: Gera um token de atualização válido por 1 hora.


Ambos os tokens são assinados usando a chave secreta definida em appsettings.json, utilizando o algoritmo HMAC-SHA256.

🔑 Configuração do JWT
A autenticação JWT é configurada diretamente no Program.cs, garantindo que todos os endpoints protegidos sejam validados automaticamente.

🚀 Execução do Projeto

1. Restaurar os pacotes
bash
CopiarEditar
dotnet restore

2. Compilar e executar
bash
CopiarEditar
dotnet build
dotnet run

A API ficará disponível no endereço:

 📍 http://localhost:5000

🧪 Testes
Para facilitar a validação dos endpoints:
O arquivo AuthApi.http pode ser usado diretamente no Visual Studio ou Visual Studio Code.


A coleção Postman login-collection.postman_collection.json, localizada na pasta postman/, contém requisições prontas para testes de autenticação.


Como testar:
Importe a collection no Postman.


Execute a requisição Login - .NET após rodar a API.



✅ Conclusão
Esta API foi construída com foco em:
Segurança na autenticação e emissão dos tokens.


Boas práticas de organização de código e separação de responsabilidades.


Facilidade de manutenção e extensibilidade futura.


O desenvolvimento segue todos os requisitos estabelecidos no desafio técnico.
