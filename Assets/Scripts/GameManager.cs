using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    STATE_MAINMENU,
    STATE_PLAYING,
    STATE_FINISHED
}

public class GameManager : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject EnemyGameObject;

    public GameState state;
    private Game game;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }
    
    private GameManager() { }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        state = GameState.STATE_MAINMENU;

        SceneManager.sceneLoaded += SceneManager_OnSceneLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.STATE_PLAYING)
            game.Update();
    }

    private void SceneManager_OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            // Main Menu
            case 0:
                Instance.state = GameState.STATE_MAINMENU;
                break;

            // Game
            case 1:
                Instance.StartGame();
                break;
        }
    }

    public void StartGame()
    {
        if (state != GameState.STATE_MAINMENU)
            return;

        game = new Game(PlayerGameObject, EnemyGameObject);
        game.Start();

        state = GameState.STATE_PLAYING;
    }

    public void FinishGame()
    {
        if (state != GameState.STATE_PLAYING)
            return;

        game.Finish();

        state = GameState.STATE_FINISHED;
    }
    
    public void IncreaseScore() { game.SetScore(game.GetScore() + 1); }

    public void SetMaxSize(float size) { game.SetMaxSize(size); }
    public void SetMinSize(float size) { game.SetMinSize(size); }

    public float GetMaxSize() { return game.GetMaxSize(); }
    public float GetMinSize() { return game.GetMinSize(); }
    public float GetEnemySpeed() { return game.GetEnemySpeed(); }
    public int GetScore() { return game.GetScore(); }
}
