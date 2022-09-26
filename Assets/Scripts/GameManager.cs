using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using NaughtyAttributes;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isManualControl = false;
    private int currentYear;
    private float currentTime = 0.0f;
    [SerializeField] private int startYear, endYear;
    [SerializeField] private float totalDuration; //time in seconds
    private List<HumanActivity> activatingActivities = new List<HumanActivity>(), //only one per role will be at this list
                                DeactivatingActivities = new List<HumanActivity>(); 

    //cards
    [SerializeField] private ActivityData activities;
    [SerializeField] private TextAsset cardFile;
    [SerializeField] private List<HumanActivity> activeCards;
    private string recievedCardID = "", pastRecievedCardID = "";
    private bool isFirstCard = false;

    //roles
    private List<ActivityRole> roles = new List<ActivityRole>() {
        new ActivityRole("Working Class"), new ActivityRole("Middle Class"), new ActivityRole("Upper Class"),
        new ActivityRole("Company"), new ActivityRole("Policymaker")
    };

    //bool to check if it's the first round
    private bool isFirstRound = true;

    private UIControl uiControl;
    private SoundManager soundManager;

    #region FSM
    // private BoardState state;
    private GameStateBase currentState;
    public GameStateIdle stateIdle = new GameStateIdle();
    public GameStateIntro stateIntro = new GameStateIntro();
    public GameStatePlay statePlay = new GameStatePlay();
    public GameStateReview stateReview = new GameStateReview();
    public GameStateEnd stateEnd = new GameStateEnd();


    public void ChangeState(GameStateBase newState)
    {
        if (!newState.Equals(currentState)) {
            if (currentState != null)
            {
                currentState.LeaveState(this);

                //test
                Debug.Log("Changing from " + currentState + " to " + newState);
            }

            currentState = newState;

            if (currentState != null)
            {
                currentState.EnterState(this);
            }
        }
    }
    #endregion

    //getters & setters
    public int CurrentYear{get=>currentYear;set=>currentYear=value;}
    public List<HumanActivity> ActiveCards{get=>activeCards;private set=>activeCards=value;}
    public List<ActivityRole> Roles{get=>roles;private set=>roles=value;}
    public bool IsFirstRound{get=>isFirstRound; set=>isFirstRound = value;}
    public UIControl UiControl{get=>uiControl;}
    public string RecievedCardID{get=>recievedCardID;set=>recievedCardID=value;}
    public bool IsFirstCard{get=>isFirstCard;set=>isFirstCard=value;}
    public float CurrentTime{get=>currentTime;set=>currentTime=value;}

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        CurrentYear = startYear;
        List<List<string>> cards = ParseCSVIntoCards(cardFile.text);
        assignCardsToActivities(cards);
        uiControl = FindObjectOfType<UIControl>();
        soundManager = FindObjectOfType<SoundManager>();

        //StartCoroutine(timeCountDown(deltaYearTime));

        //assign textmeshproUGUI to roles
        foreach(ActivityRole role in Roles) {
            switch(role.ActivityClass.Trim().ToLower()) {
                case "working class":
                    role.NarrativeText = uiControl.WorkingText;
                    role.EndNarrativeText = uiControl.WorkingEndText;
                    break;
                case "middle class":
                    role.NarrativeText = uiControl.MiddleText;
                    role.EndNarrativeText = uiControl.MiddleEndText;
                    break;
                case "upper class":
                    role.NarrativeText = uiControl.UpperText;
                    role.EndNarrativeText = uiControl.UpperEndText;
                    break;
                case "company":
                    role.NarrativeText = uiControl.CompanyText;
                    role.EndNarrativeText = uiControl.CompanyEndText;
                    break;
                case "policymaker":
                    role.NarrativeText = uiControl.PolicymakerText;
                    role.EndNarrativeText = uiControl.PolicyEndText;
                    break;
                default:
                    Debug.Log("Can't find " + role.ActivityClass.Trim().ToLower() + " when assigning narrative texts");
                    break;
            }
            if (role != null)
                role.NarrativeText.text = role.ActivityClass;
        }

        //test
        // addToActiveCards("ban ads");
        // addToActiveCards("electric car");
        // addToActiveCards("riot");
        // addToActiveCards("building");
        // addToActiveCards("nuclear");
        
        foreach (ActivityRole role in Roles)
            role.IsInGame = true;
        
        currentState = stateIdle;

        //roundEnd();
        // for (int i = 0; i < activeCards.Count; i++) {
        //     Debug.Log(i + " is " + checkCondition(activeCards[i].FirstConditions[1], activeCards[i].ActivityClass));
        // }
    }

    
    void Update()
    {
        // if (currentTime >= deltaYearTime) {
        //     nextYear();
        // }
        // else
        //     currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
            activities.Activities[0].StartYears.Add(1);
        
        if (Input.GetKeyDown(KeyCode.P))
            roundEnd();
        
        currentState.Update(this);
    }

    #region CSV to Scriptable Object
    private List<List<string>> ParseCSVIntoCards(string fileString)
    {
        //split the lines on ^ to separate textfile into different classes
        List<string> classes = new List<string>();
        classes.AddRange(fileString.Split('^'));
        removeEmptyCell(classes);
        int numClasses = classes.Count;

        //split the lines on @ to separate classes into different cards
        List<List<string>> cards = new List<List<string>>();

        for (int i = 0; i < classes.Count; i++) {
            List<string> cardNames = new List<string>();
            cardNames.AddRange(classes[i].Split('@'));
            removeEmptyCell(cardNames);
            cards.Add(cardNames);
        }
        int numCards = cards.Count;

        return cards;
    }

    private void assignCardsToActivities(List<List<string>> cards) {
        activities.Activities.Clear();

        for (int i = 0; i < cards.Count; i++) {
            for (int j = 0; j < cards[i].Count; j++) {
                HumanActivity humanActivity = new HumanActivity();
                //assign class
                humanActivity.ActivityClass = removeComma(cards[i][0]);

                //parse by &
                List<string> targets = new List<string>();
                if (j >= 1) {
                    targets.AddRange(cards[i][j].Split('&'));
                    removeEmptyCell(targets);

                    //clean extra ',' and separate name section by ','
                    targets[0] = removeComma(targets[0]);
                    string[] nameSection = targets[0].Split(',');

                    //assign activity name
                    humanActivity.ActivityName = nameSection[0];

                    //assign activity group
                    humanActivity.ActivityGroup = nameSection[1];

                    //assign display name
                    humanActivity.DisplayName = nameSection[2];

                    //assign carbon emission
                    humanActivity.CarbonEmission = float.Parse(nameSection[3]);

                    //deal with first narratives
                    string[] firstNarrativeGroup = targets[1].Split(System.Environment.NewLine.ToCharArray());
                    foreach (string firstNarrativeLine in firstNarrativeGroup) {
                        string[] firstNarrativeSection = firstNarrativeLine.Split('*');
                        
                        if (firstNarrativeSection.Length > 1) {
                            //assign first narrative
                            humanActivity.FirstNarratives.Add(firstNarrativeSection[0]);

                            //split by ',' to get conditions/priorities/targeting type out
                            List<string> firstNarrativeConditions = new List<string>(); 
                            firstNarrativeConditions.AddRange(firstNarrativeSection[1].Split(','));
                            removeEmptyCell(firstNarrativeConditions);
                            //assign priorities
                            humanActivity.FirstActivityPriorities.Add(int.Parse(firstNarrativeConditions[0]));

                            //assign conditions
                            humanActivity.FirstConditions.Add(firstNarrativeConditions[1]);

                            //assign scores
                            humanActivity.FirstScore.Add(int.Parse(firstNarrativeConditions[3]));
                        }
                    }

                    //deal with second narratives (if have one)
                    if (targets.Count-1 >= 2) {
                        string[] secondNarrativeGroup = targets[2].Split(System.Environment.NewLine.ToCharArray());
                        foreach (string secondNarrativeLine in secondNarrativeGroup) {
                            string[] secondNarrativeSection = secondNarrativeLine.Split('*');

                            if (secondNarrativeSection.Length > 1) {
                                //assign second narrative
                                humanActivity.SecondNarratives.Add(secondNarrativeSection[0]);

                                //split by ',' to get conditions/priorities/targeting type out
                                List<string> secondNarrativeConditions = new List<string>(); 
                                secondNarrativeConditions.AddRange(secondNarrativeSection[1].Split(','));
                                removeEmptyCell(secondNarrativeConditions);

                                //assign priorities
                                humanActivity.SecondActivityPriorities.Add(int.Parse(secondNarrativeConditions[0]));

                                //assign conditions
                                humanActivity.SecondConditions.Add(secondNarrativeConditions[1]);

                                //assign affecting type
                                humanActivity.SecondAffectingTypes.Add(secondNarrativeConditions[2]);

                                //assign scores
                                humanActivity.SecondScore.Add(int.Parse(secondNarrativeConditions[3]));
                            }
                        }
                    }
                }

                //clean and add to the scritable object
                removeEmptyCell(humanActivity.FirstNarratives);
                removeEmptyCell(humanActivity.SecondNarratives);
                activities.Activities.Add(humanActivity);
            }
        }

        //remove empty elements in the list
        removeEmptyCell(activities.Activities);
    }


    /* remove empty cell in the string list
       @param list that needs to remove empty cell*/
    private void removeEmptyCell(List<string> list) {
        List<int> removeIndexes = new List<int>();

        //find all empty cell indexes
        for (int i = 0; i < list.Count; i++)
            if (list[i].ToLower().Trim().Equals("") || list[i].ToLower().Trim().Equals("\""))
                removeIndexes.Add(i);
        
        //remove those cells from the string list
        for (int i = removeIndexes.Count-1; i >= 0; i--)
            list.RemoveAt(removeIndexes[i]);
    }

    /* remove empty cell in the HumanActivity list
       @param list that needs to remove empty cell*/
    private void removeEmptyCell(List<HumanActivity> list) {
        List<int> removeIndexes = new List<int>();

        //find all empty cell indexes
        for (int i = 0; i < list.Count; i++) {
            string temp = list[i].ActivityName;
            if (temp == null) {
                removeIndexes.Add(i);
            }
        }
        
        //remove those cells from the string list
        for (int i = removeIndexes.Count-1; i >= 0; i--)
            list.RemoveAt(removeIndexes[i]);
    }

    /* remove extra ',' from the end of the string
       @param str that need to remove
       @return string the string after remove ','*/
    public string removeComma(string str) {
        char[] characters = str.Trim().ToCharArray();
        for (int i = characters.Length-1; i >= 0; i--) {
            if (!characters[i].Equals(','))
                return str.Substring(0, i+1);
        }
        return str;
    }
    #endregion

    /* add cards to active cards (which will be played in the next round)
       if the same one is already in the active cards, then return false
       if the same class one is already in the active cards, then replace that one with this one
       @param activity the activitythat needs to be added
       @return bool return if sucessfully added*/
    public bool addToActiveCards(HumanActivity activity) {
        for (int i = 0; i < ActiveCards.Count; i++) {
            if (activity.Equals(ActiveCards[i]))
                return false;
            if (activity.ActivityClass.ToLower().Trim().Equals(ActiveCards[i].ActivityClass.ToLower().Trim())) {
                ActiveCards[i] = activity;
                return true;
            }
        }
        ActiveCards.Add(activity);

        //mark the role is in game
        if (IsFirstRound) {
            foreach (ActivityRole role in Roles) {
                if (activity.ActivityClass.Trim().ToLower().Equals(role.ActivityClass.Trim().ToLower())) {
                    role.IsInGame = true;
                }
            }
        }

        return true;
    }

    /* add cards to active cards (which will be played in the next round)
       call addToActiveCards(HumanActivity)
       @param activityName the name of the activity that needs to be added
       @return bool return if sucessfully added*/
    public bool addToActiveCards(string activityName) {
        foreach (HumanActivity activity in activities.Activities)
            if (activity.ActivityName.ToLower().Trim().Equals(activityName.ToLower().Trim())) {
                soundManager.playButtonSound();
                return addToActiveCards(activity);
            }
        return false;
    }

    public bool addToACtiveCardsFromPython() {
        //Debug.Log("recieved is : " + RecievedCardID + " \npast recieved is " + pastRecievedCardID);
        if (!RecievedCardID.Equals(pastRecievedCardID)) {
            switch (RecievedCardID) {
                case "1":
                    addToActiveCards("Riot");
                    break;
                case "2":
                    addToActiveCards("Working Class Public transportation");
                    break;
                case "3":
                    addToActiveCards("working class local seasonal food");
                    break;
                case "4":
                    addToActiveCards("Working Class Building");
                    break;
                case "5":
                    addToActiveCards("Protest");
                    break;
                case "6":
                    addToActiveCards("working class meat");
                    break;
                case "7":
                    addToActiveCards("oneself");
                    break;
                case "8":
                    addToActiveCards("ac");
                    break;
                case "9":
                    addToActiveCards("middle class public transportation");
                    break;
                case "a":
                    addToActiveCards("middle class meat");
                    break;
                case "b":
                    addToActiveCards("building");
                    break;
                case "c":
                    addToActiveCards("climate injustice");
                    break;
                case "d":
                    addToActiveCards("middle class local seasonal food");
                    break;
                case "e":
                    addToActiveCards("not enough power");
                    break;
                case "f":
                    addToActiveCards("fly");
                    break;
                case "g":
                    addToActiveCards("divest");
                    break;
                case "h":
                    addToActiveCards("electric car");
                    break;
                case "i":
                    addToActiveCards("upper class public transportation");
                    break;
                case "j":
                    addToActiveCards("upper class local seasonal food");
                    break;
                case "k":
                    addToActiveCards("others do it");
                    break;
                case "l":
                    addToActiveCards("advertise");
                    break;
                case "m":
                    addToActiveCards("change plastic package");
                    break;
                case "n":
                    addToActiveCards("apss");
                    break;
                case "o":
                    addToActiveCards("nuclear");
                    break;
                case "p":
                    addToActiveCards("renewable energy");
                    break;
                case "q":
                    addToActiveCards("product lives");
                    break;
                case "r":
                    addToActiveCards("reduce energy use");
                    break;
                case "s":
                    addToActiveCards("not enough benefit");
                    break;
                case "t":
                    addToActiveCards("ban ads");
                    break;
                case "u":
                    addToActiveCards("control fossil fuel");
                    break;
                case "v":
                    addToActiveCards("single-use plastic");
                    break;
                case "w":
                    addToActiveCards("education");
                    break;
                case "x":
                    addToActiveCards("reduce new road");
                    break;
                case "y":
                    addToActiveCards("don't change a lot");
                    break;
                case "z":
                    addToActiveCards("defend");
                    break;
            }
            pastRecievedCardID = RecievedCardID;
            return true;
        }
        return false;
    }

    /* use for deciding should targetingClass considers adding the narrative with this condition to their pending list
       @param condition the condition of the HumanActivity
       @param targetingClass the class this condition is refering to
       @return bool the boolean that if condition has met*/
    public bool checkCondition(string condition, string targetingClass) {
        //get what card this condition is targeting
        HumanActivity targetingActivity = null;
        foreach (HumanActivity activity in ActiveCards) {
            if (activity.ActivityClass.Equals(targetingClass)) {
                targetingActivity = activity;
                break; 
            }
        }

        //if targeting activity class is not in playing, break out
        if (targetingActivity==null)
            return false;

        foreach (ActivityRole role in Roles) {
            if (role.ActivityClass.Trim().ToLower().Equals(targetingClass.Trim().ToLower())) {
                if (role.Activity == null)
                    return false;
            }
        }

        //check conditions
        if (condition.ToLower().Trim().Equals("any"))
            return true;
        else {
            //separate condition string by '%'
            string[] conditionSections = condition.ToLower().Trim().Split('%');

            //check operator
            switch (conditionSections[1]) {
                case "==":
                    //switch according to first element
                    switch (conditionSections[0]) {
                        case "duration":
                            int duration = int.Parse(conditionSections[2]);
                            if (targetingActivity.Duration == duration)
                                return true;
                            break;
                        case "other":
                            return true;
                        case "group free":
                            if (targetingActivity.ActivityGroup.ToLower().Trim().Equals("free"))
                                return true;
                            break;
                        default:
                            //if card with name of conditionSection[0] and conditionSection[2] are all in the active cards then true
                            bool hasCard1 = false, hasCard2 = false;
                            foreach (HumanActivity activeActivity in ActiveCards) {
                                if (activeActivity.ActivityName.ToLower().Trim().Equals(conditionSections[0].ToLower().Trim()))
                                    hasCard1 = true;
                                if (activeActivity.ActivityName.ToLower().Trim().Equals(conditionSections[2].ToLower().Trim())) 
                                    hasCard2 = true;
                            }
                            if (hasCard1 && hasCard2)
                                return true;
                            break;
                    }
                    break;

                case ">=":
                    //switch according to first element
                    switch (conditionSections[0]) {
                        case "duration":
                            int duration = int.Parse(conditionSections[2]);
                            if (targetingActivity.Duration >= duration)
                                return true;
                            break;
                        case "other":
                            return true;
                        case "group free":
                            if (targetingActivity.ActivityGroup.ToLower().Trim().Equals("free"))
                                return true;
                            break;
                        default:
                            //if the start time of card with name of conditionSection[0] is >= conditionSection[2] then true
                            HumanActivity activity1 = null, activity2 = null;
                            foreach (HumanActivity activeActivity in ActiveCards) {
                                if (activeActivity.ActivityName.ToLower().Trim().Equals(conditionSections[0].ToLower().Trim()))
                                    activity1 = activeActivity;
                                if (activeActivity.ActivityName.ToLower().Trim().Equals(conditionSections[2].ToLower().Trim())) 
                                    activity2 = activeActivity;
                            }
                            if (activity1 != null && activity1.StartYears[0] >= CurrentYear)
                                return true;
                            break;
                    }
                    break;

                case "<=":
                    //switch according to first element
                    switch (conditionSections[0]) {
                        case "duration":
                            int duration = int.Parse(conditionSections[2]);
                            if (targetingActivity.Duration <= duration)
                                return true;
                            break;
                        case "other":
                            return true;
                        case "group free":
                            if (targetingActivity.ActivityGroup.ToLower().Trim().Equals("free"))
                                return true;
                            break;
                        default:
                            //if the start time of card with name of conditionSection[0] is <= conditionSection[2] then true
                            HumanActivity activity1 = null, activity2 = null;
                            foreach (HumanActivity activeActivity in ActiveCards) {
                                if (activeActivity.ActivityName.ToLower().Trim().Equals(conditionSections[0].ToLower().Trim()))
                                    activity1 = activeActivity;
                                if (activeActivity.ActivityName.ToLower().Trim().Equals(conditionSections[2].ToLower().Trim())) 
                                    activity2 = activeActivity;
                            }
                            if (activity1 != null && activity1.StartYears[0] <= CurrentYear)
                                return true;
                            break;
                    }
                break;
            }
            
        }
        return false;
    }


    public void roundEnd() {

        //assign cards into roles
        foreach (ActivityRole role in Roles) {
            foreach (HumanActivity activity in ActiveCards) { 
                if (role.ActivityClass == null) Debug.Log("role class WTF");
                if (activity.ActivityClass == null) Debug.Log("activity class WTF");
                if (role.ActivityClass.ToLower().Trim().Equals(activity.ActivityClass.ToLower().Trim()))
                    role.Activity = activity;
            }
            //play card
            if (role.IsInGame && role.Activity != null)
                role.Activity.activateForAYear(CurrentYear);
        }

        foreach (ActivityRole role in Roles) {
            if (role.IsInGame) {
                if (role.Activity != null) {

                    //adding potential first narrative into pending list
                    for (int i = 0; i < role.Activity.FirstConditions.Count; i++) {
                        if (checkCondition(role.Activity.FirstConditions[i], role.ActivityClass)) {
                            role.FirstPendingList.Add(role.Activity.FirstNarratives[i]);
                            role.FirstPriorityList.Add(role.Activity.FirstActivityPriorities[i]);
                            role.FirstScoreList.Add(role.Activity.FirstScore[i]);
                        }
                    }

                    //decides first narrative and how much score changed from pending list based on priority
                    int currentPriority = -1;
                    int changingScore = 0;
                    for (int i = 0; i < role.FirstPendingList.Count; i++) {
                        if (role.FirstPriorityList[i] > currentPriority) {
                            role.FirstNarrative = role.FirstPendingList[i];
                            changingScore = role.FirstScoreList[i];
                            currentPriority = role.FirstPriorityList[i];
                        }
                    }
                    //change score
                    role.Score += changingScore;

                    ActivityRole affectingRole = null;
                    //adding potential second narrative into pending list
                    for (int i = 0; i < role.Activity.SecondConditions.Count; i++) {
                        if (checkCondition(role.Activity.SecondConditions[i], role.Activity.SecondAffectingTypes[i])) {
                            //get the affecting type card
                            foreach (ActivityRole targetingRole in Roles) 
                                if (role.Activity.SecondAffectingTypes[i].ToLower().Trim().Equals(targetingRole.ActivityClass.ToLower().Trim()))
                                    affectingRole = targetingRole;

                            //add to affecting card second pending list if it's in game
                            if (affectingRole != null && affectingRole.IsInGame) {
                                affectingRole.SecondPendingList.Add(role.Activity.SecondNarratives[i]);
                                affectingRole.SecondPriorityList.Add(role.Activity.SecondActivityPriorities[i]);
                                affectingRole.SecondScoreList.Add(role.Activity.SecondScore[i]);
                                //Debug.Log(affectingRole.SecondPendingList[affectingRole.SecondPendingList.Count-1]);

                                //decides second narrative and how much score changed from pending list based on priority
                                changingScore = 0;
                                currentPriority = -1;
                                for (int j = 0; j < affectingRole.SecondPendingList.Count; j++) {
                                    if (affectingRole.SecondPriorityList[j] > currentPriority) {
                                        affectingRole.SecondNarrative = affectingRole.SecondPendingList[j];
                                        changingScore = affectingRole.SecondScoreList[j];
                                        //Debug.Log(affectingRole.SecondNarrative);
                                        currentPriority = affectingRole.SecondPriorityList[j];
                                    }
                                }
                                //change score
                                affectingRole.Score += changingScore;
                            }
                        }
                    }

                    //add carbon emission value to the role
                    role.CarbonEmission += role.Activity.CarbonEmission;

                    //record the activity count
                    if (role.ActivityCounts.ContainsKey(role.Activity.ActivityName.Trim().ToLower()))
                        role.ActivityCounts[role.Activity.ActivityName.Trim().ToLower()] += 1;
                    else
                        role.ActivityCounts.Add(role.Activity.ActivityName.Trim().ToLower(), 1);

                    //display narratives on UI
                    role.NarrativeText.text = role.FirstNarrative + " " + role.SecondNarrative + " - " + role.ScoreName + ": " + role.Score;
                } else { //if there's no card input for this role this round
                    role.NarrativeText.text = role.ActivityClass + " don't have any action this round";
                }
            } else { //if the role is not in this game
                role.NarrativeText.text = role.ActivityClass + " doesn't involve in this current game. You can use this role in the next game.";
            }
        }
        uiControl.rebuildUI();
    }

    /* read card name from Python, and add the card to current playing cards
       @return true if added successfully, false if the card's role is not in play */
    public bool readCardInfo(string cardID) {
        foreach (HumanActivity activity in ActiveCards) {
            if (activity.ActivityName.ToLower().Trim().Equals(cardID.ToLower().Trim())) {
                
            }
        }
        return false;
    }

    /* show each role's name and their score */
    public void showRoleNames() {
        foreach (ActivityRole role in Roles) {
            role.NarrativeText.text = role.ActivityClass + " - " + role.ScoreName + ": " + role.Score;
        }
        UiControl.rebuildUI();
    }

    /* change role's narrative text into the activity display name*/
    public void showTappedCards() {
        foreach (ActivityRole role in Roles) {
            if (role.IsInGame) {
                foreach (HumanActivity activity in ActiveCards) {
                    if (activity.ActivityClass.Trim().ToLower().Equals(role.ActivityClass.Trim().ToLower())) {
                        role.NarrativeText.text = activity.DisplayName;
                    }
                }
            }
        }
        UiControl.rebuildUI();
    }

    public IEnumerator waitToChangeState(float waitTime, GameStateBase changingState) {
        yield return new WaitForSeconds(waitTime);
        ChangeState(changingState);
    }

    public bool timerCountDown(float endTime) {
        if (currentTime >= endTime) {
            currentTime = 0.0f;
            return true;
        }
        else
            currentTime += Time.deltaTime;
        
        return false;
    }

    public void rebuildUI() {
        UiControl.rebuildUI();
    }

    public void reset() {
        IsFirstRound = true;
        CurrentYear = startYear;
        ActiveCards.Clear();
        UiControl.hideGameUI();
        foreach(ActivityRole role in Roles) {
            role.reset();
        }
    }
}