using EcoTracer.Core.Models;
using EcoTracer.Core.Services;

namespace EcoTracer.App
{
    public partial class FormHistorico : Form
    {
        private readonly HistoricoService _historicoService;
        private readonly Usuario _usuarioLogado;

        public FormHistorico(Usuario usuarioLogado, HistoricoService historicoService)
        {
            InitializeComponent();
            _usuarioLogado = usuarioLogado;
            _historicoService = historicoService;
            CarregarHistorico();
        }

        private void CarregarHistorico()
        {
            var lista = _historicoService.ListarPorUsuario(_usuarioLogado.Id);

            dgvHistorico.Columns.Clear();
            dgvHistorico.Columns.Add("Data", "Data");
            dgvHistorico.Columns.Add("Distancia", "Distância diária (km)");
            dgvHistorico.Columns.Add("Modo", "Meio de transporte");
            dgvHistorico.Columns.Add("Dias", "Dias por semana");
            dgvHistorico.Columns.Add("Co2Anual", "CO₂ anual (kg)");
            dgvHistorico.Columns.Add("Arvores", "Árvores necessárias/ano");

            foreach (var item in lista)
            {
                dgvHistorico.Rows.Add(
                    item.DataCalculo.ToString("dd/MM/yyyy HH:mm"),
                    item.DistanciaDiariaKm.ToString("0.0"),
                    item.Modo.ToString(),
                    item.DiasPorSemana.ToString(),
                    item.Co2AnualKg.ToString("0.0"),
                    item.ArvoresNecessariasAnual.ToString()
                );
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}