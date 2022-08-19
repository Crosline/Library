namespace Crosline.BuildTools.Editor {
    public interface IBuilder {

        public static CommonBuilder Instance { get; }

        public void Execute();

    }
}
