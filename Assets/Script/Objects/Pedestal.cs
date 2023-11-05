using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Pedestal : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;


    private void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(mousePos, rb.transform.position) > .09f)
        {
            rb.MovePosition(new Vector2(rb.transform.position.x, mousePos.y));
        }
    }
}
