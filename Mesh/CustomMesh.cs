using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMesh : MonoBehaviour
{
    // use polygon--triangle to explain
    Vector3 [] vertices = new Vector3 [10];
    // triangle have 3 vertices

    // Vector2 [] uv
    // 3 vertices cause 3 uv

    int [] triangles = new int [10];
    // triangle contains the index of each vertex thats make up the (shape) . by the way , the square value is 6 , because it has two triangles.

    public void MakeAMesh(){
        GetComponent<MeshFilter>().mesh = new Mesh
        {
            vertices = vertices,
            // mesh.uv = uv;
            triangles = triangles
        };

        Debug.Log(triangles.Length);

        // Triangle();

        // GetComponent<MeshFilter>().mesh = mesh;

        // GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Triangle(){
        Debug.Log("Make a Triangle!");
        vertices[0] = new Vector3(0 ,     0);
        vertices[1] = new Vector3(50 ,   100);
        vertices[2] = new Vector3(100 , 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        // vertices[3] = 
    }

    private void Rectangle(){
        // left quad
        // vertices[0] = new Vector3(0  , 0);
        // vertices[1] = new Vector3(0  , 100);
        // vertices[2] = new Vector3(100 , 100);
        // vertices[3] = new Vector3(100 , 0);

        // uv[0] = new Vector2(0 , 0);
        // uv[1] = new Vector2(0 , 1);
        // uv[2] = new Vector2(1 , 1);
        // uv[3] = new Vector2(1 , 0);

        // triangles[0] = 0;
        // triangles[1] = 1;
        // triangles[2] = 2;

        // triangles[3] = 0;
        // triangles[4] = 2;
        // triangles[5] = 3;

        // right quad
        // vertices[4] = new Vector3(100 , 0);
        // vertices[5] = new Vector3(100 , 100);
        // vertices[6] = new Vector3(200 , 100);
        // vertices[7] = new Vector3(200 , 0);

        // uv[4] = new Vector2(1 , 0);
        // uv[5] = new Vector2(1 , 1);
        // uv[6] = new Vector2(2 , 1);
        // uv[7] = new Vector2(2 , 0);

        // triangles[6] = 4;
        // triangles[7] = 5;
        // triangles[8] = 6;

        // triangles[9] = 4;
        // triangles[10] = 6;
        // triangles[11] = 7;
    }

    private void RectangleVer2(){
        vertices[0] = new Vector3(0 , 0 , 0);
        vertices[1] = new Vector3(0 , 0 , 100);
        vertices[2] = new Vector3(200 , 0 , 100);
        vertices[3] = new Vector3(200 , 0 , 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;
    }

    private void Pentagon(){
        // left tri
        vertices[0] = new Vector3(0 , 100 , 0);
        vertices[1] = new Vector3(-100 , 100 , 150);
        vertices[2] = new Vector3(50 , 100 , 300);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        // middle tri
        vertices[3] = new Vector3(100 , 100 , 0);

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        // right tri
        vertices[4] = new Vector3(200 , 100 , 150);

        triangles[6] = 3;
        triangles[7] = 2;
        triangles[8] = 4;
    }

    private void Pentagon2(){
        // left tri
        vertices[5] = new Vector3(0 , 0 , 0);
        vertices[6] = new Vector3(-100 , 0 , 150);
        vertices[7] = new Vector3(50 , 0 , 300);

        triangles[9] = 5;
        triangles[10] = 6;
        triangles[11] = 7;

        // middle tri
        vertices[8] = new Vector3(100 , 0 , 0);

        triangles[12] = 5;
        triangles[13] = 7;
        triangles[14] = 8;

        // right tri
        vertices[9] = new Vector3(200 , 0 , 150);

        triangles[15] = 8;
        triangles[16] = 7;
        triangles[17] = 9;
    }
}
