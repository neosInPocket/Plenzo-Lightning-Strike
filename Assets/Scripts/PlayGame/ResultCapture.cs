using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultCapture : MonoBehaviour
{
	[SerializeField] private TMP_Text resultInfoText;
	[SerializeField] private TMP_Text coinsInfoText;
	[SerializeField] private GameObject tryAgainNextTimeContainer;
	[SerializeField] private GameObject coinsContainer;

	public void GetResultWindow(bool resultInfo, int coinsInfo)
	{
		gameObject.SetActive(true);
		resultInfoText.text = resultInfo ? "LEVEL COMPLETED!" : "LOSE";

		tryAgainNextTimeContainer.SetActive(!resultInfo);
		coinsContainer.SetActive(resultInfo);

		coinsInfoText.text = coinsInfo.ToString();
	}

	public void LoadNew()
	{
		SceneManager.LoadScene("PlayGame");
	}

	public void LoadPrev()
	{
		SceneManager.LoadScene("PlayEnter");
	}
}
