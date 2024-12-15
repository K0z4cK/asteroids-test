using UnityEngine;

public interface IControllable
{
    public void Rotate(Vector2 direction);
    public void Shoot();
    public void StartAccelerate();
    public void StopAccelerate();
    public void Accelerate();
}
