namespace chess_mg.Pieces;

public class Tower : BasePiece
{
    public Tower(Board board, int team, int x, int y) : base(board, team, PieceType.Tower, x, y)
    {
        
    }

    public override void GetPossibleMoves()
    {
        base.GetPossibleMoves();
        
        //Left
        for (int x = X+1; x < 8; x++)
        {
            BasePiece p = GetBoard.GetPieceAt(x, Y);
            if(p == null) GetBoard.SetPossibleMove(x, Y, 1);            //Empty cell
            else if(p.Team == Team) break;                              //Piece of same team
            else                                                        //Piece of different team
            {
                GetBoard.SetPossibleMove(x, Y, 1);
                break;
            }
        }
        
        //Right
        for (int x = X-1; x >= 0; x--)
        {
            BasePiece p = GetBoard.GetPieceAt(x, Y);
            if(p == null) GetBoard.SetPossibleMove(x, Y, 1);            //Empty cell
            else if(p.Team == Team) break;                              //Piece of same team
            else                                                        //Piece of different team
            {
                GetBoard.SetPossibleMove(x, Y, 1);
                break;
            }
        }
        
        //Top
        for (int y = Y-1; y >= 0; y--)
        {
            BasePiece p = GetBoard.GetPieceAt(X, y);
            if(p == null) GetBoard.SetPossibleMove(X, y, 1);            //Empty cell
            else if(p.Team == Team) break;                              //Piece of same team
            else                                                        //Piece of different team
            {
                GetBoard.SetPossibleMove(X, y, 1);
                break;
            }
        }
        
        //Bottom
        for (int y = Y+1; y < 8; y++)
        {
            BasePiece p = GetBoard.GetPieceAt(X, y);
            if(p == null) GetBoard.SetPossibleMove(X, y, 1);            //Empty cell
            else if(p.Team == Team) break;                              //Piece of same team
            else                                                        //Piece of different team
            {
                GetBoard.SetPossibleMove(X, y, 1);
                break;
            }
        }
    }
}