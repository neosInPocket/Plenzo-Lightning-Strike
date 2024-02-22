using System;
using TMPro;
using UnityEngine;

public class GamePlayShow : MonoBehaviour
{
	[SerializeField] private string[] textsStrings;
	[SerializeField] private TMP_Text currentText;
	public Action<int> GamePlayShowEnd { get; set; }

	public void ShowGameplay()
	{
		gameObject.SetActive(true);
	}
}
