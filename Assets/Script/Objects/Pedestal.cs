using UnityEngine;

public enum TypeOfPedestal
{
    Horizontal,
    Vertical
}

public class Pedestal : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    Vector2 mousePos;
    [SerializeField]
    public TypeOfPedestal typeOfPedestal;
    private float stayTime = 0f;

    private void OnMouseDrag()
    {
        if (GameManager.instance.IsInteractable())
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(mousePos, rb.transform.position) > .01f)
            {
                if (typeOfPedestal == TypeOfPedestal.Horizontal)
                    rb.MovePosition(new Vector2(mousePos.x, this.transform.position.y));
                else if (typeOfPedestal == TypeOfPedestal.Vertical)
                {
                    rb.MovePosition(new Vector2(this.transform.position.x, mousePos.y));
                }
            }
        }
    }

    private void Update()
    {
        if(stayTime > 2f)
        {
            GameManager.instance.Lose();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stayTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stayTime = 0f;
            GameManager.instance.Bounc();
        }
    }


}
