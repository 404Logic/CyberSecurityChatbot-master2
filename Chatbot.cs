using System;
using System.Collections.Generic;

namespace CyberSecurityChatbot
{
    public class Chatbot
    {
        private readonly string userName;
        private string lastTopic = "";
        private readonly Random random = new();

        // Generic collections — required by Part 2
        private readonly Dictionary<string, List<string>> keywordResponses;
        private readonly Dictionary<string, string> userMemory;

        // Delegate for sentiment processing — required by Part 2
        public delegate string ResponseProcessor(string input, string context);
        private readonly ResponseProcessor sentimentProcessor;

        public string BotName { get; set; } = "CyberBot";
        public string BotVersion { get; set; } = "2.0";

        public Chatbot(string name)
        {
            userName = name;
            userMemory = new Dictionary<string, string> { ["name"] = name };
            sentimentProcessor = ProcessSentiment;
            keywordResponses = BuildResponses();
        }

        // ── Keyword response bank ──────────────────────────────────────────────
        private Dictionary<string, List<string>> BuildResponses() => new()
        {
            ["password"] = new()
            {
                $"Great question, {userName}! Use strong passwords — at least 12 characters mixing uppercase, lowercase, numbers, and symbols. Never reuse passwords across accounts!",
                "Consider a password manager like Bitwarden or 1Password. They generate and store complex, unique passwords for every account securely.",
                "Try a passphrase: a random sequence of words like 'PurpleTiger$Dance7' — long, memorable, and harder to crack than short complex passwords."
            },
            ["phishing"] = new()
            {
                "Phishing emails mimic trusted organisations. Always verify the sender's address — look for subtle misspellings like 'paypa1.com' instead of 'paypal.com'.",
                "Never click links in unexpected emails. Go directly to websites by typing the URL yourself rather than following email links.",
                "Be cautious of emails creating urgency ('Your account will be closed!'). Legitimate organisations never pressure you to act immediately."
            },
            ["malware"] = new()
            {
                "Malware is harmful software designed to damage or compromise your device. Install reputable antivirus software and keep it updated at all times.",
                "Only download software from official sources — the Microsoft Store, App Store, or verified vendor websites. Avoid cracked or pirated software.",
                "Signs of malware include slow performance, unexpected pop-ups, unusual data usage, or programs launching by themselves. Run a scan immediately if you notice these."
            },
            ["scam"] = new()
            {
                $"Stay alert, {userName}! Scammers create urgency to pressure you into acting fast. Always pause, verify, and research before responding to any suspicious message.",
                "Common scams: fake tech support calls, lottery winnings, urgent payment requests. If it sounds too good to be true — it is.",
                "Never share banking details, passwords, or personal information with anyone who contacts you unexpectedly, whether by phone, email, or message."
            },
            ["privacy"] = new()
            {
                "Review app permissions regularly — many apps request far more access than they need. Revoke camera, microphone, and contacts access from apps that don't need them.",
                "Use a VPN on public Wi-Fi to encrypt your internet traffic and protect your data from anyone monitoring the network.",
                "Be mindful of social media oversharing. Your birthday, address, workplace, and daily routine can all be exploited by malicious actors."
            },
            ["safe browsing"] = new()
            {
                "Always check for HTTPS and the padlock icon before entering personal information on any website. HTTP sites are not encrypted.",
                "Use a reputable ad blocker — malicious ads (malvertising) can infect your device just by viewing a webpage, even on legitimate sites.",
                "Keep your browser and all extensions updated. Outdated browser software is one of the most common entry points for attackers."
            },
            ["social engineering"] = new()
            {
                "Social engineering manipulates people into revealing confidential information. Be cautious of unexpected requests — even from people who seem familiar.",
                "Attackers often impersonate IT staff, colleagues, or authority figures. Always verify identities through official channels before complying with unusual requests.",
                "If someone pressures you to bypass a security procedure 'just this once', treat it as a red flag. Legitimate requests always follow proper procedures."
            },
            ["two-factor"] = new()
            {
                "Two-factor authentication (2FA) means attackers can't access your account even if they steal your password — they still need your second factor.",
                "Use an authenticator app (Google Authenticator, Authy) rather than SMS-based 2FA. SIM-swapping attacks can intercept text message codes.",
                $"Enable 2FA on your most important accounts first, {userName}: email, banking, and social media are your top priorities."
            },
            ["ransomware"] = new()
            {
                "Ransomware encrypts all your files and demands payment to restore access. Your best defence: regular backups stored offline or in a separate cloud account.",
                "Ransomware most commonly arrives through malicious email attachments. Never open attachments from unknown senders.",
                "Keep your OS and all software updated. Many major ransomware attacks exploited known vulnerabilities that available patches would have fixed."
            },
            ["vpn"] = new()
            {
                "A VPN encrypts your internet connection, protecting your data on public Wi-Fi in cafés, hotels, and airports from eavesdroppers.",
                "Choose a reputable paid VPN provider with a verified no-logs policy. Free VPNs typically monetise your data — the opposite of what you want.",
                "A VPN masks your IP address and encrypts traffic, making it much harder for ISPs, advertisers, and attackers to monitor your activity."
            },
            ["firewall"] = new()
            {
                "A firewall monitors incoming and outgoing network traffic and blocks suspicious connections. Always keep your OS firewall enabled.",
                "For extra protection, consider a dedicated hardware firewall for your home network — especially important if you work from home.",
                "A firewall is your network's first line of defence. Pair it with antivirus software and regular updates for layered security."
            }
        };

