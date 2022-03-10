using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject); // destroy bullet
        Debug.Log("Ouch!");
    }
}
