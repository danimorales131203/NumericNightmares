using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] // Se requiere un BoxCollider2D en este objeto
public class Food : MonoBehaviour // Define la clase Food que hereda de MonoBehaviour
{
    public Collider2D gridArea; // Área del grid donde aparecerá la comida

    private Snake snake; // Referencia al script de la serpiente

    // Método llamado al despertar el objeto
    private void Awake()
    {
        snake = FindObjectOfType<Snake>(); // Encuentra el script de la serpiente en la escena
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

        // Elige una posición aleatoria dentro de los límites y redondea los valores para asegurarse de que estén en la cuadrícula
        int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));

        // Evita que la comida aparezca en la serpiente
        while (snake.Occupies(x, y))
        {
            x++; // Incrementa la posición x

            // Si la posición x supera el límite máximo en x, reinicia x y aumenta y
            if (x > bounds.max.x)
            {
                x = Mathf.RoundToInt(bounds.min.x);
                y++;

                // Si la posición y supera el límite máximo en y, reinicia y
                if (y > bounds.max.y)
                {
                    y = Mathf.RoundToInt(bounds.min.y);
                }
            }
        }

        // Establece la posición de la comida
        transform.position = new Vector2(x, y);
    }

    // Método llamado cuando otro objeto entra en colisión con este objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        RandomizePosition(); // Llama al método para aleatorizar la posición de la comida cuando hay una colisión
    }
}
