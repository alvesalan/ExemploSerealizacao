using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ListaPersonagem : Form
    {
        public int posicao = -1;
        public static string NOME_ARQUIVO = "Personagns.bin";
        public ListaPersonagem()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Personagem personagem = new Personagem();
            personagem.SetNome(txtNome.Text);
            personagem.SetNivelChakra(Convert.ToInt32(txtNivel.Text));
            personagem.SetCla(cbCla.SelectedItem.ToString());
            PersonagemRepository tudo = new PersonagemRepository();
            if (posicao == -1)
            {
                tudo.AdicionarPersonagem(personagem);
                MessageBox.Show("Personagem cadastarado com sucesso");
            }
            else
            {
                tudo.EditarPersonagem(personagem, posicao);
                MessageBox.Show("Personagem alterado com sucesso !!");
            }
            LimparCampo();
            AtualizarListaPersonagem();

         
            //
        }

        private void LimparCampo()
        {
            txtNome.Text = "";
            txtNivel.Text = "";
            cbCla.SelectedIndex = -1;
            posicao = -1;
        }

        private void ListaPersonagem_Activated(object sender, EventArgs e)
        {
            AtualizarListaPersonagem();
        }

        private void AtualizarListaPersonagem()
        {
            PersonagemRepository tudo = new PersonagemRepository();
            dataGridView1.Rows.Clear();

            foreach (Personagem personagem in tudo.ObterPersonagem())
            {
                dataGridView1.Rows.Add(new Object[]{
                    personagem.GetNome(),
                    personagem.GetCla(),
                    personagem.GetNivelChakra()
                });
            }
        }

        private void ListaPersonagem_Load(object sender, EventArgs e)
        {

        }

        private void ListaPersonagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.L)
            {
                ApagarFuncionario();
               
            }
            else if (e.KeyCode == Keys.F2)
            {
                EditarPersonagem();
            }
        }

        private void EditarPersonagem()
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seu zueiro, selecione algo neste grid");
                return;
                
            }
            string nome = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            PersonagemRepository repositorio = new PersonagemRepository();
            int Quantidade = 0;
            foreach(Personagem personagem in repositorio.ObterPersonagem())
            {
                if (personagem.GetNome()== nome)
                {
                    txtNome.Text = personagem.GetNome();
                    
                     txtNivel.Text = Convert.ToString(personagem.GetNivelChakra());
                     cbCla.SelectedItem = personagem.GetCla();
                     posicao = Quantidade;
                     return;
                }
            }
        }

        private void ApagarFuncionario()
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Seu zueiro, selecione algo neste grid");
                return;

            }

            string nome = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            PersonagemRepository repositorio = new PersonagemRepository();
            repositorio.ApagarPersonagem(nome);
            MessageBox.Show(nome + "Apagado com sucesso.");
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            EditarPersonagem();
        }
    }
}
