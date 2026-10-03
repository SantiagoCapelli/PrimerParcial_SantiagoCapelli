using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.RespawnPlayer();
        }
    }
}