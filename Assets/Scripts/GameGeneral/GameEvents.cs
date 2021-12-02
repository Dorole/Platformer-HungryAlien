using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action onGunExpired;
    public void SpawnGun()
    {
        if (onGunExpired != null)
            onGunExpired();
    }

    public event Action onCameraSwitchEnter;
    public void EnterSwitchCamera()
    {
        if (onCameraSwitchEnter != null)
            onCameraSwitchEnter();
    }
}
