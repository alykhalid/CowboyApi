using Cowboy.API.Entities;

namespace Cowboy.API.Services
{
    public class CowboyResponse
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public CowboyEntity Cowboy { get; private set; }

        public CowboyResponse(CowboyEntity cowboy)
        {
            Success = true;
            Message = string.Empty;
            Cowboy = cowboy;
        }

        public CowboyResponse(string message)
        {
            Success = false;
            Message = message;
            Cowboy = default;
        }
    }
}
