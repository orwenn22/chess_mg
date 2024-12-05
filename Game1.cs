using chess_mg.PlayerControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace chess_mg;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    public Texture2D atlas;
    public SpriteFont font;

    private State _state;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        MouseInput.InitSingleton();
        
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = 64 * 8;
        _graphics.PreferredBackBufferHeight = 64 * 8;
        _graphics.ApplyChanges();

        _state = new MainMenu(this);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        atlas = Content.Load<Texture2D>("atlas");
        font = Content.Load<SpriteFont>("TestFont");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        MouseInput.The.Update();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _state.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _state.Draw(gameTime, _spriteBatch);
        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }


    public void SetState(State state)
    {
        _state = state;
    }
}
