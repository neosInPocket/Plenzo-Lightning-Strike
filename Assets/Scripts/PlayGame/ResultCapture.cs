using TMPro;
using UnityEngine;

public class ResultCapture : MonoBehaviour
{
	[SerializeField] private TMP_Text resultInfoText;
	[SerializeField] private TMP_Text coinsInfoText;
	[SerializeField] private TMP_Text tapInfoText;
	[SerializeField] private GameObject tryAgainNextTimeContainer;
	[SerializeField] private GameObject coinsContainer;

	public void GetResultWindow(int tapInfo, bool resultInfo, int coinsInfo)
	{
		resultInfoText.text = resultInfo ? "YOU WIN" : "LOSE";

		tryAgainNextTimeContainer.SetActive(!resultInfo);
		coinsContainer.SetActive(resultInfo);

		coinsInfoText.text = coinsInfo.ToString();
		tapInfoText.text = tapInfo.ToString();
	}
}
