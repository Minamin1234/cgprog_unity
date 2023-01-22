using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

/// <summary>
/// プレイヤーが操作可能な乗り物クラス
/// </summary>
public class Vehicle : Pawn
{
    [SerializeField]
    protected float maxtorque_ = 0.0f;

    protected virtual void MoveInput(Vector2 input)
    {
        
    }

    protected virtual void VehicleUpdate()
    {
        if (this.IsActive)
        {
            var input = new Vector2(0, 0);
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            this.MoveInput(input);
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
