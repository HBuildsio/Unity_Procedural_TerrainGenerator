# Procedural Terrain Generator

A modular Unity-based system for generating infinite, procedural 3D terrain using layered Perlin noise, dynamic chunking, and Level of Detail (LOD) optimization.

![Infinite Terrain Preview](gitAsset/InfiniteTerrain.gif)

## Technical Architecture

The project is structured into several specialized modules to handle different aspects of the generation pipeline:

### 1. Noise Generation (`Noise.cs`)
The core mathematical foundation of the terrain. It implements **Layered Perlin Noise (Fractal Brownian Motion)**.
- **Octaves**: Overlays multiple noise layers at different scales.
- **Persistence**: Controls the amplitude decay of successive octaves.
- **Lacunarity**: Controls the frequency increase of successive octaves.
- **Normalization**: Maps the final noise values into a 0-1 range using `Mathf.InverseLerp`, ensuring consistent height distribution.

### 2. Mesh Generation (`MeshGenerator.cs`)
Converts 2D height maps into 3D meshes.
- **LOD Support**: Implements mesh simplification by skipping vertices based on the Level of Detail parameter.
- **Height Curve Mapping**: Utilizes Unity's `AnimationCurve` to allow non-linear height scaling (e.g., to create flat valleys or sharp peaks).
- **Data Structure**: Uses a custom `MeshData` class to manage vertex arrays, triangle indices, and UV coordinates before committing to a Unity `Mesh` object.

### 3. Endless Terrain System (`EndlessTerrain.cs`)
Manages the lifecycle of terrain chunks relative to a viewer.
- **Dynamic Chunking**: The world is divided into discrete chunks (e.g., 241x241 vertices).
- **Culling**: Chunks are enabled/disabled based on their distance from the viewer's current position.
- **Dictionary Management**: Efficiently tracks active chunks using a coordinate-based `Dictionary`.

### 4. Map Orchestration (`MapGenerator.cs`)
The central controller that integrates all modules.
- **View Modes**: Supports visualization of raw Noise maps, Color maps (based on regions), or the final 3D Mesh.
- **Region System**: Defines `TerrainTypes` with height thresholds and colors for realistic biome distribution (e.g., Water, Sand, Grass, Rock, Snow).
- **Editor Tooling**: Includes a custom inspector (`MapGeneratorEditor.cs`) for real-time terrain updates during design.

## Key Technical Specifications

| Feature | Implementation |
| :--- | :--- |
| **Noise Algorithm** | Layered Perlin Noise |
| **Mesh Density** | 241 x 241 (Standard Chunk Size) |
| **LOD Levels** | 0 to 6 (Simplified via vertex skipping) |
| **Height Control** | AnimationCurve evaluation |
| **Performance** | Dynamic Object Pooling/Visibility Toggling |

## Development Workflow
1. **Noise Configuration**: Adjust Scale, Octaves, Persistence, and Lacunarity in the `MapGenerator` component.
2. **Biome Definition**: Configure the `Regions` array to define height-based coloring.
3. **Mesh Sculpting**: Use the `Mesh Height Multiplier` and `Mesh Height Curve` to refine the 3D topology.
4. **Endless Setup**: Assign a `Viewer` (e.g., Player Transform) to the `EndlessTerrain` script to enable infinite generation.
