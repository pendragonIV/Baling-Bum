using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform circleBorder;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private bool isFalling;

    public ParticleSystem winEffect;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(PlayerIdle());
    }

    private void Update()
    {
        circleBorder.Rotate(0, 0, 20 * Time.deltaTime);
        if(transform.position.y < GameManager.instance.destination.transform.position.y - 1f)
        {
            StartCoroutine(WaitToLose());
        }
    }

    private IEnumerator WaitToLose()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.Lose();
    }

    private void OnMouseDown()
    {
        circleBorder.DOScale(0, .3f).OnComplete(() => circleBorder.gameObject.SetActive(false));
        rb.gravityScale = 1;
        animator.Play("Falling");
        isFalling = true;
        StopCoroutine(PlayerIdle());
        GameManager.instance.NonInteractable();
    }

    private IEnumerator PlayerIdle()
    {
        animator.Play("Idle");
        while (!isFalling)
        {
            yield return new WaitForSeconds(3f);
            transform.DORotate(new Vector3(0, 0, 360), .3f, RotateMode.FastBeyond360);
        }
    }


}
