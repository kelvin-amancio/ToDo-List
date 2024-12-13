# Documentação Projeto - ToDo List

## Visão Geral
Este projeto é uma aplicação web de gerenciamento de tarefas (To-Do List) que utiliza .NET Core (C#) no back-end e Angular no front-end. O sistema permite que os usuários se autentiquem, registrem suas tarefas, e gerenciem suas tarefas de maneira simples e eficiente. As funcionalidades incluem adicionar, atualizar, remover e marcar tarefas como concluídas.

---

## Tecnologias Utilizadas

### Back-End:
- **.NET Core 8.0 (C#)**: Para o desenvolvimento da API RESTful que lida com todas as operações de CRUD das tarefas e autenticação.
- **JWT (JSON Web Tokens)**: Para autenticação e autorização de usuários.
- **SQL Server**: Para armazenamento de dados de usuários e tarefas.

### Front-End:
- **Angular 17 (standalone)**: Framework para a construção da interface web.
- **TypeScript**: Linguagem utilizada para a construção dos componentes e serviços Angular.
- **BoxIcons**: Biblioteca de ícones.

---

## Arquitetura

O sistema está dividido em duas partes principais:

1. **Back-End (WebAPI)**
2. **Front-End (Aplicação Angular)**

---

## Back-End (WebAPI)

O backend é responsável por:

### Autenticação de Usuário:
- Página de login utilizando JWT para autenticação segura.
- Página de registro para criação de novos usuários, com senha criptografada.

### Gerenciamento de Tarefas:
- Funcionalidades de CRUD (Criar, Ler, Atualizar, Excluir) para gerenciar as tarefas.
- As tarefas podem ser marcadas como concluídas.

### Estrutura do Back-End:
- **Controllers**: Controladores responsáveis pelas ações de API.
- **Context**: Contexto do banco de dados.
- **Models**: Modelos de dados (Usuário, Tarefa).
- **Repositories**: Repositórios de acesso ao banco de dados.
- **Services**: Lógica de negócios.
- **ViewModels**: Modelos utilizados nas respostas.
- **IoC**: Injeções de dependências.
- **Testes unitários**: Tarefa e Usuário

### Principais Funcionalidades da API:
- **POST /api/Auth/login**: Realiza o login de um usuário e retorna um token JWT.
- **POST /api/Auth/register**: Registra um novo usuário com senha criptografada.
- **GET /api/TaskItem**: Retorna uma lista de tarefas paginada.
- **POST /api/TaskItem**: Cria uma tarefa.
- **PUT /api/TaskItem**: Atualiza uma tarefa existente.
- **DELETE /api/TaskItem/{id}**: Remove uma tarefa.

---

## Front-End (Angular)

O frontend é responsável por:

- Exibir as páginas de login, registro e listagem de tarefas.
- Gerenciar a autenticação utilizando JWT (armazenando o token no localStorage).
- Permitir a interação do usuário com as tarefas, como adicionar, editar, excluir e marcar como concluídas.

### Estrutura do Front-End:
- **guards**: Proteção de rotas (autenticação e autorização).
- **interceptors**: Interceptadores de requisições HTTP.
- **modules**: Módulos principais da aplicação.
- **componentes**: Componentes reutilizáveis (ex: list-item, modal).
- **pages**: Páginas principais como `login`, `register`, `task-list`.
- **services**: Serviços para interação com a API.

### Principais Funcionalidades do Front-End:
- **Login**: Página onde o usuário insere seu nome de usuário e senha. Após a validação, um token JWT é armazenado.
- **Registro**: Página para criar um usuário, onde a senha é criptografada.
- **Listagem de Tarefas**: Página principal onde o usuário pode visualizar suas tarefas, paginadas. O usuário pode adicionar novas tarefas, editar, excluir ou marcar como concluídas.
- **Modal**: Usados para a criação e edição de tarefas.

---

## Fluxo da Aplicação

1. **Login e Registro**:
   - O usuário acessa a página de login, insere suas credenciais e, se autenticado com sucesso, recebe um token JWT que será armazenado no localStorage.
   - Caso o usuário não tenha uma conta, ele pode se registrar, inserindo um nome de usuário, senha e confirmação de senha.

2. **Página Principal**:
   - Após o login, o usuário é redirecionado para a página principal, onde suas tarefas são exibidas.
   - As tarefas são paginadas e o usuário pode interagir com elas: adicionar novas tarefas, editar ou excluir tarefas existentes, ou marcar tarefas como concluídas.

3. **Autenticação**:
   - O token JWT é enviado como cabeçalho de todas as requisições HTTP para garantir que o usuário esteja autenticado.

---

## Configuração do Ambiente de Desenvolvimento

### Back-End

1. **Configuração do Banco de Dados:**
   - Configure o banco de dados SQL Server e adicione as credenciais no arquivo `appsettings.json`:
     Data Source=.\\SQLEXPRESS;Database=master;Integrated Security=True;Encrypt=False;Trust Server Certificate=False

2. **Rodando as Migrations:**
   - Para aplicar as migrações no banco de dados, execute o seguinte comando no terminal: dotnet ef database update

3. **Executando a API:**
   - Para iniciar a API, execute o comando: dotnet run
   - A API estará em execução e poderá ser acessada localmente na URL configurada.

---

## Front-End

1. **Instalação:**
   - Certifique-se de ter o **Node.js** e **Angular CLI** instalados.
   - Navegue até a pasta do projeto e instale as dependências com o seguinte comando:
     npm install

2. **Executando o Front-End:**
   - Para rodar a aplicação, execute o seguinte comando:
     ng serve
   - A aplicação estará disponível em [http://localhost:4200](http://localhost:4200).

