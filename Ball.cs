using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ObjectPooler.Instance.Despawn(gameObject);
        gameObject.transform.position = new Vector2(3, 3);
    }
}
