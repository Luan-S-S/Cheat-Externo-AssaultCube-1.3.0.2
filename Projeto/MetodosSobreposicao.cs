using System.Runtime.InteropServices;

namespace MetodosSobreposicao
{
    public class Sobreposicao
    {
        // Esta classe é responsável por sobrepor um formulário (Form) nà janela do jogo
        // Isso é necessario para fazer com que os desenhos do ESP fiquem visíveis na janela do jogo

        // Construtor: inicializa a classe com o nome da janela do processo do jogo
        // A classe usa esse nome para encontrar a janela do jogo e obter o identificador (handle)
        public Sobreposicao(string nomeJanelaJogo)
        {
            this.NomeJanelaJogo = nomeJanelaJogo;
            this.HandleJanelaJogo = FindWindow(null, NomeJanelaJogo); // Obtém o handle da janela do jogo usando seu nome
        }

        private Form Formulario; // O formulário que será sobreposto à janela do jogo
        private IntPtr HandleJanelaJogo; // Handle da janela do jogo (identificador da janela)
        private string NomeJanelaJogo; // Nome da janela do jogo
        private RECT Rect; // Estrutura que armazena as dimensões da janela do jogo (Left, Top, Right, Bottom)

        // Métodos da biblioteca "User32.dll"
        // FindWindow: Busca uma janela pelo seu nome e retorna um identificador (handle) para ela
        // SetWindowLong: Modifica um atributo específico da janela (estilo, comportamento, etc.)
        // GetWindowLong: Recupera um valor de atributo específico da janela
        // GetWindowRect: Obtém as coordenadas (dimensões) da janela em relação à tela

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hwnd, ref RECT IpRect);

        // Estrutura usada pelo método GetWindowRect para armazenar as dimensões da janela do jogo
        // Contém as propriedades Left, Top, Right, e Bottom, que definem a posição e o tamanho da janela

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left, Top, Right, Bottom; // Dimensões da janela do jogo

            // Sobrecarga do operador "==" para verificar se dois objetos RECT são iguais
            // Dois RECTs serão iguais se todas suas propriedades forem idênticas
            public static bool operator ==(RECT a, RECT b)
            {
                return a.Top == b.Top && a.Bottom == b.Bottom && a.Left == b.Left && a.Right == b.Right;
            }

