using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable()]
public struct UIManagerParameters
{
    
    [Header("Answers Options")]
    [SerializeField] float margins;
    public float Margins { get { return margins; } }

    [Header("Resolution Screen Options")]
    [SerializeField] Color correctBGColor;
    public Color CorrectBGColor { get { return correctBGColor; } }
    [SerializeField] Color incorrectBGColor;
    public Color IncorrectBGColor { get { return incorrectBGColor; } }
    [SerializeField] Color finalBGColor;
    public Color FinalBGColor { get { return finalBGColor; } }
}
[Serializable()]
public struct UIElements
{
    [SerializeField] RectTransform answersContentArea;
    public RectTransform AnswersContentArea { get { return answersContentArea; } }

    [SerializeField] Image questionInfoImage;//image
    public Image QuestionInfoImage { get { return questionInfoImage; } }

    [SerializeField] TextMeshProUGUI questionInfoTextObject;
    public TextMeshProUGUI QuestionInfoTextObject { get { return questionInfoTextObject; } }

    [SerializeField] TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    [Space]

    [SerializeField] Animator resolutionScreenAnimator;
    public Animator ResolutionScreenAnimator { get { return resolutionScreenAnimator; } }

    [SerializeField] Image resolutionBG;
    public Image ResolutionBG { get { return resolutionBG; } }

    [SerializeField] TextMeshProUGUI resolutionStateInfoText;
    public TextMeshProUGUI ResolutionStateInfoText { get { return resolutionStateInfoText; } }

    [SerializeField] TextMeshProUGUI resolutionScoreText;
    public TextMeshProUGUI ResolutionScoreText { get { return resolutionScoreText; } }

    [Space]

    [SerializeField] TextMeshProUGUI highScoreText;
    public TextMeshProUGUI HighScoreText { get { return highScoreText; } }

    [SerializeField] CanvasGroup mainCanvasGroup;
    public CanvasGroup MainCanvasGroup { get { return mainCanvasGroup; } }

    [SerializeField] RectTransform finishUIElements;
    public RectTransform FinishUIElements { get { return finishUIElements; } }

    
}
public class UIManager : MonoBehaviour {

    #region Variables

    public enum         ResolutionScreenType   { Correct, Incorrect, Finish }

    [Header("References")]
    [SerializeField]    GameEvents             events                       = null;

    [Header("UI Elements (Prefabs)")]
    [SerializeField]    AnswerData             answerPrefab                 = null;

    [SerializeField]    UIElements             uIElements                   = new UIElements();

    [Space]
    [SerializeField]    UIManagerParameters    parameters                   = new UIManagerParameters();

    private             List<AnswerData>       currentAnswers               = new List<AnswerData>();
    private             int                    resStateParaHash             = 0;

    private             IEnumerator            IE_DisplayTimedResolution    = null;
 
    public int i;
    private bool pressedinc = false;
    public string questiooon;
    public string ResoString;
    public int ThisAnswer = 2;
    private int DisplayAns;
    #endregion

    #region Default Unity methods

