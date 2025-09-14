using System;
using System.Collections.Generic;

namespace JournalApp
{
    public class PromptGenerator
    {
        private List<string> _prompts;
        private Random _rand;

        public PromptGenerator()
        {
            _rand = new Random();
            _prompts = new List<string>
            {
                "Who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?",
                "What made me smile today?",
                "What did I learn today?"
            };
        }

        public string GetRandomPrompt()
        {
            if (_prompts.Count == 0) return "Write something about your day:";
            int idx = _rand.Next(0, _prompts.Count);
            return _prompts[idx];
        }
    }
}
