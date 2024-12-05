namespace chess_mg.PlayerControllers;

public class BasePlayerController
{
    private Board _board;
    
    public BasePlayerController() { }

    /// <summary>
    /// This is called by board.Update on each frames of this player's turn.
    /// </summary>
    /// <param name="currentTeam">the team id corresponding to this player, given by the board</param>
    virtual public void OnUpdate(int currentTeam)
    {
        //To be replaced in sub-classes
    }

    virtual public void SetBoard(Board board)
    {
        _board = board;
    }
    
    public Board GetBoard => _board;
}
