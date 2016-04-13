using UnityEngine;
using System.Collections;

public class PoolBoyTester : MonoBehaviour
{
    public GameObject prefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoolBoy.Instance.Spawn(prefab);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SpriteRenderer spriteRenderer = FindObjectOfType<SpriteRenderer>();

            if (spriteRenderer)
            {
                PoolBoy.Instance.Despawn(spriteRenderer.gameObject);
            }
        }
    }
}
