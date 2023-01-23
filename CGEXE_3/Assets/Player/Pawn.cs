using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ゲーム内での操作可能なクラス
/// </summary>
public class Pawn : MonoBehaviour
{
    public bool isactive_ = false;  // このオブジェクトを所有していて有効かどうか
    public Camera maincamera = null;  // メインカメラ
    public List<Camera> cameras = new List<Camera>();  // サブカメラ一覧
    protected Camera camera_ = null;  // 現在稼働しているカメラ

    public bool IsActive
    {
        get { return this.isactive_; }
        set { this.isactive_ = value; }
    }
    
    /// <summary>
    /// そのオブジェクトを所有します
    /// </summary>
    public virtual void Possess()
    {
        this.isactive_ = true;
        this.maincamera.gameObject.SetActive(true);
        this.camera_ = this.maincamera;
    }
    
    /// <summary>
    /// そのオブジェクトを手放す
    /// </summary>
    public virtual void UnPossess()
    {
        this.isactive_ = false;
        this.maincamera.gameObject.SetActive(false);
        foreach (var c in this.cameras)
        {
            c.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// MainUIに設定する
    /// </summary>
    public virtual void MainView()
    {
        this.maincamera.gameObject.SetActive(true);
        foreach (var c in this.cameras)
        {
            c.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// 指定したインデックスのUIに切り替える
    /// </summary>
    /// <param name="to">切り替えるUIリストのインデックス</param>
    public virtual void ChangeView(int to)
    {
        try
        {
            if (this.camera_ != null)
            {
                this.camera_.gameObject.SetActive(false);
            }
            this.camera_ = this.cameras[to];
            this.camera_.gameObject.SetActive(true);
            Debug.Log(this.camera_);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    /// <summary>
    /// 有効化されている状態での更新処理(Update)
    /// </summary>
    protected virtual void ActivatedUpdate()
    {
        
    }
    
    /// <summary>
    /// 有効化されている更新処理(FixedUpdate)
    /// </summary>
    protected virtual void ActivatedFixedUpdate()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
    }
}
