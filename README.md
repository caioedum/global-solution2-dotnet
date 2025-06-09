# iHelperDrone

## Descrição do Projeto

O **iHelperDrone** é uma API RESTful desenvolvida em .NET 8 para monitoramento de áreas de risco, gerenciamento de drones e emissão de alertas em tempo real. O sistema utiliza RabbitMQ para mensageria entre microserviços e ML.NET para análise de sentimento de alertas, permitindo respostas automáticas e inteligentes a situações críticas.

---

## Integrantes do Grupo

- **Caio Eduardo Nascimento Martins - RM554025**
- **Julia Mariano Barsotti Ferreira - RM552713**
- **Leonardo Gaspar Saheb - RM553383**

## Tecnologias Utilizadas

- **.NET 8 (ASP.NET Core Web API)**
- **Oracle XE** (banco de dados)
- **RabbitMQ** (mensageria)
- **ML.NET** (machine learning)
- **Docker \& Docker Compose**
- **Swagger (Swashbuckle)**
- **xUnit, Moq, FluentAssertions** (testes unitários)

---

## Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/)
- (Opcional) [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio](https://visualstudio.microsoft.com/)


### Passos

1. **Clone o repositório:**

```bash
git clone https://github.com/caioedum/global-solution2-dotnet.git
cd global-solution2-dotnet
```

2. **Configure o arquivo `appsettings.json` se necessário.**

3. **(Opcional) Execute a API localmente:**

```bash
dotnet build
dotnet run
```

## Documentação dos Endpoints

A documentação completa está disponível via Swagger em `/swagger`.

### Exemplos de endpoints principais:

#### **Usuários**

- `GET /api/usuarios` — Lista todos os usuários
- `GET /api/usuarios/{id}` — Busca usuário por ID
- `POST /api/usuarios` — Adiciona novo usuário
- `PUT /api/usuarios/{id}` — Atualiza usuário


#### **Drones**

- `GET /api/drones` — Lista todos os drones
- `GET /api/drones/{id}` — Busca drone por ID
- `POST /api/drones` — Adiciona novo drone
- `PUT /api/drones/{id}` — Atualiza drone
- `DELETE /api/drones/{id}` — Remove drone
- `GET /api/drones/disponiveis` — Drones disponíveis
- `GET /api/drones/em-missao` — Drones em missão


#### **Áreas de Risco**

- `GET /api/areasrisco` — Lista todas as áreas de risco
- `GET /api/areasrisco/{id}` — Busca área de risco por ID
- `POST /api/areasrisco` — Adiciona nova área de risco
- `PUT /api/areasrisco/{id}` — Atualiza área de risco
- `DELETE /api/areasrisco/{id}` — Remove área de risco


#### **Alertas**

- `GET /api/alertas` — Lista todos os alertas
- `GET /api/alertas/{id}` — Busca alerta por ID
- `POST /api/alertas` — Adiciona novo alerta
- `PUT /api/alertas/{id}` — Atualiza alerta
- `DELETE /api/alertas/{id}` — Remove alerta
- `GET /api/alertas/por-area/{areaId}` — Alertas por área de risco
- `GET /api/alertas/por-drone/{droneId}` — Alertas por drone
- `GET /api/alertas/por-usuario/{usuarioId}` — Alertas por usuário
- `POST /api/alertas/analisar-sentimento` — Analisa o sentimento de um texto de alerta (ML.NET)

---

## Instruções de Testes

1. **Execute os testes unitários:**

```bash
dotnet test
```

2. **Cobertura dos testes:**
    - Testes unitários cobrem controllers e regras de negócio usando Moq e FluentAssertions.
    - Os testes simulam os repositórios e a integração com RabbitMQ e ML.NET.
3. **Verifique os resultados:**
    - Os resultados dos testes serão exibidos no terminal.
    - Todos os métodos principais das controllers estão cobertos por testes.
      
## Licença

- Este projeto é de uso acadêmico - FIAP.

