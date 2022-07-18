using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageRefBank : MonoBehaviour
{
	public HostageController Controller { get; private set; }
	public HostageRunController RunController { get; private set; }

	private void Start()
	{
		if (transform.TryGetComponent(out HostageController hostageController))
			Controller = hostageController;

		if (transform.TryGetComponent(out HostageRunController hostageRunController))
			RunController = hostageRunController;
	}
}
