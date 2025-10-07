﻿# 🎮 FCG.Payments.API

API desenvolvida para gerenciamento de pagamentos, com foco em micro-serviços e arquitetura Hexagonal (Ports and Adapters).
- Hospedada na Azure usando Container Apps e imagem publicada no ACR (Azure Container Registry).
- [Vídeo com a apresentação da Fase 1](https://youtu.be/bmRaU8VjJZU)
- [Vídeo com a apresentação da Fase 2](https://youtu.be/BXBc6JKnRpw)
- [Vídeo com a apresentação da Fase 3]()

## 📌 Objetivo

Desenvolver uma API RESTful robusta e escalável, aplicando:

### **Fase 1:**
  - Domain-Driven Design (DDD) 
  - Clean Architecture 
  - Principios SOLID 
  - Middleware para log de requisições e traces 
  - Middleware de tratamento de erros centralizado
  - Exceptions personalizadas
  - Uso do Entity Framework Core com migrations
  - Autenticação baseada em JWT
  - Autorização baseada em permissões
  - Hash seguro de senhas com salt
  - Validações de domínio e DTOs
  - Testes unitários com TDD 
  - Documententação Swagger com Swashbuckle.AspNetCore
### **Fase 2:**
  - **Escalabilidade:**
    - Utilização de Docker para empacotamento da aplicação em container
    - Versionamento de imagems Docker no ACR 
    - Execução da aplicação em containers orquestrados pelo Azure Container Apps garantindo resiliência
  - **Confiabilidade:**
    - Build, testes unitários e push da imagem Docker via CI/CD multi-stage
    - Parametrização de variáveis e secrets no GitHub Environments
    - Testes de carga e performance utilziando K6
  - **Monitoramento:**
    - Traces no New Relic
    - Logs no New Relic
    - Dashboards de monitoramento (New Relic e Azure)
### **Fase 3:**
  - **Migração arquitetura Monolitica x Micro-serviços:**
    - Separação da API em dois serviços distintos com base nos contextos delimitados (Users, Games, Orders, Payments)
    - Cada API com seu próprio repositório e infraestrutura (banco de dados, container app e pipeline CI/CD)
  - **Adoção de soluções Serverless:**
    - Arquitetura orientada a eventos com comunicação assíncrona via mensageria (Azure Service Bus)
    - Utilização de Azure Functions como gatilho das mensagens do Service Bus (Tópicos e Subscriptions)
    - Utilização do Azure API Management para gerenciamento e segurança das APIs com políticas de rate limit e cache
  - **Otimização na busca de jogos:**
    - Implementação de ElasticSearch para indexação dos jogos e logs 
    - Ganho de performance com consultas avançadas
    - Implementação de filtros, paginação e ordenação, inclusive endpoint de jogos mais bem avaliados

## 🚀 Tecnologias Utilizadas

| Tecnologia        | Versão/Detalhes                  |
|-|-|
| .NET              | .NET 8                           |
| C#                | 12                               |
| Entity Framework  | Core, com Migrations             |
| Banco de Dados    | SQL Server (ou SQLite para testes) |
| Autenticação      | JWT (Bearer Token)               |
| Testes            | xUnit, Moq, FluentAssertions     |
| Swagger           | Swashbuckle.AspNetCore           |
| Segurança         | PBKDF2 + salt com SHA256         |
| Logger            | Middleware de Request/Response + LogId |
| Docker            | Multi-stage Dockerfile para build e runtime |
| Monitoramento     | New Relic (.NET Agent) + Azure |
| Mensageria        | Azure Service Bus (Tópicos e Subscriptions) |
| Consumer de Mensagens | Azure Functions                  |
| Orquestração      | Azure Container Apps             |
| API Gateway       | Azure API Management             |
| CI/CD             | GitHub Actions                   |
| Testes de Carga   | K6                               |


## 🧠 Padrões e Boas Práticas
- Arquitetura Hexagonal (Ports and Adapters)
- Camadas separadas por responsabilidade (Domain, Application, Infrastructure, API)
- Interfaces para abstração de serviços externos no domínio
- Injeção de dependência configurada via Program.cs
- Tratamento global de exceções via middleware
- DTOs com validações automáticas via DataAnnotations


## ✅ Principais Funcionalidades

### Pagamentos
- ✅ Criação de pagamento
- ✅ Autenticação com JWT
- ✅ Validação de pagamento seguro
- ✅ Verificação de status do pagamento
- ✅ Validação de informações do pagamento
- ✅ Controle de permissões (admin)

### Segurança e Middleware
- ✅ Middleware de erro global
- ✅ Retorno padronizado com `ErrorResponse`
- ✅ Registro de logs com `RequestId` único
- ✅ Token JWT com verificação de permissões por endpoint


## 🧪 Testes

- ✅ Testes unitários completos de:
  - Regras de domínio
  - Hash de senhas
  - Autenticação
  - Serviços e repositórios mockados
- ✅ Cobertura de cenários felizes e inválidos
- ✅ Testes de carga e performance (utilizando K6)
  ```bash
  k6 run load-test.js
  ```

## ⚙️ Pré-requisitos
- .NET 8 SDK instalado
- SQL Server
- K6 para testes de carga (opcional)


## 🛠️ Setup do Projeto
Siga esses passos para configurar e rodar o projeto localmente:

### 
- Clonar o repositório
  ```bash
  git clone https://github.com/seu-usuario/FCG.Payments.git
  ```
- Configurar a conexão com o banco de dados e servicebus no `appsettings.json` ou nas variáveis de ambiente
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FiapCloudGamesDb;Trusted_Connection=True;",
      "ServiceBusConnection": "Endpoint=sb://<NAMESPACE>.servicebus.windows.net/;SharedAccessKeyName=<KEY_NAME>;SharedAccessKey=<KEY_VALUE>"
    },
    "Jwt": {
      "Key": "sua-chave-secreta-supersegura",
      "Issuer": "FiapCloudGames",
      "Audience": "FiapCloudGamesUsers"
    }
  }
  ```
- Executar as migrations para criar o banco de dados, passando a connectionString
  ```bash
  Update-Database -Project Infrastructure -StartupProject API -Connection "Server=(server)\(instance);Database=(dbname);Trusted_Connection=True;TrustServerCertificate=True"
  ```
- Rodar Testes
  ```bash
  dotnet test
  ```
- Executar a aplicação
  ```bash
  dotnet run --project API
  ```
- Acessar a documentação Swagger em `http://localhost:<porta>/swagger/index.html`


 ## 🔐 Autenticação e Autorização

