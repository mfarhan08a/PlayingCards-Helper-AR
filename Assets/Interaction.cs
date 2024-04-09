using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private AppManager appManager;
    [SerializeField] public TextMeshProUGUI textTargetName;

    void Awake()
    {
        textTargetName = FindObjectOfType<TextMeshProUGUI>();
    }

    void Update()
    {
        if (appManager.elementStatus.ContainsValue(true))
        {
            textTargetName.text = appManager.GetHandCardsType();
            return;
        }
        textTargetName.text = "There is no card";
    }
}
