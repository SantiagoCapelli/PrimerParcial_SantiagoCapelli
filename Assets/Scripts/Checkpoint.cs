using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            levelManager.SetCheckpoint(transform.position + Vector3.up);
            activated = true;
        }
    }
}