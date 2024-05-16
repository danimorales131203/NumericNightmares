using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Componente de texto para mostrar el diálogo
    public string[] lines; // Arreglo de líneas de diálogo
    public float textSpeed; // Velocidad a la que se muestra el texto

    private int index; // Índice de la línea actual

    // Método llamado al inicio del juego
    void Start()
    {
        textComponent.text = string.Empty; // Inicializa el texto vacío
        StartDialogue(); // Inicia el diálogo
    }

    // Método llamado en cada frame
    void Update()
    {
        // Verifica si se hizo clic en el botón izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Si el texto actual es igual a la línea actual
            if (textComponent.text == lines[index])
            {
                NextLine(); // Muestra la siguiente línea
            }
            else // Si todavía se está escribiendo la línea
            {
                StopAllCoroutines(); // Detiene todas las coroutines
                textComponent.text = lines[index]; // Muestra la línea completa
            }
        }
    }

    // Inicia el diálogo
    void StartDialogue()
    {
        index = 0; // Reinicia el índice
        StartCoroutine(TypeLine()); // Inicia la coroutine para escribir la línea
    }

    // Coroutine para escribir la línea letra por letra
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray()) // Para cada letra en la línea
        {
            textComponent.text += c; // Agrega la letra al texto
            yield return new WaitForSeconds(textSpeed); // Espera un tiempo
        }
    }

    // Muestra la siguiente línea de diálogo
    void NextLine()
    {
        if (index < lines.Length - 1) // Si hay más líneas por mostrar
        {
            index++; // Aumenta el índice
            textComponent.text = string.Empty; // Limpia el texto
            StartCoroutine(TypeLine()); // Inicia la coroutine para escribir la siguiente línea
        }
        else // Si no hay más líneas por mostrar
        {
            gameObject.SetActive(false); // Desactiva el objeto que contiene este script
        }
    }
}
