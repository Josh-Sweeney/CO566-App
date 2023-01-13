using System;
namespace mims.iOS
{
    public class Bootstrapper : mims.Bootstrapper
    {
        public static void Init()
        {
            var instance = new Bootstrapper();  
        }
    }
}
