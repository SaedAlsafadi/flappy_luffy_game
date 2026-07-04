# Reward System - Quick Start Guide

## 📋 What Was Added

Three new features to enhance gameplay with rewards:

### 🏆 Cup Reward System
- **Silver Cup** appears at **10 pipes**
- **Gold Cup** appears at **20 pipes** (upgrades silver automatically)
- Displayed in top-right corner as a trophy

### 🛡️ Shield Reward System
- **Shield** appears at **10 coins**
- **Duration**: 5 seconds
- **Effect**: Bird survives collisions while active
- Countdown timer displayed during active period

### 🎮 Automatic Integration
- Hooks into existing scoring system automatically
- No gameplay interruption
- Works with both TextMeshPro and legacy UI

---

## 🚀 Quick Setup (5 Minutes)

### Step 1: Create Manager Object
1. Right-click in Hierarchy → **Create Empty**
2. Name it `RewardManager`
3. Click **Add Component** → Search `RewardSystem`

### Step 2: Create UI Elements
**For Cup:**
- Right-click Canvas → **UI → Image** → Name: `CupDisplay`
- Position: Top-right (350, 200)
- Size: 80x80

**For Shield:**
- Right-click Canvas → **UI → TextMeshPro - Text** → Name: `ShieldTimerDisplay`
- Position: Bottom-center (0, -100)
- Font Size: 40-50

### Step 3: Add Sprites
Create two simple sprites (or use existing):
- **Silver Cup**: Light gray square
- **Gold Cup**: Golden yellow square

### Step 4: Assign References
Select `RewardManager` and in Inspector:
- **Cup Display**: Drag the Image component
- **Silver Cup Sprite**: Drag silver sprite
- **Gold Cup Sprite**: Drag gold sprite
- **Shield Timer Display**: Drag the text component

### Step 5: Done!
Just play the game:
- Pass 10 pipes → See silver cup
- Pass 20 pipes → See gold cup
- Collect 10 coins → See shield timer
- Crash while shielded → Survive!

---

## ⚙️ Configuration Defaults

All settings use these defaults (perfect for evaluation):

| Reward | Trigger | Setting |
|--------|---------|---------|
| Silver Cup | 10 pipes | `silverCupPipesRequired = 10` |
| Gold Cup | 20 pipes | `goldCupPipesRequired = 20` |
| Shield | 10 coins | `shieldCoinsRequired = 10` |
| Shield Duration | - | `shieldDuration = 5` seconds |

---

## 📁 Files Modified/Created

### New Scripts
- `Assets/Scripts/RewardSystem.cs` - Main reward system logic

### Modified Scripts
- `Assets/Scripts/LogicScript.cs` - Added reward checks
- `Assets/Scripts/PlayerScript.cs` - Added shield protection

### Documentation
- `REWARD_SYSTEM_SETUP.md` - Complete setup guide
- `REWARD_SYSTEM_QUICK_START.md` - This file

---

## 🧪 Testing Checklist

Run through these quick tests:

1. **Cup Appears at 10 Pipes**
   - Play game
   - Pass 10 pipes
   - ✓ Silver cup visible in top-right

2. **Cup Upgrades at 20 Pipes**
   - Continue from above
   - Pass 10 more pipes (total 20)
   - ✓ Cup changes to gold color

3. **Shield Appears at 10 Coins**
   - Play game
   - Collect 10 coins
   - ✓ Shield timer visible (shows "Shield: 5.0s")

4. **Shield Protects from Collision**
   - With shield active (10 coins collected)
   - Deliberately hit a pipe
   - ✓ Bird survives (no game over)
   - ✓ Shield timer still counting down

5. **Shield Expires**
   - Wait 5 seconds with shield active
   - ✓ Shield timer disappears
   - Hit pipe
   - ✓ Normal game over occurs

---

## 🔍 Key Code Changes

### LogicScript.cs
```csharp
// In AddCoin():
if (rewardSystem != null)
    rewardSystem.CheckShieldReward(coinsCollected);

// In AddPipe():
if (rewardSystem != null)
    rewardSystem.CheckCupReward(pipesPassed);
```

### PlayerScript.cs
```csharp
// In OnCollisionEnter2D():
if (rewardSystem != null && rewardSystem.IsShieldActive())
{
    Debug.Log("Shield protected the bird!");
    return;  // Survive collision
}
```

---

## 💡 Troubleshooting Quick Fixes

| Issue | Solution |
|-------|----------|
| Cup not appearing | Ensure `Cup Display` assigned and sprites set |
| Shield not activating | Check `Shield Timer Display` assigned |
| Shield not working | Verify PlayerScript.cs modified with shield check |
| Console spam | Normal - shows debug info, can ignore |

---

## 📚 Full Documentation

For complete setup details, customization options, and advanced configuration:
→ See `REWARD_SYSTEM_SETUP.md`

---

## 🎯 Summary

The reward system is **automatic** once set up:
- ✅ LogicScript automatically triggers reward checks
- ✅ PlayerScript automatically checks shield protection
- ✅ RewardSystem manages all timers and displays
- ✅ No manual intervention needed during gameplay

**Total Setup Time**: ~5 minutes  
**Complexity**: Low (just assign UI elements and sprites)  
**Impact**: High (adds exciting gameplay mechanics)