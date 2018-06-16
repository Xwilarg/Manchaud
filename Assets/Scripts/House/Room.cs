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
    [SerializeField]
    private SwitchLight radiator;
    [SerializeField]
    private SwitchLight[] doors;

    private float temperature;

    private void Start()
    {
        temperature = 23f;
    }

    private void Update()
    {
        if (radiator == null)
            return;
        if (radiator.IsOn())
        {
            temperature += 3f * Time.deltaTime;
            if (temperature > 25f)
                radiator.SwitchOff();
        }
        else
        {
            if (temperature < 20f)
                radiator.SwitchOn();
        }
        foreach (SwitchLight door in doors)
        {
            if (door.IsOn())
                temperature -= 2.5f * Time.deltaTime;
        }
    }

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
