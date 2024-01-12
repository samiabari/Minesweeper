

namespace Minsweeper.TestMethod
{
    public interface IConsole
    {
        string ReadLine();
        void WriteLine(string value);
        ConsoleKeyInfo ReadKey();
        void Clear();
    }
}
