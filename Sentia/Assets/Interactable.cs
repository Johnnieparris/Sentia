using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{
    public GameObject audioHandler;
    void Start()
    {
        audioHandler = GameObject.FindGameObjectWithTag("Audio");
    }

    public UnityEvent onPickup;

     void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.CompareTag("Player"))
        {
            onPickup.Invoke();
            audioHandler.GetComponents<AudioSource>()[1].Play();
            Destroy(gameObject);
            collision.gameObject.GetComponent<Killable>().updateHealth(1);
        }
        
    }


    
}
