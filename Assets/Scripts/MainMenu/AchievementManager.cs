using UnityEngine;

public class AchievementManager : MonoBehaviour {

    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private GameObject canvas;

    public void Create(string title, string description)
    {
        GameObject go = Instantiate(popup, canvas.transform);
        go.GetComponent<AchievementPopup>().SetDialogBox(title, description);
    }
}
