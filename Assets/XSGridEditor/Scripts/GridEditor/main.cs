using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XSSLG;

[DefaultExecutionOrder(-1000)]
public class main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 最先初始化单例，保证使用时肯定是初始化好的
        if (UnityUtils.IsEditor())
        {
            var instance = XSEditorInstance.Instance;
        }
        else
        {
            var instance = XSInstance.Instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
