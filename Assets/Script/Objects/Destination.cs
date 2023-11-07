using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetPlayerWin(collision);
        }
    }

    private void SetPlayerWin(Collider2D collision)
    {
        collision.transform.position = new Vector2(transform.position.x, transform.position.y + .7f);
        collision.GetComponent<Rigidbody2D>().gravityScale = 0;
        collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.transform.rotation = Quaternion.Euler(0, 0, 0);
        collision.GetComponent<Rigidbody2D>().freezeRotation = true;
        collision.GetComponent<Animator>().Play("Idle");
        collision.GetComponent<Player>().winEffect.Play();
        GameManager.instance.Win();
    }
}
