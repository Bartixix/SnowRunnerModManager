# Disclamer
Due to some recent changes the app might crash and most features will not work at all. I'm currently dealing with a migration process and will deal with all code warnings soon.
# SnowRunnerModManager
## Goals
The goal of this project is to create a GUI based editor for the [SnowRunner's](https://saber.games/games/snowrunner) `initial.pak` file. Most editors allow you to modify current values and maybe add something new to existing files. That was not enough for me. I have figured out how to fully repack the `initial.pak` archive including it's `pak.load_list` file. That second part is integral for adding new files, which can be used to add engines, suspensions and more to already existing trucks!
## Scope of the project
The project mainly focuses on truck configs. Primarily trucks, engines, gearboxes, wheels and suspension sets. I may of course add more in the future. I also want to implement single-click repacking. [SnowRunner](https://saber.games/games/snowrunner) changes the `initial.pak` file every update and often makes the old one incompatible. That's a major roadblock for manual editing. The program will track all user changes in a project and will later be able to recreate them instantly, even without the previous archive.
# Development
### Current features
* Unpacking of `initial.pak` archives
* Visual field editor
* Partial integration between types (binding engines to trucks etc.)
### Currently working on
* XML parser migration - the previous implementation was really bad (XML -> C# object representation, C# objects -> GUI rendering)
* Full template/parenting support ([SnowRunner's](https://saber.games/games/snowrunner) XML stuff)
* Proper internal saving states (GUI Editor -> C# objects)
### Planned
* XML saving (C# objects -> XML files)
* Modification state tracking (partial support already done)
* `pak.load_list` repackaging
* Adding custom config files
* Full project support
* Project importing/exporting (projects will be stored as directories, exporting will allow for easy sharing)
### Possbily
* XML Replacenets for some more advanced edits unsupported by the editor
* Improvenemts to the XML storage format
# Contribution
If you have ideas for features you can open an [issue](https://github.com/Bartixix/SnowRunnerModManager/issues/) or email me at [bartixix@icloud.com](mailto:bartixix@icloud.com). It's likely I'll respond to an email faster.