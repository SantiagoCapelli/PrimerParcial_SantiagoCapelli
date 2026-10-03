using UnityEngine;
using TMPro;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private TMP_Text collectibleText;

    private int collectibles = 0;

    private void Start()
    {
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            collectibles++;

            Destroy(other.gameObject);

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        collectibleText.text = "Coleccionables: " + collectibles;
    }
}