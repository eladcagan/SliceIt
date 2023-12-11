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

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Blade"))
        {
            _completeModel.GetComponent<Collider>().isTrigger = true;
            _completeModel.GetComponent<Rigidbody>().useGravity = false;
            Debug.Log(other.collider.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blade"))
        {

            var leftrb = _leftPart.GetComponent<Rigidbody>();
            var rightrb = _rightPart.GetComponent<Rigidbody>();
            _leftPart.SetActive(true);
            _rightPart.SetActive(true);
            _completeModel.GetComponent<MeshRenderer>().enabled = false;
            leftrb.AddForce(new Vector3(5, 0, 0));
            rightrb.AddForce(new Vector3(-5, 0, 0));
        }
    }
}
