using UnityEngine;

public class Effects : MonoSingleton<Effects>
{
    public void ShakeCamera() => _camera.Shake();
    
    [SerializeField] private CameraShake _camera;
}