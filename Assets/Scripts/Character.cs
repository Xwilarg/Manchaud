using UnityEngine;

public class Character : MonoBehaviour {

    public BoxCollider2D Collider
    {
        get
        {
            return GetComponent<Collider>();
        }

        set
        {
            collider = value;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }

    // Use this for initialization
    private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
}
