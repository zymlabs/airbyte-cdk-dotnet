using System.IO;

namespace Airbyte.Cdk
{
    /// <summary>
    /// Airbyte Spec
    /// </summary>
    public class AirbyteSpec
    {
        public string SpecString { get; }

        public AirbyteSpec(string specString) => SpecString = specString;

        public static AirbyteSpec FromFile(string filename) => new (File.ReadAllText(filename));
    }
}
