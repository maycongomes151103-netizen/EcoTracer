namespace EcoTracer.App
{
    partial class FormCadastro
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Label lblTitulo;
        private Label lblNome;
        private TextBox txtNome;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblSenha;
        private TextBox txtSenha;
        private Label lblConfirmarSenha;
        private TextBox txtConfirmarSenha;
        private Button btnCadastrar;
        private Label lblMensagem;
        private Button btnFechar;

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblNome = new Label();
            txtNome = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblSenha = new Label();
            txtSenha = new TextBox();
            lblConfirmarSenha = new Label();
            txtConfirmarSenha = new TextBox();
            btnCadastrar = new Button();
            lblMensagem = new Label();
            btnFechar = new Button();
            SuspendLayout();

            lblTitulo.Text = "Criar conta";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(27, 94, 32);
            lblTitulo.Location = new Point(40, 25);
            lblTitulo.Size = new Size(300, 40);

            lblNome.Text = "Nome";
            lblNome.Location = new Point(40, 80);
            lblNome.Size = new Size(100, 20);

            txtNome.Location = new Point(40, 103);
            txtNome.Size = new Size(300, 25);

            lblEmail.Text = "Email";
            lblEmail.Location = new Point(40, 138);
            lblEmail.Size = new Size(100, 20);

            txtEmail.Location = new Point(40, 161);
            txtEmail.Size = new Size(300, 25);

            lblSenha.Text = "Senha";
            lblSenha.Location = new Point(40, 196);
            lblSenha.Size = new Size(100, 20);

            txtSenha.Location = new Point(40, 219);
            txtSenha.Size = new Size(300, 25);
            txtSenha.PasswordChar = '*';

            lblConfirmarSenha.Text = "Confirmar senha";
            lblConfirmarSenha.Location = new Point(40, 254);
            lblConfirmarSenha.Size = new Size(150, 20);

            txtConfirmarSenha.Location = new Point(40, 277);
            txtConfirmarSenha.Size = new Size(300, 25);
            txtConfirmarSenha.PasswordChar = '*';

            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.Location = new Point(40, 320);
            btnCadastrar.Size = new Size(300, 35);
            btnCadastrar.BackColor = Color.FromArgb(27, 94, 32);
            btnCadastrar.ForeColor = Color.White;
            btnCadastrar.Click += btnCadastrar_Click;

            lblMensagem.Text = "";
            lblMensagem.ForeColor = Color.Firebrick;
            lblMensagem.Location = new Point(40, 365);
            lblMensagem.Size = new Size(300, 40);

            btnFechar.Text = "Fechar";
            btnFechar.Location = new Point(240, 420);
            btnFechar.Size = new Size(100, 35);
            btnFechar.Click += btnFechar_Click;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 480);
            Controls.Add(lblTitulo);
            Controls.Add(lblNome);
            Controls.Add(txtNome);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblSenha);
            Controls.Add(txtSenha);
            Controls.Add(lblConfirmarSenha);
            Controls.Add(txtConfirmarSenha);
            Controls.Add(btnCadastrar);
            Controls.Add(lblMensagem);
            Controls.Add(btnFechar);
            Text = "EcoTracer - Criar conta";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}