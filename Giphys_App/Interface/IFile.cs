using Giphys_App.Moduls.Classes;

namespace Giphys_App.Interface
{
    public interface IFile
    {
        string ReadFromFile(string fileName);
        void WriteToFile(string giphyURL, string fileName);
        bool CheckIfFileExist(string str);
    }
}
