using UnityEngine;
using UnityEditor;

public class Window : EditorWindow
{
    public Color myColor;         // Градиент цвета
    public Vector3 myScale;
    public MeshRenderer GO;      // Ссылка на рендер объекта

    public Material newMaterial;
    private Transform MainCam;

    private bool isEditorActive = false;

    [MenuItem("Интструменты /Окна/ Генератор префабов ")] 
    public static void ShowMyWindow()
    {
        GetWindow(typeof(Window), false, "Генератор префабов");
    }

    void OnGUI()
    {
        GO = EditorGUILayout.ObjectField("Меш объекта", GO, typeof(MeshRenderer), true) as MeshRenderer;
        newMaterial = EditorGUILayout.ObjectField("Материал объекта", newMaterial, typeof(Material), true) as Material;

        if (GO)
        {
            myColor = RGBSlider(new Rect(10, 50, 200, 20), myColor);  // Отрисовка пользовательского набора слайдеров для получения градиента цвета
            GO.sharedMaterial.color = myColor; // Покраска объекта

            myScale = ScaleSlider(new Rect(10, 200, 200, 20), myScale);
            GO.transform.localScale = myScale;
        }
        else
        {
            if(GUI.Button(new Rect(10, 70, 100, 30), "Создать"))
            {
                MainCam = Camera.main.transform;

                GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                MeshRenderer GORenderer = temp.GetComponent<MeshRenderer>();
                GORenderer.sharedMaterial = newMaterial;
                temp.transform.position = new Vector3(MainCam.position.x, MainCam.position.y, MainCam.position.z - 5f);
                GO = GORenderer;

                myScale = GO.transform.localScale;

                isEditorActive = true;
            }
        }

        if (isEditorActive)
        {
            if (GUI.Button(new Rect(10, 140, 100, 30), "Сбросить"))
            {
                myColor = new Color(0, 0, 0);
            }

            if (GUI.Button(new Rect(10, 280, 100, 30), "Сбросить"))
            {
                myScale = new Vector3(1, 1, 1);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 50, 360, 100, 30), "Удалить"))
            {
                DestroyImmediate(GO.gameObject);
                GO = null;

                isEditorActive = false;
            }
        }
    }

    // Отрисовка пользовательского слайдера
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMaxValue, string labelText) // ДЗ добавить MinValue
    {
        // создаём прямоугольник с координатами в пространстве и заданой шириной и высотой 
        Rect labelRect = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(labelRect, labelText);   // создаём Label на экране

        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height); // Задаём размеры слайдера
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, 0.0f, sliderMaxValue); // Вырисовываем слайдер и считываем его параметр
        return sliderValue; // Возвращаем значение слайдера
    }

    // Отрисовка тройной слайдер группы, каждый слайдер отвечает за свой цвет
    Color RGBSlider(Rect screenRect, Color rgb)
    {
        // Используя пользовательский слайдер, создаём его
        rgb.r = LabelSlider(screenRect, rgb.r, 1.0f, "Red");

        // делаем промежуток
        screenRect.y += 20;
        rgb.g = LabelSlider(screenRect, rgb.g, 1.0f, "Green");

        screenRect.y += 20;
        rgb.b = LabelSlider(screenRect, rgb.b, 1.0f, "Blue");

        screenRect.y += 20;
        rgb.a = LabelSlider(screenRect, rgb.a, 1.0f, "alpha");

        return rgb; // возвращаем цвет
    }

    Vector3 ScaleSlider(Rect screenRect, Vector3 scale)
    {
        // Используя пользовательский слайдер, создаём его
        scale.x = LabelSlider(screenRect, scale.x, 5.0f, "Scale X");

        screenRect.y += 20;
        scale.y = LabelSlider(screenRect, scale.y, 5.0f, "Scale Y");

        screenRect.y += 20;
        scale.z = LabelSlider(screenRect, scale.z, 5.0f, "Scale Z");
        return scale; // возвращаем радиус
    }
}
