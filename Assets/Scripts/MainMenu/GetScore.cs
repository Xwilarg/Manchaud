using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GetScore : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = "Score: " + FindObjectOfType<Score>().GetScore();
    }
}
