using UnityEngine;

public class Player : MonoBehaviour, IControllable
{
    [SerializeField] private float _accelerateSpeed;
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private float _projectileForce;

    [SerializeField] private Transform _projectilesSpawnPosition; 

    private bool _isAccelerating = false;

    private Rigidbody2D _rigidbody;
    private Transform _transform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    public void StartAccelerate() => _isAccelerating = true;

    public void StopAccelerate() => _isAccelerating = false;

    public void Accelerate()
    {
        _rigidbody.AddForce(_transform.up * _accelerateSpeed);
    }

    public void Rotate(Vector2 direction)
    {
        if (direction.sqrMagnitude <= 0) return;

        float targetAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;

        float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.fixedDeltaTime * _rotateSpeed);
        _transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    public void Shoot()
    {
        ProjectilesController.instance.SpawnProjectile(_projectilesSpawnPosition.position, _transform.up * _projectileForce);
    }

    public void GetHit()
    {
        Debug.Log("player hit");
    }

    private void FixedUpdate()
    {
        if (_isAccelerating)
            Accelerate();
    }
}
