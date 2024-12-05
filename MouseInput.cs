using Microsoft.Xna.Framework.Input;

namespace chess_mg;

public class MouseInput
{
    //Static stuff
    
    private static MouseInput _instance;        //singleton
    static public MouseInput The => _instance;  //for accessing singleton

    static public void InitSingleton()          //for initialising singleton
    {
        _instance = new MouseInput();
    }
    
    
    //Class stuff
    private bool _leftPressed;
    private bool _leftDown;
    
    private MouseInput()
    {
        _leftPressed = false;
        _leftDown = false;
    }

    public void Update()
    {
        MouseState mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if(!_leftDown) _leftPressed = true;
            else _leftPressed = false;
            
            _leftDown = true;
        }
        else
        {
            _leftPressed = false;
            _leftDown = false;
        }
        
        //if(_leftPressed) System.Console.WriteLine("OMG");
    }
    
    public bool LeftPressed => _leftPressed;
    public bool LeftDown => _leftDown;
}