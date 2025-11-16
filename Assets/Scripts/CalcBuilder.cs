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
        // Create the main calculator GameObject (use UI under a Canvas)
        GameObject calculator = new GameObject("GraphingCalculator");

        // Create a Canvas (optimistic: always create a fresh one)
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<UnityEngine.UI.CanvasScaler>();
        canvasGO.AddComponent<UnityEngine.UI.GraphicRaycaster>();

        // Parent the calculator under the Canvas
        calculator.transform.SetParent(canvasGO.transform, false);
        if (calculator.GetComponent<RectTransform>() == null)
            calculator.AddComponent<RectTransform>();

        RectTransform calcRect = calculator.GetComponent<RectTransform>();
        // Default size: center of screen, half width/height (adjust anchors as needed)
        calcRect.anchorMin = new Vector2(0.25f, 0.25f);
        calcRect.anchorMax = new Vector2(0.75f, 0.75f);
        calcRect.offsetMin = Vector2.zero;
        calcRect.offsetMax = Vector2.zero;

        // Create a black background rectangle that fills the calculator area
        GameObject background = new GameObject("Background", typeof(RectTransform), typeof(UnityEngine.CanvasRenderer), typeof(UnityEngine.UI.Image));
        background.transform.SetParent(calculator.transform, false);
        var bgImage = background.GetComponent<UnityEngine.UI.Image>();
        bgImage.color = Color.blue;
        RectTransform bgRect = background.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;
        
        // Add ExpressionField component
        GameObject expressionField = new GameObject("ExpressionField");
        expressionField.transform.SetParent(calculator.transform);
        expressionField.AddComponent<ExpressionField>();

        // Add ButtonGrid component for example expressions
        GameObject buttonGrid = new GameObject("ButtonGrid");
        buttonGrid.transform.SetParent(calculator.transform);
        buttonGrid.AddComponent<ButtonGrid>();

        // Add GraphVisual component
        GameObject graphVisual = new GameObject("GraphVisual");
        graphVisual.transform.SetParent(calculator.transform);
        graphVisual.AddComponent<GraphVisual>();
    }
}
