using System.Collections.Generic;
using chess_mg.Pieces;
using chess_mg.PlayerControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace chess_mg;

//This is mostly intended for the bot top know on what he clicked
public enum BoardClickStatus
{
    ClickedOnInvalid,
    ClickedOnTeamPiece,
    ClickedOnPossibleMove
    //TODO : ClickedOnPossibleMoveAndOpponent ?
}

public class Board : State
{
    private Color[] _cellColors;       //color of the background
    private List<BasePiece> _pieces;    //all the pieces that are still alive

    private int _currentTeam;                  //index of the currently playing team
    private BasePlayerController[] _players;    //Contain the player controllers of the players
    
    private BasePiece _current;         //selected piece (or null if none is selected)
    private King[] _kings;              //keep track of the kings for checkmate checks
    private int[] _possibleMoves;      //contain the possible moves of the selected piece
    
    public Board(Game1 game, BasePlayerController controller1, BasePlayerController controller2) : base(game)
    {
        
        _cellColors = new Color[] { Color.White, Color.Black };
        _pieces = new List<BasePiece>();

        _current = null;
        _possibleMoves = new int[8*8];
        for(int i = 0; i < 8*8; i++) _possibleMoves[i] = 0;
        _currentTeam = 0;

        _players = new BasePlayerController[2] { controller1, controller2 };
        controller1.SetBoard(this);
        controller2.SetBoard(this);

        //Pawns
        for (int i = 0; i < 8; i++)
        {
            _pieces.Add(new Pawn(this, 0, i, 1));
            _pieces.Add(new Pawn(this, 1, i, 6));
        }
        
        //Towers
        _pieces.Add(new Tower(this, 0, 0, 0));
        _pieces.Add(new Tower(this, 0, 7, 0));
        _pieces.Add(new Tower(this, 1, 0, 7));
        _pieces.Add(new Tower(this, 1, 7, 7));
        
        // Knights
        _pieces.Add(new Knight(this, 0, 1, 0));
        _pieces.Add(new Knight(this, 0, 6, 0));
        _pieces.Add(new Knight(this, 1, 1, 7));
        _pieces.Add(new Knight(this, 1, 6, 7));
        
        // CRAZY !!!!1!1!
        _pieces.Add(new Crazy(this, 0, 2, 0));
        _pieces.Add(new Crazy(this, 0, 5, 0));
        _pieces.Add(new Crazy(this, 1, 2, 7));
        _pieces.Add(new Crazy(this, 1, 5, 7));
        
        // Queen
        _pieces.Add(new Queen(this, 0, 3, 0));
        _pieces.Add(new Queen(this, 1, 3, 7));
        
        // King
        _kings = new King[2] {
            new King(this, 0, 4, 0),
            new King(this, 1, 4, 7)
        };
        _pieces.Add(_kings[0]);
        _pieces.Add(_kings[1]);
    }

    public override void Update(GameTime gameTime)
    {
        _players[_currentTeam].OnUpdate(_currentTeam);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int y = 0; y < 8; ++y)
        {
            for (int x = 0; x < 8; ++x)
            {
                Rectangle dest = new Rectangle(x * 64, y * 64, 64, 64);
                
                //Cell background
                spriteBatch.Draw(
                    Game.atlas, 
                    dest, 
                    new Rectangle(0, 0, 64, 64),
                    _cellColors[(x + y)%2]);

                //Possible moves
                if (_possibleMoves[x + y * 8] == 1)
                {
                    spriteBatch.Draw(
                        Game.atlas,
                        dest,
                        new Rectangle(0, 0, 64, 64),
                        new Color(0, 255, 0, 255));     //TODO : alpha blending ?
                }
            }
        }
        
