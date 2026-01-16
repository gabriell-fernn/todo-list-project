# 📝 Todo List – Full Stack (.NET + SAPUI5)

Projeto Full Stack de gerenciamento de tarefas (Todo List), desenvolvido com:

- **Backend:** ASP.NET Core (.NET 8) seguindo princípios de DDD
- **Frontend:** SAPUI5 (Fiori-like)
- **Arquitetura:** REST API + Paginação + Filtros + Regras de negócio

---

## 🚀 Funcionalidades

### Backend
- CRUD de tarefas
- Paginação e ordenação
- Filtro por título (case-insensitive)
- Regra de negócio:
  - Máximo de **5 tarefas incompletas por usuário**
- Integração com API externa
- Testes de integração com xUnit

### Frontend (SAPUI5)
- Listagem de tarefas
- Paginação
- Filtro por título
- Ordenação por ID
- Criação de tarefas
- Marcar tarefa como concluída
- Visualização de detalhes da tarefa
- Sincronização com API externa

---

## 🏗️ Estrutura do Projeto

```
todo-list/
├── TodoList/         # API .NET
├── frontend/         # SAPUI5
└── README.md
```

---

## ⚙️ Pré-requisitos

- .NET SDK **8.0+**
- Node.js **18+**
- npm
- Git

---

## ▶️ Como rodar o Backend

```bash
cd TodoList/src/TodoList.Api
dotnet restore
dotnet run
```

- API: `http://localhost:5130`
- Swagger: `http://localhost:5130/swagger`

---

## ▶️ Como rodar o Frontend

```bash
cd frontend
npm install
npm run start:main
```

- Frontend: `http://localhost:8080`

---

## 🔗 Integração Frontend ↔ Backend

O frontend consome diretamente a API em:

```
http://localhost:5130
```

---

## 🧪 Testes

Os testes de integração estão no projeto:

```
cd TodoList/tests/WebApi.Test
```

Para executar:

```bash
dotnet test
```

---

## 📚 Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- xUnit
- SAPUI5
- JavaScript (ES6+)

---

## 👨‍💻 Autor

Gabriell Fernandes

---
