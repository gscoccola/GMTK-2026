using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RuleTileDuplicator : EditorWindow
{
    private Object originalRuleTile;
    private Texture2D newSpriteSheet;

    [MenuItem("Tools/Duplicate Rule Tile with New Sheet")]
    public static void ShowWindow()
    {
        GetWindow<RuleTileDuplicator>("Rule Tile Duplicator");
    }

    private void OnGUI()
    {
        originalRuleTile = EditorGUILayout.ObjectField("Original Rule Tile", originalRuleTile, typeof(Object), false);
        newSpriteSheet = (Texture2D)EditorGUILayout.ObjectField("New Sprite Sheet Texture", newSpriteSheet, typeof(Texture2D), false);

        if (GUILayout.Button("Duplicate and Remap"))
        {
            DuplicateTile();
        }
    }

    private void DuplicateTile()
    {
        if (originalRuleTile == null || newSpriteSheet == null)
        {
            Debug.LogError("Please assign both the original Rule Tile and the new Spritesheet.");
            return;
        }

        // 1. Load all sprites from the NEW spritesheet and map them by their Rect position
        string newSheetPath = AssetDatabase.GetAssetPath(newSpriteSheet);
        Object[] newAssets = AssetDatabase.LoadAllAssetsAtPath(newSheetPath);
        Dictionary<Vector2, Sprite> newSpriteMap = new Dictionary<Vector2, Sprite>();

        foreach (var asset in newAssets)
        {
            if (asset is Sprite newSprite)
            {
                // Key is the Min X and Min Y pixel coordinate of the sprite on its sheet
                Vector2 posKey = new Vector2(newSprite.rect.xMin, newSprite.rect.yMin);
                if (!newSpriteMap.ContainsKey(posKey))
                {
                    newSpriteMap.Add(posKey, newSprite);
                }
            }
        }

        // 2. Duplicate the original rule tile asset file
        string originalPath = AssetDatabase.GetAssetPath(originalRuleTile);
        string newPath = originalPath.Replace(".asset", "_Copied.asset");
        AssetDatabase.CopyAsset(originalPath, newPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // 3. Open the cloned tile via SerializedObject to safely swap the references
        Object clonedTile = AssetDatabase.LoadAssetAtPath<Object>(newPath);
        SerializedObject so = new SerializedObject(clonedTile);
        so.Update();

        // Remap Default Sprite
        SerializedProperty m_DefaultSprite = so.FindProperty("m_DefaultSprite");
        if (m_DefaultSprite != null && m_DefaultSprite.objectReferenceValue != null)
        {
            m_DefaultSprite.objectReferenceValue = FindMatchingSpriteByPosition(m_DefaultSprite.objectReferenceValue as Sprite, newSpriteMap);
        }

        // Remap Tiling Rules Sprites
        SerializedProperty m_TilingRules = so.FindProperty("m_TilingRules");
        if (m_TilingRules != null)
        {
            for (int i = 0; i < m_TilingRules.arraySize; i++)
            {
                SerializedProperty rule = m_TilingRules.GetArrayElementAtIndex(i);
                SerializedProperty m_Sprites = rule.FindPropertyRelative("m_Sprites");
                if (m_Sprites != null)
                {
                    for (int j = 0; j < m_Sprites.arraySize; j++)
                    {
                        SerializedProperty spriteProp = m_Sprites.GetArrayElementAtIndex(j);
                        if (spriteProp.objectReferenceValue != null)
                        {
                            spriteProp.objectReferenceValue = FindMatchingSpriteByPosition(spriteProp.objectReferenceValue as Sprite, newSpriteMap);
                        }
                    }
                }
            }
        }

        so.ApplyModifiedProperties();
        AssetDatabase.SaveAssets();
        Debug.Log("<color=green>Success!</color> Remapped Rule Tile saved at: " + newPath);
    }

    private Sprite FindMatchingSpriteByPosition(Sprite oldSprite, Dictionary<Vector2, Sprite> newSpriteMap)
    {
        if (oldSprite == null) return null;

        // Find what pixel coordinates the old sprite used on its original layout
        Vector2 oldPosKey = new Vector2(oldSprite.rect.xMin, oldSprite.rect.yMin);

        // Retrieve the new sprite that shares those exact same pixel coordinates
        if (newSpriteMap.TryGetValue(oldPosKey, out Sprite matchingSprite))
        {
            return matchingSprite;
        }

        Debug.LogWarning($"Could not find a matching grid position for sprite {oldSprite.name} at coordinates {oldPosKey}. This tile rule mapping was skipped.");
        return null;
    }
}