- Faça login com um usuário existente via /auth/login
- Use o token Bearer retornado no header Authorization das demais requisições protegidas.


 ## 📁 Estrutura de Pastas

 ```bash
│FCG.Payments/
│
├── Adapters/                   # Camada de Adapters (Ports and Adapters)
│   ├── Driven/                     # Implementações Driven (Infraestrutura)
│   │   ├── Infrastructure/         # Serviços e repositórios concretos
│   │       ├── Services/           # Serviços como ServiceBusPublisher
│   │       └── ...                 # Outros componentes Driven
│   ├── Driver/                     # Implementações Driver (Apresentação)
│       ├── API/                    # Camada de apresentação
│           ├── Controllers/        # Endpoints REST (ex: PaymentsController)
│           ├── Middleware/         # Middlewares para logs, erros, etc.
│           └── Program.cs          # Ponto de entrada da aplicação
│
├── UseCases/                  # Camada de Casos de Uso (Application)
│   ├── Application/               # Serviços e lógica de aplicação
│       ├── Services/              # Serviços de aplicação (ex: PaymentService)
│       └── ...                    # Outros componentes de Application
│
├── Tests/                     # Testes automatizados
│   ├── UnitTests/                 # Testes unitários
│       ├── Application/           # Testes de serviços de aplicação
│       ├── Helpers/               # Testes de classes auxiliares
│       └── ...                    # Outros testes unitários
│   └── ...                        # Outros tipos de testes
│
├── Dockerfile                 # Arquivo para containerização
├── README.md                  # Documentação do projeto
└── ...                        # Outros arquivos e diretórios
 ```


