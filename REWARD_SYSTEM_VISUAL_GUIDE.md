# Reward System - Visual Setup Guide

## 🎨 Visual Layout

### Game Scene Hierarchy
```
Scene
├── Canvas
│   ├── Gameplay UI
│   │   ├── CoinsDisplay (Top-left)
│   │   │   └── TextMeshPro: "Coins: 5"
│   │   ├── PipesDisplay (Top-right)
│   │   │   └── TextMeshPro: "Pipes: 3"
│   │   ├── CupDisplay ⭐ NEW
│   │   │   └── Image: Silver/Gold Cup sprite
│   │   └── ShieldTimerDisplay ⭐ NEW
│   │       └── TextMeshPro: "Shield: 4.5s"
│   └── Game Over Screen
│
├── Bird (Player)
│
├── Pipes (Obstacles)
│
├── Coins
│
├── Logic ← Tag: "Logic"
│   └── LogicScript.cs (Modified)
│
└── RewardManager ⭐ NEW
    └── RewardSystem.cs (New)
```

---

## 📍 Screen Layout (In-Game View)

```
┌─────────────────────────────────────┐
│                                     │
│   🏆           GAME SCREEN          │ 🛡️
│  Silver         [Flying Bird]      Shield
│   Cup           [Pipes & Gaps]      5.0s
│                 [Coins]             
│   Coins: 5      [Obstacles]        Pipes: 3
│
│                                     │
│                                     │
└─────────────────────────────────────┘

Legend:
🏆  = Cup Display (top-right corner)
🛡️ = Shield Timer (top-right area)
```

---

## 🛠️ Setup Steps with Screenshots

### Step 1: Create RewardManager
```
Hierarchy Panel:
├── Right-click in empty area
│   └── Create Empty
│       └── Rename to "RewardManager"
│
Result:
├── RewardManager (empty GameObject)
```

### Step 2: Add RewardSystem Component
```
Inspector (RewardManager selected):
┌─────────────────────────────────┐
│ Add Component                   │
│ ┌─────────────────────────────┐ │
│ │ RewardSystem                │ │
│ └─────────────────────────────┘ │
└─────────────────────────────────┘

Result:
RewardManager shows RewardSystem component
```

### Step 3: Create Cup Display Image
```
Hierarchy:
├── Canvas
│   └── Right-click
│       └── UI > Image
│           └── Rename to "CupDisplay"
│
Inspector (CupDisplay):
┌─────────────────────────────────┐
│ RectTransform:                  │
│   Position: (350, 200, 0)       │
│   Size: (80, 80)                │
│   Anchor: Top-Right             │
├─────────────────────────────────┤
│ Image:                          │
│   Source Image: (empty)         │
│   Color: White                  │
└─────────────────────────────────┘

Visual Result:
┌─────────────────────────────┐
│  ┏━━━┓                       │
│  ┃   ┃ ← Empty Image (80x80) │
│  ┗━━━┛                       │
└─────────────────────────────┘
```

### Step 4: Create Shield Timer Display
```
Hierarchy:
├── Canvas
│   └── Right-click
│       └── UI > TextMeshPro - Text
│           └── Rename to "ShieldTimerDisplay"
│
Inspector (ShieldTimerDisplay):
┌─────────────────────────────────┐
│ RectTransform:                  │
│   Position: (0, -100, 0)        │
│   Size: (200, 80)               │
│   Anchor: Bottom-Center         │
├─────────────────────────────────┤
│ TextMeshPro:                    │
│   Text: (empty initially)       │
│   Font Size: 40-50              │
│   Alignment: Center             │
│   Color: Cyan/Light Blue        │
└─────────────────────────────────┘

Visual Result:
┌─────────────────────────────────┐
│                                 │
│                                 │
│           (game screen)         │
│                                 │
│        Shield: 5.0s             │
│   ← Shows when shield active    │
└─────────────────────────────────┘
```

