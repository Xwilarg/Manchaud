﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private Room room, oldRoom;
    [SerializeField]
    private float speed;
    [SerializeField]
    private RuntimeAnimatorController controllerLeft, controllerRight;
    private Animator animator;
    private Directions direction;
    private enum Directions { LEFT, RIGHT }
    [SerializeField]
    private Vector3 firstFloorOffset;
    [Range(0, 100)]
    [SerializeField]
    private int switchOnRate;
    [SerializeField]
    private AudioClip[] walkingClips;
    private AudioSource audioSrc;
    private float timer;
    [SerializeField]
    private float waitingTime;
    [Range(0, 100)]
    [SerializeField]
    private int takeStairsRate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SwitchLight sl = other.GetComponent<SwitchLight>();
        if (other.CompareTag("Room"))
        {
            Stop();
            oldRoom = room;
            room = other.GetComponent<Room>();
            timer = 0f;

            SwitchOnDevices();
            TakeStairs();
        }
        else if (sl != null && sl.IsDoor())
        {
            sl.SwitchOn();
        }
    }

    private void SwitchOnDevices()
    {
        Transform[] devices = room.GetComponentsInChildren<Transform>();
        foreach (Transform device in devices)
        {
            if (Random.Range(1, 100) < switchOnRate)
            {
                SwitchLight sl = device.GetComponent<SwitchLight>();
                if (sl != null && sl.GetObject() != SwitchLight.Object.RADIATOR)
                    sl.SwitchOn();
            }
        }
    }

    private void TakeStairs()
    {
        if (Random.Range(0, 100) > takeStairsRate)
            return;
        if (room.HasStairs())
        {
            if (room.GetDown() != null && room.GetDown() != oldRoom)
                rb.MovePosition(room.GetDown().transform.position - firstFloorOffset);
            else if (room.GetUp() != null && room.GetUp() != oldRoom)
                rb.MovePosition(room.GetUp().transform.position);
        }
    }

    private void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        direction = Directions.RIGHT;
        audioSrc = GetComponent<AudioSource>();
        timer = 1f;
	}

    public void Footsteps()
    {
        audioSrc.PlayOneShot(walkingClips[Random.Range(0, 5)]);
    }

    private void Update ()
    {
        if (Wait())
            Move();
    }

    private bool Wait()
    {
        timer += Time.deltaTime;
        return timer >= waitingTime;
    }

    private void Move()
    {
        if (direction != Directions.LEFT && room.GetLeft() != null)
        {
            direction = Directions.LEFT;
            animator.runtimeAnimatorController = controllerLeft;
            rb.velocity = new Vector2(-speed * Time.deltaTime, 0); // speed < 0 == left
        }
        else if (direction != Directions.RIGHT && room.GetRight() != null)
        {
            direction = Directions.RIGHT;
            animator.runtimeAnimatorController = controllerRight;
            rb.velocity = new Vector2(speed * Time.deltaTime, 0); // speed > 0 == right
        }
    }

    private void Stop()
    {
        rb.velocity = new Vector2(0, 0);
        animator.runtimeAnimatorController = null;
    }
}
