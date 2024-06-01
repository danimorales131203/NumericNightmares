using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController6 : MonoBehaviour
{
    // Método para cargar la escena del menú principal
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Cambia "MainMenu" al nombre de tu escena de menú principal
    }
}