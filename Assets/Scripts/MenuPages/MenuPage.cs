using UnityEngine;

public abstract class MenuPage : MonoBehaviour
{
    public abstract void Create();
    public abstract void Open();
    public abstract void Opening(float elapsed);
    private void FixedUpdate()
    {
        Opening(Time.fixedDeltaTime);
    }
}