using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

[System.Serializable]
public class WheelShaft
{
    public WheelCollider WheelLeft;
    public WheelCollider WheelRight;
    public bool IsDrive = false;
    public bool IsSteer = false;
}

public class Car : Vehicle
{
    public List<WheelShaft> Shafts;
    public float maxSteerAngle = 30.0f;

    protected void RotateWheelModel(WheelCollider wc)
    {
        Transform w = wc.transform.GetChild(0);
        Vector3 pos;
        Quaternion rot;
        wc.GetWorldPose(out pos, out rot);

        w.transform.position = pos;
        w.transform.rotation = rot;
    }

    protected override void MoveInput(Vector2 input)
    {
        base.MoveInput(input);
        var torque = this.maxtorque_ * input.y;
        var steer = this.maxSteerAngle * input.x;
        foreach (var ws in this.Shafts)
        {
            if (ws.IsDrive)
            {
                ws.WheelLeft.motorTorque = torque;
                ws.WheelRight.motorTorque = torque;
            }

            if (ws.IsSteer)
            {
                ws.WheelLeft.steerAngle = steer;
                ws.WheelRight.steerAngle = steer;
            }
            this.RotateWheelModel(ws.WheelLeft);
            this.RotateWheelModel(ws.WheelRight);
        }
        Debug.Log("Active");
    }

    protected override void VehicleUpdate()
    {
        base.VehicleUpdate();
        if (this.isactive_)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                this.MainView();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                this.ChangeView(0);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.VehicleUpdate();
    }
}
