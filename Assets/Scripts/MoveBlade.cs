using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBlade : MonoBehaviour
{

    private const string CUTTABLE = "Cuttable";

    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Blade _blade;
    [SerializeField]
    private Hilt _hilt;

    [Header("Movement")]
    [SerializeField]
    private Vector3 _forwardForce;
    [SerializeField]
    private Vector3 _backwardsForce;
    [SerializeField]
    private Vector3 _forwardTorque;
    [SerializeField]
    private Vector3 _backwardsTorque;

    private void Awake()
    {
        _blade.OnBladeHit += OnBladeHit;
        _hilt.OnHiltHit += OnHiltHit;
    }

    private void OnHiltHit(string obj)
    {
        Debug.Log("OnHiltHit");
        _rigidbody.isKinematic = false;
        Jump(-1);
        Spin(-1);
    }

    private void OnBladeHit(string hitTag)
    {
        if (hitTag.Equals(CUTTABLE))
        {
            _rigidbody.isKinematic = false;
        }
        else
        {
            _rigidbody.isKinematic = true;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _rigidbody.isKinematic = false;
            Jump(1);
            Spin(1);
        }
    }

    private void Jump(int direction)
    {
        Vector3 jumpForce = direction == 1 ? _forwardForce : _backwardsForce;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(jumpForce, ForceMode.Impulse);
    }

    private void Spin(int direction)
    {
        Vector3 spinTorque = direction == 1 ? _forwardTorque : _backwardsTorque;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddTorque(spinTorque, ForceMode.Acceleration);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CUTTABLE))
        {
            _rigidbody.isKinematic = false;
        }
        else
        {
            _rigidbody.isKinematic = true;
        }
    }*/
}
