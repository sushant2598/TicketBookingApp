using Microsoft.Extensions.Configuration;
using NETCore.Encrypt;

namespace UserService.Shared
{
    public class EncryptionDecryption
    {
        //public IConfiguration Configuration { get; }

        static string key = "$5U5h@n!_ch@h@r_5U5h@n!_ch@h@r_$";
        //static string key;
        public EncryptionDecryption() { }
        /*public EncryptionDecryption(IConfiguration configuration)
        {
        this.Configuration = configuration;
        key = Configuration.GetSection("EncryptionKey").Value;
        }*/

        public static string Encrpt(string Password)
        {
            return EncryptProvider.AESEncrypt(Password, key);
        }
        public static string Decrpt(string Password)
        {
            return EncryptProvider.AESDecrypt(Password, key);
        }
    }
}
