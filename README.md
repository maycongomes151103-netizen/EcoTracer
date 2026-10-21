# EcoTracer

Aplicação desktop em C# (.NET 10) que calcula a emissão de CO2 gerada pelo deslocamento diário de uma pessoa, convertendo o total anual em número equivalente de árvores necessárias para compensar essa emissão.

## ODS relacionada

*ODS 13 — Ação Contra a Mudança Global do Clima*, meta 13.3 (educação e conscientização sobre mudança do clima).

## Funcionalidades

- Cadastro e login de usuário (senha armazenada com hash, nunca em texto puro)
- Cálculo de emissão diária e anual de CO2, a partir da distância, meio de transporte e frequência semanal
- Conversão do CO2 anual em número de árvores equivalentes
- Histórico de cálculos salvo por usuário, persistido em banco local (SQLite)
- Tratamento de erros para entradas inválidas

Fatores de emissão e fontes detalhadas em [docs/fatores-emissao.md](docs/fatores-emissao.md). Estrutura do banco de dados em [docs/modelo-dados.md](docs/modelo-dados.md).


## Tecnologias

- C# / .NET 10
- Windows Forms (interface gráfica)
- SQLite (Microsoft.Data.Sqlite) — persistência local
- BCrypt.Net-Next — hash de senha
- xUnit (testes unitários)
- GitHub Actions (integração contínua)

## Arquitetura

EcoTracer/
├── EcoTracer.slnx  
├── .gitignore  
├── README.md  
├── docs/  
│ ├── fatores-emissao.md  
│ └── modelo-dados.md  
├── .github/  
│ └── workflows/  
│ └── ci.yml  
├── src/  
│ ├── EcoTracer.Core/  
│ │ ├── Data/  
│ │ │ └── AppDatabase.cs  
│ │ ├── Models.cs  
│ │ ├── Services.cs  
│ │ └── Exceptions.cs  
│ └── EcoTracer.App/  
│ ├── Program.cs  
│ ├── FormLogin.cs  
│ ├── FormCadastro.cs  
│ ├── Form1.cs  
│ └── respectivos .Designer.cs  
└── tests/  
└── EcoTracer.Tests/  
├── CalculadoraEcoTracerTests.cs  
├── UsuarioServiceTests.cs  
└── HistoricoServiceTests.cs  


## Como instalar e rodar

git clone https://github.com/maycongomes151103-netizen/EcoTracer.git
cd EcoTracer
dotnet build EcoTracer.slnx
dotnet run --project src/EcoTracer.App


Na primeira vez que rodar, crie uma conta pelo link "Criar conta" na tela de login.

## Como rodar os testes

dotnet test EcoTracer.slnx


## Fluxo de branches

Este repositório segue o padrão Git Flow: main (estável), develop (integração) e feature/equipe (desenvolvimento conjunto).

## Integrantes

- Anna Vitória Rocha Dias — RA: 325118421 — Dev Backend / Core 
- Maycon Gomes — RA: 325125878 — QA / Testes Unitários 
- Karolyne Silva — RA: 32517941 — Documentação e DevOps / Versionamento 

## Trabalho acadêmico


Projeto desenvolvido para a disciplina de Gestão e Qualidade de Software (GQS), UNA, sob orientação do professor Daniel Henrique Matos de Paiva.

