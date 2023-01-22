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
    public TMPro.TextMeshProUGUI Text_Speed = null;  // 速度表示するテキストオブジェクト
    public TMPro.TextMeshProUGUI Text_Info = null;  // 情報を表示するテキストオブジェクト
    private bool isstart_ = false;
    private bool ismid_ = false;
    private int coursecount_ = 0;
    private float time_start_ = 0;
    private float time_prev_ = 0;

    public void OnGoal()
    {
        if (this.isstart_ && this.ismid_)
        {
            this.ismid_ = false;
            this.isstart_ = false;
            this.coursecount_++;
            this.time_prev_ = Time.time - this.time_start_;
            this.time_start_ = Time.time;
        }
        else
        {
            this.isstart_ = true;
            this.time_start_ = Time.time;
        }
    }

    public void OnMid()
    {
        if (this.isstart_)
        {
            this.ismid_ = true;
        }
    }

    public float GetSpeedKPH()
    {
        if (this.PossessObject != null && this.PossessObject.GetComponent<Rigidbody>() != null)
        {
            var speed_ = this.PossessObject.GetComponent<Rigidbody>().velocity.sqrMagnitude;
            return (speed_ * 3600) / 1000;
        }
        return 0.0f;
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

    void UpdateTexts()
    {
        if (this.Text_Speed != null)
        {
            var txt_speed_ = "";
            txt_speed_ += $"SPEED:  {this.GetSpeedKPH()}KM/H";
            this.Text_Speed.text = txt_speed_;
        }

        if (this.Text_Info != null)
        {
            var txt_info_ = "";
            txt_info_ += $"Time: {Time.time - this.time_start_}s\n";
            txt_info_ += $"Time(Previous): {this.time_prev_}s\n";
            txt_info_ += $"CourseCount: {this.coursecount_}\n";
            this.Text_Speed.text = txt_info_;
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
