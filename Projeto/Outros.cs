using CheatGame;
using CheatOffsets;
using System.Timers;
using Timer = System.Timers.Timer;


namespace CheatOutros
{
    public static class Modificador
    {
        // Classe responsável por manipular diretamente os valores dos atributos do jogador na memória do jogo.
        // Utiliza ponteiros e offsets para acessar e modificar atributos como vida, colete, munição, entre outros,
        // permitindo que esses valores sejam alterados em tempo real durante a execução do jogo.

        private static IntPtr PonteiroBase { get { return Game.MeuJogador.EnderecoBase; } }// Ponteiro Base do Meu Player
        // Propriedade que retorna o endereço base do jogador no jogo.
        // Este endereço é utilizado como ponto de partida para calcular os endereços de memória dos atributos.


        // Dicionário que mapeia os atributos do jogador (como Vida, Colete, Munição) aos seus endereços de memória.
        // Cada entrada do dicionário possui uma chave (o nome do atributo) e um valor (o endereço de memória relativo ao EnderecoBase).
        private static Dictionary<string, IntPtr> Enderecos = new Dictionary<string, IntPtr>()
        {
            // A chave é o nome do atributo, e o valor é o endereço de memória correspondente, calculado
            // a partir do endereço base do jogador (EnderecoBase) somado aos offsets específicos para cada atributo.
            {"Vida", PonteiroBase + OffsetsPlayer.Vida},
            {"Colete", PonteiroBase + OffsetsPlayer.Colete},
            {"Munição Arma Primaria", PonteiroBase + OffsetsPlayer.MunicaoPrimaria},
            {"Bolsa Arma Primaria", PonteiroBase + OffsetsPlayer.BolsaPrimaria},
            {"Munição Pistola", PonteiroBase + OffsetsPlayer.MunicaoPistola},
            {"Bolsa Pistola", PonteiroBase + OffsetsPlayer.BolsaPistola},
            {"Granada", PonteiroBase + OffsetsPlayer.Granada}
        };


        // Método principal para modificar o valor de um atributo específico do jogador.
        // O parâmetro 'chave' é o nome do atributo que será modificado, e 'valor' é o novo valor que será escrito na memória.
        public static void Modificar(string chave, int valor)
        {
            IntPtr endereco = Enderecos[chave]; // Captura o endereço de memória associado ao atributo fornecido pela chave.

            // Verifica se o atributo sendo modificado é "Munição Arma Primária" ou "Bolsa Arma Primária".
            // Esses atributos possuem múltiplos endereços sequenciais na memória, então usamos um método especial para modificá-los.
            if (chave == "Munição Arma Primaria" || chave == "Bolsa Arma Primaria")
            {
                // Chama o método ModificarPonteiros para modificar todos os endereços sequenciais (5 no total).
                ModificarPonteiros(endereco, valor);
                return;
            }

            // Se não for um atributo com multiplos endereços (como munição ou bolsa), simplesmente escreve o valor no endereço de memória.
            Game.M.EscreverInt(endereco, valor);
        }

        // Método responsável por modificar múltiplos endereços de memória sequenciais.
        // Esse método é utilizado para atributos como munição ou bolsa que possuem 5 endereços de memória seguidos.
        private static void ModificarPonteiros(IntPtr endereco, int valor)
        {

            // Itera por 5 endereços de memória sequenciais, começando pelo endereço fornecido.
            // Cada endereço contém um valor que será modificado.
            for (int i = 0; i < 5; i++)
            {
                Game.M.EscreverInt(endereco, valor); // Escreve o valor fornecido no endereço atual.

                // Incrementa o endereço para o próximo valor na sequência.
                // O incremento é de 0x4 (4 bytes), que é o tamanho típico de um valor inteiro (int) na memória.
                endereco += 0x4;
            }
        }
    }

    public static class Cheats
    {
        // Classe responsável por modificar diretamente as instruções de memória do jogo.
        // Utiliza ponteiros e valores modificados para alterar o comportamento das instruções em tempo real,
        // permitindo a ativação de cheats como vida infinita, munição infinita, tiro rápido, etc.


