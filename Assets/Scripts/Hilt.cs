using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hilt : MonoBehaviour
{
    public Action<string> OnHiltHit;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        OnHiltHit?.Invoke(other.tag);
    }
}
