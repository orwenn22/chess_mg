using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace chess_mg.Pieces;

public enum PieceType
{
    Pawn = 0,
    Tower,
    Knight,
    Crazy,          //les vrais ont la ref
    Queen,
    King
}


public class BasePiece
{
    private int _team;
    private PieceType _type;
    private Board _board;
    private int _x, _y;
    private int _move_count;
    
    public BasePiece(Board board, int team, PieceType type, int x, int y)
    {
        _board = board;
        _team = team;
        _type = type;
        _x = x;
        _y = y;
        _move_count = 0;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _board.Game.atlas,
            new Rectangle(_x*64, _y*64, 64, 64),
            new Rectangle(64* (int)_type, 64 + 64*_team, 64, 64),
            Color.White);
    }

    virtual public void GetPossibleMoves()
    {
        _board.ClearPossibleMoves();
    }

    public void ForceMoveTo(int x, int y)
    {
        if(x < 0 || y < 0 || x > 8 || y > 8) return;
        _x = x;
        _y = y;
        _move_count++;
    }

    public void UndoMove()
    {
        _move_count--;
        if(_move_count < 0) _move_count = 0;        //just in case
    }

    public int X => _x;
    public int Y => _y;
    public int Team => _team;
    public int MoveCount => _move_count;
    public PieceType Type => _type;
    public Board GetBoard => _board;
}