using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource source;

    public AudioClip soundClip;
    public string name;
    public bool isLoop;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public void Play()
    {
        if (source != null)
        {
            source.Play();
        }
    }
}
