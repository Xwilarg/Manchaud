using UnityEngine;
using UnityEngine.UI;

public class AchievementPopup : MonoBehaviour
{
    [SerializeField]
    private Text title, description;
    private float timer;
    private Image mat;

    private void Start()
    {
        timer = 3f;
        mat = GetComponent<Image>();
    }

    public void SetDialogBox(string newTitle, string newDescription)
    {
        title.text = newTitle;
        description.text = newDescription;
    }

    private void Update()
    {
        transform.Translate(new Vector2(0f, 10f * Time.deltaTime));
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - (0.2f * Time.deltaTime));
        timer -= Time.deltaTime;
        if (timer < 0f)
            Destroy(gameObject);
    }
}
