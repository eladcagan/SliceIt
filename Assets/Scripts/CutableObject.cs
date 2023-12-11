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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blade"))
        {
            //_completeModel.GetComponent<Collider>().isTrigger = true;
            Debug.Log(other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Blade"))
        {

            var leftrb = _leftPart.GetComponent<Rigidbody>();
            var rightrb = _rightPart.GetComponent<Rigidbody>();
            _leftPart.SetActive(true);
            _rightPart.SetActive(true);
            _completeModel.GetComponent<MeshRenderer>().enabled = false;
            leftrb.AddForce(new Vector3(0, 0, 20));
            rightrb.AddForce(new Vector3(0, 0, 20));
        }
    }
}
