namespace chess_mg.Pieces;

public class Queen : BasePiece
{
    public Queen(Board board, int team, int x, int y) : base(board, team, PieceType.Queen, x, y)
    {
        
    }

    public override void GetPossibleMoves()
    {
        base.GetPossibleMoves();
        TowerPossibleMoves();
        CrazyPossibleMoves();
    }
    
    
    //Copy-pasted from Tower.cs
    //TODO : better way of calling it ?
    private void TowerPossibleMoves()
    {
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
    
    //Copy-pasted from Crazy.cs
    //TODO : better way of calling it ?
    private void CrazyPossibleMoves()
    {
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