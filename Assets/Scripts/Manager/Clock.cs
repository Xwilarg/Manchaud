using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private float timer;
    private bool isPlaying;

    public enum KeyEvent { MovePlayer, }
    private Queue<KeyEvent> keyEvents;
    private Dictionary<string, bool> playingObjects;

	private void Start ()
    {
        isPlaying = false;
        keyEvents = new Queue<KeyEvent>();
        playingObjects = new Dictionary<string, bool>();
        playingObjects.Add("Orc", false);
        playingObjects.Add("Penguin", true);
        timer = 0f;
	}
	
	private void FixedUpdate ()
    {
        if (timer > 0)

        timer += Time.deltaTime;
    }

    public void Stop()
    {
        isPlaying = true;
    }

    public void Play()
    {
        isPlaying = false;
    }

    public bool IsPlaying(string type)
    {
        return playingObjects[type];
    }

    public KeyEvent GetKeyEvent()
    {
        return keyEvents.Dequeue();
    }
}
