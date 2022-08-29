using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;




[System.Serializable]
public class ListGroup
{
    [System.Serializable]
    public class Groupe
    {
        public Transform[] objects;



        public Vector3 GetCenter()
        {
            Vector3 sum = Vector3.zero;
                int nullObject = 0;
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                sum += objects[i].transform.position;
                else
                    nullObject++;
            }

            return sum / (objects.Length - nullObject);
        }
    }


    public Groupe[] groupes;

}


[RequireComponent (typeof(MeshFilter))]

[RequireComponent(typeof(MeshRenderer))]
public class MeshCreator : MonoBehaviour
{


    [SerializeField] private int resoultion = 5;
    [SerializeField] private float radius = 5;
    //  public Transform angleTerst;

    private float lastresolutuon;

    public float[] changeVertext;
    public ListGroup listGroup;


    public float angleTrasholdSelect = 30;
    [SerializeField] private float offsetPointTrashold = 1.5f;
    public BansheeGz.BGSpline.Curve.BGCurve bgCurve;


    private MeshFilter meshFilter;
    private Mesh mesh;


    private List<Vector3> vertexes;

    private List<Vector2> uv;
    private List<int> tris;


    private bool Updatevertext = false;


    void Start()
    {



        lastresolutuon = resoultion;
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();

        vertexes = new List<Vector3>();
        tris = new List<int>();
        uv = new List<Vector2>();




        changeVertext = Enumerable.Repeat<float>(1, resoultion + 1).ToArray();

        showGenerateMesh();



        bgCurve.Closed = true;

    }

    void showGenerateMesh()
    {

        if (lastresolutuon != resoultion)
        {
            lastresolutuon = resoultion;

            Updatevertext = true;
        }
        else
        {

        }


        if (Updatevertext)
        {
            Updatevertext = false;

            float[] vertexesCopy = Enumerable.Repeat<float>(1, resoultion + 1).ToArray();

            if (vertexesCopy.Length > changeVertext.Length)
            {
                for (int i = 0; i < vertexesCopy.Length; i++)
                {
                    if (i < changeVertext.Length)
                    {
                        vertexesCopy[i] = changeVertext[i];
                    }
                }


            }
            else if (vertexesCopy.Length < changeVertext.Length)
            {
                for (int i = 0; i < vertexesCopy.Length; i++)
                {
                    vertexesCopy[i] = changeVertext[i];
                }
            }


            changeVertext = vertexesCopy;
        }
       
        uv.Clear();
        vertexes.Clear();
        tris.Clear();

        mesh.Clear();


        vertexes.Add(transform.InverseTransformPoint(transform.position));

        for (int i = 0; i < resoultion; i++)
        {
            float interval = i / (float)resoultion;

            float radian = Mathf.PI * 2 * interval;


            float c = Mathf.Cos(radian);
            float s = Mathf.Sin(radian);

            Vector3 circlePoint = new Vector3(c, 0, s);

            circlePoint.Normalize();

            circlePoint *= (radius + changeVertext[i]); 
            //circlePoint *= changeVertext[i];
            vertexes.Add(transform.InverseTransformPoint(transform.position + circlePoint));
            // vertexes[i + 1] += chngeVertext[i + 1];
            //    uv.Add((new Vector2(c, s)).normalized);

        }









        for (int i = 1; i < vertexes.Count; i++)
        {
            tris.Add(0);


            int next = (i + 1) >= vertexes.Count ? 1 : i + 1;

            tris.Add(next);
            tris.Add(i);




        }






        //for (int i = 0; i < vertexes.Count; i++)
        //{
        //    Vector3 v = vertexes[i].normalized;
        //    uv.Add(new Vector2(v.x, v.z));

        //    // mesh.SetTriangles(tris, 0);

        //    //    mesh.SetUVs(0, uv);

        //    //mesh.RecalculateNormals();

        //    //meshFilter.mesh = mesh;


        //}




        // print(vertexes.Count + "  :::: " + uv.Count);

        mesh.SetVertices(vertexes);
        mesh.SetTriangles(tris, 0);
        mesh.SetUVs(0, uv);

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        meshFilter.sharedMesh = mesh;

    }

