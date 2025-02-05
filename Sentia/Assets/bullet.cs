using UnityEngine;

public class bullet : MonoBehaviour
{

    void Start()
    {
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(1f, 1.1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
