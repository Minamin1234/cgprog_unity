using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class AttackHeli : Vehicle
{
    public GameObject Rotor = null;
    public float BaseRotateSpeed = 1000.0f;
    public float MaxRotateSpeed = 5000.0f;

    protected override void VehicleUpdate()
    {
        base.VehicleUpdate();
        if (this.Rotor != null)
        {
            var rot = this.Rotor.transform.eulerAngles;
            rot.y += this.RotateSpeed * Time.deltaTime;
            this.Rotor.transform.eulerAngles = rot;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void MoveInput(Vector2 input)
    {
        base.MoveInput(input);
        
    }

    // Update is called once per frame
    void Update()
    {
        this.VehicleUpdate();
    }
}
