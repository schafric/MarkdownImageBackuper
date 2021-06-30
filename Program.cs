using System.Diagnostics;

namespace MarkdownImageBackuper
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            var sourceDirectory = IO.GetSourceDirectory();
            var backupDirectory = IO.GetOrCreateBackupDirectory(sourceDirectory);
            var imagesToDownload = MarkdownParser.ParseImageLinks(sourceDirectory);

            timer.Start();
            var (downloaded, skipped) = IO.DownloadImagesFromLinks(imagesToDownload, backupDirectory);
            timer.Stop();

            IO.PrintSummary(downloaded, skipped, timer.Elapsed, sourceDirectory, backupDirectory);
        }
    }
}