        // ── Sentiment detection via delegate ──────────────────────────────────
        private string ProcessSentiment(string input, string context)
        {
            if (input.Contains("worried") || input.Contains("scared") ||
                input.Contains("afraid") || input.Contains("anxious") || input.Contains("nervous"))
            {
                string tip = GetContextTip(context);
                return $"It's completely understandable to feel that way, {userName} — " +
                       $"cyber threats are real, but knowledge is your best defence.\n\n{tip}";
            }

            if (input.Contains("frustrated") || input.Contains("confused") ||
                input.Contains("don't understand") || input.Contains("difficult") || input.Contains("overwhelmed"))
                return $"I understand it can feel overwhelming, {userName}. " +
                       "Let's take it one step at a time. Which part would you like me to explain more clearly?";

            if (input.Contains("curious") || input.Contains("interesting") ||
                input.Contains("cool") || input.Contains("great") || input.Contains("awesome"))
                return $"Glad you find this interesting, {userName}! " +
                       "That curiosity will really help you stay safe online. What would you like to explore next?";

            return "";
        }

        private string GetContextTip(string context)
        {
            foreach (var key in keywordResponses.Keys)
                if (context.Contains(key))
                    return GetRandom(key);

            return "Start with the basics: strong passwords, two-factor authentication, " +
                   "and being cautious about links and email attachments.";
        }

        // ── Public API ────────────────────────────────────────────────────────

        public string GetPersonalisedGreeting()
        {
            if (userMemory.TryGetValue("interest", out string? interest))
                return $"Welcome back, {userName}! As someone interested in {interest}, " +
                       "shall we continue exploring that topic?";

            return $"Hello, {userName}! I'm {BotName} v{BotVersion}, your Cybersecurity Awareness Assistant.\n\n" +
                   "You can ask me about:\n" +
                   "  • Passwords & password managers\n" +
                   "  • Phishing scams\n" +
                   "  • Malware & viruses\n" +
                   "  • Safe browsing & HTTPS\n" +
                   "  • Social engineering\n" +
                   "  • Two-factor authentication (2FA)\n" +
                   "  • Ransomware\n" +
                   "  • VPNs & privacy\n" +
                   "  • Scams & firewalls\n\n" +
                   "You can also say 'tell me more' to continue any topic. What would you like to learn about?";
        }

