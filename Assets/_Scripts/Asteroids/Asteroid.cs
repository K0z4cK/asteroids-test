using UnityEngine;

public class Asteroid : MonoBehaviour
{
    protected const string ProjectileTag = "Projectile";
    protected const string PlayerTag = "Player";

    private Rigidbody2D _rigidbody;

    protected Vector2 _force;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 force)
    {
        _rigidbody.AddForce(force);
        _force = force;
    }
}
