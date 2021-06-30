using System;

namespace MarkdownImageBackuper
{
    public static class Logger
    {
        public static void LogInfo(string info)
        {
            Console.WriteLine($"[BACKUPER]: {info}");
        }

        public static void Prompt(string question)
        {
            Console.WriteLine($"Q: {question}");
            Console.Write("> ");
        }
    }
}