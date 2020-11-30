using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private float speed = 20.0f;
    private float xBound = 16;
    private float yBound = 11;
    private float verticalInput;
    private float horizontalInput;
    public int pointValue;
    public bool gameOver;

    private GameObject rudolphObject;
    private Collider playerCollider;
    private AudioSource playerAudio;
    public AudioClip goodiesSound;
    public AudioClip explosionSound;
    public ParticleSystem pickedItemParticle;
    public ParticleSystem explodeParticle;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rudolphObject = GameObject.Find("Rudolph");
        playerCollider = GetComponent<BoxCollider>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    // Method for movement
    void movement()
    {
        // Get the vertical input 
        verticalInput = Input.GetAxis("Vertical");

        //Get the horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        if (gameManager.isGameActive)
        {
            //rudolphObject.SetActive(true); Figure out how to make player appear in beta

            // Move around the screen
            transform.position = transform.position + new Vector3(0, verticalInput * speed * Time.deltaTime, 0);
            transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);


            //Limit the x and y axis movement 
            if (transform.position.x > xBound)
            {
                transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
            }

            else if (transform.position.x < -xBound)
            {
                transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
            }

            if (transform.position.y > yBound)
            {
                transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
            }

            else if (transform.position.y < -yBound)
            {
                transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
            }


        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //Will make power up functional in beta will act like goodie for now
        if (other.gameObject.CompareTag("Powerup"))
        {
            playerAudio.PlayOneShot(goodiesSound, 1.0f);
            pickedItemParticle.Play();
            Destroy(other.gameObject);
            gameManager.UpdateScore(30);
        }

        else if (other.gameObject.CompareTag("Goodies"))
        {
            playerAudio.PlayOneShot(goodiesSound, 1.0f);
            pickedItemParticle.Play();
            Destroy(other.gameObject);
            gameManager.UpdateScore(5);
        }

        else if (other.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(explosionSound, 1.0f);
            explodeParticle.Play();
            gameManager.isGameActive = false;
            rudolphObject.SetActive(false);
            playerCollider.enabled = !playerCollider.enabled;
            Debug.Log("Game Over");
            Destroy(other.gameObject);
            gameManager.GameOver();
        }

        else if (other.gameObject.CompareTag("Missile"))
        {
            playerAudio.PlayOneShot(explosionSound, 1.0f);
            explodeParticle.Play();
            gameManager.isGameActive = false;
            rudolphObject.SetActive(false);
            playerCollider.enabled = !playerCollider.enabled;
            Debug.Log("Game Over");
            Destroy(other.gameObject);
            gameManager.GameOver();
        }
    }
}
