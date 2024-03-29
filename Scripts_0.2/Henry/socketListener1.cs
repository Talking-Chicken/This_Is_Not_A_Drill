using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class socketListener1 : MonoBehaviour
{
    public ScaleTest1 ScaleChanger;
    public ChangeColor2 colorChanger;
    static Socket listener;
    private CancellationTokenSource source;
    public ManualResetEvent allDone;
    public Renderer objectRenderer;
    private Color matColor;
    public Animator animator;
    public animationController2 animationChanger;
    public static readonly int PORT = 1755;
    public static readonly int WAITTIME = 1;

    private bool hasSearchedAnimationChanger = false;
    private bool hasSearchedScaleChanger = false;
    
    private GameManager gameManager;


    socketListener1()
    {
        source = new CancellationTokenSource();
        allDone = new ManualResetEvent(false);
    }

    // Start is called before the first frame update
    async void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        animator = GetComponent<Animator>();
        //objectRenderer = GetComponent<Renderer>();
        gameManager = FindObjectOfType<GameManager>();
        await Task.Run(() => ListenEvents(source.Token)); 
    }

    // // Update is called once per frame
    void Update()
    {
        //objectRenderer.material.color = matColor;
        if (SceneManager.GetActiveScene().name.Equals("scene3")) {
            if (!hasSearchedAnimationChanger) {
                animationChanger = FindObjectOfType<animationController2>();
                hasSearchedAnimationChanger = true;
            }
            if (!hasSearchedScaleChanger) {
                ScaleChanger = FindObjectOfType<ScaleTest1>();
                hasSearchedScaleChanger = true;
            }
        } else {
            hasSearchedAnimationChanger = false;
            hasSearchedScaleChanger = false;
        }
    }

    private void ListenEvents(CancellationToken token)
    {

        
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);

         
        listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

         
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

             
            while (!token.IsCancellationRequested)
            {
                allDone.Reset();

                print("Waiting for a connection2... host :" + ipAddress.MapToIPv4().ToString() + " port : " + PORT);
                listener.BeginAccept(new AsyncCallback(AcceptCallback),listener);

                while(!token.IsCancellationRequested)
                {
                    if (allDone.WaitOne(WAITTIME))
                    {
                        break;
                    }
                }
      
            }

        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }

    void AcceptCallback(IAsyncResult ar)
    {  
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);
 
        allDone.Set();
  
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
    }

    void ReadCallback(IAsyncResult ar)
    {
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        int read = handler.EndReceive(ar);
  
        if (read > 0)
        {
            state.colorCode.Append(Encoding.ASCII.GetString(state.buffer, 0, read));
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }
        else
        {
            if (state.colorCode.Length >= 1)
            { 
                Debug.Log("There's something");
                string contents = state.colorCode.ToString();
                
                //print($"Read {contents.Length} bytes from socket.\n Data : {contents}");
                //print(contents);
                // string[] colors = contents.Split(',')[1].Split('.');
                // string animation = contents.Split(',')[0];
                // string scale = contents.Split(',')[2];
                // //print("Color: " + string.Join(".", colors) + ", animation: " + animation);
                // print("Color: " + string.Join(".", colors) + ", animation: " + animation + ", scale: " + scale);
                // colorChanger.setColor(colors);
                // ScaleChanger.setScale(scale);
                //string tmp = "";
                animationChanger.debugOutput();
                animationChanger.SetAnimation(contents);
                Debug.Log("after to set animation");
                ScaleChanger.setScale(contents);
                Debug.Log("after to set scale");
                Debug.Log("content is " + contents);
                if (gameManager != null)
                    gameManager.RecievedCardID = contents;
                else
                    Debug.Log("Missing Game Manager");
            }
            handler.Close();
        }
    }

    private void OnDestroy()
    {
        source.Cancel();
    }

    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder colorCode = new StringBuilder();
    }
}