using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip lowCold, mediumCold, highCold;
    [SerializeField]
    private AudioClip lowHot, mediumHot, highHot;

    private AudioSource[] sources;

    private void Start()
    {
        sources = GetComponents<AudioSource>();
    }

    public enum Temp
    {
        Cold,
        Hot
    }

    public enum Size
    {
        Low,
        Medium,
        High
    }

    public void Play(Size size, float volume)
    {
        SetVolume(size, volume);
        sources[0].Play();
        sources[1].Play();
    }
    
    public void SetVolume(Size size, float volume)
    {
        if (size == Size.High)
            sources[0].clip = highCold;
        else if (size == Size.Medium)
            sources[0].clip = mediumCold;
        else
            sources[0].clip = lowCold;
        sources[0].volume = 1f - volume;
        if (size == Size.High)
            sources[1].clip = highHot;
        else if (size == Size.Medium)
            sources[1].clip = mediumHot;
        else
            sources[1].clip = lowHot;
        sources[1].volume = volume;
    }
}
