using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Destroy", 2.0f);
    }
    
    private void OnDisable()
    {
        CancelInvoke("Destroy");
    }

    private void Update()
    {
        transform.Translate(Vector2.up * 5 * Time.deltaTime);
    }

    private void Destroy()
    {
        PoolBoy.Instance.Despawn(gameObject);
    }
}
