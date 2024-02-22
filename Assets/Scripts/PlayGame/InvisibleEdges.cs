using System.Collections.Generic;
using UnityEngine;

public class InvisibleEdges : MonoBehaviour
{
	[SerializeField] private List<InvisibleEdge> invisibleEdges;

	private void Start()
	{
		SetEdges();
	}

	private void SetEdges()
	{
		float ratio = (float)Screen.height / (float)Screen.width;
		var ortho = Camera.main.orthographicSize;
		var screenSize = new Vector2(ortho / ratio, ortho);

		invisibleEdges[3].SetSize(new Vector2(2 * screenSize.x, 1));
		invisibleEdges[2].SetSize(new Vector2(2 * screenSize.x, 1));
		invisibleEdges[1].SetSize(new Vector2(1, 2 * screenSize.y));
		invisibleEdges[0].SetSize(new Vector2(1, 2 * screenSize.y));

		invisibleEdges[3].transform.position = new Vector2(0, -screenSize.y - 1f / 2f);
		invisibleEdges[2].transform.position = new Vector2(0, screenSize.y + 1f / 2f);
		invisibleEdges[1].transform.position = new Vector2(screenSize.x + 1f / 2f, 0);
		invisibleEdges[0].transform.position = new Vector2(-screenSize.x - 1f / 2f, 0);
	}
}
