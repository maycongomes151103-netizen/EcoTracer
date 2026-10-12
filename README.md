# 🌱 EcoTracer

> Aplicação desktop em C# / .NET 10 para calcular o impacto ambiental de deslocamentos diários, convertendo a emissão de CO₂ no número de árvores necessárias para compensá-la.

---

## 📌 Sobre o Projeto

O **EcoTracer** calcula a emissão de CO₂ gerada pelo deslocamento diário de uma pessoa, a partir da distância percorrida, do meio de transporte utilizado e da frequência semanal, convertendo o total anual em número equivalente de árvores necessárias para absorver esse gás.

**ODS relacionada:** ODS 13 — Ação Contra a Mudança Global do Clima (meta 13.3: educação e conscientização sobre mudança do clima).

---

## 🏗️ Arquitetura do Sistema

EcoTracer/
├── EcoTracer.slnx
├── .gitignore
├── README.md
├── docs/
│ └── fatores-emissao.md # Fontes e metodologia dos fatores de emissão
├── .github/
│ └── workflows/
│ └── ci.yml # Pipeline de build e testes (GitHub Actions)
├── src/
│ ├── EcoTracer.Core/ # Biblioteca de regras de negócio
│ │ ├── Models.cs # Entidades e enum (ModoTransporte, ResultadoImpacto)
│ │ ├── Services.cs # CalculadoraEcoTracer — lógica de cálculo
│ │ └── Exceptions.cs # Exceções customizadas de validação
│ └── EcoTracer.App/ # Interface gráfica (Windows Forms)
│ ├── Form1.cs
│ └── Form1.Designer.cs
└── tests/
└── EcoTracer.Tests/
└── UnitTest1.cs # 8 testes unitários (xUnit)

---

## ⚙️ Como calcular

Emissão diária (kg) = distância_km × fator_emissão
Emissão anual (kg) = emissão_diária × dias_por_semana × 52
Árvores equivalentes = arredondar_para_cima(emissão_anual ÷ 8,16)


Fatores de emissão e fontes completas em [`docs/fatores-emissao.md`](docs/fatores-emissao.md).

---

## 🖥️ Tecnologias

- C# / .NET 10
- Windows Forms (interface gráfica)
- xUnit (testes unitários)
- GitHub Actions (integração contínua)

---

## ▶️ Como executar

Pré-requisito: .NET 10 SDK instalado.

```bash
git clone https://github.com/maycongomes151103-netizen/EcoTracer.git
cd EcoTracer
dotnet build EcoTracer.slnx
dotnet run --project src/EcoTracer.App
```

## 🧪 Como rodar os testes

```bash
dotnet test EcoTracer.slnx
```

---

## 🌿 Fluxo de branches

Este repositório segue o padrão Git Flow: `main` (estável), `develop` (integração) e branches `feature/*` por funcionalidade.

---

## 👥 Integrantes

- Anna Vitória Rocha Dias — RA: 325118421 — Dev Backend / Core 
- Maycon Gomes — RA: 325125878 — QA / Testes Unitários 
- Karolyne Silva — RA: 32517941 — Documentação e DevOps / Versionamento

---

## 🎓 Trabalho acadêmico

Projeto desenvolvido para a disciplina de Gestão e Qualidade de Software (GQS), UNA, sob orientação do professor Daniel Henrique Matos de Paiva.