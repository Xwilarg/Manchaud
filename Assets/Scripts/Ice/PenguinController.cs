﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PenguinController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private AudioClip[] movSounds;
    [SerializeField]
    private Rigidbody2D ice;

    private AudioSource source;
    private float walkTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        walkTimer = -1f;
    }

    private void Update()
    {
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
        Slide();
    }

    private void Slide()
    {
        rb.velocity = new Vector2(-ice.transform.rotation.z * 10, rb.velocity.y);
    }
}
