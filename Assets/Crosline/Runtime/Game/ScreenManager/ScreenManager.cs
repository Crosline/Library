using System.Threading.Tasks;
using Crosline.Game.Lifecycle;

namespace Crosline.Game.ScreenManager {
    public class ScreenManager : GameSystemBase, IUpdateListener {

        public override async Task Initialize() {
            await Task.Delay(5_000);
        }

        public override void Dispose() {
            throw new System.NotImplementedException();
        }

        public void OnUpdate() {
            throw new System.NotImplementedException();
        }
    }
}