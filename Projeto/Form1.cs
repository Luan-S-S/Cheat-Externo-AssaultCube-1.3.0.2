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
        ESP esp; // Inst�ncia da classe ESP, que gerencia as funcionalidades de desenho na tela do jogo (Extra Sensory Perception)
        Aimbot aimbot; // Inst�ncia da classe Aimbot, que gerencia a funcionalidade de mira autom�tica

        public Form1()
        {
            InitializeComponent();

            // Associando o evento de fechamento do formul�rio ao m�todo Form1_FormClosing
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Verifica se o processo do jogo est� aberto, antes de iniciar o cheat
            Game.VerificarProcesso();

            // Desabilita a verifica��o de chamadas ilegais de thread para permitir acesso aos controles de UI a partir de outras threads
            CheckForIllegalCrossThreadCalls = false;

            // Instanciando as classes ESP e Aimbot para inicializar suas funcionalidades
            esp = new ESP();
            aimbot = new Aimbot();

            // Desabilita a checkbox de desenhar linha, pois o ESP ainda n�o foi ativado
            checkBoxDesenhar_Linha.Enabled = false;

            // Configura o texto de ajuda no combobox, orientando o usu�rio a selecionar um item
            MostrarTextoSelecionarItem();
        }

        //
        // ESP
        //

        // M�todo que controla a ativa��o do ESP ao marcar/desmarcar o checkbox principal do ESP
        private void CheckBoxESP_CheckedChanged(object sender, EventArgs e)
        {
            bool ativar = CheckBoxESP.Checked; // Armazena o estado do checkbox (marcado ou desmarcado)

            // Ativa ou desativa o ESP com base na marca��o do checkbox
            esp.Ativar(ativar);

            // Habilita ou desabilita o checkbox de desenhar linha dependendo do estado do ESP
            checkBoxDesenhar_Linha.Enabled = ativar;

            // Se o ESP for desativado, desmarca tamb�m o checkbox de desenhar linha
            if (!ativar) { checkBoxDesenhar_Linha.Checked = false; }
        }

        // M�todo que controla o checkbox para desenhar uma linha no ESP
        private void checkBoxDesenhar_Linha_CheckedChanged(object sender, EventArgs e)
        {
            // Ativa ou desativa a funcionalidade de desenhar linha de acordo com a marca��o do checkbox
            esp.DesenharLinha = checkBoxDesenhar_Linha.Checked;
        }

        //
        // AIMBOT
        //

        // M�todo que controla a ativa��o do Aimbot ao marcar/desmarcar o checkbox correspondente
        private void checkBoxAimbot_CheckedChanged(object sender, EventArgs e)
        {
            // Ativa ou desativa o Aimbot com base na marca��o do checkbox
            aimbot.Ativar(checkBoxAimbot.Checked);
        }

        //
        // SELECIONAR VALORES COMBOBOX
        //

        // Exibe uma mensagem de indica��o "Selecione um Item" no combobox como orienta��o ao usu�rio
        private void MostrarTextoSelecionarItem()
        {
            // Adiciona o texto de ajuda "Selecione um Item" na lista de itens do combobox
            comboBoxSelecaoValor.Items.Add("Selecione um Item");

            // Define o texto de ajuda como o item selecionado
            comboBoxSelecaoValor.SelectedIndex = 7;
        }

        // Remove o texto de ajuda do combobox quando o usu�rio interage com a lista de op��es
        private void RemoverTextoDeAjudaComboBox()
        {
            // Se o texto de ajuda estiver presente na lista de itens, remove-o
            if (comboBoxSelecaoValor.Items.Count > 7) comboBoxSelecaoValor.Items.RemoveAt(7);
        }

        // M�todo que � chamado ao abrir a lista do combobox, removendo o texto de ajuda
        private void comboBoxSelecaoValor_DropDown(object sender, EventArgs e)
        {
            // Remove o texto de ajuda ao expandir a lista do combobox
            RemoverTextoDeAjudaComboBox();
        }

        // M�todo chamado ao fechar a lista do combobox, restaurando o texto de ajuda caso nada tenha sido selecionado
        private void comboBoxSelecaoValor_DropDownClosed(object sender, EventArgs e)
        {
            // Se nenhuma op��o for selecionada, restaura o texto de ajuda "Selecione um Item"
            if (comboBoxSelecaoValor.SelectedIndex == -1) MostrarTextoSelecionarItem();
        }

        //
        // TEXTBOX DIGITAR VALOR
        //

        // M�todo que restringe a entrada no TextBox para permitir apenas n�meros e a tecla "Backspace"
        private void textBoxValorItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Se o caractere digitado n�o for um n�mero nem a tecla "Backspace", ele � ignorado
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)Keys.Back) { e.Handled = true; }
        }

        //
        // BOT�O APLICAR
        //

        // M�todo que define o comportamento ao clicar no bot�o "Aplicar", modificando o atributo do jogador com base nos dados inseridos
        private void buttonAplicar_Click(object sender, EventArgs e)
        {
            // Pega o item selecionado no combobox como string
            string item = comboBoxSelecaoValor.SelectedItem.ToString();

            // Se o item selecionado for v�lido e o valor do TextBox for um n�mero, modifica o valor do atributo do jogador
            if (item != "Selecione um Item" && Int32.TryParse(textBoxValorItem.Text, out int valor))
            {
                Modificador.Modificar(item, valor); // Modifica o atributo correspondente ao item selecionado no combobox
            }

            // Limpa o TextBox ap�s a modifica��o
            textBoxValorItem.Text = String.Empty;

            // Restaura o estado inicial do combobox
            RemoverTextoDeAjudaComboBox();
            MostrarTextoSelecionarItem();
        }

        //
        // CHEATS
        //

        // M�todos que ativam e desativam os cheats de acordo com a marca��o dos checkboxes

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

        // Ativa ou desativa o cheat de tiro r�pido
        private void checkBoxTiroRapido_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.TiroRapido = checkBoxTiroRapido.Checked;
        }

        // Ativa ou desativa o cheat de aus�ncia de repuls�o ao ser atingido
        private void checkBoxSemRepulso_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.SemRepulso = checkBoxSemRepulso.Checked;
        }

        //
        // ENCERRAR PROGRAMA
        //

        // M�todo usado para liberar os recursos utilizados pelas inst�ncias criadas no formul�rio
        private void Liberar()
        {
            // Libera os recursos usados pelo ESP, Aimbot, Cheats e a classe Game
            esp.Dispose();
            aimbot.Dispose();
            Cheats.Dispose();
            Game.Dispose();
        }

        // M�todo executado quando o formul�rio � fechado, chamando o m�todo "Liberar" para liberar os recursos
        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Liberar();
        }
    }

}
