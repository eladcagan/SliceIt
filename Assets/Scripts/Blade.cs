using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public Action<Collider> OnBladeHit; // TODO: Is passing collider cost effective?

    private void OnTriggerEnter(Collider other)
    {
        OnBladeHit?.Invoke(other);
    }
}
