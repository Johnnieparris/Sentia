using UnityEngine;

public class Killable : MonoBehaviour
{

    public int health = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        health --;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
