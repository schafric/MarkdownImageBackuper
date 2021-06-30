namespace MarkdownImageBackuper
{
    public abstract class ImageLink
    {
        // Necessary, because every app which generates links can have it's own convention for naming.
        public abstract string ParseNameFromLink();

        public abstract string GetLink();
    }

    public class RoamResearchImageLink : ImageLink
    {
        private RoamResearchImageLink(string link)
        {
            Link = link;
        }

        public string Link { get; }

        public static RoamResearchImageLink Create(string link)
        {
            return new RoamResearchImageLink(link);
        }

        public override string GetLink()
        {
            return Link;
        }

        public override string ParseNameFromLink()
        {
            var splittedLink = Link.Split("%");
            return splittedLink[3].Split(".png")[0];
        }
    }
}