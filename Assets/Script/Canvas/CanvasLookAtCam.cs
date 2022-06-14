using UnityEngine;

public class CanvasLookAtCam : MonoBehaviour
{
	public bool flip = true;
	private Camera _cam;

    private void Start()
    {
		_cam = Camera.main; 
		if(TryGetComponent(out Canvas canva))
			canva.worldCamera = Camera.main;
    }

    private void Update()
    {
		//flipped because then the canvas gets flipped
		
		var direction = transform.root.position - _cam.transform.position;
        transform.rotation = Quaternion.LookRotation(flip ? direction : -direction);
    }
}
