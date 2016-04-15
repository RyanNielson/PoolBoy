using UnityEngine;

public class PoolBoyTester : MonoBehaviour
{
    public GameObject projectile;

    private void Start()
    {
        InvokeRepeating("Fire", 0, 0.1f);
    }

    private void Fire()
    {
        PoolBoy.Instance.Spawn(projectile, transform.position);
    }
}
