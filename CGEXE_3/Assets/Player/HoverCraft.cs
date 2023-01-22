using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCraft : Vehicle
{
    public float MoveSpeed
    {
        get { return this.maxtorque_; }
        set { this.maxtorque_ = value; }
    }
    
    protected override void MoveInput(Vector2 input)
    {
        base.MoveInput(input);
        var v1 = this.transform.forward * (this.MoveSpeed * input.y * Time.deltaTime);
        var v2 = this.transform.right * (this.MoveSpeed * input.x * Time.deltaTime);
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(v1);
        rb.AddForce(v2);
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
