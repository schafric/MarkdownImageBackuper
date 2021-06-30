using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MarkdownImageBackuper.Io;

namespace MarkdownImageBackuper
{
    public static class MarkdownParser
    {
        public static IEnumerable<ImageLink> ParseImageLinks(SourceDirectory directory)
        {
            return ProcessRawDirectory(directory.DirectoryPath);
        }

        private static IEnumerable<ImageLink> ParseImagesFromFile(string filePath)
        {
            var images = new List<ImageLink>();
            var fileStream = new StreamReader(filePath);
            var previous = (char)fileStream.Read();
            var current = (char)fileStream.Read();

            while (fileStream.Peek() >= 0)
            {
                var isImageStartDetected = previous == '!' && current == '[';

                if (isImageStartDetected)
                {
                    // Skip the image description.
                    while (fileStream.Peek() >= 0 && (char) fileStream.Peek() != ']')
                    {
                        fileStream.Read();
                    }

                    // Validate that the next character is ']' and skip it.
                    var validFormat = (char) fileStream.Peek() == ']';
                    fileStream.Read();
                    
                    // Validate that the next character is '(' and skip it.
                    validFormat = (char) fileStream.Peek() == '(';
                    fileStream.Read();

                    if (validFormat)
                    {
                        var imageLink = new StringBuilder();

                        // Read the image url until closing ')' is detected.
                        while (fileStream.Peek() >= 0 && (char) fileStream.Peek() != ')')
                        {
                            imageLink.Append((char) fileStream.Read());
                        }

                        var urlString = imageLink.ToString();
                        if (Uri.IsWellFormedUriString(urlString, UriKind.Absolute))
                        {
                            images.Add(RoamResearchImageLink.Create(imageLink.ToString()));
                        }
                    }
                }

                previous = current;
                current = (char) fileStream.Read();
            }

            return images;
        }

        private static IEnumerable<ImageLink> ProcessRawDirectory(string directoryPath)
        {
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                string fileExt = Path.GetExtension(file);

                if (fileExt == ".md")
                {
                    var fileImages = ParseImagesFromFile(file);
                    foreach (var image in fileImages)
                    {
                        yield return image;
                    }
                }
            }

            foreach (var subDirectory in Directory.GetDirectories(directoryPath))
            {
                var result = ProcessRawDirectory(subDirectory);

                foreach (var nestedFileImage in result)
                {
                    yield return nestedFileImage;
                }
            }
        }
    }
}