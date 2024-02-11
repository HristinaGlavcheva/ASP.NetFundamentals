using TaskBoardApp.ViewModels.Board;

namespace TaskBoardApp.Contracts
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardViewModel>> GetAllAsync ();
    }
}
