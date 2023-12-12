using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public Action<string> OnBladeHit;

    private void OnTriggerEnter(Collider other)
    {
        OnBladeHit?.Invoke(other.tag);
    }
}
