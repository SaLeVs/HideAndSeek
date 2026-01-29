using Unity.NetCode;

namespace Components
{
    public struct TestRpc : IRpcCommand
    {
        public int value;
    }
}
