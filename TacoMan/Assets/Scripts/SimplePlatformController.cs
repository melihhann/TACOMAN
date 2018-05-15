using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class SimplePlatformController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    public Transform groundCheck;
    public float velo = 1f;//Hiz artisi   
    private bool grounded = false;
    public Animator anim;
    private Rigidbody2D rb2d;
    [HideInInspector] public bool vault = false;
    [HideInInspector] public bool duck = false;
    [HideInInspector] public bool isAlive = true;

	//Android
	public Button kirikButton;
	public Button sexyButton;

      

    public GameObject ArmsCollision;
    public GameObject TorsoCollision;
    public GameObject LegsCollision;
	public GameObject CanvasObject;

    private bool flag = false;

    private BoxCollider2D boxColArms;
    private BoxCollider2D boxColLegs;
    private bool vaulTimeUp = false;
    private bool collisionOccur = false;

    private AudioSource footSteps;
    private AudioClip[] FSAudioClip = new AudioClip[8];

    private AudioSource grunts;
    private AudioClip[] GruntsAudioClip = new AudioClip[5];

    private AudioSource bones;
    private AudioClip bonesAudioClip;

    private AudioSource intro;
    private AudioClip introAudioClip;

    private AudioSource loop;
    private AudioClip loopAudioClip;

    private AudioSource memeler;
    private AudioClip[] memelerAudioClip = new AudioClip[4];

    int randomNumberForAudios;//footstep ve grunt icin.
    int randomNumberForMemeler;
    private bool PermissionForFS = false;


	//SCORE
	public Text scoreText;
	private float startTime;

    // Use this for initialization  
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Obstacle")
            collisionOccur = true;
    }
    IEnumerator Start()
    {

		// SCORE
		startTime = Time.time;

        //AUDIO
        footSteps = (AudioSource)gameObject.AddComponent<AudioSource>();
        FSAudioClip[0] = (AudioClip)Resources.Load("Footsteps/FootStep1");
        FSAudioClip[1] = (AudioClip)Resources.Load("Footsteps/FootStep2");
        FSAudioClip[2] = (AudioClip)Resources.Load("Footsteps/FootStep3");
        FSAudioClip[3] = (AudioClip)Resources.Load("Footsteps/FootStep4");
        FSAudioClip[4] = (AudioClip)Resources.Load("Footsteps/FootStep5");
        FSAudioClip[5] = (AudioClip)Resources.Load("Footsteps/FootStep6");
        FSAudioClip[6] = (AudioClip)Resources.Load("Footsteps/FootStep7");
        FSAudioClip[7] = (AudioClip)Resources.Load("Footsteps/FootStep8");
        randomNumberForAudios = Random.Range(0, 8);
        footSteps.clip = FSAudioClip[randomNumberForAudios];
        footSteps.loop = true;
        footSteps.Play();

        intro = (AudioSource)gameObject.AddComponent<AudioSource>();
        introAudioClip = (AudioClip)Resources.Load("end");
        intro.clip = introAudioClip;
        intro.loop = false;
        

        loop = (AudioSource)gameObject.AddComponent<AudioSource>();
        loopAudioClip = (AudioClip)Resources.Load("ThemeSong");
        loop.clip = loopAudioClip;
        loop.loop = true;
        loop.Play();
        

        memeler = (AudioSource)gameObject.AddComponent<AudioSource>();
        memelerAudioClip[0] = (AudioClip)Resources.Load("Memeler/Meme1");
        memelerAudioClip[1] = (AudioClip)Resources.Load("Memeler/Meme2");
        memelerAudioClip[2] = (AudioClip)Resources.Load("Memeler/Meme3");
        memelerAudioClip[3] = (AudioClip)Resources.Load("Memeler/Meme4");
        randomNumberForMemeler = Random.Range(0, 4);
        memeler.clip = memelerAudioClip[randomNumberForMemeler];
        memeler.loop = true;
        memeler.Play();

        while (!PermissionForFS)
        {
            yield return new WaitForSeconds(footSteps.clip.length);
            randomNumberForAudios = Random.Range(0, 8);
            footSteps.clip = FSAudioClip[randomNumberForAudios];
            footSteps.Play();
            randomNumberForMemeler = Random.Range(0, 4);
            memeler.clip = memelerAudioClip[randomNumberForMemeler];
            memeler.Play();

			if (PermissionForFS) {
				footSteps.Stop ();
				memeler.Stop ();
			}
            
        }
    }
    void Awake()
    {
        Application.targetFrameRate = 60;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        boxColArms = ArmsCollision.GetComponent<BoxCollider2D>();
        boxColLegs = LegsCollision.GetComponent<BoxCollider2D>();

        grunts = (AudioSource)gameObject.AddComponent<AudioSource>();
        GruntsAudioClip[0] = (AudioClip)Resources.Load("Grunts/Grunt1");
        GruntsAudioClip[1] = (AudioClip)Resources.Load("Grunts/Grunt2");
        GruntsAudioClip[2] = (AudioClip)Resources.Load("Grunts/Grunt3");
        GruntsAudioClip[3] = (AudioClip)Resources.Load("Grunts/Grunt4");
        GruntsAudioClip[4] = (AudioClip)Resources.Load("Grunts/Grunt5");
        randomNumberForAudios = Random.Range(0, 5);
        grunts.clip = GruntsAudioClip[randomNumberForAudios];

        bones = (AudioSource)gameObject.AddComponent<AudioSource>();
        bonesAudioClip = (AudioClip)Resources.Load("Bones/bones");
        bones.clip = bonesAudioClip;

    }

    // Update is called once per frame
    void Update()
    {
               
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (((Input.GetButtonDown ("Vault")) && isAlive)) 
		{
			boxColLegs.enabled = false;
			vault = true;
			rb2d.gravityScale = 0f;
			Invoke ("timeUp", 1.5f);

		}

        if ((Input.GetButtonUp("Vault") || vaulTimeUp) && isAlive)
        {
            vault = false;           
            boxColLegs.enabled = true;
            rb2d.gravityScale = 1f;            
            cooldown();
			CancelInvoke ();
            if (!vault && !duck)
                Idle();
        }
        if (Input.GetButtonDown("Duck") && isAlive)
        {
            boxColArms.enabled = false;
            duck = true;
            bones.Play();

        }
        if (Input.GetButtonUp("Duck") && isAlive)
        {
            duck = false;            
            boxColArms.enabled = true;
            if (!vault && !duck)
                Idle();
        }

		//TODO
		if (duck == true && vault == true && isAlive)   
		{
			Invoke("timeUp", 1.0f);
		}

        if (Input.GetButtonUp("Cancel"))
        {
            SceneManager.LoadScene("titleMenu");
        }
        if (flag)
        {
            intro.Play();
            grunts.Play();
            flag = false;
        }

	
		//SCORE
		float t = Time.time - startTime;
		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f1");// f2 -> 2 basamak almasi icin

		if (isAlive == false) 
		{
			return;
		}
			

		if ((int)t / 60 > 0) {
			scoreText.text = " " +  minutes + "."+ seconds;	
		} else {
			scoreText.text = " " + seconds;
		}


    }

    void FixedUpdate()
    {
		
        if (duck && !vault)
        {
            anim.SetInteger("State", 1);
            //duck = false;
            Debug.Log("aga2");            
        }
        if (vault && !duck)
        {
            anim.SetInteger("State", 2);
            //vault = false;
            Debug.Log("aga3");
            footSteps.Stop();
        }
        if (duck && vault)
        {
            randomNumberForAudios = Random.Range(0, 4);
            grunts.clip = GruntsAudioClip[randomNumberForAudios];
            footSteps.Stop();
            anim.SetInteger("State", 3);
            Debug.Log("aga4");
        }
        if (collisionOccur)
        {
            isAlive = false; 
            anim.SetInteger("State", 4);
            flag = true;
            footSteps.Stop();
            memeler.Stop();
            loop.Stop();                        
            Invoke("endGame", 2);
            rb2d.gravityScale = 0f;            
            Destroy(LegsCollision);
            Destroy(TorsoCollision);
            Destroy(ArmsCollision);
			PermissionForFS = true;
            collisionOccur = false;

        }



    }

    void endGame()
    {
		scoreText.color = Color.white;
		Vector3 newPos = new Vector3();
		newPos = transform.position;
		newPos.x = 616f;  
		newPos.y = 454f;
		CanvasObject.transform.position = newPos;
		newPos = transform.localScale;
		newPos.x = 50f;
		newPos.y = 50f;
		CanvasObject.transform.localScale = newPos;
        SceneManager.LoadScene("EndGame");
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void timeUp()
    {
        vaulTimeUp = true;
    }
    void cooldown()
    {
        vaulTimeUp = false;
    }
    void Idle()
    {
        anim.SetInteger("State", 0);
    }


	public void kirikPointerDown()
	{
		if (isAlive)
		{
			boxColArms.enabled = false;
			duck = true;
			bones.Play();

		}
	}

	public void kirikPointerUp()
	{
		if (isAlive)
		{
			duck = false;            
			boxColArms.enabled = true;
			if (!vault && !duck)
				Idle();
		}
	}

	public void sexyPointerDown()
	{
		if (isAlive)   
		{
			boxColLegs.enabled = false;
			vault = true;
			rb2d.gravityScale = 0f;
			Invoke ("timeUp", 1f);

		}
	}

	public void sexyPointerUp()
	{
		if (isAlive)
		{
			vault = false;           
			boxColLegs.enabled = true;
			rb2d.gravityScale = 0.7f;            
			cooldown();
			CancelInvoke ();
			if (!vault && !duck)
				Idle();
		}
	}

}


