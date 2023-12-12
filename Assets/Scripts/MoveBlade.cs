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
        Move(-1);
        Rotate(-1);
    }

    private void OnBladeHit(string hitTag)
    {
        if (hitTag.Equals(CUTTABLE))
        {
            _rigidbody.isKinematic = false;
            _rigidbody.angularVelocity = Vector3.zero;
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
            Move(1);
            Rotate(1);
        }
    }

    private void Move(int direction)
    {
        Vector3 force = direction == 1 ? _forwardForce : _backwardsForce;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

    private void Rotate(int direction)
    {
        Vector3 torque = direction == 1 ? _forwardTorque : _backwardsTorque;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddTorque(torque, ForceMode.Acceleration);
    }
}
