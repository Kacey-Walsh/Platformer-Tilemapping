//Creator: Kacey Walsh
//Class: DIG3480 Computer As A Medium with Prof. Kenton Howard
//University of Central Florida

//Challenge 1: Roll a Ball Challenge
//Started: March 2nd, 2022
//Due: March 20th, 2022 + Extension given to bereavement

//Challenge 2: https://webcourses.ucf.edu/courses/1401033/assignments/7396578
//Sound Animation: https://webcourses.ucf.edu/courses/1401033/pages/tutorial-1-dot-5-sound-animation-and-github-basics-notes?module_item_id=15356569

//Criteria:

//Tier 1:  - ✓
//4 horizontal platforms.  - ✓ 
//player should not be able to escape level  - ✓

//2 more collectable coin pickups.  - ✓
//add "you win! game created by ___" in the center of screen  - ✓


//Tier 2:  - ✓
//set player lives to 3  - ✓
//add UI to display lives at the top right of the screen  - ✓

//add 4 enemies  - ✓
//enemies move in straight consistant back and forth lines.
//enemies disapear upon contact and reduce player lives  - ✓
//add "you loose" screen  - ✓
//destroy player  - ✓

//create second stage when player collects all coins.  - ✓
//should work the same way as level 1  - ✓
//4 platforms  - ✓
//4 coins  - ✓
//4 enemies  - ✓
//different layout to lvl 1  - ✓
//adjust win message  - ✓
//reset lives to 3  - ✓

//add copyright-free music to the game  - ✓
//make it loop.  - ✓
//play sound effect or different background music when the player wins.  - ✓
//when you win only the win music should play  - ✓


//Tier 3:
//replace player sprite
//create idling and running animations
//idle animation should play when player is not moving
//make sure the player's collision still works
//sprite should flip when the player faces in the opposite direction

//create a jumping animation
//only should work in the air.


//Status: Work in progress


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //MOVEMENT
    private Rigidbody2D rd2d;
    public float speed;

    //ENDINGS
    public GameObject winTextObject;
    public GameObject looseTextObject;

    //LIVES
    public Text lives;
    public int livesValue = 3;

    //SCORE
    public Text score;
    private int scoreValue = 0;

    //LEVELS
    private int level;

    //MUSIC
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    //INITIATED AT START OF GAME
    void Start()
    {
        //MOVEMENT - LIVES - SCORE
        rd2d = GetComponent<Rigidbody2D>();
        lives.text = livesValue.ToString();
        score.text = scoreValue.ToString();

        //ENDINGS
        winTextObject.SetActive(false);
        looseTextObject.SetActive(false);

        //LEVEL
        level = 1;

        //MUSIC
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        Debug.Log("playing music");
    }

    //EXITING THE GAME
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    //UPDATED ONCE PER FRAME
    void FixedUpdate()
    {
        //MOVEMENT
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        //FROM LVL1 to LVL2
        if ((level == 1) && (scoreValue == 4))
        {
            transform.position = new Vector3(40f, 0f, 0f);
            level = 2;
            livesValue = 3;
            Debug.Log("Moving to Next Level");

        }

        //ENDINGS        
        //WINNING
        //if on the 2nd level and the score is 8 - sets the level to 3 to avoid bugs, debug message, displays win and starts win music
        if ((level == 2) && (scoreValue == 8))
        {
            level = 3;
            Debug.Log("You Win!");
            //YOU WIN TEXT
            winTextObject.SetActive(true);

            //WINNING MUSIC
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;

            Debug.Log("Win music plays");
            Destroy(gameObject);
        }

        //LOOSING
        //if lives = 0, display loose text and delete character.
        if (livesValue == 0)
        {
            looseTextObject.SetActive(true);
            Destroy(gameObject);
        }

        //EXITING THE GAME
            //dont know why its being weird, but theres one instance FixedUpdate and Update
            //It works so I'm not touching it
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    //COLLIDING WITH OBJECTS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //COLLIDING WITH COINS TO ADD SCORE
        //there is a bug where the text wont update immediately, can this be fixed by moving this to update? - NO
        //NEED SUGGESTIONS
        //Set score, add 1 point to score, debug log, and destroy the coin
        if (collision.collider.tag == "Coin")
        {
            scoreValue = scoreValue + 1;
            score.text = "Score: " + scoreValue.ToString();
            Debug.Log("Coin Collected");
            Destroy(collision.collider.gameObject);
        }

        //COLLIDING WITH COINS TO TAKE LIVES
        //has the same error as the coin bug
        //Set life score, take life, debug log, and destroy enemy
        if (collision.collider.tag == "Enemy")
        {
            livesValue = livesValue - 1;
            lives.text = "Lives: " + livesValue.ToString();
            Debug.Log("life lost");
            Destroy(collision.collider.gameObject);
        }
    }

    //COLLIDING WITH GROUND TO ALLOW FOR JUMPING
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                //Allows player to jump when touching floor
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }


}