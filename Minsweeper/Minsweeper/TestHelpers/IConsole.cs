

namespace Minsweeper.TestHelpers
{
    public interface IConsole
    {
        string ReadLine();
        void WriteLine(string value);
        ConsoleKeyInfo ReadKey();
        void Clear();
    }
}
