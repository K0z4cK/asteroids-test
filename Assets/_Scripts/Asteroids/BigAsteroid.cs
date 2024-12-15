using UnityEngine;

public class BigAsteroid : Asteroid
{
    [SerializeField] private SmallAsteroid _smallAsteroidPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(ProjectileTag))
        {
            ProjectilesController.instance.RemoveProjectile(collision.transform);

            Vector2 dividedForce = _force / 2f;

            var smallAsteroid = Instantiate(_smallAsteroidPrefab, transform.position, transform.rotation);
            smallAsteroid.Init(dividedForce);

            smallAsteroid = Instantiate(_smallAsteroidPrefab, transform.position, transform.rotation);
            smallAsteroid.Init(-dividedForce);

            Destroy(gameObject);
        }
        else if (collision.CompareTag(PlayerTag))
        {
            Destroy(gameObject);
            collision.GetComponent<Player>().GetHit();
        }
    }
}
