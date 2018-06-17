using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class SwitchLight : MonoBehaviour
{
    [SerializeField]
    private Sprite spriteOn, spriteOff;
    [SerializeField]
    private Sprite[] additonalSprites;
    [SerializeField]
    private float[] additonalStats;
    private bool isOn;
    private SpriteRenderer currSprite;
    [SerializeField]
    private Type type;
    enum Type { FRIDGE, LIGHT, RADIATOR, DOOR, COMPUTER }
    private AudioSource audioSrc;
    [SerializeField]
    private AudioClip clipOn, clipOff;
    [SerializeField]
    private Object obj;
    private float secConso;
    private float currConso;
    private Room room;

    [SerializeField]
    private Sprite[] numbers;
    private int currNb;

    [SerializeField]
    private SpriteRenderer childSprite;

    private PenguinController player;

    private float rawConso;

    public Object GetObject() { return (obj); }
    public bool IsOn() { return (isOn); }

    public enum Object
    {
        NOCONSO,
        FRIDGE,
        LIGHHIGH,
        LIGHTLOW,
        COMPUTER,
        RADIATOR
    }

    private void Start()
    {
        currNb = 0;
        rawConso = 0f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinController>();
        room = transform.parent.GetComponent<Room>();
        currSprite = GetComponent<SpriteRenderer>();
        isOn = (currSprite.sprite == spriteOn);
        audioSrc = GetComponent<AudioSource>();
        const float multiplicator = 2f;
        switch (obj)
        {
            case Object.COMPUTER:
                secConso = 0.05f * multiplicator;
                break;

            case Object.FRIDGE:
                secConso = 0.112f * multiplicator;
                break;

            case Object.LIGHHIGH:
            case Object.RADIATOR:
                secConso = 0.076f * multiplicator;
                break;

            case Object.LIGHTLOW:
                secConso = 0.025f * multiplicator;
                break;

            default:
                secConso = 0f;
                break;
        }
    }

    public float GetConso()
    {
        if (obj == Object.RADIATOR)
            return (secConso + (rawConso * Time.deltaTime * 2f));
        if (isOn)
            return (secConso);
        return (0f);
    }

    private void Update()
    {
        if (isOn)
        {
            currConso += Time.deltaTime * secConso;
            rawConso += Time.deltaTime * secConso;
        }
        else
            rawConso = 0f;
    }

    private void SetSprite(string state)
    {
        if (state == "on")
        {
            if (childSprite != null && numbers.Length != 0)
            {
                currNb = numbers.Length - 1;
                childSprite.sprite = numbers[currNb];
            }
            if (obj != Object.RADIATOR && obj != Object.NOCONSO)
                room.AddLamp();
            int i = 0;
            foreach (Sprite s in additonalSprites)
            {
                if (Random.Range(0, 100) < additonalStats[i])
                {
                    currSprite.sprite = s;
                    return;
                }
                i++;
            }
        }
        else if (obj != Object.RADIATOR && obj != Object.NOCONSO)
            room.RemoveLamp();
        currSprite.sprite = state == "on" ? spriteOn : spriteOff;
    }

    private void OnMouseDown()
    {
        if (obj != Object.RADIATOR && player.IsAlive())
        {
            if (currNb > 0)
            {
                currNb--;
                childSprite.sprite = numbers[currNb];
            }
            else if (isOn)
            {
                if (childSprite != null)
                    childSprite.sprite = null;
                SwitchOff();
            }
            else
                SwitchOn();
        }
    }

    public void SwitchOn()
    {
        if (!isOn)
        {
            isOn = true;
            SetSprite("on");
            audioSrc.PlayOneShot(clipOn);
        }
    }

    public void SwitchOff()
    {
        if (isOn)
        {
            isOn = false;
            SetSprite("off");
            audioSrc.PlayOneShot(clipOff);
        }
    }

    public bool IsDoor()
    {
        return type == Type.DOOR;
    }
}
