using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

[System.Serializable]


public class Player : MonoBehaviour
{
    public GameObject PossessObject = null;
    
    // Start is called before the first frame update
    void Start()
    {
        if (this.PossessObject == null) return;
        var p = this.PossessObject.GetComponent<Pawn>();
        p.Possess();
    }

    void Possess(GameObject newobject)
    {
        if (newobject != null)
        {
            this.PossessObject = newobject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.PossessObject != null)
        {
            this.gameObject.transform.localPosition = this.PossessObject.transform.localPosition;
        }
    }

    private void FixedUpdate()
    {
    }
}
