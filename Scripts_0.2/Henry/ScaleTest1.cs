using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ScaleTest1 : MonoBehaviour
{
    //public float scale = .01f;
    //int scaleValue = 0;
    string scaleValue = "";

    static Socket listener;
    private CancellationTokenSource source;
    public ManualResetEvent allDone;

        ScaleTest1()
    {
        source = new CancellationTokenSource();
        allDone = new ManualResetEvent(false);
    }
    // Start is called before the first frame update
    // void Start()
    // {
    
    // }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(Input.GetKey("1"))
        // {
        //     transform.localScale = transform.localScale + new Vector3(0, -scale,0);
        // }
        //bool isSmall = GetBool(transform.localScale);
        
        if(scaleValue.Equals("case1"))
        {
            transform.localScale = transform.localScale + new Vector3(0, -0.001f,-0.001f);
        }
        if(scaleValue.Equals("case2")){
            transform.localScale = transform.localScale + new Vector3(-0.01f, -0.01f,0);
        }
        if(scaleValue.Equals("case3")){
            transform.localScale = transform.localScale + new Vector3(-0.004f, -0.01f,0);
        }
        if(scaleValue.Equals("case4")){
            transform.localScale = transform.localScale + new Vector3(-0.004f, -0.005f,0);
        }
        if(scaleValue.Equals("case5")){
            transform.localScale = transform.localScale + new Vector3(-0.004f, -0.008f,0);
        }
    }
    public string setScale (string data)
    {
        scaleValue = data;
        return data;
    }
}
