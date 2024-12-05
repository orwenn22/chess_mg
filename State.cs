using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace chess_mg;

public class State
{
    private Game1 _game;
    
    public State(Game1 game)
    {
        _game = game;
    }
    
    
    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {}
    
    public Game1 Game => _game;
}
