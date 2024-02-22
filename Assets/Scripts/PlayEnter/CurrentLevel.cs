using TMPro;
using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;

    private void Start()
    {
        levelText.text = DataCaptureController.Capture.level.ToString();
    }
}
