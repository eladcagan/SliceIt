using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _completeModel;
    [SerializeField]
    private GameObject _leftPart;
    [SerializeField]
    private GameObject _rightPart;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Blade"))
        {
            _completeModel.GetComponent<MeshRenderer>().enabled = false;
            _leftPart.SetActive(true);
            _rightPart.SetActive(true);
            _leftPart.GetComponent<Rigidbody>().AddForce(Vector3.left/ 10000, ForceMode.Acceleration);
            _rightPart.GetComponent<Rigidbody>().AddForce(Vector3.right/ 10000, ForceMode.Acceleration);
            }
        }
    }
