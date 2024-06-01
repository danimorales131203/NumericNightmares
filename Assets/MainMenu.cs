// Importa los espacios de nombres necesarios
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para gestionar las escenas en Unity
 
public class MainMenu : MonoBehaviour // Define una clase llamada MainMenu que hereda de MonoBehaviour
{
    // Este método se llama cuando se hace clic en el botón "Jugar" en el menú principal
    public void PlayGame()
    {
        // Carga la escena número 1 de forma asíncrona (mientras el juego está en ejecución)
        SceneManager.LoadScene("Warning");
    }
}

