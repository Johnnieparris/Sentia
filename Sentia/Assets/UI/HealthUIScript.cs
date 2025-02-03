using TMPro;
using UnityEngine;

public class HealthUIScript : MonoBehaviour

{

    public GameObject player;
    public TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealthUI()
    {
        int newVal = player.GetComponent<Killable>().health;
        text.text = newVal.ToString();
    }
}
