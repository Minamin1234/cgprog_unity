using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム内での操作可能なクラス
/// </summary>
public class Pawn : MonoBehaviour
{
    [SerializeField]
    private bool isactive_ = false;

    public bool IsActive
    {
        get { return this.isactive_; }
        set { this.isactive_ = value; }
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
        if (this.isactive_)
        {
            this.ActivatedUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (this.isactive_)
        {
            this.ActivatedFixedUpdate();
        }
    }
}
