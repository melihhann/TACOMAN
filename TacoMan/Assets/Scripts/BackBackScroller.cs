using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBackScroller : MonoBehaviour {

	public float speed;
	Vector3 startPOS;
    private float speedrate = 0.1f;


    void Start () 
	{
		startPOS = transform.position;	
	}
	  
	void Update()
	{
		transform.Translate((new Vector3(-1, 0, 0)) * speed * Time.deltaTime);


		if (transform.position.x < -44.82615)   
			transform.position = startPOS;   

		if (Time.timeScale % 1 == 0)
		{
            speed = speed + (speedrate / speed);
        } 
	}
}