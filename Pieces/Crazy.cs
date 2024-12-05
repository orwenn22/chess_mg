namespace chess_mg.Pieces;

// THE CRAZY !!!1!1!
public class Crazy : BasePiece
{
    public Crazy(Board board, int team, int x, int y) : base(board, team, PieceType.Crazy, x, y)
    {
        
    }
    
    public override void GetPossibleMoves()
    {
        base.GetPossibleMoves();
        
        //Up right
        for (int i = 1; i < 8; i++)
        {
            if(X+i > 7 || Y-i < 0) break;
            
            BasePiece p = GetBoard.GetPieceAt(X+i, Y-i);
            if(p == null) GetBoard.SetPossibleMove(X+i, Y-i, 1);            //Empty cell
            else if(p.Team == Team) break;
            else                                                        //Piece of different team
            {
                GetBoard.SetPossibleMove(X+i, Y-i, 1);
                break;
            }
        }
        
        //Up left
        for (int i = 1; i < 8; i++)
        {
            if(X-i < 0 || Y-i < 0) break;
            
            BasePiece p = GetBoard.GetPieceAt(X-i, Y-i);
            if(p == null) GetBoard.SetPossibleMove(X-i, Y-i, 1);            //Empty cell
            else if(p.Team == Team) break;
            else                                                        //Piece of different team
            {
                GetBoard.SetPossibleMove(X-i, Y-i, 1);
                break;
            }
        }
        
        //Bottom right
        for (int i = 1; i < 8; i++)
        {
            if(X+i > 7 || Y+i > 7) break;
            
            BasePiece p = GetBoard.GetPieceAt(X+i, Y+i);
            if(p == null) GetBoard.SetPossibleMove(X+i, Y+i, 1);            //Empty cell
            else if(p.Team == Team) break;
            else                                                        //Piece of different team
            {
                GetBoard.SetPossibleMove(X+i, Y+i, 1);
                break;
            }
        }
        
        //Bottom left
        for (int i = 1; i < 8; i++)
        {
            if(X-i < 0 || Y+i > 7) break;

            BasePiece p = GetBoard.GetPieceAt(X-i, Y+i);
            if(p == null) GetBoard.SetPossibleMove(X-i, Y+i, 1);            //Empty cell
            else if(p.Team == Team) break;
            else                                                        //Piece of different team
            {
                GetBoard.SetPossibleMove(X-i, Y+i, 1);
                break;
            }
        }
    }
}
