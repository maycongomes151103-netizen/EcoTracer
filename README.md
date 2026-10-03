\# 🌱 EcoTracer



> Aplicação de consola modular em .NET 10 para monitorização e cálculo de pegada de carbono na mobilidade corporativa, com relatórios de compensação ambiental (metas ESG).



\---



\## 📌 Sobre o Projeto



O \*\*EcoTracer\*\* é uma ferramenta desenvolvida em C# / .NET 10 para calcular o impacto ambiental diário e anual decorrente dos deslocamentos de colaboradores ou frotas. Com base na distância percorrida e no meio de transporte utilizado, o sistema gera o volume aproximado de emissões de CO2 e estima a quantidade de árvores necessárias para neutralizar essa pegada ecológica.



\---



\## ️️ Arquitetura do Sistema



A solução foi estruturada seguindo boas práticas de modularidade e separação de responsabilidades em projetos .NET:



```text

EcoTracer/

├── EcoTracer.sln

├── .gitignore

├── README.md

├── src/

│   └── EcoTracer.Core/              # Projeto Principal (Console App)

│       ├── Models/                  # Entidades e Enums (ModoTransporte, ResultadoImpacto)

│       ├── Services/                # Regras de Negócio (CalculadoraEcoTracer)

│       └── Program.cs               # Interface CLI e navegação entre telas

└── tests/

&#x20;   └── EcoTracer.Tests/             # Projeto de Testes Unitários

&#x20;       └── UnitTest1.cs             # Cobertura xUnit para o serviço de cálculo

