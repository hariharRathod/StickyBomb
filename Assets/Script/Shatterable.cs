using UnityEngine;

public class Shatterable : MonoBehaviour
{
	private Rigidbody _rb;
	private bool _canShatter;
	private ShatterableParent _parent;
	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_parent = transform.parent.GetComponent<ShatterableParent>();
	}

	public void MakeShatterable()
	{
		_canShatter = true;
	}

	private void OnCollisionEnter(Collision other)
	{
		if(!_canShatter) return;
		
		if (!other.transform.CompareTag("TargetEnemy")) return;

		if (!_parent) return;
		_parent.ShatterTheShatterables();
		
	}

	public void Shatter()
	{
		transform.parent = null;
		_rb.isKinematic = false;
		_rb.useGravity = true;
		
	}
}
