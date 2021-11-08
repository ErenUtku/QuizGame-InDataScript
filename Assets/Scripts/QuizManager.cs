using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private List<Question> questions;
    private Question selectedQuestion;
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private QuizData quizData;

    private GAMESTATUS gameStatus = GAMESTATUS.Next;

    public GAMESTATUS GameStatus { get { return gameStatus; } }

    private void Start()
    {
        questions = quizData.question;

        Selected();
        gameStatus = GAMESTATUS.Playing;
    }


    void Selected()
    {
        int value = Random.Range(0, questions.Count);
        selectedQuestion = questions[value];
        quizUI.SetQuestion(selectedQuestion);
    }


    public bool Answer(string answered)
    {
        bool correctAnswer = false;
        if (answered == selectedQuestion.correctAnswer)
        {
            correctAnswer = true;
        }
        Invoke("Selected", 1f);
        return correctAnswer;
    }
}

