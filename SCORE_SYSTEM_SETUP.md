# Scoring System Setup Guide

## Overview
The new scoring system tracks two separate metrics:
- **Coins Collected** - Displayed in top-left
- **Pipes Passed** - Displayed in top-right
- **Timer** (optional) - Shows countdown when in time-based mode

The game can end in two ways:
1. **Time-Based**: Player survives for 60 seconds (or custom time)
2. **Score-Based**: Player passes 20 pipes (or custom score)

---

## Part 1: Setup Game UI During Gameplay

### Step 1: Create Canvas
1. Right-click in Hierarchy → UI → Canvas
2. Name it `GameplayCanvas`
3. Set it as a sibling to other game elements

### Step 2: Create Coins Display (Top-Left)
1. Right-click on Canvas → UI → TextMeshPro - Text
2. Name it `CoinsDisplay`
3. Configure:
   - **Text**: "Coins: 0"
   - **Font Size**: 50-60
   - **Position**: Top-left corner (e.g., Pos: -300, 200)
   - **Alignment**: Left aligned

### Step 3: Create Pipes Display (Top-Right)
1. Right-click on Canvas → UI → TextMeshPro - Text
2. Name it `PipesDisplay`
3. Configure:
   - **Text**: "Pipes: 0"
   - **Font Size**: 50-60
   - **Position**: Top-right corner (e.g., Pos: 300, 200)
   - **Alignment**: Right aligned

### Step 4: Create Timer Display (Top-Center, optional)
1. Right-click on Canvas → UI → TextMeshPro - Text
2. Name it `TimerDisplay`
3. Configure:
   - **Text**: "60.0"
   - **Font Size**: 40-50
   - **Position**: Top-center (e.g., Pos: 0, 200)
   - **Alignment**: Center aligned

### Step 5: Assign UI to LogicScript
1. Select the object with **LogicScript** component
2. In Inspector, drag:
   - **CoinsDisplay** text to `Coins Text` field
   - **PipesDisplay** text to `Pipes Text` field
   - **TimerDisplay** text to `Timer Text` field (optional)

---

## Part 2: Setup Win Panel

### Step 1: Create Win Panel
1. Right-click on Canvas → Panel
2. Name it `WinPanel`
3. Configure:
   - **Color**: Semi-transparent dark (RGBA: 0, 0, 0, 200)
   - **Size**: Full screen or modal (e.g., 600x400)
   - **Position**: Center

### Step 2: Add Title Text
1. Right-click on WinPanel → UI → TextMeshPro - Text
2. Name it `WinTitleText`
3. Configure:
   - **Text**: "YOU WON!"
   - **Font Size**: 80
   - **Position**: Top of panel

### Step 3: Add Final Scores Display
1. Right-click on WinPanel → UI → TextMeshPro - Text
2. Name it `CoinsCountText` (IMPORTANT: Must match this name)
3. Configure:
   - **Text**: "Coins: 0"
   - **Font Size**: 40
   - **Position**: Upper-middle

4. Right-click on WinPanel → UI → TextMeshPro - Text
5. Name it `PipesCountText` (IMPORTANT: Must match this name)
6. Configure:
   - **Text**: "Pipes: 0"
   - **Font Size**: 40
   - **Position**: Below coins count

### Step 4: Add Buttons
1. Right-click on WinPanel → UI → Button - TextMeshPro
2. Name it `RestartButton`
3. Configure:
   - **Text**: "Restart"
   - **Position**: Bottom-left
4. Click the button's On Click () event → Add OnClick event → Select LogicScript → Select `restartGame()`

5. Right-click on WinPanel → UI → Button - TextMeshPro
6. Name it `MenuButton`
7. Configure:
   - **Text**: "Main Menu"
   - **Position**: Bottom-right
8. Click the button's On Click () event → Add OnClick event → Select LogicScript → Select `GoToMainMenu()`

### Step 5: Disable Win Panel Initially
1. Select **WinPanel** in Hierarchy
2. **Uncheck the checkbox** to disable it (will be enabled by LogicScript when player wins)

### Step 6: Assign Win Panel to LogicScript
1. Select the object with **LogicScript** component
2. In Inspector, drag **WinPanel** to the `Win Screen` field

---

## Part 3: Enable Time-Based Mode

### To Use Time-Based Win Condition:
1. Select the object with **LogicScript**
2. In Inspector, find `useTimeBasedWin` checkbox
3. **Check it** to enable time-based mode
4. Adjust `Win Time In Seconds` (default: 60)
5. Game will now end when time runs out

### To Use Score-Based Win Condition:
1. Select the object with **LogicScript**
2. **Uncheck** `useTimeBasedWin`
3. Adjust `Win Score` (default: 20 pipes)
4. Game will end when player passes that many pipes

---

## How It Works

### During Gameplay:
- Player collects coins → `Coins: X` increments
- Player passes pipe → `Pipes: X` increments
- Timer counts down (if time-based mode enabled)

### When Game Ends:
**Loss Condition**: Player collides with obstacle → Game Over screen shows
**Win Condition**: 
- Time runs out OR
- Required pipes passed
→ Win panel shows with final coin and pipe counts

### Methods Called:
- `logic.AddCoin()` - Called by Coin.cs when collected
- `logic.AddPipe()` - Called by MiddlePipeScript.cs when passed
- `logic.TriggerWin()` - Called automatically when win condition met
- `logic.gameOver()` - Called by PlayerScript.cs on collision

---

## Testing Checklist

- [ ] Coins text displays in top-left (shows current count)
- [ ] Pipes text displays in top-right (shows current count)
- [ ] Timer displays in top-center and counts down (if time-based mode enabled)
- [ ] Collect a coin → Coins count increases by 1
- [ ] Pass through a pipe → Pipes count increases by 1
- [ ] Win panel appears after 60 seconds (time-based) or 20 pipes (score-based)
- [ ] Win panel shows final coin and pipe counts
- [ ] Restart button works
- [ ] Main Menu button works

---

## Customization Options

### Change Win Time:
- In LogicScript Inspector → `Win Time In Seconds` → set to desired value (e.g., 30 for 30 seconds)

### Change Score-Based Win Condition:
- In LogicScript Inspector → `Win Score` → set to desired pipe count (e.g., 10)

### Toggle Win Mode:
- In LogicScript Inspector → `useTimeBasedWin` → check/uncheck

---

## Troubleshooting

**Scores not displaying:**
- Make sure TextMeshPro text objects are properly assigned in LogicScript
- Verify TextMeshPro is imported in your project

**Win panel not appearing:**
- Make sure WinPanel is assigned in LogicScript `Win Screen` field
- Make sure child text elements are named exactly: `CoinsCountText` and `PipesCountText`
- Check that WinPanel is disabled initially (checkbox unchecked in Hierarchy)

**Timer not showing/counting:**
- Make sure `useTimeBasedWin` is checked in LogicScript
- Make sure TimerDisplay is assigned in LogicScript `Timer Text` field

**Coins/Pipes not incrementing:**
- Verify Coin.cs script is attached to coin prefab
- Verify MiddlePipeScript.cs is attached to middle pipe
- Check console for errors