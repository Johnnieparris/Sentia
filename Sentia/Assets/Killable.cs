using UnityEngine;
using UnityEngine.Events;

public class Killable : MonoBehaviour
{

    public int health = 1;
    public string[] takenDamageTags;
    public Animator animator;

    public UnityEvent onDied;

    public UnityEvent OnDamage;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        foreach (string tag in takenDamageTags){
            if (collision.gameObject.CompareTag(tag))
            {
                health --;
                OnDamage.Invoke();

                animator.SetTrigger("TakeDamage");
                
                if (health <= 0) 
                {
                    onDied.Invoke();
                }
                break;
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
