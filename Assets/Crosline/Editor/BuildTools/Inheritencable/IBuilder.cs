namespace Crosline.BuildTools.Editor {
    public interface IBuilder {

        public static Builder Instance { get; }

        public void StartBuild();

    }
}
