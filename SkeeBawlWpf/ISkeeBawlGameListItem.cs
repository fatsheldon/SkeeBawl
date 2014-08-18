namespace SkeeBawlWpf
{
    public  interface ISkeeBawlGameListItem 
    {
        string GameImage { get;}
        ISkeeGame GetGame();
    }
}