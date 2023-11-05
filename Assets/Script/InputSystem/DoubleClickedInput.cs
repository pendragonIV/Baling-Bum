
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClickedInput : MonoBehaviour
{
    private float clicked = 0;
    private float lastClickTime;
    private float clickDelay = 0.5f;
    private bool isDoubleClicked = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            clicked++;
            if (clicked == 1)
            {
                lastClickTime = Time.time;
            }

            if (clicked > 1 && Time.time - lastClickTime < clickDelay)
            {
                clicked = 0;
                lastClickTime = 0;
                isDoubleClicked = true;
            }
            else if (clicked > 2 || Time.time - lastClickTime > clickDelay)
            {
                clicked = 0;
                isDoubleClicked = false;
            }
        }

    }

    public bool IsDoubleClicked()
    {
        return isDoubleClicked;
    }

    public void ResetDoubleClicked()
    {
        isDoubleClicked = false;
    }
}
