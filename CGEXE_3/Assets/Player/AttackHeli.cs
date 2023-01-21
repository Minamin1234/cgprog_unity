using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHeli : Vehicle
{
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
