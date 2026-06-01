namespace MulticastP2P
{
    partial class FrmMain
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
            LblMensaje = new Label();
            TxtBoxMensaje = new TextBox();
            LblContra = new Label();
            TxtBoxContra = new TextBox();
            BtnEnviar = new Button();
            TxtBoxChat = new RichTextBox();
            LblMensajeRecibir = new Label();
            SuspendLayout();
            // 
            // LblMensaje
            // 
            LblMensaje.AutoSize = true;
            LblMensaje.Location = new Point(30, 17);
            LblMensaje.Name = "LblMensaje";
            LblMensaje.Size = new Size(77, 25);
            LblMensaje.TabIndex = 0;
            LblMensaje.Text = "Mensaje";
            // 
            // TxtBoxMensaje
            // 
            TxtBoxMensaje.Location = new Point(30, 45);
            TxtBoxMensaje.Multiline = true;
            TxtBoxMensaje.Name = "TxtBoxMensaje";
            TxtBoxMensaje.Size = new Size(515, 310);
            TxtBoxMensaje.TabIndex = 1;
            // 
            // LblContra
            // 
            LblContra.AutoSize = true;
            LblContra.Location = new Point(35, 393);
            LblContra.Name = "LblContra";
            LblContra.Size = new Size(101, 25);
            LblContra.TabIndex = 2;
            LblContra.Text = "Contraseña";
            // 
            // TxtBoxContra
            // 
            TxtBoxContra.Location = new Point(142, 387);
            TxtBoxContra.Name = "TxtBoxContra";
            TxtBoxContra.Size = new Size(927, 31);
            TxtBoxContra.TabIndex = 3;
            // 
            // BtnEnviar
            // 
            BtnEnviar.Location = new Point(30, 465);
            BtnEnviar.Name = "BtnEnviar";
            BtnEnviar.Size = new Size(1039, 64);
            BtnEnviar.TabIndex = 4;
            BtnEnviar.Text = "ENVIAR";
            BtnEnviar.UseVisualStyleBackColor = true;
            BtnEnviar.Click += BtnEnviar_Click;
            // 
            // TxtBoxChat
            // 
            TxtBoxChat.Location = new Point(573, 45);
            TxtBoxChat.Name = "TxtBoxChat";
            TxtBoxChat.Size = new Size(496, 310);
            TxtBoxChat.TabIndex = 5;
            TxtBoxChat.Text = "";
            // 
            // LblMensajeRecibir
            // 
            LblMensajeRecibir.AutoSize = true;
            LblMensajeRecibir.Location = new Point(573, 17);
            LblMensajeRecibir.Name = "LblMensajeRecibir";
            LblMensajeRecibir.Size = new Size(147, 25);
            LblMensajeRecibir.TabIndex = 6;
            LblMensajeRecibir.Text = "Mensaje entrante";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1111, 561);
            Controls.Add(LblMensajeRecibir);
            Controls.Add(TxtBoxChat);
            Controls.Add(BtnEnviar);
            Controls.Add(TxtBoxContra);
            Controls.Add(LblContra);
            Controls.Add(TxtBoxMensaje);
            Controls.Add(LblMensaje);
            Name = "FrmMain";
            Text = "Multicast P2P";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LblMensaje;
        private TextBox TxtBoxMensaje;
        private Label LblContra;
        private TextBox TxtBoxContra;
        private Button BtnEnviar;
        private RichTextBox TxtBoxChat;
        private Label LblMensajeRecibir;
    }
}
