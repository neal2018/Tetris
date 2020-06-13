using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


[CustomEditor(typeof(dmBlockBase))]
public class dmBlockBaseEditor : Editor
{
    dmBlockBase targetBlock
    {
        get { return target as dmBlockBase; }
    }
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        Vector2 tempSize = targetBlock.blockSize;
        targetBlock.blockSize = EditorGUILayout.Vector2Field("Block Size:", targetBlock.blockSize);

        if (tempSize != targetBlock.blockSize) //当方块尺寸变更时,删除超出方块尺寸范围的数据
        {
            // TODO: make this more efficient
            List<Vector2> tempList = new List<Vector2>();
            tempList.AddRange(targetBlock.squareCoordList);
            foreach (Vector2 vec in tempList)
            {
                if (vec.x >= targetBlock.blockSize.x || vec.y >= targetBlock.blockSize.y)
                {
                    targetBlock.squareCoordList.Remove(vec);
                }
            }
        }

        GUILayout.Space(5);

        EditorGUILayout.LabelField("Block Shape:");
        for (int y = 0; y < targetBlock.blockSize.y; ++y)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < targetBlock.blockSize.x; ++x)
            {
                Vector2 nVec = new Vector2(x, y);
                GUI.backgroundColor = targetBlock.squareCoordList.Contains(nVec) ? Color.green : Color.gray;
                if (GUILayout.Button("", GUILayout.Width(EditorGUIUtility.singleLineHeight), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                {
                    if (targetBlock.squareCoordList.Contains(nVec))
                    {
                        targetBlock.squareCoordList.Remove(nVec);
                    }
                    else
                    {
                        targetBlock.squareCoordList.Add(nVec);
                    }
                    EditorUtility.SetDirty(target);
                }
                GUILayout.Space(2);
            }
            EditorGUILayout.EndHorizontal();
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }
}