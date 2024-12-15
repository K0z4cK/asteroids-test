using UnityEngine;
using UnityEngine.Events;

public class BigAsteroid : Asteroid
{
    private event UnityAction<bool, BigAsteroid> OnBigAsteroidDestory;

    public void Init(Vector2 force, UnityAction<bool, BigAsteroid> onBigAsteroidDestory)
    {
        Init(force);
        OnBigAsteroidDestory = onBigAsteroidDestory;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(ProjectileTag))
        {
            ProjectilesController.instance.RemoveProjectile(collision.transform);

            OnBigAsteroidDestory?.Invoke(true, this);
            Destroy(gameObject);
        }
        else if (collision.CompareTag(PlayerTag))
        {
            collision.GetComponent<Player>().GetHit();

            OnBigAsteroidDestory?.Invoke(false, this);
            Destroy(gameObject);          
        }
    }
}
