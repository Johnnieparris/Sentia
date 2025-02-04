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
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            return;
        }

        foreach (string tag in takenDamageTags){
            if (collision.gameObject.CompareTag(tag))
            {
                if (collision.gameObject.CompareTag("BossDamage"))
                {
                    updateHealth(-5);
                } else 
                {
                    updateHealth(-1);
                }
                
                break;
            }
        }
        
    }

    public void destoryObejct(){
        Destroy(gameObject);
    }

    public void updateHealth(int num = 1)
    {
        health += num;
        if (gameObject.CompareTag("Player")){UICanvas.GetComponent<HealthUIScript>().updateHealthUI();}
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
