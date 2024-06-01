using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript3 : MonoBehaviour
{
    public bool isCorrect = false; // Indica si esta respuesta es correcta
    public QuizManager3 quizManager; // Referencia al script QuizManager2

    public Color startColor; // Color inicial del botón

    private void Start()
    {
        startColor = GetComponent<Image>().color; // Obtiene el color inicial del botón al iniciar
    }

    // Método llamado cuando se selecciona la respuesta
    public void Answer()
    {
        if (isCorrect) // Si la respuesta es correcta
        {
            GetComponent<Image>().color = Color.green; // Cambia el color del botón a verde
            Debug.Log("Correct Answer"); // Imprime en la consola que la respuesta es correcta
            quizManager.correct(); // Llama al método correcto del quizManager2
        }
        else // Si la respuesta es incorrecta
        {
            GetComponent<Image>().color = Color.red; // Cambia el color del botón a rojo
            Debug.Log("Wrong Answer"); // Imprime en la consola que la respuesta es incorrecta
            quizManager.wrong(); // Llama al método incorrecto del quizManager2
        }
    }
}


