using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム内での操作可能なクラス
/// </summary>
public class Pawn : MonoBehaviour
{
    public bool isactive_ = false;
    public Camera maincamera = null;
    public List<Camera> cameras = new List<Camera>();
    protected Camera camera_ = null;

    public bool IsActive
    {
        get { return this.isactive_; }
        set { this.isactive_ = value; }
    }

    public virtual void Possess()
    {
        this.isactive_ = true;
        this.maincamera.gameObject.SetActive(true);
        this.camera_ = this.maincamera;
    }

    public virtual void UnPossess()
    {
        this.isactive_ = false;
        this.maincamera.gameObject.SetActive(false);
        foreach (var c in this.cameras)
        {
            c.gameObject.SetActive(false);
        }
    }

    public virtual void MainView()
    {
        this.maincamera.gameObject.SetActive(true);
        foreach (var c in this.cameras)
        {
            c.gameObject.SetActive(false);
        }
    }

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
