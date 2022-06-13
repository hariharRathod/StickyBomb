
using UnityEngine;

public class BombRefBank : MonoBehaviour
{
	public BombStickCollison BombStickCollison { get; private set; }

	public BombArrowCollison BombArrowCollison { get; private set; }

	private void Start()
	{
		BombStickCollison = GetComponent<BombStickCollison>();
		BombArrowCollison = GetComponent<BombArrowCollison>();
	}
}
