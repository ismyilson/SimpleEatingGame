using UnityEngine;
using UnityEngine.UI;

enum SpawnType
{
    SPAWN_UP,
    SPAWN_DOWN,
    SPAWN_LEFT,
    SPAWN_RIGHT
}

struct Boundaries
{
    public Boundaries(float width, float height)
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        minX = v.x - 0.1f;
        maxX = Mathf.Abs(minX);

        minY = v.y - 0.1f;
        maxY = Mathf.Abs(minY);
    }

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
}

public class Game
{
    private GameObject PlayerGameObject;
    private GameObject EnemyGameObject;
    private Boundaries boundaries;

    private float difficultyTimer;
    private const float difficultyTimerEnd = 3f;

    private float spawnTimer;
    private float spawnTimerEnd;

    private int score;
    private Text scoreText;
    private GameObject gameOverPanel;

    private float minSize;
    private float maxSize;
    private float enemySpeed;

    public Game(GameObject playerGameObject, GameObject enemyGameObject)
    {
        PlayerGameObject = playerGameObject;
        EnemyGameObject = enemyGameObject;

        boundaries = new Boundaries(Screen.width, Screen.height);
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);
    }

    public void Start()
    {
        SetupGame();
        SetupPlayer();
    }

    public void Finish()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOverPanel.SetActive(true);
    }

    public void Update()
    {
        if (spawnTimer >= spawnTimerEnd)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
        else
            spawnTimer += Time.deltaTime;

        if (difficultyTimer >= difficultyTimerEnd)
        {
            IncreaseDifficulty();
            difficultyTimer = 0f;
        }
        else
            difficultyTimer += Time.deltaTime;
    }

    private void SetupGame()
    {
        score = 0;

        difficultyTimer = 0f;
        
        spawnTimer = 0f;
        spawnTimerEnd = 0.3f;

        enemySpeed = 0.5f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        
        SetMinSize(0.1f);
        SetMaxSize(0.7f);
    }

    private void SetupPlayer()
    {
        GameObject player = GameObject.Instantiate(PlayerGameObject);
        player.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void SpawnEnemy()
    {
        SpawnType type = (SpawnType) Random.Range(0, System.Enum.GetNames(typeof(SpawnType)).Length);

        Vector2 spawnPoint = new Vector2();
        Vector2 moveDirection = new Vector2();
        switch (type)
        {
            case SpawnType.SPAWN_UP:
                spawnPoint.y = boundaries.maxY;
                spawnPoint.x = Random.Range(boundaries.minX, boundaries.maxX);

                moveDirection.y = boundaries.minY;
                moveDirection.x = Random.Range(boundaries.minX, boundaries.maxX);
                break;

            case SpawnType.SPAWN_DOWN:
                spawnPoint.y = boundaries.minY;
                spawnPoint.x = Random.Range(boundaries.minX, boundaries.maxX);

                moveDirection.y = boundaries.maxY;
                moveDirection.x = Random.Range(boundaries.minX, boundaries.maxX);
                break;

            case SpawnType.SPAWN_LEFT:
                spawnPoint.x = boundaries.minX;
                spawnPoint.y = Random.Range(boundaries.minY, boundaries.maxY);

                moveDirection.x = boundaries.maxX;
                moveDirection.y = Random.Range(boundaries.minY, boundaries.maxX);
                break;

            case SpawnType.SPAWN_RIGHT:
                spawnPoint.x = boundaries.maxX;
                spawnPoint.y = Random.Range(boundaries.minY, boundaries.maxY);

                moveDirection.x = boundaries.minX;
                moveDirection.y = Random.Range(boundaries.minY, boundaries.maxX);
                break;
        }

        GameObject enemy = GameObject.Instantiate(EnemyGameObject);
        enemy.transform.position = spawnPoint;
        enemy.GetComponent<Enemy>().Move(moveDirection);
    }

    public void IncreaseDifficulty()
    {
        spawnTimerEnd -= 0.025f;
        enemySpeed += 0.1f;
    }

    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    public void SetMinSize(float size)
    {
        if (size < 0.01f)
            return;

        minSize = size;
    }

    public void SetMaxSize(float size)
    {
        maxSize = size;
    }

    public int GetScore() { return score; }
    public float GetMinSize() { return minSize; }
    public float GetMaxSize() { return maxSize; }
    public float GetEnemySpeed() { return enemySpeed; }
}
