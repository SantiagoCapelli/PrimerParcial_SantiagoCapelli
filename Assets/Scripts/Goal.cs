using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.CompleteLevel();
        }
    }
}