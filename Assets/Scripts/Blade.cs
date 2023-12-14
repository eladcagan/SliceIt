using System;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public Action<Collider> OnBladeHit; 

    private void OnTriggerEnter(Collider other)
    {
        OnBladeHit?.Invoke(other);
    }
}
