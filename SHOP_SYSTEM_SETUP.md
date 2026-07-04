# Shop System Setup Guide

## Overview
This guide walks you through setting up the character shop system in your Flappy Bird game.

## Scripts Created
1. **CharacterManager.cs** - Manages character selection and persistence
2. **ShopUIManager.cs** - Handles shop UI and character display
3. **Modified PlayerScript.cs** - Now loads selected character sprite
4. **Modified StartMenuScript.cs** - Added shop button integration

---

## Step 1: Prepare Character Sprites

1. **Create the folder structure:**
   - In your `Assets\Sprites\` folder, create a new folder called `Characters`
   - Path: `Assets\Sprites\Characters\`

2. **Place your character sprite images** (PNG format with transparent backgrounds):
   - Example: `Character1.png`, `Character2.png`, `Character3.png`, `Character4.png`
   - **Important:** All sprites should be the same size (e.g., 100x100 pixels)

3. **Configure each sprite in Unity Inspector:**
   - Select each sprite in the Project window
   - In Inspector, set:
     - **Texture Type:** Sprite (2D and UI)
     - **Sprite Mode:** Single
     - **Pixels Per Unit:** 100
     - Click **Apply**

---

## Step 2: Create CharacterManager GameObject

1. In your **Start Menu Scene**, right-click in the Hierarchy
2. Create an **Empty GameObject** and name it `CharacterManager`
3. Attach the **CharacterManager.cs** script to this object
4. In the Inspector, populate the **Available Characters** list:
   - Click the **+** button to add characters
   - For each character entry:
     - **Character Name:** (e.g., "Red Bird", "Blue Bird")
     - **Sprite:** Drag your sprite image here
     - **Character ID:** Unique number (0, 1, 2, 3, etc.)
   
   **Example setup for 4 characters:**
   ```
   Character 0: Red Bird (ID: 0)
   Character 1: Blue Bird (ID: 1)
   Character 2: Yellow Bird (ID: 2)
   Character 3: Green Bird (ID: 3)
   ```

---

## Step 3: Create Shop UI Panel

1. **Create the Shop Panel:**
   - In your Start Menu Canvas, right-click and select **UI > Panel**
   - Name it `ShopPanel`
   - Set its **Anchor Preset** to "Stretch" to fill the screen
   - Set color to a semi-transparent dark (e.g., RGBA: 0,0,0,200)

2. **Add Shop Title:**
   - Add a **TextMeshPro > Text** to ShopPanel
   - Text: "CHARACTER SHOP"
   - Position: Top center
   - Font size: 60

3. **Create Character Selection Area:**
   - Add a new **Panel** inside ShopPanel
   - Name it `CharacterGrid`
   - Position: Left side, showing character options
   - Add a **Grid Layout Group** component:
     - Preferred Columns: 2
     - Child Force Size: Width and Height (checked)

4. **Create Character Button Prefab:**
   - Inside CharacterGrid, create a **Button - TextMeshPro**
   - Name it `CharacterButtonPrefab`
   - Size: 150x150
   - Delete the TextMeshProUGUI child (we only need the button image)
   - This will be instantiated for each character

5. **Create Character Preview Area:**
   - Add a new **Panel** inside ShopPanel
   - Name it `PreviewPanel`
   - Position: Right side
   - Add a **LayoutElement** component to control size

6. **Add Preview Image:**
   - Inside PreviewPanel, add a **UI > Image**
   - Name it `CharacterPreview`
   - Make it large (e.g., 300x300)
   - Leave Sprite empty (ShopUIManager will set it)

7. **Add Character Name Text:**
   - Add a **TextMeshPro > Text** below the preview image
   - Name it `CharacterName`
   - Text: "Character Name"
   - Font size: 40

8. **Add Select Button:**
   - Add a **Button - TextMeshPro** to PreviewPanel
   - Name it `SelectButton`
   - Text: "SELECT"
   - Position: Below character name

9. **Add Close Button:**
   - Add a **Button - TextMeshPro** to ShopPanel (top-right corner)
   - Name it `CloseButton`
   - Text: "CLOSE" or "X"
   - Size: 100x50

---

## Step 4: Configure ShopUIManager

1. In your Start Menu, create a new **Empty GameObject** and name it `ShopUIManager`
2. Attach **ShopUIManager.cs** script to this object
3. In the Inspector, assign:
   - **Shop Panel:** Drag `ShopPanel` here
   - **Character Grid Container:** Drag `CharacterGrid` here
   - **Character Button Prefab:** Drag `CharacterButtonPrefab` here
   - **Character Preview Image:** Drag `CharacterPreview` here
   - **Character Name Text:** Drag `CharacterName` here
   - **Select Button:** Drag `SelectButton` here
   - **Close Button:** Drag `CloseButton` here

---

## Step 5: Add Shop Button to Start Menu

1. In your Start Menu Canvas, add a new **Button - TextMeshPro**
2. Name it `ShopButton`
3. Text: "SHOP"
4. Position: Between "Play" and "Settings" buttons

5. Configure the button click:
   - In the Inspector, find the **Button** component
   - Click the **+** under **On Click ()**
   - Drag the **StartMenuScript** GameObject here
   - From the dropdown, select **StartMenuScript > OpenShop()**

---

## Step 6: Update Start Menu Script Reference

1. Select the **GameObject with StartMenuScript** component
2. In the Inspector, find the **Shop UI Manager** field
3. Drag the **ShopUIManager** GameObject here

---

## Step 7: Verify Player Script Integration

The **PlayerScript.cs** has been automatically updated to:
- Load the selected character sprite at game start
- Apply it to the player's SpriteRenderer

No additional setup needed here!

---

## Testing Checklist

- [ ] CharacterManager GameObject created and configured with 3-4 characters
- [ ] Shop Panel UI created with all components
- [ ] ShopUIManager assigned all UI elements
- [ ] Shop button added to Start Menu and connected
- [ ] StartMenuScript has ShopUIManager reference assigned
- [ ] Each character sprite is properly imported as Sprite type
- [ ] Open Start Menu scene and click "SHOP" button
- [ ] Character selection buttons appear
- [ ] Character preview updates when clicking buttons
- [ ] "SELECT" button saves the choice
- [ ] Play game and verify correct character appears

---

## Troubleshooting

**Shop panel won't open:**
- Verify ShopUIManager is assigned in StartMenuScript
- Check that OpenShop() button is connected to StartMenuScript.OpenShop()

**Character buttons not showing:**
- Verify CharacterManager has characters in the list
- Check that CharacterGrid is assigned to ShopUIManager
- Ensure CharacterButtonPrefab is assigned

**Wrong character appears in game:**
- Verify CharacterManager is not destroyed between scenes (DontDestroyOnLoad is set)
- Check PlayerScript has the updated code that reads from CharacterManager

**Character sprites not visible:**
- Import sprites as "Sprite (2D and UI)" type
- Set Pixels Per Unit to 100

---

## Next Steps

Once the shop system is working, you can:
1. Add pricing and currency system
2. Add character unlock progression
3. Add character stats/abilities
4. Create character animations
5. Add character-specific sounds or effects