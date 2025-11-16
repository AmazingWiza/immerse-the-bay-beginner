using UnityEngine;

public class WireLoop : MonoBehaviour {
    public int segments = 50;
    public float radius = 2f;
    LineRenderer lr;
    public Vector3 direction;
    public float distance = 10;
    void Start() {
        direction = new Vector3(0, 0, 1);
        lr = GetComponent<LineRenderer>();
        lr.positionCount = segments + 1;

        for (int i = 0; i <= segments; i++) {
            float angle = i * Mathf.PI * 2f / segments;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            lr.SetPosition(i, pos);
        }
    }
    void Update()
    {
        if (transform.position.z<distance)
        {
            distance = 10;
            Backward();
        }
        else
        {
            distance = 0;
            Forward();
        }
        transform.Translate(direction);
    }

    void Forward()
    {
        direction = new Vector3(0, 0, -1 * Time.deltaTime);
    }
    void Backward()
    {
        direction = new Vector3(0, 0, 1 * Time.deltaTime);
    }
}