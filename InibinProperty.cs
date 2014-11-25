namespace Dargon.Plugins.InibinTroybin
{
	/// <summary>
	/// A struct that exposes a key value pair from an inibin/troybin file as well as the section the pair came from
	/// </summary>
	public class InibinProperty
	{
		/// <summary>
		/// Hash key corresponding to a specific property
		/// </summary>
		public uint Key { get; private set; }
		/// <summary>
		/// Value
		/// </summary>
		public object Value { get; private set; }
		/// <summary>
		/// The section the key value pair came from within the file. Not hugely useful, but it can be useful for further reverse engineering
		/// </summary>
		public InibinTroybinDataSection Section { get; private set; }

		/// <summary>
		/// A struct that exposes a key value pair from an inibin/troybin file as well as the section the pair came from
		/// </summary>
		public InibinProperty(uint key, object value, InibinTroybinDataSection section)
		{
			Key = key;
			Value = value;
			Section = section;
		}
	}
}