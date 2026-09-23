using _01_Scripts.Level;
using UnityEditor;
using UnityEngine;

namespace _01_Scripts.Editor
{
    [CustomEditor(typeof(MapPartManager))]
    public class AutoVariable : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = (MapPartManager)target;

            if (GUILayout.Button("Spawn Point 자동 채우기"))
            {
                Undo.RecordObject(script, "Auto Fill Spawn Points");

                SerializedObject serializedObject = new SerializedObject(script);
                serializedObject.Update();

                // MapPartSpawnPoints 채우기
                var mapGroup = script.MapPartSpawnPointGroup;
                var mapListProperty = serializedObject.FindProperty("_mapPartSpawnPoints");
                if (mapGroup != null && mapListProperty != null)
                {
                    mapListProperty.ClearArray();
                    for (int i = 0; i < mapGroup.childCount; i++)
                    {
                        mapListProperty.InsertArrayElementAtIndex(i);
                        mapListProperty.GetArrayElementAtIndex(i).objectReferenceValue = mapGroup.GetChild(i);
                    }
                }

                // ObjectSpawnPoints 채우기
                var objGroup = script.ObjectSpawnPointGroup;
                var objListProperty = serializedObject.FindProperty("_objectSpawnPoints");
                if (objGroup != null && objListProperty != null)
                {
                    objListProperty.ClearArray();
                    for (int i = 0; i < objGroup.childCount; i++)
                    {
                        objListProperty.InsertArrayElementAtIndex(i);
                        objListProperty.GetArrayElementAtIndex(i).objectReferenceValue = objGroup.GetChild(i);
                    }
                }
                
                // entitySpawnPoints 채우기
                var entityGroup = script.EntitySpawnPointGroup;
                var entityListProperty = serializedObject.FindProperty("_entitySpawnPoints");
                if (entityGroup != null && entityListProperty != null)
                {
                    entityListProperty.ClearArray();
                    for (int i = 0; i < entityGroup.childCount; i++)
                    {
                        entityListProperty.InsertArrayElementAtIndex(i);
                        entityListProperty.GetArrayElementAtIndex(i).objectReferenceValue = entityGroup.GetChild(i);
                    }
                }
                
                // ItemSpawnPoints 채우기
                var ItemGroup = script.ItemSpawnPointGroup;
                var ItemListProperty = serializedObject.FindProperty("_itemSpawnPoints");
                if (ItemGroup != null && ItemListProperty != null)
                {
                    ItemListProperty.ClearArray();
                    for (int i = 0; i < ItemGroup.childCount; i++)
                    {
                        ItemListProperty.InsertArrayElementAtIndex(i);
                        ItemListProperty.GetArrayElementAtIndex(i).objectReferenceValue = ItemGroup.GetChild(i);
                    }
                }

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(script);
                PrefabUtility.RecordPrefabInstancePropertyModifications(script);
            }
        }
    }
}