using EcoTracer.Core.Models;
using EcoTracer.Core.Services;

// TELA 1: LOGIN / ACESSO
Console.Clear();
Console.WriteLine("==================================================");
Console.WriteLine("         🔒 ECOTRACER - ACESSO AO SISTEMA        ");
Console.WriteLine("==================================================");
Console.Write(" Usuário: ");
string usuario = Console.ReadLine() ?? "";

Console.Write(" Senha:   ");
string senha = Console.ReadLine() ?? "";

if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
{
    Console.WriteLine("\n⚠️ Usuário ou senha inválidos. Encerrando aplicação.");
    return;
}

Console.WriteLine($"\n✅ Bem-vindo(a), {usuario}! Pressione qualquer tecla para continuar...");
Console.ReadKey();

var calculadora = new CalculadoraEcoTracer();
bool executando = true;

while (executando)
{
    // TELA 2: MENU PRINCIPAL
    Console.Clear();
    Console.WriteLine("==================================================");
    Console.WriteLine("        🌱 ECOTRACER - PAINEL PRINCIPAL           ");
    Console.WriteLine("==================================================");
    Console.WriteLine($" Usuário Ativo: {usuario}");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine(" 1 - Novo Cálculo de Impacto Ambiental");
    Console.WriteLine(" 2 - Sobre a Metodologia ESG & EcoTracer");
    Console.WriteLine(" 0 - Sair do Sistema");
    Console.WriteLine("==================================================");
    Console.Write(" Opção: ");

    string opcaoMenu = Console.ReadLine() ?? "";

    switch (opcaoMenu)
    {
        case "1":
            ExibirTelaCalculo(calculadora);
            break;
        case "2":
            ExibirTelaSobre();
            break;
        case "0":
            executando = false;
            Console.WriteLine("\nSessão encerrada no EcoTracer. Até logo!");
            break;
        default:
            Console.WriteLine("\n⚠️ Opção inválida! Pressione qualquer tecla...");
            Console.ReadKey();
            break;
    }
}

// TELA 3: CÁLCULO E RELATÓRIO ESG
static void ExibirTelaCalculo(CalculadoraEcoTracer calculadora)
{
    Console.Clear();
    Console.WriteLine("==================================================");
    Console.WriteLine("      📝 ECOTRACER - CÁLCULO DE PEGADA DE CO2     ");
    Console.WriteLine("==================================================\n");

    Console.Write("Digite a distância percorrida por dia (km): ");
    if (!double.TryParse(Console.ReadLine(), out double distancia) || distancia < 0)
    {
        Console.WriteLine("\n⚠️ Distância inválida. Pressione qualquer tecla para voltar...");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("\nEscolha o meio de transporte principal:");
    Console.WriteLine(" 1 - Bicicleta / Caminhada");
    Console.WriteLine(" 2 - Metrô");
    Console.WriteLine(" 3 - Ônibus");
    Console.WriteLine(" 4 - Moto");
    Console.WriteLine(" 5 - Carro a Gasolina");
    Console.Write(" Opção: ");

    string opcao = Console.ReadLine() ?? "";

    ModoTransporte modo = opcao switch
    {
        "1" => ModoTransporte.BicicletaCaminhada,
        "2" => ModoTransporte.Metro,
        "3" => ModoTransporte.Onibus,
        "4" => ModoTransporte.Moto,
        "5" => ModoTransporte.CarroGasolina,
        _   => ModoTransporte.CarroGasolina
    };

    string nomeModo = opcao switch
    {
        "1" => "Bicicleta / Caminhada",
        "2" => "Metrô",
        "3" => "Ônibus",
        "4" => "Moto",
        "5" => "Carro a Gasolina",
        _   => "Carro a Gasolina (Padrão)"
    };

    var resultado = calculadora.Calcular(distancia, modo);

    Console.Clear();
    Console.WriteLine("==================================================");
    Console.WriteLine("        📊 RELATÓRIO DE IMPACTO AMBIENTAL         ");
    Console.WriteLine("==================================================");
    Console.WriteLine($" Transporte Utilizado:   {nomeModo}");
    Console.WriteLine($" Distância Diária:       {distancia:F2} km");
    Console.WriteLine($" Emissão Diária de CO2:  {resultado.Co2DiarioKg:F3} kg");
    Console.WriteLine($" Emissão Anual Estimada: {resultado.Co2AnualKg:F3} kg");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine(" 🌳 COMPENSAÇÃO AMBIENTAL (METAS ESG)");
    Console.WriteLine($" Árvores para Compensar: {resultado.ArvoresNecessariasAnual} árvore(s)/ano");
    Console.WriteLine("==================================================\n");

    Console.WriteLine("Pressione qualquer tecla para voltar ao Menu...");
    Console.ReadKey();
}

static void ExibirTelaSobre()
{
    Console.Clear();
    Console.WriteLine("==================================================");
    Console.WriteLine("             ℹ️ SOBRE O ECOTRACER                ");
    Console.WriteLine("==================================================");
    Console.WriteLine(" O EcoTracer monitora e calcula o impacto das    ");
    Console.WriteLine(" emissões de carbono na mobilidade corporativa.   ");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine(" Arquitetura: .NET 10 / C#");
    Console.WriteLine(" Módulos: Core & Unit Tests (xUnit)");
    Console.WriteLine("==================================================\n");

    Console.WriteLine("Pressione qualquer tecla para voltar...");
    Console.ReadKey();
}
