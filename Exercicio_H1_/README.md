# Exercício 4 — API com autenticação JWT (ASP.NET Core)

API Web em C# / ASP.NET Core (.NET 10) que faz login com usuário e senha fixos e devolve um
token JWT, que depois é exigido para acessar uma rota protegida com `[Authorize]`.

> Projeto **didático**: não usa banco de dados e o usuário/senha estão fixos no código.

## Estrutura

```
JwtAuthApi.slnx
JwtAuthApi/
├── Configuration/JwtSettings.cs     # Classe com as configurações do JWT (seção "Jwt")
├── Controllers/
│   ├── AuthController.cs            # POST /api/auth/login
│   └── UsuarioController.cs         # GET  /api/usuario/perfil  ([Authorize])
├── Models/
│   ├── LoginRequest.cs              # { "usuario", "senha" }
│   └── LoginResponse.cs             # { "token", "tipo", "expiraEm" }
├── Services/
│   ├── UsuarioService.cs            # Valida as credenciais fixas
│   └── TokenService.cs              # Gera o token JWT (HMAC-SHA256)
├── Program.cs                       # Configuração da autenticação JwtBearer
├── appsettings.json                 # Issuer, Audience, tempo de expiração
├── appsettings.Development.json     # Chave didática usada só em desenvolvimento
└── JwtAuthApi.http                  # Os 4 testes para rodar direto no Visual Studio
postman/
├── JwtAuthApi.postman_collection.json   # Coleção exportada com os 4 testes
└── resultado-newman.txt                 # Evidência da execução da coleção
```

## Credenciais fixas (somente para fins didáticos)

| Usuário | Senha      |
|---------|------------|
| `aluno` | `senha123` |

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download) (ou Visual Studio 2026 com a carga de trabalho "ASP.NET e desenvolvimento Web")
- [Postman](https://www.postman.com/downloads/)

## Como executar

### Visual Studio
1. Abra `JwtAuthApi.slnx` (formato de solução do Visual Studio 2026 / .NET 10).
2. Selecione o perfil **http** na barra de ferramentas e pressione **F5**.
3. A API sobe em `http://localhost:5010`.

### Linha de comando
```bash
dotnet run --project JwtAuthApi --launch-profile http
```

## Sobre a chave secreta

O token é assinado com a chave `Jwt:SecretKey` (mínimo de 32 caracteres).
O `appsettings.Development.json` traz uma chave **fictícia** apenas para o projeto rodar
direto em desenvolvimento — ela não é uma chave real e não deve ser usada em produção.

Para usar sua própria chave sem colocá-la no repositório, use o *User Secrets*:

```bash
dotnet user-secrets init --project JwtAuthApi
```

```bash
dotnet user-secrets set "Jwt:SecretKey" "sua-chave-com-pelo-menos-32-caracteres" --project JwtAuthApi
```

ou a variável de ambiente `Jwt__SecretKey`. Fora do ambiente de desenvolvimento a API não
inicia se a chave não estiver configurada.

## Endpoints

| Método | Rota                   | Autenticação | Descrição                              |
|--------|------------------------|--------------|----------------------------------------|
| POST   | `/api/auth/login`      | Não          | Recebe usuário e senha e retorna o JWT |
| GET    | `/api/usuario/perfil`  | Bearer JWT   | Retorna mensagem com o usuário logado  |

**Login — requisição**
```json
{ "usuario": "aluno", "senha": "senha123" }
```

**Login — resposta 200 OK**
```json
{ "token": "eyJhbGciOiJIUzI1NiIs...", "tipo": "Bearer", "expiraEm": "2026-10-22T23:59:00Z" }
```

**Perfil — resposta 200 OK**
```json
{ "mensagem": "Olá, aluno! Você acessou uma rota protegida com sucesso.", "usuario": "aluno" }
```

## Testes no Postman

1. Com a API rodando, no Postman clique em **Import** e selecione
   `postman/JwtAuthApi.postman_collection.json`.
2. Execute as requisições **na ordem** (ou use **Run collection**). O Teste 1 salva o token
   automaticamente na variável `{{token}}`, usada pelo Teste 4.

| Teste | Requisição | Resultado esperado |
|-------|------------|--------------------|
| 1 — Login válido | `POST /api/auth/login` com `aluno` / `senha123` | **200 OK** + token JWT |
| 2 — Login inválido | `POST /api/auth/login` com senha errada | **401 Unauthorized** |
| 3 — Acesso sem token | `GET /api/usuario/perfil` sem Authorization | **401 Unauthorized** |
| 4 — Acesso com token | `GET /api/usuario/perfil` com **Authorization → Bearer Token** = token do Teste 1 | **200 OK** + mensagem com o nome do usuário |

Para fazer o Teste 4 manualmente: copie o `token` retornado no Teste 1, abra a aba
**Authorization** da requisição, escolha o tipo **Bearer Token** e cole o token no campo *Token*.

### Evidência

A coleção foi executada com o [Newman](https://github.com/postmanlabs/newman) (executor de
linha de comando do Postman) e todos os testes passaram — veja
[`postman/resultado-newman.txt`](postman/resultado-newman.txt):

```
→ Teste 1 - Login válido            [200 OK]           √ Status 200 OK  √ Retorna um token JWT
→ Teste 2 - Login inválido          [401 Unauthorized] √ Status 401 Unauthorized
→ Teste 3 - Acesso sem token        [401 Unauthorized] √ Status 401 Unauthorized
→ Teste 4 - Acesso com token válido [200 OK]           √ Status 200 OK  √ Mensagem contém o nome do usuário
assertions: 6 executadas, 0 falhas
```

Para reproduzir pela linha de comando (com a API rodando):

```bash
npx newman run postman/JwtAuthApi.postman_collection.json
```

Os mesmos 4 testes também estão em `JwtAuthApi/JwtAuthApi.http`, que pode ser executado
direto no Visual Studio.
