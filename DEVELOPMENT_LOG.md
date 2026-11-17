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

**Not yet implemented**
- No playable FPS/TPS movement or camera scripts verified.
- No shooting/damage system, AI bots, campaign gameplay, online networking, or quality-presets implementation verified.
- Unity play-mode tests and build validation have not been run as part of this review.

## Next proposed milestone — Player controller (approval required)
1. Review existing Input System actions and scene references before writing code.
2. Propose a small modular design for player input, CharacterController movement and camera.
3. Implement WASD, mouse look, jump and FPS/TPS toggle first.
4. Verify grounding/collision, camera clipping and Unity Console output in play mode.
5. Add sprint/crouch/slide only after the basics work.
6. Review Git diff, then commit and push only with approval.

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
