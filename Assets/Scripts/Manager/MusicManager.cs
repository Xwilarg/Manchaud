using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip lowCold, mediumCold, highCold;
    [SerializeField]
    private AudioClip lowHot, mediumHot, highHot;
    [SerializeField]
    private AudioSource[] sources;

    private int SourceAlternance = 0;

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
        SetVolume(size, volume, true);
        sources[0].Play();
        sources[1].Play();
    }
    
    public void SetVolume(Size size, float volume, bool change = false)
    {
        if (change)
        {
            // Set cold size
            if (size == Size.High)
                CrossFade(0.5f, sources[SourceAlternance], sources[SourceAlternance + 1], highCold);
            else if (size == Size.Medium)
                CrossFade(0.5f, sources[SourceAlternance], sources[SourceAlternance + 1], mediumCold);
            else
                CrossFade(0.5f, sources[SourceAlternance], sources[SourceAlternance + 1], lowCold);

            // Set hot size
            if (size == Size.High)
                CrossFade(0.5f, sources[SourceAlternance + 2], sources[(SourceAlternance + 3) % 4], highHot);
            else if (size == Size.Medium)
                CrossFade(0.5f, sources[SourceAlternance + 2], sources[(SourceAlternance + 3) % 4], mediumHot);
            else
                CrossFade(0.5f, sources[SourceAlternance + 2], sources[(SourceAlternance + 3) % 4], lowHot);

            // Update counter
            SourceAlternance = (SourceAlternance + 1) % 2;
        }
        // Intensity gauge is  = 1- volume
        sources[SourceAlternance].volume = 1f - volume;
        // Gauge intensity is = volume
        sources[SourceAlternance + 2].volume = volume;
    }

    private void CrossFade(float duration, AudioSource audioOut, AudioSource audioIn, AudioClip audioFileIn)
    {
        FadeOut(duration, audioOut);
        audioIn.clip = audioFileIn;
        FadeIn(duration, audioIn);
    }

    private void FadeOut(float duration, AudioSource audioOut)
    {
        // Linearly Fade Out audio
        while (audioOut.volume > 0.1)
        {
            audioOut.volume = Mathf.Lerp(audioOut.volume, 0.0f, duration * Time.deltaTime);
        }
        // Set volume to 0 before cutting the AudioSource
        audioOut.volume = 0.0f;
        audioOut.Stop();
    }

    private void FadeIn(float duration, AudioSource audioIn)
    {
        audioIn.Play();
        // Linearly Fade In audio
        while (audioIn.volume < 0.9)
        {
            audioIn.volume = Mathf.Lerp(audioIn.volume, 1.0f, duration * Time.deltaTime);
        }
        // Set volume to 1 to finish the FadeIn
        audioIn.volume = 1.0f;
    }
}