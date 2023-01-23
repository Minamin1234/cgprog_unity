using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]


public class Player : MonoBehaviour
{
    public List<GameObject> Vehicles = new List<GameObject>();
    public GameObject PossessObject = null;  // 所有している乗り物(乗っている乗り物)
    public Canvas MainUI = null;
    public Canvas MenuUI = null;
    public Canvas VehicleSelectUI = null;
    public Canvas EndUI = null;
    public TMPro.TextMeshProUGUI Text_Speed = null;  // 速度表示するテキストオブジェクト
    public TMPro.TextMeshProUGUI Text_Info = null;  // 情報を表示するテキストオブジェクト
    public TMPro.TextMeshProUGUI Text_EndUI_Info = null;
    public TMPro.TMP_Dropdown Dropdown_Vehicles = null;
    public string TitleScene = "";
    public string GameScene = "";
    private bool isstart_ = false;
    private bool ismid_ = false;
    private int coursecount_ = 0;
    private float time_start_ = 0;
    private float time_prev_ = 0;
    private bool ismenu_ = false;
    private Canvas currentui_ = null;
    private int selectedvehicle_ = 0;
    private bool isend_ = false;
    
    /// <summary>
    /// スタート/ゴール線通過イベント
    /// </summary>
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
    
    /// <summary>
    /// 中継線の通過イベント
    /// </summary>
    public void OnMid()
    {
        if (this.isstart_)
        {
            this.ismid_ = true;
        }
    }
    
    /// <summary>
    /// ゲーム終了する際の処理
    /// </summary>
    public void OnEnd()
    {
        Time.timeScale = 0.0f;
        this.ShowUI(this.EndUI);
        this.isend_ = true;
    }
    
    /// <summary>
    /// リトライする際の処理
    /// </summary>
    public void OnRetry()
    {
        if (this.GameScene != string.Empty)
        {
            SceneManager.LoadScene(this.GameScene, LoadSceneMode.Single);
            this.Pause(false);
        }
    }

    public void OnGetItem()
    {
        this.time_start_ += 5;
    }
    
    /// <summary>
    /// VehicleChangeボタンが押された際のイベント
    /// </summary>
    public void OnButtonVehicleChangeClicked()
    {
        this.Dropdown_Vehicles.value = this.selectedvehicle_;
        if (this.VehicleSelectUI != null)
        {
            this.ShowUI(this.VehicleSelectUI);
        }
    }
    
    /// <summary>
    /// Titleボタンが押された際のイベント
    /// </summary>
    public void OnButtonTitleClicked()
    {
        if (this.TitleScene == string.Empty) return;
        SceneManager.LoadScene(this.TitleScene, LoadSceneMode.Single);
    }
    
    /// <summary>
    /// Exitボタンが押された際のイベント
    /// </summary>
    public void OnButtonExitClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    /// <summary>
    /// Changeボタンが押された際のイベント
    /// </summary>
    public void OnButtonChangeClicked()
    {
        this.selectedvehicle_ = this.Dropdown_Vehicles.value;
        var vehicle_ = this.Vehicles[this.selectedvehicle_];
        this.Possess(vehicle_);
        this.time_start_ = Time.time;
        this.isstart_ = false;
        this.ismid_ = false;
        this.coursecount_ = 0;
    }
    
    /// <summary>
    /// 所有している乗り物の速度(km/h)を取得する
    /// </summary>
    /// <returns>速度(km/h)</returns>
    public float GetSpeedKPH()
    {
        if (this.PossessObject != null && this.PossessObject.GetComponent<Rigidbody>() != null)
        {
            var speed_ = this.PossessObject.GetComponent<Rigidbody>().velocity.sqrMagnitude;
            return (speed_ * 3600) / 10000;
        }
        return 0.0f;
    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (this.PossessObject == null) return;
        var p = this.PossessObject.GetComponent<Pawn>();
        p.Possess();
        this.ShowUI(this.MainUI);
        this.Pause(false);
    }
    
    /// <summary>
    /// 現在所有しているオブジェクトを手放して指定したオブジェクトを所有する
    /// </summary>
    /// <param name="newobject">所有するオブジェクト</param>
    void Possess(GameObject newobject)
    {
        if (newobject != null && newobject.GetComponent<Vehicle>() != null)
        {
            if (this.PossessObject != null && this.PossessObject.GetComponent<Vehicle>() != null)
            {
                this.PossessObject.GetComponent<Vehicle>().UnPossess();
            }
            this.PossessObject = newobject;
            this.PossessObject.GetComponent<Vehicle>().Possess();
        }
    }
    
    /// <summary>
    ///  UIのテキストを更新する
    /// </summary>
    void UpdateTexts()
    {
        if (this.Text_Speed != null)
        {
            var txt_speed_ = "";
            txt_speed_ += $"SPEED:  {this.GetSpeedKPH():F1}KM/H";
            this.Text_Speed.text = txt_speed_;
        }

        if (this.Text_Info != null && this.Text_EndUI_Info != null)
        {
            var txt_info_ = "";
            txt_info_ += $"Time: {Time.time - this.time_start_}s\n";
            txt_info_ += $"Time(Previous): {this.time_prev_}s\n";
            txt_info_ += $"CourseCount: {this.coursecount_}\n";
            this.Text_Info.text = txt_info_;
            this.Text_EndUI_Info.text = txt_info_;
        }
    }
    
    /// <summary>
    /// MenuUIを表示する
    /// </summary>
    /// <param name="show">Menu表示するかどうか</param>
    /// <returns>Menu表示するかどうか</returns>
    public void ShowMenu(bool show)
    {
        if (show)
        {
            if (this.currentui_ != null)
            {
                this.currentui_.gameObject.SetActive(false);
            }

            if (this.MenuUI != null)
            {
                this.currentui_ = this.MenuUI;
                this.currentui_.gameObject.SetActive(true);
            }
        }
        else
        {
            if (this.currentui_ != null)
            {
                this.currentui_.gameObject.SetActive(false);
            }

            if (this.MainUI != null)
            {
                if (this.EndUI != null && this.isend_)
                {
                    this.currentui_ = this.EndUI;
                }
                else
                {
                    this.currentui_ = this.MainUI;
                }
                this.currentui_.gameObject.SetActive(true);
            }
        }
        this.Pause(show);
        this.ismenu_ = show;
    }
    
    /// <summary>
    /// 指定したUIを表示させる
    /// </summary>
    /// <param name="newui"></param>
    void ShowUI(Canvas newui)
    {
        if (newui == null) return;
        if (this.currentui_ != null)
        {
            this.currentui_.gameObject.SetActive(false);
        }
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
            this.ShowMenu(!this.ismenu_);
        }
    }

    private void FixedUpdate()
    {
    }
}
