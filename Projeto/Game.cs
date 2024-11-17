using System.Diagnostics;
using MetodosMemoria;
using CheatOffsets;
using CheatPlayer;
using System.Timers;
using Timer = System.Timers.Timer;

namespace CheatGame
{
    public static class Game
    {
        // Classe principal que manipula o processo do jogo

        // Método para verificar se o processo do jogo está em execução
        public static void VerificarProcesso()
        {
            // Tenta obter o processo do jogo pelo nome
            if (ObterProcesso(NOME_PROCESSO))
            {
                HandleProcesso = Processo.Handle;
                EnderecoBase = Processo.MainModule.BaseAddress; // Endereço base do módulo principal do processo
                M = new Memoria(HandleProcesso);
                IniciarTimerVerificarProcessoAberto(true); // Inicia o timer para verificar se o processo está aberto
                return;
            }
            //Se o jogo não estiver aberto, Encerra o Cheat.
            EncerrarAplicativo("Processo do jogo não encontrado\nAbra o AssaultCube");
        }

        public const string NOMEJANELA_JOGO = "AssaultCube"; // Nome da janela do jogo
        public static Memoria M; // Instância da classe Memoria para manipulação de leitura e escrita de memória
        private const string NOME_PROCESSO = "ac_client"; // Nome do processo do jogo
        private static object ControleDeEncerramento = new object(); // Objeto para controle de threads para o encerramento do cheat
        private static bool _RecursosLiberados = false; // Flag para verificar se os recursos foram liberados
        private static Timer TimerVerificarProcessoAberto = new Timer(100); // Timer para verificar periodicamente o estado do processo
        private static Process Processo; // Instância do processo do jogo
        private static IntPtr HandleProcesso, EnderecoBase; // Handle do processo e endereço base do módulo

        public static float[] ViewMatrix { get { return M.LerMatrix(OffsetsEstaticos.ViewMatrix); } }
        // Propriedade para obter a matriz de visualização do jogo. A ViewMatrix aqui é um array de 16 indices [0, ..., 15].

        public static Player MeuJogador { get { return new Player(OffsetsEstaticos.LocalPlayer); } }
        // Propriedade para obter a instância do jogador local

        private static IntPtr PonteiroEntityList { get { return M.LerPonteiro(OffsetsEstaticos.EntityList); } }
        // Propriedade para obter o ponteiro para a lista de entidades (jogadores e NPCs)

        private static int QuantJogadores { get { return M.LerInt(OffsetsEstaticos.QuanPlayers); } }
        // Propriedade para obter a quantidade de jogadores na partida

        private static int ModoDeJogo { get { return M.LerInt(OffsetsEstaticos.ModoJogo); } }
        // Propriedade para obter o modo de jogo atual

        private static bool ProcessoFechado { get { return Processo.HasExited; } }
        // Propriedade para verificar se o processo do jogo foi fechado

        public static int? MeuTime() // Método para obter o time do jogador baseado no modo de jogo
        {
            int modoJogo = ModoDeJogo;

            if (modoJogo == 7 || modoJogo == 20 || modoJogo == 21) // 7, 20, 21 == Modo Team
            {
                return MeuJogador.Time; // Retorna o Time do jogador se o modo de jogo for Team
            }
            return null; // Retorna nulo se o modo de jogo for SinglePlayer
        }

        // Método para atualizar a lista de jogadores com base na quantidade de jogadores
        public static List<Player> AtualizarListaJogadores()
        {
            List<Player> result = new List<Player>(); // Cria uma nova Lista de Players

            for (int i = result.Count; result.Count < QuantJogadores; i++)
            {
                result.Add(new Player(PonteiroEntityList + (i * 0x4))); // Adiciona jogadores à lista com base no primeiro ponteiro da EntityList
            }

            return result; // Retorna a lista atualizada de jogadores
        }

        // Método para obter o processo do jogo pelo nome
        private static bool ObterProcesso(string nomeProcesso)
        {
            Processo = Process.GetProcessesByName(nomeProcesso).FirstOrDefault();
            return Processo != null; // Retorna true se o processo for encontrado, false caso contrário
        }

        // Método chamado pelo timer para verificar se o processo do jogo foi fechado
        private static void VerificarProcessoAberto(object? sender, ElapsedEventArgs e)
        {
            if (ProcessoFechado)
            {
                EncerrarAplicativo("O jogo foi finalizado.");
            }
        }

        // Método para iniciar ou parar o timer de verificação do processo
        private static void IniciarTimerVerificarProcessoAberto(bool ativar)
        {
            if (ativar)
            {
                TimerVerificarProcessoAberto.Elapsed += VerificarProcessoAberto;
                TimerVerificarProcessoAberto.Start(); // Inicia o timer
                return;
            }

            TimerVerificarProcessoAberto.Close(); // Libera o Timer
            TimerVerificarProcessoAberto = null;

        }

        // Método para encerrar o aplicativo e exibir uma mensagem de erro
        public static void EncerrarAplicativo(string msg)
        {
            lock (ControleDeEncerramento)
            {
                MessageBox.Show(msg + "\n\nO cheat será finalizado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dispose(); // Libera os recursos
                Environment.Exit(0); // Encerra o aplicativo
            }
        }

        // Método para liberar recursos utilizados pelo cheat
        public static void Dispose()
        {
            if (!_RecursosLiberados && M != null)
            {
                M.Dispose(); // Libera recursos de memória
                IniciarTimerVerificarProcessoAberto(false); // Para o timer
                _RecursosLiberados = true; // Marca os recursos como liberados
            }
        }
    }
}
