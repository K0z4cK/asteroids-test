using UnityEngine;
using UnityEngine.Events;

public class SmallAsteroid : Asteroid
{
    private event UnityAction<bool> OnSmallAsteroidDestory;

    public void Init(Vector2 force, UnityAction<bool> onSmallAsteroidDestory)
    {
        Init(force);
        OnSmallAsteroidDestory = onSmallAsteroidDestory;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ProjectileTag))
        {
            ProjectilesController.instance.RemoveProjectile(collision.transform);

            OnSmallAsteroidDestory?.Invoke(true);
            Destroy(gameObject);
        }
        else if (collision.CompareTag(PlayerTag))
        {
            collision.GetComponent<Player>().GetHit();

            OnSmallAsteroidDestory?.Invoke(false);
            Destroy(gameObject);
        }
    }
}
