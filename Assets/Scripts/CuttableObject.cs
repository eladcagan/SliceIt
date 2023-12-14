using System.Collections;
using TMPro;
using UnityEngine;

public class CuttableObject : MonoBehaviour
{
    [SerializeField]
    private int _cutValue;
    [SerializeField]
    private ParticleSystem _cutPS;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private float _cutForce;
    [SerializeField]
    private float _textDelay = 1f;

    private Rigidbody[] _rigidbodies;
    private Collider _collider;

    public int CutValue
    {
        get
        {
            return _cutValue;
        }
    }

  

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.BLADE))
        {
            foreach (var part in _rigidbodies)
            {
                _collider.enabled = false;
                part.isKinematic = false;
                _cutPS.Play();
                if (_cutValue > 0)
                {
                    _scoreText.text = "+" + _cutValue.ToString();
                    StartCoroutine(TextDelay(_textDelay));
                }
                part.AddExplosionForce(_cutForce, part.position, 1);
            }
        }
    }

    private IEnumerator TextDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _scoreText.gameObject.SetActive(false);
    }

}
