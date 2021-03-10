using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject losePanel;

    //scoring system vars
    private int playerScore;
    private float timer;

    //items and item states
    public string powerUpName;
    public bool hasPowerUp;
    public GameObject toad;
    public GameObject clouds;
    public bool timeSlowDownActive;

    //texts for ui display
    public Text scoreUI;
    public Text scoreUI_Lose;
    public Text highScore;
    public Text healthDisplay;

    //movement vars
    public float speed;
    private float input;

    //animation and sound vars
    Rigidbody2D rb;
    Animator anim;
    AudioSource source;
    public AudioClip narutoVSasukeScream;


    //player states and stats
    public float startDashTime;
    private float dashTime;
    public float extraSpeed;
    private bool isDashing;
    public int health;
    public bool isCloaked;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthDisplay.text = health.ToString();
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        playerScore = 0;
    }

    private void Update()
    {

        //checking health and keeping it in range
        LockMinMaxHealth();

        //Score Updating
        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            playerScore += 1;

            scoreUI.text = playerScore.ToString();

            //reset Timer
            timer = 0;
        }



        //Player Animations and Sprite Positioning

            //normal animations
        if (input != 0 && isCloaked == false)
        {
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsCloaked", false);
        }

        else if (input == 0 && isCloaked == false)
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsCloaked", false);
        }

            //cloaked animations

        if (input != 0 && isCloaked == true)
        {
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsCloaked", true);
        }

        else if (input == 0 && isCloaked == true)
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsCloaked", true);
        }

        if (input > 0)
        {
            transform.eulerAngles = new Vector3 (0, 0, 0);
        }

        else if (input < 0)
        {
            transform.eulerAngles = new Vector3 (0, 180, 0);
        }



        //Dashing Mechanic
        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false)
        {
            speed += extraSpeed;
            isDashing = true;
            dashTime = startDashTime;
        }

        if (dashTime <= 0 && isDashing == true)
        {
            isDashing = false;
            speed -= extraSpeed;

        }
        else
        {
            dashTime -= Time.deltaTime;
        }

        if (hasPowerUp == true && Input.GetKeyDown(KeyCode.Z))
        {
            usePowerUp(powerUpName);
        }

    }

    // Update is called once per frame
    // Fixed update used for rigidbody
    void FixedUpdate()
    {
        // Storing player's input
        input = Input.GetAxisRaw("Horizontal");

        // Moving player
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

    }

    public void TakeDamage(int damageAmount)
    {
        source.Play();
        health -= damageAmount;
        healthDisplay.text = health.ToString();

        if(health <= 0)
        {

            if(playerScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", playerScore);
                highScore.text = playerScore.ToString();
            }

            //DESTORYS PLAYER
            losePanel.SetActive(true);
            showScoreOnLose();
            Destroy(gameObject);
        }
    }

    public void usePowerUp(string name)
    {
            if (name == "scroll" && !GameObject.FindWithTag("Gamabunta"))
            {
              Vector3 pos = new Vector3 (0, 3, 0);    //added to player position

              //spawning gamabunta
              Instantiate(clouds, transform.position, Quaternion.identity);     //spawns cloud particle effect
              Instantiate(toad, transform.position + pos , transform.rotation); //spawns slightly above player position

              //powerup reset
              setPowerUpState(false, "none");
            }

            else if (name == "healthpack")
            {

                health = health + 5;
                healthDisplay.text = health.ToString();
                setPowerUpState(false, "none");
            }

            else if (name == "cloak" && timeSlowDownActive != true)
            {
                StartCoroutine(changeTimeScale());
                setPowerUpState(false, "none");
            }
    }

    public void setPowerUpState(bool state, string name)
    {
        hasPowerUp = state;
        powerUpName = name;
    }

    public void LockMinMaxHealth()
    {
        if (health < 0)
        {
            health = 0;
            healthDisplay.text = health.ToString();
        }

        if (health > 100)
        {
            health = 100;
            healthDisplay.text = health.ToString();
        }
    }

    IEnumerator changeTimeScale()
    {
        isCloaked = true;
        timeSlowDownActive = true;
        Time.timeScale = 0.3f;
        source.PlayOneShot(narutoVSasukeScream, 1f);
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 1f;
        timeSlowDownActive = false;
        isCloaked = false;
    }

    public void showScoreOnLose()
    {
        scoreUI_Lose.text = playerScore.ToString();
    }
}
