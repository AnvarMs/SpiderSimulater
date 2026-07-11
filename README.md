<div align="center">

<img src="https://readme-typing-svg.demolab.com?font=Fira+Code&size=26&pause=1000&color=00F7FF&center=true&vCenter=true&width=600&lines=Procedural+Spider+Locomotion;IK-Driven+Leg+Animation+in+Unity" alt="Typing SVG" />

<br/>

<img src="https://skillicons.dev/icons?i=unity,cs&theme=dark" title="Unity, C#"/>

<br/><br/>

[![Live Demo](https://img.shields.io/badge/🎮_Play_Now-itch.io-FA5C5C?style=for-the-badge)](https://anvarms.itch.io/spider-simulator)

</div>

<br/>

## 🕷️ About

A Unity prototype built to test **procedural animation skills** — instead of relying on baked walk cycles, each of the spider's six legs is animated in real time using **Inverse Kinematics**, driven by the spider's body movement and player input. As the body moves, the legs solve their own footstep targets procedurally, giving fully dynamic, terrain-adaptive locomotion with no pre-authored animation clips.

This is a technical experiment first, gameplay prototype second — the goal was to understand IK rigging, gait systems, and procedural motion at a deep level.

<br/>

## 🎥 Preview

<div align="center">

<br/><i>Hexapod rig with per-leg IK targets, navigating a stepped obstacle course</i>
<br/><br/>
<img width="1267" height="712" alt="Screenshot 2026-03-14 231753" src="https://github.com/user-attachments/assets/4c13fbae-dec7-4b50-aa89-315d50814f6b" />


<br/><i>Close-up of the leg IK chains in a raised gait pose</i>
</div>
<img width="1920" height="1080" alt="Screenshot 2026-03-14 233305" src="https://github.com/user-attachments/assets/9ddd0695-da66-4698-ab28-3ade93d436a0" />
<br/>

## ✨ Features

- 🦿 **Six-legged procedural IK rig** — each leg independently targets footstep positions using Unity's Animation Rigging package
- 🚶 **Diagonal gait system** — legs step in a cross-pattern gait group (e.g. front-left + back-right together) instead of animating all at once, matching how real hexapods walk
- 🧭 **NavMesh-driven navigation** — AI Navigation (Surfaces, Agents, Obstacles) used to path the spider across the test environment
- 🪜 **Terrain-adaptive testing course** — stepped platforms and scattered stepping-stone blocks built specifically to stress-test how the IK legs adapt to uneven, non-flat ground
- 🎯 **Real-time target solving** — leg targets are recalculated live off the body's movement rather than played back from a fixed clip

<br/>

## 🛠️ Tech Stack

<div align="center">

<img src="https://skillicons.dev/icons?i=unity,cs,git,github&theme=dark" title="Unity, C#, Git, GitHub"/>

</div>

`Unity` `C#` `Animation Rigging (Two Bone IK / Chain IK)` `AI Navigation / NavMesh` `Procedural Animation`

<br/>

## 🚀 Getting Started

```bash
git clone https://github.com/AnvarMs/SpiderSimulater.git
```

Open the project folder in **Unity Hub** (add it as an existing project), let it import, then open the main scene under `Assets/` and hit Play.

<br/>

## 🤝 Connect

<div align="center">

<a href="mailto:anvarms2005@gmail.com">📧 Email</a>&nbsp;&nbsp;|&nbsp;&nbsp;
<a href="https://www.linkedin.com/in/-anvar-ms">💼 LinkedIn</a>&nbsp;&nbsp;|&nbsp;&nbsp;
<a href="https://youtube.com/@CHARLydp">📺 YouTube</a>&nbsp;&nbsp;|&nbsp;&nbsp;
<a href="https://anvarms.itch.io/spider-simulator">🎮 itch.io</a>&nbsp;&nbsp;|&nbsp;&nbsp;
<a href="https://github.com/AnvarMs">🐙 GitHub</a>

</div>
