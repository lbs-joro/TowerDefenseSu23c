using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerShooter : MonoBehaviour
{

    List<GameObject> enemiesInRange = new List<GameObject>();

    public GameObject bulletPrefab;
    public GameObject gun;

    float cooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        //Finns det något i listan?
        if (enemiesInRange.Count > 0)
        {
            Vector2 aimDirection = enemiesInRange[0].transform.position - transform.position;

            //Skjuta på första fienden!
            if (cooldown < 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = aimDirection.normalized * 10f;

                cooldown = 1f;
            }
            float angle = Vector2.SignedAngle(Vector2.up, aimDirection);
            gun.transform.rotation = Quaternion.Euler(0, 0, angle);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Var det en fiende som gick in?
        
        if(collision.gameObject.tag == "Enemy")
        {
            print("Det gick in en fiende!");
            enemiesInRange.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            print("Det var en fiende som lämnade!");
            enemiesInRange.Remove(collision.gameObject);
        } 
    }
}
