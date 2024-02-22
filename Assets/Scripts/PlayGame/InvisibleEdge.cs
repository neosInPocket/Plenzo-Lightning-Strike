using UnityEngine;

public class InvisibleEdge : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;

	public void SetSize(Vector2 size)
	{
		spriteRenderer.size = size;
	}
}
