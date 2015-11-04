namespace LoginTestApp.Crosscutting.Contracts
{
    public interface ICryptoProvider
    {
        string Encrypt(string plainText);

        string Decrypt(string encryptedText);
    }
}