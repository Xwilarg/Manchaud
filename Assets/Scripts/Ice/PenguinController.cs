using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PenguinController : MonoBehaviour
{
    private Rigidbody2D rb;
    private const float speed = 120f;
    [SerializeField]
    private AudioClip[] movSounds;
    [SerializeField]
    private Rigidbody2D ice;
    [SerializeField]
    private GameObject gameOverPanel;
    private bool isAlive;
    [SerializeField]
    private RuntimeAnimatorController controllerRight, controllerBotRight, controllerBot, controllerBotLeft, controllerLeft, controllerTopLeft, controllerTop, controllerTopRight;
    [SerializeField]
    private RuntimeAnimatorController deathRight, deathBotRight, deathBot, deathBotLeft, deathLeft, deathTopLeft, deathTop, deathTopRight;
    private Animator animator;

    [SerializeField]
    private bool killable;

    private float? gameOverTimer = null;

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
            if (gameOverTimer != null)
            {
                gameOverTimer -= Time.deltaTime;
                if (gameOverTimer < 0f)
                {
                    gameOverTimer = null;
                    gameOverPanel.SetActive(true);
                }
            }
            rb.velocity = Vector2.zero;
            return;
        }
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(hor * speed * Time.deltaTime, ver * speed * Time.deltaTime);
        walkTimer -= Time.deltaTime;
        if (walkTimer < 0f && Mathf.Abs(hor) > float.Epsilon && Mathf.Abs(ver) > float.Epsilon)
        {
            source.clip = movSounds[Random.Range(0, movSounds.Length)];
            source.Play();
            walkTimer = source.clip.length + 0.2f;
        }
        AnimateDirection(hor, ver);
        Slide();
    }

    private void AnimateDirection(float hor, float ver)
    {
        if (hor > 0.5f && (ver > -0.5f && ver < 0.5f)) // Right
            animator.runtimeAnimatorController = controllerRight;
        else if ((hor < 0.5f && hor > -0.5f) && ver < -0.5f) // Bottom
            animator.runtimeAnimatorController = controllerBot;
        else if (hor > 0.5f && ver < -0.5f) // Bottom-Right
            animator.runtimeAnimatorController = controllerBotRight;
        else if (hor < -0.5f && ver < -0.5f) // Bottom-Left
            animator.runtimeAnimatorController = controllerBotLeft;
        else if (hor < -0.5f && (ver > -0.5f && ver < 0.5f)) // Left
            animator.runtimeAnimatorController = controllerLeft;
        else if (hor < -0.5f && ver > 0.5f) // Top-Left
            animator.runtimeAnimatorController = controllerTopLeft;
        else if ((hor > -0.5f && hor < 0.5f) && ver > 0.5f) // Top
            animator.runtimeAnimatorController = controllerTop;
        else if (hor > 0.5f && ver > 0.5f) // Top-Right;
            animator.runtimeAnimatorController = controllerTopRight;
    }

    private void Slide()
    {
        rb.velocity += new Vector2(-ice.transform.rotation.z * 10, 0);
    }

    public void Sink()
    {
        isAlive = false;
        gameOverTimer = 1f;

        if (animator.runtimeAnimatorController == controllerRight) animator.runtimeAnimatorController = deathRight;
        if (animator.runtimeAnimatorController == controllerBot) animator.runtimeAnimatorController = deathBot;
        if (animator.runtimeAnimatorController == controllerBotRight) animator.runtimeAnimatorController = deathBotRight;
        if (animator.runtimeAnimatorController == controllerBotLeft) animator.runtimeAnimatorController = deathBotLeft;
        if (animator.runtimeAnimatorController == controllerLeft) animator.runtimeAnimatorController = deathLeft;
        if (animator.runtimeAnimatorController == controllerTopLeft) animator.runtimeAnimatorController = deathTopLeft;
        if (animator.runtimeAnimatorController == controllerTop) animator.runtimeAnimatorController = deathTop;
        if (animator.runtimeAnimatorController == controllerTopRight) animator.runtimeAnimatorController = deathTopRight;
    }

    public void SetAlive(bool state)
    {
        if (!killable)
            return;
        isAlive = state;
        if (!isAlive)
        {
            animator.runtimeAnimatorController = null;
            gameOverTimer = 1f;
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
