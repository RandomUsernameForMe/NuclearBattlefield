# Nuclear Battlefield

A turn-based party battler built in Unity as my bachelor project (2022).

## Highlights

- **Component-based creatures.** Every ability or trait (weapons, armor, poison, stun, healing, first strike, …) is a separate `Component`. An attack is a `Query` object passed through all components of the attacker and the defender, each one adding or modifying damage and effects.
- **Dependency-ordered effects.** Components declare what they depend on; a `TopologicalWizard` computes a topological order so effects always resolve in the right sequence.
- **Simulation-balanced enemies.** A `SimulationRunner` plays hundreds of automated battles and adjusts the enemy party until the player's win rate lands in a target range.
- **Upgrades and campfire** between battles, AI-controlled enemies, story screens and level transitions.

Design notes: [GameLoopExplained.pdf](GameLoopExplained.pdf), [StatusEffectsExplained.pdf](StatusEffectsExplained.pdf), [Level managementExplained.pdf](Level%20managementExplained.pdf).

## Running

Open the project in **Unity 2020.3.0f1**. Game code is in [`Assets/Scripts/`](Assets/Scripts/).

---

🇨🇿 **Česky:** Tahová bojová hra s partou postav v Unity, bakalářská práce (2022). Schopnosti postav jsou samostatné komponenty, útok prochází jako dotaz všemi komponentami v topologickém pořadí podle závislostí, a obtížnost nepřátel se vyvažuje automatickými simulacemi bitev na cílovou úspěšnost.