## 🔗 Diagrama de Relacionamento (Simplificado)
```plaintext
+--------------------+           +------------------+          +------------------+
|   Request_log      |<--------->|    Trace_log     |          |     Payments     |
+--------------------+   (1:N)   +------------------+          +------------------+
| LogId (PK)         |           | TraceId (PK)     |          | PaymentId (PK)   |
| UserId (FK)        |           | LogId (FK)       |          | OrderId          |
| HttpMethod         |           | Timestamp        |          | Amount           |
| Path               |           | Level            |          | Status           |
| StatusCode         |           | Message          |          | PaymentMethod    |
| RequestBody        |           | StackTrace       |          | CardNumber       |
| ResponseBody       |           +------------------+          | CardHolder       |
| StartDate          |                                         | ExpiryDate       |
| EndDate            |                                         | Cvv              |
| Duration           |                                         | CreatedAt        |
| UpdatedAt          |                                         | UpdatedAt        |
+--------------------+                                         +------------------+
```


## 🚀 Pipeline CI/CD

O workflow está definido em `.github/workflows/ci-cd.yml`. 
Automatizando os seguintes passos:

- Build e testes unitários
- Build da imagem Docker
- Push para Azure Container Registry (ACR)
- MultiStage para Deploy automatizado no Azure Container Apps:
   - DEV
   - UAT (necessário aprovação)
   - PRD (apenas com PR na branch `master` e necessário aprovação)
   

## ☁️ Infraestrutura na Azure

O projeto utiliza os seguintes recursos na Azure:

- **Azure Resource Group**: `RG_FCG`
- **Azure SQL Database**: `FCG.PaymentsDB`
- **Azure Container Registry (ACR)**: `acrfcg.azurecr.io`
- **Azure Container Apps**:
  - DEV: `aca-fcg-payments-dev` 
  - UAT: `aca-fcg-payments-uat` 
  - PRD: `aca-fcg-payments` 
- **Azure Api Management**: `apim-fcg`
- **Azure Service Bus**: `servicebus-fcg`
- **Azure Functions**: `func-fcg-payments`

As variáveis de ambiente sensíveis (como strings de conexão) são gerenciadas via Azure e GitHub Secrets.
[Link para o desenho de infraestrutura](https://miro.com/app/board/uXjVIteOb6w=/?share_link_id=230805148396)

## 🐳Dockerfile e 📊Monitoramento

Este projeto utiliza um Dockerfile em duas etapas para garantir uma imagem otimizada e segura:

- **Stage 1 - Build**: Usa a imagem oficial do .NET SDK 8.0 para restaurar dependências, compilar e publicar a aplicação em modo Release.
- **Stage 2 - Runtime**: Utiliza a imagem mais leve do ASP.NET 8.0 para executar a aplicação, copiando apenas os artefatos publicados da etapa de build, o que reduz o tamanho final da imagem.

Além disso, o agente do **New Relic** é instalado na imagem de runtime para habilitar monitoramento detalhado da aplicação. As variáveis de ambiente necessárias para a configuração do agente são definidas no Dockerfile, podendo ser sobrescritas via ambiente de execução (ex.: Kubernetes, Azure Container Apps).

Esse processo segue as melhores práticas:

- **Multi-stage build:** mantém a imagem final enxuta e rápida para deploy.
- **Separação clara:** entre build e runtime para evitar expor ferramentas de desenvolvimento.
- **Instalação do agente New Relic:** automatizada e integrada para facilitar o monitoramento.
- **Configuração via variáveis de ambiente:** flexível e segura para licenças e nomes de aplicação.

 ## ✍️ Autor
- Frank Vieira
- GitHub: @fkwesley
- Projeto desenvolvido para fins educacionais no curso da FIAP.
 
