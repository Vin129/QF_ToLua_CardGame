using System.Collections;
namespace ScriptKit {
	public interface IPlug  {
		int PlugId {get;}

		void Init();
		void Destroy();
	}
}
