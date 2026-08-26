using UnityEngine;

public class ClearZone : MonoBehaviour
{
    public GameObject ClearUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ClearUI.SetActive(true);
    }
}
