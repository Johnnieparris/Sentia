using UnityEngine;

public class AcidScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<Killable>().updateHealth(-1);
        }
    }
}
