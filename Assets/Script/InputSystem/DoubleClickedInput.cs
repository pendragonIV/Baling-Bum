
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClickedInput : MonoBehaviour
{
    private float clicked = 0;
    private float lastClickTime;
    private float clickDelay = 0.5f;
    private bool isClickable = true;
    private bool isDoubleClicked = false;
    private GameObject lastClickedObject;
    private GameObject currentClickedObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(isClickable)
            {
                CastRay();
            }
        }

        if (clicked > 2 || Time.time - lastClickTime > clickDelay)
        {
            clicked = 0;
            lastClickedObject = null;
        }
    }

    private void FixedUpdate()
    {
        if(isDoubleClicked && GameManager.instance.IsInteractable())
        {
            if(currentClickedObject.GetComponent<RotateObject>() != null)
            {
                currentClickedObject.GetComponent<RotateObject>().Rotate();
            }
            else if(currentClickedObject.transform.parent.GetComponent<RotateObject>() != null)
            {
                currentClickedObject.transform.parent.GetComponent<RotateObject>().Rotate();
            }
        }
    }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            if(lastClickedObject != null && lastClickedObject != hit.collider.gameObject)
            {
                lastClickedObject = hit.collider.gameObject;
                return;
            }
            lastClickedObject = hit.collider.gameObject;
            clicked++;
            
            if (clicked == 1)
            {
                lastClickTime = Time.time;
            }

            if (clicked > 1 && Time.time - lastClickTime <= clickDelay)
            {
                clicked = 0;
                lastClickTime = 0;
                isDoubleClicked = true;
                isClickable = false;
                currentClickedObject = lastClickedObject;
                lastClickedObject = null;
                StartCoroutine(WaitForNextCLick());
            }
        }

    }

    private IEnumerator WaitForNextCLick()
    {
        yield return new WaitForSeconds(.5f);
        isClickable = true;
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
