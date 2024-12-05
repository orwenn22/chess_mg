namespace chess_mg.Pieces;

public class Pawn : BasePiece
{
    public Pawn(Board board, int team, int x, int y) : base(board, team, PieceType.Pawn, x, y) 
    {
        
    }

    public override void GetPossibleMoves()
    {
        base.GetPossibleMoves();
        
        int direction = (Team == 0) ? 1 : -1;

        //Facing piece
        BasePiece other_cell = GetBoard.GetPieceAt(X, Y + direction);
        if(other_cell == null) GetBoard.SetPossibleMove(X, Y+direction, 1);

        //Facing piece (distance = 2)
        if (other_cell == null && MoveCount == 0)
        {
            other_cell = GetBoard.GetPieceAt(X, Y + direction*2);
            if(other_cell == null) GetBoard.SetPossibleMove(X, Y + direction*2, 1);
        }
        
        //diagonals
        other_cell = GetBoard.GetPieceAt(X+1, Y + direction);
        if(other_cell != null && other_cell.Team != Team) GetBoard.SetPossibleMove(X+1, Y + direction, 1);
        other_cell = GetBoard.GetPieceAt(X-1, Y + direction);
        if(other_cell != null && other_cell.Team != Team) GetBoard.SetPossibleMove(X-1, Y + direction, 1);
    }
}
