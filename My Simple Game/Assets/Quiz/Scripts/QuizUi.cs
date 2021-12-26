using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizUi : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private Text questionText, scoreText, timerText, scoreakhirText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel, mainMenuPanel, gameMenuPanel, halamanTentangPanel;
    [SerializeField] private Image questionImage;
    [SerializeField] private List<Button> options, uiButtons;
    [SerializeField] private Color CorrectCol, WrongCol, NormalCol;

    private Question question;
    private bool answered;

    public Text ScoreText { get { return scoreText; } }

    public Text TimerText { get { return timerText; } }

    public Text SkorAkhirText { get { return scoreakhirText; } }

    public GameObject GameOverPanel { get { return gameOverPanel; } } 

    public GameObject Halamantentang { get { return halamanTentangPanel; } } 

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        for(int i = 0; i < uiButtons.Count; i++)
        {
            Button localBtn = uiButtons[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    public void SetQuestion(Question question)
    {
        this.question = question;
        
        switch (question.questionType)
        {
            case QuestionType.TEXT:

                questionImage.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionType.IMAGE:
                ImageHolder();
                questionImage.transform.gameObject.SetActive(true);
                questionImage.sprite = question.questionImg;
                break;
        }

        questionText.text = question.questionInfo;

        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0; i < options.Count; i++)
        {
           options[i].GetComponentInChildren<Text>().text = answerList[i];
           options[i].name = answerList[i];
           options[i].image.color = NormalCol; 
        }

        answered = false;

    }

    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(false);

    }

    private void OnClick(Button btn)
    {
        if(quizManager.GameStatus == GameStatus.Playing)
        {
            if (!answered)
            {
                answered = true;
                bool val = quizManager.Answer(btn.name);

                if(val)
                {
                    btn.image.color = CorrectCol;

                }
                else
                {
                    btn.image.color = WrongCol;
                }
            }
        }

        switch (btn.name)
        {
            
        
            case "Acak":
            quizManager.StartGame(0);
            mainMenuPanel.SetActive(false);
            gameMenuPanel.SetActive(true);
                break;
            
        }
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AboutPage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   
    

    public void ReduceLife(int index)
    {
        lifeImageList[index].color = WrongCol;
    }

   
}
