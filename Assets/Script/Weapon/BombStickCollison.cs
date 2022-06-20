using UnityEngine;
using DG.Tweening;

public class BombStickCollison : MonoBehaviour
{

	private Rigidbody _rb;
	private bool _isSticked;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_isSticked = false;
	}
	private void OnCollisionEnter(Collision other)
	{

		if (_isSticked) return;

		if (other.transform.TryGetComponent(out IStickable transfomStickable))
		{
			if (transfomStickable.OnStick(this.gameObject, other.collider.transform))
			{
				_rb.isKinematic = true;
				_isSticked = true;
			}
		}

		if (other.transform.parent)
		{
			if (other.transform.parent.TryGetComponent(out IStickable parentStickable))
			{
				if (parentStickable.OnStick(this.gameObject, other.collider.transform))
				{
					_rb.isKinematic = true;
					_isSticked = true;
				}
			}
		}

		if (other.transform.root.TryGetComponent(out IStickable rootStickable))
		{
			if (rootStickable.OnStick(this.gameObject, other.collider.transform))
			{
				_rb.isKinematic = true;
				_isSticked = true;
			}
		}
			
		
	}
}
