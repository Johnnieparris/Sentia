using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPreFab;
    public float fireForce = 20f;
    public Transform firePoint;
    public Transform Player;

    public float fireRate = 4;

    private float time = 1; 

    private void Update()
    {
 
    }

    public void Fire()
    {
        
        time += Time.deltaTime;

        float nextTimeToFire = 1/fireRate;
        Debug.Log(nextTimeToFire);

        if (time >= nextTimeToFire)
        {
          
            GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            Debug.Log("fire");
            time = 0;
        }
        
        
    }
 
}
