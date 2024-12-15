using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private const string MenuSceneName = "Menu";

    private LevelDataScriptableObject _levelData;

    [SerializeField] private UIManager _uiManager;

    [SerializeField] private AsteroidsController _asteroidsController;
    [SerializeField] private Player _player;

    [SerializeField] private int _startPlayerHealth = 3;

    private int _score = 0;

    private bool _isLose = false;

    private void Awake()
    {
        _levelData = LevelDataHolder.Instance.CurrentLevelData;

        _uiManager.Init(_player.StartAccelerate, _player.StopAccelerate, _player.Shoot, GoToMenu, RestartLevel, NextLevel);
        _uiManager.UpdateScore(_score);
        _uiManager.UpdateLives(_startPlayerHealth);

        _asteroidsController.Init(_levelData,
            delegate { AddScoreForAsteroid(_levelData.bigAsteroidScore); },
            delegate { AddScoreForAsteroid(_levelData.smallAsteroidScore); },
            OnLevelWin);

        _player.Init(_startPlayerHealth, PlayerHit);
    }

    private void AddScoreForAsteroid(int score)
    {
        _score += score;
        _uiManager.UpdateScore(_score);
    }

    private void PlayerHit(int health)
    {
        _uiManager.UpdateLives(health);
        if (health <= 0)
        {
            OnLevelLose();
        }
    }

    private void OnLevelWin()
    {
        if (_isLose) return;

        ProgressManager.Instance.UpdateCurrentLevel(LevelDataHolder.Instance.CurrentLevelIndex);
        ProgressManager.Instance.UpdateTotalScore(_score);

        Time.timeScale = 0;
        _uiManager.ShowWinPanel();
        Debug.Log("LevelWin");
    }

    private void OnLevelLose()
    {
        _isLose = true;
        Time.timeScale = 0;
        _uiManager.ShowLosePanel();
        Debug.Log("LevelLose");
    }

    private void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MenuSceneName);
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void NextLevel()
    {
        Time.timeScale = 1f;
        if (LevelDataHolder.Instance.IsLastLevelIndex())
            _uiManager.ShowLAstLevelPopup();
        else
        {
            LevelDataHolder.Instance.UpdateToNextLevelIndex();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
