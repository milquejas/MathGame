using UnityEngine;

public class MathQuestionManager : MonoBehaviour
{
    public delegate void DisplayQuestionDelegate(MathQuestion mathQuestion);
    public static event DisplayQuestionDelegate OnDisplayQuestion;

    private MathQuestionGenerator questionGenerator;

    private void Start()
    {
        questionGenerator = new MathQuestionGenerator();
        DisplayMathQuestion();
    }

    private void DisplayMathQuestion()
    {
        // Kutsu questionGenerator-oliota generoimaan uusi matemaattinen teht�v�
        MathQuestion mathQuestion = questionGenerator.GenerateQuestionWithAnswers();

        // L�het� tapahtuma (event) ja v�lit� teht�v�n tiedot
        OnDisplayQuestion?.Invoke(mathQuestion);
    }
}