        // Estrutura que encapsula uma instrução de memória em Assembly.
        // Armazena o endereço da instrução, seu valor original e o valor modificado.
        // O valor modificado será usado para ativar o cheat, enquanto o valor original restaura o comportamento padrão.
        private struct InstrucoesMemoria
        {
            public InstrucoesMemoria(IntPtr endereco, int valorOriginal, int valorModificado)
            {
                this.Endereco = endereco; // Endereço de memória da instrução
                this.ValorOriginal = valorOriginal; // Valor original da instrução (comportamento padrão)
                this.ValorModificado = valorModificado; // Valor modificado da instrução (cheat ativado)
            }

            public IntPtr Endereco; // Endereço de memória onde a instrução está armazenada
            public int ValorOriginal; // Valor padrão da instrução (desativado)
            public int ValorModificado; // Valor alterado para ativar o cheat
        }

        private static Timer TimerVidaInfinita = new Timer(100);
        // Timer que controla a atualização periódica da vida do jogador (para o cheat de vida infinita).
        // A cada 100 milissegundos, o valor da vida do jogador é redefinido para 9999.

        private static bool _timerVidaInfinitaAtivado { get { return TimerVidaInfinita.Enabled; } }
        // Propriedade booleana que verifica se o TimerVidaInfinita está ativado ou não.

        private static IntPtr pVida;
        // Ponteiro que armazena o endereço de memória da vida do jogador.
        // Esse endereço é usado para modificar diretamente o valor da vida em tempo real.

        // ___Instruções de memória específicas para cada cheat, encapsuladas na estrutura InstrucoesMemoria.

        private static InstrucoesMemoria DecrementoMunicao = new InstrucoesMemoria(OffsetsInstrucoes.DecrementoMunicao, 1150093567, 1150128272);
        // Cheat que controla o decremento da munição ao atirar.
        // Quando o cheat está ativado, o valor modificado impede que a munição diminua.

        private static InstrucoesMemoria DecrementoBolsa = new InstrucoesMemoria(OffsetsInstrucoes.DecrementoBolsa, 1200304169, 1200328848);
        // Cheat que controla o decremento da bolsa de munição (número total de munições disponíveis).
        // O valor modificado impede que a quantidade de munição na bolsa diminua.

        private static InstrucoesMemoria RecuoMira = new InstrucoesMemoria(OffsetsInstrucoes.RecuoMira, 1443958771, 1443958528);
        // Cheat que remove o recuo da mira ao atirar, mantendo a mira estável e estatica.

        private static InstrucoesMemoria VelocidadeTiro = new InstrucoesMemoria(OffsetsInstrucoes.VelocidadeTiro, 1115163179, 1115197584);
        // Cheat que aumenta a velocidade dos tiros do jogador, tornando-os mais rápidos.

        private static (InstrucoesMemoria X, InstrucoesMemoria Y) Repulso = (new InstrucoesMemoria(OffsetsInstrucoes.RepulsoX, 1074860019, 1079644147), new InstrucoesMemoria(OffsetsInstrucoes.RepulsoY, 1074860019, 1079644147));
        // Cheat que remove o efeito de repulso (recuo) do jogador ao atirar, tanto na direção X quanto Y.
        // A repulso horizontal e vertical são controladas por dois endereços de memória diferentes.

        // ___Propriedades que ativam ou desativam cada cheat individualmente, modificando as instruções de memória.

        public static bool BalaInfinita { set { Ativar_Desativar(DecrementoMunicao, value); } }
        // Propriedade que controla o cheat de munição infinita. Quando ativada, o jogador não perde munição ao atirar.

        public static bool BolsaInfinita { set { Ativar_Desativar(DecrementoBolsa, value); } }
        // Propriedade que controla o cheat de bolsa de munição infinita. Quando ativada, o jogador não perde munição total.

        public static bool SemRecuo { set { Ativar_Desativar(RecuoMira, value); } }
        // Propriedade que controla o cheat de recuo nulo. Quando ativada, a mira do jogador não recua ao atirar.

        public static bool TiroRapido { set { Ativar_Desativar(VelocidadeTiro, value); } }
        // Propriedade que controla o cheat de tiro rápido. Quando ativada, os tiros do jogador são mais rápidos.


