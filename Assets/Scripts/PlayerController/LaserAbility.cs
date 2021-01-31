using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAbility : PlayerAbility
{
    [Header("Settings")]
    public float delayBetweenShots = 0.5f;
    public float shotSpeed = 5;

    [Header("Components")]
    public GameObject shotPrefab;
    public Transform launchPosition;

    [Header("Sound Effects")]
    public AudioClip shootSound;

    private float lastShootTime;

    protected override void init()
    {
        PlayerController.OnPlayerShoot += shoot;
    }

    protected override void OnDisable()
    {
        PlayerController.OnPlayerShoot -= shoot;
    }

    private void shoot()
    {
        if (Time.time > lastShootTime + delayBetweenShots)
        {
            lastShootTime = Time.time;
            GameObject shot = Instantiate(shotPrefab);
            shot.transform.position = launchPosition.transform.position;
            shot.GetComponent<Rigidbody2D>().velocity =
                Vector2.right * Mathf.Sign(transform.localScale.x) * shotSpeed;
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
    }

}
