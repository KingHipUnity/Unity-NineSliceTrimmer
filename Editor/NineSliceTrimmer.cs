using UnityEngine;
using UnityEditor;
using System.IO;

public class NineSliceTrimmer : EditorWindow {
  private Texture2D originalTexture;
  private Texture2D previewTexture;
  private string assetPath;
  private Vector4 border;

  [MenuItem("Tools/Nine-Slice Trimmer")]
  static void Init() {
    NineSliceTrimmer window = (NineSliceTrimmer) EditorWindow.GetWindow(typeof(NineSliceTrimmer));
    window.titleContent = new GUIContent("9-Slice Trimmer");
    window.minSize = new Vector2(350, 250);
    window.Show();
  }

  void OnGUI() {
    GUILayout.Space(10);
    GUILayout.Label("🖼 Trim 9-Slice Center", EditorStyles.boldLabel);

    EditorGUILayout.HelpBox("Select a sprite texture with a valid 9-slice border. You can preview before saving.", MessageType.Info);

    originalTexture = (Texture2D) EditorGUILayout.ObjectField("Sprite Texture", originalTexture, typeof(Texture2D), false);

    GUILayout.Space(10);

    EditorGUILayout.BeginHorizontal();
    GUI.enabled = originalTexture != null;

    if (GUILayout.Button("👁 Preview", GUILayout.Height(40))) {
      GeneratePreview();
    }

    if (GUILayout.Button("✂ Trim & Save", GUILayout.Height(40))) {
      TrimAndSave();
    }

    GUI.enabled = true;
    EditorGUILayout.EndHorizontal();

    if (previewTexture != null) {
      GUILayout.Space(10);
      GUILayout.Label("Preview:", EditorStyles.boldLabel);
      GUILayout.Label(new GUIContent(previewTexture), GUILayout.Width(200), GUILayout.Height(200));
    }
  }

  void GeneratePreview() {
    if (!ValidateTexture()) return;

    previewTexture = RemoveCenterPart(originalTexture, border);
  }

  void TrimAndSave() {
    if (!ValidateTexture()) return;

    Texture2D trimmedTexture = RemoveCenterPart(originalTexture, border);

    File.WriteAllBytes(assetPath, trimmedTexture.EncodeToPNG());
    AssetDatabase.ImportAsset(assetPath);

    ApplySpriteImportSettings(assetPath, border);

    EditorUtility.DisplayDialog("Success", "Sprite trimmed and saved successfully!", "OK");
  }

  bool ValidateTexture() {
    if (originalTexture == null) return false;

    assetPath = AssetDatabase.GetAssetPath(originalTexture);
    TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

    if (importer == null || importer.spriteImportMode != SpriteImportMode.Single) {
      EditorUtility.DisplayDialog("Error", "Selected texture is not a valid single sprite!", "OK");
      return false;
    }

    if (!importer.isReadable) {
      EditorUtility.DisplayDialog("Error", "The selected sprite does not have 'Read/Write Enabled'.\n\nPlease enable 'Read/Write Enabled' in the Texture Import Settings and reimport the texture.", "OK");
      return false;
    }

    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
    border = sprite.border;

    return true;
  }

  Texture2D RemoveCenterPart(Texture2D texture, Vector4 border) {
    int width = texture.width;
    int height = texture.height;

    int left = (int) border.x;
    int right = (int) border.z;
    int top = (int) border.w;
    int bottom = (int) border.y;

    int newWidth = (left + right > 0) ? left + right : width;
    int newHeight = (top + bottom > 0) ? top + bottom : height;

    Texture2D newTexture = new Texture2D(newWidth, newHeight);

    if (left + right > 0 && top + bottom > 0) {
      CopyRegion(texture, newTexture, 0, height - top, left, top, 0, newHeight - top);
      CopyRegion(texture, newTexture, width - right, height - top, right, top, left, newHeight - top);
      CopyRegion(texture, newTexture, 0, 0, left, bottom, 0, 0);
      CopyRegion(texture, newTexture, width - right, 0, right, bottom, left, 0);
    } else if (left + right > 0) {
      CopyRegion(texture, newTexture, 0, 0, left, height, 0, 0);
      CopyRegion(texture, newTexture, width - right, 0, right, height, left, 0);
    } else if (top + bottom > 0) {
      CopyRegion(texture, newTexture, 0, height - top, width, top, 0, newHeight - top);
      CopyRegion(texture, newTexture, 0, 0, width, bottom, 0, 0);
    }

    newTexture.Apply();
    return newTexture;
  }

  void CopyRegion(Texture2D source, Texture2D dest, int srcX, int srcY, int width, int height, int destX, int destY) {
    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        dest.SetPixel(destX + x, destY + y, source.GetPixel(srcX + x, srcY + y));
      }
    }
  }

  void ApplySpriteImportSettings(string path, Vector4 border) {
    TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
    if (importer == null) return;

    importer.textureType = TextureImporterType.Sprite;
    importer.spriteImportMode = SpriteImportMode.Single;
    importer.spriteBorder = border;
    importer.SaveAndReimport();
  }
}
