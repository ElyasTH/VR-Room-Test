using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSocket : MonoBehaviour
{
    [SerializeField, Range(1,6)]
    public int SocketNumber = 1;
    
    private GameObject loadedBullet;
    
    public void LoadBullet(GameObject bullet)
    {
        loadedBullet = bullet;
    }
    
    public GameObject GetLoadedBullet()
    {
        return loadedBullet;
    }
    
    public void RemoveBullet()
    {
        loadedBullet = null;
    }
    
    public bool IsLoaded()
    {
        return loadedBullet != null;
    }
}
