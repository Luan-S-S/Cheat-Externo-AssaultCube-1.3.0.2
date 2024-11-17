using CheatGame;
using CheatOffsets;
using CheatPlayer;
using MetodosSobreposicao;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Timers;
using Timer = System.Timers.Timer;

namespace CheatESP_AIMBOT
{
    //
    //ESP
    //
    public class ESP : IDisposable
    {
        // A classe ESP (Extra Sensory Perception) é responsável por fornecer ao jogador uma vantagem visual ao revelar informações extras dos adversários.
        // Ela desenha uma sobreposição gráfica na tela, destacando os inimigos em tempo real com um retângulo ao redor de suas posições e, opcionalmente, uma linha
        // que vai da base da tela até os pés dos adversários.
        //
        // O funcionamento do ESP envolve a criação de um formulário transparente que é sobreposto à janela do jogo, sem interferir diretamente na renderização do jogo.
        // Este formulário atua como uma camada invisível, onde os elementos gráficos são desenhados para indicar a posição exata dos oponentes no ambiente 3D,
        // convertendo essas posições para coordenadas 2D da tela.
        //
        // O ESP é continuamente atualizado para refletir as novas posições dos adversários, utilizando informações da ViewMatrix do jogo, que transforma as coordenadas
        // 3D (mundo do jogo) em coordenadas 2D (tela do usuário). Ele detecta se os jogadores estão visíveis na tela e, se estiverem, desenha o retângulo em volta
        // do inimigo e a linha, caso essa opção esteja habilitada.
        //
        // A classe conta com um sistema de atualização baseado em Timer, garantindo que a sobreposição gráfica seja redesenhada periodicamente de acordo com a posição
        // atual dos jogadores. O formulário sobreposto é redimensionado automaticamente caso a janela do jogo seja ajustada, e todos os desenhos são feitos em um
        // objeto Graphics vinculado ao formulário.

        public bool DesenharLinha { get; set; }
        // Flag que indica se deve desenhar a linha da base da janela até os pés do inimigo.

        private Form FormularioSobreposto;
        // Formulário sobreposto na janela do jogo, onde os desenhos do ESP serão realizados.

        private Timer TimerESP = new Timer(18);
        // Timer responsável por atualizar periodicamente a lista de jogadores e redesenhar o ESP.

        private List<Player> ListaPlayers { get { return Game.AtualizarListaJogadores(); } }
        // Lista de players na partida, quando acionada ela retorna uma lista atualizada de players na partida
        //Atravez do metodo "Game.AtualizarListaJogadores"

        private Sobreposicao Sobreposto = new Sobreposicao(Game.NOMEJANELA_JOGO);
        // Instância da classe "Sobreposicao" que lida com a sobreposição do formulário sobre a janela do jogo.

        private Point
            PosicaoLinha, // Posição inicial da linha desenhada no ESP, da base da janela até o inimigo.
            PosPesTela, // Posição dos pés do jogador inimigo na tela (coordenadas em 2D).
            PosCabecaTela; // Posição da cabeça do jogador inimigo na tela (coordenadas em 2D).

        private Pen
            CorRetangulo = new Pen(Color.LightBlue), // Cor do retângulo desenhado ao redor do inimigo.
            CorLinha = new Pen(Color.Purple); // Cor da linha que será desenhada até os pés do inimigo.

        // Método pivo para ativar ou desativar o ESP.
        public void Ativar(bool ativar)
        {
            if (ativar)
            {
                // Se ativar, sobrepõe o formulário e inicia o timer de atualização.
                SobreporFormulario(ativar);
                AtivarTimerESP(ativar);
                return;
            }

            // Se desativar, para o timer e remove o formulário sobreposto.
            AtivarTimerESP(ativar);
            SobreporFormulario(ativar);
            DesenharLinha = ativar; // Desativa a flag de desenho de linha.
        }


        // Método responsável por ativar ou desativar o TimerESP.
        private void AtivarTimerESP(bool ativar)
        {
            if (ativar)
            {
                // Associa o método MetodoESP ao evento Elapsed do Timer e inicia o Timer.
                TimerESP.Elapsed += MetodoESP;
                TimerESP.Start();
                return;
            }

            // Remove a associação do método MetodoESP e para o Timer.
            TimerESP.Elapsed -= MetodoESP;
            TimerESP.Stop();
        }


        // Método disparado pelo TimerESP que redesenha o formulário.
        private void MetodoESP(object? sender, ElapsedEventArgs e)
        {
            // Atualiza completamente o formulário sobreposto.
            FormularioSobreposto.Refresh();

            // Reajusta a posição da linha se houver redimensionamento do formulário.
            if (Sobreposto.ReajustarSobreposicao() && DesenharLinha)
            {
                ReajustarPosicaoLinha();
            }
        }

