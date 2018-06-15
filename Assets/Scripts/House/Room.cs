using UnityEngine;

public class Room : MonoBehaviour {
    [SerializeField]
    private bool stairs;

    public Room Up { get; }
    public Room Down { get; }
    public Room Left { get; }
    public Room Right { get; }

    public bool HasStairs()
    {
        return stairs;
    }
}
