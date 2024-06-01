using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController2 : MonoBehaviour
{
    // Método para cargar una escena específica para volver a intentar (retry)
    public void Continue()
    {
        SceneManager.LoadScene("Level1"); // Cambia "RetryScene" al nombre de tu escena de reintento
    }

}