### Step 5: Create Cup Sprites
```
Option A: Create Simple Sprites
┌──────────────────────────────────┐
│ Silver Cup Sprite                │
│                                  │
│   Light Gray (#C0C0C0)           │
│   ┌──────────┐                   │
│   │░░░░░░░░░░│  32x32 pixels     │
│   │░░░░░░░░░░│                   │
│   │░░░░░░░░░░│                   │
│   └──────────┘                   │
└──────────────────────────────────┘

┌──────────────────────────────────┐
│ Gold Cup Sprite                  │
│                                  │
│   Golden Yellow (#FFD700)        │
│   ┌──────────┐                   │
│   │████████████│  32x32 pixels   │
│   │████████████│                 │
│   │████████████│                 │
│   └──────────┘                   │
└──────────────────────────────────┘

Option B: Use Asset Sprites
- Import from Assets/Sprites/
- Or find custom cup sprites online
```

### Step 6: Assign Sprites to RewardSystem
```
Inspector (RewardManager with RewardSystem):
┌─────────────────────────────────────┐
│ RewardSystem (Script)               │
├─────────────────────────────────────┤
│ [Cup Reward Settings]               │
│ Silver Cup Pipes Required: 10       │
│ Gold Cup Pipes Required: 20         │
│                                     │
│ [Shield Reward Settings]            │
│ Shield Coins Required: 10           │
│ Shield Duration: 5                  │
│                                     │
│ [UI References]                     │
│ Cup Display: [CupDisplay Image]  ✓  │
│ Shield Timer Display: [Text]     ✓  │
│ Silver Cup Sprite: [Silver]      ✓  │
│ Gold Cup Sprite: [Gold]          ✓  │
└─────────────────────────────────────┘

Drag Actions:
├── Cup Display: Drag CupDisplay from Hierarchy
├── Shield Timer Display: Drag ShieldTimerDisplay
├── Silver Cup Sprite: Drag silver sprite from Assets
└── Gold Cup Sprite: Drag gold sprite from Assets
```

---

## 🔀 Data Flow Diagram

### Cup Progression Flow
```
Player Passes Pipe
        ↓
    MiddlePipeScript
        ↓
    LogicScript.AddPipe()
        ↓
    pipesPassed = X
        ↓
    RewardSystem.CheckCupReward(X)
        ↓
    ┌─────────────────────┐
    │  pipesPassed >= 10? │
    └─────────────────────┘
        ↓
      YES → ActivateSilverCup()
      │     ├── Show CupDisplay
      │     └── Set sprite to Silver
      │
      NO  → Continue
        ↓
    ┌─────────────────────┐
    │  pipesPassed >= 20? │
    └─────────────────────┘
        ↓
      YES → UpgradeToGoldCup()
      │     ├── Update CupDisplay
      │     └── Set sprite to Gold
      │
      NO  → Continue

Timeline:
Pipes: 0 ──→ 10 ──→ 15 ──→ 20 ──→ 25
      Cup: □    🥈    🥈    🥇    🥇
```

### Shield Activation Flow
```
Player Collects Coin
        ↓
    Coin.CollectCoin()
        ↓
    LogicScript.AddCoin()
        ↓
    coinsCollected = X
        ↓
    RewardSystem.CheckShieldReward(X)
        ↓
    ┌────────────────────────┐
    │ coinsCollected >= 10 ? │
    └────────────────────────┘
        ↓
      YES → ActivateShield()
      │     ├── Show ShieldTimerDisplay
      │     ├── shieldActive = true
      │     └── Timer: 5.0s → counting down
      │
      NO  → Continue

Timeline:
Coins: 0 ──→ 5 ──→ 10 ──→ 12 ──→ 15
Shield: □   □   🛡️  🛡️  🛡️

When Shield Active:
Player hits pipe → Bird survives (shield absorbs)
Shield timer continues: 5.0 → 4.5 → 4.0 → ... → 0
At 0 → Shield expires → Player normal again
```

---

## 🧪 Testing Flow Diagram

