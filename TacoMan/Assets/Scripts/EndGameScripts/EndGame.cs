using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	//destroyTacoman'i her yerde cagir
	void Update ()
    {
		if (Input.GetButtonDown ("Skip")) {
			SceneManager.LoadScene("titleMenu");
			destroyTacoman ();
		}
            
        if (Input.GetButtonDown("Cancel"))
            Application.Quit();
    }

	public void destroyTacoman()
	{
		Destroy (GameObject.Find ("hero 1"));
	}

	public void restartGame()
	{
		SceneManager.LoadScene("aga");
		destroyTacoman ();
	}

	public void quitGame()
	{
		Application.Quit();
	}  
		
}
