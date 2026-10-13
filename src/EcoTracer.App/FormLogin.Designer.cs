namespace EcoTracer.App
{
    partial class FormLogin
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
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblSenha;
        private TextBox txtSenha;
        private Button btnEntrar;
        private LinkLabel linkCriarConta;
        private Label lblErro;

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblSenha = new Label();
            txtSenha = new TextBox();
            btnEntrar = new Button();
            linkCriarConta = new LinkLabel();
            lblErro = new Label();
            SuspendLayout();

            lblTitulo.Text = "EcoTracer - Login";
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(27, 94, 32);
            lblTitulo.Location = new Point(40, 30);
            lblTitulo.Size = new Size(300, 40);

            lblEmail.Text = "Email";
            lblEmail.Location = new Point(40, 95);
            lblEmail.Size = new Size(100, 20);

            txtEmail.Location = new Point(40, 118);
            txtEmail.Size = new Size(300, 25);

            lblSenha.Text = "Senha";
            lblSenha.Location = new Point(40, 155);
            lblSenha.Size = new Size(100, 20);

            txtSenha.Location = new Point(40, 178);
            txtSenha.Size = new Size(300, 25);
            txtSenha.PasswordChar = '*';

            btnEntrar.Text = "Entrar";
            btnEntrar.Location = new Point(40, 220);
            btnEntrar.Size = new Size(300, 35);
            btnEntrar.BackColor = Color.FromArgb(27, 94, 32);
            btnEntrar.ForeColor = Color.White;
            btnEntrar.Click += btnEntrar_Click;

            linkCriarConta.Text = "Não tem conta? Criar conta";
            linkCriarConta.Location = new Point(40, 265);
            linkCriarConta.Size = new Size(300, 20);
            linkCriarConta.LinkClicked += linkCriarConta_LinkClicked;

            lblErro.Text = "";
            lblErro.ForeColor = Color.Firebrick;
            lblErro.Location = new Point(40, 295);
            lblErro.Size = new Size(300, 40);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 350);
            Controls.Add(lblTitulo);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblSenha);
            Controls.Add(txtSenha);
            Controls.Add(btnEntrar);
            Controls.Add(linkCriarConta);
            Controls.Add(lblErro);
            Text = "EcoTracer - Login";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}