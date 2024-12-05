using System;
using Microsoft.Xna.Framework.Input;

namespace chess_mg.PlayerControllers;

/**
 * Used when a player is playing locally.
 * If the game is fully local then there is two instances of this.
 */
public class LocalPlayerController : BasePlayerController
{
    public LocalPlayerController() { }
    
    public override void OnUpdate(int currentTeam)
    {
        //base.OnUpdate();
        MouseState mouseState = Mouse.GetState();
        if (MouseInput.The.LeftPressed)
        {
            BoardClickStatus s = GetBoard.OnCellClick(
                mouseState.X / 64 - ((mouseState.X < 0) ? 1 : 0), 
                mouseState.Y / 64  - ((mouseState.Y < 0) ? 1 : 0));
            Console.WriteLine(s);
        }
    }
}
