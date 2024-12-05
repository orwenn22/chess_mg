using chess_mg.PlayerControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace chess_mg;

public class MainMenu : State
{
    public MainMenu(Game1 game) : base(game)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        //base.Update(gameTime);
        KeyboardState keyboardState = Keyboard.GetState();
        if(keyboardState.IsKeyDown(Keys.L))
        {
            Game.SetState(new Board(Game, new LocalPlayerController(), new LocalPlayerController()));
        }
        else if (keyboardState.IsKeyDown(Keys.B))
        {
            Game.SetState(new Board(Game, new LocalPlayerController(), new BotPlayerController()));
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //base.Draw(gameTime, spriteBatch);
        spriteBatch.DrawString(Game.font, "L : local", new Vector2(10, 10), Color.White);
        spriteBatch.DrawString(Game.font, "B : bot", new Vector2(10, 23), Color.White);
        //spriteBatch.DrawString(Game.font, "H : host", new Vector2(10, 36), Color.White);
        //spriteBatch.DrawString(Game.font, "J : join", new Vector2(10, 49), Color.White);
    }
}