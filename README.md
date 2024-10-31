# 📊 Projeto Completo: Backend com .NET 8 e Frontend em React com TypeScript

Este repositório contém uma aplicação completa que integra um backend robusto em .NET 8 com banco de dados PostgreSQL e um frontend moderno em React com TypeScript. O frontend utiliza componentes do PrimeReact e é estilizado com Styled Components, proporcionando uma interface agradável e responsiva.

## 🛠️ Tecnologias Utilizadas

### Backend
- **Linguagem**: C# 
- **Framework**: .NET 8
- **Banco de Dados**: PostgreSQL
- **Autenticação**: JWT para controle de acesso
- **ORM**: Entity Framework Core
- **Design de APIs**: RESTful, seguindo padrões de nomenclatura e boas práticas

### Frontend
- **Linguagem**: TypeScript
- **Framework**: React.js
- **Biblioteca de Componentes**: PrimeReact
- **Estilização**: Styled Components
- **Gerenciamento de Estado**: Context API

---

## ⚙️ Funcionalidades do Projeto

### Backend
1. **Autenticação** e **Autorização** via JWT, protegendo endpoints e definindo permissões específicas.
2. **CRUD Completo** para gerenciamento de dados de entidades.
3. **Integração com PostgreSQL** usando Entity Framework Core para modelagem e persistência de dados.
4. **Validação e Tratamento de Erros** para melhorar a segurança e robustez das APIs.

### Frontend
1. **UI Responsiva** usando PrimeReact e Styled Components para uma experiência de usuário fluida e bonita.
2. **Gestão de Estado Centralizada** com Context API, garantindo consistência de dados entre os componentes.
3. **Autenticação com JWT** e persistência de sessão.
4. **Formulários Dinâmicos e Validação** para captura de dados de forma prática e intuitiva.

---

## 📂 Estrutura do Projeto

A estrutura dos arquivos foi organizada para manter o código modular e reutilizável.

```bash
.
├── backend
│   ├── WorkerApi
│   ├── WorkerModels
│   ├── WorkerServices
│   ├── WorkerRepositories
│   ├── WorkerSecurity
│
└── frontend
    ├── public    
    ├── src
    │   ├── components
    │   ├── api
    │   ├── utils
    │   └── App.tsx
```


- **backend**: Contém a API RESTful desenvolvida em .NET 8.
- **frontend**: Frontend em React com TypeScript, estruturado em componentes reutilizáveis.

---

## 🚀 Como Executar o Projeto

### Pré-requisitos
- **.NET SDK** (versão 8 ou superior)
- **Node.js** (versão 18 ou superior)
- **PostgreSQL** (configurado e rodando na máquina)

### Passo a Passo

#### Backend
1. Clone o repositório e navegue até o diretório `backend`.
2. Configure a string de conexão com o PostgreSQL em `appsettings.json`.
3. Execute os seguintes comandos:
```bash
   dotnet restore
   dotnet ef database update
   dotnet run
```

4. A API estará disponível em https://localhost:5001.

#### Backend - Teste
1. Execute o comando abaixo para rodar o Teste
```bash
   cd Worker.Tests
   dotnet test
```

#### Frontend
1. Navegue até o diretório frontend..
2. Instale as dependências com:
```bash
   npm install
```
3. Configure a URL do backend em um arquivo de configuração ou no .env.

4. Inicie a aplicação com:
```bash
    npm start
```

5. A interface estará disponível em http://localhost:3000.