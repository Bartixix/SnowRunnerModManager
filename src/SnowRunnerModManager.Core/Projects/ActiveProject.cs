using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Core.GameClasses.Common;
using SnowRunnerModManager.Core.GameClasses.Objects;
using SnowRunnerModManager.Core.GameClasses.Templates;
using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.Projects
{
    public class ActiveProject
    {
        //[EditorSource]
        //public static GroupRegistry<Engine> Engines { get { return Project.CurrentProject.Engines; } }
        //[EditorSource]
        //public static GroupRegistry<Gearbox> Gearboxes { get { return Project.CurrentProject.Gearboxes; } }
        //[EditorSource]
        //public static GroupRegistry<TruckTire> Wheels { get { return Project.CurrentProject.Wheels; } }
        //public static Registry<Truck> Trucks { get { return Project.CurrentProject.Trucks; } }
        ////public static Registry<WheelFrictionTemplate> WheelTemplates { get { return Project.CurrentProject.WheelTemplates; } }



        //[EditorSource]
        //public static Registry<TruckTire> TestRegistry { get; } = new Func<Registry<TruckTire>>(() =>
        //{
        //    Registry<TruckTire> reg = new();

        //    reg.Add(new() { Name = "t1", Origin = "", BodyFriction = new(0), BodyFrictionAsphalt = new(0), Price = 0, StateInit = ModificationState.Original, SubstanceFriction = new(0), UnlockByRank = 0, UnlockByExploration = false });
        //    reg.Add(new() { Name = "t2", Origin = "", BodyFriction = new(0), BodyFrictionAsphalt = new(0), Price = 0, StateInit = ModificationState.Original, SubstanceFriction = new(0), UnlockByRank = 0, UnlockByExploration = false });
        //    reg.Add(new() { Name = "t3", Origin = "", BodyFriction = new(0), BodyFrictionAsphalt = new(0), Price = 0, StateInit = ModificationState.Original, SubstanceFriction = new(0), UnlockByRank = 0, UnlockByExploration = false });

        //    return reg; ;
        //})();


        [EditorSource]
        public static Registry<GameContainer<Engine>> Engines => Project.CurrentProject.Engines;
        [EditorSource]
        public static Registry<GameContainer<Gearbox>> Gearboxes => Project.CurrentProject.Gearboxes;
        [EditorSource]
        public static Registry<GameContainer<TruckTire>> Tires => Project.CurrentProject.Tires;
        [EditorSource]
        public static Registry<GameContainer<Truck>> Trucks => Project.CurrentProject.Trucks;

        [EditorSource]
        public static Registry<TruckTireTemplate> TireTemplates => Project.CurrentProject.TireTemplates;
        [EditorSource]
        public static Registry<WheelFrictionTemplate> FrictionTemplates => Project.CurrentProject.FrictionTemplates;

        [EditorSource]
        public static Registry<TruckTire> TestRegistry { get; } = new Func<Registry<TruckTire>>(() =>
        {
            Registry<TruckTire> reg = new();

            reg.Add(new() { Name = "t1", Origin = "", BodyFriction = new("", 0), BodyFrictionAsphalt = new("", 0), Price = 0, StateInit = ModificationState.Original, SubstanceFriction = new("", 0), UnlockByRank = 0, UnlockByExploration = false });
            reg.Add(new() { Name = "t2", Origin = "", BodyFriction = new("", 0), BodyFrictionAsphalt = new("", 0), Price = 0, StateInit = ModificationState.Original, SubstanceFriction = new("", 0), UnlockByRank = 0, UnlockByExploration = false });
            reg.Add(new() { Name = "t3", Origin = "", BodyFriction = new("", 0), BodyFrictionAsphalt = new("", 0), Price = 0, StateInit = ModificationState.Original, SubstanceFriction = new("", 0), UnlockByRank = 0, UnlockByExploration = false });

            return reg; ;
        })();
    }
}
