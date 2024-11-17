using CheatGame;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace MetodosMemoria
{
    public class Memoria : IDisposable
    {
        // Classe responsável por ler e escrever na memória de outro processo (jogo).
        // Implementa IDisposable para garantir o fechamento correto do handle do processo.
        public Memoria(IntPtr hp) { HandleProcesso = hp; } // Construtor que recebe o handle do processo.

        private IntPtr HandleProcesso; // Handle do processo no qual a memória será lida/escrita.
        private bool _liberado; // Flag para evitar liberar recursos mais de uma vez.

        // ReadProcessMemory: Lê dados de um espaço de memória específico de outro processo.
        // WriteProcessMemory: Escreve dados em um espaço de memória específico de outro processo.
        // CloseHandle: Fecha um handle aberto de um objeto do sistema (ex.: processo, thread, etc.)

        [DllImport("Kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hP, IntPtr endereco, byte[] buffer, int tamBuffer, out int quantBytesLidos);

        [DllImport("Kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hP, IntPtr endereco, byte[] buffer, int tamBuffer, out int quantBytesEscritos);

        [DllImport("Kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hProcess);

        // Métodos de leitura de memória

        public int LerInt(IntPtr endereco) { return BitConverter.ToInt32(PivoLer(endereco)); }  
        // Lê um valor inteiro a partir de um endereço de memória.

        public float LerFloat(IntPtr endereco) { return BitConverter.ToSingle(PivoLer(endereco)); } 
        // Lê um valor float a partir de um endereço de memória.

        public IntPtr LerPonteiro(IntPtr endereco) { return new IntPtr(LerInt(endereco)); } 
        // Lê um ponteiro a partir de um endereço de memória, interpretando como int.

        public string LerString(IntPtr endereco) { return Encoding.UTF8.GetString(PivoLer(endereco, 10)); }
        // Lê uma string (UTF8) a partir de um endereço de memória.


        // Lê um Vector2 (coordenadas X e Y) a partir de um endereço de memória.
        public Vector2 LerVector2(IntPtr endereco) 
        {
            Vector2 vector2 = new Vector2();

            vector2.X = LerFloat(endereco);
            vector2.Y = LerFloat(endereco + 0x4);

            return vector2;
        }

        // Lê um Vector3 (coordenadas X, Y e Z) a partir de um endereço de memória.
        public Vector3 LerVector3(IntPtr endereco)
        {
            Vector3 vector3 = new Vector3();

            vector3.X = LerFloat(endereco);
            vector3.Y = LerFloat(endereco + 0x4);
            vector3.Z = LerFloat(endereco + 0x8);

            return vector3;
        }

        // Lê uma matriz 4x4 (16 valores float) a partir de um endereço de memória. Usado para ler a ViewMatrix
        public float[] LerMatrix(IntPtr endereco) 
        {
            float[] matrix = new float[16]; // Buffer para armazenar a matriz.
            int y = 0;

            // Lê 16 floats consecutivos, formando uma matriz 4x4.
            for (int i = 0; i < 16; i++)
            {
                matrix[i] = LerFloat(endereco + 0x0 + y);
                y += 4; //Incremente 4 no offset para o endereço seguinte
            }
            return matrix;
        }

        // Métodos de escrita de memória
        public void EscreverInt(IntPtr endereco, int valor) { PivoEscrever(endereco, valor); } 
        // Escreve um valor inteiro em um endereço de memória.

        public void EscreverFloat(IntPtr endereco, float valor) { PivoEscrever(endereco, valor); }
        // Escreve um valor float em um endereço de memória.

        // Pivos que encapsulam ReadProcessMemory e WriteProcessMemory
        private byte[] PivoLer(IntPtr endereco, int tamBuffer = 4)
        {
            // Pivô Método auxiliar para a leitura de memória encapsulando ReadProcessMemory.
            // Retorna um array de bytes lidos da memória do processo alvo.

            byte[] buffer = new byte[tamBuffer]; // Buffer para armazenar os dados lidos.

            // Realiza a leitura na memória do processo.
            bool sucesso = ReadProcessMemory(HandleProcesso, endereco, buffer, tamBuffer, out int quantBytesLidos);

            // Verifica se a leitura foi bem-sucedida, caso contrário encerra o aplicativo.
            if (!sucesso)
            {
                Game.EncerrarAplicativo("Erro ao Ler Memoria!");
            }
            return buffer; // Retorna os dados lidos.

        }

        private void PivoEscrever(IntPtr endereco, object valor)
        {
            // Pivô Método auxiliar para a escrita de memória encapsulando WriteProcessMemory.
            // Escreve um valor (int ou float) na memória do processo alvo.

            // Lança exceção se o tipo de valor for diferente de int e float.
            if (!(valor is int) && !(valor is float)) throw new ArgumentException("Tipo de valor inválido");

            byte[] buffer; // Buffer para armazenar os dados a serem escritos.

            // atribui um array de bytes a buffer de acordo com o tipo (int ou float)
            buffer = valor is float ? BitConverter.GetBytes((float)valor) : BitConverter.GetBytes((int)valor);

            // Realiza a escrita na memória do processo.
            bool sucesso = WriteProcessMemory(HandleProcesso, endereco, buffer, buffer.Length, out int quanBytesEscritos);

            // Verifica se a escrita foi bem-sucedida, caso contrário encerra o aplicativo.
            if (!sucesso)
            {
                Game.EncerrarAplicativo("Erro ao Escrever Memoria!");
            }
        }

        public void Dispose() // Libera recursos não gerenciados ao encerrar o uso da memória.
        {
            
            if(!_liberado && HandleProcesso != IntPtr.Zero)
            {
                CloseHandle(HandleProcesso); // Fecha o handle do processo.
                HandleProcesso = IntPtr.Zero;
                _liberado = true;
                GC.SuppressFinalize(this);
            }
        }
        ~Memoria() { Dispose(); } // Finalizador que garante a liberação dos recursos.
    }


}
