using UnityEngine;

public class Effects : MonoBehaviour
{
	[SerializeField] private AudioSource[] sources;

	private void Start()
	{
		if (DataCaptureController.Capture.effectsAvaliable)
		{
			for (int k = 0; k < sources.Length; k++)
			{
				sources[k].volume = 0;
			}
		}
	}
}
