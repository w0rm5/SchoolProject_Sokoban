using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private void Awake()
    {
        foreach(Sound sound in sounds)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = sound.soundClip;
            audioSource.volume = sound.volume;
            audioSource.pitch = sound.pitch;
            audioSource.loop = sound.isLoop;
            sound.source = audioSource;
        }
    }
    public void Play(string name)
    {
        Sound sound = sounds.Where(s => s.name == name).FirstOrDefault();
        if (sound != null)
        {
            sound.Play();
        }
    }
}
