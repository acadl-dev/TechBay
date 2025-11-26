# 🛒 TechBay (em produção...)

> **TechBay** é uma aplicação web desenvolvida com **.NET Razor Pages** que simula um e-commerce de eletrônicos.  
O sistema permite que o usuário atue tanto como **vendedor** quanto como **comprador**, oferecendo uma experiência completa de marketplace.

[Documento de Especificação de Requisitos](https://docs.google.com/document/d/1J_LONfgWOgjPTzsTTBuO8A9_g_1l9m_W4sDKiDV3ItE/edit?usp=sharing)
[Trello: planejamento e execução](https://trello.com/b/YGKhlZHh/planejamento-techbay)


---

## 🚀 Funcionalidades

### 👤 Autenticação e Autorização
- Sistema de login e registro utilizando **ASP.NET Identity**.
- Escolha de perfil após login:
  - **Vendedor**: pode cadastrar produtos, acompanhar dashboards de vendas e consultar valores.
  - **Comprador**: pode navegar pelos produtos, adicionar ao carrinho e realizar pedidos.

> ⚠️ No estágio atual do projeto, o usuário já consegue realizar login e selecionar sua role (vendedor ou comprador).

---

## 🧩 Tecnologias Utilizadas

- **.NET 8.0** (Razor Pages)
- **Entity Framework Core**
- **ASP.NET Identity**
- **SQL Server (LocalDB)** — banco de dados padrão para desenvolvimento
- **Tailwind CSS via CDN** (para layout e responsividade)

---


## 📁 Estrutura do Projeto

```
TechBay/
├── Pages/              # Razor Pages
├── Areas/              # Scafolded itens do Identity
├── Data/               # Contexto do EF Core
├── Models/             # Modelos de domínio
├── Properties/         # Configuracões
├── wwwroot/            # Arquivos estáticos (CSS, JS, imagens)
└── appsettings.json    # Configurações da aplicação
```

## 🔧 Como Executar

### Pré-requisitos
- .NET 8.0 SDK
- SQL Server LocalDB ou instância completa
- Visual Studio 2022 ou VS Code

### Instalação
```bash
# Clone o repositório
git clone https://github.com/acadl-dev/TechBay.git


# Configure a string de conexão
Edite o arquivo appsettings.json e configure a connection string do banco de dados:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TechBayDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}

# Restaure as dependências
dotnet restore

# Execute as migrations
dotnet ef database update

# Execute a aplicação
dotnet run
```

### Acesso
- **URL:** `https://localhost:5001`


## 💡 Destaques Técnicos

### Processamento de Dados
- **Arquitetura Limpa** Separação clara de responsabilidades com Data, Models e Services
- **Entity Framework Core** Implementação completa com Code First, Migrations e Fluent API
- **Design Patterns**  Repository Pattern, Dependency Injection e Service Layer
- **Segurança de Dados**  Validação robusta com TryUpdateModel e proteção contra over-posting
- **Performance:**  Lazy Loading otimizado e consultas eficientes com LINQ
- **Soft Delete**  Implementação de exclusão lógica para auditoria e recuperação de dados



## 📈 Próximos Passos

- Implementar CRUD de produtos para vendedores
- Criar catálogo de produtos para compradores
- Desenvolver carrinho de compras
- Implementar sistema de pedidos
- Criar dashboard de vendas
- Adicionar sistema de pagamento
- Implementar busca e filtros de produtos



## 📈 Métricas do Sistema

O dashboard apresenta métricas calculadas dinamicamente:
- Agregação de dados em tempo real
- Filtros por status automatizados
- Cálculos financeiros precisos
- Ordenação cronológica inteligente

## 🎓 Contexto Acadêmico

Este projeto foi desenvolvido como projeto pessoal para demostrar habilidades adquiridas na disciplina de **Desenvolvimento Back-end**:

- **Aplicação prática** de conceitos teóricos
- **Resolução de problemas** do mundo real
- **Arquitetura escalável** e mantível
- **Boas práticas** de desenvolvimento

---

## 🤝 Contribuindo
Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

- Fork o projeto
- Crie uma branch para sua feature (git checkout -b feature/MinhaFeature)
- Commit suas mudanças (git commit -m 'Adiciona MinhaFeature')
- Push para a branch (git push origin feature/MinhaFeature)
- Abra um Pull Request

⭐ **Se este projeto foi útil, considere dar uma estrela no repositório!**
