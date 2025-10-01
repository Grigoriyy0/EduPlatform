## EduPlatform (EduNEXT)

A modern learning management platform: student and lesson tracking, scheduling, analytics, and notifications. The project focuses on Clean Architecture, modularity, and simple Docker-based deployment.

### Repository structure
- **Backend**: `src/EduNEXT.API` — ASP.NET Core API
- **Core/Application/Infrastructure**: `src/EduNEXT.Core`, `src/EduNEXT.Application`, `src/EduNEXT.Infrastructure`
- **Docker/Infra**: `deploy/` (compose, DB init)
- **(Optional) Web client**: `eduplatfrom.client` — React SPA (https://github.com/Grigoriyy0/eduplatfrom.client)
- **(Optional) Notification Bot**: `eduplatform.notification` — Telegram bot (https://github.com/Grigoriyy0/eduplatform.notification)

---

## 1) EduNEXT.API

### Features
- **Authentication** and access control (JWT/tokens)
- **Students**: CRUD and updates
- **Lessons and time slots**: scheduling, editing, timetable retrieval
- **Finance**: salary metrics
- **Analytics**: aggregated metrics for lessons/income, etc.
- **Events/notifications**: publishing event messages to the broker

### Architectural highlights
- **Clean Architecture** (layers: `Core`/`Application`/`Infrastructure`/`API`)
- **Use Cases in CQRS style**: separate `Commands` and `Queries`
- **Ports & Adapters**: interfaces in `Application`, implementations in `Infrastructure`
- **Infrastructure adapters**: repositories, hash/password providers, message publisher
- **Modular configuration** and DI via `DependencyInjection`

### Tech stack
- **ASP.NET Core** (Web API)
- **Entity Framework Core** + **PostgreSQL**
- **RabbitMQ** for messaging
- **Docker**/Docker Compose for environment
- (Recommended) **Swagger/OpenAPI** for endpoint documentation

---

## 2) Web client (React)

The project includes a **React** SPA (Vite, React Router, TailwindCSS, etc.). To run it correctly, clone it from a separate repository into the root of this project under the directory name `eduplatfrom.client`.

Example:

```bash
# from the repository root
git clone <CLIENT_REPO_URL> eduplatfrom.client
```

After this, the client will start alongside other services via Docker Compose.

---

## 3) Telegram notification bot

The Telegram bot consumes messages from **RabbitMQ** and sends notifications. Its sources live in a separate repository. To run it correctly, place it in the project root under the directory `eduplatform.notification`.

Example:

```bash
# from the repository root
git clone <BOT_REPO_URL> eduplatform.notification
```

The bot connects to the message broker, listens to a queue, and sends messages via Telegram Bot API.

---

## 4) Quick start with Docker

### Prerequisites
- Docker and Docker Compose installed
- The root directory contains `eduplatfrom.client` and `eduplatform.notification` (if you need the client and the bot)

### Connection settings
- Values in `appsettings.json`/`appsettings.Development.json` for DB and broker connections MUST match those in `deploy/docker-compose.yml`.
  - PostgreSQL: host, port, database, username, password (`Host`, `Port`, `Database`, `Username`, `Password`).
  - RabbitMQ: host, port, username, password, virtual host.
- If you change credentials or database names in compose, mirror the same values in `appsettings*.json`.

### Start
```bash
docker compose -f deploy/docker-compose.yml up -d
```

### Stop
```bash
docker compose -f deploy/docker-compose.yml down
```

After startup:
- The API is available at the address defined in compose (see `deploy/docker-compose.yml`).
- Endpoint documentation is available at `/swagger` of the API service.
- The client app and bot start as their own services (if the corresponding directories exist).

---

## 5) TODO
- Add integration with external calendars (Google/Apple)
- Design and develop external service responsible for storing materials for lessons
- Develop mobile version of the client
- Add automated tests (unit/integration) for Core, Application, and API
- Improve API documentation (full OpenAPI, examples, error schemas)
- Perform a global refactor of modules and DI configuration
- Add observability: centralized logs, metrics, tracing
- Improve error handling and validation contracts
- Set up CI/CD (lint, tests, image builds, deploy)
- Expand analytics and dashboards on the frontend

---

## License
Feel free to use the project for educational purposes. For commercial use, please agree on terms separately.


