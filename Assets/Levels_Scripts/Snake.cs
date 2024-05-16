using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))] // Se requiere un BoxCollider2D en el objeto
public class Snake : MonoBehaviour // Define la clase Snake que hereda de MonoBehaviour
{
    public Transform segmentPrefab; // Prefab para el segmento de la serpiente
    public float speed = 20f; // Velocidad de movimiento de la serpiente
    public float speedMultiplier = 1f; // Multiplicador de velocidad
    public int initialSize = 4; // Tamaño inicial de la serpiente
    public bool moveThroughWalls = false; // Indica si la serpiente puede atravesar paredes
    public Text infoText; // Texto para mostrar mensajes
    
    public Animator animator; // Animator de la serpiente
    
    private List<Transform> segments = new List<Transform>(); // Lista de segmentos de la serpiente
    private Vector2Int direction = Vector2Int.right; // Dirección de movimiento
    private Vector2Int input; // Dirección de entrada
    private float nextUpdate; // Tiempo hasta la próxima actualización

    private string[] eatMessages = { // Mensajes mostrados al comer
        "Fórmula de integración por partes",
        "∫udv= ** −∫v**",
        "∫udv= ** −∫vdu",
        "∫udv= uv −∫vdu"
    };
    private int messageIndex = 0; // Índice actual del mensaje

    // Método llamado al inicio del juego
    private void Start()
    {
        ResetState(); // Reinicia el estado de la serpiente
    }

    // Método llamado en cada frame
    private void Update()
    {
        // Verifica las entradas de teclado para cambiar la dirección de la serpiente
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                input = Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                input = Vector2Int.down;
            }
        }
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                input = Vector2Int.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                input = Vector2Int.left;
            }
        }
    }

    // Método llamado en cada fixed frame rate frame
    private void FixedUpdate()
    {
        // Actualiza la dirección de la animación
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        // Verifica si es tiempo de actualizar la posición de la serpiente
        if (Time.time < nextUpdate)
        {
            return;
        }

        // Actualiza la dirección si hay una nueva entrada
        if (input != Vector2Int.zero)
        {
            direction = input;
        }

        // Mueve cada segmento de la serpiente
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Calcula la nueva posición de la cabeza de la serpiente
        int x = Mathf.RoundToInt(transform.position.x) + direction.x;
        int y = Mathf.RoundToInt(transform.position.y) + direction.y;
        transform.position = new Vector2(x, y);

        // Actualiza el tiempo para la próxima actualización
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }

    // Método para hacer crecer la serpiente
    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab); // Instancia un nuevo segmento
        segment.position = segments[segments.Count - 1].position; // Posiciona el segmento al final de la serpiente
        segments.Add(segment); // Agrega el segmento a la lista

        // Muestra un mensaje al comer
        if (messageIndex < eatMessages.Length)
        {
            infoText.text = eatMessages[messageIndex];
            messageIndex++;
        }
    }

    // Reinicia el estado de la serpiente
    public void ResetState()
    {
        direction = Vector2Int.right; // Restablece la dirección
        transform.position = Vector3.zero; // Reposiciona la serpiente al centro

        // Destruye los segmentos adicionales
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear(); // Limpia la lista de segmentos
        segments.Add(transform); // Agrega el segmento inicial

        messageIndex = 0; // Reinicia el índice de mensajes

        // Crea segmentos adicionales para alcanzar el tamaño inicial
        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }
    }

    // Verifica si la serpiente ocupa una posición específica
    public bool Occupies(int x, int y)
    {
        foreach (Transform segment in segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x &&
                Mathf.RoundToInt(segment.position.y) == y)
            {
                return true;
            }
        }

        return false;
    }

    // Método llamado cuando se produce una colisión
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica el tipo de objeto con el que colisiona
        if (other.gameObject.CompareTag("Food")) // Si es comida
        {
            Grow(); // Hace crecer la serpiente
        }
        else if (other.gameObject.CompareTag("Obstacle")) // Si es obstáculo
        {
            ResetState(); // Reinicia la serpiente
        }
        else if (other.gameObject.CompareTag("Wall")) // Si es pared
        {
            if (moveThroughWalls) // Si la serpiente puede atravesar paredes
            {
                Traverse(other.transform); // Atraviesa la pared
            }
            else // Si no puede atravesar paredes
            {
                ResetState(); // Reinicia la serpiente
            }
        }
    }

    // Método para atravesar una pared
    private void Traverse(Transform wall)
    {
        Vector3 position = transform.position;

        if (direction.x != 0f) // Si la dirección es horizontal
        {
            position.x = Mathf.RoundToInt(-wall.position.x + direction.x);
        }
        else if (direction.y != 0f) // Si la dirección es vertical
        {
            position.y = Mathf.RoundToInt(-wall.position.y + direction.y);
        }

        transform.position = position; // Actualiza la posición
    }
}
