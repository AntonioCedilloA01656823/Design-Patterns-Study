---
name: design-patterns-analysis
description: Analyze a named software design pattern, explain when to use it, and create small code examples and exercises when requested.
---

# Design patterns analysis

Use this skill when the user asks to understand, compare, implement, or practice a software design pattern.

## Inputs

- Pattern name.
- Optional context, such as a codebase, design problem, or desired difficulty.

Generated examples and exercises always use Python. Do not ask the user to choose a programming language.

If the pattern name is missing or ambiguous, ask one concise question before proceeding.

## Workflow

1. Identify the pattern's category and the design problem it addresses.
2. Explain the intent, the participating roles, the collaboration between those roles, and the tradeoffs.
3. Distinguish the pattern from the closest commonly confused alternative.
4. Create a minimal example that demonstrates the pattern's defining behavior. Keep the example runnable when practical and avoid unrelated framework code.
5. Add one or more exercises that require the learner to apply or modify the pattern. Include a short goal and acceptance criteria, but do not reveal the solution unless the user asks for it.
6. Check that the example matches the explanation, uses consistent names, and does not claim that the pattern is always the best choice.

## Generated artifacts

Create a folder only when the user asks for files, a study guide, examples, or exercises. Name it with the pattern name in lowercase kebab-case. Use this structure:

```text
<pattern-name>/
	summary.md
	example.ipynb
	exercises.ipynb
```

`summary.md` must contain:

- Intent and the problem it solves.
- Roles and a small structural outline.
- When to use it and when not to use it.
- Tradeoffs and a comparison with a related pattern.
- Instructions for running or reading the example.

`example.ipynb` must contain one focused Python example. `exercises.ipynb` must contain Python exercises ordered from basic application to adaptation or critique. Include markdown cells for explanations and instructions, and code cells for runnable Python.

Every notebook must be valid JSON. Each cell must be an object in the `cells` array with a `metadata.language` property set to `markdown` or `python`. Existing cells must retain a unique `metadata.id`; new cells do not need an id. Include the standard notebook fields such as `nbformat`, `nbformat_minor`, and `metadata`.

If the user asks only for an explanation, answer in the conversation and do not create a folder. After creating artifacts, report the exact relative path and list the files created.