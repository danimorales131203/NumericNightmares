using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] // Se requiere un BoxCollider2D en este objeto
public class Food2 : MonoBehaviour // Define la clase Food que hereda de MonoBehaviour
{
    public Collider2D gridArea; // Área del grid donde aparecerá la comida

    private Snake2 snake; // Referencia al script de la serpiente

    // Método llamado al despertar el objeto
    private void Awake()
    {
        snake = FindObjectOfType<Snake2>(); // Encuentra el script de la serpiente en la escena
    }

    // Método llamado al inicio del juego
    private void Start()
    {
        RandomizePosition(); // Llama al método para aleatorizar la posición de la comida
    }

    // Método para aleatorizar la posición de la comida
    public void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds; // Obtén los límites del área del grid

        int x, y;

        // Encuentra una posición válida para la comida
        do
        {
            // Elige una posición aleatoria dentro de los límites y redondea los valores para asegurarse de que estén en la cuadrícula
            x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
            y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));
        }
        while (snake.Occupies(x, y) || IsPositionOccupiedByObstacle(x, y));

        // Establece la posición de la comida
        transform.position = new Vector2(x, y);
    }

    // Método para verificar si una posición está ocupada por un obstáculo
    private bool IsPositionOccupiedByObstacle(int x, int y)
    {
        // Obtiene todos los objetos con el tag "Obstacle"
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        // Verifica si alguno de los obstáculos ocupa la posición (x, y)
        foreach (GameObject obstacle in obstacles)
        {
            if (Mathf.RoundToInt(obstacle.transform.position.x) == x &&
                Mathf.RoundToInt(obstacle.transform.position.y) == y)
            {
                return true;
            }
        }

        return false;
    }

    // Método llamado cuando otro objeto entra en colisión con este objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Verifica si el objeto con el que colisiona tiene el tag "Player" (la serpiente)
        {
            RandomizePosition(); // Llama al método para aleatorizar la posición de la comida cuando hay una colisión
        }
    }
}

