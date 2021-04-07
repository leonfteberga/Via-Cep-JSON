using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;

namespace ViaCepJSON
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            btnPesquisar.Enabled = false;
            try
            {
                // Instanciar o obj Endereco:
                Endereco end = new Endereco();
                // Objeto para "acessar a internet":
                // Chamada assíncrona dentro do using:
                using (WebClient navegador = new WebClient())
                {
                    // Link a ser acessado:
                    string link = "http://viacep.com.br/ws/" + txbCEP.Text + "/json/";
                    // Baixar o conteúdo desse link como uma string:
                    string json = navegador.DownloadString(link);
                    // Converter para UTF-8:
                    json = Encoding.UTF8.GetString(Encoding.Default.GetBytes(json));
                    // Desserealizar o JSON (converter o resultado para um obj):
                    end = JsonConvert.DeserializeObject<Endereco>(json);
                }
                //MessageBox.Show(end.localidade);
                // Mostrar os resultados nos campos:
                txbLogradouro.Text = end.logradouro;
                txbComplemento.Text = end.complemento;
                txbBairro.Text = end.bairro;
                txbLocalidade.Text = end.localidade;
                txbDDD.Text = end.ddd;
                txbUF.Text = end.uf;
            }
            catch
            {
                var resultado = MessageBox.Show("Verifique as informações digitadas!", "ERRO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //if(resultado == DialogResult.Yes)
                //{
                //    MessageBox.Show("Você clicou no sim");
                //}
                //else
                //{
                //    MessageBox.Show("Você clicou no NÃO");
                //}
            }
            btnPesquisar.Enabled = true;
        }


    }
}
