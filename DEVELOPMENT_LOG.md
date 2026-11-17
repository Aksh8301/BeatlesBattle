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
**Status: Implemented, user-tested, and merged into `main`**
- **Branch:** `feature/fps-tps-controller-v1`
- **Merged PR:** https://github.com/Aksh8301/BeatlesBattle/pull/1
- **Files changed:** Added `PlayerInputHandler.cs`, `PlayerMovement.cs`, `PlayerCameraController.cs` and their Unity `.meta` files; configured `Assets/_Game/Scenes/CombatTestScene.unity`.
- **Implemented:** Existing `InputSystem_Actions` reused; local WASD movement, Shift sprint, Space jump, mouse look, V FPS/TPS switching, and prototype third-person camera obstruction handling.
- **Scene setup:** `PlayerPlaceholder` uses CharacterController (Capsule Collider disabled), three scripts attached, Input Actions assigned, Main Camera and player mesh references assigned.
- **Tests performed (user-reported Unity Play Mode):** WASD moves the player; FPS movement works; V switches perspectives; jumping works; sprinting works; mouse look works in both modes. Unity Console was reported empty before testing.
- **Git verification:** User committed and pushed the modified scene to the feature branch; remote scene inspected and controller components confirmed. PR #1 was subsequently approved and squash-merged into `main`.
- **Remaining at this milestone:** Crouch/slide were subsequently developed in the next milestone; vaulting, camera framing/wall occlusion, standalone build, automated tests, networking, and multiplayer authority validation remain.

## 2026-11-17 — Crouching and Sliding V1
**Status: Implemented, committed, pushed, and user-tested on feature branch; awaiting PR #2 merge approval**
- **Branch:** `feature/crouch-slide`
- **Commit:** `3996f5f` (`feat: implement and test crouching and sliding`)
- **PR:** https://github.com/Aksh8301/BeatlesBattle/pull/2 — open and ready for review, no reported conflicts, **not merged**.
- **Files changed:** `Assets/_Game/Scripts/Player/PlayerMovement.cs`, `Assets/_Game/Scripts/Player/PlayerInputHandler.cs`, `Assets/_Game/Scripts/Camera/PlayerCameraController.cs`, and `Assets/_Game/Scenes/CombatTestScene.unity`.
- **Implemented:** Hold C to crouch with smoothly adjusted CharacterController height and camera height; stand when clear; sprint-forward + press C to slide briefly, with decreasing slide speed; overhead obstruction test before standing. The prototype still uses a capsule without soldier animations.
- **User-reported Unity Play Mode tests:** Crouch lowers camera; release C returns to standing when unobstructed; sprint + C initiates a slide; under a temporary low ceiling the CharacterController height stays near 1.2 m after releasing C, then returns to 2 m when moving clear. Baseline WASD/sprint/jump and FPS/TPS controls previously passed.
- **Temporary test scene geometry:** `TestCeiling` cube was created for the overhead test and removed afterward. The scene diff also included `stanceCameraSpeed: 6` and other serialized crouch/slide settings.
- **Git checks:** `git diff --check` passed with no whitespace errors; Git displayed an LF-to-CRLF line-ending warning for the Unity scene. Four changed files were committed and pushed, and PR #2 was created.
- **Not verified:** Other overhead shapes and edge cases, detailed slide collisions and slopes, full camera occlusion, animations, automated tests, and builds.

## Next proposed milestone — Review and merge PR #2 (approval required)
1. Review this documentation update and final [PR #2 diff](https://github.com/Aksh8301/BeatlesBattle/pull/2/files).
2. Obtain explicit user approval before merging into `main`.
3. After merge, switch VS Code to `main` and pull updates.
4. Plan camera polish and new gameplay features in a new feature branch.

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
