using System;
using System.Collections.Generic;
using chess_mg.Pieces;

namespace chess_mg.PlayerControllers;

public class BotPlayerController : BasePlayerController
{
    private Random _random;
    public BotPlayerController() 
    {
        _random = new Random();
    }

    public override void OnUpdate(int currentTeam)
    {
        //base.OnUpdate(currentTeam);
        
        
        //Get all alive pieces
        List<BasePiece> alivePieces = new List<BasePiece>();
        for (int y = 0; y < 8; ++y)
        {
            for (int x = 0; x < 8; ++x)
            {
                BasePiece p = GetBoard.GetPieceAt(x, y);
                if(p == null || p.Team != currentTeam) continue;
                alivePieces.Add(p);
            }
        }
        
        //Click on a piece with available movements
        List<BoardPos> possibleMoves = new List<BoardPos>();
        do
        {
            BasePiece piece = alivePieces[_random.Next(0, alivePieces.Count)];
            GetBoard.OnCellClick(piece.X, piece.Y);
            possibleMoves = GetMoves();
        } while (possibleMoves.Count == 0);

        //3. Click on one of the possible moves (the one with an opponent piece should be prioritised ?)
        int randomMove = _random.Next(0, possibleMoves.Count);
        GetBoard.OnCellClick(possibleMoves[randomMove].x, possibleMoves[randomMove].y);
    }
    

    List<BoardPos> GetMoves()
    {
        List<BoardPos> moves = new List<BoardPos>();
        for (int y = 0; y < 8; ++y)
        {
            for (int x = 0; x < 8; ++x)
            {
                if(GetBoard.IsMovePossible(x, y)) moves.Add(new BoardPos(x, y));
            }
        }
        return moves;
    }
}
