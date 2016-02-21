using UnityEngine;
using System.Collections;

public class Spline : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDrawGizmos()
    {
    }


    public static Vector3 Getcatmullrom(float t, params Vector3[] cp)
    {
        if (cp.Length < 4)
        {
            Debug.LogError("ControlPoints is less than four");
        }
        Vector3 ret = Vector3.zero;
        Vector3 v0 = 0.5f * (cp[2] - cp[0]);
        Vector3 v1 = 0.5f * (cp[3] - cp[1]);

        Vector3 t3 = Mathf.Pow(t, 3) * (2.0f  * cp[0]- 2.0f * cp[1] + v0 + v1);
        Vector3 t2 = Mathf.Pow(t, 2) * (-3.0f * cp[0]+ 3.0f * cp[1] - 2.0f * v0 - v1);
        Vector3 t1 = Mathf.Pow(t, 1) * v0;
        Vector3 t0 = cp[0];

        ret = t3 + t2 + t1 + t0;
        return ret;
    }

    public static Vector3 GetFergsonCoons(float t, Vector3 p1, Vector3 p2, Vector3 v1, Vector3 v2)
    {
        Vector3 ret = Vector3.zero;

        Vector3 t3 = Mathf.Pow(t, 3) * (2.0f * p1 - 2.0f * p2 + v1 + v2);
        Vector3 t2 = Mathf.Pow(t, 2) * (-3.0f * p1 + 3.0f * p2 - 2.0f * v1 - v2);
        Vector3 t1 = Mathf.Pow(t, 1) * v1;
        Vector3 t0 = p1;

        ret = t3 + t2 + t1 + t0;
        return ret;
    }
}
