namespace chess_mg.Pieces;

public class Knight : BasePiece
{
    public Knight(Board board, int team, int x, int y) : base(board, team, PieceType.Knight, x, y)
    {
        
    }

    public override void GetPossibleMoves()
    {
        base.GetPossibleMoves();
        
        int[][] moves = new int[][]
        {
            new int[2]{ X+2, Y+1 },
            new int[2]{ X+2, Y-1 },
            new int[2]{ X-2, Y+1 },
            new int[2]{ X-2, Y-1 },
            new int[2]{ X+1, Y+2 },
            new int[2]{ X-1, Y+2 },
            new int[2]{ X+1, Y-2 },
            new int[2]{ X-1, Y-2 },
        };

        foreach (var move in moves)
        {
            BasePiece p = GetBoard.GetPieceAt(move[0], move[1]);
            if(p == null || p.Team != Team) GetBoard.SetPossibleMove(move[0], move[1], 1);
        }
    }
}