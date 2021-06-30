using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MarkdownImageBackuper.Io
{
    public static class IO
    {
        public static SourceDirectory GetSourceDirectory()
        {
            Logger.Prompt("Provide the directory you want to run backup from");
            var inputPath = Console.ReadLine();
            var isInputEmpty = inputPath == null || inputPath.Length == 0;

            if (isInputEmpty)
            {
                Logger.LogInfo("You provided empty input, please provide path to directory.");
                return GetSourceDirectory();
            }

            var directoryPath = inputPath.Split()[0];
            var isDirectoryInvalid = directoryPath.Length == 0 || !Directory.Exists(directoryPath);

            if (isDirectoryInvalid)
            {
                Logger.LogInfo("The path you've provided points to invalid directory.");
                return GetSourceDirectory();
            }

            return SourceDirectory.CreateNew(directoryPath);
        }

        public static BackingDirectory GetOrCreateBackupDirectory(string sourceDirectoryPath)
        {
            
            Logger.Prompt("Provide the directory where you want to backup images or hit enter to automatically create new one:");
            var inputPath = Console.ReadLine();

            if (String.IsNullOrEmpty(inputPath))
            {
                Logger.LogInfo("Empty input, will create new directory");
                var newDirectory = CreateNewDirectory(sourceDirectoryPath);
                Logger.LogInfo($"Created backup directory: {newDirectory.DirectoryPath}");

                return newDirectory;
            }

            var directoryPath = inputPath.Split()[0];
            var isInvalidDirectory = directoryPath.Length == 0 || !Directory.Exists(directoryPath);

            if (isInvalidDirectory)
            {
                Logger.LogInfo("The path you've provided points to invalid directory, will create new one");
                var newDirectory = CreateNewDirectory(sourceDirectoryPath);
                Logger.LogInfo($"Created backup directory: {newDirectory.DirectoryPath}");

                return newDirectory;
            }

            return ExistingBackingDirectory.CreateNew(inputPath);
        }

        public static (int, int) DownloadImagesFromLinks(IEnumerable<ImageLink> imageLinks, BackingDirectory backupDirectory)
        {
            var downloadedCount = 0;
            var skippedCount = 0;
            
            Logger.LogInfo("Starting with backup...");
            
            using (WebClient client = new WebClient())
            {
                foreach (var imageLink in imageLinks)
                {
                    var name = imageLink.ParseNameFromLink();
                    if (File.Exists($"{backupDirectory.DirectoryPath}\\{name}.png"))
                    {
                        Console.WriteLine($"{name} already downloaded, skipping");
                        skippedCount += 1;
                    }
                    else
                    {
                        Console.Write($"Downloading: [{name}]");
                        client.DownloadFile(new Uri(imageLink.GetLink()), $"{backupDirectory.DirectoryPath}\\{name}.png");
                        Console.Write("- DONE\n");
                        downloadedCount += 1;
                    }
                }
            }

            return (downloadedCount, skippedCount);
        }

        public static void PrintSummary(int downloadedCount, int skippedCount, TimeSpan timeTaken, string fromPath, string toPath)
        {
            Console.WriteLine("------------ SUMMARY ---------------");
            Console.WriteLine($"Backed up (downloaded): {downloadedCount} and skipped {skippedCount} images");
            Console.WriteLine($" - FROM: {fromPath}");
            Console.WriteLine($" - TO: {toPath}");
            Console.WriteLine("It took: " + timeTaken.ToString(@"m\:ss\.fff"));
        }

        private static NewBackingDirectory CreateNewDirectory(string sourceDirectoryPath)
        {
            var nowTime = DateTime.Now;
            return NewBackingDirectory.CreateNew($"{sourceDirectoryPath}\\backup_{nowTime.Year}-{nowTime.Month}-{nowTime.Day}-{nowTime.Hour}-{nowTime.Minute}-{nowTime.Second}");
        }
    }
}