using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    public bool encontrado = false;

    void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);
        Vector3 viewAngleC = fow.DirFromAngle2(-fow.viewAngle / 2, false);
        Vector3 viewAngleD = fow.DirFromAngle2(fow.viewAngle / 2, false);
        Vector3 viewAngleE = fow.DirFromAngle3(-fow.viewAngle / 2, false);
        Vector3 viewAngleF = fow.DirFromAngle3(fow.viewAngle / 2, false);
        Vector3 viewAngleG = fow.DirFromAngle4(-fow.viewAngle / 2, false);
        Vector3 viewAngleH = fow.DirFromAngle4(fow.viewAngle / 2, false);
        Vector3 viewAngleI = fow.DirFromAngle5(-fow.viewAngle / 2, false);
        Vector3 viewAngleJ = fow.DirFromAngle5(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleC * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleD * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleE * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleF * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleG * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleH * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleI * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleJ * fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
            encontrado = true;

        }
        
    }

}