        //Pieces
        foreach (BasePiece piece in _pieces) piece.Draw(spriteBatch);
    }

    /**
     * called when a cell is clicked with the mouse
     */
    public BoardClickStatus OnCellClick(int cellX, int cellY)
    {
        if(cellX < 0 || cellX >= 8 || cellY < 0 || cellY >= 8) return BoardClickStatus.ClickedOnInvalid;
        System.Console.WriteLine("OnCellClick {0} {1}", cellX, cellY);
        
        if (_current == null)       //No piece selected
        {
            if (TryToSelectPieceAt(cellX, cellY) == true) return BoardClickStatus.ClickedOnTeamPiece;
            return BoardClickStatus.ClickedOnInvalid;
        }
        else                       //A piece is currently selected
        {
            if (_possibleMoves[cellX + cellY * 8] == 1)        //Check if the piece can move there
            {
                //TODO : logic to undo the move if the king of the playing team can be captured
                BasePiece dest_backup = GetPieceAt(cellX, cellY);
                int previous_x_backup = _current.X;
                int previous_y_backup = _current.Y;
                
                if(dest_backup != null && dest_backup.Type == PieceType.King) return BoardClickStatus.ClickedOnInvalid;       //don't do the move if the player is trying to take a king (this should never happen, but let's check just in case)
                
                RemovePieceAt(cellX, cellY);             //remove the piece from _pieces (for checkmate check)
                _current.ForceMoveTo(cellX, cellY);     //move the selected piece to the clicked location

                if (IsInCheck(_currentTeam))       //Undo move
                {
                    _current.ForceMoveTo(previous_x_backup, previous_y_backup);     //move the selected piece back at its original location
                    if(dest_backup != null) _pieces.Add(dest_backup);               //add back the destroyed piece
                    _current.UndoMove();                                            //decrement move counter of piece
                    _current.UndoMove();
                    _current.GetPossibleMoves();                                    //redisplay possible moves (nuked by IsInCheck)
                    return BoardClickStatus.ClickedOnInvalid;
                }
                else     //Next turn
                {
                    PromoteIfPossible(cellX, cellY);
                    _current = null;                                                //Unselect the piece
                    for (int i = 0; i < 8 * 8; i++) _possibleMoves[i] = 0;         //Clear the possible moves
                    _currentTeam = (_currentTeam + 1) % 2;                        //Go to next turn
                    return BoardClickStatus.ClickedOnPossibleMove;
                }
            }
            else                                                //If not try to switch piece
            {
                if (TryToSelectPieceAt(cellX, cellY) == true) return BoardClickStatus.ClickedOnTeamPiece;
                return BoardClickStatus.ClickedOnInvalid;
            }
        }

        //System.Console.WriteLine("THIS SHOULD NEVER GET REACHED");
        //return BoardClickStatus.ClickedOnInvalid;
    }

    private bool TryToSelectPieceAt(int cellX, int cellY)
    {
        BasePiece clickedPiece = GetPieceAt(cellX, cellY);
        if(clickedPiece != null && clickedPiece.Team == _currentTeam) _current = clickedPiece; 
        if(_current != null) _current.GetPossibleMoves();

        if (clickedPiece != null && clickedPiece == _current) return true;      //In this case we successfully selected the new piece
        else return false;      //In this case we did not select a new piece
    }
    
    /**
     * Get a piece at a specific cell. return null if there is no piece
     */
    public BasePiece GetPieceAt(int cellX, int cellY)
    {
        foreach (BasePiece piece in _pieces)
        {
            if (piece.X == cellX && piece.Y == cellY) return piece;
        }
        return null;
    }

    /**
     * Remove a piece from _piece.
     */
    void RemovePieceAt(int cellx, int celly)
    {
        BasePiece piece = GetPieceAt(cellx, celly);
        if(piece != null) _pieces.Remove(piece);
    }

    public void ClearPossibleMoves()
    {
        for(int i = 0; i < 8*8; i++) _possibleMoves[i] = 0;
    }

    //SHOULD ONLY BE CALLED BY PIECES AND BOARD
    public void SetPossibleMove(int cellX, int cellY, int value)
    {
        if(cellX < 0 || cellX >= 8 || cellY < 0 || cellY >= 8) return;
        _possibleMoves[cellX + cellY * 8] = value;
    }

    public bool IsMovePossible(int cellX, int cellY)
    {
        if(cellX < 0 || cellX >= 8 || cellY < 0 || cellY >= 8) return false;
        return _possibleMoves[cellX + cellY * 8] == 1;
    }

    public bool IsInCheck(int team)
    {
        if (team < 0 || team > 1) return false;

        foreach (BasePiece piece in _pieces)
        {
            if (piece.Team == team) continue;       //A team can't checkmate itself
            
            //ClearPossibleMoves();
            piece.GetPossibleMoves();
            if (_possibleMoves[_kings[team].X + _kings[team].Y * 8] == 1)
            {
                ClearPossibleMoves();
                return true;
            }
        }


        ClearPossibleMoves();
        return false;
    }

    void PromoteIfPossible(int cellX, int cellY)
    {
        if(cellY < 0 || cellY >= 8 || cellX < 0 || cellX >= 8) return;
        BasePiece piece = GetPieceAt(cellX, cellY);
        if(piece == null || piece.Type != PieceType.Pawn) return;
        int team = piece.Team;
        
        if (cellY == 0 || cellY == 7)       // TODO : less naive check ?
        {
            RemovePieceAt(cellX, cellY);         //Remove pawn
            Queen promoted_piece = new Queen(this, team, cellX, cellY);
            _pieces.Add(promoted_piece);        //Place promoted piece
        }
    }
}
