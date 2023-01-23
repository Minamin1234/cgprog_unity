using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeArea : MonoBehaviour
{
    public float Decay = 0.5f;
    
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
        Debug.Log("Enter");
        if (other.gameObject.GetComponent<Vehicle>() != null)
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                var rb = other.gameObject.GetComponent<Rigidbody>();
                var vel = rb.velocity;
                rb.AddForce(-vel * this.Decay, ForceMode.Impulse);
            }
        }
    }
}
