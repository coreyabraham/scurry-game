/// <summary>
/// An Enumeration that dictates what Type of Animation is being currently used to any Script that reads it. <br/>
/// (Used by: `PlayerSystem.cs`)
/// </summary>
public enum AnimType
{
    Custom = 0,
    HorizontalSpeed,
    VerticalSpeed,
    Jumping,
    Climbing,
    Scurrying,
    Sliding,
    Pushing,
    Pulling,
    Idle,
    Burned,
    Drowned,
    Prayed,
    XVelocity
}