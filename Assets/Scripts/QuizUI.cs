using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuizUI : MonoBehaviour
{
    //Data
    [SerializeField] private QuizManager quizManager;
    private Question question;
    //UI
    [SerializeField] private Text questionText, scoreText, timerText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel, mainMenuPanel, gameMenuPanel;
    [SerializeField] private Image questionImage;
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;
    [SerializeField] private AudioSource questionAudio;
    [SerializeField] private List<Button> options,uiButtons;
    [SerializeField] private Color correctColor, wrongColor, normalColor;

    private bool answered;
    private float audioLength;

    public Text ScoreText { get { return scoreText; } }
    public Text TimerText { get { return timerText; } }

    private void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localButton = options[i];
            localButton.onClick.AddListener(() => OnClick(localButton));
        }
        for (int i = 0; i < uiButtons.Count; i++)
        {
            Button localButton = uiButtons[i];
            localButton.onClick.AddListener(() => OnClick(localButton));
        }
    }

    public void SetQuestion(Question question)
    {
        this.question = question;
        switch (question.questiontype)
        {
            case QUESTIONTYPE.TEXT:
                questionImage.transform.parent.gameObject.SetActive(false);          
                break;
            case QUESTIONTYPE.IMAGE:
                DataHolder();
                questionImage.transform.gameObject.SetActive(true);
                questionImage.sprite = question.questionImage;
                break;
            case QUESTIONTYPE.VIDEO:
                DataHolder();
                questionVideo.transform.gameObject.SetActive(true);
                questionVideo.clip = question.questionVideo;
                questionVideo.Play();
                break;
            case QUESTIONTYPE.AUDIO:
                DataHolder();
                questionAudio.transform.gameObject.SetActive(true);
                audioLength = question.questionAudio.length;
                StartCoroutine(PlayAudio());
                break;
        }
        questionText.text = question.questionInfo;

        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalColor;
        }

        answered = false;

    }
    IEnumerator PlayAudio()
    {
        if (question.questiontype == QUESTIONTYPE.AUDIO)
        {
            questionAudio.PlayOneShot(question.questionAudio);
            yield return new WaitForSeconds(audioLength+ 2f);
            StartCoroutine(PlayAudio());
        }
        else
        {
            StopCoroutine(PlayAudio());
            yield return null;
        }
        StopCoroutine(PlayAudio());

    }
    void DataHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(false);
        questionAudio.transform.gameObject.SetActive(false);
        questionVideo.transform.gameObject.SetActive(false);
    }
    private void OnClick(Button btn)
    {
        if (quizManager.GameStatus == GAMESTATUS.Playing)
        {
            if (!answered)
            {
                answered = true;
                bool value = quizManager.Answer(btn.name);
                if (value)
                {
                    btn.image.color = correctColor;
                    //GetComponent<Animator>().Play("New Animation");
                }
                else
                {
                    btn.image.color = wrongColor;
                }
            }
        }
        switch (btn.name)
        {
            case "Animal":
                break;
            case
                "Bird":
                break;
            case
                "Mix":
                break;
        }    
    }
}
