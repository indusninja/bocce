namespace BocceEngine.Core.EngineComponents
{
    public interface IEngineObject
    {
		bool IsDebugMode { get; set; }

        bool IsDrawable { get; set; }

		int DrawOrder { get; set; }
        
		bool IsUpdateable { get; set; }
    }
}
