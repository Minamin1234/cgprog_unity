using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGoalLine : LineSensor
{
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
        var player = this.Player.GetComponent<Player>();
        player.OnGoal();
    }
}
