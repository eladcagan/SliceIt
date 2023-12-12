using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutableObject : MonoBehaviour
{
    private const string BLADE = "Blade";

    [SerializeField]
    private float _cutForce;
    private Rigidbody[] _rigidbodies;
    private Collider _collider;

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(BLADE))
        {
            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _collider.enabled = false;
                _rigidbodies[i].isKinematic = false;
                _rigidbodies[i].AddExplosionForce(_cutForce, _rigidbodies[i].position, 1);
            }
        }
    }
}
