using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jitter : MonoBehaviour
{
    public float maxDistance = 1;

    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = new Vector2(
            startPos.x + Random.Range(-maxDistance, maxDistance),
            startPos.y + Random.Range(-maxDistance, maxDistance)
            );
        if (newPos.magnitude > maxDistance)
        {
            newPos = newPos.normalized * maxDistance;
        }
        transform.localPosition = newPos;
    }
}
