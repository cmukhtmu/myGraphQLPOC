# GraphQL People API

A **.NET 10 GraphQL API** built with **HotChocolate** and **PostgreSQL**, fully containerized with Docker. Supports filtering, sorting, pagination, and nested queries across People and Address History data.

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | .NET 10 / ASP.NET Core |
| GraphQL | HotChocolate |
| Database | PostgreSQL 16 |
| ORM | Entity Framework Core |
| Containerization | Docker / Docker Compose |

---

## 📁 Project Structure
```
myGraphQLAPI/
├── Data/                        # DbContext and database configuration
├── GraphQL/                     # Query types and HotChocolate configuration
├── Model/                       # Entity models (Person, AddressHistory)
├── Properties/                  # Launch settings
├── Dockerfile                   # Production image
├── Dockerfile.dev               # Development image with hot reload
├── docker-compose.yml           # Production compose config
├── docker-compose.dev.yml       # Development compose overrides
├── Makefile                     # Shorthand commands
├── .env.example                 # Environment variable template
├── appsettings.json             # App configuration (no secrets)
└── appsettings.Development.json # Local dev overrides (gitignored)
```

---

## ⚙️ Getting Started

### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [.NET 10 SDK](https://dotnet.microsoft.com/download) (for local development outside Docker)
- A running PostgreSQL container or instance

### 1. Clone the repository
```bash
git clone https://github.com/cmukhtmu/myGraphQLPOC.git
cd myGraphQLPOC
```

### 2. Set up environment variables

Copy the example env file and fill in your credentials:
```bash
cp .env.example .env
```
```env
DB_HOST=people-postgres
DB_PORT=5432
DB_NAME=AWSLearning
DB_USER=postgres
DB_PASSWORD=yourpassword
```

### 3. Connect your PostgreSQL container to the shared network

If your PostgreSQL container is already running separately, connect it to the shared Docker network:
```bash
docker network create project1_people-network
docker network connect project1_people-network people-postgres
```

### 4. Start the API

**Development** (with hot reload):
```bash
make dev
```

**Production**:
```bash
make prod
```

**Stop containers**:
```bash
make down
```

The API will be available at: `http://localhost:5000/graphql`

---

## 🔍 Example Queries

### Query people with filtering
```graphql
query ($personFilter: PersonFilterInput, $addressFilter: AddressHistoryFilterInput) {
  people(where: $personFilter) {
    nodes {
      id
      firstName
      lastName
      phone
      addressHistory(where: $addressFilter) {
        id
        address1
        city
        state
        zip
        active
      }
    }
  }
}
```

### Variables — filter with OR clause
```json
{
  "personFilter": {
    "firstName": {
      "startsWith": "Dan"
    },
    "or": [
      { "lastName": { "startsWith": "Smith" } },
      { "lastName": { "startsWith": "Doe" } }
    ]
  },
  "addressFilter": {
    "active": { "eq": true }
  }
}
```

---

## 🐳 Docker Setup

This project uses two Compose files:

| File | Purpose |
|---|---|
| `docker-compose.yml` | Base/production configuration |
| `docker-compose.dev.yml` | Dev overrides — hot reload, source volume mount |

The `Makefile` merges them automatically:
```makefile
make dev     # Runs with hot reload (dotnet watch)
make prod    # Runs production build
make down    # Stops and removes containers
```

### Hot Reload

The dev setup uses `dotnet watch` with polling file watcher enabled for WSL2/Docker Desktop on Windows:
```yaml
DOTNET_USE_POLLING_FILE_WATCHER: "true"
```

`obj/` and `bin/` folders are shadowed with named Docker volumes to prevent permission conflicts on the host machine.

---

## 🔒 Environment Variables

Credentials are managed via a `.env` file (gitignored). See `.env.example` for required variables:

| Variable | Description |
|---|---|
| `DB_HOST` | PostgreSQL container name or hostname |
| `DB_PORT` | PostgreSQL port (default: `5432`) |
| `DB_NAME` | Database name |
| `DB_USER` | Database username |
| `DB_PASSWORD` | Database password |

> ⚠️ Never commit your `.env` or `appsettings.Development.json` files.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).