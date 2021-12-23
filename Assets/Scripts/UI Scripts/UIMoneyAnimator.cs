using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoneyAnimator : MonoBehaviour
{
    public Sprite[] sprites;
    public Image img;
    Vector3 startpos;
    Vector3 targetPos;

    private void Awake()
    {
        img.sprite = sprites[Random.Range(0, sprites.Length)];
        startpos = transform.localPosition;
    }
    private void Start()
    {
        targetPos.x = Random.Range(-500, 500);
        targetPos.y = Random.Range(-500, 500);
        targetPos.z = 0;
        StartCoroutine(delay());

    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.3f));
        StartCoroutine(floatAnim());
    }
    float AnimTimer = 0;

    IEnumerator floatAnim()
    {
        yield return new WaitForEndOfFrame();
        AnimTimer += Time.deltaTime;
        if (AnimTimer <= 0.75)
        {
            StartCoroutine(floatAnim());
            transform.localPosition = Vector3.Lerp(startpos, targetPos, AnimTimer);
        }
        else
        {
            StartCoroutine(scaleAnim());

        }
    }
    IEnumerator scaleAnim()
    {
        yield return new WaitForEndOfFrame();
        AnimTimer += Time.deltaTime;
        if (AnimTimer<1)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, (1 - AnimTimer) / 0.25f);
            StartCoroutine(scaleAnim());
        }
        else
        {
            Destroy(gameObject);
        }


    }



}
