using UnityEngine;

public class MoveHistory
{
    public Vector2 PlayerPosition { get; set; }
    public Vector2 BoxPosition { get; set; }
    public GameObject BoxReference { get; set; }
    public MoveHistory()
    {

    }
    public MoveHistory(Vector2 PlayerPosition, Vector2 BoxPosition, GameObject BoxReference)
    {
        this.PlayerPosition = PlayerPosition;
        this.BoxPosition = BoxPosition;
        this.BoxReference = BoxReference;
    }
}
