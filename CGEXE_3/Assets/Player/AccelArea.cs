using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelArea : MonoBehaviour
{
    public float Accel = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Vehicle>() != null)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                Debug.Log("Accel");
                var rb = other.gameObject.GetComponent<Rigidbody>();
                var vel = rb.velocity;
                rb.AddForce(vel * this.Accel, ForceMode.Impulse);
            }
        }
    }
}
