using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Room : MonoBehaviour {
    [SerializeField]
    private bool stairs;
    [SerializeField]
    private Room up;
    [SerializeField]
    private Room down;
    [SerializeField]
    private Room left;
    [SerializeField]
    private Room right;

    public bool HasStairs()
    {
        return stairs;
    }

    public Room GetUp()
    {
        return up;
    }

    public Room GetDown()
    {
        return down;
    }

    public Room GetLeft()
    {
        return left;
    }

    public Room GetRight()
    {
        return right;
    }
}
