using TMPro;
using UnityEngine;

public class CurrentCoins : MonoBehaviour
{
	[SerializeField] private TMP_Text coinsText;

	private void Start()
	{
		coinsText.text = DataCaptureController.Capture.goldCoins.ToString();
	}

	public void UpdateData()
	{
		coinsText.text = DataCaptureController.Capture.goldCoins.ToString();
	}
}
