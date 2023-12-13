using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutableObject : MonoBehaviour
{
    [SerializeField]
    private int _cutValue;
    [SerializeField]
    private ParticleSystem _cutPS;

    public int CutValue
    {
        get
        {
            return _cutValue;
        }
    }

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
        if (other.CompareTag(Constants.BLADE))
        {
            foreach(var part in _rigidbodies)
            {
                _collider.enabled = false;
                part.isKinematic = false;
                _cutPS.Play();
                part.AddExplosionForce(_cutForce, part.position, 1);
            }
        }
    }
}
