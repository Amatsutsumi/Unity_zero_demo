using UnityEngine;

public class GameInputManager : MonoSingle<GameInputManager>
{
    private GameInputSystem gameInputSystem;
    public Vector2 move => gameInputSystem.GameInputAction.Move.ReadValue<Vector2>();
    public Vector2 Camera =>gameInputSystem.GameInputAction.Camera.ReadValue<Vector2>();
    public bool leftFire => gameInputSystem.GameInputAction.LeftFire.triggered;
    public bool RightFire => gameInputSystem.GameInputAction.RightFire.triggered;
    //Õë¶ÔËøµÐÐ´µÄ
    private bool _lock = false;
    public bool Lock
    {
        get
        {
            if (gameInputSystem.GameInputAction.Lock.triggered)
                _lock = !_lock;
            return _lock;
              
        }
    }

    public bool Parry => gameInputSystem.GameInputAction.Parry.triggered;
    public bool execute => gameInputSystem.GameInputAction.execute.triggered;
    public bool roll =>gameInputSystem.GameInputAction.roll.triggered;
    public bool speed => gameInputSystem.GameInputAction.roll.IsPressed();
    private void OnEnable()
    {
        gameInputSystem ??=new GameInputSystem();
        gameInputSystem.Enable();
    }

    private void OnDisable()
    {
        gameInputSystem.Disable();
    }
}
