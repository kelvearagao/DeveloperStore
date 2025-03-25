# **Ambev Developer Evaluation**

## **Descrição do Projeto**

Este projeto é uma aplicação backend desenvolvida para gerenciar vendas, usuários e produtos. Ele segue padrões modernos de desenvolvimento, utilizando tecnologias robustas e boas práticas para garantir escalabilidade, manutenibilidade e facilidade de uso.

---

## **Organização do Projeto**

O projeto está organizado em uma estrutura modular para separar responsabilidades e facilitar a manutenção:

### **1. Camadas do Projeto**

- **Domain**: Contém as entidades, enums e interfaces que representam o núcleo do domínio.
- **Application**: Contém os casos de uso (handlers), comandos, validações e lógica de negócios.
- **Infrastructure**: Contém implementações de repositórios, configurações de banco de dados e serviços externos.
- **WebApi**: Contém os controladores e a configuração da API REST.
- **Tests**: Contém os testes unitários e de integração.

### **2. Padrões Utilizados**

- **CQRS (Command Query Responsibility Segregation)**:
  - Separação entre comandos (escrita) e consultas (leitura).
- **Mediator Pattern**:
  - Utilizado para desacoplar os handlers dos controladores, implementado com a biblioteca `MediatR`.
- **Repository Pattern**:
  - Abstração para acesso ao banco de dados.
- **Dependency Injection**:
  - Todas as dependências são injetadas para facilitar testes e modularidade.
- **Validation**:
  - Validações centralizadas utilizando a biblioteca `FluentValidation`.

### **3. Boas Práticas**

- **SOLID**:
  - O projeto segue os princípios SOLID para garantir um design limpo e extensível.
- **Clean Code**:
  - Código legível, com nomes descritivos e responsabilidades bem definidas.
- **Logs**:
  - Uso de logs detalhados para rastrear o fluxo de execução e facilitar a depuração.
- **Testes Automatizados**:
  - Testes unitários para garantir a qualidade e evitar regressões.

---

## **Tecnologias Utilizadas**

- **.NET 6**: Framework principal para desenvolvimento da aplicação.
- **Entity Framework Core**: ORM para acesso ao banco de dados.
- **MediatR**: Implementação do padrão Mediator.
- **FluentValidation**: Biblioteca para validação de comandos e requisições.
- **AutoMapper**: Biblioteca para mapeamento de objetos.
- **Bogus**: Gerador de dados fictícios para testes.
- **NSubstitute**: Biblioteca para criação de mocks em testes.

---

## **Como Rodar o Projeto**

### **1. Pré-requisitos**

- **.NET 6 SDK** instalado.
- **Banco de Dados PostgreSQL** configurado e rodando.
- **Ferramenta CLI do EF Core** instalada:

```bash
dotnet tool install --global dotnet-ef
```

### **2. Configuração do Banco de Dados**

Configure a string de conexão no arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=AmbevEvaluation;Username=postgres;Password=yourpassword"
}
```

### **3. Aplicar Migrations**

Para criar o banco de dados e aplicar as migrations, execute:

```bash
   dotnet ef database update --project Ambev.DeveloperEvaluation.ORM --startup-project Ambev.DeveloperEvaluation.WebApi
```

### **4. Rodar o Projeto**

Navegue até o diretório do projeto WebApi:

```bash
cd Ambev.DeveloperEvaluation.WebApi
```

Execute o comando:

```bash
dotnet run
```

A API estará disponível em http://localhost:5119.

### **Testes**

1. Rodar Testes Unitários
   Navegue até o diretório de testes:

```bash
cd Ambev.DeveloperEvaluation.Unit
```

Execute os testes:

```bash
dotnet test
```

### **Banco de Dados**

- O projeto utiliza PostgreSQL como banco de dados relacional.
- As tabelas são gerenciadas pelo Entity Framework Core.
- As migrations estão localizadas no diretório:

```bash
src/Ambev.DeveloperEvaluation.ORM/Migrations
```

### **Contribuição**

- Siga os padrões de código definidos no projeto.
- Certifique-se de adicionar testes para qualquer funcionalidade nova.
- Utilize mensagens de commit claras e descritivas.
