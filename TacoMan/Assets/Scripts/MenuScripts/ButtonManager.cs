using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    public string levelToLoad;
    
    void Update()
    {
        if (Input.GetButtonDown("Vault") || Input.GetButtonDown("Duck"))
            SceneManager.LoadScene(levelToLoad);
        if (Input.GetButtonUp("Cancel"))
            Application.Quit();
    }

	public void startGame()
	{
		SceneManager.LoadScene("aga");  
	}
}
