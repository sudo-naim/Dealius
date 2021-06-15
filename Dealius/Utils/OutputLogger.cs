using BoDi;
using Xunit.Abstractions;

namespace Dealius
{
    public static class OutputLogger
    {
        private static IObjectContainer _objectContainer;

        public static void Initialize(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public static void Log(string message)
        {
            _objectContainer.Resolve<ITestOutputHelper>().WriteLine(message);
        }
    }
}
