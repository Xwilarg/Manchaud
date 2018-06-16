using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PenguinController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private AudioClip[] movSounds;
    [SerializeField]
    private Rigidbody2D ice;
    [SerializeField]
    private GameObject gameOverPanel;
    private bool isAlive;
    [SerializeField]
    private RuntimeAnimatorController controllerRight, controllerBotRight, controllerBot, controllerBotLeft, controllerLeft, controllerTopLeft, controllerTop, controllerTopRight;
    private Animator animator;

    private AudioSource source;
    private float walkTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        walkTimer = -1f;
        isAlive = true;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAlive)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(hor * speed * Time.deltaTime, ver * speed * Time.deltaTime);
        walkTimer -= Time.deltaTime;
        if (walkTimer < 0f && Mathf.Abs(hor) > float.Epsilon && Mathf.Abs(ver) > float.Epsilon)
        {
            source.clip = movSounds[UnityEngine.Random.Range(0, movSounds.Length)];
            source.Play();
            walkTimer = source.clip.length + 0.2f;
        }
        if (hor > 0.5f && (ver > -0.5f && ver < 0.5f)) // Right
            animator.runtimeAnimatorController = controllerRight;
        else if (hor > 0.5f && ver < -0.5f) // Bottom-Right
            animator.runtimeAnimatorController = controllerBotRight;
        else if ((hor < 0.5f && hor > -0.5f) && ver < -0.5f)
            animator.runtimeAnimatorController = controllerBot;
        else if (hor < -0.5f && ver < -0.5f)
            animator.runtimeAnimatorController = controllerBotLeft;
        else if (hor < -0.5f && (ver > -0.5f && ver < 0.5f)) // Left
            animator.runtimeAnimatorController = controllerLeft;
        else if (hor < -0.5f && ver > 0.5f)
            animator.runtimeAnimatorController = controllerTopLeft;
        else if ((hor < -0.5f && hor > 0.5f) && ver > 0.5f)
            animator.runtimeAnimatorController = controllerTop;
        else if (hor > 0.5f && ver > 0.5f)
            animator.runtimeAnimatorController = controllerTopRight;
        Slide();
    }

    private void Slide()
    {
        rb.velocity += new Vector2(-ice.transform.rotation.z * 10, 0);
    }

    public void SetAlive(bool state)
    {
        isAlive = state;
        if (!isAlive)
            gameOverPanel.SetActive(true);
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
