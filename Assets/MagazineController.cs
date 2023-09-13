using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineController : MonoBehaviour
{
    
    public float speedThreshold = 1.25f;
    private Rigidbody rigidBody;
    private Animator animator;
    public float shownSpeed = 0;
    
    public List<XRSocketInteractor> bulletSockets = new List<XRSocketInteractor>();
    private int activeSocketIndex = 0;
    private int loadedBulletsCount = 0;
    private bool loaded = true;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        GetComponent<XRGrabInteractable>().activated.AddListener(ShootBullet);
        
        foreach (var bulletSocket in bulletSockets)
        {
            bulletSocket.selectEntered.AddListener(AddBullet);
            bulletSocket.selectExited.AddListener(RemoveBullet);
        }
        SortSockets();
    }

    private void Update()
    {
        CheckVelocity();
    }
    
    private void SortSockets()
    {
        List<XRSocketInteractor> sortedSockets = new List<XRSocketInteractor>();
        foreach(var bulletSocket in bulletSockets)
        {
            int socketNumber = bulletSocket.GetComponent<GunSocket>().SocketNumber;
            sortedSockets.Insert(socketNumber-1, bulletSocket);
        }
        bulletSockets = sortedSockets;
    }

    private void AddBullet(SelectEnterEventArgs args)
    {
        args.interactorObject.transform.GetComponent<GunSocket>().LoadBullet(args.interactableObject.transform.gameObject);
        loadedBulletsCount++;
    }
    
    private void RemoveBullet(SelectExitEventArgs args)
    {
        args.interactorObject.transform.GetComponent<GunSocket>().RemoveBullet();
        loadedBulletsCount--;
    }

    private void ShootBullet(ActivateEventArgs args)
    {
        if (!IsLoaded()) return;
        GunSocket socket = bulletSockets[activeSocketIndex].GetComponent<GunSocket>();
        if (socket.IsLoaded())
        {
            Destroy(socket.GetLoadedBullet());
            socket.RemoveBullet();
            loadedBulletsCount--;
        }
        activeSocketIndex++;
        if (activeSocketIndex > 5) activeSocketIndex = 0;
    }
    
    
    private void CheckVelocity()
    {
        Vector3 velocity = rigidBody.velocity;
        shownSpeed = velocity.x;
        
        //Trigger unload animation if the gun is moving to the left
        if (velocity.x < -speedThreshold)
        {
            animator.SetTrigger("Unload");
            loaded = false;
        }
        else if (velocity.x > speedThreshold)
        {
            animator.SetTrigger("Load");
            loaded = true;
        }
    }
    
    public bool IsLoaded()
    {
        return loaded;
    }
    
    public bool IsActiveSocketLoaded()
    {
        return bulletSockets[activeSocketIndex].GetComponent<GunSocket>().IsLoaded();
    }
    
}
