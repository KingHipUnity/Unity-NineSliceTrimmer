---

# **📜 Nine-Slice Trimmer**  

A Unity Editor tool that trims the center slice of a **9-Slice Sprite** while preserving the outer edges. This reduces texture size while keeping the sprite's slicing functionality.  

---

## **📌 Features**  
✅ **Trim 9-Slice Sprites** – Removes the central portion while keeping the outer edges.  
✅ **Preview Before Saving** – Option to preview how the trimmed sprite will look before saving.  
✅ **Handles Vertical & Horizontal Slice-Only Cases** – Works even if only one axis is sliced.  
✅ **Easy & Modern UI** – Simple, clean, and efficient interface.  
✅ **Read/Write Check** – Ensures "Read/Write Enabled" is turned on before processing.  

---

## **🛠 Installation (UPM via Git)**  
You can install **Nine-Slice Trimmer** via **Unity Package Manager (UPM)** using the following steps:  

1. Open **Unity Editor**.  
2. Go to **Window → Package Manager**.  
3. Click the **+ (Add package from git URL)** button.  
4. Paste the following Git URL and click **Add**:  
   ```
   https://github.com/KingHipUnity/Unity-NineSliceTrimmer.git
   ```
5. Wait for Unity to download and install the package.  

---

## **🚀 How to Use**  
1. **Open the Tool**  
   - In Unity, go to **Tools → Nine-Slice Trimmer**.  

2. **Select a Sprite**  
   - Drag & drop a **9-Slice sprite** (with a border) into the tool.  

3. **Preview (Optional)**  
   - Click **👁 Preview** to see how the trimmed sprite will look.  

4. **Trim & Save**  
   - Click **✂ Trim & Save** to trim the sprite and replace the original asset.  
   - The tool updates the sprite’s slicing settings automatically.  

---

## **⚠️ Important Notes**  
- **Enable Read/Write:** The tool requires **"Read/Write Enabled"** in **Texture Import Settings**.  
- **Single Sprites Only:** This tool does **not** work with sprite atlases or multiple sprites in a single texture.  
- **Preserves Borders:** Works with **horizontal, vertical, or full 9-slice** configurations.  

---

## **📝 License**  
This tool is **free to use and modify** for personal or commercial projects. 🎨✨  

---

This version makes installation easier via UPM while keeping everything clear and concise! 🚀
