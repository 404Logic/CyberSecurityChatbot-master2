using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CyberSecurityChatbot
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<ChatMessage> messages = new();
        private Chatbot? bot;
        private bool isWaitingForName = true;

        // Delegate for adding messages — demonstrates delegate usage required by Part 2
        private delegate void AddMessageHandler(string sender, string message, bool isUser);
        private readonly AddMessageHandler addMessage;

        public MainWindow()
        {
            InitializeComponent();
            ChatList.ItemsSource = messages;
            addMessage = AddChatMessage;

            AddBotMessage(
                "Welcome to CyberBot! 🔒\n\n" +
                "I'm your Cybersecurity Awareness Assistant — here to help you\n" +
                "stay safe in the digital world.\n\n" +
                "First, could you please tell me your name?");
        }

        private void AddChatMessage(string sender, string message, bool isUser)
        {
            messages.Add(new ChatMessage { Sender = sender, Message = message, IsUser = isUser });
            // Scroll after layout update so the new item is visible
            Dispatcher.BeginInvoke(
                new Action(() => ChatScrollViewer.ScrollToEnd()),
                System.Windows.Threading.DispatcherPriority.ContextIdle);
        }

        private void AddBotMessage(string message) => addMessage("CyberBot 🔒", message, false);
        private void AddUserMessage(string message) => addMessage("You", message, true);

        private void SendMessage()
        {
            string input = InputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            InputBox.Clear();
            PlaceholderText.Visibility = Visibility.Visible;

            // First message: capture user's name and initialise the chatbot
            if (isWaitingForName)
            {
                AddUserMessage(input);
                bot = new Chatbot(input);
                isWaitingForName = false;
                AddBotMessage(bot.GetPersonalisedGreeting());
                StatusText.Text = $"CyberBot v2.0  |  Chatting with {input}";
                return;
            }

            AddUserMessage(input);
            string response = bot!.GetResponse(input.ToLower());
            AddBotMessage(response);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
            InputBox.Focus();
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        private void InputBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (PlaceholderText != null)
                PlaceholderText.Visibility = string.IsNullOrEmpty(InputBox.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }
    }
}
