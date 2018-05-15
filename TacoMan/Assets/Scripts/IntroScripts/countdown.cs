using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class countdown : MonoBehaviour
{
    public string levelToLoad;
    private float timer = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0||Input.GetButtonDown("Skip"))
            SceneManager.LoadScene(levelToLoad);
        if (Input.GetButtonUp("Cancel"))
            Application.Quit();
       
        
	}
}
