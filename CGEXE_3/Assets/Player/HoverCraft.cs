using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverCraft : Vehicle
{
    public float TurnSpeed = 1500.0f;
    public float BreakSpeed = 0.5f;
    public float MoveSpeed
    {
        get { return this.maxtorque_; }
        set { this.maxtorque_ = value; }
    }
    
    protected override void MoveInput(Vector2 input)
    {
        base.MoveInput(input);
        var mouseinput = new Vector2(0, 0);
        mouseinput.x = Input.GetAxis("Mouse X");
        mouseinput.y = Input.GetAxis("Mouse Y");
        var v1 = this.transform.forward * (this.MoveSpeed * input.y * Time.deltaTime);
        var v2 = this.transform.right * (this.MoveSpeed * input.x * Time.deltaTime);
        var v4 = this.transform.up * (this.TurnSpeed * mouseinput.x * Time.deltaTime);
       
        var rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.Space))
        {
            var v3 = rb.velocity;
            rb.AddForce(-v3 * this.BreakSpeed);
        }
        else
        {
            rb.AddForce(v1);
            rb.AddForce(v2);
            rb.AddTorque(v4);
        }
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
