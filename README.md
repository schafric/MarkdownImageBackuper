# MarkdownImageBackuper
C# console application for incremental backups of images from markdown files (currently supports naming from links from Roam Research).
Basic idea behind the program is to be able to regularly backup your images from markdown files which are written through Roam.

### Usage:
1. Download the project through GitHub
2. Get into terminal and navigate into the folder containing the project
##### Through terminal:
3a. Run `dotnet run .\MarkdownImageBackuper.csproj` and follow the instructions in the program.

##### Through your IDE (Visual Studio / IDEA Ridea etc.)
3b. Click on run button in your IDE and follow the instructions in the program.

### Examples:
##### Clean start
```
PS C:\Users\RichardSchafer\Dev\MarkdownImageBackuper> dotnet run .\MarkdownImageBackuper.csproj
Q: Provide the directory you want to run backup from
> C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic
Q: Provide the directory where you want to backup images or hit enter to automatically create new one:
>
[BACKUPER]: Empty input, will create new directory
[BACKUPER]: Created backup directory: C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic\backup_2021-6-30-13-25-31
[BACKUPER]: Starting with backup...
Downloading: [2Fiaz3ATgVUk]- DONE
Downloading: [2Fapt282DtSS]- DONE
Downloading: [2FkohQg5YRIB]- DONE
Downloading: [2FcMOq2eEO7N]- DONE
------------ SUMMARY ---------------
Backed up (downloaded): 4 and skipped 0 images
 - FROM: C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic
 - TO: C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic\backup_2021-6-30-13-25-31
It took: 0:11.217
```

##### Incremental download (manually deleted one image from previous step backup)
```
PS C:\Users\RichardSchafer\Dev\MarkdownImageBackuper> dotnet run .\MarkdownImageBackuper.csproj
Q: Provide the directory you want to run backup from
> C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic
Q: Provide the directory where you want to backup images or hit enter to automatically create new one:
> C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic\backup_2021-6-30-13-25-31
[BACKUPER]: Starting with backup...
2Fiaz3ATgVUk already downloaded, skipping
2Fapt282DtSS already downloaded, skipping
2FkohQg5YRIB already downloaded, skipping
Downloading: [2FcMOq2eEO7N]- DONE
------------ SUMMARY ---------------
Backed up (downloaded): 1 and skipped 3 images
 - FROM: C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic
 - TO: C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic\backup_2021-6-30-13-25-31
It took: 0:01.791
```

##### With invalid input (non existing folders)
```
PS C:\Users\RichardSchafer\Dev\MarkdownImageBackuper> dotnet run .\MarkdownImageBackuper.csproj
Q: Provide the directory you want to run backup from
> C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic-nonexisting\
[BACKUPER]: The path you've provided points to invalid directory.
Q: Provide the directory you want to run backup from
> C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic-nonexisting-two\
[BACKUPER]: The path you've provided points to invalid directory.
Q: Provide the directory you want to run backup from
>
[BACKUPER]: You provided empty input, please provide path to directory.
Q: Provide the directory you want to run backup from
> C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic
Q: Provide the directory where you want to backup images or hit enter to automatically create new one:
> C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic\backup_2021-6-nonexisting
[BACKUPER]: The path you've provided points to invalid directory, will create new one
[BACKUPER]: Created backup directory: C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic\backup_2021-6-30-13-33-0
[BACKUPER]: Starting with backup...
Downloading: [2Fiaz3ATgVUk]- DONE
Downloading: [2Fapt282DtSS]- DONE
Downloading: [2FkohQg5YRIB]- DONE
Downloading: [2FcMOq2eEO7N]- DONE
------------ SUMMARY ---------------
Backed up (downloaded): 4 and skipped 0 images
 - FROM: C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic
 - TO: C:\Users\RichardSchafer\Dev\MarkdownImageBackuper\examples\basic\backup_2021-6-30-13-33-0
It took: 0:20.917
```


### Examples
The `/examples` folder contains other directories with files, to test the program on:
- `/examples/basic` contains the most basic usage of the program
- `/examples/invalid-links` contains a file with some invalid / incomplete markdown image links, link to nonexistent image and one image with description (valid)
- `/examples/multiple-files` contains more files in the directory to backup images from
- `/examples/nested` contains nested structure of file to backup images from