        // Método para sobrepor ou liberar o formulário sobreposto sobre a janela do jogo.
        private void SobreporFormulario(bool sobrepor)
        {
            if (sobrepor)
            {
                // Cria um novo formulário e associa o evento de desenho ao método DesenhoESP.
                FormularioSobreposto = new Form();
                FormularioSobreposto.Paint += DesenhoESP;

                // Executa a sobreposição do formulário sobre a janela do jogo.
                Sobreposto.Sobrepor(FormularioSobreposto);

                // Ajusta a posição da linha com base nas dimensões do formulário.
                ReajustarPosicaoLinha();
                return;
            }
            // Libera a sobreposição do formulário, removendo-o da janela do jogo.
            Sobreposto.LiberarSobreposicao();
        }

        // Método responsável por realizar os desenhos no formulário sobreposto.
        // Assinado no evento Paint do formulário.
        private void DesenhoESP(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; // Obtém a propriedade Graphics do formulário. Usado para desenhar

            // Itera sobre todos os jogadores na lista de players.
            // Se o jogador estiver vivo, for inimigo e sua posição estiver visível na tela, desenha o retângulo ao redor dele.
            foreach (Player p in ListaPlayers)
            {
                if (p.EstaVivo 
                    && p.Time != Game.MeuTime()
                    && WorldToScreem(p.PosicaoPes, ref PosPesTela)
                    && WorldToScreem(p.PosicaoCabeca, ref PosCabecaTela))
                {
                    // Desenha o retângulo ao redor do inimigo.
                    g.DrawRectangle(CorRetangulo, Retangulo(PosPesTela, PosCabecaTela));

                    // Se a flag DesenharLinha estiver ativada, desenha a linha.
                    if (DesenharLinha) g.DrawLine(CorLinha, PosicaoLinha, PosPesTela);
                }
            }
        }

        // Método para calcular o retângulo ao redor do jogador inimigo com base na posição dos pés e da cabeça.
        private Rectangle Retangulo(Point pes, Point cabeca)
        {
            int alturaY = pes.Y - cabeca.Y;
            int x = cabeca.X - alturaY / 4; // Calcula a posição X do retângulo, ajustado pela altura do player.
            int y = cabeca.Y; // A posição Y é a posição da cabeça do jogador.
            int width = alturaY / 2; // A largura do retângulo é proporcional à altura do player.
            int height = alturaY; // A altura é a distância entre os pés e a cabeça.

            return new Rectangle(x, y, width, height);
        }

        // Método que ajusta a posição da linha no meio inferior do formulário sobreposto.
        private void ReajustarPosicaoLinha()
        {
            PosicaoLinha.X = FormularioSobreposto.Width / 2; // Centraliza a linha horizontalmente.
            PosicaoLinha.Y = FormularioSobreposto.Height; // A linha parte da base do formulário (parte inferior).
        }

        // Converte a posição 3D do jogador no mundo para coordenadas 2D na tela (Point).
        // Retorna true se o jogador estiver visível na tela, false caso contrário.
        private bool WorldToScreem(Vector3 PosPlayer, ref Point result)
        {
            // Método responsável por converter as coordenadas 3D (posição do jogador no mundo do jogo) em coordenadas 2D na tela.
            // Essa conversão é feita com base na "ViewMatrix" do jogo, que transforma as posições do mundo 3D para as coordenadas
            // 2D visíveis na tela do usuário.
            //
            // Parâmetros:
            // - PosPlayer: Vetor contendo a posição do jogador no mundo 3D (X, Y, Z).
            // - result: Referência para armazenar a posição 2D resultante (coordenadas X e Y na tela).
            //
            // Retorno:
            // - Retorna true se o jogador estiver visível na tela, ou seja, se a transformação para 2D resultar em coordenadas válidas.
            // - Retorna false se o jogador estiver fora do campo de visão da câmera (não visível na tela).


            var vmtx = Game.ViewMatrix; // Obtém a matriz de visão (ViewMatrix) do jogo, usada para converter de 3D para 2D.
            //A ViewMatrix aqui é um array de 16 indices [0, ..., 15].

            // Calcula a componente W da posição na tela, que será usada para determinar a profundidade do jogador em relação à câmera.
            float screenW = (vmtx[3] * PosPlayer.X) + (vmtx[7] * PosPlayer.Y) + (vmtx[11] * PosPlayer.Z) + vmtx[15];

            // Verifica se o jogador está à frente da câmera. Se screenW for muito pequeno ou negativo, o jogador está fora da visão.
            if (screenW > 0.001f)
            {
                // Calcula a posição X na tela (screenX) aplicando a matriz de visão ao vetor da posição do jogador.
                float screenX = (vmtx[0] * PosPlayer.X) + (vmtx[4] * PosPlayer.Y) + (vmtx[8] * PosPlayer.Z) + vmtx[12];

                // Calcula a posição Y na tela (screenY) aplicando a matriz de visão ao vetor da posição do jogador.
                float screenY = (vmtx[1] * PosPlayer.X) + (vmtx[5] * PosPlayer.Y) + (vmtx[9] * PosPlayer.Z) + vmtx[13];

                // Define as coordenadas da tela (centro da tela é a origem, com as dimensões do formulário).
                float camX = FormularioSobreposto.Width / 2f;  // Metade da largura da tela.
                float camY = FormularioSobreposto.Height / 2f; // Metade da altura da tela.

                // Converte as coordenadas 3D do jogador para coordenadas 2D na tela usando a relação screenX/screenW e screenY/screenW.
                float x = camX + (camX * screenX / screenW);  // Calcula a posição X final na tela.
                float y = camY - (camY * screenY / screenW);  // Calcula a posição Y final na tela.

                // Armazena o resultado como coordenadas 2D (inteiras) na variável result.
                result.X = (int)x;
                result.Y = (int)y;

                return true; // Retorna true indicando que o jogador está visível na tela.
            }

            // Se screenW for negativo ou muito pequeno, o jogador está fora do campo de visão e não será desenhado.
            return false;
        }


