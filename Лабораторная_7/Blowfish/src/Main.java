import java.util.Scanner;


public class Main implements Runner{

    public static String encryption(String message)
    {

        int w = 0;
        for(int i = 0; i < 13; i++)
        {
            w = (int) Math.random()*c.length();
            message = message + c.charAt(w);
        }
        return message;
    }


    @SuppressWarnings("resource")
    public static void main(String[] arg)
    {
        Scanner j = new Scanner(System.in);
        System.out.println("message: ");
        String message = j.nextLine();
        Main.encryption(message);

    }
}
