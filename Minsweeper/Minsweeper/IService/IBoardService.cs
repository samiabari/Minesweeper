/// <author>Samia Bari</author>
/// <file>IBoardService.cs</file>
/// <summary>
/// Description: This interface provides method to start game play.
/// </summary>
namespace Minsweeper.IService
{
    /// <summary>
    /// This interface provides method to start game play by letting users to take square input.
    /// </summary>
    public interface IBoardService
    {
        /// <summary>
        /// this method contains the functionality of letting users to take square input.
        /// </summary>
        void StartTakingSquareInput();
    }
}
