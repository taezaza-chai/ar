using UnityEngine;

public class ShootControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform cameraPosition;

    [Header("Setting")]
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifeTime = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.OnShootButtonPressed += Shoot;
    }

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, cameraPosition.position, Quaternion.identity);
        var rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(cameraPosition.forward * bulletSpeed);
        Destroy(bullet,bulletLifeTime);
    }
}
