using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private GameObject weaponInRadius;
    private Weapon currentHeldWeapon;
    private Camera cam;
    [SerializeField] private KeyCode pickUpKey;
    [SerializeField] private KeyCode dropKey;
    [SerializeField] private GameObject weaponHolder;
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetKeyDown(pickUpKey) && weaponInRadius != null)
            PickUpGun();
        if (Input.GetKeyDown(dropKey) && currentHeldWeapon != null)
            DropGun();

        if(currentHeldWeapon != null && currentHeldWeapon.weaponStats.isRapid && Input.GetMouseButton(0))
            currentHeldWeapon.Shoot();
        if(currentHeldWeapon != null && !currentHeldWeapon.weaponStats.isRapid && Input.GetMouseButtonDown(0))
            currentHeldWeapon.Shoot();
    }
    private void PickUpGun()
    {
        weaponInRadius.GetComponent<Rigidbody>().isKinematic = true;
        weaponInRadius.GetComponent<Collider>().enabled = false;
        weaponInRadius.transform.SetParent(weaponHolder.transform);
        LeanTween.moveLocal(weaponInRadius, Vector3.zero, 0.3f);
        LeanTween.rotateLocal(weaponInRadius, Vector3.zero, 0.3f);
        currentHeldWeapon = weaponInRadius.GetComponent<Weapon>();
    }
    private void DropGun()
    {
        weaponInRadius.transform.SetParent(null);
        currentHeldWeapon = null;
        var rb = weaponInRadius.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        weaponInRadius.GetComponent<Collider>().enabled = true;
        rb.AddForce(cam.transform.forward * 500);
        rb.angularVelocity = cam.transform.forward * 500;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableGun"))
            weaponInRadius = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickableGun"))
            weaponInRadius = null;
    }
}
public enum WeaponType
{
    PISTOL,
    AR
}