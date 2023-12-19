using BLL.Models;

namespace BLL.Utils
{
    public class FileUtis
    {
        public static Dictionary<FileExtension, string> GetExtensions()
        {
            return new Dictionary<FileExtension, string>
            {
                { FileExtension.DOCX, ".docx" },
                { FileExtension.PDF, ".pdf" },
                { FileExtension.TXT, ".txt" },
            };
        }
    }
}
