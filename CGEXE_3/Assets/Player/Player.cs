using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class Player : MonoBehaviour
{
    public List<WheelShaft> Shafts;
    public float maxTorque = 1.0f;
    public float maxSteerAngle = 30.0f;

    private Vector3 axis;

    public void RotateWheelModel(WheelCollider wcollider)
    {
        Transform wheel = wcollider.transform.GetChild(0);
        Vector3 position;
        Quaternion rotation;
        wcollider.GetWorldPose(out position, out rotation);
        
        wheel.transform.position = position;
        wheel.transform.rotation = rotation;
    }

    void Move(Vector3 axisxy)
    {
        float speed = this.maxTorque * axisxy.y;
        float steer = this.maxSteerAngle * axisxy.x;
        foreach (var ws in this.Shafts)
        {
            if (ws.IsSteer)
            {
                ws.WheelLeft.steerAngle = steer;
                ws.WheelRight.steerAngle = steer;
            }

            if (ws.IsDrive)
            {
                ws.WheelLeft.motorTorque = speed;
                ws.WheelRight.motorTorque = speed;
            }
            this.RotateWheelModel(ws.WheelLeft);
            this.RotateWheelModel(ws.WheelRight);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.axis.y = 1.0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.axis.y = -1.0f;
        }
        else
        {
            this.axis.y = 0.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.axis.x = -1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.axis.x = 1.0f;
        }
        else
        {
            this.axis.x = 0.0f;
        }
        this.Move(this.axis);
    }
}
