using System.Collections;

public interface IPlug  {
	int PlugId {get;}

	void Init();
	void Destroy();
}
