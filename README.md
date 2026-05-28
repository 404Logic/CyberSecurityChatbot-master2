[# 🔒 CyberSecurity Awareness Chatbot]
[# 🔒 CyberSecurity Awareness Chatbot]

[![.NET](https://github.com/dotnet/core/workflows/.NET/badge.svg)](https://github.com/dotnet/core)

[![CI](https://github.com/ronew/CyberSecurityChatbot/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ronew/CyberSecurityChatbot/actions/workflows/dotnet.yml)

A **WPF cybersecurity awareness chatbot** built in C# (.NET). Provides interactive education on key cybersecurity topics like passwords, phishing, malware, and more. Features an on-load voice greeting, an ASCII-art banner UI, and a chat interface with themed message bubbles.


## ✨ Features

- 🎵 **Voice Greeting**: Plays `greeting.wav` on startup (with fallback)
- 🎨 **ASCII Art Logo**: Eye-catching cybersecurity-themed banner
- ⌨️ **User Validation**: Ensures valid name input
- 💬 **Interactive Chat**: Keyword-based responses for 10+ topics

- 🎨 **Message Bubble UI**: Different styling for user vs bot
- 🌐 **Topic Memory**: The bot remembers what topic you’re interested in

- 📱 **Topics Covered**

  | Topic | Description |
  |-------|-------------|
  | Passwords | Strong password best practices |

  | Phishing | Email scam detection |
  | Malware/Virus | Protection strategies |
  | Safe Browsing | HTTPS & link safety |
  | Social Engineering | Human manipulation tactics |
  | 2FA | Multi-factor authentication |
  | Scams | Urgency pressure tactics |
  | Privacy | Data protection tips |

## 📋 Project Structure

```
CyberSecurityChatbot/
├── Program.cs          # Main entry: UI, audio, chat loop
├── Chatbot.cs          # Core logic: GetResponse() with keyword matching
├── greeting.wav        # Voice greeting audio file
├── CyberSecurityChatbot.csproj  # .NET project file
├── CyberSecurityChatbot.slnx    # Solution file
├── .gitignore          # Git ignores
├── TODO.md             # Task tracking
└── README.md           # This file!
```

## 🔄 CI/CD Pipeline

Automated GitHub Actions (`.github/workflows/dotnet.yml`):
- **Triggers**: push/PR to `main/master`
- **Matrix**: .NET 8.0.x (ubuntu-latest)
- **Jobs**: restore → build (Release) → test → publish → artifact upload
- **Status**: [![CI](https://github.com/ronew/CyberSecurityChatbot/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ronew/CyberSecurityChatbot/actions/workflows/dotnet.yml)

## 🛠️ Prerequisites

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- Windows (for `System.Media.SoundPlayer`; Linux/Mac need alternatives)

## 🚀 Quick Start & GitHub Workflow Test

1. **Clone/Push to GitHub**:
   
```bash
   git add .
   git commit -m "Add CyberSecurityChatbot with CI workflow"
   git push origin main
   
```

2. **Verify Workflow**: Check [Actions tab](https://github.com/ronew/CyberSecurityChatbot/actions) for green CI checks.

3. **Local Setup**:
   - Open terminal in project root
   - Set `greeting.wav` → Properties → Copy to Output = "Copy always"

4. **Run Locally**:
   
```bash
   dotnet restore
   dotnet build
   dotnet run
   
```

## 💻 Usage

```
1. Enter your name when prompted
2. Ask questions like:
   - "tell me about passwords"
   - "how to avoid phishing?"
   - "what is malware"
   - "help" (topic list)
3. Type 'exit' to quit
```

**Example Session:**
```
Please enter your name: Alice
Welcome, Alice! I'm your Cybersecurity Awareness Assistant...

Alice: how do I make good passwords?
Bot: Great question, Alice! Use strong passwords with uppercase, lowercase, numbers, and symbols...
```

## 🧪 Testing

Run `dotnet test` (no unit tests yet). Manual testing via `dotnet run`.

## 📸 Screenshots

*(Console output - add actual screenshots here)*

```
[ASCII Logo]
Welcome, User!
User: phishing
Bot: Phishing scams try to trick you...
```

## 🔮 Roadmap

- [ ] Add unit tests (xUnit/NUnit)
- [ ] Cross-platform audio (NAudio)
- [ ] NLP improvements (ML.NET)
- [ ] GUI version (WPF/MAUI)
- [ ] More topics & quizzes
- [ ] Multi-language support

## 🤝 Contributing

1. Fork the repo
2. Create feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push (`git push origin feature/AmazingFeature`)
5. Open Pull Request

## 📄 License

MIT License - see [LICENSE](LICENSE) file (create if needed).

## 🙏 Acknowledgments

- Built with ❤️ for cybersecurity awareness
- .NET Foundation

---

**Stay safe online! 🔒**
