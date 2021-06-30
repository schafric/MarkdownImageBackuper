using System.Diagnostics;

namespace MarkdownImageBackuper
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            var sourceDirectory = IO.GetSourceDirectory();
            var backupDirectory = IO.GetOrCreateBackupDirectory(sourceDirectory.DirectoryPath);
            var imageLinks = MarkdownParser.ParseImageLinks(sourceDirectory);

            timer.Start();
            var (downloaded, skipped) = IO.DownloadImagesFromLinks(imageLinks, backupDirectory);
            timer.Stop();

            IO.PrintSummary(downloaded, skipped, timer.Elapsed, sourceDirectory.DirectoryPath, backupDirectory.DirectoryPath);
        }
    }
}
