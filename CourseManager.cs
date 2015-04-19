using UnityEngine;
using System.Collections;

public class CourseManager : MonoBehaviour {

    public GameObject[] controlPoint;
    public GameObject meshPrefab;
    CatmullRom catmullRom;
    public float catmullDelta;
    public float width;
	// Use this for initialization
	void Start () {
        catmullRom = GetComponent<CatmullRom>();
        //controlPoint = GameObject.FindGameObjectsWithTag("ControlPoint");
        
        MakeMesh();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void MakeMesh()
    {
        int j = 0;
        Vector3 posp = Vector3.forward;
        Quaternion rot1, rot2;
        rot1 = Quaternion.identity;
        rot2 = Quaternion.identity;
        Quaternion rotdiff = Quaternion.identity;
        for (int i = 0; i < controlPoint.Length - 1; i++, j++)
        {
            float delta = 0.0f;
            int index1 = i;
            int index2 = i + 1;
            int index3 = i + 2;
            int index4 = i + 3;

            if (index3 >= controlPoint.Length)
            {
                index3 = controlPoint.Length - 1;
                index4 = controlPoint.Length - 1;
            }
            if (index4 >= controlPoint.Length)
            {
                index4 = controlPoint.Length - 1;
            }

            Vector3 p = catmullRom.Getcatmullrom(delta, controlPoint[index1], controlPoint[index2], controlPoint[index3], controlPoint[index4]);
            Mesh mesh = new Mesh();
            Debug.Log(6 * (int)(1.0f / catmullDelta) + 6 + 6);
            Vector3[] vertices = new Vector3[6 * (int)(1.0f / catmullDelta) + 6 + 6];
            int index = 0;
            int[] tris = new int[vertices.Length];
            for (int k = 0; k < tris.Length; k++)
            {
                tris[k] = k;
            }
            while (delta <= 1.0f)
            {

                Vector3 pos = catmullRom.Getcatmullrom(delta, controlPoint[index1], controlPoint[index2], controlPoint[index3], controlPoint[index4]);
                rot2 = Quaternion.FromToRotation(Vector3.forward, posp);
                rot2 = Quaternion.identity;
                vertices[index] =     p + rot1 * (Vector3.right * 0.5f * width);
                vertices[index + 1] = p + rot1 * (Vector3.right * -0.5f * width);
                vertices[index + 2] = pos + rot2 * (Vector3.right * -0.5f * width);

                vertices[index + 3] = p + rot1 * (Vector3.right * 0.5f * width);
                vertices[index + 4] = pos + rot2 * (Vector3.right * -0.5f * width);
                vertices[index + 5] = pos + rot2 * (Vector3.right * 0.5f * width);
               
                rot1 = rot2;
                if (delta >= 1.0f)
                {
                    break;
                }
                delta += catmullDelta;
                j++;
                index += 6;
                posp = pos - p;
                p = pos;
                if (delta > 1.0f)
                {
                    delta = 1.0f;
                }
            }
           
            mesh.vertices = vertices;
            mesh.triangles = tris;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            var mp = Instantiate(meshPrefab);
            var meshFilter = mp.GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;
        }
    }
}
