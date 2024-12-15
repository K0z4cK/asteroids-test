using UnityEngine;

public class SmallAsteroid : Asteroid
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ProjectileTag))
        {
            ProjectilesController.instance.RemoveProjectile(collision.transform);
            Destroy(gameObject);
        }
        else if (collision.CompareTag(PlayerTag))
        {
            Destroy(gameObject);
            collision.GetComponent<Player>().GetHit();
        }
    }
}
