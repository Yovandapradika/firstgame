using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
 
    [SerializeField] private QuizUi quizUi;
    [SerializeField] private List<QuizDataScriptable> quizData;
    [SerializeField] private float timeLimit = 60f;

    private List<Question> questions;
    private Question selectedQuestion;
    private int scoreCount = 0;
    private float currentTime;
    private int lifeRemaining = 3;

    private GameStatus gameStatus = GameStatus.Next;

    public GameStatus GameStatus { get { return gameStatus; } }

    public void StartGame(int index)
    {
        scoreCount = 0;
        currentTime = timeLimit;
        lifeRemaining = 3;
        questions = new List<Question>();

        for (int i = 0; i < quizData[index].questions.Count; i++ )
        {
            questions.Add (quizData[index].questions[i]);
        }


        SelectQuestion();
        gameStatus = GameStatus.Playing;
    }

    void SelectQuestion()
    {
        int val = UnityEngine.Random.Range(0, questions.Count);
        selectedQuestion = questions[val];

        quizUi.SetQuestion(selectedQuestion);

        questions.RemoveAt(val);
    }

    private void Update()
    {
        if(gameStatus == GameStatus.Playing)
        {
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
        }
    }

    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUi.TimerText.text = "Time:" + time.ToString("mm' : 'ss");

        if(currentTime <= 0 )
        {
            gameStatus = GameStatus.Next;
            quizUi.GameOverPanel.SetActive(true);
            quizUi.SkorAkhirText.text = "" + scoreCount;
            
        }
    }

    public bool Answer(string answered)
    {
        bool correctAns = false;

        if(answered == selectedQuestion.correctAns)
        {

            //yes
            correctAns = true;
            scoreCount += 50;
            quizUi.ScoreText.text = "Score:" + scoreCount;
        }
        else
        {
            //no
            lifeRemaining--;
            quizUi.ReduceLife(lifeRemaining);

            if(lifeRemaining <= 0 )
            {
                gameStatus = GameStatus.Next;
                quizUi.GameOverPanel.SetActive(true);
                 quizUi.SkorAkhirText.text = "" + scoreCount;
               
                
            }
        }

        if(gameStatus == GameStatus.Playing)
        {
            if(questions.Count > 0)
            {
                Invoke("SelectQuestion", 0.4f);
            }
            else
            {
                gameStatus = GameStatus.Next;
                quizUi.GameOverPanel.SetActive(true);
                 quizUi.SkorAkhirText.text = "" + scoreCount;
            }
        }

        return correctAns;

    }

}

[System.Serializable]
public class Question
{
    public string questionInfo;
    public List<string> options;
    public string correctAns;
    public QuestionType questionType;
    public Sprite questionImg;
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
}

[System.Serializable]

public enum GameStatus
{
    Playing,
    Next
}

