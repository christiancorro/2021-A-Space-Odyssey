using UnityEngine;

[System.Serializable]
public class Sentences : MonoBehaviour {
    [TextArea(3, 20)]
    public string[] sentences;
}
