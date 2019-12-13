using UnityEngine.Audio;
using System;
using UnityEngine;

public class ARD_SoundManager : MonoBehaviour
{
    public ARD_Sound[] sounds;

    //FindObjectOfType<ARD_SoundManager>().Play("name");


    void Awake()
    {
        foreach(ARD_Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        ARD_Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

}
