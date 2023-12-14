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
    [SerializeField]
    private Vector3 _powerUpMultiplier;
    [SerializeField]
    private float _powerUpDuration;
    [HideInInspector]
    public GameStates gameState
    {
        set;
        private get;
    }

    public Action OnSwordMove;
    public Action OnBladeHitGround;
    public Action OnHiltHit;
    public Action OnBombHit;
    public Action<bool> OnPowerupHit;
    public Action<int> OnBladeCut;
    public Action<int> OnBladeFinish;
    

    private void Awake()
    {
        _blade.OnBladeHit += OnBladeHit;
        _hilt.OnHiltHit += HandleHiltHit;
    }

    private void HandleHiltHit(string tag)
    {
        if (tag.Equals(Constants.GROUND))
        {
            OnGroundHit();
        }
        else
        {
            _rigidbody.isKinematic = false;
            Move(-1);
            OnHiltHit?.Invoke();
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
                var cuttable = collider.GetComponent<CuttableObject>();
                OnCuttableHit(cuttable);
                break;
            case Constants.GROUND:
                OnGroundHit();
                break;
            case Constants.BOMB:
                Move(0);
                OnBombHit?.Invoke();
                break;
            case Constants.POWERUP:
                HandlePowerUp(_powerUpMultiplier, true);
                break;
            case Constants.BADPOWERUP:
                var badPowerup = new Vector3(1 / _powerUpMultiplier.x, 1 / _powerUpMultiplier.y, 1 / _powerUpMultiplier.z);
                HandlePowerUp(badPowerup, false);
                break;
            case Constants.FINISH:
                OnFinishLineHit(collider);
                break;
            default:
                _rigidbody.isKinematic = true;
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
                break;
        }
    }

    private void OnFinishLineHit(Collider collider)
    {
        _rigidbody.isKinematic = true;
        var multiplier = collider.GetComponent<Finish>().Multiplier;
        OnBladeFinish?.Invoke(multiplier);
    }

    private void OnCuttableHit(CuttableObject cuttable)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.angularVelocity = Vector3.zero;
        
        if (cuttable != null)
        {
            var score = cuttable.CutValue;
            OnBladeCut?.Invoke(score);
        }
    }

    private void HandlePowerUp(Vector3 powerUpMultiplier, bool isGood)
    {
        StartCoroutine(ChangeSize(powerUpMultiplier));
        OnPowerupHit?.Invoke(isGood);
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
        }
    }

    private void Move(int direction)
    {
        OnSwordMove?.Invoke();
        Vector3 force = Vector3.zero;
        Vector3 torque = Vector3.zero;

        switch (direction)
        {
            case -1:
                force = _backwardsForce;
                torque = _backwardsTorque;
                break;
            case 0:
                force = _backwardsForce * _brickBombMultiplier;
                torque = _backwardsTorque * _brickBombMultiplier;
                break;
            case 1:
                force = _forwardForce;
                torque = _forwardTorque;
                break;
        }
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(force, ForceMode.Impulse);
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddTorque(torque, ForceMode.Acceleration);
    }

    private IEnumerator ChangeSize(Vector3 sizeChange)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 target = new Vector3(initialScale.x * sizeChange.x, initialScale.y * sizeChange.y, initialScale.z * sizeChange.z);

        for (float time = 0; time < _powerUpDuration ; time += Time.deltaTime)
        {
            float progress = time/ _powerUpDuration;
            transform.localScale = Vector3.Lerp(initialScale, target, progress);
            yield return null;
        }

        transform.localScale = target;
    }
}

