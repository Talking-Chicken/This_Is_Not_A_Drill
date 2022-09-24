using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class animationController2 : MonoBehaviour
{
    private CancellationTokenSource source;
    public ManualResetEvent allDone;
    public Animator animator;

    int animationValue = 0;
    int isJumpingHash;
    int isHardNoddingHash;
    int isNoddingHash;
    int isAnnoyedHash;
    int isPoutingHash;
    int isDefeatedHash;
    int isPrayingHash;

    animationController2()
    {
        source = new CancellationTokenSource();
        allDone = new ManualResetEvent(false);
    }

    // Start is called before the first frame update
    void Start()
    {        
        animator = GetComponent<Animator>();
        
        isJumpingHash = Animator.StringToHash("isJumping");
        isHardNoddingHash = Animator.StringToHash("isHardNodding");
        isNoddingHash = Animator.StringToHash("isNodding");
        isAnnoyedHash = Animator.StringToHash("isAnnoyed");
        isPoutingHash = Animator.StringToHash("isPouting");
        isDefeatedHash = Animator.StringToHash("isDefeated");
        isPrayingHash = Animator.StringToHash("isPraying");

        //await Task.Run(() => ListenEvents(source.Token));   
    }

    // Update is called once per frame
    void Update()
    {
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isHardNodding = animator.GetBool(isHardNoddingHash);
        bool isNodding = animator.GetBool(isNoddingHash);
        bool isAnnoyed = animator.GetBool(isAnnoyedHash);
        bool isPouting = animator.GetBool(isPoutingHash);
        bool isDefeated = animator.GetBool(isDefeatedHash);
        bool isPraying = animator.GetBool(isPrayingHash);

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
        if (!isHardNodding && animationValue == 2)
        {
            animator.SetBool(isHardNoddingHash, true);
            //play audio - "great job"
        }
        if (isHardNodding && animationValue != 2)
        {
            animator.SetBool(isHardNoddingHash, false);
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
        if (!isPraying && animationValue == 7)
        {
            animator.SetBool(isPrayingHash, true);
            //play audio - "i'm begging you. please help me out"
        }
        if (isPraying && animationValue != 7)
        {
            animator.SetBool(isPrayingHash, false);
        }


        //ask RJ if he can write a line that says - so that we accept the number that is higher than 1-7 if 8-12 scenarios occur, and if 8-12 scenarios occur more than once, then either choose a random one, or pick the lowest number?
        // card_w_animation = [-,-,-,-,-]
        // if (value_of_card_tapped == i){
        //     python send value_of_card_tapped
        // }else{
        //     send sea level rise data for of all actions - 1/2/3/4/5/6/7
        // }


        // 8 upper class does nothing
        // if (!isAnnoyed && animationValue == 8)
        // {
        //     animator.SetBool(isAnnoyedHash, true);
        //     //delay by 5 seconds?
        //     //play attached audio named UC_others
        //     //voice: "upper class citizens have to do better. think about your children! you have as much responsibility as everyone else does."
        // }
        // if(isAnnoyed && animationValue != 8)
        // {
        //     animator.SetBool(isAnnoyedHash, false);
        // }
        
        // // 9 companies focus on profit rather than climate-related issues
        
        // if (!isAnnoyed && animationValue == 9)
        // {
        //     animator.SetBool(isAnnoyedHash, true);
        //     //delay by 5 seconds?
        //     //play attached audio named BO_profit
        //     //voice: "companies these days! you ignore climate-related issues and blame your destruction of the world on us consumers"
        // }
        // if(isAnnoyed && animationValue != 9)
        // {
        //     animator.SetBool(isAnnoyedHash, false);
        // }


        // // 10 policy-makers accept bribes
        // if(animationValue == 10)
        // {
        //     //delay by 5 seconds?
        //     //play attached audio named PM_corrupt
        //     //voice: "how can policy-makers be so selfish? is money or fame really worth destruction?!"
        //     //animation: talking (hands out, question)
        // }
        
        // // 11 middle class climate injustice
        // if(animationValue == 11)
        // {
        //     //delay by 5 seconds?
        //     //play attached audio named MC_injustice
        //     //voice: "yes! Climate injustice needs to be addressed. climate change affects us all differently, and too many suffer for the actions of the few rich, ignorant people!"
        //     //animation: talking (hands out)
        // }
        
        // // 12 working class self-doubt
        // if(animationValue == 12)
        // {
        //     //delay by 5 seconds?
        //     //play attached audio named 
        //     //voice: "get back up working class citizens! just because it feels hopeless right now doesn't mean you should stop now!"
        //     //animation: hopeful
        // }
    }

    //Set animation to object
    public void SetAnimation (string animation) 
    {
        animationValue = int.Parse(animation);
    }

}