using EcoTracer.Core.Data;
using EcoTracer.Core.Exceptions;
using EcoTracer.Core.Services;

namespace EcoTracer.App
{
    public partial class FormCadastro : Form
    {
        private readonly UsuarioService _usuarioService = new(new AppDatabase());

        public FormCadastro()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                lblMensagem.ForeColor = Color.Firebrick;
                lblMensagem.Text = "Preencha todos os campos.";
                return;
            }

            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                lblMensagem.ForeColor = Color.Firebrick;
                lblMensagem.Text = "As senhas não coincidem.";
                return;
            }

            try
            {
                _usuarioService.Cadastrar(txtNome.Text, txtEmail.Text, txtSenha.Text);
                lblMensagem.ForeColor = Color.FromArgb(27, 94, 32);
                lblMensagem.Text = "Conta criada! Você já pode fazer login.";
            }
            catch (EmailJaCadastradoException ex)
            {
                lblMensagem.ForeColor = Color.Firebrick;
                lblMensagem.Text = ex.Message;
            }
        }
    }
}