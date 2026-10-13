using EcoTracer.Core.Data;
using EcoTracer.Core.Exceptions;
using EcoTracer.Core.Services;

namespace EcoTracer.App
{
    public partial class FormLogin : Form
    {
        private readonly UsuarioService _usuarioService = new(new AppDatabase());

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = _usuarioService.Autenticar(txtEmail.Text, txtSenha.Text);
                lblErro.Text = "";

                this.Hide();
                var form1 = new Form1(usuario);
                form1.ShowDialog();
                this.Close();
            }
            catch (CredenciaisInvalidasException ex)
            {
                lblErro.Text = ex.Message;
            }
        }

        private void linkCriarConta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var formCadastro = new FormCadastro();
            formCadastro.ShowDialog();
        }
    }
}