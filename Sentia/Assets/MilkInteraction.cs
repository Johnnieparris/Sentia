using UnityEngine;
using UnityEngine.Events;

public class MilkInteraction : MonoBehaviour
{
    public GameObject audioHandler;
    public GameObject player;

    private bool Bused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioHandler = GameObject.FindGameObjectWithTag("Audio");
    }


    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player") && !Bused)
        {
            Bused = true;
            player = collision.gameObject;
            audioHandler.GetComponents<AudioSource>()[1].Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            player.GetComponent<Weapon>().fireRate = 6f;
            player.GetComponent<PlayerCtrl>().moveSpeed = 3f;
            StartCoroutine(ReturnToNorm(5f));
        }

    System.Collections.IEnumerator ReturnToNorm(float delay){
            yield return new WaitForSeconds(delay);
            player.GetComponent<Weapon>().fireRate = 4f;
            player.GetComponent<PlayerCtrl>().moveSpeed = 2f;
            Destroy(gameObject);
    }
    }
}
