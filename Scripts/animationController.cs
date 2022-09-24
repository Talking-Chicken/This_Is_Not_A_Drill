using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class animationController : MonoBehaviour
{
    static Socket listener;
    private CancellationTokenSource source;
    public ManualResetEvent allDone;
    public Animator animator;

    int animationValue = 0;

    public static readonly int PORT = 1755;
    public static readonly int WAITTIME = 1;
    
    int isJumpingHash;
    int isVictoriousHash;
    int isNoddingHash;
    int isAnnoyedHash;
    int isPoutingHash;
    int isDefeatedHash;
    int isDisappointedHash;

    animationController()
    {
        source = new CancellationTokenSource();
        allDone = new ManualResetEvent(false);
    }

    // Start is called before the first frame update
    async void Start()
    {        
        animator = GetComponent<Animator>();
        
        isJumpingHash = Animator.StringToHash("isJumping");
        isVictoriousHash = Animator.StringToHash("isVictorious");
        isNoddingHash = Animator.StringToHash("isNodding");
        isAnnoyedHash = Animator.StringToHash("isAnnoyed");
        isPoutingHash = Animator.StringToHash("isPouting");
        isDefeatedHash = Animator.StringToHash("isDefeated");
        isDisappointedHash = Animator.StringToHash("isDisappointed");

        await Task.Run(() => ListenEvents(source.Token));   

    }

    // Update is called once per frame
    void Update()
    {
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isVictorious = animator.GetBool(isVictoriousHash);
        bool isNodding = animator.GetBool(isNoddingHash);
        bool isAnnoyed = animator.GetBool(isAnnoyedHash);
        bool isPouting = animator.GetBool(isPoutingHash);
        bool isDefeated = animator.GetBool(isDefeatedHash);
        bool isDisappointed = animator.GetBool(isDisappointedHash);

        // 1 Jumping
        if (!isJumping && animationValue == 1)
        {
            animator.SetBool(isJumpingHash, true);
            //play audio - "fantastic - keep up the amazing work"
        }
        if (isJumping && animationValue != 1)
        {
            animator.SetBool(isJumpingHash, false);
        }

        // 2 HardNodding
        if (!isVictorious && animationValue == 2)
        {
            animator.SetBool(isVictoriousHash, true);
            //play audio - "great job"
        }
        if (isVictorious && animationValue != 2)
        {
            animator.SetBool(isVictoriousHash, false);
        }

        // 3 Nodding
        if (!isNodding && animationValue == 3)
        {
            animator.SetBool(isNoddingHash, true);
            //play audio - "keep it up! we're getting there"
        }
        if (isNodding && animationValue != 3)
        {
            animator.SetBool(isNoddingHash, false); 
        }

        // 4 Annoyed
        if (!isAnnoyed && animationValue == 4)
        {
            animator.SetBool(isAnnoyedHash, true);
            //play audio - "you can do better"
        }
        if (isAnnoyed && animationValue != 4)
        {
            animator.SetBool(isAnnoyedHash, false);
        }

        // 5 Pouting
        if (!isPouting && animationValue == 5)
        {
            animator.SetBool(isPoutingHash, true);
            //play audio - "this is too upsetting"
        }
        if (isPouting && animationValue != 5)
        {
            animator.SetBool(isPoutingHash, false);
        }

        // 6 Defeated
        if (!isDefeated && animationValue == 6)
        {
            animator.SetBool(isDefeatedHash, true);
            //play audio - "come ooon!"
        }
        if (isDefeated && animationValue != 6)
        {
            animator.SetBool(isDefeatedHash, false);
        }

        // 7 Praying
        if (!isDisappointed && animationValue == 7)
        {
            animator.SetBool(isDisappointedHash, true);
            //play audio - "i'm begging you. please help me out"
        }
        if (isDisappointed && animationValue != 7)
        {
            animator.SetBool(isDisappointedHash, false);
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

                print("Waiting for a connection... host :" + ipAddress.MapToIPv4().ToString() + " port : " + PORT);
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
            state.animationCode.Append(Encoding.ASCII.GetString(state.buffer, 0, read));
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }
        else
        {
            if (state.animationCode.Length >= 1)
            { 
                string content = state.animationCode.ToString();
                print($"Read {content.Length} bytes from socket.\n Data : {content}");
                SetAnimation(content);
            }
            handler.Close();
        }
    }

    //Set animation to object
    private void SetAnimation (string data) 
    {
        animationValue = int.Parse(data);
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
        public StringBuilder animationCode = new StringBuilder();
    }
}