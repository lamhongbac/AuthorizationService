using System.Security.Cryptography;
using System.Text;

namespace PwdSampleService
{
    /// <summary>
    /// Note:
    /// Saltsize va HashAlgorithm must be parametterized from outside (ex: config)
    /// 
    /// </summary>
    public class PasswordService
    {
        public PasswordService()
        {
            SaltSize = 64;
            HashAlgorithm = HashAlgorithmName.SHA256;
        }
        public int SaltSize { get; set; }
        public HashAlgorithmName HashAlgorithm { get; set; }

        /// <summary>
        /// Usage: khi tao moi password (register, change pwd, reset pwd)
        ///  
        /// 1. Input pass
        /// 2. random salt
        /// 3. hash=1+2
        /// 4 store: 2+3= (DB salt+DB hash)
        /// Check process
        /// 1/input+DB salt => hash
        /// 2/Compare hash with DB hash
        /// </summary>
        /// <param name="plainText">input pass</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public (string salt, string hash)
            GenerateHash(string plainText)
        {
            if (plainText == null)
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            // b1:

            //salt la 1 chuoi dc lay ngau nhien theo size
            // buoc 1 lay ra so bytes, buoc 2 doi byte ra string
            // co the dung convert tobase 64 hoac to hextring

            var buffer = RandomNumberGenerator.GetBytes(SaltSize);
            var salt = Convert.ToBase64String(buffer);
            // or Convert.ToHexString(buffer);

            //b2
            //Generate ra hash su dung salt +plain text
            var hash = GenerateHashPassword(plainText, salt);
            return (salt, hash);
        }
        /// <summary>
        /// salt dc tinh toan va sinh ra truoc do
        /// 
        /// DB hash=DB salt+input plain text
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private string GenerateHashPassword(string plainText, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(plainText + salt);

            //default
            var hash = SHA256.HashData(bytes);
            if (HashAlgorithm == HashAlgorithmName.SHA256)
            {
                hash = SHA256.HashData(bytes);
            }
            if (HashAlgorithm == HashAlgorithmName.SHA512)
            {
                hash = SHA512.HashData(bytes);
            }
            // so on....

            return Convert.ToBase64String(hash);

        }
        /// <summary>
        /// usage: khi login
        /// process:
        /// 
        /// 1/can cu vao userID=> db salt+ db hash
        /// 2/su dung input password+dbSalt=> hash
        /// 3/su dung db hash compare voi hash de biet la pwd co correct hay kg
        /// 
        /// </summary>
        /// <param name="inputPlainText">input pass</param>
        /// <param name="dbSalt">DB salt</param>
        /// <param name="dbHashPassword">=pass+DBSalt</param>
        /// <returns></returns>
        public bool Compare(string inputPlainText, string dbSalt, string dbHashPassword)
        {
            var calculatedHashPassword = GenerateHashPassword(inputPlainText, dbSalt);

            return calculatedHashPassword == dbHashPassword;
        }

    }
}