    /// <summary>
    /// Function that is called when the object becomes enabled and active
    /// </summary>
    void OnEnable()
    {
        events.UpdateQuestionUI         += UpdateQuestionUI;
        events.DisplayResolutionScreen  += DisplayResolution;
        events.ScoreUpdated             += UpdateScoreUI;
    }
    /// <summary>
    /// Function that is called when the behaviour becomes disabled
    /// </summary>
    void OnDisable()
    {
        events.UpdateQuestionUI         -= UpdateQuestionUI;
        events.DisplayResolutionScreen  -= DisplayResolution;
        events.ScoreUpdated             -= UpdateScoreUI;
    }

    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        UpdateScoreUI();
        resStateParaHash = Animator.StringToHash("ScreenState");
    }

    #endregion

    /// <summary>
    /// Function that is used to update new question UI information.
    /// </summary>
    void UpdateQuestionUI(Question question)
    {
        uIElements.QuestionInfoImage.sprite = question.InfoImage;
        uIElements.QuestionInfoTextObject.text = question.Info;
        CreateAnswers(question);
        PlayerPrefs.SetString("Questioon", uIElements.QuestionInfoTextObject.text);
          
    }
    /// <summary>
    /// Function that is used to display resolution screen.
    /// </summary>
    void DisplayResolution(ResolutionScreenType type, int score)
    
    {
        UpdateResUI(type, score);
        pressedinc = false;
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 2);
        uIElements.MainCanvasGroup.blocksRaycasts = false;
        if (type != ResolutionScreenType.Finish)
        {
            
            if (IE_DisplayTimedResolution != null)
            {
                StopCoroutine(IE_DisplayTimedResolution);
            }
            IE_DisplayTimedResolution = DisplayTimedResolution();
            StartCoroutine(IE_DisplayTimedResolution);
        }
    }
    IEnumerator DisplayTimedResolution()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        while (!pressedinc)
        {
            if (Input.touchCount > 0 || Input.GetMouseButton(0))
                pressedinc = true;
            yield return null;
        }
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 1);
        uIElements.MainCanvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Function that is used to display resolution UI information.
    /// </summary>
    void UpdateResUI(ResolutionScreenType type, int score)
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);
        questiooon = PlayerPrefs.GetString("Questioon");
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
                    if (questiooon == "What is the most composed gas on Earth's atmosphere?")
                    {
                        ResoString = "Nitrogen";
                    }
                    else if (questiooon == "How many earth years does Jupiter have?")
                    {
                        ResoString = "12 Earth Years";
                    }
                    else if (questiooon == "It is the smallest planet in our solar system?")
                    {
                        ResoString = "Mercury";
                    } 
                    else if (questiooon == "A year on Neptune lasts ___ Earth years.")
                    {
                        ResoString = "165";
                    }
                    else if (questiooon == "What is the meaning of Earth in Germanic word?")
                    {
                        ResoString = "The ground";
                    }
                    else if (questiooon == "Jupiter have __ earth hours in a 1 earth day.")
                    {
                        ResoString = "10 earth hours";
                    }
                    else if (questiooon == "It is called rocky planet and also known as terrestrial planet?")
                    {
                        ResoString= "Mercury";
                    }
                    else if (questiooon == "Neptune’s atmosphere consists of ___, ___ and ___.")
                    {
                        ResoString = "Hydrogen, Helium and Methane";
                    }
                    else if (questiooon == "What is the fifth largest planet in the solar system?")
                    {
                        ResoString = "Earth";
                    }
                    else if (questiooon == "What is the surface Gravity of Jupiter?")
                    {
                        ResoString = "Approximately 2.5 times Earth’s";
                    }
                     else if (questiooon == "One day on Mercury spin takes how many earth days? ")
                    {
                        ResoString = "59 days";
                    }
                    else if (questiooon == "Neptune’s gravity is similar to that of ___.")
                    {
                        ResoString = "Earth";
                    }
                    else if (questiooon == "What does the Earth is made up?")
                    {
                        ResoString = "Rock and Metal";
                    }
                    else if (questiooon == "The Jupiter is the Largest planet in our solar system")
                    {
                        ResoString = "True";
                    }
                    else if (questiooon == "How many moons does Mercury have?")
                    {
                        ResoString= "None of the above";
                    }
                    else if (questiooon == "Uranus does not have any rings.")
                    {
                        ResoString = "False";
                    }
                    else if (questiooon == "These are floating on top of the magma interior of the Earth and can move against one another.")
                    {
                        ResoString = "Tectonic Plates";
                    }
                    else if (questiooon == "The Jupiter is the 6th planet from our sun")
                    {
                        ResoString= "False";
                    }


        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            
                    if (questiooon == "Earth is one of the planet in our solar system with liquid water on the surface.")
                    {
                        ResoString = "False";
                    }
                    else if (questiooon == "Jupiter atmosphere is made up of what element?")
                    {
                        ResoString = "Hydrogen and Helium";
                    }
                    else if (questiooon == "It is the nearest planet to the Sun?")
                    {
                        ResoString = "None of the above";
                    } 
                    else if (questiooon == "Uranus was the 1st planet ever discovered by use of a telescope.")
                    {
                        ResoString = "True";
                    }
                    else if (questiooon == "Our atmosphere protects us from incoming meteoroids.")
                    {
                        ResoString = "True";
                    }
                    else if (questiooon == "How many moons does Jupiter have?")
                    {
                        ResoString = "More than 75 Moons";
                    }
                    else if (questiooon == "The Distance between Mercury and the sun?")
                    {
                        ResoString= "36 million miles";
                    }
                    else if (questiooon == "How many moons does Uranus have?")
                    {
                        ResoString = "27";
                    }
                    else if (questiooon == "How many percentage of Earth's surface does the Water Covering?")
                    {
                        ResoString = "70%";
                    }
                    else if (questiooon == "The Average Diameter of Jupiter is  Approximately 88,846 miles.")
                    {
                        ResoString = "True";
                    }
                     else if (questiooon == "Mercury’s atmosphere and exosphere are mostly composed of oxygen, sodium, hydrogen, helium and, carbon dioxide.")
                    {
                        ResoString = "False";
                    }
                    else if (questiooon == "Uranus is a ___ giant.")
                    {
                        ResoString = "Ice";
                    }
                    else if (questiooon == "What is the average diameter of the Earth?")
                    {
                        ResoString = "7,918 miles";
                    }
                    else if (questiooon == "How many spacecraft have visited Jupiter?")
                    {
                        ResoString = "Nine Spacecraft";
                    }
                    else if (questiooon == "How many earth days to take Mercury a year?")
                    {
                        ResoString= "88 days";
                    }
                    else if (questiooon == "The axial rotation of Uranus is  ___.")
                    {
                        ResoString = "Sideways";
                    }
                    else if (questiooon == "What is the Earth's surface temperature?")
                    {
                        ResoString = "57.2 Farenheit";
                    }
                    else if (questiooon == "Jupiter has big storms like the Great Orange Spot, which has been going for hundreds of years. ")
                    {
                        ResoString= "False";
                    }
                    else if (questiooon == "The mercury’s shape is like elliptical egg.")
                    {
                        ResoString= "True";
                    }
                    else if (questiooon == "Who made the observation that made the call to make Uranus a proper planet?")
                    {
                        ResoString= "Sir William Herschel";
                    }
                    else if (questiooon == "The atmosphere of the Mars is made of")
                    {
                        ResoString= "Carbon dioxide, nitrogen, and argon";
                    }
                    else if (questiooon == "Saturn is the 6th planet from our Sun")
                    {
                        ResoString= "True";
                    }
                    else if (questiooon == "Mercury has a solid, cratered surface, such like the Earth’s moon?")
                    {
                        ResoString= "True";
                    }
                    else if (questiooon == "Uranus is the ___ planet in our solar system.")
                    {
                        ResoString= "Coldest";
                    }
                    else if (questiooon == "What is the surface temperature of Mars")
                    {
                        ResoString= "-81 degrees farenheit";
                    }
                    else if (questiooon == "Saturn atmosphere is made up of what element?")
                    {
                        ResoString= "Hydrogen and Helium";
                    }
                    else if (questiooon == "It is called the twin of the Earth.")
                    {
                        ResoString= "Venus";
                    }
                    else if (questiooon == "A year on Uranus is equivalent to ___ Earth years.")
                    {
                        ResoString= "84";
                    }
                    else if (questiooon == "The distance between Venus and the sun.")
                    {
                        ResoString= "108 million km";
                    }
                    else if (questiooon == "What are the moons of Uranus named after?")
                    {
                        ResoString= "Characters from Shakespeare";
                    }


        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            
                    if (questiooon == "A Mars makes a complete orbit around the Sun in how many Earth days")
                    {
                        ResoString = "687 Earth days";
                    }
                    else if (questiooon == "Except from Jupiter, Uranus and Neptune. The Saturn also have rings")
                    {
                        ResoString = "True";
                    }
                    else if (questiooon == "Venus has 2 moons and no rings")
                    {
                        ResoString = "False";
                    } 
                    else if (questiooon == "Neptune is the most distant planet.")
                    {
                        ResoString = "True";
                    }
                    else if (questiooon == "What is the name of the two moons of Mars")
                    {
                        ResoString = "Deimos and Phobos";
                    }
                    else if (questiooon == "Juno Spacecraft was Intentionally Vaporized in Saturn’s Atmosphere in 2017.")
                    {
                        ResoString = "False";
                    }
                    else if (questiooon == "Venus surface temperature is about what degrees?")
                    {
                        ResoString= "864 Fahrenheit";
                    }
                    else if (questiooon == "Neptune is the biggest “Gas Giant” in our solar system.")
                    {
                        ResoString = "False";
                    }
                    else if (questiooon == "Mars is known as the Red Planet")
                    {
                        ResoString = "True";
                    }
                    else if (questiooon == "Saturn is Fifth Largest Planet in our Solar System")
                    {
                        ResoString = "False";
                    }
                     else if (questiooon == "Venus average diameter:")
                    {
                        ResoString = "7,500 miles";
                    }
                    else if (questiooon == "Neptune is the ____ planet from the sun.")
                    {
                        ResoString = "8th";
                    }
                    else if (questiooon == "What is the average diameter of the Mars?")
                    {
                        ResoString = "4212 miles";
                    }
                    else if (questiooon == "how many earth years does Saturn have?")
                    {
                        ResoString = "About 29 earth years";
                    }
                    else if (questiooon == "Venus is the hottest planet in our solar system.")
                    {
                        ResoString= "True";
                    }
                    else if (questiooon == "How many moons does Neptune have?")
                    {
                        ResoString = "14";
                    }
                    else if (questiooon == "The fourth planet from the Sun is the")
                    {
                        ResoString = "Mars";
                    }
                    else if (questiooon == "What is the Diameter of Saturn?")
                    {
                        ResoString= "About 72,000 miles";
                    }
                    else if (questiooon == "What is the first spacecraft that discovered Venus?")
                    {
                        ResoString= "Mariner 2";
                    }
                    else if (questiooon == "How many rings does Neptune have?")
                    {
                        ResoString= "6";
                    }
                    else if (questiooon == "At this time, Mars' surface cannot support life")
                    {
                        ResoString= "True";
                    }
                    else if (questiooon == "What is the Effective Temperature in Saturn?")
                    {
                        ResoString= "288 Fahrenheit";
                    }
                    else if (questiooon == "What year Venus discovered ")
                    {
                        ResoString= "1962";
                    }
                    else if (questiooon == "Which is the biggest moon of Neptune?")
                    {
                        ResoString= "Triton";
                    }
                    else if (questiooon == "What is the average distance of Mars from the Sun")
                    {
                        ResoString= "About 142 million miles";
                    }
                    else if (questiooon == "Saturn have __ earth hours in a 1 earth day")
                    {
                        ResoString= "10.7 earth hours";
                    }
                    else if (questiooon == "How many spacecrafts explored Venus?")
                    {
                        ResoString= "40 spacecrafts";
                    }
                    else if (questiooon == "How many times have Neptune been ever visited close up?")
                    {
                        ResoString= "Only once";
                    }
                    else if (questiooon == "How many moons does the Saturn has?")
                    {
                        ResoString= "82 moons";
                    }
                    else if (questiooon == "Venus is a terrestrial planet.")
                    {
                        ResoString= "True";
                    }

        }
        
        switch (type)
          {
              
            case ResolutionScreenType.Correct:
                PlayerPrefs.SetInt("ThisAnswer",1);
                //DogAnimation.Battle();
                uIElements.ResolutionBG.color = parameters.CorrectBGColor;
                uIElements.ResolutionStateInfoText.text ="CORRECT ANSWER:" + ResoString;
                uIElements.ResolutionScoreText.text = "+" + score;
                //PlayerPrefs.SetInt("ThisAnswer",2);
                break;
            case ResolutionScreenType.Incorrect:
                PlayerPrefs.SetInt("ThisAnswer",0);
                //DogAnimation.Battle();
                uIElements.ResolutionBG.color = parameters.IncorrectBGColor;
                uIElements.ResolutionStateInfoText.text = "CORRECT ANSWER:" + ResoString;
                uIElements.ResolutionScoreText.text = "-" + score;
                //PlayerPrefs.SetInt("ThisAnswer",2);
                break;
            case ResolutionScreenType.Finish:
                uIElements.ResolutionBG.color = parameters.FinalBGColor;
                uIElements.ResolutionStateInfoText.text ="CORRECT ANSWER:" + ResoString;
                

                StartCoroutine(CalculateScore());
                    uIElements.FinishUIElements.gameObject.SetActive(true);
                    uIElements.HighScoreText.gameObject.SetActive(false);
                    uIElements.HighScoreText.text = ((highscore > events.StartupHighscore) ? "<color=yellow>new </color>" : string.Empty) + "Highscore: " + highscore;
                break;
            }

                    
                   
            
        
        
        
       
        
        
    }

    /// <summary>
    /// Function that is used to calculate and display the score.
    /// </summary>
    int scoreValue;
    IEnumerator CalculateScore()
    {
            uIElements.ResolutionScoreText.text = events.CurrentFinalScore.ToString();
            yield return null;
    }

    /// <summary>
    /// Function that is used to create new question answers.
    /// </summary>
    void CreateAnswers(Question question)
    {
        EraseAnswers();

        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerData newAnswer = (AnswerData)Instantiate(answerPrefab, uIElements.AnswersContentArea);
            newAnswer.UpdateData(question.Answers[i].Info, i);

            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);

            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
            uIElements.AnswersContentArea.sizeDelta = new Vector2(uIElements.AnswersContentArea.sizeDelta.x, offset * -1);

            currentAnswers.Add(newAnswer);
        }
    }
    /// <summary>
    /// Function that is used to erase current created answers.
    /// </summary>
    void EraseAnswers()
    {
        foreach (var answer in currentAnswers)
        {
            Destroy(answer.gameObject);
        }
        currentAnswers.Clear();
    }

    /// <summary>
    /// Function that is used to update score text UI.
    /// </summary>
    void UpdateScoreUI()
    {
        Debug.Log(events.CurrentFinalScore);
        uIElements.ScoreText.text = "Score: " + events.CurrentFinalScore;
    }
}