using System;
using System.Collections;
using System.Collections.Generic;
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

    private bool canShoot;

    public void Fire()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * bulletSpeed;
        revolverAnimator.SetTrigger("Fire");
        muzzleFlash.Play();
        Destroy(bullet, bulletLife);
        
        RotateMagazine();
        Recoil();


    }

    //smoothly rotate the magazine when firing by 60 degrees
    private async void RotateMagazine()
    {
        float duration = 0.2f;
        float elapsed = 0.0f;
        Quaternion startRotation = magazine.localRotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 60, 0);
        while (elapsed < duration)
        {
            magazine.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            await Task.Yield();
        }
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
