using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float xBound = 16;
    [SerializeField] private float yBound = 11;
    [SerializeField] private float verticalInput;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float invincibleTime = 3.0f;

    [HideInInspector] private bool isInvincible;

    private GameObject rudolphObject;
    private Collider playerCollider;
    private AudioSource playerAudio;
    private GameManager gameManager;
    [SerializeField] public AudioClip goodiesSound;
    [SerializeField] public AudioClip explosionSound;
    [SerializeField] public ParticleSystem pickedItemParticle;
    [SerializeField] public ParticleSystem explodeParticle;

    [SerializeField] public GameObject powerupEnabler;

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

        // Move while game is active
        if (gameManager.isGameActive)
        {
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

    // Methods for powerup  
    private void setInvincible()
    {
        isInvincible = true;
        powerupEnabler.SetActive(true);
        Debug.Log("Invincibility Active");

        CancelInvoke("setKillable");
        Invoke("setKillable", invincibleTime);

    }

    private void setKillable()
    {
        isInvincible = false;
        powerupEnabler.SetActive(false);
        Debug.Log("Invincibility Inactive");
    }

    private void OnCollisionEnter(Collision other)
    {
        //Setting up the powerup
        if (other.gameObject.CompareTag("Powerup"))
        {
            setInvincible();
            playerAudio.PlayOneShot(goodiesSound, 1.0f);
            pickedItemParticle.Play();
            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Goodies"))
        {
            playerAudio.PlayOneShot(goodiesSound, 1.0f);
            pickedItemParticle.Play();
            Destroy(other.gameObject);
            gameManager.UpdateScore(10);
        }

        // If statement for the bomb
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            if (isInvincible == true)
            {
                playerAudio.PlayOneShot(explosionSound, 1.0f);
                explodeParticle.Play();
                gameManager.isGameActive = true;
                rudolphObject.SetActive(true);
                Destroy(other.gameObject);
                Debug.Log("Testing invincibilty");
            }
            else
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

        // Seperate if statement for the missiles
        else if (other.gameObject.CompareTag("Missile"))
        {
            if (isInvincible == true)
            {
                playerAudio.PlayOneShot(explosionSound, 1.0f);
                explodeParticle.Play();
                gameManager.isGameActive = true;
                rudolphObject.SetActive(true);
                Destroy(other.gameObject);
                Debug.Log("Testing invincibilty");
            }
            else
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


}
