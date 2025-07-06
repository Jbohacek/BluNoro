using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using BluChat.TestClient;
using BluNoro.Core.Client;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Common.MessageTypes.Authenticate;
using BluNoro.Core.Common.MessageTypes.GetChatMessages;
using BluNoro.Core.Common.MessageTypes.GetChats;
using BluNoro.Core.Common.MessageTypes.SendMessage;

namespace BluNoro.TestClient
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            dgw_log.AutoGenerateColumns = false;
        }

        private Client client;

        public BindingList<Log> _logs = new BindingList<Log>();

        private void Main_Load(object sender, EventArgs e)
        {
            dgw_log.DataSource = _logs;
            _logs.Add(new Log("ClientLoaded"));
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            client = new Client(txt_IpAdress.Text, Convert.ToInt32(txt_port.Text));
            SetEvents();


            client.Connect();

            if (client.IsConnected)
            {
                gr_Authentication.Visible = true;
            }

            btn_start.Enabled = false;
            btn_dissconnect.Enabled = true;
        }

        private void SetEvents()
        {
            client.Events.SyncContext = SynchronizationContext.Current;

            client.TcpEvents.DataReceived += DataReceived!;

            client.Events.Register<ClientSuccessVerification>(SuccAuth);
            client.Events.Register<ClientFailedVerification>(FailedAuth);
            client.Events.Register<ClientReturnChats>(ReloadChats);
            client.Events.Register<ClientBroadcastMessage>(Messagerecieved);
            client.Events.Register<ClientMultipleStringMessages>(LoadChatMessages);

        }


        private void btn_dissconnect_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            gr_Authentication.Visible = false;
            gr_chats_messages.Visible = false;
            btn_start.Enabled = true;
            btn_dissconnect.Enabled = false;
        }

        //zapsani dat do listu
        private void DataReceived(object sender, SuperSimpleTcp.DataReceivedEventArgs e)
        {
            string message = $"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}";
            var action = new Action((() => AddLog(message)));
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        #region Authentication
            private void SuccAuth(ClientSuccessVerification msg)
            {
                txt_output.Text =
                    $@"{msg.AuthenticatedUser.Id}
{msg.AuthenticatedUser.UserName}
{msg.AuthenticatedUser.ProfilePicPath}
Chat Count: {msg.AuthenticatedUser.Chats.Count}";
                gr_chats_messages.Visible = true;

                client.Manager.ChatsClientController.ReloadChats();
                btn_reload.Enabled = false;
            }

            private void FailedAuth(ClientFailedVerification msg)
            {
                txt_output.Text = msg.Reason;
            }
        #endregion

        #region Log
            private void AddLog(string message)
            {
                _logs.Add(new Log(message));
            }

            public class Log
            {
                public string context { get; set; }
                public DateTime time { get; set; }

                public Log(string message)
                {
                    context = message;
                    time = DateTime.Now;
                }
            }

        #endregion

        private void btn_authenticate_Click(object sender, EventArgs e)
        {
            if (!client.IsConnected)
            {
                MessageBox.Show("Nepøipojeno k server!");
                return;
            }
            client.Manager.AuthClientController.SendAuthetication(txt_Username.Text, txt_Password.Text);
        }

        private Chat? _selectedChat = null;
        private void box_chats_SelectedValueChanged(object sender, EventArgs e)
        {

            if (box_chats.SelectedItem is not Chat)
                return;

            Chat? selectedChat = box_chats.SelectedItem as Chat;
            if (selectedChat == null)
                return;

            _selectedChat = selectedChat;

            client.Manager.ChatsClientController.GetChatMessages(selectedChat);

            Debug.WriteLine(sender);
        }

        private void LoadChatMessages(ClientMultipleStringMessages msg)
        {
            if (_selectedChat == null)
                return;

            box_messages.Items.Clear();
            foreach (var message in _selectedChat.Messages)
            {
                box_messages.Items.Add(message);
            }
            btn_send.Enabled = true;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (_selectedChat == null)
            {
                btn_send.Enabled = false;
                return;
            }

            client.Manager.MessageClientController.SendMessage(txt_userInputMessage.Text, _selectedChat);
        }

        private void Messagerecieved(ClientBroadcastMessage msg)
        {
            if (_selectedChat == null)
            {
                return;
            }

            if (_selectedChat.Id != msg.ParentIdChat)
            {
                return;
            }
            _selectedChat.Messages.Add(msg.Message);
            box_messages.Items.Add(msg.Message.ToString());
        }


        private void dgw_log_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgw_log.SelectedCells.Count == 1)
            {
                string message = dgw_log.SelectedCells[0].Value.ToString()!;

                LogShow logShow = new LogShow(message);
                logShow.ShowDialog();
            }
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            client.Manager.ChatsClientController.ReloadChats();
            btn_reload.Enabled = false;
        }

        private void ReloadChats(ClientReturnChats msg)
        {
            box_chats.Items.Clear();
            box_messages.Items.Clear();
            foreach (var chat in msg.Chats)
            {
                box_chats.Items.Add(chat);
            }

            btn_reload.Enabled = true;

            _selectedChat = null;
        }


    }
}
