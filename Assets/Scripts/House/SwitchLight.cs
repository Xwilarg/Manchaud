using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SwitchLight : MonoBehaviour {

    [SerializeField]
    private Sprite spriteOn, spriteOff;
    private bool isOn;
    private SpriteRenderer currSprite;

    private void Start()
    {
        currSprite = GetComponent<SpriteRenderer>();
        isOn = (currSprite.sprite == spriteOn);
    }

    private void OnMouseDown()
    {
        isOn = !isOn;
        currSprite.sprite = (isOn) ? (spriteOn) : (spriteOff);
    }
}
