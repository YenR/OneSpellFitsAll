using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;




public class gameLoop : MonoBehaviour
{



    List<string> words = new List<string>
        {
            "sandwich",
            "blanket",
            "book",
            "hammer",
            "screwdriver",
            "map",
            "pencil",
            "flowers",
            "sword",
            "shirt",
            "candle",
            "glasses",
            "key",
            "scarecrow",
            "paper",
            "water bottle",
            "shoes",
            "hat",
            "guitar",
            "paint brush",
            //"camera",
            "candle",
            "broom",
            "lantern",
            "mirror",
            "bucket",
            "fishing net",
            "herbs",
            "rope",
            "flute",
            "cart",
            "chair",
            "shovel",
            "basket",
            "pitchfork",
            "needle",
            "ink",
            "bow",
            "cloak",
            "key",
            "lock",
            "mushroom",
            "cup",
            "hat",
            "shoes",
            "messenger pigeon",
            "dress",
            "ring",
            "hair brush",
            "pendant",
            "parchment",
            "crystal",
            "necklace",
            //"seashell",
            "bow and arrow",
            "spoon",
            "envelope",
            "barrel",

            // chat gpt generated below

        "hammer",
        "screwdriver",
        //"duct tape",
        "flashlight",
        "batteries",
        "knife",
        "scissors",
       // "zip ties",
        "rubber bands",
        "paper clips",
        "pen",
        "pencil",
        "notebook",
        "ruler",
        //"tape measure",
        "glue",
        //"super glue",
        "lighter",
        "matches",
        "candle",
        "bucket",
        "mop",
        "broom",
        "plunger",
        "toilet brush",
        "cleaning spray",
        "sponge",
       // "steel wool",
        "vacuum cleaner",
        //"laundry basket",
        "hanger",
       // "clothesline",
       // "clothespins",
        "washing machine",
        "dryer",
        "ironing board",
        "iron",
        "fan",
       // "space heater",
       // "air purifier",
        "toolbox",
        "nails",
        "screws",
      //  "drill",
     //   "drill bits",
        "wrench",
     //   "pliers",
     //   "tweezers",
        "measuring cup",
     //   "funnel",
        "bowl",
        "pot",
        "pan",
     //   "strainer",
        "cutting board",
        "can opener",
        "bottle opener",
        "corkscrew",
        "thermometer",
        "first aid kit",
        "bandages",
        "antiseptic",
    //    "alcohol wipes",
    //    "cotton swabs",
    //    "ice pack",
    //    "heating pad",
    //    "mirror",
        "towel",
        "blanket",
        "pillow",
        "sheet",
        "curtain",
        "lamp",
        "light bulb",
        "extension cord",
    //    "power strip",
        "alarm clock",
    //    "phone charger",
    //    "laptop",
    //    "tablet",
    //    "router",
    //    "speaker",
        "headphones",
        "remote control",
        "TV",
        "batteries",
        "trash bag",
    //    "recycling bin",
        "storage box",
    //    "ziplock bags",
    //    "plastic wrap",
        "aluminum foil",
        "safety pins",
        "sewing kit",
        "needle",
        "thread",
        "buttons",
        "glasses",
        "sunglasses",
        "key",
        "lock"

        };

