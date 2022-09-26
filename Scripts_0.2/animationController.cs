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

    string animationValue = "";

    public static readonly int PORT = 1755;
    public static readonly int WAITTIME = 1;
    
    int isJumpingHash;
    int isThankfulHash;
    int isNoddingHash;
    int isAnnoyedHash;
    int isPoutingHash;
    int isDefeatedHash;
    int isDisappointedHash;

    public AudioClip AcSound1;
    public AudioClip AcSound2;
    public AudioClip AcSound3;
    public AudioClip AcSound4;
    public AudioClip AcSound5;
    public AudioClip AcSound6;
    public AudioClip AcSound7;
    
    public AudioClip AcJumping;
    public AudioClip AcThankful;
    public AudioClip AcNodding;
    public AudioClip AcAnnoyed;
    public AudioClip AcPouting;
    public AudioClip AcDefeated;
    public AudioClip AcDisappointed;

    AudioSource sound1;
    AudioSource sound2;
    AudioSource sound3;
    AudioSource sound4;
    AudioSource sound5;
    AudioSource sound6;
    AudioSource sound7;

    AudioSource AudJumping;
    AudioSource AudThankful;
    AudioSource AudNodding;
    AudioSource AudAnnoyed;
    AudioSource AudPouting;
    AudioSource AudDefeated;
    AudioSource AudDisappointed;

    bool play1;
    bool play2;    
    bool play3;
    bool play4;
    bool play5;
    bool play6;
    bool play7;

    bool AudJump;
    bool AudThank;
    bool AudNod;
    bool AudAnnoy;
    bool AudPout;
    bool AudDefeat;
    bool AudDisappoint;


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
        isThankfulHash = Animator.StringToHash("isThankful");
        isNoddingHash = Animator.StringToHash("isNodding");
        isAnnoyedHash = Animator.StringToHash("isAnnoyed");
        isPoutingHash = Animator.StringToHash("isPouting");
        isDefeatedHash = Animator.StringToHash("isDefeated");
        isDisappointedHash = Animator.StringToHash("isDisappointed");

        sound1 = gameObject.AddComponent<AudioSource>();
        sound2 = gameObject.AddComponent<AudioSource>();
        sound3 = gameObject.AddComponent<AudioSource>();
        sound4 = gameObject.AddComponent<AudioSource>();
        sound5 = gameObject.AddComponent<AudioSource>();
        sound6 = gameObject.AddComponent<AudioSource>();
        sound7 = gameObject.AddComponent<AudioSource>();

        AudJumping = gameObject.AddComponent<AudioSource>();
        AudThankful = gameObject.AddComponent<AudioSource>();
        AudNodding = gameObject.AddComponent<AudioSource>();
        AudAnnoyed = gameObject.AddComponent<AudioSource>();
        AudPouting = gameObject.AddComponent<AudioSource>();
        AudDefeated = gameObject.AddComponent<AudioSource>();
        AudDisappointed = gameObject.AddComponent<AudioSource>();

        play1 = false;
        play2 = false;
        play3 = false;
        play4 = false;
        play5 = false;
        play6 = false;
        play7 = false;

        AudJump = false;
        AudThank = false;
        AudNod = false;
        AudAnnoy = false;
        AudPout = false;
        AudDefeat = false;
        AudDisappoint = false;

        await Task.Run(() => ListenEvents(source.Token));   

    }

    // Update is called once per frame
    void Update()
    {
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isThankful = animator.GetBool(isThankfulHash);
        bool isNodding = animator.GetBool(isNoddingHash);
        bool isAnnoyed = animator.GetBool(isAnnoyedHash);
        bool isPouting = animator.GetBool(isPoutingHash);
        bool isDefeated = animator.GetBool(isDefeatedHash);
        bool isDisappointed = animator.GetBool(isDisappointedHash);
        // 1 Jumping
        if (!isJumping && animationValue.Equals("case1") && AudJump == false && play1 == false)
        {
            animator.SetBool(isJumpingHash, true);
            sound1.clip = AcSound1;
            AudJumping.clip = AcJumping;
            sound1.Play();
            AudJumping.Play();            
            play1 = true;
            AudJump = true;

            //play audio - "fantastic - keep up the amazing work"
        }
        if (isJumping && !animationValue.Equals("case1") && AudJump == true && play1 == true)
        {
            animator.SetBool(isJumpingHash, false);
            sound1.clip = AcSound1;
            AudJumping.clip = AcJumping;
            sound1.Stop();
            AudJumping.Stop();
            play1 = false;
            AudJump = false;

        } 

        // 2 Thankful
        if (!isThankful && animationValue.Equals("case2") && AudThank == false && play2 == false)
        {
            animator.SetBool(isThankfulHash, true);
            sound2.clip = AcSound2;
            AudThankful.clip = AcThankful;
            sound2.Play();
            AudThankful.Play();
            play2 = true;
            AudThank = true;
        }
        if (isThankful && !animationValue.Equals("case2") && AudThank == true && play2 == true)
        {
            animator.SetBool(isThankfulHash, false);
            sound2.clip = AcSound2;
            AudThankful.clip = AcThankful;           
            sound2.Stop();
            AudThankful.Stop();
            play2 = false;
            AudThank = false;
        }

        // 3 Nodding
        if (!isNodding && animationValue.Equals("case3") && AudNod == false && play3 == false)
        {
            animator.SetBool(isNoddingHash, true);
            sound3.clip = AcSound3;
            AudNodding.clip = AcNodding;
            sound3.Play();
            AudNodding.Play();
            play3 = true;
            AudNod = true;
        }
        if (isNodding && !animationValue.Equals("case3") && AudNod == true && play3 == true)
        {
            animator.SetBool(isNoddingHash, false); 
            sound3.clip = AcSound3;
            AudNodding.clip = AcNodding;
            sound3.Stop();
            AudNodding.Stop();
            play3 = false;
            AudNod = false;
        }

        // 4 Annoyed
        if (!isAnnoyed && animationValue.Equals("case4") && AudAnnoy == false && play4 == false)
        {
            animator.SetBool(isAnnoyedHash, true);
            sound4.clip = AcSound4;
            AudAnnoyed.clip = AcAnnoyed;
            sound4.Play();
            AudAnnoyed.Play();
            play4 = true;
            AudAnnoy = true;
        }
        if (isAnnoyed && !animationValue.Equals("case4") && AudAnnoy == true && play4 == true)
        {
            animator.SetBool(isAnnoyedHash, false);
            sound4.clip = AcSound4;
            AudAnnoyed.clip = AcAnnoyed;
            sound4.Stop();
            AudAnnoyed.Stop();
            play4 = false;
            AudAnnoy = false;
        }

        // 5 Pouting
        if (!isPouting && animationValue.Equals("case5") && AudPout == false && play5 == false)
        {
            animator.SetBool(isPoutingHash, true);
            sound5.clip = AcSound5;
            AudPouting.clip = AcPouting;
            sound5.Play();
            AudPouting.Play();
            play5 = true;
            AudPout = true;
        }
        if (isPouting && !animationValue.Equals("case5") && AudPout == true && play5 == true)
        {
            animator.SetBool(isPoutingHash, false);
            sound5.clip = AcSound5;
            AudPouting.clip = AcPouting;
            sound5.Stop();
            AudPouting.Stop();
            play5 = false;
            AudPout = false;
        }

        // 6 Defeated
        if (!isDefeated && animationValue.Equals("case6") && AudDefeat == false && play6 == false)
        {
            animator.SetBool(isDefeatedHash, true);
            sound6.clip = AcSound6;
            AudDefeated.clip = AcDefeated;
            sound6.Play();
            AudDefeated.Play();
            play6 = true;
            AudDefeat = true;
        }
        if (isDefeated && !animationValue.Equals("case6") && AudDefeat == true && play6 == true)
        {
            animator.SetBool(isDefeatedHash, false);
            sound6.clip = AcSound6;
            AudDefeated.clip = AcDefeated;
            sound6.Stop();
            AudDefeated.Stop();
            play6 = false;
            AudDefeat = false;
        }

        // 7 Disappointed
        if (!isDisappointed && animationValue.Equals("case7") && AudDisappoint == false && play7 == false)
        {
            animator.SetBool(isDisappointedHash, true);
            sound7.clip = AcSound7;
            AudDisappointed.clip = AcDisappointed;
            sound7.Play();
            AudDisappointed.Play();
            play7 = true;
            AudDisappoint = true;
        }
        if (isDisappointed && !animationValue.Equals("case7") && AudDisappoint == true && play7 == true)
        {
            animator.SetBool(isDisappointedHash, false);
            sound7.clip = AcSound7;
            AudDisappointed.clip = AcDisappointed;
            sound7.Stop();
            AudDisappointed.Stop();
            play7 = false;
            AudDisappoint = false;
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
                animationValue = SetAnimation(content);
            }
            handler.Close();
        }
    }

    //Set animation to object
    private string SetAnimation (string data) 
    {
        Debug.Log("trying to set animation");
        animationValue = data;
        return data;
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