        // Propriedade que controla o cheat de repulso nulo. Quando ativada, o jogador não é empurrado ao atirar.
        // Ativa ou desativa os efeitos de repulso tanto na direção X quanto Y.
        public static bool SemRepulso
        {
            set
            {
                Ativar_Desativar(Repulso.X, value); // Remove repulso horizontal
                Ativar_Desativar(Repulso.Y, value); // Remove repulso vertical
            }
        }

        // Propriedade que controla o cheat de vida infinita. Quando ativada, a vida do jogador é constantemente redefinida para 9999.
        public static bool VidaInfinita
        {
            set
            {
                // Se o cheat for ativado e o Timer não estiver rodando, inicia o processo de atualização da vida.
                if (value && (!_timerVidaInfinitaAtivado))
                {
                    pVida = Game.MeuJogador.EnderecoBase + OffsetsPlayer.Vida; // Obtém o ponteiro para o endereço da vida do jogador
                    TimerVidaInfinita.Elapsed += MetodoVidaInfinita;
                    TimerVidaInfinita.Start(); // Inicia o Timer para atualizar a vida periodicamente
                    return;
                }
                // Caso contrário, pausa o Timer e desativa o cheat de vida infinita.
                PausarTimerVidaInfinita();
            }
        }

        // Método auxiliar que ativa ou desativa uma instrução de memória com base no valor booleano fornecido.
        // Se ativado, o valor modificado é escrito na memória; se desativado, o valor original é restaurado.
        private static void Ativar_Desativar(InstrucoesMemoria instrucao, bool ativar)
        {
            // Seta o valor a ser modificado de acordo com o parametro "atrivar.
            int valor = ativar ? instrucao.ValorModificado : instrucao.ValorOriginal;

            Game.M.EscreverInt(instrucao.Endereco, valor);
            // Modifica o valor de memoria
            // if(ativar) valor = valorModificado; ativa o cheat
            // else valor = valorOriginal; desativa o cheat
        }

        // Método que é executado periodicamente pelo TimerVidaInfinita.
        // Ele garante que a vida do jogador não fique abaixo de 9999, atualizando o valor na memória.
        private static void MetodoVidaInfinita(object? sender, ElapsedEventArgs e)
        {
            int vida = Game.M.LerInt(pVida); // Lê o valor atual da vida do jogador na memória
            if (vida < 9999) // Se a vida estiver abaixo de 9999, redefine para 9999
            {
                Game.M.EscreverInt(pVida, 9999);
            }
        }

        // Método que pausa o TimerVidaInfinita e redefine a vida do jogador para 100 (valor padrão).
        private static void PausarTimerVidaInfinita()
        {
            if (_timerVidaInfinitaAtivado) // Verifica se o Timer está ativo
            {
                TimerVidaInfinita.Elapsed -= MetodoVidaInfinita; // Desvincula o método do evento Elapsed
                TimerVidaInfinita.Stop(); // Para o Timer
                Game.M.EscreverInt(pVida, 100); // Redefine a vida para 100
            }
        }

        // Método que libera os recursos utilizados pelo TimerVidaInfinita, garantindo que ele seja fechado corretamente.
        private static void LiberarTimerVidaInfinita()
        {
            PausarTimerVidaInfinita(); // Pausa o Timer e redefine a vida para 100
            TimerVidaInfinita.Close(); // Fecha o Timer, liberando seus recursos
            TimerVidaInfinita = null; // Define o Timer como nulo
        }

        // Método para desativar todos os cheats e restaurar as instruções de memória aos seus valores originais.
        // Também libera os recursos utilizados pelo Timer.
        public static void Dispose()
        {
            BalaInfinita = false; // Desativa Bala Infinita
            BolsaInfinita = false; // Desativa Bolsa Infinita
            SemRecuo = false; // Desativa o controle de Recuo
            TiroRapido = false; // Desativa o Tiro rapido
            SemRepulso = false; // Desativa o controle de repulso
            LiberarTimerVidaInfinita(); // Desativa e libera o timer Vida Infinita
        }
    }
}