### Complete Test Path
```
Start Game
    ↓
Pass 1 pipe → pipesPassed = 1
    ↓
[Continue...]
    ↓
Pass 10th pipe → pipesPassed = 10
    ↓ ✓ TEST: Silver cup appears (top-right)
    ↓
Collect 1 coin → coinsCollected = 1
    ↓
[Continue...]
    ↓
Collect 10th coin → coinsCollected = 10
    ↓ ✓ TEST: Shield timer appears (shows "Shield: 5.0s")
    ↓
[Option A] Wait 5 seconds
    ↓
Shield expires (timer = 0)
    ↓ ✓ TEST: Shield timer disappears
    ↓
Hit pipe → Game over
    ↓ ✓ TEST: Normal game over occurs
    ↓
[Option B] Hit pipe immediately (shield active)
    ↓ ✓ TEST: Bird survives (no game over)
    ↓
Pass 20th pipe → pipesPassed = 20
    ↓ ✓ TEST: Silver cup changes to gold cup
    ↓
[End Test]
```

---

## 📋 Inspector Configuration Reference

### RewardManager Inspector View
```
┌────────────────────────────────────────────┐
│ RewardManager                              │
├────────────────────────────────────────────┤
│ Transform                                  │
├────────────────────────────────────────────┤
│ RewardSystem (Script)                      │
├────────────────────────────────────────────┤
│ ▼ Cup Reward Settings                      │
│  Silver Cup Pipes Required            10   │
│  Gold Cup Pipes Required              20   │
│                                             │
│ ▼ Shield Reward Settings                   │
│  Shield Coins Required                10   │
│  Shield Duration                      5    │
│                                             │
│ ▼ UI References                            │
│  Cup Display           [  CupDisplay  ]    │
│  Shield Timer Display  [ShieldTimer   ]    │
│  Shield Timer Display  [ShieldTimer   ]    │
│  Legacy                [  None/Legacy ]    │
│                                             │
│ ▼ Cup Sprites                              │
│  Silver Cup Sprite     [ Silver Icon   ]   │
│  Gold Cup Sprite       [ Gold Icon     ]   │
│                                             │
│ Tag: Untagged      Layer: Default          │
└────────────────────────────────────────────┘
```

---

## 🎮 UI Element Positions

### Default Layout
```
Screen Boundaries:
┌───────────────────────────────────┐
│                                   │
│  Top-Left:                 Top-Right:
│  Coins: X                  Pipes: X
│  
│                                   │  🏆 Cup
│                                   │  
│            [GAME PLAY]            │
│           [BIRD / PIPES]          │  🛡️ Shield Timer
│          [COINS / GAPS]           │  (when active)
│                                   │
│                                   │
│            Shield: X.Xs           │
│          (Bottom-Center)          │
│                                   │
└───────────────────────────────────┘

Coordinates (Unity Canvas):
- Coins Display: (-300, 200)
- Pipes Display: (300, 200)
- Cup Display: (350, 200)
- Shield Timer: (0, -100)
```

---

## ⚡ Quick Reference Matrix

### Status Indicators

| Event | Visual Indicator | Audio | Console |
|-------|------------------|-------|---------|
| Pass 10 pipes | Silver cup appears | (optional) | "Silver Cup Unlocked!" |
| Pass 20 pipes | Cup turns gold | (optional) | "Gold Cup Unlocked!" |
| Collect 10 coins | Shield timer appears | (optional) | "Shield Activated!" |
| Shield protecting | Timer counting down | None | "Shield protected bird!" |
| Shield expires | Timer disappears | (optional) | "Shield Deactivated!" |

---

## 🔧 Troubleshooting Visual Flowchart

```
Problem: Cup not appearing at 10 pipes
    ↓
┌──────────────────────────┐
│ Cup Display assigned?    │ ─ NO → Assign CupDisplay Image
└──────────────────────────┘
    ↓ YES
┌──────────────────────────┐
│ Sprites assigned?        │ ─ NO → Assign Silver/Gold sprites
└──────────────────────────┘
    ↓ YES
┌──────────────────────────┐
│ RewardSystem in scene?   │ ─ NO → Add RewardSystem to GameObject
└──────────────────────────┘
    ↓ YES
Check console for errors → Debug LogicScript.AddPipe() calls

────────────────────────────────────────────

Problem: Shield not protecting on collision
    ↓
┌──────────────────────────┐
│ Shield timer showing?    │ ─ NO → Check shield activation (10 coins)
└──────────────────────────┘
    ↓ YES
┌──────────────────────────┐
│ PlayerScript modified?   │ ─ NO → Apply PlayerScript changes
└──────────────────────────┘
    ↓ YES
Check console for IsShieldActive() calls
```