            // Sobrecarga do operador "!=" para verificar se dois objetos RECT são diferentes
            public static bool operator !=(RECT a, RECT b) { return !(a == b); }
        }

        // Método principal para sobrepor o formulário na janela do jogo
        // Recebe como parâmetro o formulário que será sobreposto à janela do jogo
        public void Sobrepor(Form formularioSobreposto)
        {
            Formulario = formularioSobreposto; // Atribui o formulário que será sobreposto

            ObterRect(ref Rect); // Obtém as dimensões da janela do jogo e as armazenam na variável "Rect"
            FicarInvisivel(); // Torna o formulário invisível e intangível (não responde a cliques), para que só os gráficos desenhados sejam visíveis
            AjustarSobreposicao(); // Ajusta o formulário para ficar exatamente sobre a janela do jogo
            Formulario.Show(); // Exibe o formulário sobreposto à janela do jogo
        }

        // Método que reajusta a posição e o tamanho do formulário se a janela do jogo for redimensionada
        public bool ReajustarSobreposicao()
        {
            if (RectMudou()) // Verifica se as dimensões da janela do jogo mudaram
            {
                Thread.Sleep(100); // Aguarda 100 milissegundos para dar tempo do redimensionamento ocorrer completamente
                AjustarSobreposicao(); // Reajusta o formulário para as novas dimensões da janela do jogo
                return true; // Retorna verdadeiro se houve mudança
            }
            return false; // Retorna falso se não houve redimensionamento
        }

        // Método que libera os recursos associados ao formulário sobreposto
        public void LiberarSobreposicao()
        {
            if (Formulario != null)
            {
                Formulario.Close(); // Fecha o formulário
                Formulario.Dispose(); // Libera os recursos usados pelo formulário
                Formulario = null; // Remove a referência ao formulário
            }
        }

        // Método que ajusta as dimensões do formulário para corresponderem às dimensões da janela do jogo
        private void AjustarSobreposicao()
        {
            Formulario.Size = CalcSize(); // Calcula e ajusta o tamanho do formulário para o tamanho da janela do jogo
            AjustarFormulario(); // Ajusta a posição e o tamanho do formulário considerando o DPI do monitor
        }

        // Método que encapsula GetWindowRect para obter as dimensões da janela do jogo
        // e armazena essas dimensões na variável rect passada por referencia
        private void ObterRect(ref RECT rect) { GetWindowRect(HandleJanelaJogo, ref rect); }

        // Método para tornar o formulário invisível e intangível (não interage com cliques)
        private void FicarInvisivel()
        {
            Formulario.ShowInTaskbar = false; // Remove o formulário da barra de tarefas
            Formulario.BackColor = Color.Wheat; // Define a cor de fundo como "Wheat"
            Formulario.TransparencyKey = Color.Wheat; // Define a cor transparente, tornando o fundo invisível
            Formulario.TopMost = true; // Garante que o formulário fique sempre acima das outras janelas
            Formulario.FormBorderStyle = FormBorderStyle.None; // Remove a borda do formulário
            InvalidarClique(); // Faz com que o formulário não responda a eventos de clique
        }

        // Método que invalida eventos de clique no formulário, tornando-o intangível
        private void InvalidarClique()
        {
            IntPtr handleFormulario = Formulario.Handle; // Obtém o handle do formulário

            int initialStyle = GetWindowLong(handleFormulario, -20); // Recupera o estilo atual da janela
            SetWindowLong(handleFormulario, -20, initialStyle | 0x8000 | 0x20); // Modifica o estilo para impedir eventos de clique
        }

        // Método que calcula e retorna o tamanho (largura e altura) de um formulário com base nas dimensões da janela do jogo
        private Size CalcSize() { return new Size(Rect.Right - Rect.Left, Rect.Bottom - Rect.Top); }

        // Método que ajusta o formulário precisamente sobre a janela do jogo, levando em conta a escala de DPI do monitor
        private void AjustarFormulario()
        {
            float escala = ObterDPI() / 100f; // Obtém a escala de DPI do monitor (em percentual)

            // Ajusta as posições (Top, Left) e dimensões (Width, Height) do formulário
            // de acordo com a janela do jogo, levando em conta a escala de DPI
            Formulario.Top = Rect.Top + (int)(32 * escala);
            Formulario.Left = Rect.Left + (int)(8 * escala);
            Formulario.Width -= (int)(16 * escala);
            Formulario.Height -= (int)(40 * escala);
        }

        // Método que verifica se houve mudança nas dimensões da janela do jogo
        private bool RectMudou()
        {
            RECT rectAnterior = Rect; // Armazena o valor anterior de Rect
            ObterRect(ref Rect); // Atualiza Rect com as novas dimensões da janela do jogo

            // Retorna true se as dimensões forem diferentes (a janela foi redimensionada)
            return rectAnterior != Rect;
        }

        // Método que obtém o DPI (pontos por polegada) atual do monitor do usuário
        private int ObterDPI()
        {
            using (Graphics g = Formulario.CreateGraphics()) // Cria um objeto Graphics para obter informações de DPI
            {
                float dpiX = g.DpiX; // Obtém o DPI horizontal
                float dpiY = g.DpiY; // Obtém o DPI vertical

                // Calcula a escala de DPI em relação ao DPI padrão (96)
                float scaleX = dpiX / 96f;
                float scaleY = dpiY / 96f;

                // Converte a escala para percentual
                int scalePercentX = (int)(scaleX * 100);
                int scalePercentY = (int)(scaleY * 100);

                // Calcula a média entre as escalas horizontal e vertical
                int dpi = (scalePercentX + scalePercentY) / 2;

                return dpi; // Retorna o DPI calculado
            };
        }
    }
}
