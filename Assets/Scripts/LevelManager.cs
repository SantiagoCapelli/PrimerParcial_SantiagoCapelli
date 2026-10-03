using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject levelCompleteUI;

    private Vector3 respawnPoint;
    private Rigidbody playerRb;

    private void Start()
    {
        respawnPoint = player.position;
        playerRb = player.GetComponent<Rigidbody>();

        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(false);
    }

    public void RespawnPlayer()
    {
        player.position = respawnPoint;

        playerRb.linearVelocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }

    public void SetCheckpoint(Vector3 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }

    public void CompleteLevel()
    {
        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}