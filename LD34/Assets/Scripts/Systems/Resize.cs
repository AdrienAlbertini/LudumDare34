﻿using UnityEngine;
using System.Collections;

public class Resize : MonoBehaviour
{
    void Awake()
    {
        //SpriteRenderer[] allSprites = (SpriteRenderer[])FindObjectsOfTypeAll(typeof(SpriteRenderer));

        //Debug.Log("AllSprites: " + allSprites.Length);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null)
            return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth;

        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;
    }
}