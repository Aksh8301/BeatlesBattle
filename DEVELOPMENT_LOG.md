# Beatle's Battle — Development Log

> Chronological development record. Distinguish verified completion from proposed work.

## 2026-11-17 — Project foundation
**Status: Completed / repository verified**
- Installed and created project with Unity 6.3 LTS (6000.3.28f1).
- Chose URP and semi-realistic, medium-quality art direction.
- Confirmed design: hybrid tactical + modern movement; switchable FPS/TPS; 10v10 online; offline bot matches; modern military special-forces story campaign.
- Created `Assets/_Game/` folders for art, scripts, campaign, scenes, prefabs and related assets.
- Created `Assets/_Game/Scenes/CombatTestScene.unity` containing `Ground`, `PlayerPlaceholder`, `Main Camera`, `Directional Light` and `Global Volume`.
- Published Unity project to GitHub: https://github.com/Aksh8301/BeatlesBattle
- Verified core repository files, Unity version and package manifest via GitHub.
- Confirmed Git ignore rules for Unity-generated files.

**Not implemented in foundation milestone**
- At the time of initial foundation review, player movement/camera scripts, combat, AI bots, campaign gameplay, online networking, and quality presets were not verified. The subsequent controller milestone is documented below.

## 2026-11-17 — FPS/TPS controller V1 prototype
**Status: Implemented and user-tested in Unity; awaiting review and merge**
- **Branch:** `feature/fps-tps-controller-v1`
- **Draft PR:** https://github.com/Aksh8301/BeatlesBattle/pull/1
- **Files changed:** Added `PlayerInputHandler.cs`, `PlayerMovement.cs`, `PlayerCameraController.cs` and their Unity `.meta` files; configured `Assets/_Game/Scenes/CombatTestScene.unity`.
- **Implemented:** Existing `InputSystem_Actions` reused; local WASD movement, Shift sprint, Space jump, mouse look, V FPS/TPS switching, and prototype third-person camera obstruction handling.
- **Scene setup:** `PlayerPlaceholder` uses CharacterController (Capsule Collider disabled), three scripts attached, Input Actions assigned, Main Camera and player mesh references assigned.
- **Tests performed (user-reported Unity Play Mode):** WASD moves the player; FPS movement works; V switches perspectives; jumping works; sprinting works; mouse look works in both modes. Unity Console was reported empty before testing.
- **Git verification:** User committed and pushed the modified scene to the feature branch; remote scene inspected and controller components confirmed. PR remains draft; `main` has not been merged.
- **Remaining issues:** Crouch/slide/vault not implemented; camera pitch/framing and third-person wall occlusion need further validation; no standalone build, automated tests, networking, or multiplayer authority validation yet.

## Next proposed milestone — Controller polish and PR merge (approval required)
1. Review the documentation updates and final PR diff.
2. Obtain explicit approval before merging the tested V1 feature into `main`.
3. After merge, pull `main` locally and verify the Unity scene opens.
4. Plan crouch, slide, vault, camera refinement and broader collision tests in a separate milestone.

## Issue / caution
- An existing folder `Assets/_Game/Scenes/CombatTest/` has a similar name to the real scene file `CombatTestScene.unity`. Avoid confusing the two.
- The repository was public at the previous check; choose appropriate visibility for commercial source/assets.
- All future entries should name the commit or PR when available and record tests performed.

## Change-log template
### YYYY-MM-DD — Milestone
- **Files changed:**
- **Implemented:**
- **Tests performed / results:**
- **Remaining issues:**
- **Commit / PR:**
