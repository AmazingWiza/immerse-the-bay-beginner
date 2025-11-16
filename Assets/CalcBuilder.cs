using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// CalcBuilder is responsible for generating a touch-manipulable graphing calculator interface in the Unity scene.
// It sets up the necessary components and configurations for user interaction--firing touch events and rendering the calculator UI.
// The top-level interface components are: ExpressionField, a ButtonGrid of ExampleExpressions, and the GraphVisual.
public class CalcBuilder : MonoBehaviour
{
    void Start()
    {
        BuildUI();
    }

    void BuildUI()
    {
        Canvas canvas = GetOrCreateCanvas();
        Font arial = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        // Root panel for calculator
        GameObject root = new GameObject("CalcRoot", typeof(RectTransform));
        root.transform.SetParent(canvas.transform, false);
        RectTransform rootRt = root.GetComponent<RectTransform>();
        rootRt.anchorMin = new Vector2(0.05f, 0.1f);
        rootRt.anchorMax = new Vector2(0.95f, 0.9f);
        rootRt.offsetMin = Vector2.zero;
        rootRt.offsetMax = Vector2.zero;

        VerticalLayoutGroup vlayout = root.AddComponent<VerticalLayoutGroup>();
        vlayout.spacing = 8f;
        vlayout.childControlHeight = true;
        vlayout.childControlWidth = true;
        vlayout.childForceExpandHeight = false;
        vlayout.childForceExpandWidth = true;

        // Expression field (placeholder)
        GameObject expr = new GameObject("ExpressionField", typeof(RectTransform));
        expr.transform.SetParent(root.transform, false);
        Image exprBg = expr.AddComponent<Image>();
        exprBg.color = new Color(1f, 1f, 1f, 0.9f);
        RectTransform exprRt = expr.GetComponent<RectTransform>();
        exprRt.sizeDelta = new Vector2(0, 40);

        GameObject exprTextGo = new GameObject("Text", typeof(RectTransform));
        exprTextGo.transform.SetParent(expr.transform, false);
        Text exprText = exprTextGo.AddComponent<Text>();
        exprText.font = arial;
        exprText.text = "Expression: (placeholder)";
        exprText.color = Color.black;
        exprText.alignment = TextAnchor.MiddleLeft;
        RectTransform etRt = exprTextGo.GetComponent<RectTransform>();
        etRt.anchorMin = new Vector2(0, 0);
        etRt.anchorMax = new Vector2(1, 1);
        etRt.offsetMin = new Vector2(8, 0);
        etRt.offsetMax = new Vector2(-8, 0);

        // Button grid (placeholder example expressions)
        GameObject grid = new GameObject("ButtonGrid", typeof(RectTransform));
        grid.transform.SetParent(root.transform, false);
        RectTransform gridRt = grid.GetComponent<RectTransform>();
        gridRt.sizeDelta = new Vector2(0, 160);

        GridLayoutGroup gridLayout = grid.AddComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2(120, 40);
        gridLayout.spacing = new Vector2(8, 8);
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = 3;

        string[] examples = new string[] { "sin(x)", "x^2", "x+1", "cos(x)", "log(x)", "e^x" };
        foreach (string ex in examples)
        {
            CreateButton(grid.transform, ex, arial);
        }

        // Graph visual placeholder
        GameObject graph = new GameObject("GraphVisual", typeof(RectTransform));
        graph.transform.SetParent(root.transform, false);
        Image graphImg = graph.AddComponent<Image>();
        graphImg.color = new Color(0.9f, 0.9f, 0.95f, 1f);
        RectTransform graphRt = graph.GetComponent<RectTransform>();
        graphRt.sizeDelta = new Vector2(0, 300);

        GameObject gTextGo = new GameObject("Text", typeof(RectTransform));
        gTextGo.transform.SetParent(graph.transform, false);
        Text gText = gTextGo.AddComponent<Text>();
        gText.font = arial;
        gText.text = "Graph Placeholder";
        gText.alignment = TextAnchor.MiddleCenter;
        gText.color = Color.black;
        RectTransform gtRt = gTextGo.GetComponent<RectTransform>();
        gtRt.anchorMin = new Vector2(0, 0);
        gtRt.anchorMax = new Vector2(1, 1);
        gtRt.offsetMin = Vector2.zero;
        gtRt.offsetMax = Vector2.zero;
    }

    Canvas GetOrCreateCanvas()
    {
        Canvas existing = FindObjectOfType<Canvas>();
        if (existing != null)
            return existing;

        GameObject canvasGo = new GameObject("Canvas", typeof(Canvas));
        Canvas canvas = canvasGo.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGo.AddComponent<CanvasScaler>();
        canvasGo.AddComponent<GraphicRaycaster>();

        // Ensure EventSystem exists
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        return canvas;
    }

    void CreateButton(Transform parent, string label, Font font)
    {
        GameObject btnGo = new GameObject("Button-" + label, typeof(RectTransform));
        btnGo.transform.SetParent(parent, false);
        Image img = btnGo.AddComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 1f);
        Button btn = btnGo.AddComponent<Button>();

        GameObject txtGo = new GameObject("Text", typeof(RectTransform));
        txtGo.transform.SetParent(btnGo.transform, false);
        Text txt = txtGo.AddComponent<Text>();
        txt.font = font;
        txt.text = label;
        txt.color = Color.black;
        txt.alignment = TextAnchor.MiddleCenter;

        RectTransform txtRt = txtGo.GetComponent<RectTransform>();
        txtRt.anchorMin = new Vector2(0, 0);
        txtRt.anchorMax = new Vector2(1, 1);
        txtRt.offsetMin = Vector2.zero;
        txtRt.offsetMax = Vector2.zero;
    }
}
