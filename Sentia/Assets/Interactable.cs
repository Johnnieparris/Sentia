using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{

    public UnityEvent onPickup;

     void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.CompareTag("Player"))
        {
            onPickup.Invoke();
            Destroy(gameObject);
            collision.gameObject.GetComponent<Killable>().updateHealth(1);
        }
        
    }


    
}
