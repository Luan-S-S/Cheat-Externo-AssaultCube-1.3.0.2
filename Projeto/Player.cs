using CheatGame;
using CheatOffsets;
using System.Numerics;

namespace CheatPlayer
{
    public class Player
    {
        // Esta classe representa a estrutura de um player no jogo. Ela encapsula o ponteiro do endereço base do player,
        // permitindo acessar seus atributos somando os offsets de memória ao endereço base.

        // Construtor do Player, que recebe um ponteiro base como parâmetro.
        public Player(nint ponteiroBase) { this.PonteiroBase = ponteiroBase; }

        private IntPtr PonteiroBase;
        // Ponteiro que aponta para o endereço base do player. O valor deste ponteiro é o endereço na memória base
        // que sera o ponto de partida para chegar aos atributos do player.

        public IntPtr EnderecoBase { get { return Game.M.LerPonteiro(PonteiroBase); } }
        // Propriedade que retorna o endereço base do player. Este valor é obtido lendo o ponteiro base
        // e permite calcular os endereços dos atributos do jogador ao somar os offsets.

        // Atributos do jogador, acessados através do endereço base e offsets específicos.

        public int Vida { get { return Game.M.LerInt(SomarEnderecoBase(OffsetsPlayer.Vida)); } }
        // Propriedade que retorna a vida do player. O valor é obtido somando o endereço base com o offset correspondente.

        public int Time { get { return Game.M.LerInt(SomarEnderecoBase(OffsetsPlayer.Time)); } }
        // Propriedade que indica o time ao qual o player pertence: pode ser 0 ou 1.

        public bool EstaVivo { get { return this.Vida > 0 && this.Vida <= 100; } }
        // Propriedade que indica se o player está vivo. Retorna true se a vida do player estiver
        // entre 1 e 100, e false caso contrário.

        public Vector3 PosicaoPes { get { return Game.M.LerVector3(SomarEnderecoBase(OffsetsPlayer.Pes)); } }
        // Propriedade que retorna as posições [X, Y, Z] dos pés do player, lidas da memória

        public Vector3 PosicaoCabeca { get { return Game.M.LerVector3(SomarEnderecoBase(OffsetsPlayer.Cabeca)); } }
        // Propriedade que retorna as posições [X, Y, Z] da cabeça do player, acessadas de forma semelhante
        // às posições dos pés.

        public Vector2 PosicaoMouse { get { return Game.M.LerVector2(SomarEnderecoBase(OffsetsPlayer.Angulos)); } }
        // Propriedade que retorna os ângulos [X, Y] da mira do player, lidas da memória

        // Método que calcula a distância entre este player e outro.
        public float Distancia(Player player)
        {
            // Calcula a distância ao quadrado entre as posições dos pés dos dois players.
            float distPes = Vector3.DistanceSquared(this.PosicaoPes, player.PosicaoPes);

            // Calcula a distância ao quadrado entre as posições das cabeças dos dois players.
            float distCabeca = Vector3.DistanceSquared(this.PosicaoCabeca, player.PosicaoCabeca);

            // Retorna o produto das distâncias ao quadrado
            return distPes * distCabeca;
        }

        // Método que retorna um endereço resultante da soma do endereço base com o offset especificado.
        private IntPtr SomarEnderecoBase(int offset) { return EnderecoBase + offset; }
    }

}
