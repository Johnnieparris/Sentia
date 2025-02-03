using UnityEngine;
using UnityEngine.Events;

public class Killable : MonoBehaviour
{

    public int health = 1;
    public string takenDamageTag;
    public Animator animator;

    public UnityEvent onDied;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(takenDamageTag))
        {
            health --;
            animator.SetTrigger("TakeDamage"); 
             if (health <= 0) 
            {
                onDied.Invoke();
            }
        }
        
        
    }

    public void destoryObejct(){
        Destroy(gameObject);
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
