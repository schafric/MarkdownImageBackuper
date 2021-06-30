using System;
using System.IO;

namespace MarkdownImageBackuper.Io
{
    public abstract class BackingDirectory
    {
        public string DirectoryPath { get; protected set; }
    }

    public class ExistingBackingDirectory : BackingDirectory
    {
        private ExistingBackingDirectory(string path)
        {
            DirectoryPath = path;
        }

        public static ExistingBackingDirectory CreateNew(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new ArgumentException($"The directory does not exist at: {path}");
            }

            // System call in order to get absolute path.
            var directory = Directory.CreateDirectory(path);

            return new ExistingBackingDirectory(directory.FullName);
        }
    }

    public class NewBackingDirectory : BackingDirectory
    {
        private NewBackingDirectory(string path)
        {
            DirectoryPath = path;
        }
        public static NewBackingDirectory CreateNew(string path)
        {
            var directory = Directory.CreateDirectory(path);

            return new NewBackingDirectory(directory.FullName);
        }
    }

    public class SourceDirectory
    {
        private SourceDirectory(string path)
        {
            DirectoryPath = path;
        }
        
        public string DirectoryPath { get; }

        public static SourceDirectory CreateNew(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new ArgumentException($"The directory does not exist at: {path}");
            }

            // System call in order to get absolute path.
            var directory = Directory.CreateDirectory(path);

            return new SourceDirectory(directory.FullName);
        }
    }
}