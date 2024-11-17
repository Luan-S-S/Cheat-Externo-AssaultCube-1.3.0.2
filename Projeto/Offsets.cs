using System;

namespace CheatOffsets
{
    // Aqui ficam os offsets de memória. Endereços estáticos do jogo.

    public static class OffsetsEstaticos
    {
        // Endereços estáticos são usados para acessar diferentes partes da memória do jogo.
        // Estes endereços podem precisar ser atualizados se o jogo for atualizado ou modificado.

        public static int
            ViewMatrix = 0x0057DFD0, // Endereço base da ViewMatrix; usado para cálculos de perspectiva e visão do jogador
            LocalPlayer = 0x0058AC00, //Endereço para o Ponteiro da estrutura de dados do jogador local (o próprio jogador)
            EntityList = 0x00591FCC, // Endereço para a lista de entidades (jogadores e NPCs) na partida
            ModoJogo = 0x0058ABF8, // Endereço para o modo de jogo atual: Deathmatch, TeamDeathmatch, etc. (Valores podem indicar diferentes modos)
            QuanPlayers = 0x00591FD4; // Endereço para o número de jogadores atualmente na partida
    }

    public static class OffsetsPlayer
    {
        // Offsets para acessar dados específicos dentro da estrutura do jogador.
        // Esses valores são usados para acessar informações sobre o estado e atributos do jogador.

        public static int
            Cabeca = 0x4,            // Offset para a posição da cabeça do jogador
            Pes = 0x28,              // Offset para a posição dos pés do jogador
            Angulos = 0x34,          // Offset para os ângulos de visão do jogador
            Vida = 0xEC,             // Offset para a quantidade de vida (HP) do jogador
            Nome = 0x205,            // Offset para o nome do jogador
            Time = 0x30C,            // Offset para o time que o jogador esta
            MunicaoPrimaria = 0x130, // Offset para a quantidade de munição da arma primária
            MunicaoPistola = 0x12C,  // Offset para a quantidade de munição da pistola
            BolsaPrimaria = 0x10C,   // Offset para a quantidade de munição na bolsa para a arma primária
            BolsaPistola = 0x108,    // Offset para a quantidade de munição na bolsa para a pistola
            Granada = 0x144,         // Offset para a quantidade de granadas
            Colete = 0xF0;           // Offset para o colete (armadura) do jogador
    }

    public static class OffsetsInstrucoes
    {
        // Endereços das instruções Assembly que controlam o comportamento do jogo.
        // Alterar essas instruções pode modificar a forma como o jogo lida com certos aspectos, como munição e recuo.

        public static int
            DecrementoMunicao = 0x004C73EF, //Endereço da Instrução que controla o decremento da munição primária (cada tiro reduz a munição)
            DecrementoBolsa = 0x004C8FE9, //Endereço da Instrução que controla o decremento da bolsa de munição (pode afetar a munição total disponível)
            RecuoMira = 0x004C2EC3, //Endereço da Instrução que controla o recuo da mira ao atirar (afeta a precisão do tiro)
            VelocidadeTiro = 0x004C721C, //Endereço da Instrução que controla a velocidade do tiro (pode alterar a rapidez dos projéteis)
            RepulsoX = 0x004C8DD2, //Endereço da Instrução que controla o repulso horizontal do jogador ao atirar (efeito de empurro na direção horizontal)
            RepulsoY = 0x004C8DFB; //Endereço da Instrução que controla o repulso vertical do jogador ao atirar (efeito do empurro na direção vertical)
    }
}
