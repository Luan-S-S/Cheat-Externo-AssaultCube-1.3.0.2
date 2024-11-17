namespace Cheat_Externo_AssaultCube_1._3._0._2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            groupBoxESP = new GroupBox();
            checkBoxDesenhar_Linha = new CheckBox();
            CheckBoxESP = new CheckBox();
            groupBoxAimbot = new GroupBox();
            checkBoxAimbot = new CheckBox();
            groupBoxModificarValores = new GroupBox();
            buttonAplicar = new Button();
            textBoxValorItem = new TextBox();
            comboBoxSelecaoValor = new ComboBox();
            groupBoxCheats = new GroupBox();
            checkBoxSemRepulso = new CheckBox();
            checkBoxTiroRapido = new CheckBox();
            checkBoxSemRecuo = new CheckBox();
            checkBoxVidaInfinita = new CheckBox();
            checkBoxBolsaInfinita = new CheckBox();
            checkBoxBalaInfinita = new CheckBox();
            AjudaTextBox = new ToolTip(components);
            groupBoxESP.SuspendLayout();
            groupBoxAimbot.SuspendLayout();
            groupBoxModificarValores.SuspendLayout();
            groupBoxCheats.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxESP
            // 
            groupBoxESP.BackColor = Color.DimGray;
            groupBoxESP.Controls.Add(checkBoxDesenhar_Linha);
            groupBoxESP.Controls.Add(CheckBoxESP);
            groupBoxESP.ForeColor = Color.White;
            groupBoxESP.Location = new Point(10, 9);
            groupBoxESP.Name = "groupBoxESP";
            groupBoxESP.Size = new Size(174, 82);
            groupBoxESP.TabIndex = 0;
            groupBoxESP.TabStop = false;
            groupBoxESP.Text = "Percepção Extra-Sensorial";
            // 
            // checkBoxDesenhar_Linha
            // 
            checkBoxDesenhar_Linha.AutoSize = true;
            checkBoxDesenhar_Linha.BackColor = Color.DarkRed;
            checkBoxDesenhar_Linha.Cursor = Cursors.Hand;
            checkBoxDesenhar_Linha.ForeColor = Color.Black;
            checkBoxDesenhar_Linha.Location = new Point(14, 50);
            checkBoxDesenhar_Linha.Name = "checkBoxDesenhar_Linha";
            checkBoxDesenhar_Linha.Size = new Size(107, 19);
            checkBoxDesenhar_Linha.TabIndex = 1;
            checkBoxDesenhar_Linha.Text = "Desenhar Linha";
            checkBoxDesenhar_Linha.UseVisualStyleBackColor = false;
            checkBoxDesenhar_Linha.CheckedChanged += checkBoxDesenhar_Linha_CheckedChanged;
            // 
            // CheckBoxESP
            // 
            CheckBoxESP.AutoSize = true;
            CheckBoxESP.BackColor = Color.DarkRed;
            CheckBoxESP.Cursor = Cursors.Hand;
            CheckBoxESP.ForeColor = Color.Black;
            CheckBoxESP.Location = new Point(14, 28);
            CheckBoxESP.Name = "CheckBoxESP";
            CheckBoxESP.Size = new Size(45, 19);
            CheckBoxESP.TabIndex = 0;
            CheckBoxESP.Text = "ESP";
            CheckBoxESP.UseVisualStyleBackColor = false;
            CheckBoxESP.CheckedChanged += CheckBoxESP_CheckedChanged;
            // 
            // groupBoxAimbot
            // 
            groupBoxAimbot.BackColor = Color.DimGray;
            groupBoxAimbot.Controls.Add(checkBoxAimbot);
            groupBoxAimbot.ForeColor = Color.White;
            groupBoxAimbot.Location = new Point(186, 9);
            groupBoxAimbot.Name = "groupBoxAimbot";
            groupBoxAimbot.Size = new Size(182, 82);
            groupBoxAimbot.TabIndex = 1;
            groupBoxAimbot.TabStop = false;
            groupBoxAimbot.Text = "Mira Altomatica";
            // 
            // checkBoxAimbot
            // 
            checkBoxAimbot.AutoSize = true;
            checkBoxAimbot.BackColor = Color.DarkRed;
            checkBoxAimbot.Cursor = Cursors.Hand;
            checkBoxAimbot.ForeColor = Color.Black;
            checkBoxAimbot.Location = new Point(55, 35);
            checkBoxAimbot.Name = "checkBoxAimbot";
            checkBoxAimbot.Size = new Size(66, 19);
            checkBoxAimbot.TabIndex = 0;
            checkBoxAimbot.Text = "Aimbot";
            checkBoxAimbot.UseVisualStyleBackColor = false;
            checkBoxAimbot.CheckedChanged += checkBoxAimbot_CheckedChanged;
            // 
            // groupBoxModificarValores
            // 
            groupBoxModificarValores.BackColor = Color.DimGray;
            groupBoxModificarValores.Controls.Add(buttonAplicar);
            groupBoxModificarValores.Controls.Add(textBoxValorItem);
            groupBoxModificarValores.Controls.Add(comboBoxSelecaoValor);
            groupBoxModificarValores.ForeColor = Color.White;
            groupBoxModificarValores.Location = new Point(10, 97);
            groupBoxModificarValores.Name = "groupBoxModificarValores";
            groupBoxModificarValores.Size = new Size(359, 66);
            groupBoxModificarValores.TabIndex = 2;
            groupBoxModificarValores.TabStop = false;
            groupBoxModificarValores.Text = "Modificar Valores";
            // 
            // buttonAplicar
            // 
            buttonAplicar.BackColor = Color.DarkRed;
            buttonAplicar.Cursor = Cursors.Hand;
            buttonAplicar.ForeColor = Color.Black;
            buttonAplicar.Location = new Point(292, 22);
            buttonAplicar.Name = "buttonAplicar";
            buttonAplicar.Size = new Size(59, 23);
            buttonAplicar.TabIndex = 2;
            buttonAplicar.Text = "Aplicar";
            buttonAplicar.UseVisualStyleBackColor = false;
            buttonAplicar.Click += buttonAplicar_Click;
            // 
            // textBoxValorItem
            // 
            textBoxValorItem.Location = new Point(153, 22);
            textBoxValorItem.Name = "textBoxValorItem";
            textBoxValorItem.Size = new Size(133, 23);
            textBoxValorItem.TabIndex = 1;
            AjudaTextBox.SetToolTip(textBoxValorItem, "Digite um Valor\nPara o Item Selecionado");
            textBoxValorItem.KeyPress += textBoxValorItem_KeyPress;
            // 
            // comboBoxSelecaoValor
            // 
            comboBoxSelecaoValor.Cursor = Cursors.Hand;
            comboBoxSelecaoValor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSelecaoValor.FormattingEnabled = true;
            comboBoxSelecaoValor.Items.AddRange(new object[] { "Vida", "Colete", "Munição Arma Primaria", "Munição Pistola", "Bolsa Arma Primaria", "Bolsa Pistola", "Granada" });
            comboBoxSelecaoValor.Location = new Point(6, 22);
            comboBoxSelecaoValor.Name = "comboBoxSelecaoValor";
            comboBoxSelecaoValor.Size = new Size(141, 23);
            comboBoxSelecaoValor.TabIndex = 0;
            comboBoxSelecaoValor.DropDown += comboBoxSelecaoValor_DropDown;
            comboBoxSelecaoValor.DropDownClosed += comboBoxSelecaoValor_DropDownClosed;
            // 
            // groupBoxCheats
            // 
            groupBoxCheats.BackColor = Color.DimGray;
            groupBoxCheats.Controls.Add(checkBoxSemRepulso);
            groupBoxCheats.Controls.Add(checkBoxTiroRapido);
            groupBoxCheats.Controls.Add(checkBoxSemRecuo);
            groupBoxCheats.Controls.Add(checkBoxVidaInfinita);
            groupBoxCheats.Controls.Add(checkBoxBolsaInfinita);
            groupBoxCheats.Controls.Add(checkBoxBalaInfinita);
            groupBoxCheats.ForeColor = Color.White;
            groupBoxCheats.Location = new Point(10, 169);
            groupBoxCheats.Name = "groupBoxCheats";
            groupBoxCheats.Size = new Size(359, 87);
            groupBoxCheats.TabIndex = 3;
            groupBoxCheats.TabStop = false;
            groupBoxCheats.Text = "Cheats";
            // 
            // checkBoxSemRepulso
            // 
            checkBoxSemRepulso.AutoSize = true;
            checkBoxSemRepulso.BackColor = Color.DarkRed;
            checkBoxSemRepulso.Cursor = Cursors.Hand;
            checkBoxSemRepulso.ForeColor = Color.Black;
            checkBoxSemRepulso.Location = new Point(248, 54);
            checkBoxSemRepulso.Name = "checkBoxSemRepulso";
            checkBoxSemRepulso.Size = new Size(94, 19);
            checkBoxSemRepulso.TabIndex = 5;
            checkBoxSemRepulso.Text = "Sem Repulso";
            checkBoxSemRepulso.UseVisualStyleBackColor = false;
            checkBoxSemRepulso.CheckedChanged += checkBoxSemRepulso_CheckedChanged;
            // 
            // checkBoxTiroRapido
            // 
            checkBoxTiroRapido.AutoSize = true;
            checkBoxTiroRapido.BackColor = Color.DarkRed;
            checkBoxTiroRapido.Cursor = Cursors.Hand;
            checkBoxTiroRapido.ForeColor = Color.Black;
            checkBoxTiroRapido.Location = new Point(129, 54);
            checkBoxTiroRapido.Margin = new Padding(3, 2, 3, 2);
            checkBoxTiroRapido.Name = "checkBoxTiroRapido";
            checkBoxTiroRapido.Size = new Size(86, 19);
            checkBoxTiroRapido.TabIndex = 4;
            checkBoxTiroRapido.Text = "Tiro Rapido";
            checkBoxTiroRapido.UseVisualStyleBackColor = false;
            checkBoxTiroRapido.CheckedChanged += checkBoxTiroRapido_CheckedChanged;
            // 
            // checkBoxSemRecuo
            // 
            checkBoxSemRecuo.AutoSize = true;
            checkBoxSemRecuo.BackColor = Color.DarkRed;
            checkBoxSemRecuo.Cursor = Cursors.Hand;
            checkBoxSemRecuo.ForeColor = Color.Black;
            checkBoxSemRecuo.Location = new Point(10, 54);
            checkBoxSemRecuo.Margin = new Padding(3, 2, 3, 2);
            checkBoxSemRecuo.Name = "checkBoxSemRecuo";
            checkBoxSemRecuo.Size = new Size(85, 19);
            checkBoxSemRecuo.TabIndex = 3;
            checkBoxSemRecuo.Text = "Sem Recuo";
            checkBoxSemRecuo.UseVisualStyleBackColor = false;
            checkBoxSemRecuo.CheckedChanged += checkBoxSemRecuo_CheckedChanged;
            // 
            // checkBoxVidaInfinita
            // 
            checkBoxVidaInfinita.AutoSize = true;
            checkBoxVidaInfinita.BackColor = Color.DarkRed;
            checkBoxVidaInfinita.Cursor = Cursors.Hand;
            checkBoxVidaInfinita.ForeColor = Color.Black;
            checkBoxVidaInfinita.Location = new Point(10, 20);
            checkBoxVidaInfinita.Margin = new Padding(3, 2, 3, 2);
            checkBoxVidaInfinita.Name = "checkBoxVidaInfinita";
            checkBoxVidaInfinita.Size = new Size(89, 19);
            checkBoxVidaInfinita.TabIndex = 2;
            checkBoxVidaInfinita.Text = "Vida Infinita";
            checkBoxVidaInfinita.UseVisualStyleBackColor = false;
            checkBoxVidaInfinita.CheckedChanged += checkBoxVidaInfinita_CheckedChanged;
            // 
            // checkBoxBolsaInfinita
            // 
            checkBoxBolsaInfinita.AutoSize = true;
            checkBoxBolsaInfinita.BackColor = Color.DarkRed;
            checkBoxBolsaInfinita.Cursor = Cursors.Hand;
            checkBoxBolsaInfinita.ForeColor = Color.Black;
            checkBoxBolsaInfinita.Location = new Point(248, 20);
            checkBoxBolsaInfinita.Margin = new Padding(3, 2, 3, 2);
            checkBoxBolsaInfinita.Name = "checkBoxBolsaInfinita";
            checkBoxBolsaInfinita.Size = new Size(94, 19);
            checkBoxBolsaInfinita.TabIndex = 1;
            checkBoxBolsaInfinita.Text = "Bolsa Infinita";
            checkBoxBolsaInfinita.UseVisualStyleBackColor = false;
            checkBoxBolsaInfinita.CheckedChanged += checkBoxBolsaInfinita_CheckedChanged;
            // 
            // checkBoxBalaInfinita
            // 
            checkBoxBalaInfinita.AutoSize = true;
            checkBoxBalaInfinita.BackColor = Color.DarkRed;
            checkBoxBalaInfinita.Cursor = Cursors.Hand;
            checkBoxBalaInfinita.ForeColor = Color.Black;
            checkBoxBalaInfinita.Location = new Point(118, 20);
            checkBoxBalaInfinita.Margin = new Padding(3, 2, 3, 2);
            checkBoxBalaInfinita.Name = "checkBoxBalaInfinita";
            checkBoxBalaInfinita.Size = new Size(113, 19);
            checkBoxBalaInfinita.TabIndex = 0;
            checkBoxBalaInfinita.Text = "Munição Infinita";
            checkBoxBalaInfinita.UseVisualStyleBackColor = false;
            checkBoxBalaInfinita.CheckedChanged += checkBoxBalaInfinita_CheckedChanged;
            // 
            // AjudaTextBox
            // 
            AjudaTextBox.AutomaticDelay = 10;
            AjudaTextBox.AutoPopDelay = 100000;
            AjudaTextBox.InitialDelay = 10;
            AjudaTextBox.ReshowDelay = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(381, 261);
            Controls.Add(groupBoxCheats);
            Controls.Add(groupBoxModificarValores);
            Controls.Add(groupBoxAimbot);
            Controls.Add(groupBoxESP);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Cheat Externo AssaultCube 1.3.0.2";
            Load += Form1_Load;
            groupBoxESP.ResumeLayout(false);
            groupBoxESP.PerformLayout();
            groupBoxAimbot.ResumeLayout(false);
            groupBoxAimbot.PerformLayout();
            groupBoxModificarValores.ResumeLayout(false);
            groupBoxModificarValores.PerformLayout();
            groupBoxCheats.ResumeLayout(false);
            groupBoxCheats.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxESP;
        private GroupBox groupBoxAimbot;
        private GroupBox groupBoxModificarValores;
        private GroupBox groupBoxCheats;
        private CheckBox checkBoxDesenhar_Linha;
        private CheckBox CheckBoxESP;
        private CheckBox checkBoxAimbot;
        private Button buttonAplicar;
        private TextBox textBoxValorItem;
        private ComboBox comboBoxSelecaoValor;
        private ToolTip AjudaTextBox;
        private CheckBox checkBoxBalaInfinita;
        private CheckBox checkBoxBolsaInfinita;
        private CheckBox checkBoxVidaInfinita;
        private CheckBox checkBoxSemRecuo;
        private CheckBox checkBoxTiroRapido;
        private CheckBox checkBoxSemRepulso;
    }
}
