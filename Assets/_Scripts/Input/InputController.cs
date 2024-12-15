using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private IControllable _controllable;
    private InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Enable();

        _controllable = GetComponent<IControllable>();
        if (_controllable == null)
        {
            throw new System.Exception($"{gameObject.name} don't have IControllable");
        }
    }

    private void OnEnable()
    {
        _inputActions.Player.Acelerate.started += OnAccelerateStart;
        _inputActions.Player.Acelerate.canceled += OnAccelerateStop;
        _inputActions.Player.Shoot.performed += OnShootPerformed;   
    }

    private void OnAccelerateStart(InputAction.CallbackContext context) => _controllable.StartAccelerate();
    private void OnAccelerateStop(InputAction.CallbackContext context) => _controllable.StopAccelerate();

    private void OnShootPerformed(InputAction.CallbackContext context) => _controllable.Shoot();

    private void PerformRotate() => _controllable.Rotate(_inputActions.Player.Rotate.ReadValue<Vector2>());

    private void FixedUpdate()
    {
        PerformRotate();
    }

    private void OnDisable()
    {
        _inputActions.Player.Acelerate.started -= OnAccelerateStart;
        _inputActions.Player.Acelerate.canceled -= OnAccelerateStop;
        _inputActions.Player.Shoot.performed -= OnShootPerformed;
    }
}

