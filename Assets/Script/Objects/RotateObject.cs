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
    private Vector2 pedestalLastPos;
    private void Update()
    {
        if(doubleClickedInput.IsDoubleClicked())
        {
            StartCoroutine(RotateObjects());
        }
    }

    private IEnumerator RotateObjects()
    {
        DisablePedestal();
        if (this.transform.localRotation.eulerAngles.z == 0)
        {
            this.gameObject.transform.DORotate(new Vector3(0, 0, 180), 0.5f).SetEase(Ease.OutElastic);
            pedestal.transform.DORotate(new Vector3(0, 0, 180), 0.5f).SetEase(Ease.OutElastic);
        }
        else if (this.transform.localRotation.eulerAngles.z == 180)
        {
            this.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutElastic);
            pedestal.transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.OutElastic);
        }
        doubleClickedInput.ResetDoubleClicked();
        yield return new WaitForSeconds(0.5f);
        EnablePedestal();

    }

    private void DisablePedestal()
    {
        pedestalLastPos = pedestal.transform.position;
        pedestal.transform.parent = null;
        pedestal.GetComponent<PolygonCollider2D>().enabled = false;
    }

    private void EnablePedestal()
    {
        pedestal.transform.parent = this.gameObject.transform;
        pedestal.transform.position = pedestalLastPos;
        pedestal.GetComponent<PolygonCollider2D>().enabled = true;
    }
}
