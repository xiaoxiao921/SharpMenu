namespace SharpMenu
{
    internal class Config
    {
        internal interface IValue
        {
            object Value { get; }
        }

        internal class Value<T> : IValue where T : unmanaged
        {
            internal T _Value;

            object IValue.Value => _Value;

            internal Value(T value)
            {
                _Value = value;
                _configValues.Add(this);
            }
        }

        internal static string FilePath;

        private static List<IValue> _configValues = new();

        internal static void Init(string filePath)
        {
            FilePath = filePath;
        }

        internal static void Save()
        {
            foreach (var configValue in _configValues)
            {

            }
        }
    }
}
