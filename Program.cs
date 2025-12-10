namespace InlämningsUppgift2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            User.Admin(); //Kallar metoden Admin från klassen User
            MenuHelper menu = new MenuHelper(); //Skapar en objekt från klassen MenuHelper och instasierar den
            menu.Menu(); //Kallar Menu Metoden från klassen MenuHelper


        }
    }
}
