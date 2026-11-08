namespace SnowRunnerModManager.Infrastructure.IO
{
    public class Constants
    {
        // pak files
        public const string InitialPAK = "initial.pak";
        public const string SharedPAK = "shared.pak";
        public const string SharedSoundPAK = "shared_sound.pak";

        // game dir constants
        public const string PAKFolderRelative = "preload\\paks\\client";
        public const string EXERelative = "Sources\\Bin\\SnowRunner.exe";

        // project dir constants
        public const string InitialUnpacked = "initial-unpacked";
        public const string SavedModified = "saved\\modified";
        public const string SavedCustom = "saved\\custom";

        // initial.pak structures
        public const string Templates = "[media]\\_templates\\trucks.xml";
        public const string Engines = "engines";
        public const string Gearboxes = "gearboxes";
        public const string Wheels = "wheels";
        public const string Trucks = "trucks";

    }
}
