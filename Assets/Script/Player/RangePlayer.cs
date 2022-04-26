using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePlayer : PlayerBase
{
    [SerializeField] private GameObject shuriken;
    [SerializeField] private int launchSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        BasicAttack();
    }

    public override void BasicAttack()
    {
        if (Input.GetKeyDown( KeyCode.C))
        {
            dir = new Vector2(crossAir.transform.position.x - transform.position.x, crossAir.transform.position.y - transform.position.y);


            GameObject projectile = Instantiate(shuriken, transform.position, Quaternion.identity);
            Destroy(projectile, 5.0f);
            projectile.GetComponent<Rigidbody2D>().AddForce(dir * launchSpeed);
        }
    }
}
