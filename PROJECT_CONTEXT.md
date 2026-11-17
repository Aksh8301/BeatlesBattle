# Beatle's Battle — Project Context

> Working project reference for resuming development across ChatGPT conversations.
> Last reviewed: 2026-11-17. Update this document when decisions or milestones change.

## Project identity
- **Game title:** Beatle's Battle
- **Unity project/repository:** `BeatlesBattle`
- **Repository:** https://github.com/Aksh8301/BeatlesBattle
- **Engine:** Unity 6.3 LTS (6000.3.28f1)
- **Render pipeline:** Universal Render Pipeline (URP); package 17.3.0 in reviewed manifest
- **Platform:** Windows PC first
- **Goal:** Original commercial military shooter; commercial asset rights and title availability need verification before release.

## Approved design direction
- **Visuals:** Semi-realistic, medium-quality baseline with scalable Low / Medium / High presets (planned, not yet configured).
- **Combat:** Hybrid tactical gunplay and responsive modern military movement.
- **Perspectives:** Switchable first-person / third-person view during gameplay (V key); basic prototype merged into `main` via PR #1 and user-tested in Play Mode.
- **Online multiplayer:** Up to 10v10 (20 combatants); dedicated authoritative server architecture planned using Unity Netcode for GameObjects (NGO).
- **Offline battles:** Configurable matches against AI bots, targeting 10v10 capacity.
- **Campaign:** Single-player modern military special-forces story with missions, allied/enemy AI, objectives and checkpoints.
- **Competitive fairness:** Consider separate FPS-only matchmaking rules from mixed-perspective modes.
- **Original content:** Original maps, characters, branding, and commercially permitted assets; no copying proprietary game content.

## Scope and milestone strategy
1. **Foundation:** Unity scene, repository, project layout, input baseline.
2. **Controller prototype:** CharacterController-based movement, mouse look, FPS/TPS camera switching — V1 implemented, tested, and merged into `main` via PR #1; crouch/slide changes on `feature/crouch-slide` in PR #2, pending merge.
3. **Combat prototype:** Aiming, shooting, recoil, reload, health/damage.
4. **Vertical slice:** One polished level/mission, friendly and enemy AI, mission loop.
5. **Offline battles:** Team match rules, bots, difficulty, performance tests.
6. **Online multiplayer:** NGO integration, prediction/lag handling, 10v10 dedicated-server testing.
7. **Commercial content and release:** Maps, weapons, soldier art, UI/audio, optimization, licensing, QA and publishing.

These are development goals, not completed features or delivery promises. Prefer validating a small playable vertical slice before scaling content.

## Repo structure and verified files
- `Assets/_Game/` organized for Art, Animations, Audio, Campaign, Prefabs, Scenes, Scripts, Settings, Tests.
- `Assets/_Game/Scenes/CombatTestScene.unity` exists and was inspected on GitHub.
  - Scene objects verified: `Ground`, `PlayerPlaceholder`, `Main Camera`, `Directional Light`, `Global Volume`.
  - Ground position `(0,0,0)`, scale `(10,1,10)`; placeholder is a capsule.
- `Packages/manifest.json` includes URP 17.3.0, Input System 1.20.1, AI Navigation 2.0.16.
- `ProjectSettings/ProjectVersion.txt` specifies `6000.3.28f1`.
- Root `.gitignore` excludes generated Unity and IDE files.
- `Assets/_Game/Scenes/CombatTest/` is an extra folder, distinct from `CombatTestScene.unity`; do not confuse it with the scene or delete it without approval.

## Current status (2026-11-17)
- Unity foundation, test scene, ground and player capsule created and pushed to GitHub.
- **FPS/TPS controller V1 merged into `main`** through [PR #1](https://github.com/Aksh8301/BeatlesBattle/pull/1). Scripts: `Assets/_Game/Scripts/Player/PlayerInputHandler.cs`, `PlayerMovement.cs`, and `Assets/_Game/Scripts/Camera/PlayerCameraController.cs`.
- `CombatTestScene.unity` connects the components to `PlayerPlaceholder`; its Capsule Collider is disabled and CharacterController is enabled, with input and camera references assigned.
- **User-reported Unity Play Mode tests passed (V1):** WASD movement, sprint with Left Shift, jump with Space, mouse look in both perspectives, and V to switch FPS/TPS.
- **Crouch & slide V1 implemented and user-tested on `feature/crouch-slide`**; [PR #2](https://github.com/Aksh8301/BeatlesBattle/pull/2) is open, ready for review, and **not yet merged into `main`**.
- Crouch uses the existing C action, adjusts CharacterController height to about 1.2 m, and slows movement; sprint-forward + C initiates a short slide. Camera stance height transitions smoothly. No animation assets have been integrated.
- **User-reported Unity Play Mode tests passed (crouch/slide):** C crouches, releasing C restores standing height (2 m) when clear; sliding works while sprinting; under a temporary test ceiling, releasing C keeps the controller at 1.2 m, then height returns to 2 m after exiting the overhead obstacle.
- `TestCeiling` was temporary test geometry and was removed after testing. `CombatTestScene.unity` was included in the crouch/slide branch commit for camera stance settings; the PR includes four modified files.
- **Remaining work/limits:** Standing clearance was checked with one simple ceiling only, not exhaustively; additional collider edge cases, camera framing/occlusion polish, crouch/slide animations, vaulting, automated tests, standalone build, and networking validation remain.
- Low/Medium/High quality presets, combat, campaign logic, bots, and networking are **planned**, not implemented or tested.
- The repository was reported as **public** at the last check. Recheck visibility before uploading proprietary commercial material.

## Collaboration rules
- Inspect only relevant files, reuse what is already known, and review existing code before suggesting changes.
- Present a concise implementation plan and affected file list; **ask for explicit approval before editing, deleting, committing, or pushing gameplay code**.
- Prefer incremental, safe modifications and Git diffs; do not overwrite existing scenes/settings unnecessarily.
- Keep generated Unity directories, credentials and other secrets out of Git.
- Maintain `DEVELOPMENT_LOG.md` after approved milestones.
- Report what was actually tested; never claim Unity runtime validation without running it.

## How to resume in a new chat
Share this GitHub repository and ask ChatGPT to read `PROJECT_CONTEXT.md` and `DEVELOPMENT_LOG.md` first, then only the relevant Unity files. Continue from the latest unfinished milestone and obtain approval before edits.
