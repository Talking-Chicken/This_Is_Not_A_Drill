using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


public class ChangeColor2 : MonoBehaviour
{
    private CancellationTokenSource source;
    public ManualResetEvent allDone;
    public Renderer objectRenderer;

    ChangeColor2()
    {
        source = new CancellationTokenSource();
        allDone = new ManualResetEvent(false);
    }

    // void Start()
    // {
    //     if (objectRenderer == null)
    //         Debug.Log("obj is null start");
    //     //objectRenderer = GetComponent<Renderer>();
    //     //await Task.Run(() => ListenEvents(source.Token));   
    // }

        void Update()
    {   
        objectRenderer.material.color = matColor;
    }

    Color matColor = new Color();
    public void setColor (string[] colors) 
    {
        
        matColor = new Color()
        {   
            r = float.Parse(colors[0]) / 255.0f,
            g = float.Parse(colors[1]) / 255.0f,
            b = float.Parse(colors[2]) / 255.0f,
            a = float.Parse(colors[3]) / 255.0f
        };

        /*
        string[] colors = data.Split(',');
        matColor = new Color()
        {   
            r = float.Parse(colors[0]) / 255.0f,
            g = float.Parse(colors[1]) / 255.0f,
            b = float.Parse(colors[2]) / 255.0f,
            a = float.Parse(colors[3]) / 255.0f
        };
        */

    }
}