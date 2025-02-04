using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{
    public GameObject audioHandler;
    void Start()
    {
        audioHandler = GameObject.FindGameObjectWithTag("Audio");
    }


    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.CompareTag("Player"))
        {   
            audioHandler.GetComponents<AudioSource>()[1].Play();
            collision.gameObject.GetComponent<Killable>().updateHealth(1);
            Destroy(gameObject);
        }
        
    }


    
}
