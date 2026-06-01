using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MulticastP2P
{
    public partial class FrmMain : Form
    {
        private UdpClient udpClient = null!;
        private IPEndPoint endPoint = null!;
        private string ipMulticast = "239.255.255.250"; // IP de grupo para pruebas
        private int puerto = 5000;
        private string miNombre = "Francisco";

        public FrmMain()
        {
            InitializeComponent();
            ConfigurarRed();
        }

        private void ConfigurarRed()
        {
            // 1. Configuramos el cliente UDP para reutilizar el puerto (vital para P2P local)
            udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, puerto));

            // 2. Nos unimos al grupo Multicast
            udpClient.JoinMulticastGroup(IPAddress.Parse(ipMulticast));
            endPoint = new IPEndPoint(IPAddress.Parse(ipMulticast), puerto);

            // 3. Iniciamos el hilo que escucha la red sin congelar la pantalla
            Task.Run(EscucharMensajesAsync);
        }

        private async Task EscucharMensajesAsync()
        {
            while (true)
            {
                try
                {
                    // Esperamos a que llegue algo por la red
                    UdpReceiveResult resultado = await udpClient.ReceiveAsync();
                    string mensajeRecibido = Encoding.UTF8.GetString(resultado.Buffer);

                    // Separamos el nombre del mensaje cifrado (Formato: Nombre|MensajeCifrado)
                    string[] partes = mensajeRecibido.Split('|');
                    if (partes.Length == 2)
                    {
                        string remitente = partes[0];
                        string textoCifrado = partes[1];
                        string llave = TxtBoxContra.Text; // Tomamos la llave actual de la interfaz

                        string textoMostrar = Descifrar(textoCifrado, llave);

                        // Actualizamos la interfaz desde el hilo secundario
                        Invoke((MethodInvoker)delegate {
                            TxtBoxChat.AppendText($"[{remitente}]: {textoMostrar}{Environment.NewLine}");
                        });
                    }
                }
                catch (Exception ex)
                {
                    // Manejo básico de errores de red
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBoxMensaje.Text)) return;

            string mensajePlano = TxtBoxMensaje.Text;
            string llave = TxtBoxContra.Text;

            // Ciframos el mensaje
            string mensajeCifrado = Cifrar(mensajePlano, llave);

            // Armamos el paquete: Nombre|MensajeCifrado
            string paquete = $"{miNombre}|{mensajeCifrado}";
            byte[] bytesEnviar = Encoding.UTF8.GetBytes(paquete);

            // Lo lanzamos al grupo Multicast
            udpClient.Send(bytesEnviar, bytesEnviar.Length, endPoint);

            // Limpiamos la caja de texto
            TxtBoxMensaje.Clear();
        }

        private string Cifrar(string texto, string contraseña)
        {
            if (string.IsNullOrEmpty(contraseña)) return texto; // Si no hay llave, enviamos crudo para que los demás vean basura

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = GenerarLlaveSegura(contraseña);
                    aes.IV = new byte[16]; // Vector de inicialización estático por simplicidad

                    ICryptoTransform encriptador = aes.CreateEncryptor(aes.Key, aes.IV);
                    byte[] textoBytes = Encoding.UTF8.GetBytes(texto);
                    byte[] cifrado = encriptador.TransformFinalBlock(textoBytes, 0, textoBytes.Length);

                    return Convert.ToBase64String(cifrado);
                }
            }
            catch
            {
                return texto;
            }
        }

        private string Descifrar(string textoCifrado, string contraseña)
        {
            if (string.IsNullOrEmpty(contraseña)) return textoCifrado; // Muestra la basura

            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = GenerarLlaveSegura(contraseña);
                    aes.IV = new byte[16];

                    ICryptoTransform desencriptador = aes.CreateDecryptor(aes.Key, aes.IV);
                    byte[] cifradoBytes = Convert.FromBase64String(textoCifrado);
                    byte[] textoBytes = desencriptador.TransformFinalBlock(cifradoBytes, 0, cifradoBytes.Length);

                    return Encoding.UTF8.GetString(textoBytes);
                }
            }
            catch (CryptographicException)
            {
                // ¡Aquí ocurre la magia de la práctica!
                // Si la llave es incorrecta, AES falla y devuelve la basura cifrada.
                return $"{textoCifrado} (Llave Incorrecta)";
            }
            catch
            {
                return textoCifrado;
            }
        }

        // Helper para que cualquier contraseña sirva adaptándola a 32 bytes (256 bits)
        private byte[] GenerarLlaveSegura(string contraseña)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
            }
        }
    }
}
