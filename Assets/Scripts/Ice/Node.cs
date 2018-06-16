using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private Node next;

    public Node GetNextNode()
    {
        return (next);
    }
}