        // Método para liberar os recursos usados pela classe ESP.
        public void Dispose()
        {
            Ativar(false); // Desativa o ESP.
            TimerESP.Close(); // Fecha o TimerESP.
            TimerESP = null;
            GC.SuppressFinalize(this); // Suprime a chamada do coletor de lixo para o destrutor.
        }

        // Destrutor da classe. Chama o Dispose para liberar os recursos. Caso o Dispose não tenha sido chamado manualmente.
        ~ESP() { Dispose(); }
    }

    //
    //AIMBOT
    //
    public class Aimbot : IDisposable
    {
        // Aimbot é uma funcionalidade que automaticamente ajusta a mira do jogador para focar no inimigo mais próximo.
        // Ele atua modificando os ângulos da mira do jogador para coincidir com a posição da cabeça do inimigo na tela.
        // O Aimbot é ativado enquanto o botão direito do mouse está pressionado e busca continuamente o inimigo mais próximo.

        // Importa o método "GetAsyncKeyState" da biblioteca User32.dll, responsável por verificar o estado de uma tecla ou botão.
        // O método retorna informações sobre se o botão está sendo pressionado ou se foi pressionado desde a última verificação.
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int botao);

        private List<Player> ListaPlayers { get { return Game.AtualizarListaJogadores(); } }
        // Propriedade que obtém uma lista atualizada de jogadores na partida, chamando o método "AtualizarListaJogadores" da classe "Game".
        
        private Timer TimerAimbot = new Timer(100);
        // Timer que define o intervalo de tempo entre cada execução do aimbot.A cada 100 milissegundos, o Aimbot verifica e ajusta a mira.

        private int Botao = 02;
        // Botão usado para ativar o Aimbot. O valor 02 corresponde ao botão direito do mouse.
        
        private Player MeuPlayer, Inimigo;
        // Instâncias de players, sendo "MeuPlayer" o jogador local e "Inimigo" o inimigo mais próximo, alvo do aimbot.

        private bool BotaoPressionado { get { return GetAsyncKeyState(Botao) < 0; } }
        // Propriedade que retorna "true" se o botão direito do mouse estiver pressionado, utilizando o método GetAsyncKeyState.

