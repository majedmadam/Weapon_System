using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponStats weaponStats;
    private Camera cam;
    private float nextFire;
    [SerializeField] private Transform tipOfGun;
    private void Start()
    {
        nextFire = weaponStats.fireRate;
        cam = Camera.main;
    }
    public void Shoot()
    {
        switch (weaponStats.weaponType)
        {
            case WeaponType.PISTOL:
                if(Time.time >= nextFire)
                {
                    nextFire = Time.time + weaponStats.fireRate;
                    OneShot();
                }
                break;
            case WeaponType.AR:
                if (Time.time >= nextFire)
                {
                    nextFire = Time.time + weaponStats.fireRate;
                    OneShot();
                }
                break;
        }
    }
    private void OneShot()
    {
        GameObject bulletInstance = Instantiate(weaponStats.bullet, tipOfGun.position, tipOfGun.rotation);
        var rb = bulletInstance.GetComponent<Rigidbody>();
        rb.AddForce(CalculateDirection() * weaponStats.bulletSpeed);
    }
    private Vector3 CalculateDirection()
    {
        var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 3f));
        RaycastHit hit;
        Vector3 dir;
        if (Physics.Raycast(ray, out hit))
            dir = (hit.point - tipOfGun.position).normalized;
        else
            dir = ray.direction;
        return dir;
    }
}
