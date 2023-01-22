using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class AttackHeli : Vehicle
{
    public GameObject Rotor = null;
    public List<GameObject> SubRotors = new List<GameObject>();
    public float BaseRotorRotateSpeed = 1000.0f;
    public float MaxRotorRotateSpeed = 2000.0f;
    public Vector3 MaxRotateSpeed = new Vector3(1000, 250, 150);

    public float MaxUpSpeed
    {
        get { return this.maxtorque_; }
        set { this.maxtorque_ = value; }
    }

    protected override void VehicleUpdate()
    {
        base.VehicleUpdate();
    }

    protected virtual void RotorRotate(float input, float speed)
    {
        if (this.Rotor != null)
        {
            var rot = this.Rotor.transform.localRotation.eulerAngles;
            rot.y += speed * Time.deltaTime;
            this.Rotor.transform.localRotation = Quaternion.Euler(rot);
            foreach (var sr in this.SubRotors)
            {
                var srrot = sr.transform.localRotation.eulerAngles;
                srrot.x += (speed * 0.15f) * Time.deltaTime;
                sr.transform.localRotation = Quaternion.Euler(srrot);
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void MoveInput(Vector2 input)
    {
        base.MoveInput(input);
        Vector2 mouseinput = new Vector2(0, 0);
        mouseinput.x = Input.GetAxis("Mouse X");
        mouseinput.y = Input.GetAxis("Mouse Y");
        var rb = this.GetComponent<Rigidbody>();
        
        if (input.y != 0)
        {
            this.RotorRotate(1, this.MaxRotorRotateSpeed);
        }
        else
        {
            this.RotorRotate(1, this.BaseRotorRotateSpeed);
        }

        var v1 = new Vector3(0, 0, 0);
        v1 = this.transform.up;
        v1 = v1 * (this.MaxUpSpeed * input.y * Time.deltaTime);
        var v2 = new Vector3(0, 0, 0);
        v2 = this.transform.forward;
        v2 = v2 * (this.MaxRotateSpeed.y * -mouseinput.x * Time.deltaTime);
        var v3 = this.transform.right;
        v3 = v3 * (this.MaxRotateSpeed.x * mouseinput.y * Time.deltaTime);
        var v4 = this.transform.up;
        v4 = v4 * (this.MaxRotateSpeed.y * input.x * Time.deltaTime);
        rb.AddForce(v1);
        rb.AddTorque(v2);
        rb.AddTorque(v3);
        rb.AddTorque(v4);
    }

    // Update is called once per frame
    void Update()
    {
        this.VehicleUpdate();
    }
}
