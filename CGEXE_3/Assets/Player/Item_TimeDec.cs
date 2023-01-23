using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TimeDec : MonoBehaviour
{
    public Player player = null;
    
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
        if (this.player != null)
        {
            this.player.OnGetItem();
            Destroy(this.gameObject);
        }
    }
}
