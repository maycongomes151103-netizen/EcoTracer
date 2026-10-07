# Fatores de emissão e absorção

| Item | Valor | Unidade | Fonte (link) | Data de acesso | Observação |
|---|---|---|---|---|---|
| Carro a gasolina | 0,175 | kg CO₂/km | https://www.sinaldetransito.com.br/artigos/gases_efeito_estufa.pdf (2,098 kg CO₂/l ÷ 12 km/l) | 25/09/2026 | Rendimento de 12 km/l conforme ANPET/Metrô-RJ |
| Moto | 0,052 | kg CO₂/km | https://transportes.anpet.org.br/anpet/article/download/653/pdf_1 (2,098 kg CO₂/l ÷ 40 km/l) | 25/09/2026 | Rendimento de 40 km/l |
| Ônibus | 0,038 | kg CO₂/km por passageiro | Ipea, TD 1606: https://portalantigo.ipea.gov.br/agencia/images/stories/PDFs/TDs/td_1606.pdf (2,6 kg CO₂/l ÷ 2,3 km/l ÷ 30 passageiros) | 25/09/2026 | Lotação média de 30 passageiros |
| Metrô | 0,004 | kg CO₂/km por passageiro | https://transportes.anpet.org.br/anpet/article/download/653/pdf_1 | 25/09/2026 | Medido no Metrô do Rio de Janeiro (4,08 g/passageiro-km) |
| Bicicleta/Caminhada | 0 | kg CO₂/km | — | 25/09/2026 | Sem combustão |
| Absorção por árvore | 8,16 | kg CO₂/ano | https://www.sosma.org.br/noticias/cada-arvore-da-mata-atlantica-chega-a-retirar-163-kg-de-gas-carbonico-da-atmosfera/ (163,14 kg em 20 anos, projetado) | 25/09/2026 | Espécie: Mata Atlântica |

## Premissas
- Semanas por ano: 52
- Lotação média do ônibus: 30 passageiros
- Arredondamento das árvores: sempre para cima (`Math.Ceiling`)

## Implementação

Os fatores estão centralizados como constantes em `src/EcoTracer.Core/Services.cs`, na classe `CalculadoraEcoTracer`:

```csharp
private static readonly Dictionary<ModoTransporte, double> FatoresEmissaoKgKm = new()
{
    { ModoTransporte.CarroGasolina, 0.175 },
    { ModoTransporte.Moto, 0.052 },
    { ModoTransporte.Onibus, 0.038 },
    { ModoTransporte.Metro, 0.004 },
    { ModoTransporte.BicicletaCaminhada, 0.0 },
};

private const double AbsorcaoArvoreKgAno = 8.16;
private const int SemanasPorAno = 52;
```

## Limitações
- Os valores de gasolina/rendimento são de estudos de 2011–2023; frotas atuais podem ser mais eficientes.
- A absorção por árvore é uma projeção linear sobre 20 anos, não uma medição anual real. Um estudo do mesmo grupo mediu 7,27 kg CO₂/ano em dados observados (2000–2011); optamos pelo valor projetado (8,16) por ser mais conservador para o usuário final.
- Este modelo é uma estimativa educativa para conscientização (ODS 13), não um inventário oficial de emissões.