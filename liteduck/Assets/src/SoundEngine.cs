using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEngine : MonoBehaviour 
{
    public GameObject ClipEmitter;
    public AudioSource MusicEmitter;
    public List<string> Names = new List<string>();
    public List<AudioClip> Clips = new List<AudioClip>();

    private static SoundEngine Instance;
    private AudioSource[] _sources;

    void Awake()
    {
        Instance = this;
        _sources = ClipEmitter.GetComponents<AudioSource>();
    }

    private void iPlayClip(string name)
    {
        if (Names.Contains(name))
        {
            for (int i = 0; i != _sources.Length; i++)
            {
                if (!_sources[i].isPlaying || i == _sources.Length-1)
                {
                    _sources[i].clip = Clips[Names.IndexOf(name)];
                    _sources[i].Play();
                    break;
                }
            }
        }
    }

    private void iPlayMusic(string name)
    {
        if (Names.Contains(name))
        {
            MusicEmitter.clip = Clips[Names.IndexOf(name)];
            MusicEmitter.loop = true;
            MusicEmitter.Play();
        }
    }

    public static void PlayClip(string name)
    {
        Instance.iPlayClip(name);
    }

    public static void PlayRandomClip(params string[] names)
    {
        Instance.iPlayClip(names[(int)Mathf.Round(Random.Range(0, names.Length))]);
    }

    public static void PlayMusic(string name)
    {
        Instance.iPlayMusic(name);
    }
}
