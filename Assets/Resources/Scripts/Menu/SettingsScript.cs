using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour {

    public GameObject settingsButtons;
    public bool settingsButtonsActive = false;

    public GameObject menuButtons;

	// Use this for initialization
	void Start () {
        settingsButtons.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SettingsActivation()
    {
        if(settingsButtonsActive == false)
        {
            settingsButtonsActive = true;
            settingsButtons.SetActive(true);
            menuButtons.SetActive(false);
        }
        else if(settingsButtonsActive == true)
        {
            settingsButtonsActive = false;
            settingsButtons.SetActive(false);
            menuButtons.SetActive(true);
        }
    }
}
