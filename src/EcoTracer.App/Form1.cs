using EcoTracer.Core.Exceptions;
using EcoTracer.Core.Models;
using EcoTracer.Core.Services;

namespace EcoTracer.App
{
    public partial class Form1 : Form
    {
        private readonly CalculadoraEcoTracer _calculadora = new();

        private static readonly Dictionary<string, ModoTransporte> TransportesLabels = new()
        {
            { "Carro a gasolina", ModoTransporte.CarroGasolina },
            { "Moto", ModoTransporte.Moto },
            { "Ônibus", ModoTransporte.Onibus },
            { "Metrô", ModoTransporte.Metro },
            { "Bicicleta/Caminhada", ModoTransporte.BicicletaCaminhada },
        };

        public Form1()
        {
            InitializeComponent();
            comboTransporte.Items.AddRange(TransportesLabels.Keys.ToArray());
            comboTransporte.SelectedIndex = 0;
        }

                private (double distancia, int dias, ModoTransporte transporte) LerFormulario()
        {
            double distancia = double.Parse(txtDistancia.Text.Replace(",", "."));
            int dias = int.Parse(txtDias.Text);
            ModoTransporte transporte = TransportesLabels[comboTransporte.SelectedItem!.ToString()!];
            return (distancia, dias, transporte);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                var (distancia, dias, transporte) = LerFormulario();
                var resultado = _calculadora.Calcular(distancia, transporte, dias);

                lblResultado.Text =
                    $"Emissão diária: {resultado.Co2DiarioKg} kg de CO2\n" +
                    $"Emissão anual: {resultado.Co2AnualKg} kg de CO2\n" +
                    $"Equivalente a {resultado.ArvoresNecessariasAnual} árvores/ano  para compensar";
                lblResultado.ForeColor = Color.FromArgb(27, 94, 32);
            }
            catch (FormatException)
            {
                MessageBox.Show("Digite números válidos para distância e dias.", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (EntradaInvalidaException ex)
            {
                MessageBox.Show(ex.Message, "Erro nos dados",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro inesperado",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}