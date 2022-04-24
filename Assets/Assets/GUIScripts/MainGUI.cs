using UnityEngine;


public class MainGUI : MonoBehaviour
{
    float _health = 100f;

#if UNITY_EDITOR
    void OnGUI()
    {
        // Группа GUI
        string _strHealth = "Health: " + _health.ToString() + "%";
        GUI.Box(new Rect(Screen.width / 2 - _strHealth.Length / 2, Screen.height - 25, 100, 30), _strHealth);
    }
#endif
}
