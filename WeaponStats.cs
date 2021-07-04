using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Manager/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    public float fireRate;
    public int magazineSize;
    public float reloadTime;
    public float damagePerShot;
    public float bulletSpeed;
    public GameObject bullet;
    public bool isRapid;
    public WeaponType weaponType;
}
