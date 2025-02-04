using UnityEngine;
using UnityEngine.Events;

public class Killable : MonoBehaviour
{

    public int health = 1;
    public string[] takenDamageTags;
    public Animator animator;
    public Canvas UICanvas;

    public UnityEvent onDied;

    public UnityEvent OnDamage;



    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){Destroy(gameObject);}

        foreach (string tag in takenDamageTags){
            if (collision.gameObject.CompareTag(tag))
            {
                health --;
                OnDamage.Invoke();

                if (gameObject.CompareTag("Player")){animator.SetTrigger("TakeDamage");}
                
                
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

    public void updateHealth(int num)
    {
        health += num;
        UICanvas.GetComponent<HealthUIScript>().updateHealthUI();
        if (num < 0)
        {
            OnDamage.Invoke();
            if (gameObject.CompareTag("Player")){animator.SetTrigger("TakeDamage");}
        }
        
        

        if (health <= 0) 
        {
            onDied.Invoke();
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
