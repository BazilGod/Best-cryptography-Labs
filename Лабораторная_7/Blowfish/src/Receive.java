
public class Receive implements Runner{


    public String decryption(String message)
    {
        for(int i = 0; i < message.length(); i++)
        {
            if(message.substring(i,i+1) == Main.encryption(message).substring(i,i+1))
            {
                message = Main.encryption(message);
            }
        }
        return message;
    }

}
