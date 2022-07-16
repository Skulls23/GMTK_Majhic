using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        print("here");
        if (collision.transform.tag == "Enemy")
        {
            transform.parent.GetComponent<Bullet>().ChildTriggered(collision);
        }
    }
    /*private void OnTriggerStay2D(Collider2D other)
    {
        print("here");
        if (other.GetComponent<Collider2D>().tag == "Enemy")
        {
            transform.parent.GetComponent<Bullet>().ChildTriggered(other);
        }
    }*/
}
