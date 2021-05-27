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
    public int AnsQues;
    public int i, DisplayAns;
    private bool pressedinc = false;
    public string questiooon;
    public string ResoString;
    public int ThisAnswer = 2;
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
            PlayerPrefs.GetInt("DisplayAns");
            Debug.Log(DisplayAns);
                    if (DisplayAns==0)
                    {
                        ResoString = "The natural causes of earthquakes are sliding of tectonic plates and volcanic activities.";
                    }
                    else if (DisplayAns==1)
                    {
                        ResoString = "Learning simple first aid techniques can be very advantageous. ";
                    }
                    else if (DisplayAns==2)
                    {
                        ResoString = "Freezed Ham is not included because it is needed to be processed first in order to eat it.";
                    } 
                    else if (DisplayAns==3)
                    {
                        ResoString = "Spare Batteries and Flashlight are included in the emergency kit.";
                    }
                    else if (DisplayAns==4)
                    {
                        ResoString = "The Philippines is considered to be an earthquake prone country because it is located near the Pacific Ring of Fire.";
                    }
                    else if (DisplayAns==5)
                    {
                        ResoString = "“Batingaw” is the mobile application developed by NDRRMC for public use.";
                    }
                    else if (DisplayAns==6)
                    {
                        ResoString= "National Disaster Risk Reduction Management Council(NDRRMC) is the agency that is responsible for ensuring the protection and welfare of the people.";
                    }
                    else if (DisplayAns==7)
                    {
                        ResoString = "Philippine Institute of Volcanology and Seismology is the agency responsible for mitigation of disasters that arises from geotectonic phenomenas like volcanic eruptions, earthquakes, and tsunamis.";
                    }
                    else if (DisplayAns==8)
                    {
                        ResoString = "Philippine Atmospheric,Geophysical and Astronomical Services Administration(PAG-ASA) is the agency responsible for assessing and forecasting weather, flood, and other conditions essential for the welfare of the people.";
                    }
                    else if (DisplayAns==9)
                    {
                        ResoString = "Aftershocks, Tsunamis, Landslides are all possible effects of earthquakes.";
                    }
                     else if (DisplayAns==10)
                    {
                        ResoString = "Climb to safety immediately. Flash floods develop quickly. Do not wait until you see rising water.";
                    }
                    else if (DisplayAns==11)
                    {
                        ResoString = "Assemble disaster supplies. Emergency Kits are a MUST and can comes very handy in emergency situations.";
                    }
                    else if (DisplayAns==12)
                    {
                        ResoString = "Be prepared to evacuate. If you have a place you can stay, identify alternative routes that are not prone to flooding and immediately evacuate. If not, go to the designated evacuation assigned by the local government.";
                    }
                    else if (DisplayAns==13)
                    {
                        ResoString = "Discuss a disaster plan to your family. Discuss flood plans with your family. Decide where you will meet if separated.";
                    }
                    else if (DisplayAns==14)
                    {
                        ResoString= "You should not prioritze to save crops and animals. Get out of low areas that may be subject to flooding,prioritze your safety above all else";
                    }
                    else if (DisplayAns==15)
                    {
                        ResoString = "Evacuate immediately. Move to a safe area as soon as possible before access is cut off by rising water.";
                    }
                    else if (DisplayAns==16)
                    {
                        ResoString = "NEVER try to walk or swim through flowing water. If the water is moving swiftly, water 6 inches deep can knock you off your feet.";
                    }
                    else if (DisplayAns==17)
                    {
                        ResoString= "Shut off the electricity at the circuit breakers. Water conducts electricity and loose electric connection can result in someone being electrocuted.";
                    }

        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            
                    if (questiooon == "What are the natural cause of earthquakes?")
                    {
                        ResoString = "The natural causes of earthquakes are sliding of tectonic plates and volcanic activities.";
                    }
                    else if (questiooon == "What is the necessary skill to be learned in case of emergency?")
                    {
                        ResoString = "Learning simple first aid techniques can be very advantageous. ";
                    }
                    else if (questiooon == "Emergency kits should contain food in case of emergency, what is not needed?")
                    {
                        ResoString = "Freezed Ham is not included because it is needed to be processed first in order to eat it.";
                    } 
                    else if (questiooon == "Emergency kits should contain all of the essential tools that is needed for survival. What is not included?")
                    {
                        ResoString = "Spare Batteries and Flashlight are included in the emergency kit.";
                    }
                    else if (questiooon == "Why is the Philippines is considered to be an earthquake prone country?")
                    {
                        ResoString = "The Philippines is considered to be an earthquake prone country because it is located near the Pacific Ring of Fire.";
                    }
                    else if (questiooon == "NDRRMC mad a mobile application to provide a handy electronic resources to the public that can be utilize in case of emergency. What is the application called?")
                    {
                        ResoString = "“Batingaw” is the mobile application developed by NDRRMC for public use.";
                    }
                    else if (questiooon == "What is the agency responsible for ensuring the protection and welfare of the people?")
                    {
                        ResoString= "National Disaster Risk Reduction Management Council(NDRRMC) is the agency that is responsible for ensuring the protection and welfare of the people.";
                    }
                    else if (questiooon == "What government agency is responsible for mitigating disasters that arises from geotectonic phenomena? ")
                    {
                        ResoString = "Philippine Institute of Volcanology and Seismology is the agency responsible for mitigation of disasters that arises from geotectonic phenomenas like volcanic eruptions, earthquakes, and tsunamis.";
                    }
                    else if (questiooon == "What government agency is responsible for monitoring the weather changes in the Philippines?")
                    {
                        ResoString = "Philippine Atmospheric,Geophysical and Astronomical Services Administration(PAG-ASA) is the agency responsible for assessing and forecasting weather, flood, and other conditions essential for the welfare of the people.";
                    }
                    else if (questiooon == "What are the possible effects of earthquakes?")
                    {
                        ResoString = "Aftershocks, Tsunamis, Landslides are all possible effects of earthquakes.";
                    }
                     else if (questiooon == "You are currently residing in Brgy. Tomana in Marikina. You heard that the current water level in Marikina river has the potential to reach it’s critical level. What should you do?")
                    {
                        ResoString = "Climb to safety immediately. Flash floods develop quickly. Do not wait until you see rising water.";
                    }
                    else if (questiooon == "You recently attended a seminar about survival tips conducted by your barangay. What should you do?")
                    {
                        ResoString = "Assemble disaster supplies. Emergency Kits are a MUST and can comes very handy in emergency situations.";
                    }
                    else if (questiooon == "The rain is falling so hard due to the super typhoon in your area. There is a chance of flash floods. What should you do?")
                    {
                        ResoString = "Be prepared to evacuate. If you have a place you can stay, identify alternative routes that are not prone to flooding and immediately evacuate. If not, go to the designated evacuation assigned by the local government.";
                    }
                    else if (questiooon == "You saw in a documentary the danger of flash floods. The local government warns every Filipino family regarding this issue. What should you do?")
                    {
                        ResoString = "Discuss a disaster plan to your family. Discuss flood plans with your family. Decide where you will meet if separated.";
                    }
                    else if (questiooon == "You own a farm and a waist deep flood is currently devastating your farm. You should not?")
                    {
                        ResoString= "You should not prioritze to save crops and animals. Get out of low areas that may be subject to flooding,prioritze your safety above all else";
                    }
                    else if (questiooon == "You woke up early in the morning and you notice that the water is starting to rise. The local government failed to inform the citizens about the flood.  What should you do?")
                    {
                        ResoString = "Evacuate immediately. Move to a safe area as soon as possible before access is cut off by rising water.";
                    }
                    else if (questiooon == "A family of illegal settlers dwells near an estero. An unexpected rise of water level woke them up. What they should not do is ?")
                    {
                        ResoString = "NEVER try to walk or swim through flowing water. If the water is moving swiftly, water 6 inches deep can knock you off your feet.";
                    }
                    else if (questiooon == "A family is currently living in a household located in a low ground location. The area is currently suffering the onslaught of a super typhoon. The flood started to fill the premises. There are extension cords that are just laying on the ground ? What is the danger that the family may suffer?")
                    {
                        ResoString= "Shut off the electricity at the circuit breakers. Water conducts electricity and loose electric connection can result in someone being electrocuted.";
                    }

        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            
                    if (questiooon == "What are the natural cause of earthquakes?")
                    {
                        ResoString = "The natural causes of earthquakes are sliding of tectonic plates and volcanic activities.";
                    }
                    else if (questiooon == "What is the necessary skill to be learned in case of emergency?")
                    {
                        ResoString = "Learning simple first aid techniques can be very advantageous. ";
                    }
                    else if (questiooon == "Emergency kits should contain food in case of emergency, what is not needed?")
                    {
                        ResoString = "Freezed Ham is not included because it is needed to be processed first in order to eat it.";
                    } 
                    else if (questiooon == "Emergency kits should contain all of the essential tools that is needed for survival. What is not included?")
                    {
                        ResoString = "Spare Batteries and Flashlight are included in the emergency kit.";
                    }
                    else if (questiooon == "Why is the Philippines is considered to be an earthquake prone country?")
                    {
                        ResoString = "The Philippines is considered to be an earthquake prone country because it is located near the Pacific Ring of Fire.";
                    }
                    else if (questiooon == "NDRRMC mad a mobile application to provide a handy electronic resources to the public that can be utilize in case of emergency. What is the application called?")
                    {
                        ResoString = "“Batingaw” is the mobile application developed by NDRRMC for public use.";
                    }
                    else if (questiooon == "What is the agency responsible for ensuring the protection and welfare of the people?")
                    {
                        ResoString= "National Disaster Risk Reduction Management Council(NDRRMC) is the agency that is responsible for ensuring the protection and welfare of the people.";
                    }
                    else if (questiooon == "What government agency is responsible for mitigating disasters that arises from geotectonic phenomena? ")
                    {
                        ResoString = "Philippine Institute of Volcanology and Seismology is the agency responsible for mitigation of disasters that arises from geotectonic phenomenas like volcanic eruptions, earthquakes, and tsunamis.";
                    }
                    else if (questiooon == "What government agency is responsible for monitoring the weather changes in the Philippines?")
                    {
                        ResoString = "Philippine Atmospheric,Geophysical and Astronomical Services Administration(PAG-ASA) is the agency responsible for assessing and forecasting weather, flood, and other conditions essential for the welfare of the people.";
                    }
                    else if (questiooon == "What are the possible effects of earthquakes?")
                    {
                        ResoString = "Aftershocks, Tsunamis, Landslides are all possible effects of earthquakes.";
                    }
                     else if (questiooon == "You are currently residing in Brgy. Tomana in Marikina. You heard that the current water level in Marikina river has the potential to reach it’s critical level. What should you do?")
                    {
                        ResoString = "Climb to safety immediately. Flash floods develop quickly. Do not wait until you see rising water.";
                    }
                    else if (questiooon == "You recently attended a seminar about survival tips conducted by your barangay. What should you do?")
                    {
                        ResoString = "Assemble disaster supplies. Emergency Kits are a MUST and can comes very handy in emergency situations.";
                    }
                    else if (questiooon == "The rain is falling so hard due to the super typhoon in your area. There is a chance of flash floods. What should you do?")
                    {
                        ResoString = "Be prepared to evacuate. If you have a place you can stay, identify alternative routes that are not prone to flooding and immediately evacuate. If not, go to the designated evacuation assigned by the local government.";
                    }
                    else if (questiooon == "You saw in a documentary the danger of flash floods. The local government warns every Filipino family regarding this issue. What should you do?")
                    {
                        ResoString = "Discuss a disaster plan to your family. Discuss flood plans with your family. Decide where you will meet if separated.";
                    }
                    else if (questiooon == "You own a farm and a waist deep flood is currently devastating your farm. You should not?")
                    {
                        ResoString= "You should not prioritze to save crops and animals. Get out of low areas that may be subject to flooding,prioritze your safety above all else";
                    }
                    else if (questiooon == "You woke up early in the morning and you notice that the water is starting to rise. The local government failed to inform the citizens about the flood.  What should you do?")
                    {
                        ResoString = "Evacuate immediately. Move to a safe area as soon as possible before access is cut off by rising water.";
                    }
                    else if (questiooon == "A family of illegal settlers dwells near an estero. An unexpected rise of water level woke them up. What they should not do is ?")
                    {
                        ResoString = "NEVER try to walk or swim through flowing water. If the water is moving swiftly, water 6 inches deep can knock you off your feet.";
                    }
                    else if (questiooon == "A family is currently living in a household located in a low ground location. The area is currently suffering the onslaught of a super typhoon. The flood started to fill the premises. There are extension cords that are just laying on the ground ? What is the danger that the family may suffer?")
                    {
                        ResoString= "Shut off the electricity at the circuit breakers. Water conducts electricity and loose electric connection can result in someone being electrocuted.";
                    }

        }
        
        switch (type)
          {
              
            case ResolutionScreenType.Correct:
                PlayerPrefs.SetInt("ThisAnswer",1);
                //DogAnimation.Battle();
                uIElements.ResolutionBG.color = parameters.CorrectBGColor;
                uIElements.ResolutionStateInfoText.text = ResoString;
                uIElements.ResolutionScoreText.text = "+" + score;
                //PlayerPrefs.SetInt("ThisAnswer",2);
                break;
            case ResolutionScreenType.Incorrect:
                PlayerPrefs.SetInt("ThisAnswer",0);
                //DogAnimation.Battle();
                uIElements.ResolutionBG.color = parameters.IncorrectBGColor;
                uIElements.ResolutionStateInfoText.text = ResoString;
                uIElements.ResolutionScoreText.text = "-" + score;
                //PlayerPrefs.SetInt("ThisAnswer",2);
                break;
            case ResolutionScreenType.Finish:
                uIElements.ResolutionBG.color = parameters.FinalBGColor;
                uIElements.ResolutionStateInfoText.text = ResoString;
                

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