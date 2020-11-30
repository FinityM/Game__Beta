using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float objectSpawnRate = 3.0f;
    private float xSpawn = 50;
    private float ySpawnRange = 10;
    private int score;
    public bool isGameActive;

    public List<GameObject> objects;
    public List<GameObject> clouds;
    public GameObject powerup;
    public GameObject titleScreen;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public Button restartButton;

    private Difficulty difficulty;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
        //difficulty.setDifficulty();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawner()
    {
        while (isGameActive)
        {
            scoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(objectSpawnRate);
            Vector3 randomSpawn = new Vector3(xSpawn, Random.Range(-ySpawnRange, ySpawnRange), 0);
            Vector3 cloudRandomSpawn = new Vector3(xSpawn, Random.Range(-ySpawnRange, ySpawnRange), 10);
            Vector3 powerupRandomSpawn = new Vector3(xSpawn, Random.Range(-ySpawnRange, ySpawnRange), 0);

            int objectIndex = Random.Range(0, objects.Count);
            int cloudIndex = Random.Range(0, clouds.Count);

            Instantiate(objects[objectIndex], randomSpawn, objects[objectIndex].transform.rotation);
            Instantiate(clouds[cloudIndex], cloudRandomSpawn, objects[cloudIndex].transform.rotation);            
            Instantiate(powerup, powerupRandomSpawn, powerup.transform.rotation);

        }

    }

    
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart was pressed"); // Caused issues from editor, was missing an EventSystem
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;

        StartCoroutine(Spawner());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);

    }

}
