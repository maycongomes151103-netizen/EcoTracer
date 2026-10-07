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
            { "Onibus", ModoTransporte.Onibus },
            { "Metro", ModoTransporte.Metro },
            { "Bicicleta/Caminhada", ModoTransporte.BicicletaCaminhada },
        };

        public Form1()
        {
            InitializeComponent();
            comboTransporte.Items.AddRange(TransportesLabels.Keys.ToArray());
            comboTransporte.SelectedIndex = 0;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                double distancia = double.Parse(txtDistancia.Text.Replace(",", "."));
                int dias = int.Parse(txtDias.Text);
                ModoTransporte transporte = TransportesLabels[comboTransporte.SelectedItem!.ToString()!];

                var resultado = _calculadora.Calcular(distancia, transporte, dias);

                lblResultado.Text =
                    $"Emissao diaria: {resultado.Co2DiarioKg} kg de CO2\n" +
                    $"Emissao anual: {resultado.Co2AnualKg} kg de CO2\n" +
                    $"Equivalente a {resultado.ArvoresNecessariasAnual} arvores/ano para compensar";
                lblResultado.ForeColor = Color.FromArgb(27, 94, 32);
            }
            catch (FormatException)
            {
                MessageBox.Show("Digite numeros validos para distancia e dias.", "Erro",
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