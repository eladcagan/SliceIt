using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutableObject : MonoBehaviour
{
    private const string BLADE = "Blade";

    [SerializeField]
    private float _cutForce;
    private Rigidbody[] _rigidbodies;

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(BLADE))
        {
            for(int i = 0;  i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].isKinematic = false;
                Debug.Log(Vector3.forward * _cutForce * Mathf.Pow(-1, i));
                _rigidbodies[i].AddExplosionForce(_cutForce, _rigidbodies[i].position,1);
            }
        }
    }
}