    public static gameLoop instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        villagerManager.maxVillagers = villagerCounter;


    }

    public int step = 0;
    public string solution = "";

    void initProblem()
    {
        string problem = words[Random.Range(0, words.Count)];

        debugtxt.SetText("Debug\nlast solution: " + solution);// + "\ncurrent: " + problem);

        solution = problem;



        villagerManager.activeVillagers.Clear();

        //villagerManager.instance.killAllVillagers();

        // TODO remove all villagers

        //villagerManager.instance.problemQueue.Clear();

        int problemsNumber = villagerCounter;

        string problemPrompt = "Imagine you have a problem. The solution to the problem is " + problem + ". The answer directly solves the problem. For example - bread can solve I am hungry and I want to feed ducks. Give me " + problemsNumber + " such problems. Each problem is one short sentence with six words or less and no commas. Return only the problems, not the solutions. Do not mention the word " + problem + " in the response. Give the problems as a list, like this [problem 1, problem2, problem3]. Example: Solution: Phone , Problems: [I feel lonely, I need to talk to someone, There is an emergency]. Your solution is: " + problem; // return the problems in a JSON format {problems:[problem1, problem2, problem3]}. the response has to be in JSON format";

        Debug.Log("Asking LLM: " + problemPrompt);
        step = 0;
        askLLM(problemPrompt);

        // read json out


        /*foreach (string s in problems)
        {
            villagerManager.instance.createVillager(s);
        }*/

    }

    bool valid = false;

    public void callback1()
    {
        if (step == 2)
        {
            if (LLMAnswer.Contains("yes") || LLMAnswer.Contains("Yes"))
                valid = true;
        }

        LLMAnswer = LLMAnswer.Replace("Problems:", "");
        LLMAnswer = LLMAnswer.Replace("Problem:", "");
        LLMAnswer = LLMAnswer.Replace("problems:", "");
        LLMAnswer = LLMAnswer.Replace("problem:", "");
        LLMAnswer = LLMAnswer.Replace("Problems", "");
        LLMAnswer = LLMAnswer.Replace("Problem", "");
        LLMAnswer = LLMAnswer.Replace("problems", "");
        LLMAnswer = LLMAnswer.Replace("problem", "");

        LLMAnswer = LLMAnswer.Replace("[", "");
        LLMAnswer = LLMAnswer.Replace("]", "");
        LLMAnswer = LLMAnswer.Replace("\"", "");
        LLMAnswer = LLMAnswer.Replace("1", "");
        LLMAnswer = LLMAnswer.Replace("2", "");
        LLMAnswer = LLMAnswer.Replace("3", "");
        LLMAnswer = LLMAnswer.Replace("4", "");
        LLMAnswer = LLMAnswer.Replace("5", "");
        LLMAnswer = LLMAnswer.Replace("6", "");
        LLMAnswer = LLMAnswer.Replace("7", "");
        LLMAnswer = LLMAnswer.Replace("8", "");
        LLMAnswer = LLMAnswer.Replace("9", "");
        LLMAnswer = LLMAnswer.Replace("0", "");
        LLMAnswer = LLMAnswer.Replace(".", "");


        string[] substrings = LLMAnswer.Split(',');

        //List<string> problems = splitList(LLMAnswer);
        List<string> problems = new List<string>();


        foreach (string s in substrings)
        {
            string a = s.Trim();
            Debug.Log(a);
            if (a.Length > 2)
                problems.Add(a);
        }

        villagerManager.instance.createVillagers(problems);
    }

    public int score;
    public int remaining_resource;

    public int villagerCounter = 2;

    public GameObject witchSpeechBubble;
    public TMP_Text witchWords;
    public TMP_InputField playerInput;

    public void witchSays(string s)
    {
        witchWords.SetText(s);
        witchSpeechBubble.SetActive(true);
        StartCoroutine(stopSaying());
    }

    public float speechStayTime = 3f;
    IEnumerator stopSaying()
    {
        yield return new WaitForSeconds(speechStayTime);
        witchSpeechBubble.SetActive(false);
    }

    public string[] witchErrorMsgs;

    public void askLLM(string s)
    {
        LLMAnswer = null;

        LLMUnitySamples.MyServerClient.instance.interaction1.ask(s);

    }

    IEnumerator waitForLLM()
    {
        yield return new WaitUntil(() => (LLMAnswer != null));

        Debug.Log("got LLM answer: " + LLMAnswer);

        LLMAnswer = null;
    }

    public string LLMAnswer = null;

    string lastPlayerInput = "";

    public void handlePlayerInput(string s)
    {
        lastPlayerInput = s;
        // TODO validate
        // if not valid -> show error
        if (isNotValidInput(s))
        {
            witchSays(witchErrorMsgs[Random.Range(0, witchErrorMsgs.Length)]);

            playerInput.text = "";
            playerInput.interactable = true;
            playerInput.Select();

            return;
        }

        // generate spellwords
        witchSays(generateSpellWords(s));

        // start generating image
        ComfyPromptCtr.instance.startGeneration(s);

        // evaluate for each villager and handle villager responses + make them leave

        // wait for imageGen to finish

        StartCoroutine(waitForImageGen());
    }

    public float similarityThreshold = 0.75f;
    public int points = 0;

    public TMP_Text pointCounter;

    void updatePoints()
    {
        pointCounter.SetText("" + points);
    }

    IEnumerator waitForImageGen()
    {
        //if (ComfyPromptCtr.instance.skipImageGen)
        //    yield break;

        // Wait until the condition is true
        if (ComfyPromptCtr.instance.skipImageGen == false)
            yield return new WaitUntil(() => !ComfyPromptCtr.generating);

        // Code to execute after the condition is true
        Debug.Log("Condition is true");

        // calculate score, increase villager counter 

        string[] tmp = new string[] { lastPlayerInput };

        List<System.Tuple<string, float>> tup = similarityTest.instance.getSimilarityScores(solution, tmp);

        Debug.Log("tested for similarity: " + solution + " and " + lastPlayerInput + ", got score: " + tup[0].Item2);

        if (tup[0].Item2 >= similarityThreshold)
        {
            // all villagers accept!
            foreach (villagerScript v in villagerManager.activeVillagers)
            {
                v.happy();
                points++;
            }
            villagerManager.maxVillagers = ++villagerCounter;
        }
        else
        {
            foreach (villagerScript v in villagerManager.activeVillagers)
            {
                v.die();
            }
            // handle each villager separately
        }

        /*villagerManager.activeVillagers[Random.Range(0, villagerManager.activeVillagers.Count)].happy();
        villagerManager.maxVillagers = ++villagerCounter;*/
        updatePoints();
        if (villagerCounter >= 10)
            villagerCounter = 9;

        villagerManager.instance.problemQueue.Clear();
        yield return new WaitForSeconds(2f);
        initProblem();
        // have peasants come again

    }

    public static List<string> splitList(string problems)
    {
        string pattern = "\"(.*?)\"";

        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(problems);

        List<string> problemsList = new List<string>();
        foreach (Match match in matches)
        {
            problemsList.Add(match.Groups[1].Value);
        }

        return problemsList;
    }


    public AudioSource magic;

    public bool isNotValidInput(string s)
    {
        valid = false;
        step = 2;
        //askLLM("Is the following valid input? Valid input contains less than 5 words and must be an object. it is not allowed to be obscene or NSFW. Please answer only with a yes or no. input: " + s);

        // TODO
        if (s.Length <= 2)
            return true;
        return false;
    }

    public string[] problems;

    public string generateSpellWords(string conjuration)
    {
        // TODO
        // LLM command: Create me 2-5 word spell to conjure + conjuration + answer only with the spell words
        magic.PlayOneShot(magic.clip);
        return "abra cadabra";
    }

    // Update is called once per frame

    public GameObject targetObject;
    public TMPro.TMP_Text debugtxt;
    public KeyCode toggleKey = KeyCode.F2;

    bool started = false;
    void Update()
    {
        if (!started)
        {
            //witchSays("Everyone, come to my hut and start wishing! It wont cost much, just your soul! Hehe");

            initProblem();
            started = true;
        }

        if (Input.GetKeyDown(toggleKey) && targetObject != null)
        {
            // Toggle the active state
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}
