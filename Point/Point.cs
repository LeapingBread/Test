using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Point : MonoBehaviour
{
    public Point previousPoint;
    public Point linkedPoint;
    public bool canClick;
    public bool isFirst;
    public bool isEnd;
    [HideInInspector]
    public bool clicked;
    SpriteRenderer spriteRenderer;
    [SerializeField]Sprite clickedSprite;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetClcikedSprite()
    {
        spriteRenderer.sprite = clickedSprite;
    }
    public Point GetCurrentClikedPoint()
    {
        if (canClick)
        {
            return this;
        }
        else
            return null;
    }
    public void SetPoint()
    {
        if(canClick)
        {
            if (linkedPoint != null)
                linkedPoint.canClick = true;
            SetClcikedSprite();
            clicked = true;
            canClick = false;
        }
    }
    public void FadeOut()
    {
        Color targetColor = new Color(1, 1, 1, 0);
        transform.GetChild(0).GetComponent<SpriteRenderer>().DOColor(targetColor, 1f);
    }

}
