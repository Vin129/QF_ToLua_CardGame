using System;
namespace ScriptKit {
	public class PlugCollector {
		private LuaPlug plug;
		public PlugCollector(){}
		public IPlug GetPlug(){
			return plug;
		}
		public void InitPlug(){
			plug = LuaPlug.GetInstance();
			plug.Init();
		}
	}
}
