graffiti
========

Graffiti is a high-performance rendering engine built specifically for the Reach profile on top of XNA/Monogame with a very specific feature set

* Support for the Reach profile
* CPU/GPU vertex transformation (using SkinnedEffect)
* Quake 3 shader style effects (Multi-pass) for anything Graffiti can render
* Keyframed/procedural animation framework
* Primitive rendering (antialiased, variable-sized points and lines)
* Particle system using complex/primitive objects
*Text rendering (with effects)
* Multiresolution/aspect ratio rendering support
* Designed to support third-party physics engines

Graffiti will only ever include rendering tasks and is not meant to be a catch-all game engine. For example: A 2D skeletal animation framework (to be released soon) will sit on top of Graffiti, not be a part of it. Most features you can think of have been thoughtfully omitted because they don't fit into the narrow range of functionality that Graffiti is meant to support. It is entirely possible that I missed something - so if you can think of any omissions do let me know.