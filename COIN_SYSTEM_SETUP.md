# Coin Collection System Setup Guide

## Overview
This guide sets up a coin collection system where players can collect coins between pipes for extra points.

## Scripts Created
1. **CoinSpawner.cs** - Spawns coins at intervals between pipes
2. **Coin.cs** - Handles coin collection and scoring
3. **Updated AudioManager.cs** - Added coin collection sound support

---

## Step 1: Prepare Coin Sprite

### Option A: Use an existing sprite
If you have a coin sprite in `Assets/Sprites/`, use it. Make sure it's:
- **Texture Type:** Sprite (2D and UI)
- **Sprite Mode:** Single
- **Pixels Per Unit:** 100
- Size: ~50x50 pixels

### Option B: Create a simple coin sprite
1. In `Assets/Sprites/`, create a yellow circle (or use an existing icon)
2. Name it `Coin` or `coin`
3. Apply the sprite settings above

---

## Step 2: Create Coin Prefab

1. **Create an Empty GameObject** in your scene and name it `CoinPrefab`
2. **Add components:**
   - **SpriteRenderer** - Assign your coin sprite
   - **CircleCollider2D** - Set as trigger (check "Is Trigger")
   - Attach the **Coin.cs** script
3. **In the Coin.cs script, configure:**
   - **moveSpeed:** 5 (same as pipe speed)
   - **scoreValue:** 5 (points per coin)

4. **Drag the CoinPrefab from Hierarchy into your Prefabs folder**
   - Path: `Assets/Prefabs/Coin.prefab` or `Assets/Prefabs/CoinPrefab.prefab`
5. **Delete from scene** (keep only the prefab file)

---

## Step 3: Setup CoinSpawner in Scene

1. **Create an Empty GameObject** and name it `CoinSpawner`
2. **Attach CoinSpawner.cs script** to it
3. **In the Inspector, configure:**
   - **Coin Prefab:** Drag the coin prefab from `Assets/Prefabs/`
   - **Spawn Rate:** 2 (same as PipeSpawner)
   - **Height Offset:** 10 (same as PipeSpawner)
   - **Coin Spawn Chance:** 0.7 (0-1 scale, 70% chance a coin spawns with each pipe)
   - **Coin Vertical Range:** 3 (random vertical offset)

---

## Step 4: Add Coin Sound to AudioManager

1. **Select AudioManager** in your scene
2. **In the Inspector**, find the **Coin Sound** field
3. **Assign a coin collection audio clip** (choose from your Audio folder)
   - Suggested: Use any "pick up", "ding", or "pop" sound
   - If you don't have one, leave empty (game will still work, just no sound)

---

## Step 5: Verify Player Collision Tag

Make sure your **Player GameObject** has the tag "Player":
1. Select your bird/player in the Hierarchy
2. In Inspector, find **Tag** dropdown
3. Select "Player" (should already be set)

---

## Testing Checklist

- [ ] Coin prefab created with SpriteRenderer and CircleCollider2D (trigger)
- [ ] CoinSpawner assigned coin prefab
- [ ] CoinSpawner spawn rate and height offset match PipeSpawner
- [ ] Player has "Player" tag
- [ ] AudioManager has coin sound assigned (optional)
- [ ] Play game and coins should appear between pipes
- [ ] Collect coins by flying through them
- [ ] Score increases by 5 points per coin
- [ ] Coins fade out when collected

---

## Customization Options

### Adjust Coin Spawn Frequency
In **CoinSpawner.cs**:
- `coinSpawnChance = 0.7f;` → Lower value = fewer coins (e.g., 0.5 = 50% chance)
- `coinSpawnChance = 1.0f;` → Guaranteed coin with each pipe

### Adjust Coin Points
In **Coin.cs** (or Inspector):
- `scoreValue = 5;` → Change to reward more/less points

### Adjust Coin Speed
In **Coin.cs**:
- `moveSpeed = 5;` → Must match PipeSpawner's move speed
- (Check PipeMovement.cs to confirm pipe speed)

### Adjust Coin Vertical Variation
In **CoinSpawner.cs**:
- `coinVerticalRange = 3f;` → Higher = more vertical spread, encourages skillful movement

---

## Troubleshooting

**Coins not appearing:**
- Verify CoinSpawner has coin prefab assigned
- Check coin prefab has SpriteRenderer with sprite
- Confirm spawn rate isn't too high (should be 2)

**Coins not collectible:**
- Make sure CircleCollider2D is set as trigger (check "Is Trigger")
- Verify Player has "Player" tag
- Check Coin.cs script is attached to prefab

**Score not updating:**
- Verify LogicScript is tagged "Logic"
- Check Coin.cs can find LogicScript with `GameObject.FindGameObjectWithTag("Logic")`

**Coins moving too fast/slow:**
- Match `moveSpeed` in Coin.cs to pipe movement speed
- Check PipeMovement.cs for the correct speed value

---

## Next Steps

Once coins are working:
1. Add different coin types (worth different points)
2. Add coin visual effects (particles, scale pulse)
3. Add coin multipliers or chains
4. Track "Coins Collected" as a stat
5. Create coin-based achievements