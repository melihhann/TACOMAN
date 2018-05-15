using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [HideInInspector] private GameObject[] obstacles = new GameObject[10] ;    
    public GameObject umbrello;
    public GameObject bench;
    public GameObject barrel;
    public GameObject trolley;
    public GameObject hydrant;
    public GameObject trafficLamp;
    public GameObject box;
    public GameObject cart;
    public GameObject signPost;
    public GameObject jynx;
    private int xVector = 14;
    private Vector2 originPosition;
    public float speed = 1f;
    private float speedrate = 0.1f;
    private GameObject spawn;
    private GameObject spawn2;
    private GameObject spawn3;
    private float[] objectsVertPos = new float[10];
    int objectToSpawn;
    
    
    


	// Use this for initialization
	void Start () {

        obstacles[0] = umbrello;
        obstacles[1] = bench;
        obstacles[2] = barrel;
        obstacles[3] = trolley;
        obstacles[4] = hydrant;
        obstacles[5] = trafficLamp;
        obstacles[6] = box;
        obstacles[7] = cart;
        obstacles[8] = signPost;
        obstacles[9] = jynx;
        objectsVertPos[0] = 2.817862f;
        objectsVertPos[1] = 0.913439f;
        objectsVertPos[2] = 0.931405f;
        objectsVertPos[3] = 0.913442f;  
        objectsVertPos[4] = 0.918036f;
        objectsVertPos[5] = 2.035145f;
        objectsVertPos[6] = 0.913442f;
        objectsVertPos[7] = 0.913442f;
        objectsVertPos[8] = 2.314808f;
        objectsVertPos[9] = 2.081247f;


        objectToSpawn = Random.Range(0, 10);
        originPosition = transform.position;
        spawn = Instantiate(obstacles[objectToSpawn], new Vector2(35f, objectsVertPos[objectToSpawn]), Quaternion.identity);
        objectToSpawn = Random.Range(0, 10);
        spawn2 = Instantiate(obstacles[objectToSpawn], new Vector2(Random.Range(35f, 40f), objectsVertPos[objectToSpawn]), Quaternion.identity);
        objectToSpawn = Random.Range(0, 10);
        spawn3 = Instantiate(obstacles[objectToSpawn], new Vector2(Random.Range(35f, 40f), objectsVertPos[objectToSpawn]), Quaternion.identity);
		spawn.tag="Obstacle";
		spawn2.tag="Obstacle";
		spawn3.tag="Obstacle";

    }
		
    void Spawn()
    {
        
       
        /*
        for(int i = 0; i < maxPlatforms; i++)// Engele carptigi zaman platform yaratmayi bitireceksin ve endgame logosu cikacak
        {    
            Vector2 platformPosition = originPosition + new Vector2(xVector, 0);
            Vector2 cloudPosition = originPosition + new Vector2(1, 0);
            Vector2 buildingPosition = originPosition + new Vector2(1, 0); 
		

            Instantiate(platform, platformPosition, Quaternion.identity);
            Instantiate(platform, cloudPosition, Quaternion.identity);
            Instantiate(platform, buildingPosition, Quaternion.identity);
            originPosition = platformPosition;  
        }
        */

    }
    void Update()
    {

        
        spawn.transform.Translate(new Vector2(-1 * speed * Time.deltaTime, 0));       
        if (Time.timeScale % 1 == 0)
        {
            speed = speed + (speedrate/speed);
        }
        if (spawn.transform.position.x < -22.600)
        {
            Destroy(spawn, 0f);
            objectToSpawn = Random.Range(0, 9);
            spawn = Instantiate(obstacles[objectToSpawn], new Vector2(Random.Range(35f,40f), objectsVertPos[objectToSpawn]), Quaternion.identity);
            spawn.tag = "Obstacle";
        }
        if (speed > 15f)
        {               
            spawn2.transform.Translate(new Vector2(-1 * speed * Time.deltaTime, 0));
            if (spawn2.transform.position.x < -22.600)
            {
                Destroy(spawn2, 0f);
                objectToSpawn = Random.Range(0, 9);
                spawn2 = Instantiate(obstacles[objectToSpawn], new Vector2(Random.Range(35f, 40f), objectsVertPos[objectToSpawn]), Quaternion.identity);
                spawn2.tag = "Obstacle";
            }
        }
        
        if (speed > 20f)
        {            
            spawn3.transform.Translate(new Vector2(-1 * speed * Time.deltaTime, 0));
            if (spawn3.transform.position.x < -22.600)
            {
                Destroy(spawn3, 0f);
                objectToSpawn = Random.Range(0, 9);
                spawn3 = Instantiate(obstacles[objectToSpawn], new Vector2(Random.Range(35f, 40f), objectsVertPos[objectToSpawn]), Quaternion.identity);
                spawn3.tag = "Obstacle";
            }
        }
        

    }

}
