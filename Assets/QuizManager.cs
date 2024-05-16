using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA; // Lista de preguntas y respuestas
    public GameObject[] options; // Array de opciones de respuesta
    public int currentQuestion; // Índice de la pregunta actual

    public GameObject Quizpanel; // Panel del quiz
    public GameObject GoPanel; // Panel de finalización

    public Text QuestionTxt; // Texto de la pregunta
    public Text ScoreTxt; // Texto de la puntuación

    int totalQuestions = 0; // Número total de preguntas
    public int score; // Puntuación actual

    private void Start()
    {
        totalQuestions = QnA.Count; // Obtiene el número total de preguntas
        GoPanel.SetActive(false); // Desactiva el panel de finalización
        generateQuestion(); // Genera la primera pregunta
    }

    // Método para volver a intentar el quiz
    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual
    }

    // Método llamado cuando se acaba el juego
    void GameOver()
    {
        Quizpanel.SetActive(false); // Desactiva el panel del quiz
        GoPanel.SetActive(true); // Activa el panel de finalización
        ScoreTxt.text = score + "/" + totalQuestions; // Actualiza el texto de puntuación
    }

    // Método llamado cuando la respuesta es correcta
    public void correct()
    {
        score += 1; // Aumenta la puntuación
        QnA.RemoveAt(currentQuestion); // Elimina la pregunta actual de la lista
        StartCoroutine(waitForNext()); // Espera antes de mostrar la siguiente pregunta
    }

    // Método llamado cuando la respuesta es incorrecta
    public void wrong()
    {
        QnA.RemoveAt(currentQuestion); // Elimina la pregunta actual de la lista
        StartCoroutine(waitForNext()); // Espera antes de mostrar la siguiente pregunta
    }

    // Corrutina para esperar antes de mostrar la siguiente pregunta
    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        generateQuestion(); // Genera la siguiente pregunta
    }

    // Método para configurar las respuestas
    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor; // Restaura el color de las opciones
            options[i].GetComponent<AnswerScript>().isCorrect = false; // Establece que la respuesta no es correcta
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i]; // Establece el texto de las opciones

            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true; // Marca la respuesta correcta
            }
        }
    }

    // Método para generar una pregunta aleatoria
    void generateQuestion()
    {
        if(QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count); // Elige una pregunta aleatoria

            QuestionTxt.text = QnA[currentQuestion].Question; // Establece el texto de la pregunta
            SetAnswers(); // Configura las respuestas
        }
        else
        {
            Debug.Log("Out of Questions"); // Si no hay más preguntas, muestra un mensaje en la consola
            GameOver(); // Finaliza el juego
        }
    }
}
