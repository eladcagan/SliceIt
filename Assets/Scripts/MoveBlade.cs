using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBlade : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force;
    [SerializeField] private float rotationForce;

    [Header("Physics")]
    [SerializeField] private Vector3 maxVelocity;
    [SerializeField] private float maxAngularVelocity;
    [SerializeField] private Vector2 minMaxAngleToFlip;

    private void Move()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce((-Vector3.right * force) + Vector3.up * 5, ForceMode.Impulse);
        float zRot = transform.eulerAngles.z;
        if (zRot < 0)
        {
            zRot += 360;
        }

        rb.AddTorque(transform.forward * rotationForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            float zRot = transform.eulerAngles.z;
            if (zRot < 0)
            {
                zRot += 360;
            }
            Move();
        }

        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxVelocity.x, maxVelocity.x), Mathf.Clamp(rb.velocity.y, -maxVelocity.y, maxVelocity.y), 0);
    }
}
