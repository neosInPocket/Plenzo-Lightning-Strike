using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private GameObject passEffect;

	public void Show(bool value, Vector2 position = new Vector2())
	{
		transform.position = position;
		gameObject.SetActive(value);

	}
}
