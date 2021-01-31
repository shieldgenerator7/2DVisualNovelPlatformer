using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    [Range(0, 1)]
    public float minAlpha = 0;
    [Range(0, 1)]
    public float maxAlpha = 1;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = sr.color;
        color.a = Random.Range(minAlpha, maxAlpha);
        sr.color = color;
    }
}
