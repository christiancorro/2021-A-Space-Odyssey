using UnityEngine;

[System.Serializable]
public class Sentences : MonoBehaviour {
    [TextArea(10, 40)]
    public string[] sentences;
}