    void Update()
    {
        //    Debug.Log(direToAngle(transform.position, transform.position + angleTerst.transform.position));
        bgCurve.Clear();

        bgCurve.transform.position = transform.position;

       showGenerateMesh();
       // HandleVertextes();

        PointHnadler();
        for (int i = 1; i < mesh.vertices.Length; i++)
        {

            Vector3 pos = transform.TransformPoint(mesh.vertices[i]);

            BansheeGz.BGSpline.Curve.BGCurvePoint bgCurvePoint = new BansheeGz.BGSpline.Curve.BGCurvePoint(bgCurve, pos, true);

            bgCurve.AddPoint(bgCurvePoint);
        }
    
    }

    public void HandleVertextes()
    {

        Vector3[] vert = mesh.vertices;


        int[] tris = mesh.triangles;
        for (int i = 1; i < vert.Length ; i++)
        {
            
            float interval = i / (float)vert.Length;

            float radian = Mathf.PI * 2 * interval;


            float c = Mathf.Cos(radian);
            float s = Mathf.Sin(radian);

            Vector3 circlePoint = new Vector3(c, 0, s) * (radius + changeVertext[i]);
            //circlePoint *= changeVertext[i];

            vert[i] = (transform.position + circlePoint);

        
            // vertexes[i + 1] += chngeVertext[i + 1];
            //    uv.Add((new Vector2(c, s)).normalized);

        }

        mesh = new Mesh();

        mesh.SetVertices(vert);

        vertexes = vert.ToList();
        mesh.SetTriangles(tris,0);

        mesh.RecalculateNormals();

        meshFilter.sharedMesh = mesh;

    }
    public void PointHnadler()
    {

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
        outer:
            if (i > resoultion)
                break;
            float interval = i / (float)resoultion;

            float radian = Mathf.PI * 2 * interval;


            float c = Mathf.Cos(radian);
            float s = Mathf.Sin(radian);

            Vector3 circlePoint = new Vector3(c, 0, s) * radius;

            List<ListGroup.Groupe> validGroups = new List<ListGroup.Groupe>();
            // float angletoCircle = direToAngle(transform.position, transform.position + circlePoint);



            //find valid groups
            for (int j = 0; j < listGroup.groupes.Length; j++)
            {

                ListGroup.Groupe groupe = listGroup.groupes[j];

                Vector3 centerPos = groupe.GetCenter();
                centerPos.y = 0;


                Vector3 direGroup = centerPos - transform.position;
                direGroup.y = 0;

                float angletoGroup = Vector3.Angle(direGroup, circlePoint.normalized);

                if (Mathf.Abs(angletoGroup) < angleTrasholdSelect)
                {
                    validGroups.Add(groupe);
                }
                else
                {


                }
            }

            if (validGroups.Count <= 0)
            {
                changeVertext[i] = 1;
                i++;
                goto outer;
            }

            var clossestGroup = (from g in validGroups orderby Vector3.Distance(transform.position, g.GetCenter()) descending select g).FirstOrDefault();

         

            var clossestObjToGroup = (from o in clossestGroup.objects where o != null
                                      orderby Vector3.Distance(transform.position, o.position) descending select o).FirstOrDefault();
            

            if (clossestGroup != null && clossestObjToGroup != null)
                changeVertext[i] = Vector3.Distance(transform.position, clossestObjToGroup.position) / offsetPointTrashold;
            else
            {
                changeVertext[i] = 1;
            }
        }
    }

    private float direToAngle(Vector3 from, Vector3 to)
    {
        Vector3 dire = (to - from).normalized;

        //dire.y = 0;


        return Mathf.Atan2(dire.z, dire.x) * Mathf.Rad2Deg;
    }


    private Vector3 centerVert()
    {

        Vector3 s = Vector3.zero;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            s += mesh.vertices[i];
        }

        return transform.TransformPoint(s / mesh.vertices.Length);
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < resoultion; i++)
        {
            float interval = i / (float)resoultion;

            float radian = Mathf.PI * 2 * interval;


            float c = Mathf.Cos(radian);
            float s = Mathf.Sin(radian);

            Vector3 circlePoint = new Vector3(c, 0, s) * radius;

            Gizmos.DrawWireSphere(transform.position + circlePoint, 0.25f);

        }



       // Gizmos.DrawSphere(centerVert(),0.5f);
    }
}
