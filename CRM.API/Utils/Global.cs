using CRM.Domain.Entities;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace CRM.API.Utils
{
    public static class Global
    {
        public static string Criptografa(string valor)
        {
            try
            {
                byte[] encrypted;
                byte[] toEncrypt;
                byte[] key;
                byte[] IV;

                ASCIIEncoding textConverter = new ASCIIEncoding();
                //RijndaelManaged myRijndael = new RijndaelManaged();
                //testar pra ver
                using var myRijndael = Aes.Create("AesManaged");

                //Seta key e IV.
                key = Encoding.ASCII.GetBytes("tafnersoftwareta");
                IV = Encoding.ASCII.GetBytes("tafnersoftwareta");

                //Pega o Encryptor.
                ICryptoTransform encryptor = myRijndael.CreateEncryptor(key, IV);

                //Criptografa o dado
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                //Converte o dado em um Array de Bytes
                toEncrypt = textConverter.GetBytes(valor);

                //Escreve todo o dado em Stream e atualiza
                csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
                csEncrypt.FlushFinalBlock();

                //Pega a Criptografia do Array de Bytes
                encrypted = msEncrypt.ToArray();

                return Convert.ToBase64String(encrypted).ToString();
            }
            catch (Exception err)
            {
                return "Erro ao Criptografar(" + err.ToString() + ")";
            }
        }

        public static string Decriptografa(string valor)
        {
            try
            {
                byte[] key;
                byte[] IV;
                byte[] fromEncrypt;

                ASCIIEncoding textConverter = new ASCIIEncoding();
                //RijndaelManaged myRijndael = new RijndaelManaged();
                using var myRijndael = Aes.Create("AesManaged");

                //Seta key e IV.
                key = Encoding.ASCII.GetBytes("tafnersoftwaretafner");
                IV = Encoding.ASCII.GetBytes("tafnersoftwaretafner");

                //Cria um decryptor que use os mesmos Key e IV que o encryptor 
                ICryptoTransform decryptor = myRijndael.CreateDecryptor(key, IV);

                //Pega a Criptografia do Array de Bytes

                //Descifrar agora a mensagem previamente cifrada usando o decryptor obtido na etapa acima. 
                byte[] bytTemp = textConverter.GetBytes(valor.Length.ToString());
                byte[] bytDataToBeDecrypted = Convert.FromBase64String(valor);

                MemoryStream msDecrypt = new MemoryStream(bytDataToBeDecrypted);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                //Convert the data to a byte array.			
                byte[] tmp = textConverter.GetBytes(valor);
                fromEncrypt = new byte[tmp.Length]; //OU AQUI	

                //Le os dados fora do Crypto Stream
                _ = csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                return textConverter.GetString(fromEncrypt).Replace("\0", "");
            }
            catch (Exception err)
            {
                return "Erro ao DeCriptografar(" + err.ToString() + ")";
            }
        }

        public static T CloneObject<T>(this T source)
        {
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        public static string GerarToken()
        {
            string token = "";
            var chars = "0123456789";
            var stringChars = new char[6];
            string finalString;
            var random = new Random();


            // Gerar um novo token
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            finalString = new String(stringChars);
            token = finalString;

            return token;
        }

        //20240701
        public static string GeraRefreshTolkienNumerico(int salt)
        {
            try
            {
                string sSalt = salt.ToString();

                string s4 = sSalt[4].ToString();
                string s5 = sSalt[5].ToString();

                string mid_salt = s4 + s5;
                mid_salt = Shuffle(mid_salt);

                string timestamp = ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
                string mid_stamp = timestamp.Substring(timestamp.Length - 4, 4);

                //3120
                mid_stamp = mid_stamp[3].ToString() + mid_stamp[1].ToString() + mid_stamp[2].ToString() + mid_stamp[0].ToString();

                return mid_salt + mid_stamp;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static string Shuffle(this string str)
        {
            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }
    }
}
