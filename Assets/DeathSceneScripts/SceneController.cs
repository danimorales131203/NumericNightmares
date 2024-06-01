using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Método para cargar una escena específica para volver a intentar (retry)
    public void RetryLevel()
    {
        SceneManager.LoadScene("Level1"); // Cambia "RetryScene" al nombre de tu escena de reintento
    }

    // Método para cargar la escena del menú principal
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Cambia "MainMenu" al nombre de tu escena de menú principal
    }
}


