public interface ICryptography
{
    string EncryptString(string plainText);
    string DecryptString(string cipherText);
}