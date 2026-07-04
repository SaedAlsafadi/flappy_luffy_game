# 🎮 Flappy Bird - Complete Setup Guide

## ✅ Changes Made

### **PlayerController.cs** - Updated to BirdScript Style
- ✅ Uses `Rigidbody2D.velocity` directly (simpler physics)
- ✅ Uses old Input System (`Input.GetKeyDown(KeyCode.Space)`)
- ✅ Uses `OnCollisionEnter2D` (not triggers)
- ✅ Public fields: `myRigidbody`, `flapStrength`, `logic`, `birdIsAlive`
- ✅ Auto-finds GameManager by "Logic" tag or by type

### **GameManager.cs** - Added Logic Tag Support
- ✅ Checks for "Logic" tag on Start
- ✅ Warns if tag is missing

---

## 🔧 Required Unity Setup

### **1. Create Tags**
Go to **Edit → Project Settings → Tags and Layers**, add these tags:
- ✅ `Player`
- ✅ `Obstacle`
- ✅ `Logic`
- ⚠️ `Ground` (optional, if you have ground)

---

### **2. Player GameObject Setup**

**GameObject Name:** `Player` (or "Bird")

**Components:**
1. **Transform** - Position at (0, 0, 0)
2. **Sprite Renderer** - Assign your bird sprite
3. **Rigidbody2D**
   - Body Type: `Dynamic`
   - Gravity Scale: `1` (or higher for faster falling)
   - Constraints: `Freeze Rotation Z` ✅
4. **Collider2D** (BoxCollider2D or CircleCollider2D)
   - ❌ **Is Trigger: UNCHECKED** (solid collision)
5. **PlayerController Script**
   - Flap Strength: `10` (adjust to taste)
   - My Rigidbody: Drag Rigidbody2D here (or leave empty for auto-find)
   - Logic: Drag GameManager here (or leave empty for auto-find)

**Tag:** `Player`

---

### **3. GameManager GameObject Setup**

**GameObject Name:** `GameManager` (or "Logic")

**Components:**
1. **Transform** - Position doesn't matter
2. **GameManager Script**
   - Player: Drag Player GameObject here (optional)
   - Spawner: Drag Spawner GameObject here (optional)

**Tag:** `Logic` ⚠️ **IMPORTANT!**

---

### **4. Obstacle Prefab Setup**

**Structure:**
```
Obstacle (Root)
├── ObstacleMovement.cs
├── TopPipe
│   ├── Tag: "Obstacle"
│   ├── Sprite Renderer
│   ├── BoxCollider2D
│   └── Is Trigger: ❌ UNCHECKED
├── BottomPipe
│   ├── Tag: "Obstacle"
│   ├── Sprite Renderer
│   ├── BoxCollider2D
│   └── Is Trigger: ❌ UNCHECKED
└── ScoreTrigger
    ├── ScoreTrigger.cs
    ├── BoxCollider2D
    └── Is Trigger: ✅ CHECKED
```

**Critical Settings:**
- **TopPipe & BottomPipe**: Colliders are **NOT triggers** (solid walls)
- **ScoreTrigger**: Collider **IS a trigger** (invisible scoring zone)
- **Tags**: TopPipe and BottomPipe tagged as `Obstacle`

---

### **5. Spawner GameObject Setup**

**GameObject Name:** `Spawner`

**Components:**
1. **Transform** - Position at spawn location (e.g., x=10, y=0)
2. **ObstacleSpawner Script**
   - Obstacle Prefab: Drag your obstacle prefab here
   - Spawn Interval: `2` seconds
   - Min Y: `-2`
   - Max Y: `2`

---

## 🎯 How It Works

### **Player Jump:**
1. Press **Space** → `Input.GetKeyDown(KeyCode.Space)` detects it
2. Sets `myRigidbody.velocity = Vector2.up * flapStrength`
3. Bird jumps upward with instant velocity

### **Collision Detection:**
1. Player hits pipe → `OnCollisionEnter2D` is called
2. Sets `birdIsAlive = false`
3. Calls `logic.GameOver()`
4. GameManager stops spawner and freezes obstacles

### **Scoring:**
1. Player passes through ScoreTrigger → `OnTriggerEnter2D` in ScoreTrigger.cs
2. Calls `GameManager.AddScore(1)`
3. Score increases by 1

---

## 🐛 Troubleshooting

### **Player Not Jumping:**
- ✅ Check Rigidbody2D is attached
- ✅ Check `flapStrength` is set (try 10-15)
- ✅ Check `birdIsAlive` is true in Inspector while playing

### **Player Passes Through Pipes:**
- ❌ Pipe colliders should **NOT be triggers**
- ✅ Player collider should **NOT be a trigger**
- ✅ Both need Rigidbody2D (player has it, pipes don't need it)

### **Game Doesn't End on Collision:**
- ✅ Check GameManager has "Logic" tag
- ✅ Check PlayerController found the GameManager (check Console for warnings)
- ✅ Check pipes are tagged as "Obstacle"

### **Score Not Increasing:**
- ✅ Check ScoreTrigger has BoxCollider2D with "Is Trigger" checked
- ✅ Check ScoreTrigger.cs is attached
- ✅ Check Player has Rigidbody2D (required for triggers)

---

## 🎨 Recommended Settings

### **Player:**
- Flap Strength: `10-15`
- Gravity Scale: `1-3`
- Collider: CircleCollider2D (more forgiving)

### **Spawner:**
- Spawn Interval: `2-3` seconds
- Min/Max Y: `-2` to `2` (adjust based on screen size)

### **Obstacles:**
- Move Speed: `2-5`
- Gap between pipes: `3-4` units

---

## 🚀 Quick Start Checklist

- [ ] Create tags: `Player`, `Obstacle`, `Logic`
- [ ] Tag Player GameObject as `Player`
- [ ] Tag GameManager GameObject as `Logic`
- [ ] Tag pipe GameObjects as `Obstacle`
- [ ] Player has Rigidbody2D with Gravity Scale > 0
- [ ] Player has Collider2D (NOT a trigger)
- [ ] Pipes have Collider2D (NOT triggers)
- [ ] ScoreTrigger has Collider2D (IS a trigger)
- [ ] PlayerController has `flapStrength` set
- [ ] ObstacleSpawner has prefab assigned

---

**You're all set! Press Play and enjoy your Flappy Bird game! 🐦**