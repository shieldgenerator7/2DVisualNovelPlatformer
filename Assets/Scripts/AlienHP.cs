using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienHP : MonoBehaviour
{
    public int startHP;
    public bool dangerous = true;

    private int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = startHP;
    }

    public void damage()
    {
        hp--;
        checkHP();
        onDamaged?.Invoke();
    }
    public delegate void OnDamaged();
    public OnDamaged onDamaged;

    private void checkHP()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
