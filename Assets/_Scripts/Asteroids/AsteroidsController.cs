using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    [SerializeField] private Asteroid _asteroidPrefab;

    [SerializeField] private float _spawnDistance = 10f;

    private int _asteroidsCount = 5; // temp
    private float _minSpeed = 100f; // temp
    private float _maxSpeed = 300f; // temp

    private Vector2 _screenBounds;

    void Start()
    {
        Camera cam = Camera.main;
        _screenBounds = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);

        for (int i = 0; i < _asteroidsCount; i++)
        {
            SpawnAsteroid();
        }
    }

    public void SpawnAsteroid()
    {
        Vector2 spawnPosition = GetRandomPositionOutsideScreen();

        var asteroid = Instantiate(_asteroidPrefab, spawnPosition, Quaternion.identity);

        Vector2 targetDirection = GetDirectionTowardsScreen(spawnPosition);
        float randomSpeed = Random.Range(_minSpeed, _maxSpeed);

        asteroid.Init(targetDirection.normalized * randomSpeed);
    }

    private Vector2 GetRandomPositionOutsideScreen()
    {
        float side = Random.Range(0f, 4f);
        Vector2 position;

        if (side < 1f)
            position = new Vector2(-_screenBounds.x - _spawnDistance, Random.Range(-_screenBounds.y, _screenBounds.y));
        else if (side < 2f)
            position = new Vector2(_screenBounds.x + _spawnDistance, Random.Range(-_screenBounds.y, _screenBounds.y));
        else if (side < 3f)
            position = new Vector2(Random.Range(-_screenBounds.x, _screenBounds.x), -_screenBounds.y - _spawnDistance);
        else
            position = new Vector2(Random.Range(-_screenBounds.x, _screenBounds.x), _screenBounds.y + _spawnDistance);

        return position;
    }

    private Vector2 GetDirectionTowardsScreen(Vector2 position)
    {
        Vector2 randomTarget = new Vector2(
            Random.Range(-_screenBounds.x, _screenBounds.x),
            Random.Range(-_screenBounds.y, _screenBounds.y)
        );
        return randomTarget - position;
    }
}

