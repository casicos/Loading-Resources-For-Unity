using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource CurrentPlayingBackgroundMusicSource { get; private set; }

    public List<BackgroundMusic> BackgroundMusicGroup;
    
    protected override void Awake()
    {
        base.Awake();
        
        CurrentPlayingBackgroundMusicSource = GetComponent<AudioSource>();        
    }

    public void SetTrigger(int stage)
    {        
        CurrentPlayingBackgroundMusicSource.clip = BackgroundMusicGroup[stage].Clip;
        CurrentPlayingBackgroundMusicSource.Play();
        
//        RhythmManager.GetInstance.SetTrigger(BgmInfo);
    }

    /// <summary>
    /// Extended information for the bgm files.
    /// </summary>
    /// <exception cref="FileLoadException">File is not allocated.</exception>
    public class BackgroundMusic
    {
        public readonly string Id;
        public readonly float Bpm;
        public AudioClip Clip;
		
        public BackgroundMusic(string id, float bpm)
        {
            Id = id;
            Bpm = bpm;         
            Clip = Resources.Load<AudioClip>(Loader.BackgroundMusicSourcePath + Id);

            if (Clip == null)
            {
                throw new FileLoadException();
            }
        }
    }
    
}