using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
public GameObject Player;
public GameObject [] hazards;
public GameObject Background;
public Vector3 spawnValues;
public AudioSource VictoryMusic;
public AudioSource BackgroundMusic;
    public AudioClip DefeatClip;
public AudioSource DefeatMusic;
public AudioSource Warp1;
public AudioSource Warp2;

public int hazardCount;
public float spawnWait;
public float startWait;
public float waveWait;

public Text PointsText;
public Text restartText;
public Text gameOverText;
public Text winText;
public Text credits;

private bool gameOver;
private bool restart;
private int Points;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        credits.text = "";
        Points = 0;
        UpdatePoints();
        StartCoroutine(SpawnWaves());
    }
    private void FixedUpdate()
    {
        if (Player == null)
        {
            BackgroundMusic.Stop();
            VictoryMusic.PlayOneShot(DefeatClip, 0.35F);
        }
    }
    void Update()
    {
        
        if (restart)
        {
         if (Input.GetKeyDown(KeyCode.Space))
          {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
          }
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    IEnumerator SpawnWaves()
{
yield return new WaitForSeconds(startWait);
while (true)
{
for (int i = 0; i < hazardCount; i++)
{
GameObject hazard = hazards[Random.Range (0, hazards.Length)];
Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
Quaternion spawnRotation = Quaternion.identity;
Instantiate(hazard, spawnPosition, spawnRotation);
yield return new WaitForSeconds(spawnWait);
}
yield return new WaitForSeconds(waveWait);

if (gameOver)
{
  restartText.text = "Press 'Space' to Restart";
  restart = true;
   break;
 }
 }
}

public void AddPoints(int newPointsValue)
{
        Points += newPointsValue;
        UpdatePoints();
}
    void UpdatePoints()
    {
        PointsText.text = "Points: " + Points;
        if (Points >= 150)
        {
            winText.text = "You win!";
            credits.text = "Created by Jhon Vergara, Thanks for playing!";
            gameOver = true;
            restart = true;
            Destroy(Background, 4f);
            BackgroundMusic.Stop();
            if (Warp1.isPlaying) return;
            if (Warp2.isPlaying) return;
            Warp1.PlayDelayed(.5f);
            Warp2.PlayDelayed(4f);
            VictoryMusic.PlayDelayed(4f);            
        }
    }

    public void GameOver()
{
gameOverText.text = "Game Over!";
gameOver = true;

        
}
    
}
