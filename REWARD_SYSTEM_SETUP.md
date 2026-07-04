# Reward System Setup Guide

## Overview
The reward system introduces two types of rewards that players can unlock by achieving milestones:

1. **Cup Reward System** - Visual trophy that upgrades as players pass more pipes
   - **Silver Cup**: Unlocked after passing **10 pipes**
   - **Gold Cup**: Unlocked after passing **20 pipes** (upgrades from Silver)

2. **Shield Reward System** - Temporary invincibility protection
   - **Shield**: Unlocked after collecting **10 coins**
   - **Duration**: 5 seconds of protection from crashes
   - **Effect**: Bird survives collisions while shield is active

---

## Part 1: Add Reward System Script

### Step 1: Attach RewardSystem Component
1. Create an empty GameObject in the scene
2. Name it `RewardManager`
3. Click **Add Component** → Search for `RewardSystem`
4. If script doesn't appear, ensure `RewardSystem.cs` is in `Assets/Scripts/`

---

## Part 2: Setup Cup Reward Display

### Step 1: Create Cup Display Image
1. Select **Canvas** in Hierarchy
2. Right-click → **UI** → **Image**
3. Name it `CupDisplay`
4. Configure:
   - **Position**: Top-right corner (e.g., Pos: 350, 200)
   - **Size**: Small (e.g., 80x80)
   - **Image**: Leave empty initially (will be set via RewardSystem)

### Step 2: Prepare Cup Sprites
You'll need two sprites:
- **Silver Cup Sprite** - Shows at 10 pipes
- **Gold Cup Sprite** - Shows at 20 pipes

