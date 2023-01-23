using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacle : MonoBehaviour
{
    public float RotationSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rot = this.transform.rotation.eulerAngles;
        rot.y += this.RotationSpeed * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(rot);
    }
}
