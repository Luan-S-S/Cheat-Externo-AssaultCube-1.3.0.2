using CheatESP_AIMBOT;
using CheatGame;
using CheatOutros;

//
//Formulario
//

namespace Cheat_Externo_AssaultCube_1._3._0._2
{
    public partial class Form1 : Form
    {
        ESP esp; // Instância da classe ESP, que gerencia as funcionalidades de desenho na tela do jogo (Extra Sensory Perception)
        Aimbot aimbot; // Instância da classe Aimbot, que gerencia a funcionalidade de mira automática

        public Form1()
        {
            InitializeComponent();

            // Associando o evento de fechamento do formulário ao método Form1_FormClosing
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Verifica se o processo do jogo está aberto, antes de iniciar o cheat
            Game.VerificarProcesso();

            // Desabilita a verificação de chamadas ilegais de thread para permitir acesso aos controles de UI a partir de outras threads
            CheckForIllegalCrossThreadCalls = false;

            // Instanciando as classes ESP e Aimbot para inicializar suas funcionalidades
            esp = new ESP();
            aimbot = new Aimbot();

            // Desabilita a checkbox de desenhar linha, pois o ESP ainda não foi ativado
            checkBoxDesenhar_Linha.Enabled = false;

            // Configura o texto de ajuda no combobox, orientando o usuário a selecionar um item
            MostrarTextoSelecionarItem();
        }

        //
        // ESP
        //

        // Método que controla a ativação do ESP ao marcar/desmarcar o checkbox principal do ESP
        private void CheckBoxESP_CheckedChanged(object sender, EventArgs e)
        {
            bool ativar = CheckBoxESP.Checked; // Armazena o estado do checkbox (marcado ou desmarcado)

            // Ativa ou desativa o ESP com base na marcação do checkbox
            esp.Ativar(ativar);

            // Habilita ou desabilita o checkbox de desenhar linha dependendo do estado do ESP
            checkBoxDesenhar_Linha.Enabled = ativar;

            // Se o ESP for desativado, desmarca também o checkbox de desenhar linha
            if (!ativar) { checkBoxDesenhar_Linha.Checked = false; }
        }

        // Método que controla o checkbox para desenhar uma linha no ESP
        private void checkBoxDesenhar_Linha_CheckedChanged(object sender, EventArgs e)
        {
            // Ativa ou desativa a funcionalidade de desenhar linha de acordo com a marcação do checkbox
            esp.DesenharLinha = checkBoxDesenhar_Linha.Checked;
        }

        //
        // AIMBOT
        //

        // Método que controla a ativação do Aimbot ao marcar/desmarcar o checkbox correspondente
        private void checkBoxAimbot_CheckedChanged(object sender, EventArgs e)
        {
            // Ativa ou desativa o Aimbot com base na marcação do checkbox
            aimbot.Ativar(checkBoxAimbot.Checked);
        }

        //
        // SELECIONAR VALORES COMBOBOX
        //

        // Exibe uma mensagem de indicação "Selecione um Item" no combobox como orientação ao usuário
        private void MostrarTextoSelecionarItem()
        {
            // Adiciona o texto de ajuda "Selecione um Item" na lista de itens do combobox
            comboBoxSelecaoValor.Items.Add("Selecione um Item");

            // Define o texto de ajuda como o item selecionado
            comboBoxSelecaoValor.SelectedIndex = 7;
        }

        // Remove o texto de ajuda do combobox quando o usuário interage com a lista de opções
        private void RemoverTextoDeAjudaComboBox()
        {
            // Se o texto de ajuda estiver presente na lista de itens, remove-o
            if (comboBoxSelecaoValor.Items.Count > 7) comboBoxSelecaoValor.Items.RemoveAt(7);
        }

        // Método que é chamado ao abrir a lista do combobox, removendo o texto de ajuda
        private void comboBoxSelecaoValor_DropDown(object sender, EventArgs e)
        {
            // Remove o texto de ajuda ao expandir a lista do combobox
            RemoverTextoDeAjudaComboBox();
        }

        // Método chamado ao fechar a lista do combobox, restaurando o texto de ajuda caso nada tenha sido selecionado
        private void comboBoxSelecaoValor_DropDownClosed(object sender, EventArgs e)
        {
            // Se nenhuma opção for selecionada, restaura o texto de ajuda "Selecione um Item"
            if (comboBoxSelecaoValor.SelectedIndex == -1) MostrarTextoSelecionarItem();
        }

        //
        // TEXTBOX DIGITAR VALOR
        //

        // Método que restringe a entrada no TextBox para permitir apenas números e a tecla "Backspace"
        private void textBoxValorItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Se o caractere digitado não for um número nem a tecla "Backspace", ele é ignorado
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back) { e.Handled = true; }
        }

        //
        // BOTÃO APLICAR
        //

        // Método que define o comportamento ao clicar no botão "Aplicar", modificando o atributo do jogador com base nos dados inseridos
        private void buttonAplicar_Click(object sender, EventArgs e)
        {
            // Pega o item selecionado no combobox como string
            string item = comboBoxSelecaoValor.SelectedItem.ToString();

            // Se o item selecionado for válido e o valor do TextBox for um número, modifica o valor do atributo do jogador
            if (item != "Selecione um Item" && Int32.TryParse(textBoxValorItem.Text, out int valor))
            {
                Modificador.Modificar(item, valor); // Modifica o atributo correspondente ao item selecionado no combobox
            }

            // Limpa o TextBox após a modificação
            textBoxValorItem.Text = String.Empty;

            // Restaura o estado inicial do combobox
            RemoverTextoDeAjudaComboBox();
            MostrarTextoSelecionarItem();
        }

        //
        // CHEATS
        //

        // Métodos que ativam e desativam os cheats de acordo com a marcação dos checkboxes

        // Ativa ou desativa o cheat de bala infinita
        private void checkBoxBalaInfinita_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.BalaInfinita = checkBoxBalaInfinita.Checked;
        }

        // Ativa ou desativa o cheat de bolsa infinita
        private void checkBoxBolsaInfinita_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.BolsaInfinita = checkBoxBolsaInfinita.Checked;
        }

        // Ativa ou desativa o cheat de vida infinita
        private void checkBoxVidaInfinita_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.VidaInfinita = checkBoxVidaInfinita.Checked;
        }

        // Ativa ou desativa o cheat de tiro sem recuo
        private void checkBoxSemRecuo_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.SemRecuo = checkBoxSemRecuo.Checked;
        }

        // Ativa ou desativa o cheat de tiro rápido
        private void checkBoxTiroRapido_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.TiroRapido = checkBoxTiroRapido.Checked;
        }

        // Ativa ou desativa o cheat de ausência de repulsão ao ser atingido
        private void checkBoxSemRepulso_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.SemRepulso = checkBoxSemRepulso.Checked;
        }

        //
        // ENCERRAR PROGRAMA
        //

        // Método usado para liberar os recursos utilizados pelas instâncias criadas no formulário
        private void Liberar()
        {
            // Libera os recursos usados pelo ESP, Aimbot, Cheats e a classe Game
            esp.Dispose();
            aimbot.Dispose();
            Cheats.Dispose();
            Game.Dispose();
        }

        // Método executado quando o formulário é fechado, chamando o método "Liberar" para liberar os recursos
        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Liberar();
        }
    }

}
