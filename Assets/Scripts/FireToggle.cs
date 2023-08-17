using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireToggle : MonoBehaviour
{
	[SerializeField]
	ParticleSystem fireParticle;
	[SerializeField]
	Light fireLight;

	public void Light(){
		fireParticle.Play();
		fireLight.enabled = true;
		fireParticle.GetComponent<AudioSource>().Play();
		
	}

	public void PutOut(){
		fireParticle.Stop();
		fireLight.enabled = false;
		fireParticle.GetComponent<AudioSource>().Stop();
	}
	
	
}