        // Método principal para ativar ou desativar o Aimbot.
        // Se ativar, ele começa a monitorar e ajustar a mira. Se desativar, para o timer e a execução.
        public void Ativar(bool ativar)
        {
            if (ativar)
            {
                // Associa o método "MetodoAimbot" ao evento de temporização do TimerAimbot e inicia o Timer.
                // Também exibe uma mensagem informando que o Aimbot será ativado com o botão direito do mouse.
                TimerAimbot.Elapsed += MetodoAimbot;
                TimerAimbot.Start();
                MessageBox.Show("O Aimbot é Ativado ao apertar o 'Botão Direito' do mouse!", "Aviso Aimbot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Remove o método associado ao evento do Timer e para o Timer.
            TimerAimbot.Elapsed -= MetodoAimbot;
            TimerAimbot.Stop();
        }

        // Método principal do Aimbot. Este método é chamado repetidamente pelo Timer a cada 100ms.
        // Ele verifica se o botão direito está pressionado e ajusta a mira para o inimigo mais próximo.
        private void MetodoAimbot(object sender, ElapsedEventArgs e)
        {
            if (BotaoPressionado) // Verifica se o botão direito do mouse está pressionado.
            {
                MeuPlayer = Game.MeuJogador; // Obtém a instância atual do jogador local.

                // Seleciona o inimigo mais próximo ao jogador e que está vivo.
                Inimigo = ListaPlayers.Where(p => p.EstaVivo && p.Time != Game.MeuTime()).OrderBy(p => MeuPlayer.Distancia(p)).FirstOrDefault();

                TimerAimbot.Stop(); // Pausa o timer temporariamente para evitar chamadas repetitivas enquanto ajusta a mira.

                // Enquanto houver um inimigo, estiver vivo, e o botão direito continuar pressionado, ajusta a mira continuamente.
                while (Inimigo != null && Inimigo.EstaVivo && BotaoPressionado)
                {
                    Vector2 angulo = CalcularAngulo(Inimigo, MeuPlayer); // Calcula os ângulos X e Y para mirar na cabeça do inimigo.
                    Mirar(angulo.X, angulo.Y); // Ajusta a mira para a posição da cabeça do inimigo.
                    Thread.Sleep(30); // Aguarda 30ms antes de realizar o próximo ajuste para suavizar o movimento da mira.
                }

                TimerAimbot.Start(); // Reinicia o Timer para continuar verificando os jogadores.
            }
        }

        // Método responsável por calcular os ângulos de rotação(X) e elevação(Y) necessários para alinhar a mira
        // na cabeça do inimigo. Ele utiliza as diferenças de posição entre o jogador local e o inimigo no mundo do jogo,
        // considerando tanto a distância horizontal quanto a diferença de altura (eixo Z).
        private Vector2 CalcularAngulo(Player inimigo, Player meuPlayer)
        {

            // Variáveis que irão armazenar os ângulos X (horizontal) e Y (vertical).
            float x, y;

            // Calcula a diferença nas coordenadas X e Y entre o inimigo e o jogador local (distância horizontal).
            var deltaX = inimigo.PosicaoPes.X - meuPlayer.PosicaoPes.X;
            var deltaY = inimigo.PosicaoPes.Y - meuPlayer.PosicaoPes.Y;

            // Calcula o ângulo horizontal (X) usando a função 'Atan2' que retorna o ângulo entre a linha conectando dois pontos
            // e o eixo X. A função Atan2 é usada para evitar ambiguidades em qual quadrante o ângulo está.
            // Multiplicamos por 180/PI para converter o valor de radianos para graus.
            // O valor 90 é adicionado para ajustar o ângulo para o sistema de coordenadas do jogo.
            x = (float)(Math.Atan2(deltaY, deltaX) * 180 / Math.PI) + 90;

            // Calcula a diferença de altura (coordenada Z) entre o jogador local e o inimigo.
            float deltaZ = inimigo.PosicaoPes.Z - meuPlayer.PosicaoPes.Z;

            // Calcula a distância horizontal total (distância no plano XY) entre o jogador local e o inimigo.
            // Utiliza o teorema de Pitágoras para calcular essa distância: sqrt(deltaX² + deltaY²).
            float dist = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

            // Calcula o ângulo vertical (Y) com base na diferença de altura (Z) e a distância horizontal (XY).
            // Atan2 aqui calcula o ângulo entre a linha vertical (Z) e a distância horizontal.
            y = (float)(Math.Atan2(deltaZ, dist) * 180 / Math.PI);

            // Retorna um vetor 2D contendo os ângulos X (horizontal) e Y (vertical).
            // O ângulo X controla o giro lateral (esquerda/direita), e o ângulo Y controla a elevação (cima/baixo).
            return new Vector2(x, y);
        }


        // Método responsável por colocar a mira na cabeça do player inimigo, ultiliza o retorno dos calculos
        // do metodo "CalcularAngulo"
        private void Mirar(float x, float y)
        {
            // Obtém o ponteiro da memória onde estão armazenados os ângulos de mira do jogador.
            IntPtr PosicaoMira = Game.MeuJogador.EnderecoBase + OffsetsPlayer.Angulos;

            // Escreve os novos valores de ângulo (X e Y) na memória para ajustar a mira.
            Game.M.EscreverFloat(PosicaoMira, x); // Define o ângulo X da mira.
            Game.M.EscreverFloat(PosicaoMira + 0x4, y); // Define o ângulo Y da mira.
        }

        // Método que libera os recursos alocados pelo Aimbot, como o Timer.
        // Também impede que o coletor de lixo finalize o objeto sem liberar recursos adequadamente.
        public void Dispose()
        {
            TimerAimbot.Close(); // Fecha o Timer, liberando seus recursos.
            TimerAimbot = null;
            GC.SuppressFinalize(this); // Suprime a finalização do coletor de lixo, pois já foi feito o cleanup.
        }

        // Destrutor que garante que os recursos sejam liberados caso o Dispose não tenha sido chamado manualmente.
        ~Aimbot() { Dispose(); }
    }

}