To use existing sprites or create placeholders:
1. In Project panel, navigate to `Assets/Sprites/`
2. If you don't have cup sprites, you can:
   - Create simple colored squares (32x32):
     - Silver: Light gray (#C0C0C0)
     - Gold: Golden yellow (#FFD700)
   - Or import cup sprite assets if available

### Step 3: Assign Cup Sprites to RewardSystem
1. Select the object with **RewardSystem** component
2. In Inspector:
   - **Silver Cup Sprite**: Drag the silver cup sprite here
   - **Gold Cup Sprite**: Drag the gold cup sprite here
   - **Cup Display**: Drag the `CupDisplay` Image component here

### Step 4: Configure Cup Thresholds (Optional)
1. Select the object with **RewardSystem** component
2. In Inspector:
   - **Silver Cup Pipes Required**: 10 (default)
   - **Gold Cup Pipes Required**: 20 (default)
   - Adjust if desired, but **use 10 and 20 for evaluation**

---

## Part 3: Setup Shield Reward Display

### Step 1: Create Shield Timer Display (TextMeshPro)
1. Right-click on **Canvas** → **UI** → **TextMeshPro - Text**
2. Name it `ShieldTimerDisplay`
3. Configure:
   - **Text**: Leave empty (will be updated by RewardSystem)
   - **Font Size**: 40-50
   - **Position**: Top-center or bottom-center (e.g., Pos: 0, -100)
   - **Alignment**: Center aligned
   - **Color**: Light blue or cyan for visibility

### Step 2: Create Shield Timer Display (Legacy Text - Optional)
If you're using legacy Text components instead of TextMeshPro:
1. Right-click on **Canvas** → **UI** → **Text**
2. Name it `ShieldTimerDisplayLegacy`
3. Configure same as above

### Step 3: Assign Shield UI to RewardSystem
1. Select the object with **RewardSystem** component
2. In Inspector:
   - **Shield Timer Display**: Drag the TextMeshPro text here
   - **Shield Timer Display Legacy**: Drag the legacy text here (if using)

### Step 4: Configure Shield Settings (Optional)
1. Select the object with **RewardSystem** component
2. In Inspector:
   - **Shield Coins Required**: 10 (default - **use this for evaluation**)
   - **Shield Duration**: 5 (default, in seconds)
   - Adjust if desired

---

## Part 4: Integration with Existing Systems

### LogicScript Integration (AUTOMATIC)
The reward system is automatically integrated with LogicScript.cs:
- When `AddCoin()` is called → RewardSystem checks for shield reward
- When `AddPipe()` is called → RewardSystem checks for cup reward
- **No additional setup required** - just ensure RewardSystem is in the scene

### PlayerScript Integration (AUTOMATIC)
The shield protection is automatically integrated with PlayerScript.cs:
- When collision occurs → checks if shield is active
- If shield is active → bird survives the collision
- If shield is inactive → normal game over
- **No additional setup required** - just ensure RewardSystem is in the scene

---

## Part 5: Testing the Reward System

### Test Cup Reward (Silver)
1. Start the game
2. Pass through pipes until you've passed **10 pipes**
3. **Expected**: Silver cup sprite appears in top-right corner
4. **Log**: Console shows "Silver Cup Unlocked! Passed 10 pipes!"

### Test Cup Reward (Gold)
1. Continue playing after unlocking silver cup
2. Pass through pipes until you've passed **20 pipes** total
3. **Expected**: Silver cup sprite changes to gold cup sprite
4. **Log**: Console shows "Gold Cup Unlocked! Passed 20 pipes!"

### Test Shield Reward
1. Start the game
2. Collect coins until you've collected **10 coins**
3. **Expected**: Shield timer appears showing "Shield: 5.0s"
4. **Log**: Console shows "Shield Activated for 5 seconds!"
5. Immediately collide with a pipe during this time
6. **Expected**: Bird survives the collision (no game over)
7. **Log**: Console shows "Shield protected the bird! Remaining time: X.Xs"

### Test Shield Expiration
1. After activating shield, wait 5 seconds without colliding
2. **Expected**: Shield timer disappears
3. **Log**: Console shows "Shield Deactivated!"
4. Collide with pipe after shield expires
5. **Expected**: Normal game over occurs

---

## Part 6: Configuration Reference

### Configurable Values in RewardSystem Inspector

| Setting | Default | Purpose | For Evaluation |
|---------|---------|---------|-----------------|
| Silver Cup Pipes Required | 10 | Pipes needed for silver cup | **Keep at 10** |
| Gold Cup Pipes Required | 20 | Pipes needed for gold cup | **Keep at 20** |
| Shield Coins Required | 10 | Coins needed for shield | **Keep at 10** |
| Shield Duration | 5 | Seconds of shield protection | 5 seconds recommended |

---

## Part 7: Customization Guide

### Change Cup Thresholds
1. Select object with **RewardSystem**
2. Modify:
   - `silverCupPipesRequired` (e.g., 5 for easier unlock)
   - `goldCupPipesRequired` (e.g., 15 for earlier gold upgrade)

### Change Shield Duration
1. Select object with **RewardSystem**
2. Modify `shieldDuration` (in seconds, e.g., 10 for longer protection)

### Change Shield Coin Requirement
1. Select object with **RewardSystem**
2. Modify `shieldCoinsRequired` (e.g., 5 for more frequent shields)

### Custom Cup Sprites
1. Import your own cup sprites to `Assets/Sprites/`
2. In RewardSystem Inspector:
   - Drag silver sprite to `Silver Cup Sprite`
   - Drag gold sprite to `Gold Cup Sprite`

---

## Part 8: Troubleshooting

### Cup not appearing at 10 pipes
- **Check**: Is `Silver Cup Sprite` assigned in RewardSystem Inspector?
- **Check**: Is `Cup Display` Image component assigned?
- **Check**: Is RewardSystem script in the scene?
- **Solution**: Ensure all three are properly configured

### Cup not upgrading to gold at 20 pipes
- **Check**: Is `Gold Cup Sprite` assigned in RewardSystem Inspector?
- **Check**: Silver cup already unlocked?
- **Solution**: Gold cup only appears if silver cup is active first

### Shield not appearing when collecting coins
- **Check**: Is `Shield Timer Display` assigned in RewardSystem Inspector?
- **Check**: Have you collected **exactly 10 coins**?
- **Solution**: Check coin collection is working (see SCORE_SYSTEM_SETUP.md)

### Shield not protecting from collisions
- **Check**: Is shield timer displaying when you collide?
- **Check**: Is RewardSystem.cs attached to a GameObject in the scene?
- **Check**: Is PlayerScript.cs updated (modifications are already in place)?
- **Solution**: Run in console: `Debug.Log(rewardSystem.IsShieldActive())` to verify

### Shield timer not counting down
- **Check**: Is `Shield Timer Display` assigned in RewardSystem Inspector?
- **Check**: Are there console errors?
- **Solution**: Make sure TextMeshPro is imported in your project

### No console messages
- **Solution**: Open Console (Ctrl+Shift+C or Window → General → Console)
- Check if "Shield Activated", "Silver Cup Unlocked", etc. messages appear
- If not, RewardSystem may not be finding required components

---

## Part 9: How It Works (Technical Details)

### Cup Reward Flow
1. Player passes pipe → `LogicScript.AddPipe()` called
2. LogicScript calls `rewardSystem.CheckCupReward(pipesPassed)`
3. RewardSystem checks milestones:
   - If `pipesPassed >= 10` AND cup level is 0 → Activate silver cup
   - If `pipesPassed >= 20` AND cup level is 1 → Upgrade to gold cup
4. Cup display image is updated with appropriate sprite

### Shield Reward Flow
1. Player collects coin → `LogicScript.AddCoin()` called
2. LogicScript calls `rewardSystem.CheckShieldReward(coinsCollected)`
3. RewardSystem checks:
   - If `coinsCollected >= 10` AND shield not active → Activate shield
4. Shield timer starts counting down from duration (5 seconds)
5. Shield timer display shows remaining time
6. When timer reaches 0 → Shield deactivates

### Shield Protection Flow
1. Player collides with obstacle → `PlayerScript.OnCollisionEnter2D()` called
2. PlayerScript calls `rewardSystem.IsShieldActive()`
3. If shield active:
   - Log "Shield protected the bird!"
   - Return without calling `logic.gameOver()` (bird survives)
4. If shield inactive:
   - Normal game over sequence

---

## Part 10: Methods Reference

### RewardSystem Public Methods

```csharp
// Called by LogicScript when pipes are passed
public void CheckCupReward(int pipesPassed)

// Called by LogicScript when coins are collected
public void CheckShieldReward(int coinsCollected)

// Called by PlayerScript on collision
public bool IsShieldActive()

// Returns current cup level (0=none, 1=silver, 2=gold)
public int GetCupLevel()

// Returns remaining shield time in seconds
public float GetShieldTimeRemaining()
```

---

## Checklist for Complete Setup

- [ ] RewardSystem.cs script exists in Assets/Scripts/
- [ ] RewardSystem component added to a GameObject in scene
- [ ] Cup Display Image created on Canvas
- [ ] Silver Cup Sprite assigned in RewardSystem
- [ ] Gold Cup Sprite assigned in RewardSystem
- [ ] Cup Display assigned to RewardSystem
- [ ] Shield Timer Display created on Canvas
- [ ] Shield Timer Display assigned to RewardSystem
- [ ] Silver Cup Pipes Required set to 10
- [ ] Gold Cup Pipes Required set to 20
- [ ] Shield Coins Required set to 10
- [ ] Shield Duration set to 5
- [ ] LogicScript.cs updated with reward system integration
- [ ] PlayerScript.cs updated with shield protection logic
- [ ] Test cup at 10 pipes ✓
- [ ] Test cup upgrade at 20 pipes ✓
- [ ] Test shield at 10 coins ✓
- [ ] Test shield protection on collision ✓

---

## Complete Features Summary

✅ **Cup Reward System**
- Unlocks at configurable pipe milestones
- Auto-upgrades from silver to gold
- Visual indicator in top-right corner
- Automatic sprite switching

✅ **Shield Reward System**
- Unlocks at configurable coin threshold
- Provides temporary invincibility
- Displays countdown timer during active period
- Automatic deactivation after duration expires

✅ **Seamless Integration**
- Automatically hooks into scoring system
- No gameplay interruption
- Console logging for debugging
- Full compatibility with existing systems