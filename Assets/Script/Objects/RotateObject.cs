using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private DoubleClickedInput doubleClickedInput;
    [SerializeField]
    private GameObject pedestal;
    [SerializeField]
    private Vector3 pedestalLastPos;
    [SerializeField]
    private Collider2D holderCollider;
    
    public void Rotate()
    {
        if(doubleClickedInput.IsDoubleClicked())
        {
            StartCoroutine(RotateObjects());
            doubleClickedInput.ResetDoubleClicked();
        }
    }

    private IEnumerator RotateObjects()
    {
        DisablePedestal();
        if (this.transform.localRotation.eulerAngles.z == 0)
        {
            this.gameObject.transform.DORotate(new Vector3(0, 0, 180), 0.5f).SetEase(Ease.OutElastic);
            pedestal.transform.DORotate(new Vector3(0, 0, 180), 0.4f).SetEase(Ease.OutElastic);
        }
        else if (this.transform.localRotation.eulerAngles.z == 180)
        {
            this.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutElastic);
            pedestal.transform.DORotate(new Vector3(0, 0, 0), 0.4f).SetEase(Ease.OutElastic);
        }
        yield return new WaitForSeconds(.5f);
        EnablePedestal();
    }

    private void DisablePedestal()
    {
        pedestalLastPos = new Vector3(this.transform.position.x, pedestal.transform.position.y, -1f);
        pedestal.transform.parent = this.transform.parent;
        pedestal.GetComponent<PolygonCollider2D>().enabled = false;   
    }

    private void EnablePedestal()
    {
        pedestal.GetComponent<PolygonCollider2D>().enabled = true;
        pedestal.transform.parent = this.gameObject.transform;
        pedestal.transform.position = pedestalLastPos;
    }
}
