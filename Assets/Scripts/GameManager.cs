using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public Text scoreText;
    public Text livesText;
    public GameObject gameOver;

    private Blaster playerBlaster;
    private Centipede centipede;
    private MushroomField mushroomField;

    private int score;
    private int lives;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        playerBlaster = FindAnyObjectByType<Blaster>();
        centipede = FindAnyObjectByType<Centipede>();
        mushroomField = FindAnyObjectByType<MushroomField>();
        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        centipede.Respawn();
        playerBlaster.Respawn();
        mushroomField.Clear();
        mushroomField.Generate();
        gameOver.SetActive(false);
    }

    private void Update()
    {
        if (lives <= 0 && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            NewGame();
        }
    }

    public void AddScore(int amount)
    {
        SetScore(score + amount);
    }

    private void GameOver()
    {
        playerBlaster.gameObject.SetActive(false);
        centipede.gameObject.SetActive(false);
        gameOver.SetActive(true);
    }

    public void ResetRound()
    {
        SetLives(lives - 1);

        if (lives <= 0)
        {
            GameOver();
        }
        centipede.Respawn();
        playerBlaster.Respawn();
        mushroomField.Heal();
    }

    public void NextLevel()
    {
        centipede.speed *= 1.1f;
        centipede.size += 4;
        centipede.Respawn();
        mushroomField.Clear();
        mushroomField.Generate();
    }

    private void SetScore(int value)
    {
        score = value;
        scoreText.text = score.ToString();
    }

    private void SetLives(int value)
    {
        lives = value;
        livesText.text = lives.ToString();
    }
}
