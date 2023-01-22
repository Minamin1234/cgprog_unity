using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]


public class Player : MonoBehaviour
{
    public List<GameObject> Vehicles = new List<GameObject>();
    public GameObject PossessObject = null;  // 所有している乗り物(乗っている乗り物)
    public Canvas MainUI = null;
    public Canvas MenuUI = null;
    public TMPro.TextMeshProUGUI Text_Speed = null;  // 速度表示するテキストオブジェクト
    public TMPro.TextMeshProUGUI Text_Info = null;  // 情報を表示するテキストオブジェクト
    public TMPro.TMP_Dropdown Dropdown_Vehicles = null;
    private bool isstart_ = false;
    private bool ismid_ = false;
    private int coursecount_ = 0;
    private float time_start_ = 0;
    private float time_prev_ = 0;
    private bool ismenu_ = false;
    private Canvas currentui_ = null;

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

    public void OnButtonVehicleChangeClicked()
    {
        
    }

    public void OnButtonTitleClicked()
    {
        
    }

    public void OnButtonExitClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void OnDropdownVehiclesChanged()
    {
        var selected = 0;
        if (this.Dropdown_Vehicles != null)
        {
            selected = this.Dropdown_Vehicles.value;
            Debug.Log(selected);
        }
    }

    public float GetSpeedKPH()
    {
        if (this.PossessObject != null && this.PossessObject.GetComponent<Rigidbody>() != null)
        {
            var speed_ = this.PossessObject.GetComponent<Rigidbody>().velocity.sqrMagnitude;
            return (speed_ * 3600) / 10000;
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
            txt_speed_ += $"SPEED:  {this.GetSpeedKPH():F1}KM/H";
            this.Text_Speed.text = txt_speed_;
        }

        if (this.Text_Info != null)
        {
            var txt_info_ = "";
            txt_info_ += $"Time: {Time.time - this.time_start_}s\n";
            txt_info_ += $"Time(Previous): {this.time_prev_}s\n";
            txt_info_ += $"CourseCount: {this.coursecount_}\n";
            this.Text_Info.text = txt_info_;
        }
    }

    bool ShowMenu(bool show)
    {
        if (show)
        {
            this.MainUI.gameObject.SetActive(false);
            this.MenuUI.gameObject.SetActive(true);
        }
        else
        {
            this.MainUI.gameObject.SetActive(true);
            this.MenuUI.gameObject.SetActive(false);
        }

        return show;
    }

    void ShowUI(Canvas newui)
    {
        this.MenuUI.gameObject.SetActive(false);
        currentui_ = newui;
        currentui_.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.PossessObject != null)
        {
            this.gameObject.transform.localPosition = this.PossessObject.transform.localPosition;
            this.UpdateTexts();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.ismenu_ = this.ShowMenu(!this.ismenu_);
        }
    }

    private void FixedUpdate()
    {
    }
}
