using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class AudioManagerMenu : MonoBehaviour
{
    public AudioSource music;
    public static AudioManagerMenu instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null && SceneManager.GetActiveScene().buildIndex != 1)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Destroy(this.gameObject);
        }
    }
}
