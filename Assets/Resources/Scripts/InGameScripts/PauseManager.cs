using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public static bool paused = false;
    public GameObject pause;
    public GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        Resume();
    }  
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseManage();
        }
	}
    public void PauseManage()
    {
        if (paused == false && gm.dontUPauseIt == false)
        {
            gm.stopTime = true;
            Time.timeScale = 0f;
            paused = true;
            pause.SetActive(true);
        }
        else if (paused == true)
        {
            gm.stopTime = false;
            Time.timeScale = gm.tempoMeter * gm.actualTimeModulations;
            paused = false;
            pause.SetActive(false);
        }
    }
    public void Resume()
    {
        Time.timeScale = gm.tempoMeter * gm.actualTimeModulations;
        paused = false;
        pause.SetActive(false);
    }
    public void Pause()
    {
        gm.stopTime = true;
        Time.timeScale = 0f;
        paused = true;
        pause.SetActive(true);
    }
}
