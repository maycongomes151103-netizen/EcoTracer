# Modelo de dados

O EcoTracer usa SQLite (arquivo local ecotracer.db, gerado na primeira execução, não versionado) para persistir usuários e histórico de cálculos.)

## Tabela Usuarios 

| Coluna | Tipo | Descrição |
|---|---|---|
| Id | INTEGER | Chave primária, autoincremento |
| Nome | TEXT | Nome do usuário |
| Email | TEXT | Único, usado como login |
| SenhaHash | TEXT | Hash da senha (BCrypt), nunca texto puro |

## Tabela Historico

| Coluna | Tipo | Descrição |
|---|---|---|
| Id | INTEGER | Chave primária, autoincremento |
| UsuarioId | INTEGER | Chave estrangeira para Usuarios.Id |
| DistanciaDiariaKm | REAL | Distância informada no cálculo |
| Modo | TEXT | Meio de transporte usado |
| DiasPorSemana | INTEGER | Frequência semanal |
| Co2AnualKg | REAL | Resultado do cálculo |
| ArvoresNecessariasAnual | INTEGER | Resultado do cálculo |
| DataCalculo | TEXT | Data/hora do cálculo |

## Relação

Um usuário pode ter múltiplos registros de histórico (1:N). Cada cálculo feito na aplicação, após login, é salvo automaticamente vinculado ao usuário autenticado.
