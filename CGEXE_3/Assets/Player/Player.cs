using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEditor.UI;
using UnityEngine;

[System.Serializable]


public class Player : MonoBehaviour
{
    public GameObject PossessObject = null;  // 所有している乗り物(乗っている乗り物)
    private bool isstart_ = false;
    private bool ismid_ = false;
    private int coursecount_ = 0;

    public void OnGoal()
    {
        if (this.isstart_ && this.ismid_)
        {
            this.ismid_ = false;
            this.isstart_ = false;
            this.coursecount_++;
            Debug.Log(this.coursecount_);
        }
        else
        {
            this.isstart_ = true;
        }
    }

    public void OnMid()
    {
        if (this.isstart_)
        {
            this.ismid_ = true;
        }
    }
    
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
