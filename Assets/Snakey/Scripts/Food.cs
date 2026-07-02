using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SnakeMovement snake = other.GetComponent<SnakeMovement>();
        if (snake != null)
        {
            snake.Grow();
            
            if (GameManager.Instance != null) {
                GameManager.Instance.AddScore(10);
            }

            Destroy(gameObject); 
        }
    }
}