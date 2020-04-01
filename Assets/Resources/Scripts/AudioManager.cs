using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {


    public Sound[] sounds;
    public AudioSource music;
    bool bossMusic = false;
    int musicNum;
    GameMaster gm;

    public GalaxyTracks[] galaxyTracks;

    [System.Serializable]
    public class GalaxyTracks
    {
        public AudioClip[] musicClips;
    }

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
    }
    void Start()
    { 
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        musicNum = UnityEngine.Random.Range(0, 4);
        music.clip = Resources.Load<AudioClip>("Sounds/Music/Clip" + Informations.statistics[0] + "" + UnityEngine.Random.Range(0, 4));
        music.Play();
    }
    void Update()
    {
       if(!music.isPlaying && !gm.bossHealth.activeSelf)
        {
            musicNum += 1;
            if (musicNum >= 4)
            {
                musicNum = 0;
            }
            music.clip = Resources.Load<AudioClip>("Sounds/Music/Clip" + Informations.statistics[0] +""+ musicNum);
            music.Play();
        }
       else if(gm.bossHealth.activeSelf == true && bossMusic == false)
        {
            music.clip = Resources.Load<AudioClip>("Sounds/Music/Clip04");
            music.Play();
            bossMusic = true;
        }
    }
    public void Play (string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not Found!");
            return;
        }
       s.source.Play();
         
    }
}