        public string GetResponse(string input)
        {
            // 1. Sentiment detection (via delegate)
            string sentimentResponse = sentimentProcessor(input, lastTopic);
            if (!string.IsNullOrEmpty(sentimentResponse))
                return sentimentResponse;

            // 2. Memory: user states their interest
            if (input.Contains("interested in") || input.Contains("want to learn about") || input.Contains("tell me about"))
            {
                foreach (var key in keywordResponses.Keys)
                {
                    if (input.Contains(key))
                    {
                        userMemory["interest"] = key;
                        lastTopic = key;
                        return $"Great! I'll remember that you're interested in {key}, {userName}. " +
                               $"It's a crucial part of staying safe online.\n\n{GetRandom(key)}";
                    }
                }
            }

            // 3. Conversation flow: follow-up requests
            if (input.Contains("more") || input.Contains("another tip") ||
                input.Contains("go on") || input.Contains("elaborate") || input.Contains("expand"))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    string prefix = userMemory.TryGetValue("interest", out string? i) && i == lastTopic
                        ? $"As someone especially interested in {lastTopic}, here's another tip:\n\n"
                        : $"Here's another tip about {lastTopic}:\n\n";
                    return prefix + GetRandom(lastTopic);
                }
                return "What topic would you like to explore further? Try asking about passwords, " +
                       "phishing, malware, privacy, or type 'help' for the full list.";
            }

            // 4. Memory recall
            if (input.Contains("what do you know about me") || input.Contains("what do you remember"))
            {
                var facts = new List<string> { $"your name is {userName}" };
                if (userMemory.TryGetValue("interest", out string? interest))
                    facts.Add($"you're interested in {interest}");
                return $"Here's what I remember about you: {string.Join(", and ", facts)}. " +
                       "Is there anything else you'd like to discuss?";
            }

            // 5. General conversation
            if (input.Contains("how are you"))
                return $"Running smoothly and ready to help, {userName}! " +
                       "What cybersecurity topic can I assist you with today?";

            if (input.Contains("who are you") || input.Contains("what's your purpose") || input.Contains("what is your purpose"))
                return $"I'm {BotName} v{BotVersion}, your Cybersecurity Awareness Assistant! " +
                       $"My goal is to help people like you, {userName}, understand cyber threats and stay safe online.";

            if (input.Contains("help") || input.Contains("what can i ask") || input.Contains("topics") || input.Contains("what can you do"))
                return "Here's what you can ask me about:\n" +
                       "  • Passwords\n  • Phishing\n  • Malware & viruses\n" +
                       "  • Safe browsing\n  • Social engineering\n  • Two-factor authentication (2FA)\n" +
                       "  • Ransomware\n  • VPNs\n  • Privacy\n  • Scams\n  • Firewalls\n\n" +
                       "Say 'tell me more' or 'another tip' to continue any topic!";

            // 6. Keyword recognition using dictionary
            foreach (var kvp in keywordResponses)
            {
                if (input.Contains(kvp.Key))
                {
                    lastTopic = kvp.Key;
                    if (!userMemory.ContainsKey("interest"))
                        userMemory["last_visited"] = kvp.Key;
                    return GetRandom(kvp.Key);
                }
            }

            // 7. Keyword aliases
            if (input.Contains("virus") || input.Contains("trojan") || input.Contains("spyware"))
            { lastTopic = "malware"; return GetRandom("malware"); }

            if (input.Contains("2fa") || input.Contains("mfa") || input.Contains("authenticator"))
            { lastTopic = "two-factor"; return GetRandom("two-factor"); }

            if (input.Contains("https") || input.Contains("browser") || input.Contains("website"))
            { lastTopic = "safe browsing"; return GetRandom("safe browsing"); }

            if (input.Contains("hack") || input.Contains("breach") || input.Contains("attack"))
                return $"Cyber attacks are a serious concern, {userName}. " +
                       "Your best defences are strong unique passwords, 2FA on all accounts, " +
                       "keeping software updated, and being cautious about links and attachments. " +
                       "Which of these would you like to explore further?";

            // 8. Default fallback (error handling)
            return $"I didn't quite catch that, {userName}. Could you rephrase? " +
                   "You can ask about passwords, phishing, malware, privacy, or type 'help' for the full list.";
        }

        private string GetRandom(string keyword) =>
            keywordResponses.TryGetValue(keyword, out var list)
                ? list[random.Next(list.Count)]
                : "I don't have specific information on that yet — keep asking!";
    }
}
