using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ContinousArrowsParticlesIndication : MonoBehaviour
{
	[SerializeField] private ParticleSystem indicationParticles;

	private void Start()
	{
		if (!indicationParticles) return;
		
		DOVirtual.DelayedCall(0.2f,()=>indicationParticles.Play());

	}
}
