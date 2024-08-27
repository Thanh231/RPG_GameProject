using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformTemp : MonoBehaviour
{
    private float timeValue = 2;
    public float dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeValue -= Time.deltaTime;
        if (timeValue < 0) 
        {
            ChangeDir();
        }
        transform.position += new Vector3(0, dir, 0) * 3f * Time.deltaTime;
    }

    public void ChangeDir()
    {
        dir *= -1;
        timeValue = 2f;
    }
}