---

## 📊 State Diagram

### Cup State Machine
```
        ┌────────────────────────────────┐
        │     INITIAL STATE: NO CUP      │
        │       (currentCupLevel = 0)    │
        └────────────────────────────────┘
                      ↓
              [pipes >= 10?]
              
        ┌────────────────────────────────┐
        │    SILVER CUP STATE            │
        │    (currentCupLevel = 1)       │
        │   🥈 Showing Silver Sprite    │
        └────────────────────────────────┘
                      ↓
              [pipes >= 20?]
              
        ┌────────────────────────────────┐
        │     GOLD CUP STATE             │
        │     (currentCupLevel = 2)      │
        │    🥇 Showing Gold Sprite     │
        └────────────────────────────────┘
                      ↓
                   [END]
```

### Shield State Machine
```
        ┌────────────────────────────────┐
        │   INITIAL: NO SHIELD           │
        │   (shieldActive = false)       │
        │   (coinsCollected < 10)        │
        └────────────────────────────────┘
                      ↓
          [coins collected >= 10?]
          
        ┌────────────────────────────────┐
        │   SHIELD ACTIVE STATE          │
        │   (shieldActive = true)        │
        │   Timer: 5.0 → 0.0            │
        │   🛡️ Shows Shield Timer       │
        └────────────────────────────────┘
                      ↓
          [shieldTimeRemaining <= 0?]
          
        ┌────────────────────────────────┐
        │   SHIELD EXPIRED               │
        │   (shieldActive = false)       │
        │   (timer hidden)               │
        │   Back to INITIAL              │
        └────────────────────────────────┘
```

---

## 🎯 Quick Setup Checklist

```
□ Step 1: Create RewardManager GameObject
□ Step 2: Add RewardSystem component
□ Step 3: Create CupDisplay Image UI element
□ Step 4: Create ShieldTimerDisplay Text UI element
□ Step 5: Create Silver Cup sprite (or import)
□ Step 6: Create Gold Cup sprite (or import)
□ Step 7: Assign CupDisplay to RewardSystem
□ Step 8: Assign ShieldTimerDisplay to RewardSystem
□ Step 9: Assign Silver Cup Sprite to RewardSystem
□ Step 10: Assign Gold Cup Sprite to RewardSystem
□ Step 11: Verify all parameters (10/20/10/5)
□ Step 12: Play and test all scenarios
```

---

## ✅ Verification Checklist

After setup, verify:

```
Cup System:
✓ Silver cup appears at exactly 10 pipes
✓ Gold cup appears at exactly 20 pipes
✓ Cup stays visible once unlocked
✓ Cup sprite changes (silver → gold)
✓ Console shows unlock messages

Shield System:
✓ Shield timer appears at exactly 10 coins
✓ Timer shows "Shield: X.Xs" format
✓ Timer counts down in real-time
✓ Bird survives collision while shielded
✓ Bird dies after shield expires
✓ Shield timer disappears at 0 seconds
✓ Console shows activation/deactivation messages

Integration:
✓ No errors in console
✓ All UI elements visible
✓ Game performance unaffected
✓ All features work simultaneously
```

---

This visual guide provides:
- ✅ Complete scene hierarchy
- ✅ Screen layout reference
- ✅ Step-by-step setup with visuals
- ✅ Data flow diagrams
- ✅ State machines
- ✅ Troubleshooting flowchart
- ✅ Quick reference matrix
- ✅ Verification checklist

Use this alongside REWARD_SYSTEM_SETUP.md for complete implementation!