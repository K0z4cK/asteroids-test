using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    private event UnityAction<int> OnPlayerHit;

    [Header("Controll")]
    [SerializeField] private VariableJoystick _variableJoystick;
    [SerializeField] private float _accelerateSpeed;
    [SerializeField] private float _rotateSpeed;

    [Header("Projectiles")]
    [SerializeField] private float _projectileForce;
    [SerializeField] private Transform _projectilesSpawnPosition;

    [Header("Shield")]
    [SerializeField] private float _shieldTime;
    [SerializeField] private string _animatorShieldBoolKey = "IsShieldActivated";

    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Animator _animator;

    private int _healthCount;

    private bool _isAccelerating = false;
    private bool _isShieldActivated = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _transform = transform;
    }

    public void Init(int healthCount, UnityAction<int> onPlayerHit)
    {
        _healthCount = healthCount;
        OnPlayerHit = onPlayerHit;
    }

    public void StartAccelerate() => _isAccelerating = true;

    public void StopAccelerate() => _isAccelerating = false;

    public void Accelerate()
    {
        _rigidbody.AddForce(_transform.up * _accelerateSpeed);
    }

    public void Rotate()
    {
        Vector2 direction = new Vector2 (_variableJoystick.Horizontal, _variableJoystick.Vertical);

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
        if(_isShieldActivated) return;

        _healthCount--;
        OnPlayerHit?.Invoke(_healthCount);
        StartCoroutine(ShieldCoroutine());

        Debug.Log("player hit");
    }

    private IEnumerator ShieldCoroutine()
    {
        _isShieldActivated = true;
        _animator.SetBool(_animatorShieldBoolKey, _isShieldActivated);
        yield return new WaitForSeconds(_shieldTime);
        _isShieldActivated = false;
        _animator.SetBool(_animatorShieldBoolKey, _isShieldActivated);
    }

    private void FixedUpdate()
    {
        Rotate();

        if (_isAccelerating)
            Accelerate();
    }
}
