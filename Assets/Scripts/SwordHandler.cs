using System;
using UnityEngine;
using Enums;
using System.Collections;

public class SwordHandler : MonoBehaviour
{
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
    [SerializeField]
    private float _brickBombMultiplier;
    [HideInInspector]
    public GameStates gameState
    {
        set;
        private get;
    }

    public Action<int> OnBladeCut;
    public Action<int> OnBladeFinish;
    public Action OnBladeHitGround;

    private void Awake()
    {
        _blade.OnBladeHit += OnBladeHit;
        _hilt.OnHiltHit += OnHiltHit;
    }

    private void OnHiltHit(string tag)
    {
        if (tag.Equals(Constants.GROUND))
        {
            OnGroundHit();
        }
        else
        {
            _rigidbody.isKinematic = false;
            Move(-1);
            Rotate(-1);
        }
    }

    private void OnBladeHit(Collider collider)
    {
        if (gameState != GameStates.InProgress)
        {
            return;
        }

        var tag = collider.tag;
        switch (tag)
        {
            case Constants.CUTTABLE:
                _rigidbody.isKinematic = false;
                _rigidbody.angularVelocity = Vector3.zero;
                var cuttable = collider.GetComponent<CutableObject>();
                if (cuttable != null)
                {
                    var score = cuttable.CutValue;
                    OnBladeCut?.Invoke(score);
                }
                break;
            case Constants.GROUND:
                OnGroundHit();
                break;
            case Constants.BOMB:
                Move(0);
                Rotate(0);
                break;
            case Constants.FINISH:
                _rigidbody.isKinematic = true;
                var multiplier = collider.GetComponent<Finish>().Multiplier;
                OnBladeFinish?.Invoke(multiplier);
                break;
            default:
                _rigidbody.isKinematic = true;
                _rigidbody.angularVelocity = Vector3.zero;
                break;
        }
    }

    private void OnGroundHit()
    {
        _rigidbody.isKinematic = true;
        OnBladeHitGround?.Invoke();
    }

    private void FixedUpdate()
    {
        if (gameState != GameStates.InProgress)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            _rigidbody.isKinematic = false;
            Move(1);
            Rotate(1);
        }
    }

    private void Move(int direction)
    {
        Vector3 force = Vector3.zero;
        switch (direction)
        {
            case -1:
                force = _backwardsForce;
                break;
            case 0:
                force = _backwardsForce * 3;
                break;
            case 1:
                force = _forwardForce;
                break;
        }
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

    private void Rotate(int direction)
    {
        Vector3 torque = Vector3.zero;
        switch (direction)
        {
            case -1:
                torque = _backwardsTorque;
                break;
            case 0:
                torque = _backwardsTorque * _brickBombMultiplier;
                break;
            case 1:
                torque = _forwardTorque;
                break;
        }
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddTorque(torque, ForceMode.Acceleration);
    }
}
