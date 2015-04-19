using UnityEngine;
using System.Collections;

public class CatmullRom : MonoBehaviour
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


    public Vector3 Getcatmullrom(float t, params GameObject[] cp)
    {
        if (cp.Length < 4)
        {
            Debug.LogError("ControlPoints is less than four");
        }
        Vector3 ret = Vector3.zero;
        Vector3 v0 = 0.5f * (cp[2].transform.localPosition - cp[0].transform.localPosition);
        Vector3 v1 = 0.5f * (cp[3].transform.localPosition - cp[1].transform.localPosition);

        Vector3 t3 = Mathf.Pow(t, 3) * (2.0f  * cp[0].transform.localPosition - 2.0f * cp[1].transform.localPosition + v0 + v1);
        Vector3 t2 = Mathf.Pow(t, 2) * (-3.0f * cp[0].transform.localPosition + 3.0f * cp[1].transform.localPosition - 2.0f * v0 - v1);
        Vector3 t1 = Mathf.Pow(t, 1) * v0;
        Vector3 t0 = cp[0].transform.localPosition;

        ret = t3 + t2 + t1 + t0;
        return ret;
    }

}
