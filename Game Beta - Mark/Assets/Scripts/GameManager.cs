using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float objectSpawnRate = 3;
    private float powerupSpawnRate = 5;
    private float xSpawn = 50;
    private float ySpawnRange = 9;
    private int score;
    public float objectSpeedMultiplier = 1;
    public bool isGameActive = false;

    public List<GameObject> objects;
    public List<GameObject> clouds;
    public GameObject powerup;
    public GameObject titleScreen;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // IEnumerator for the spawner
    IEnumerator Spawner()
    {
        while (isGameActive)
        {
            scoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(objectSpawnRate);

            Vector3 randomSpawn = new Vector3(xSpawn, Random.Range(-ySpawnRange, ySpawnRange), 0);
            Vector3 cloudRandomSpawn = new Vector3(xSpawn, Random.Range(-ySpawnRange, ySpawnRange), 10);

            int objectIndex = Random.Range(0, objects.Count);
            int cloudIndex = Random.Range(0, clouds.Count);

            GameObject spawnNewObject = Instantiate(objects[objectIndex], randomSpawn, objects[objectIndex].transform.rotation);
            spawnNewObject.GetComponent<MoveLeft>().speed *= objectSpeedMultiplier;
            GameObject spawnNewCloudObject = Instantiate(clouds[cloudIndex], cloudRandomSpawn, objects[cloudIndex].transform.rotation);
            spawnNewCloudObject.GetComponent<MoveLeft>().speed *= objectSpeedMultiplier;



        }

    }

    // IEnumerator for the powerup
    IEnumerator PowerupSpawner()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(powerupSpawnRate);
            Vector3 powerupRandomSpawn = new Vector3(xSpawn, Random.Range(-ySpawnRange, ySpawnRange), 0);
            GameObject spawnNewPowerup = Instantiate(powerup, powerupRandomSpawn, powerup.transform.rotation);
            spawnNewPowerup.GetComponent<MoveLeft>().speed *= objectSpeedMultiplier;
        }
    }


    // Method for updating the score 
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }


    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Caused issues from editor in the game alpha, was missing an EventSystem
    // Buttons for the menu
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart was pressed");
    }

    public void easyStartGame()
    {
        isGameActive = true;
        score = 0;

        StartCoroutine(Spawner());
        StartCoroutine(PowerupSpawner());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }

    public void mediumStartGame()
    {
        objectSpawnRate = 2;
        objectSpeedMultiplier = 2;
        isGameActive = true;
        score = 0;

        StartCoroutine(Spawner());
        StartCoroutine(PowerupSpawner());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);

    }

    public void hardStartGame()
    {
        objectSpawnRate = 1;
        objectSpeedMultiplier = 3;
        isGameActive = true;
        score = 0;

        StartCoroutine(Spawner());
        StartCoroutine(PowerupSpawner());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);

    }
}
