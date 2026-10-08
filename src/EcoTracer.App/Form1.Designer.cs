namespace EcoTracer.App
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblOds;
        private Label lblCampoDistancia;
        private TextBox txtDistancia;
        private Label lblCampoTransporte;
        private ComboBox comboTransporte;
        private Label lblCampoDias;
        private TextBox txtDias;
        private Button btnCalcular;
        private Panel panelResultado;
        private Label lblResultado;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblOds = new Label();
            lblCampoDistancia = new Label();
            txtDistancia = new TextBox();
            lblCampoTransporte = new Label();
            comboTransporte = new ComboBox();
            lblCampoDias = new Label();
            txtDias = new TextBox();
            btnCalcular = new Button();
            panelResultado = new Panel();
            lblResultado = new Label();
            SuspendLayout();

            ClientSize = new Size(420, 520);
            Text = "EcoTracer";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(244, 248, 244);
            StartPosition = FormStartPosition.CenterScreen;

            lblTitulo.Text = "EcoTracer";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(27, 94, 32);
            lblTitulo.Location = new Point(24, 20);
            lblTitulo.AutoSize = true;

            lblOds.Text = "ODS 13 - Acao Contra a Mudanca Global do Clima";
            lblOds.Font = new Font("Segoe UI", 9);
            lblOds.Location = new Point(24, 52);
            lblOds.AutoSize = true;

            lblCampoDistancia.Text = "Distancia diaria (km)";
            lblCampoDistancia.Location = new Point(24, 92);
            lblCampoDistancia.AutoSize = true;

            txtDistancia.Location = new Point(24, 112);
            txtDistancia.Size = new Size(372, 23);

            lblCampoTransporte.Text = "Meio de transporte";
            lblCampoTransporte.Location = new Point(24, 148);
            lblCampoTransporte.AutoSize = true;

            comboTransporte.Location = new Point(24, 168);
            comboTransporte.Size = new Size(372, 23);
            comboTransporte.DropDownStyle = ComboBoxStyle.DropDownList;

            lblCampoDias.Text = "Dias por semana (1 a 7)";
            lblCampoDias.Location = new Point(24, 204);
            lblCampoDias.AutoSize = true;

            txtDias.Location = new Point(24, 224);
            txtDias.Size = new Size(372, 23);

            btnCalcular.Text = "Calcular";
            btnCalcular.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnCalcular.Location = new Point(24, 264);
            btnCalcular.Size = new Size(372, 36);
            btnCalcular.Click += btnCalcular_Click;

            panelResultado.Location = new Point(24, 316);
            panelResultado.Size = new Size(372, 180);
            panelResultado.BackColor = Color.White;
            panelResultado.BorderStyle = BorderStyle.FixedSingle;
            panelResultado.Controls.Add(lblResultado);

            lblResultado.Text = "Preencha os dados e clique em Calcular.";
            lblResultado.Location = new Point(16, 16);
            lblResultado.Size = new Size(340, 148);
            lblResultado.Font = new Font("Segoe UI", 10);

            Controls.Add(lblTitulo);
            Controls.Add(lblOds);
            Controls.Add(lblCampoDistancia);
            Controls.Add(txtDistancia);
            Controls.Add(lblCampoTransporte);
            Controls.Add(comboTransporte);
            Controls.Add(lblCampoDias);
            Controls.Add(txtDias);
            Controls.Add(btnCalcular);
            Controls.Add(panelResultado);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}