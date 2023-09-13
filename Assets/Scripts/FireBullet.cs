using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    
    public ParticleSystem muzzleFlash;
    
    public Animator revolverAnimator;
    
    public Transform magazine;
    public Transform bulletSpawn;
    
    public float bulletSpeed = 15f;
    public float bulletLife = 2.0f;

    public ActionBasedController holdingHand;

    private bool canShoot = true;
    private MagazineController magazineController;

    void Awake()
    {
         magazineController = GetComponent<MagazineController>();
    }

    public void Fire()
    {
        if (!canShoot || !magazineController.IsLoaded()) return;

        if (magazineController.IsActiveSocketLoaded())
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * bulletSpeed;
            muzzleFlash.Play();
            Destroy(bullet, bulletLife);
        }
        
        revolverAnimator.SetTrigger("Fire");
        canShoot = false;
        RotateMagazine();
        Recoil();
    }

    //smoothly rotate the magazine when firing by 60 degrees
    private async void RotateMagazine()
    {
        float duration = 0.5f;
        float elapsed = 0.0f;
        Quaternion startRotation = magazine.localRotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 60, 0);
        while (elapsed < duration)
        {
            magazine.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            await Task.Yield();
        }
        canShoot = true;
    }
    
    //add a small recoil effect by rotating the gun back and forth
    private async void Recoil()
    {
        // float duration = 0.1f;
        // float elapsed = 0.0f;
        //
        // Quaternion startRotation = transform.localRotation;
        // Quaternion endRotation = startRotation * Quaternion.Euler(-10, 0, 0);
        // while (elapsed < duration)
        // {
        //     transform.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
        //     elapsed += Time.deltaTime;
        //     await Task.Yield();
        // }
        // elapsed = 0.0f;
        // endRotation = startRotation * Quaternion.Euler(10, 0, 0);
        // while (elapsed < duration)
        // {
        //     transform.localRotation = Quaternion.Slerp(endRotation, startRotation, elapsed / duration);
        //     elapsed += Time.deltaTime;
        //     await Task.Yield();
        // }
        //
        
        
        //add f
    }
    
    public void SetCanShoot(bool value)
    {
        canShoot = value;
    }

}
